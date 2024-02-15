<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiniGamePage.aspx.cs" Inherits="VisualMech.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="stylesheet" href="/Content/custom_styles.css">
    <script src="/Content/custom.js"></script>
    <main>
        <section class="gameMech-bgColor d-grid">
            <!-- MINI GAME LAYOUT -->
            <div class="row align-self-center mini_game_box m-auto my-5">
                        <div class="row minigame_title text-center d-grid m-auto">
                            <p class="text-white display-5 m-auto">MINI GAME TITLE</p>
                        </div>
                        <div class="row  center_custom m-auto my-5 mt-0">
                            <iframe src="https://almers5.github.io/Game-Mechanics/MiniGame1/" class="mini_game_inner_box" scrolling="no"></iframe>
                        </div>
            </div>

            
            
            <div class="row align-self-center mini_game_box m-auto my-5">
                        <div class="row minigame_title text-center d-grid m-auto">
                            <p class="text-white display-5 m-auto">LEADERBOARD</p>
                        </div>
                        <div>
                            <hr / class="text-white">
                        </div>

                        <!-- LEADERBOARD SECTION LAYOUT -->
                        <!-- THINGS TO DO:
                            1. Add dynamic display in leaderboards depending on the number of top players
                            2. Connect to databse
                            3. Perform queries to get top 5
                            4. Query to update databse when new top player is recorded
                            5. Perform method to update leaderboard onced done playing-->
                        <div class="container m-auto my-5 mt-0">


                            <asp:Literal ID="LeaderboardHTML1" runat="server"></asp:Literal>

                            

                            

                            



                            

                        </div>
            </div>







        </section>



    </main>


    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>
</asp:Content>
