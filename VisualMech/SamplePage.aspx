<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SamplePage.aspx.cs" Inherits="VisualMech.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="stylesheet" href="/Content/custom_styles.css">
    <script src="/Content/custom.js"></script>
    <script src="Scripts/jquery.signalR-2.4.3.min.js"></script>
    <script src="signalr/hubs"></script>

    <main >
        <asp:Literal ID="gameMechLit" runat="server"></asp:Literal>

        <section class="gameMech-bgColor">
            <!-- Comment Section layout -->

            <div class="row m-auto gameMech-layout-padding">
                <div class="container gameMech-comment-layout d-grid">
                    <div class="gameMech-outer-box m-auto d-grid">
                        <div class="container m-0 mx-auto">
                            <div class="row ">
                                <h4 class="fw-bolder">Share your thoughts</h4>
                            </div>
                            <div>
                                <hr />
                            </div>

                            <!--Per Comment Layout -->

                            <section>
                                <div class="container"> 
                                    <div class="row">
                                        <div class="col-sm-5 col-md-6 col-12 pb-4 comment-section-size">
                                            
                                            <asp:Literal ID="CommentHtml" runat="server"></asp:Literal>
                                        </div>
                                        

                                        <div class="col-lg-4 col-md-5 col-sm-4 offset-md-1 offset-sm-1 col-12 mt-4">
                                                <div class="form-group">
                                                    <h4>Leave a comment</h4>
                                                    

                                                    <% if (Session["CurrentUser"] != null) { %>
                                                    <label for="message">Message</label>
                                                    <textarea name="msg" id="commentbox" cols="30" rows="5" class="form-control" style="background-color: white; resize: none; height:400px;" draggable="false"></textarea>
                                                    
                                                    
                                                    <% } %>
                                                
                                                </div>
                                                <div>
                                                    <% if (Session["CurrentUser"] == null) { %>
                                                    <button type="button" id="post_disbled" class="comment_button my-2 bg-danger" onclick="sign_in_comment()">Sign in to comment</button>
                                                     <% } else { %>
                                                    <button type="button" id="post" class="comment_button my-2 bg-danger" onclick="post_Click()">Post Comment</button>
                                                    <% } %>
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




    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>
</asp:Content>
