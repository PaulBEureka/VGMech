<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerificationPage.aspx.cs" Inherits="VisualMech.WebForm5"  Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <h3 class=" text-center my-5">OTP sent to your provided email</h3>
        <br />

        <div class="row">
            <div class="col text-center">
               <asp:Label ID="lblTimer" runat="server" Text="" Font-Size="Small"></asp:Label><br />
            </div>
        </div> 
        <div class="row">
            <div class="col text-center">
                <asp:TextBox ID="OTPtb" runat="server" Placeholder="Type OTP here"></asp:TextBox>
                <asp:Button class="signin_button m-auto" ID="ResendBtn" runat="server" Text="Resend OTP (Limit - 5/5)" OnClientClick="DisableButtons()" OnClick="ResendBtn_Click"/>
                <asp:HiddenField ID="hfCountdownValue" runat="server" Value="5" />
            </div>
        </div>     
        <div class="row">
            <div class="col text-center my-4">
                <asp:Button class="signin_button m-auto" ID="OTPbtn" runat="server" Text="Submit OTP" OnClientClick="DisableButtons()" OnClick="OTPbtn_Click"/>
      
            </div>
        </div>
    </main>
           
    <script>
        function updateCountdown() {
            var btn = document.getElementById('<%= ResendBtn.ClientID %>');
        var hiddenField = document.getElementById('<%= hfCountdownValue.ClientID %>');
            var currentValue = parseInt(hiddenField.value);

            if (currentValue > 0) {
                btn.value = currentValue - 1; // Update button text
                hiddenField.value = currentValue - 1; // Update hidden field value
            }
        }

        function DisableButtons() {
            var btn1 = document.getElementById('<%= ResendBtn.ClientID %>');
            var btn2 = document.getElementById('<%= OTPbtn.ClientID %>');
            setTimeout(function () {
                btn1.disabled = true;
                btn2.disabled = true;
            }, 0);
            return true;
        }
    </script>
</asp:Content>
