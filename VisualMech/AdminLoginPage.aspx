<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLoginPage.aspx.cs" Inherits="VisualMech.AdminLoginPage" %>

<!DOCTYPE html>
<script src="/Content/custom.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="adminLoginPanel" runat="server" BackColor="#ea5d50" Height="120px" style="padding-top:25px; padding-left:20px">
            <asp:Label ID="adminLogoLabel" runat="server" Font-Bold="True" ForeColor="White" Font-Size="75px" padding-left="100px">VGMech Admin</asp:Label>
        </asp:Panel>
        <br/>
        <br/>

        <div style="align-content:center">
            <div style="padding-left:35%">
                <asp:Panel ID="Panel1" runat="server" Height="500px" Width="420px" BorderStyle="Groove" HorizontalAlign="Left">
                            <h1 style="padding-left:25px; padding-top:15px">Log in</h1>
        <hr />
                <br />
                <div style="padding-left:25px">
                    Username<br />
                    <asp:TextBox ID="TextBox1" runat="server" Width="350px"></asp:TextBox>
                </div>
                <br />
                <div style="padding-left:25px">
                    Password
                    <br />
                    <asp:TextBox ID="TextBox2" runat="server" Width="350px" style="margin-top: 0px"></asp:TextBox>
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Underline="true" ForeColor="MediumBlue">Forgot your Password?</asp:HyperLink>
                </div>
                <br />
                <div style="padding-left:25px">
                     <asp:Button ID="Button1" runat="server" Height="40px" Text="Log in >>" Width="358px"/>
                </div>
                <br />
                            <hr />

                <div style="padding-left:35%">
                    <asp:Image ID="Image1" runat="server" Height="120px" ImageUrl="~/Images/VGM_logo.png" Width="120px" />
                </div>
    
            </asp:Panel>
            </div>
            
        </div>
    
</form>
</body>
</html>
