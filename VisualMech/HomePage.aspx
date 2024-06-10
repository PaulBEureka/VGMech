<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="VisualMech._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <script src="Scripts/jquery-3.4.1.js"></script>
    <script src="Scripts/jquery.signalR-2.4.3.js"></script>
    <script src="signalr/hubs"></script>
    
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>

    <script src="/Content/custom.js"></script>
    <main id="content">

        <div class="main-home-color">
            <!-- Home page layout -->
        <div>
            <div class="question-rectangle position-fixed row">
                <div class="question-badge d-grid text-center col-3">
                    <i class="fa-solid fa-circle-question question-icon m-auto text-center"></i>
                </div>
                <div class="col-9">
                    <div class="row text-start">
                        <p class="text-white question-font"><strong>Having troubles?</strong><br />Download our User Manual <a href="/PDFs/User_Manual_VGMech.pdf" class="text-white" download="User_Manual_VGMech.pdf">Here!</a></p>
                    </div>
                </div>
            </div>
        </div>


       <section id="homepage" class="intro-section-color" aria-labelledby="aspnetTitle">
            <div id="carouselExampleInterval" class="carousel slide container carousel-container" data-bs-ride="carousel">
                <div class="carousel-indicators">
                    <button type="button" data-bs-target="#carouselExampleInterval" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                    <button type="button" data-bs-target="#carouselExampleInterval" data-bs-slide-to="1" aria-label="Slide 2"></button>
                    <button type="button" data-bs-target="#carouselExampleInterval" data-bs-slide-to="2" aria-label="Slide 3"></button>
                    <button type="button" data-bs-target="#carouselExampleInterval" data-bs-slide-to="3" aria-label="Slide 4"></button>
                    <button type="button" data-bs-target="#carouselExampleInterval" data-bs-slide-to="4" aria-label="Slide 5"></button>
                </div>
                <div class="carousel-inner carousel-container">
                    <div class="carousel-item active" data-bs-interval="5000">
                        <div class="row my-3 py-5">
                            <div class="col-md-6 justify-content-center text-center my-3 py-5 d-grid">
                                <p class="display-3 text-center text-light fw-bolder m-auto">Master the Basics: <br /> fluid movement is <br /> the foundation of gameplay</p>
                            </div>
                            <div class="col-md-6 justify-content-center text-center ">
                                <img class="ratio ratio-4x3 container-md" src="Images/movement_gif.gif" alt=" ">
                            </div>
                        </div>
                    </div>
                    <div class="carousel-item" data-bs-interval="5000">
                        <div class="row my-3 py-5">
                            <div class="col-md-6 justify-content-center text-center">
                                <img class="ratio ratio-4x3 container-md" src="Images/shooting_gif.gif" alt=" " >
                            </div>
                            <div class="col-md-6 justify-content-center text-center my-3 py-5 d-grid">
                                <p class="display-3 text-center text-light m-auto fw-bolder">Sharpen your aim: <br />precise shooting <br />makes all the difference.</p>
                            </div>
                        </div>
                    </div>
                    <div class="carousel-item" data-bs-interval="5000">
                        <div class="row my-3 py-5">
                            <div class="col-md-6 justify-content-center text-center my-3 py-5 d-grid">
                                <p class="display-3 text-center text-light fw-bolder m-auto">Gather wisely: <br />every item counts towards your success.</p>
                            </div>
                            <div class="col-md-6 justify-content-center text-center">
                                <img class="ratio ratio-4x3 container-md" src="Images/collecting_gif.gif" alt=" ">
                            </div>
                        </div>
                    </div>
                    <div class="carousel-item" data-bs-interval="5000">
                        <div class="row my-3 py-5">
                            <div class="col-md-6 justify-content-center text-center">
                                <img class="ratio ratio-4x3 container-md" src="Images/interaction_gif.gif" alt=" " >
                            </div>
                            <div class="col-md-6 justify-content-center text-center my-3 py-5 d-grid">
                                <p class="display-3 text-center text-light fw-bolder m-auto">Engage actively:<br /> interaction opens new possibilities.</p>
                            </div>
                        </div>
                    </div>
                    <div class="carousel-item" data-bs-interval="5000">
                    <div class="row my-3 py-5">
                        <div class="col-md-6 justify-content-center text-center my-3 py-5 d-grid">
                            <p class="display-3 text-center text-light fw-bolder m-auto">Manage your vitality: health is<br /> key to survival.</p>
                        </div>
                        <div class="col-md-6 justify-content-center text-center">
                            <img class="ratio ratio-4x3 container-md" src="Images/health_gif.gif" alt=" ">
                        </div>
                    </div>
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
                <div class="row py-5 justify-content-center text-center ">
                    <asp:Literal ID="litCardMiniGameHtml" runat="server"></asp:Literal>
                    
                    
                </div>
            </div>

        </section>

        <!-- About Us layout -->

        <section id="about us" class="about_us_bg ">
            <div class="row text-center text-light ">
                <h1 class="display-4 mini_custom_padding fw-bolder border-2">MEET THE TEAM AND<br /> EVERYTHING YOU NEED TO KNOW<br /> ABOUT US</h1>
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
                <div class="col-md-12 d-grid">
                    <div class="col-md-6 square-bg-2 m-auto text-center my-3">
                       <img src="Images/andrei.png" alt="imgpng" class="img-fluid w-100 h-100" />
                    </div>
                    <div class="text-center">
                        <p class="text-light">Andrei Robin Sta. Ana</p>
                    </div>
                </div>
            </div>

            <!-- Core Purpose layout -->

            <div class="row text-center">
                <h1 class="display-5 text-light fw-bolder">Our Core Purpose</h1>
            </div>
            <div class="container">
                <p class="text-light py-5 text-center fw-bold h4 mb-0">
                    "To empower aspiring game developers and enthusiasts with deep understanding of game mechanics through interactive learning experiences,  fostering both knowledge acquisition and enjoyment in a dynamic virtual environment."
                </p>
            </div>

        </section>

            
            
        </div>

        
         
        

    </main>
    
    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>



    <script src="/Content/custom.js"></script>
    <script>
        const questionRectangle = document.querySelector('.question-rectangle');

        questionRectangle.addEventListener('mouseover', () => {
            questionRectangle.style.animation = 'none'; 
        });
    </script>
   

</asp:Content>
