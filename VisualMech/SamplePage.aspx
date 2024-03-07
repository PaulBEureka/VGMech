<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SamplePage.aspx.cs" Inherits="VisualMech.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="stylesheet" href="/Content/custom_styles.css">
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery.signalR-2.4.3.js"></script>
    <script src="signalr/hubs"></script>
    
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>

    


    <script src="/Content/custom.js"></script>

    <main >
        <asp:Literal ID="gameMechLit" runat="server"></asp:Literal>

        <section class="gameMech-bgColor">
            <!-- Comment Section layout -->

            <div class="row m-auto gameMech-layout-padding">
                <div class="container d-grid">
                    <div class="gameMech-outer-box m-auto d-grid">
                        <div class="container m-0 mx-auto d-grid">
                            <div class="row ">
                                <h4 class="fw-bolder">Share your thoughts</h4>
                            </div>
                            <div>
                                <hr />
                            </div>

                            <!--Per Comment Layout -->
                            <div class="row container d-grid">
                                    
                                                    

                                    <% if (Session["CurrentUser"] != null) { %>
                                    <div class="col-5 mb-1 d-flex d-grid">
                                        <img src="Images/person_icon.png" alt="" class="rounded-circle" width="40" height="40">
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
                                        <div class="col pb-4 comment-section-size">
                                            
                                            <div id="commentSection">
                                                <!-- Existing comments will be dynamically added here -->
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
                    

        
    </main>

    <script>
        $(function () {
            var chat = $.connection.myHub;

            // Function to update comments
            function updateComments(cardTitle) {
                chat.server.updateComments(cardTitle)
                    .done(function () {
                        console.log("Comments updated successfully.");
                    })
                    .fail(function (error) {
                        console.error("Error updating comments: " + error);
                    });
            }

            // Handle the updateComments message from the server
            chat.client.updateComments = function (commentHTML) {
                var firstComment = commentHTML[0]; 
                var secondComment = commentHTML[1]; 

                // Update the comments on the webpage
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

            
        });

        function handleItemClick(order) {
            PageMethods.changeCommentOrder(order, onSuccess4);
        }

        function onSuccess4(response) {
            window.location.href = window.location.pathname; // Redirect to the same page
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
