<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignInPage.aspx.cs" Inherits="VisualMech.SignInPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    
    <link rel="stylesheet" href="/Content/custom_styles.css">
    
    
    
    <script src="/Content/custom.js"></script>
    <main>


        <div class="row align-self-center my-5 signup_background signup_rectangle_2 d-grid m-auto">
            <div class="row align-self-center signup_rectangle m-auto signup_padding">
                    <div class="d-grid">                        
                        <img src="Images/VGM_logo.png" alt="imgpng" class="img-fluid m-auto" style="width:100px; height:100px" />
                        <h5 style="color: white; padding-bottom:10px; text-align:left">Username: <asp:TextBox ID="Username_tb" runat="server" Width="348px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Username_tb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                            <asp:Label Text ="No Username found" Visible="false" Font-Size="Small" ID="No_Username_lbl" runat="server"></asp:Label>
                        </h5>                
                        <h5 style="color: white; padding-bottom:10px; text-align:left">Password: <asp:TextBox ID="Password_tb" runat="server" Width="348px" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Password_tb" ErrorMessage="*" ForeColor="White"></asp:RequiredFieldValidator>
                            <asp:Label Text ="Incorrect Password" Visible="false" Font-Size="Small" ID="Incorrect_lbl" runat="server"></asp:Label>
                        </h5>
                        <asp:Button class="signin_button m-auto" ID="Login_btn" runat="server" Text="Login" OnClick="Login_btn_Click"/>
                        
                        <br />
                        <a style="text-align:right; text-decoration-line: underline" class="nav-link white-link" runat="server" href="SignUpPage.aspx" >Sign Up</a>
                    </div>


                    


            </div>
        </div>


        
        
    </main>


    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>
</asp:Content>
