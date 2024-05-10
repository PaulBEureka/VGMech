<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SamplePage.aspx.cs" Inherits="VisualMech.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery.signalR-2.4.3.js"></script>
    <script src="signalr/hubs"></script>
    
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>

    


    <script src="/Content/custom.js"></script>

    <main >
        
        

        
        <section class="gameMech-bgColor">
            <asp:Literal ID="gameMechLit" runat="server"></asp:Literal>
            <!-- Comment Section layout -->

            <div class="row m-auto gameMech-layout-padding mt-5">
                <div class="container d-grid">
                    <div class="gameMech-outer-box m-auto d-grid">
                        <div class="container m-0 mx-auto d-grid">
                            <div class="row ">
                                <h4 class="fw-bolder">Share your thoughts</h4>
                               
                            </div>
                            <div>
                                <hr />
                            </div>

                            <div class="row container d-grid">
                                    
                                                    

                                    <% if (Session["CurrentUser"] != null) { %>
                                    <div class="col-5 mb-1 d-flex d-grid">
                                        <img src="<%# Session["CurrentAvatarPath"] ?? "Images/person_icon.png" %>" alt="" class="rounded-circle" width="40" height="40">
                                        <h5 class="ms-3 my-auto"><%# Session["CurrentUser"] ?? "Default Name" %></h5>
                                    </div>

                                    
                                    
                                    <br />
                                    <textarea name="msg" placeholder="Type your comment here" id="commentbox" rows="5" class="form-control" style="background-color: white; resize: none;" draggable="false"></textarea>
                                    

                                    <% } %>
                                    
                                    <% if (Session["CurrentUser"] == null) { %>
                                    <button type="button" id="post_disbled" class="comment_button mx-auto my-2 bg-danger" onclick="sign_in_comment()">Sign in</button>
                                        <% } else { %>
                                    <button type="button" id="post" class="comment_button my-2 bg-danger" onclick="post_Click()">Post Comment</button>
                                    <% } %>
                            </div>

                            <hr />
                            <div class="container">
                                <div class="row justify-content-md-start align-items-center">
                                    <div class="col-lg-2 fw-bolder" id="commentCountDiv">
                                        
                                    </div>
                                    <div class="col-lg-5" id="sortByDiv">
                                        
                                    </div>
                                </div>
                            </div>


                            <section>
                                <div class="container"> 
                                    

                                    <div class="row">
                                        <div class="col pb-4 comment-section-size shadow">
                                            
                                            <div id="commentSection" class="d-grid">
                                                <!-- Existing comments will be dynamically added here -->
                                                    <div class="spinner-border text-danger m-auto mt-5" role="status">
                                                        <span class="visually-hidden">Loading...</span>
                                                    </div>
                                                
                                            </div>
                                            
                                        </div>
                                        
                                    </div>
                                </div>
                            </section>





                        </div>
                    </div>
                </div>
            </div>


        </section>
                    

        

        <!-- Modal -->
        <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
          <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="deleteModalLabel">Confirm Delete</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                <p id="commentToDelete"></p>
                Are you sure you want to delete this comment permanently?
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                <button type="button" class="btn btn-danger" id="confirmDelete">Delete</button>
              </div>
            </div>
          </div>
        </div>

    </main>

    <script>

        

        function onContentLoaded() {
            var currentPopover = null;

            var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
            var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
                var popover = new bootstrap.Popover(popoverTriggerEl, {
                    trigger: 'hover' 
                });

                popoverTriggerEl.addEventListener('shown.bs.popover', function () {
                    if (currentPopover && currentPopover !== popover) {
                        currentPopover.hide();
                    }
                    currentPopover = popover;
                });

                popoverTriggerEl.addEventListener('hidden.bs.popover', function () {
                    currentPopover = null;
                });

                return popover;
            });


            var commentIdToDelete; 

            $(".deleteOption").click(function () {
                commentIdToDelete = $(this).data("comment-id");
                $("#deleteModal").modal("show");
            });

            $("#confirmDelete").click(function () {
                PageMethods.DeleteComment(commentIdToDelete, onSuccess5, onError5);
                $("#deleteModal").modal("hide");
            });
        }



        var chat = $.connection.myHub;

        function updateComments(cardTitle) {
            chat.server.updateComments(cardTitle)
                .done(function () {
                    console.log("Comments updated successfully.");
                })
                .fail(function (error) {
                    console.error("Error updating comments: " + error);
                });
        }

        function updateCommentsOrder(cardTitle) {
            chat.server.updateCommentsOrder(cardTitle)
                .done(function () {
                    console.log("Comments updated successfully.");
                })
                .fail(function (error) {
                    console.error("Error updating comments: " + error);
                });
        }

        chat.client.sendComments = function (commentHTML) {
            var firstComment = commentHTML[0]; 
            var secondComment = commentHTML[1]; 

            // Update the comments on the webpage
            $('#commentSection').html(commentHTML[0]);
            $('#commentCountDiv').html(commentHTML[1]);
            $('#sortByDiv').html(commentHTML[2]);

            onContentLoaded();
        };

        chat.client.updateCommentsOrder = function (commentHTML) {
            var firstComment = commentHTML[0];
            var secondComment = commentHTML[1];

            $('#commentSection').html(commentHTML[0]);
            $('#commentCountDiv').html(commentHTML[1]);
            $('#sortByDiv').html(commentHTML[2]);

        };




        $.connection.hub.start().done(function () {
            console.log("SignalR connected.");

            PageMethods.get_Comments(onSuccess3);

        });

        function onSuccess3(response) {
            updateComments(response);
        }


        function post_Click() {
            var message = document.getElementById('commentbox').value; 

            PageMethods.post_Click(message, onSuccess, onError);
            document.getElementById('commentbox').value = null;
        }

        function onSuccess(response) {
            PageMethods.get_Comments(onSuccess3);

            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "10000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }

            toastr['success']('Your comment was posted successfully', 'Comment Posted');
        }
        
        

        function onError(response) {
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "10000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }

            toastr['error']('An error occurred while posting your comment', 'Error');
        }

        function reply_Click(parentCommentId) {
            var parentString = "replybox-" + parentCommentId.toString();
            var message = document.getElementById(parentString).value; 

            PageMethods.reply_Click(parentCommentId, message, onSuccess, onError);
            document.getElementById(parentString).value = null;
        }

        function onSuccess4(response) {
            updateCommentsOrder(response)
        }

        function onSuccess5(response) {
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "10000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }

            toastr['success'](response);
            PageMethods.get_Comments(onSuccess3);
        }

        function onError5(response) {
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "10000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }

            toastr['error'](response);
        }


        function onSuccess2(response) {
       
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "10000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }

            toastr['info']('Order of comment changed to: ' + response);

            PageMethods.get_Comments(onSuccess4);
            
        }



        function onError2(response) {
            toastr.options = {
                "closeButton": false,
                "debug": false,
                "newestOnTop": false,
                "progressBar": false,
                "positionClass": "toast-top-right",
                "preventDuplicates": false,
                "onclick": null,
                "showDuration": "300",
                "hideDuration": "1000",
                "timeOut": "10000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }

            toastr['error']('An error occurred while changing comment order', 'Error');
        }


        function innerReply_Click(parentCommentId, commentID) {
            var parentString = "replybox-" + commentID.toString();
            var message = document.getElementById(parentString).value; 

            PageMethods.innerReply_Click(parentCommentId, message, onSuccess, onError);
            document.getElementById(parentString).value = null;
        }

        function handleItemClick(order) {
            PageMethods.changeCommentOrder(order, onSuccess2, onError2);
        }


        function toggleReplies(commentId) {
            var button = document.getElementById("toggle-replies-btn-" + commentId);
            var container = document.getElementById("reply-container-" + commentId);

            // Toggle aria-expanded attribute
            var expanded = button.getAttribute("aria-expanded") === "true" || false;
            button.setAttribute("aria-expanded", !expanded);

            // Toggle aria-hidden attribute
            var hidden = container.getAttribute("aria-hidden") === "true" || false;
            container.setAttribute("aria-hidden", !hidden);
        }

        function toggleRespond(commentId) {
            var button = document.getElementById("toggle-respond-btn-" + commentId);
            var container = document.getElementById("respond-container-" + commentId);

            // Toggle aria-expanded attribute
            var expanded = button.getAttribute("aria-expanded") === "true" || false;
            button.setAttribute("aria-expanded", !expanded);

            // Toggle aria-hidden attribute
            var hidden = container.getAttribute("aria-hidden") === "true" || false;
            container.setAttribute("aria-hidden", !hidden);
        }
        

    </script>


    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>
</asp:Content>
