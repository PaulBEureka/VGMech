<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignupPage.aspx.cs" Inherits="VisualMech.SignupPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="stylesheet" href="/Content/custom_styles.css">
    <script src="/Content/custom.js"></script>
    <main>
        <div class="row align-self-center m-auto my-5 signup_background signup_rectangle_2 d-grid">
            <div class="row align-self-center signup_rectangle m-auto signup_padding">
                    <div class="d-grid">                        
                        <img src="Images/VGM_logo.png" alt="imgpng" class="img-fluid m-auto" style="width:100px; height:100px" />
                        <h5 style="color: white; padding-bottom:10px; text-align:left">New Username: <asp:TextBox ID="New_Username_tb" runat="server" Width="348px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="New_Username_tb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ControlToValidate="New_Username_tb"
                                ErrorMessage="Username must be 1-15 characters long."
                                ValidationExpression="^.{1,15}$"
                                ForeColor="White"
                                 Font-Size="Small">
                            </asp:RegularExpressionValidator>
                            <asp:Label runat="server" ID="taken_lbl" Text ="Username already taken" Font-Size="Small" ForeColor="White" Visible="false">

                            </asp:Label>


                        </h5>                
                        <h5 style="color: white; padding-bottom:10px; text-align:left">New Password: <asp:TextBox ID="New_Password_tb" runat="server" Width="348px" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="New_Password_tb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server"
                                ControlToValidate="New_Password_tb"
                                ErrorMessage="Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and be at least 8 characters long."
                                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"
                                ForeColor="White"
                                Font-Size="Small">

                            </asp:RegularExpressionValidator>
                        
                        </h5>
                        <h5 style="color: white; padding-bottom:10px; text-align:left">Confirm Password: <asp:TextBox ID="Confirm_Password_tb" runat="server" Width="348px" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Confirm_Password_tb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidatorPassword" runat="server"
                                ControlToValidate="Confirm_Password_tb"
                                ControlToCompare="New_Password_tb"
                                ErrorMessage="Passwords do not match."
                                ForeColor="White"
                                 Font-Size="Small">

                            </asp:CompareValidator>
                        
                        
                        </h5>
                        
                        <asp:Button class="signup_button m-auto " ID="Register_btn" runat="server" Text="Register" OnClick="Register_btn_Click"/>
                        
                        <br />
                        <a style="text-align:right; text-decoration-line: underline" class="nav-link white-link" runat="server" href="SignInPage.aspx" >Sign In</a>
                    </div>
                    
                

            </div>
        </div>

        
        
    </main>


    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>
</asp:Content>
