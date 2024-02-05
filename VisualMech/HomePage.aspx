<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="VisualMech._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="stylesheet" href="/Content/custom_styles.css">
    <script src="/Content/custom.js"></script>
    <main id="content">

        <div class="main-home-color">
            <!-- Home page layout -->


        <section id="homepage" class="intro-section-color" aria-labelledby="aspnetTitle">
            <div class="container">
                <div class="row my-3 py-5">
                    <div class="col-md-6 justify-content-center text-center my-3 py-5">
                        <h1 class=" display-3 text-center text-light fw-bolder">Redefining <br /> Learning of Game<br /> Mechanics!</h1>
                    </div>
                    <div class="col-md-6 justify-content-center text-center ">
                        <img class="img-fluid" src="Images/game-mechanics-cover.png" alt=" " >
                    </div>
                </div>
            </div>
            
        </section>

        <!-- Game Mechanic Cards Layout -->


        <section id="game cards" class="container align-content-center justify-content-center" >
            <ul class="cards align-content-center justify-content-center d-grid">
                <asp:Literal ID="litCardHtml" runat="server"></asp:Literal>

            </ul>

        </section>  

        <!-- Mini Game layout -->
        <section id="mini games" class="mini_games_bg_home">
            <div class="row text-center text-light ">
                <h1 class="display-4 mini_custom_padding fw-bolder">Play competitive mini games<br /> with other learners!</h1>
            </div>
            
            <div class="container">
                <div class="row my-3 py-5 justify-content-center text-center">
                    <div class="col-md-6 justify-content-center text-center mx-md-5 square-bg my-5">
                        MINI GAME 1 HERE
                    </div>
                    <div class="col-md-6 justify-content-center text-center mx-md-5 square-bg my-5 ">
                        MINI GAME 2 HERE
                    </div>
                </div>
            </div>

        </section>

        <!-- About Us layout -->

        <section id="about us" class="about_us_bg ">
            <div class="row text-center text-light ">
                <h1 class="display-4 mini_custom_padding fw-bolder">MEET THE TEAM AND<br /> EVERYTHING YOU NEED TO KNOW<br /> ABOUT US</h1>
            </div>
            <div class="row">
                <div class="col-md-6 d-grid">
                    <div class="col-md-6 square-bg-2 m-auto text-center my-3">
                        
                    </div>
                    <div class="text-center">
                        <p class="text-light">Paul Carlo Bataga</p>
                    </div>
                </div>
                <div class="col-md-6 d-grid">
                    <div class="col-md-6 square-bg-2 m-auto text-center my-3">
                        
                    </div>
                    <div class="text-center">
                        <p class="text-light">Ciriaco John Almeron</p>
                    </div>
                </div>
                <div class="col-md-6 d-grid">
                    <div class="col-md-6 square-bg-2 m-auto text-center my-3">
                        
                    </div>
                    <div class="text-center">
                        <p class="text-light">Aries Diomampo</p>
                    </div>
                </div>
                <div class="col-md-6 d-grid">
                    <div class="col-md-6 square-bg-2 m-auto text-center my-3">
                       
                    </div>
                    <div class="text-center">
                        <p class="text-light">Kimtribi Aleksie Cuevas</p>
                    </div>
                </div>
            </div>

            <!-- Core Purpose layout -->

            <div class="row text-center">
                <h1 class="display-5 text-light fw-bolder">Our Core Purpose</h1>
            </div>
            <div class="container">
                <p class="text-light my-2 py-4 text-center fw-bold h4">
                    "To empower aspiring game developers and enthusiasts with deep understanding of game mechanics through interactive learning experiences,  fostering both knowledge acquisition and enjoyment in a dynamic virtual environment."
                </p>
            </div>

        </section>
        </div>

        

        

    </main>
    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>

    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            var cards = document.querySelectorAll('.card');
            cards.forEach(function (card) {
                card.addEventListener('click', function () {
                    var cardId = this.getAttribute('data-card-id');
                    console.log("Card ID: " + cardId);
                    HandleIT(cardId);

                });
            });
        });


        function HandleIT(cardId) {
            PageMethods.ProcessIT(cardId);

        }  

    </script>



    <script src="/Content/custom.js"></script>
    
   

</asp:Content>
