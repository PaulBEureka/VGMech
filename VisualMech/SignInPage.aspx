<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignInPage.aspx.cs" Inherits="VisualMech.SignInPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="stylesheet" href="/Content/custom_styles.css">
    <script src="/Content/custom.js"></script>
    <main>
        <div class="row align-self-center m-auto my-5 signup_background signup_rectangle_2">
            <div class="row align-self-center signup_rectangle m-auto signup_padding">
                <asp:Panel ID="Panel1" runat="server" Height="194px" HorizontalAlign="Center">
                    <div>                        
                        <img src="Images/VGM_logo.png" alt="imgpng" class="img-fluid" style="width:100px; height:100px" />
                        <h4 style="color: white; padding-bottom:10px; text-align:left">Username: <asp:TextBox ID="TextBox5" runat="server" Width="348px"></asp:TextBox></h4>                
                        <h4 style="color: white; padding-bottom:10px; text-align:left">Password: <asp:TextBox ID="TextBox6" runat="server" Width="348px"></asp:TextBox></h4>
                        <asp:Panel ID="Panel2" runat="server" >
                            <asp:Button class="signup_button" ID="Button5" runat="server" Text="Login"/>
                        </asp:Panel>
                        <br />
                        <a style="text-align:right; text-decoration-line: underline" class="nav-link white-link" runat="server" href="SignUpPage.aspx" >Sign Up</a>
                    </div>
                    
                </asp:Panel>


            </div>
        </div>
        
    </main>


    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>
</asp:Content>
