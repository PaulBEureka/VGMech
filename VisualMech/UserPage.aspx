<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="VisualMech.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="stylesheet" href="/Content/custom_styles.css">
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery.signalR-2.4.3.js"></script>
    <script src="signalr/hubs"></script>
    
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>

    <script src="/Content/custom.js"></script>



    <main>
        <div class="container">
            <div class="main-body">
              <div class="row gutters-sm">
                <div class="col-md-4 mb-3">
                  <div class="card_user">
                    <div class="card-body-user">
                      <div class="d-flex flex-column align-items-center text-center">
                        <asp:Literal ID="UserAvatarLit" runat="server"></asp:Literal>


                        <div class="mt-3">
                            <asp:Literal ID="UsernameLit" runat="server"></asp:Literal>
                          

                            <asp:FileUpload ID="customFile" runat="server" CssClass="form-control" Accept=".jpg, .jpeg, .png, .gif" />
                            
                            <asp:Button ID="UploadBtn" Text="Change Avatar" runat="server" OnClick="UploadBtn_Click" CssClass="comment_button my-2 bg-danger" />
                            <br />
                            <asp:Label ID = "lblMessage" Text="Error" runat="server" ForeColor="Green" Visible="false" />
                            

                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="card_user mt-3">
                      <div class="card-body-user">
                        <h6 class="d-flex align-items-center mb-3">Learn Completion Status</h6>
                        <asp:Literal ID="UserProgressLit" runat="server"></asp:Literal>
                        
                      </div>
                  </div>
                </div>
                <div class="col-md-8">
                  <div class="card_user mb-3" id="userInfoDiv">
                    <div class="card-body-user">
                      <asp:Literal ID="UserInfosLit" runat="server"></asp:Literal>
                      <asp:Panel ID="UserInfosEditPanel" runat="server" Visible="false">
                            <div class='row mb-3'>
                                <div class='col-sm-3'>
                                    <h6 class='mb-0'>Username</h6>
                                </div>
                                <div class='col-sm-9 text-secondary'>
                                    <asp:TextBox ID='InputUsername' runat='server' CssClass='form-control'></asp:TextBox>
                                </div>
                            </div>
                            <hr />
                            <div class='row mb-3'>
                                <div class='col-sm-3'>
                                    <h6 class='mb-0'>Email</h6>
                                </div>
                                <div class='col-sm-9 text-secondary'>
                                    <asp:TextBox ID='InputEmail' runat='server' CssClass='form-control'></asp:TextBox>
                                </div>
                            </div>
                            <hr />
                            <div class='row mb-3'>
                                <div class='col-sm-3'>
                                    <h6 class='mb-0'>About Me</h6>
                                </div>
                                <div class='col-sm-9 text-secondary'>
                                    <asp:TextBox ID='InputAboutMe' runat='server' CssClass='form-control'></asp:TextBox>
                                </div>
                            </div>
                            <hr />
                        </asp:Panel>
                        <div class="row">
                          <div class="col-sm-12 d-grid">
                            <asp:Button ID="ConfirmBtn" Text="Confirm Change" runat="server" Visible="false" OnClick="ConfirmBtn_Click" OnClientClick="return confirm('Are you sure you want to commit any changes?');" CssClass="mx-auto comment_button my-2 bg-danger" />
                        </div>
                        </div>
                      <div class="row">
                        <div class="col-sm-12">
                          <asp:Button ID="EditBtn" Text="Edit" runat="server" OnClick="EditBtn_Click" CssClass="comment_button my-2 bg-danger" />
                          <asp:Button ID="CancelEditBtn" Text="Cancel Edit" runat="server" OnClick="EditBtn_ClickBack" Visible="false" CssClass="comment_button my-2 bg-danger" />
                             
                          <asp:Button ID="ChangePassBtn" Text="Change Password" runat="server" OnClick="ChangePassBtn_Click" CssClass="comment_button my-2 bg-danger" />
                                    
                        
                        </div>
                      
                      </div>
                    </div>
                  </div>

                  <div class="row gutters-sm">
                    <div class="col-sm-6 mb-3">
                      <div class="card_user h-100">
                        <div class="card-body-user">
                            <h6 class="d-flex align-items-center mb-3">Recommended New Learn Game Mechanics</h6>
                            <ul class="list-group list-group-flush">
                                
                                <asp:Literal ID="RecomenddedPagesLit" runat="server"></asp:Literal>
                            </ul>
                        </div>
                      </div>
                    </div>
                    <div class="col-sm-6 mb-3">
                      <div class="card_user h-100">
                        <div class="card-body-user">
                            <h6 class="d-flex align-items-center mb-3">Visited Learn Game Mechanics</h6>
                            <ul class="list-group list-group-flush">
                                <asp:Literal ID="VisitedPagesLit" runat="server"></asp:Literal>
                      
                            
                            </ul>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="row gutters-sm">
                    
                    <div class="col-sm-6 mb-3">
                      <div class="card_user h-100">
                        <div class="card-body-user">
                            <h6 class="d-flex align-items-center mb-3">Friends</h6>
                            
                        </div>
                      </div>
                    </div>
                  </div>

                </div>
              </div>

            </div>
        </div>

    </main>
    
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            var learnLinks = document.querySelectorAll('.learn-link');
            learnLinks.forEach(function (learnLink) {
                learnLink.addEventListener('click', function () {
                    var linkId = this.getAttribute('data-card-id');
                    console.log("Card ID: " + linkId);
                    HandleLink(linkId);

                });
            });
        });

        function HandleLink(linkId) {
            PageMethods.ProcessLink(linkId);

        }  

        function changeContent() {
            var firstComment = commentHTML[0];

            $('#userInfoDiv').html(commentHTML[2]);
        }

        document.getElementById('UploadBtn').onclick = function () {
            var fileInput = document.getElementById('customFile');
            var filePath = fileInput.value;
            var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;

            if (!allowedExtensions.exec(filePath)) {
                alert('Please upload only image files (JPEG, PNG, GIF).');
                return false;
            }

            return true;
        };

        
    </script>
    

    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>

    


</asp:Content>
