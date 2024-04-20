<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSite.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="VisualMech.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Page Heading -->
    <div class="d-sm-flex align-items-center justify-content-between mb-4">
        <h1 class="h3 mb-0 text-gray-800">Dashboard</h1>
    </div>

    <!-- Information Cards -->
    <div class="row">
        <asp:Literal runat="server" ID="InformationCardsPanel"></asp:Literal>
    </div>

    


    
    
</asp:Content>
