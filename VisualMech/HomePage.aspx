<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="VisualMech._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="stylesheet" href="/Content/custom_styles.css">
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery.signalR-2.4.3.js"></script>
    <script src="signalr/hubs"></script>
    
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>

    
    <script src="/Content/custom.js"></script>
    <main id="content">

        <div class="main-home-color">
            <!-- Home page layout -->


        <section id="homepage" class="intro-section-color my-3" aria-labelledby="aspnetTitle">
            <div id="carouselExampleInterval" class="carousel slide container " data-bs-ride="carousel">
                <div class="carousel-indicators">
                    <button type="button" data-bs-target="#carouselExampleInterval" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                    <button type="button" data-bs-target="#carouselExampleInterval" data-bs-slide-to="1" aria-label="Slide 2"></button>
                    <button type="button" data-bs-target="#carouselExampleInterval" data-bs-slide-to="2" aria-label="Slide 3"></button>
                </div>
                <div class="carousel-inner">
                    <div class="carousel-item active" data-bs-interval="5000">
                        <div class="row my-3 py-5">
                            <div class="col-md-6 justify-content-center text-center my-3 py-5 d-grid">
                                <h1 class="display-3 text-center text-light fw-bolder m-auto">Redefining <br /> Learning of Game<br /> Mechanics!</h1>
                            </div>
                            <div class="col-md-6 justify-content-center text-center">
                                <img class="img-fluid carousel-img" src="Images/game-mechanics-cover.png" alt=" ">
                            </div>
                        </div>
                    </div>
                     <div class="carousel-item" data-bs-interval="5000">
                        <div class="row my-3 py-5">
                            <div class="col-md-6 justify-content-center text-center">
                                <img class="img-fluid carousel-img" src="Images/game-mechanics-cover2.png" alt=" ">
                            </div>
                            <div class="col-md-6 justify-content-center text-center my-3 py-5 d-grid">
                                <h1 class="display-3 text-center text-light fw-bolder m-auto">Interactively learn<br />Each Game Mechanic!</h1>
                            </div>
                            
                        </div>
                    </div>
                     <div class="carousel-item" data-bs-interval="5000">
                         <div class="row my-3 py-5">
                            <div class="col-md-6 justify-content-center text-center my-3 py-5 d-grid">
                                <h1 class="display-3 text-center text-light fw-bolder m-auto">Collaboratively learn<br />with other learners!</h1>
                            </div>
                             <div class="col-md-6 justify-content-center text-center">
                                <img class="img-fluid carousel-img1" src="Images/game-mechanics-cover3.png" alt=" ">
                            </div>
                            
                        </div>



                    </div>






                    <!-- Add other carousel items here -->
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
                <div class="row my-3 py-5 justify-content-center text-center ">
                    <div class="col-md-6 justify-content-center text-center mx-md-5 square-bg my-5 no_bg">
                        <p class="fw-bolder h3 text-light">BLOCK BREAKER</p>
                        <a class="btn w-100 h-100 rcorners2" href="MiniGamePage.aspx" role="button">
                            <img src="Images/Mini_Game1_thumb.png" alt="buttonpng" class="img-fluid w-100 h-100 rounded_corners" />
                        </a>

                    </div>
                    <div class="col-md-6 justify-content-center text-center mx-md-5 square-bg my-5 no_bg">
                        <p class="fw-bolder h3 text-light">BLOCK BREAKER</p>
                        <a class="btn w-100 h-100 rcorners2" href="MiniGamePage.aspx" role="button">
                            <img src="Images/Mini_Game1_thumb.png" alt="buttonpng" class="img-fluid w-100 h-100 rounded_corners" />
                        </a>

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
                        <img src="Images/paul_pic.jpg" alt="imgpng" class="img-fluid w-100 h-100" />
                    </div>
                    <div class="text-center">
                        <p class="text-light">Paul Carlo Bataga</p>
                    </div>
                </div>
                <div class="col-md-6 d-grid">
                    <div class="col-md-6 square-bg-2 m-auto text-center my-3">
                        <img src="Images/cj_pic.jpg" alt="imgpng" class="img-fluid w-100 h-100" />
                    </div>
                    <div class="text-center">
                        <p class="text-light">Ciriaco John Almeron</p>
                    </div>
                </div>
                <div class="col-md-6 d-grid">
                    <div class="col-md-6 square-bg-2 m-auto text-center my-3">
                        <img src="Images/aries_pic.jpg" alt="imgpng" class="img-fluid w-100 h-100" />
                    </div>
                    <div class="text-center">
                        <p class="text-light">Aries Diomampo</p>
                    </div>
                </div>
                <div class="col-md-6 d-grid">
                    <div class="col-md-6 square-bg-2 m-auto text-center my-3">
                       <img src="Images/kim_pic.jpg" alt="imgpng" class="img-fluid w-100 h-100" />
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
