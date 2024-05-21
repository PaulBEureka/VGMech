<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiniGamePage.aspx.cs" Inherits="VisualMech.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery.signalR-2.4.3.js"></script>
    <script src="signalr/hubs"></script>
    
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>

    
    <script src="/Content/custom.js"></script>
    <main>
        <section class="gameMech-bgColor d-grid">
            <!-- MINI GAME LAYOUT -->
            <asp:Literal ID="MiniGameLit" runat="server"></asp:Literal>
            

            <div class="row align-self-center mini_game_box m-auto my-5">
                <div class="row minigame_title text-center d-grid m-auto">
                    <p class="text-white display-5 m-auto">LEADERBOARD</p>
                </div>
                <div>
                    <hr / class="text-white">
                </div>
                <div class="container m-auto my-5 mt-0">
                    <div id="leaderboardSection">
                        <!-- Existing game record will be dynamically added here -->
                    </div>
                </div>
            </div>

        </section>



    </main>

    <script>
        
        var pageContextMini = null;
        var chat = $.connection.myHub;

        PageMethods.GetMiniTitle(onSuccessOrderMini);

        function onSuccessOrderMini(response) {
            pageContextMini = response;
            $.connection.hub.qs = { "page": pageContextMini };


        }



        function updateLeaderboards(cardTitle) {
            chat.server.updateLeaderboards(cardTitle)
                .done(function () {
                    console.log("Leaderboards updated successfully.");
                })
                .fail(function (error) {
                    console.error("Error updating leaderboards: " + error);
                });
        }

        chat.client.updateLeaderboards = function (leaderboardHTML) {
            $('#leaderboardSection').html(leaderboardHTML[0]);
            var ranking = leaderboardHTML[1];
         
            if (ranking != null) {
                eval(ranking);
            }
            

            PageMethods.UpdateSessionInfo(leaderboardHTML[2], leaderboardHTML[3]);
        };

        $.connection.hub.start().done(function () {
            console.log("SignalR connected.");

            PageMethods.Get_Leaderboards(onSuccess4);

            setInterval(function () {
                PageMethods.Get_Leaderboards(onSuccess4);
            }, 6000);

        });

        function onSuccess4(response) {
            updateLeaderboards(response);
            
        }
        


        

    </script>


    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>
</asp:Content>
