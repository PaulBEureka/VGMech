<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSite.Master" AutoEventWireup="true" CodeBehind="AdminChange.aspx.cs" Inherits="VisualMech.WebForm8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="modal-header">
        <h5 class="modal-title" id="passwordModalLabel">Change password</h5>
    </div>
    <div class="modal-body">
        <div class="row">
            <div class="col">
                <p class="text-black fs-4"> Re-enter current password </p>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <label for="CurrentPassTb" class="form-label text-black">Current password: </label>
                <asp:TextBox runat="server" ID="CurrentPassTb" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <label for="NewPasswordTb" class="form-label text-black">New password: </label>
                <asp:TextBox runat="server" ID="NewPasswordTb" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
        </div> 
        <br />
        <div class="row">
            <asp:Label ID="submitErrorLbl" CssClass="text-danger" runat="server" Visible="false"></asp:Label>

        </div>
        <div class="row">
            <asp:Label ID="submitSuccessLbl" CssClass="text-success" runat="server" Visible="false"></asp:Label>

        </div>
                           

                                
    </div>
                        
    <div class="modal-footer">
        <asp:Button class="btn btn-primary bg-gradient h-100" runat="server"  Text="Change Password" OnClick="PassChangeBtn_Click" ID="PassChangeBtn"></asp:Button>
    </div>

 
</asp:Content>
