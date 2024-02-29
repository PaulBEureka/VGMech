<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiniGamePage.aspx.cs" Inherits="VisualMech.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="stylesheet" href="/Content/custom_styles.css">
    
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery.signalR-2.4.3.js"></script>
    <script src="signalr/hubs"></script>
    
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>

    <script src="/Content/custom.js"></script>
    <main>
        <section class="gameMech-bgColor d-grid">
            <!-- MINI GAME LAYOUT -->
            <div class="row align-self-center mini_game_box m-auto my-5">
                        <div class="row minigame_title text-center d-grid m-auto">
                            <p class="text-white display-5 m-auto">MINI GAME TITLE</p>
                        </div>
                        
                        <% if(Session["Current_ID"] != null) { %>
                            <div class="row  center_custom m-auto my-5 mt-0" >
                                <iframe src="http://localhost/unity/WebGl Build/index.html?current_id=<%= Session["Current_ID"] %>" class="mini_game_inner_box" scrolling="no"></iframe>
                            </div>
                        <% } else { %>
                            <div class="row  center_custom m-auto my-5 mt-0 ">
                                <button type="button" id="post_disbled" class="comment_button my-2 bg-danger w-100" onclick="sign_in_comment()">Sign in to play mini game :></button>
                            </div>
                        <% } %>
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


                            <div id="leaderboardSection">
                                <!-- Existing comments will be dynamically added here -->
                            </div>

                            

                            

                            



                            

                        </div>
            </div>







        </section>



    </main>

    <script>
        // Add this code to your existing client-side JavaScript
        $(function () {
            var chat = $.connection.myHub;

            // Function to update leaderboards
            function updateLeaderboards() {
                chat.server.updateLeaderboards()
                    .done(function () {
                        console.log("Leaderboards updated successfully.");
                    })
                    .fail(function (error) {
                        console.error("Error updating leaderboards: " + error);
                    });
            }

            // Handle the updateLeaderboards message from the server
            chat.client.updateLeaderboards = function (leaderboardHTML) {
                // Update the leaderboard on the webpage
                $('#leaderboardSection').html(leaderboardHTML);
                console.log("Leaderboards updated.");
            };

            $.connection.hub.start().done(function () {
                console.log("SignalR connected.");

                // Initial call to update leaderboards
                updateLeaderboards();

                // Set interval to call updateLeaderboards every 10 seconds
                setInterval(updateLeaderboards, 5000);
            });
        });


        

    </script>


    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>
</asp:Content>
