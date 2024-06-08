<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="VisualMech.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery.signalR-2.4.3.js"></script>
    <script src="signalr/hubs"></script>
    
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>

    <script src="/Content/custom.js"></script>



    <main>
        <div class="container mt-4">
            <div class="main-body">
              <div class="row gutters-sm">
                <div class="col-md-12">
                    <asp:Button ID="ManageBtn" Text="Manage account" runat="server" OnClick="ManageBtn_Click" CssClass="comment_button my-2 bg-danger" />
                    <asp:Button ID="ExitManageBtn" Text="Exit Manage" CausesValidation="false" Visible="false" runat="server" OnClick="ExitManageBtn_Click" CssClass="comment_button my-2 bg-danger" />
                    <asp:Button ID="DeleteAccManageBtn" Text="Delete Account" CausesValidation="false" runat="server" Visible="false" OnClick="DeleteAccManageBtn_Click" CssClass="comment_button my-2 bg-danger" />
                
                </div> 
                  


                <div class="col-md-4 mb-3">
                  <div class="card_user">
                    <div class="card-body-user">
                      <div class="d-flex flex-column align-items-center text-center">
                        <asp:Literal ID="UserAvatarLit" runat="server"></asp:Literal>


                        <div class="mt-3">
                            <asp:Literal ID="UsernameLit" runat="server"></asp:Literal>
                          

                            <asp:FileUpload ID="customFile" runat="server" ClientIDMode="Static" CssClass="form-control" Visible="false" Accept=".jpg, .jpeg, .png, .gif" onchange="handleFileSelect(event)" />
                            
                            <asp:Button ID="UploadBtn" Text="Change Avatar" Visible="false" runat="server" OnClick="UploadBtn_Click" CssClass="comment_button my-2 bg-danger" />
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" Font-Size="Small" runat="server" ControlToValidate="InputUsername" ErrorMessage="Required field" ForeColor="Red"></asp:RequiredFieldValidator>
                                    
                                    <asp:Label ID = "UsernameValidatorlbl" Text="" runat="server" ForeColor="Red" Font-Size="Small" Visible="false" />
                                
                                </div>
                                    
                            </div>
                            <hr />
                            <div class='row mb-3'>
                                <div class='col-sm-3'>
                                    <h6 class='mb-0'>Email</h6>
                                </div>
                                <div class='col-sm-9 text-secondary'>
                                    <asp:TextBox ID='InputEmail' runat='server' ClientIDMode="Static" CssClass='form-control'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" Font-Size="Small" runat="server" ControlToValidate="InputEmail" ErrorMessage="Required field" ForeColor="Red"></asp:RequiredFieldValidator>
                                        
                                    <asp:Label ID = "EmailValidatorlbl" Text="" runat="server" ForeColor="Red" Font-Size="Small" Visible="false" />
                            
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



                      <asp:Panel ID="ChangePasswordPanel" runat="server" Visible="false">
                          <div class='row mb-3'>
                                <div class='col'>
                                    <asp:Button ID="CancelChangePassBtn" CausesValidation="false" Text="Back" runat="server" OnClick="CancelChangePassBtn_Click" CssClass="comment_button my-2 bg-danger" />
                                </div>
                         </div>

                          <asp:Panel ID="VerifyPassPanel" runat="server">
                              <div class='row mb-3'>
                                    <div class='col d-grid'>
                                        <h6 class='mb-0 mx-auto'>To continue, first verify it’s you</h6>
                                    </div>
                             </div>
                              <div class='row mb-3'>
                                    <div class='col-sm-3'>
                                        <h6 class='mb-0'>Enter Password</h6>
                                    </div>
                                    <div class='col-sm-9 text-secondary'>
                                        <asp:TextBox ID='CurrentPasstb' runat='server' CssClass='form-control' TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="CurrentPasstb" ErrorMessage="Required field" ForeColor="Red" Font-Size="Small" CssClass="ml-2" Display="Dynamic"></asp:RequiredFieldValidator>
                                        
                                        <br />
                                        <asp:Label ID = "CurrentPassValidatorLbl" Text="" runat="server" ForeColor="Red" Font-Size="Small" Visible="false" />
                                
                                    </div>
                             </div>
                             <hr />
                             <div class='row mb-3'>
                                    <div class='col d-grid'>
                                        <asp:Button ID="VerifyBtn" Text="Verify" runat="server" OnClick="VerifyBtn_Click" CssClass="mx-auto comment_button my-2 bg-danger" />
                                    </div>
                             </div>

                          </asp:Panel>
                         
                          <asp:Panel ID="NewPasswordPanel" runat="server" Visible ="false">
                              <div class='row mb-3'>
                                    <div class='col d-grid'>
                                        <h6 class='mb-0 mx-auto'>Choose a strong password and don't reuse it for other accounts.</h6>
                                    </div>
                             </div>
                             <div class='row mb-3'>
                                    <div class='col-sm-3'>
                                        <h6 class='mb-0'>New Password</h6>
                                    </div>
                                    <div class='col-sm-9 text-secondary'>
                                        <asp:TextBox ID='NewPasswordTb' TextMode="Password" runat='server' CssClass='form-control'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" Font-Size="Small" runat="server" ControlToValidate="NewPasswordTb" ErrorMessage="Required field" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" Display="Dynamic" runat="server"
                                            ControlToValidate="NewPasswordTb"
                                            ErrorMessage="Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and be at least 8 characters long."
                                            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"
                                            ForeColor="Red"
                                            Font-Size="Small">

                                        </asp:RegularExpressionValidator>
                                    </div>
                             </div>
                             <hr />
                             <div class='row mb-3'>
                                    <div class='col-sm-3'>
                                        <h6 class='mb-0'>Confirm New Password</h6>
                                    </div>
                                    <div class='col-sm-9 text-secondary'>
                                        <asp:TextBox ID='ConNewPasswordTb' TextMode="Password" runat='server' CssClass='form-control'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" Font-Size="Small" ControlToValidate="ConNewPasswordTb" ErrorMessage="Required field" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <br />
                                        <asp:CompareValidator ID="CompareValidatorPassword" Display="Dynamic" runat="server"
                                            ControlToValidate="ConNewPasswordTb"
                                            ControlToCompare="NewPasswordTb"
                                            ErrorMessage="Passwords do not match."
                                            ForeColor="Red"
                                            Font-Size="Small">

                                        </asp:CompareValidator>
                                    </div>
                             </div>
                             <hr />
                             <div class='row mb-3'>
                                <div class='col d-grid'>
                                    <asp:Button ID="UpdatePassBtn" Text="Change Password" runat="server" OnClick="UpdatePassBtn_Click" CssClass="mx-auto comment_button my-2 bg-danger" />
                                </div>
                             </div>
                             <div class='row mb-3'>
                                    <div class='col d-grid'>
                                        <asp:Label ID = "PassUpdateLbl" CssClass="mx-auto" Text="" runat="server" ForeColor="Green" Font-Size="Medium" Visible="false" />
                                
                                    </div>
                             </div>
                          </asp:Panel>

                      </asp:Panel>

                      <asp:Panel ID="DeleteAccPanel" runat="server" Visible="false">
                            
                          <asp:Panel ID="VerifyDeletePanel" runat="server">
                       
                              <div class='row mb-3'>
                                        <div class='col d-grid'>
                                            <h6 class='mb-0 mx-auto'>To continue, first verify it’s you</h6>
                                        </div>
                                 </div>
                                <div class='row mb-3'>
                                        <div class='col-sm-3'>
                                            <h6 class='mb-0'>Enter Password</h6>
                                        </div>
                                        <div class='col-sm-9 text-secondary'>
                                            <asp:TextBox ID='VerifyPassDelTb' runat='server' TextMode="Password" CssClass='form-control'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="VerifyPassDelTb" ErrorMessage="Required field" ForeColor="Red" Font-Size="Small" CssClass="ml-2" Display="Dynamic"></asp:RequiredFieldValidator>
                                        
                                            <br />
                                            <asp:Label ID = "VerifyPassValidatorLbl" Text="" runat="server" ForeColor="Red" Font-Size="Small" Visible="false" />
                                
                                        </div>
                                 </div>
                                 <hr />
                                 <div class='row mb-3'>
                                        <div class='col d-grid'>
                                            <asp:Button ID="VerifyPassDelBtn" Text="Verify" runat="server" OnClick="VerifyPassDelBtn_Click" CssClass="mx-auto comment_button my-2 bg-danger" />
                                        </div>
                                 </div>
                           </asp:Panel>
                      
                          <asp:Panel ID="ConfirmDeletePanel" runat="server" Visible="false">
                       
                              <div class='row mb-3'>
                                        <div class='col'>
                                            <h5 class='mb-0 fw-bold'>Please read this carefully</h5>
                                        </div>
                                 </div>
                                <div class='row mb-3'>
                                        <div class='col'>
                                            <p class='mb-0'>You're trying to delete your VGMech account, all progress, comments, email notifications, and 
                                                associated contents. You'll no longer be able to access any of those information, and your account and data will be lost.
                                            </p>
                                        </div>
                                        
                                 </div>
                                 <br />
                                 <div class='row mb-3'>
                                        <div class='col form-check' >
                                            <asp:CheckBox runat="server" ID="AckDelCheckbox" ClientIDMode="Static" Text="Yes, I want to permanently delete this VGMech Account and all its data."/>
                                            <br />
                                            <asp:CustomValidator ID="AccDelValidator" runat="server" 
                                                ErrorMessage="Please confirm that you wish to delete this VGMech Account."
                                                Font-Size="Small" ForeColor="Red"
                                                ClientValidationFunction = "ValidateCheckBox"></asp:CustomValidator>
                                        </div>
                                        
                                 </div>
                                 <hr />
                                 <div class='row mb-3'>
                                        <div class='col d-grid'>
                                            <asp:Button ID="DeleteAccBtn" Text="Delete Account" runat="server" OnClick="DeleteAccBtn_Click" CssClass="mx-auto comment_button my-2 bg-danger" />
                                        </div>
                                 </div>
                           </asp:Panel>
                      </asp:Panel>
                        <div class="row">
                            <div class="col-sm-12 d-grid">
                                <asp:Label ID = "OTPlbl" Text="Confirm email with the OTP sent" runat="server" ForeColor="Black" Visible="false" CssClass="mx-auto" />
                                <asp:TextBox ID='OTPtb' runat='server' CssClass='form-control mx-auto' Placeholder="Type OTP here" Visible ="false" ></asp:TextBox>
                                <asp:Label ID = "OTPValidator" Text="Invalid OTP" runat="server" ForeColor="Red" Font-Size="Small" Visible="false" CssClass="mx-auto" />
                                <asp:Button ID="ConfirmBtn" Text="Confirm Change" runat="server" Visible="false" OnClick="ConfirmBtn_Click" CssClass="mx-auto comment_button my-2 bg-danger" />
                            </div>
                        </div>
                      <div class="row">
                        <div class="col-sm-12">
                          <asp:Button ID="EditBtn" Text="Edit" runat="server" OnClick="EditBtn_Click" CssClass="comment_button my-2 bg-danger" Visible="false"/>
                          <asp:Button ID="CancelEditBtn" Text="Cancel Edit" runat="server" OnClick="EditBtn_ClickBack" Visible="false" CssClass="comment_button my-2 bg-danger" />
                             
                          <asp:Button ID="ChangePassBtn" Text="Change Password" runat="server" OnClick="ChangePassBtn_Click" Visible="false" CssClass="comment_button my-2 bg-danger" />
                                    
                        
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
                            <ul class="list-group list-group-flush visited-pages-custom">
                                <asp:Literal ID="VisitedPagesLit" runat="server"></asp:Literal>
                      
                            
                            </ul>
                        </div>
                      </div>
                    </div>

                    <div class="col-sm-12 mb-3">
                        <div class="card_user h-100">
                            <div class="card-body-user">
                                <h6 class="d-flex align-items-center mb-3">Badges Earned</h6>
                                <div class="justify-content-evenly badge-container">
                                    <asp:Literal ID="GainedBadgesLit" runat="server"></asp:Literal>
                                    

                                    
                                </div>
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


        function handleFileSelect(event) {
            var fileInput = event.target; // Get the input element that triggered the event
            var file = fileInput.files[0]; // Get the selected file

            var fileName = file.name.toLowerCase();
            // Check if the file extension is allowed
            if (!(/\.(jpg|jpeg|png|gif)$/i).test(fileName)) {
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

                toastr['error']('Please select only JPG, JPEG, PNG, or GIF files.', 'Error');
                // Clear the selected files if needed
                fileInput.value = '';
                return;
            }
            else {
                var reader = new FileReader();

                reader.onload = function (event) {
                    document.getElementById('avatarImg').src = event.target.result;
                    // Show the image preview
                    document.getElementById('avatarImg').style.display = 'block';
                };

                // Read the file as a data URL
                reader.readAsDataURL(file);
            }
        }


        function ValidateCheckBox(sender, args) {
            if (document.getElementById("AckDelCheckbox").checked == true) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }

        

        
    </script>
    

    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>

    


</asp:Content>
