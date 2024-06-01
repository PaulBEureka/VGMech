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

            <div class="row d-grid mt-3">
                <div class="row justify-content-center gameMech-layout-padding m-auto">

                        <asp:Literal ID="gameMechInfoLit" runat="server"></asp:Literal>

                        <div class="col-md-6 gameMech-section-holders mx-md-3 my-3">
                            <div class="container bg-white m-0 mx-auto d-grid">
                                <div class="row pt-3">
                                    <h4 class="fw-bolder">Share your thoughts</h4>
                                </div>
                                <hr />

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
                                        <div class="col-6 text-start fw-bolder" id="commentCountDiv">
                                        
                                        </div>
                                        <div class="col-6 text-end" id="sortByDiv">
                                        
                                        </div>
                                    </div>
                                </div>


                                <section>
                                    <div class="container"> 
                                        <div class="row pb-4">
                                            <div class="col comment-section-size shadow">
                                                <div id="commentSection" class="d-grid py-3">
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
                <button type="button" class="btn btn-danger" onclick="delete_Click()">Delete</button>
              </div>
            </div>
          </div>
        </div>

    </main>

    <script>
        var isValidUpdate = "0";
        var commentIdToDelete;
        var pageContext = null;
        var mainCommentStreamPage
        var chat = $.connection.myHub;

        PageMethods.GetCardTitle(onSuccessOrder);

        function onSuccessOrder(response) {
            pageContext = response;
            
            $.connection.hub.qs = { "page": pageContext }; 
        }

        

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

            

            $(".deleteOption").click(function () {
                    commentIdToDelete = $(this).data("comment-id");
                    $("#deleteModal").modal("show");
            });

            
        }


        function delete_Click() {
            PageMethods.DeleteComment(commentIdToDelete, onSuccess5);
            $("#deleteModal").modal("hide");
        }

        $.connection.hub.start().done(function () {
            console.log("SignalR connected.");

            PageMethods.get_Comments(fetchComments);

        });

        function fetchComments(pageDetails) {
            chat.server.fetchComments(pageDetails);
        }

        chat.client.fetchCommentSolo = function (commentHTML) {
            if (isValidUpdate == "0") {

                // Update the comments on the webpage
                $('#commentSection').html(commentHTML[0]);
                $('#commentCountDiv').html(commentHTML[1]);
                $('#sortByDiv').html(commentHTML[2]);
                PageMethods.SetOffset(commentHTML[3]);
                onContentLoaded();
            }
        };




        function loadMoreCommentsHub(pageDetails) {
            chat.server.loadMoreComments(pageDetails);
        }

        chat.client.loadMoreSolo = function (commentHTML) {
           
            $('#insertNextCommentDiv').remove();
            $('#commentSection').append(commentHTML[0]);

            PageMethods.SetOffset(commentHTML[3]);
            onContentLoaded();
            
        };

        function loadMoreRepliesHub(pageDetails) {
            chat.server.loadMoreReplies(pageDetails);
        }

        chat.client.loadMoreRepliesSolo = function (commentHTML) {
            var replyContainer = document.getElementById("inner-reply-" + commentHTML[1]);
            var insertDiv = document.getElementById('insertNextCommentDiv-' + commentHTML[1]);
            var initialSpinner = document.getElementById('initial-spinner-' + commentHTML[1]);

            if (initialSpinner != null) {
                $(initialSpinner).remove();
            }
            else {
                $(insertDiv).remove();
            }

            $(replyContainer).append(commentHTML[0]);

            PageMethods.SetReplyOffset(commentHTML[1], commentHTML[2]);
            onContentLoaded();
            
        };



        function sendComment(cardTitle) {
            chat.server.callForGroupUpdate(cardTitle);
        }

        chat.client.getCommentGroup = function () {

            PageMethods.get_Comments(fetchComments);
        };


        function updateCommentsOrder(cardTitle) {
            chat.server.updateCommentsOrder(cardTitle);
        }

        chat.client.updateCommentsOrder = function (commentHTML) {
            var firstComment = commentHTML[0];
            var secondComment = commentHTML[1];

            $('#commentSection').html(commentHTML[0]);
            $('#commentCountDiv').html(commentHTML[1]);
            $('#sortByDiv').html(commentHTML[2]);
            PageMethods.SetOffset(commentHTML[3]);

            onContentLoaded();
        };



        function post_Click() {
            var message = document.getElementById('commentbox').value; 

            PageMethods.post_Click(message, onSuccess, onError);
            document.getElementById('commentbox').value = null;
        }

        function onSuccess(response) {
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

            if (response == null) {
                toastr['info']('Please type in your comment', 'Message empty');
            }
            else {
                toastr['success']('Comment posted successfully', 'Comment Posted');
                isValidUpdate = "0";

                
                sendComment(pageContext);
            }

            
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

        function onSuccess5(response) {
            isValidUpdate = "0";

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
            sendComment(pageContext);
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

            PageMethods.get_Comments(updateCommentsOrder);
            
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

            PageMethods.reply_Click(parentCommentId, message, onSuccess, onError);
            document.getElementById(parentString).value = null;
            
        }

        function handleItemClick(order) {
            PageMethods.changeCommentOrder(order, onSuccess2, onError2);
        }

        function toggleReplies(commentId) {
            var button = document.getElementById("toggle-replies-btn-" + commentId);
            var icon = document.getElementById("toggle-replies-btn-icon-" + commentId);
            var container = document.getElementById("reply-container-" + commentId);
            var replyContainer = document.getElementById("inner-reply-" + commentId);
                

            var expanded = button.getAttribute("aria-expanded") === "true" || false;
            button.setAttribute("aria-expanded", !expanded);

            const spinnerElement = replyContainer.querySelector(".spinner-border");

            if (icon.classList.contains('fa-chevron-down')) {
                icon.classList.remove('fa-chevron-down');
                icon.classList.add('fa-chevron-up');

                //If there is a spinner element, no replies have been loaded yet so perform an initial load upon toggle
                if (spinnerElement) {
                    //Get the information from the server regarding the offset and other page data, then pass to the signalR Hub
                    PageMethods.get_Replies(commentId, loadMoreRepliesHub) 
                } 
            }
            else if (icon.classList.contains('fa-chevron-up')) {
                icon.classList.remove('fa-chevron-up');
                icon.classList.add('fa-chevron-down');
            }

            if (expanded == false) {
                isValidUpdate = null;
            }
            else {
                isValidUpdate = "0";
            }

            // Toggle aria-hidden attribute
            var hidden = container.getAttribute("aria-hidden") === "true" || false;
            container.setAttribute("aria-hidden", !hidden);
        }

        function toggleRespond(commentId) {
            var button = document.getElementById("toggle-respond-btn-" + commentId);
            var container = document.getElementById("respond-container-" + commentId);
            var container = document.getElementById("respond-container-" + commentId);
            var replyBox = document.getElementById("replybox-" + commentId);

            //Clear previous contents
            replyBox.value = null;

            // Toggle aria-expanded attribute
            var expanded = button.getAttribute("aria-expanded") === "true" || false;
            button.setAttribute("aria-expanded", !expanded);

            if (expanded == false) {
                isValidUpdate = null;
            }
            else {
                isValidUpdate = "0";
            }


            // Toggle aria-hidden attribute
            var hidden = container.getAttribute("aria-hidden") === "true" || false;
            container.setAttribute("aria-hidden", !hidden);
        }

        


        // Function to load more comments
        function loadMoreComments() {
            const spinnerHTML = `
                <div class="spinner-border text-danger m-auto text-center" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            `;
            $('#loadMoreDiv').html(spinnerHTML);
            PageMethods.get_Comments(loadMoreCommentsHub);
        }

        // Function to load more comments
        function loadMoreReplies(commentId) {
            const spinnerHTML = `
                <div class="spinner-border text-danger m-auto text-center" role="status">
                    <span class="visually-hidden">Loading...</span>
                </div>
            `;

            $('#loadMoreReplies-' + commentId).html(spinnerHTML);
            PageMethods.get_Replies(commentId, loadMoreRepliesHub) 
        }
        



    </script>


    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>
</asp:Content>
