<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignInPage.aspx.cs" Inherits="VisualMech.SignInPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    
    <script src="/Content/custom.js"></script>
    <main>





        <asp:HiddenField ID="EmailHidden" runat="server" ClientIDMode="Static" Value="" />

        <asp:Panel runat="server" ID="SignInPanel" Visible="true">
            <div class="row align-self-center mx-auto signin_rectangle d-grid my-5">
                <div class="d-grid m-auto">                        
                    <img src="Images/VGM_logo.png" alt="imgpng" class="img-fluid m-auto" style="width:100px; height:100px" />
                    <h5 style="color: white; padding-bottom:10px; text-align:left" class="m-auto">Username: <asp:TextBox ID="Username_tb" runat="server" Width="348px" CssClass="m-auto form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="m-auto" ControlToValidate="Username_tb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                        <asp:Label Text ="No Username found" Visible="false" Font-Size="Small" ID="No_Username_lbl" CssClass="m-auto" runat="server"></asp:Label>
                    </h5>                
                    <h5 style="color: white; padding-bottom:10px; text-align:left" class="m-auto">Password: <asp:TextBox ID="Password_tb" runat="server" Width="348px" TextMode="Password" CssClass="m-auto form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="m-auto" ControlToValidate="Password_tb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                        <asp:Label Text ="Incorrect Password" Visible="false" Font-Size="Small" CssClass="m-auto" ID="Incorrect_lbl" runat="server"></asp:Label>
                    </h5>
                        
                    <div class=" align-content-center text-center m-auto">
                        <asp:Image ID="captchaImage" runat="server" Height="40px" Width="150px" ImageUrl="~/Captcha.aspx" /><br />
                        <br />
                        <asp:TextBox ID="captchacode" runat="server" Placeholder="Enter Captcha code" CssClass="form-control"></asp:TextBox><br />
                        <asp:Label ID="lblCaptchaErrorMsg" runat="server" Text="" Font-Size="Small"></asp:Label><br />
                    </div>
                    <br />
                        
                    <asp:Button class="signin_button m-auto" ID="Login_btn" runat="server" Text="Login" OnClick="Login_btn_Click"/>
                        
                    <br />
                    <div class="row">
                        <div class="col-6 text-start">
                            <asp:Button class="white-link no_bg border-0" runat="server" Text="Forgot Password?" CausesValidation="false" ID="ForgotPassBtn" OnClick="ForgotPassBtn_Click"></asp:Button>
                        </div>
                        <div class="col-6 text-end">
                            <a class="white-link" runat="server" href="SignUpPage.aspx" >Sign Up</a>
                        </div>

                    </div>
                </div>
            </div>

        </asp:Panel>
       
        <asp:Panel runat="server" ID="ForgotPasswordPanel" Visible="false">

            <div class="row align-self-center mx-auto signin_rectangle d-grid my-5">
                <div class="d-grid m-auto">                        
                    <h5 class="text-white pt-3 text-center">Enter your account email</h5>
                    <hr class="text-white"/>
                    <br />
                    <h5 style="color: white; padding-bottom:10px; text-align:left" class="m-auto">Email: <asp:TextBox ID="EmailTb" runat="server" Width="348px" CssClass="m-auto form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="m-auto" ControlToValidate="EmailTb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="EmailValidator" Display="Dynamic" runat="server"
                            ControlToValidate="EmailTb" 
                            ErrorMessage="Invalid email format"
                            ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                            ForeColor="White" CssClass="m-auto"
                            Font-Size="Small">
                        </asp:RegularExpressionValidator>
                        <br />
                        
                        
                        
                    </h5>   

                    <div class="row">
                        <div class="col text-center">
                            <asp:Label Text ="" Visible="false" Font-Size="Small" ID="CustomValidator" CssClass="m-auto fw-bold text-white" runat="server"></asp:Label>
                    
                        </div>
                    </div>  
                    <div class="row py-2">
                        <asp:Button class="signin_button m-auto" ID="ResendOTPBtn" runat="server" ClientIDMode="Static" Text="Send OTP (Limit - 5/5)" UseSubmitBehavior="false" OnClientClick="DisableButtons();" OnClick="ResendOTPBtn_Click"/>
                        <asp:HiddenField ID="hfCountdownValue" runat="server" ClientIDMode="Static" Value="5" />
                    </div>
                    
                    <asp:Panel runat="server" ID="OTPCheckPanel" Visible="false">
               
                        <div class="row py-2">
                            <div class="col-12 text-center d-grid">
                                <p class="text-white">Check your email for the OTP</p>
                            </div>
                            <div class="col-12 text-center d-grid">
                                <asp:TextBox ID="OTPtb" runat="server" CssClass="m-auto" Placeholder="Type OTP here"></asp:TextBox>
                            </div>
                            <div class="col-12 text-center d-grid py-3">
                                <asp:Button class="signin_button m-auto" ID="ConfirmOTPBtn" runat="server" ClientIDMode="Static" Text="Confirm OTP" OnClick="ConfirmOTPBtn_Click"/>
                            </div>

                        
                        </div>
                    </asp:Panel>
                    
                </div>
            </div>
            
        </asp:Panel>


        <asp:Panel runat="server" ID="ChangePasswordPanel" Visible="false">

            <div class="row align-self-center mx-auto signin_rectangle d-grid my-5">
                <div class="d-grid m-auto">                        
                    <h5 class="text-white text-center pt-3">Change Password</h5>
                    <hr class="text-white"/>
                    <br />
                    <h5 class="m-auto" style="color: white; padding-bottom:10px; text-align:left">New Password: <asp:TextBox ID="NewPassTb" ClientIDMode="Static" CssClass="form-control m-auto" runat="server" Width="348px" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPassWordRequiredValidator" CssClass="m-auto" runat="server" Display="Dynamic" ControlToValidate="NewPassTb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                    </h5> 
                             
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server" Display="Dynamic"
                        ControlToValidate="NewPassTb"
                        ErrorMessage="Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and be at least 8 characters long."
                        ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"
                        ForeColor="White" CssClass="pb-2 text-center"
                        Font-Size="Small"
                        ClientIDMode="Inherit">

                    </asp:RegularExpressionValidator>  


                    <h5 class="m-auto" style="color: white; padding-bottom:10px; text-align:left">Confirm Password: <asp:TextBox ID="Confirm_Password_tb" CssClass="form-control m-auto" runat="server" Width="348px" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmRequiredValidator" CssClass="m-auto" runat="server" Display="Dynamic" ControlToValidate="Confirm_Password_tb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidatorPassword" runat="server" Display="Dynamic"
                            ControlToValidate="Confirm_Password_tb"
                            ControlToCompare="NewPassTb"
                            ErrorMessage="Passwords do not match."
                            ForeColor="White" CssClass="m-auto"
                            Font-Size="Small">

                        </asp:CompareValidator>
                    </h5>

                    <div class="row py-2">
                        <asp:Button class="signin_button m-auto" ID="ChangePassBtn" runat="server" Text="Change Password" OnClick="ChangePassBtn_Click"/>
                    </div>
                    
                </div>
            </div>
            
        </asp:Panel>



        <div class="modal fade" id="confirmModal" tabindex="-1" aria-labelledby="confirmModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="confirmModalLabel">Confirm New Password</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        Are you sure about your new password?
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="CancelChangeBtn" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <asp:Button runat="server" ID="ProceedChangeBtn" ClientIDMode="Static" class="btn btn-danger"  OnClientClick="Change_Click()" Text="Update" UseSubmitBehavior="false" CausesValidation="false"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
        
    </main>


    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>

    <script>

        function updateCountdown() {
            var btn = document.getElementById('ResendOTPBtn');
            var hiddenField = document.getElementById('hfCountdownValue');
            var currentValue = parseInt(hiddenField.value);

            if (currentValue > 0) {
                btn.value = currentValue - 1; 
                hiddenField.value = currentValue - 1; 
            }
        }

        function Change_Click() {
            var newPass = document.getElementById('NewPassTb').value;
            var userEmail = document.getElementById('EmailHidden').value;
            var ProceedChangeBtn = document.getElementById('ProceedChangeBtn');

            ProceedChangeBtn.value = "Updating...";

            $('.btn').prop('disabled', true);

            PageMethods.ChangePassByEmail(userEmail, newPass, onSuccessChange, onErrorChange);
            
        }


        function onSuccessChange(response) {
            var ProceedChangeBtn = document.getElementById('ProceedChangeBtn');

            ProceedChangeBtn.value = "Update";
            $('.btn').prop('disabled', false);
            
            window.location.href = "HomePage.aspx";

        }

        function onErrorChange(response) {
            var ProceedChangeBtn = document.getElementById('ProceedChangeBtn');
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

            ProceedChangeBtn.value = "Update";
        }


        function DisableButtons() {
            var btn1 = document.getElementById('ResendOTPBtn');
            var btn2 = document.getElementById('ConfirmOTPBtn');
            var emailContent = document.getElementById('<%= EmailTb.ClientID %>').value;
            var isValidEmail = document.getElementById('<%= EmailValidator.ClientID %>').isvalid;



            if (emailContent.length > 0 && isValidEmail) {
                btn1.disabled = true;
                btn1.value = "Sending OTP...";
            }

            if (btn2 != null) {
                btn2.disabled = true;
            }

            return true;
        }

    </script>
</asp:Content>
