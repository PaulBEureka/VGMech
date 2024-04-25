<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignInPage.aspx.cs" Inherits="VisualMech.SignInPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    
    <script src="/Content/custom.js"></script>
    <main>
        
       
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
                        <a class="white-link text-end" runat="server" href="SignUpPage.aspx" >Sign Up</a>
                    </div>


                    


        </div>


        
    </main>


    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>
</asp:Content>
