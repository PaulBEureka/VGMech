<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignupPage.aspx.cs" Async="true" Inherits="VisualMech.SignupPage"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <script src="/Content/custom.js"></script>
    <main>

        <asp:Panel runat="server" ID="SignUpPanel" Visible="true">
            <div class="row align-self-center m-auto my-5 signup_rectangle d-grid">
                        <div class="d-grid m-auto align-items-center">                        
                            <img src="Images/VGM_logo.png" alt="imgpng" class="img-fluid m-auto" style="width:100px; height:100px" />
                            <h5 style="color: white; padding-bottom:10px; text-align:left" class="m-auto">New Username: <asp:TextBox ID="New_Username_tb" runat="server" Width="348px" CssClass=" m-auto form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="m-auto" Display="Dynamic" ControlToValidate="New_Username_tb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="m-auto"
                                    ControlToValidate="New_Username_tb"
                                    ErrorMessage="Username must be 1-15 characters long."
                                    ValidationExpression="^.{1,15}$"
                                    ForeColor="White"
                                     Font-Size="Small"
                                     Display="Dynamic">
                                </asp:RegularExpressionValidator>
                                <br />
                                <asp:Label runat="server" ID="taken_lbl" CssClass="m-auto" Text ="Username already taken" Font-Size="Small" ForeColor="White" Visible="false">

                                </asp:Label>


                            </h5>        
                        
                            <h5 style="color: white; padding-bottom:10px; text-align:left" class="m-auto">Email: <asp:TextBox ID="Email_tb" runat="server" Width="348px" CssClass="m-auto form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="m-auto" Display="Dynamic" ControlToValidate="Email_tb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" runat="server"
                                    ControlToValidate="Email_tb" 
                                    ErrorMessage="Invalid email format"
                                    ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                                    ForeColor="White" CssClass="m-auto"
                                    Font-Size="Small">
                                </asp:RegularExpressionValidator>
                                <br />
                                <asp:Label CssClass="m-auto" runat="server" ID="email_lbl" Text ="Email Already Used" Font-Size="Small" ForeColor="White" Visible="false">

                                </asp:Label>


                            </h5>



                            

                            <h5 class="m-auto" style="color: white; padding-bottom:10px; text-align:left">New Password: <asp:TextBox ID="New_Password_tb" CssClass="form-control m-auto" runat="server" Width="348px" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="m-auto" runat="server" Display="Dynamic" ControlToValidate="New_Password_tb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                            </h5> 
                            
                            
                                <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server" Display="Dynamic"
                                    ControlToValidate="New_Password_tb"
                                    ErrorMessage="Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and be at least 8 characters long."
                                    ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"
                                    ForeColor="White" CssClass="pb-2 text-center"
                                    Font-Size="Small"
                                    ClientIDMode="Inherit">

                                </asp:RegularExpressionValidator>
                        
                        
                            



                            <h5 class="m-auto" style="color: white; padding-bottom:10px; text-align:left">Confirm Password: <asp:TextBox ID="Confirm_Password_tb" CssClass="form-control m-auto" runat="server" Width="348px" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="m-auto" runat="server" Display="Dynamic" ControlToValidate="Confirm_Password_tb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidatorPassword" runat="server" Display="Dynamic"
                                    ControlToValidate="Confirm_Password_tb"
                                    ControlToCompare="New_Password_tb"
                                    ErrorMessage="Passwords do not match."
                                    ForeColor="White" CssClass="m-auto"
                                     Font-Size="Small">

                                </asp:CompareValidator>
                        
                        
                            </h5>

                            <div class=" align-content-center text-center m-auto">
                                <asp:Image ID="captchaImage" runat="server" Height="40px" Width="150px" ImageUrl="~/Captcha.aspx" /><br />
                                <br />
                                <asp:TextBox ID="captchacode" runat="server" Placeholder="Enter Captcha code" CssClass="form-control"></asp:TextBox><br />
                                <asp:Label ID="lblCaptchaErrorMsg" runat="server" Text="" Font-Size="Small" ForeColor="White"></asp:Label><br />
                            </div>
                            <br />


                            <asp:Button class="signup_button m-auto " ID="Register_btn" UseSubmitBehavior="false" ClientIDMode="Static" OnClientClick="DisableButton()" runat="server" Text="Register" OnClick="Register_btn_Click"/>
                            <br />
                            <a class="white-link text-end" runat="server" href="SignInPage.aspx" >Sign In</a>
                    
                            
                        
                        </div>
                    
                

            </div>


        </asp:Panel>

        
        <asp:Panel runat="server" ID="VerifyPanel" Visible="false">
            <div class="row mt-5 ms-3">
                <asp:Button class="signin_button" ID="ChangeBtn" runat="server" UseSubmitBehavior="false" ClientIDMode="Static" Text="Change Credentials" OnClientClick="DisableButtons2()" OnClick="ChangeBtn_Click"/>
                <br />
                <h4 class=" text-center my-5">OTP sent to your provided email, verify to complete account registation</h4>
            </div>
            

            <div class="row">
                <div class="col text-center">
                   <asp:Label ID="lblTimer" runat="server" Text="" Font-Size="Large"></asp:Label><br />
                </div>
            </div> 
            <div class="row">
                <div class="col text-center">
                    <asp:TextBox ID="OTPtb" runat="server" Placeholder="Type OTP here"></asp:TextBox>
                    <asp:Button class="signin_button m-auto" ID="ResendBtn" runat="server" UseSubmitBehavior="false" ClientIDMode="Static" Text="Resend OTP (Limit - 5/5)" OnClientClick="DisableButtons()" OnClick="ResendBtn_Click"/>
                    <asp:HiddenField ID="hfCountdownValue" runat="server" ClientIDMode="Static" Value="5" />
                </div>
            </div>     
            <div class="row">
                <div class="col text-center my-4">
                    <asp:Button class="signin_button m-auto" ID="OTPbtn" runat="server" UseSubmitBehavior="false" ClientIDMode="Static" Text="Submit OTP" OnClientClick="DisableButtons2()" OnClick="OTPbtn_Click"/>
      
                </div>
            </div>
        </asp:Panel>
        
    </main>

    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>
    <script>
        function DisableButton() {
            var isValid = Page_ClientValidate();

            if (isValid) {
                var btn1 = document.getElementById('Register_btn');
                btn1.disabled = true;
                btn1.value = 'Sending OTP...';

                return true;
            }
        }

        function EnableButton() {
            var btn1 = document.getElementById('Register_btn');
            btn1.disabled = false;
            btn1.value = 'Register';

            console.log("ENABLE BUTTON");
            return true;
        }

        function updateCountdown() {
            var btn = document.getElementById('ResendBtn');
            var hiddenField = document.getElementById('hfCountdownValue');
            var currentValue = parseInt(hiddenField.value);

            if (currentValue > 0) {
                btn.value = currentValue - 1; // Update button text
                hiddenField.value = currentValue - 1; // Update hidden field value
            }
        }


        function DisableButtons() {
            var btn1 = document.getElementById('ResendBtn');
            var btn2 = document.getElementById('OTPbtn');
            var btn3 = document.getElementById('ChangeBtn');
            btn1.disabled = true;
            btn1.value = 'Resending OTP...';
            btn2.disabled = true;
            btn3.disabled = true;

            return true;
        }

        function DisableButtons2() {
            var btn1 = document.getElementById('ResendBtn');
            var btn2 = document.getElementById('OTPbtn');
            var btn3 = document.getElementById('ChangeBtn');
            btn1.disabled = true;
            btn2.disabled = true;
            btn3.disabled = true;

            return true;
        }

    </script>
</asp:Content>
