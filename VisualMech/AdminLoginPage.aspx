<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLoginPage.aspx.cs" Inherits="VisualMech.AdminLoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> VGMech</title>
    

    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery.signalR-2.4.3.js"></script>
    <script src="signalr/hubs"></script>
    
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>

   

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    
    
    <script src="/Content/custom.js"></script>
    <script src="/Scripts/toastr.js"></script>
</head>
<body>
    <form id="form1" runat="server" class="main_wrap">
        <div class="bg-danger bg-gradient w-100" style="padding-top:25px; padding-left:20px; height:120px;">
            <p class="text-white display-4 ps-3 fw-bold">VGMech Admin</p>
        </div>

        <br/>
        <br/>

        <div class="row text-center justify-content-center align-content-center d-grid ">
            <div class="col-md-12 m-auto border-2 border-dark border d-grid">
                <div class="container d-grid mt-3">
                    <p class="display-6 fw-normal text-start">Login</p>
                </div>
                    <hr />
                <div class="container">
                    <div class="row text-start">
                        <label for="UsernameTb" class="form-label text-black">Username</label>
                        <asp:TextBox ID="UsernameTb" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <br />
                    <div class="row text-start">
                        <label for="PasswordTb" class="form-label text-black">Password</label>
                        <asp:TextBox ID="PasswordTb" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <br />
                    <div class="row">
                        <asp:Button ID="LoginBtn" OnClick="LoginBtn_Click" runat="server" CssClass="btn btn-primary" Height="40px" Text="Log in >>" Width="358px"/>
                    </div>
                </div>
                    <hr />
                <div class="container d-grid pb-3">
                    <asp:Image ID="Image1" runat="server" Height="120px" CssClass="m-auto" ImageUrl="~/Images/VGM_logo.png" Width="120px" />
                </div>
                    

            </div>
        </div>
    
</form>

<asp:PlaceHolder runat="server">
    <%: Scripts.Render("~/Scripts/bootstrap.js") %>
</asp:PlaceHolder>
</body>
</html>
