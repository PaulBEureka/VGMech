<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="VisualMech._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link rel="stylesheet" href="/Content/custom_styles.css">
    <script src="/Content/custom.js"></script>
    <main id="content">

        <!-- Home page layout -->

        <section id="homepage" class="intro-section-color" aria-labelledby="aspnetTitle">
            <div class="container">
                <div class="row my-3 py-5">
                    <div class="col-md-6 justify-content-center text-center my-3 py-5">
                        <h1 class=" display-3 text-center text-light fw-bolder">Redefining <br /> Learning of Game<br /> Mechanics!</h1>
                    </div>
                    <div class="col-md-6 justify-content-center text-center ">
                        <img class="img-fluid" src="Images/game-mechanics-cover.png" alt=" ">
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
                    <div class="col-md-6 square-bg-3 m-auto text-center my-3">
                    <img src="./Images/PaulBataga.jpg" height="300" width="250"/> 
                    </div>
                    <div class="text-center">
                        <p class="text-light"> Paul Carlo I. Bataga</p>
                    </div>
                </div>
                <div class="col-md-6 d-grid">
                    <div class="col-md-6 square-bg-3 m-auto text-center my-3">
                    <img src="./Images/CJAlmeron.jpg" height="300" width="250"/>   
                    </div>
                    <div class="text-center">
                        <p class="text-light"> Ciriaco John L. Almeron</p>
                    </div>
                </div>
                <div class="col-md-6 d-grid">
                    <div class="col-md-6 square-bg-3 m-auto text-center my-3">
                    <img src="./Images/AriesDiomampo.jpg" height="300" width="250"/>    
                    </div>
                    <div class="text-center">
                        <p class="text-light"> Aries S. Diomampo</p>
                    </div>
                </div>
                <div class="col-md-6 d-grid">
                    <div class="col-md-6 square-bg-3 m-auto text-center my-3">
                    <img src="./Images/KimCuevas.jpg" height="300" width="250"/>
                    </div>
                    <div class="text-center">
                        <p class="text-light"> Kimtribi Aleksie B. Cuevas</p> 
                    </div>
                </div>
            </div>

            <!-- Core Purpose layout -->

            <div class="row text-center">
                <h1 class="display-5 text-light fw-bolder">Our Core Purpose</h1>
            </div>
            <div class="container">
                <p class="text-light my-2 py-4">
                For aspiring game developers, understanding the core purposes of visual learning in game mechanics is crucial for creating engaging and immersive game experiences.<br /><br />

                1. <b>Refining Player Interaction</b> : The visuals in game mechanics play a vital role in providing feedback to the aspiring developers, helping them make informed decisions.
                Providing visual feedback is crucial for enhancing player understanding, engagement, and decision-making. Having gameplay feedback such as visual cues, rewards, information presentation contribute to creating
                a more immersive and enjoyable gaming experience. Furthermore, aspiring game developers can create games that effectively communicate with players and provide meaningful feedback throughout the gameplay.
                <br /><br />

                2. <b>Enhancing Immersion</b> : Enhancing immersion in Game Mechanics involves aspiring game developers can create a sense of presence and draw players into the game world.
                To help them create visually and audibly captivating experiences, designing responsive game mechanics, crafting engaging narratives, and finding the right balance between realism and abstraction. 
                By implementing these techniques, game developers can create immersive gaming experiences that captivates and engage players.
                <br /><br />

                3. <b>Engaging and Retaining Players</b> : To encourage the aspiring developers, we aim to encourage developers to make a visually appealing and aesthetically pleasing graphics to effectively capture and hold
                player's attention. Engaging visuals not only contribute to the overall enjoyment of the game but also serve as a motivation for players to continue playing.
    
                </p>
            </div>

        </section>

        <!-- Footer layout -->

        <footer class="footer-background">
            <div class="row d-grid">
                
                    <div class="col-sm-9 m-auto center_custom m-0">
                        <div class="row center_custom">
                            <img class="footer_logo" src="Images/VGM_logo.png" alt="Logo">
                        </div>
                        
                    </div>
                    
                    <div class="col-sm-9 m-auto">
                        <div class="row">
                            <p class="text-light footer_container fw-bolder">Join our community!</p>
                        </div>
                        <div class="row ">
                            <div class="col">
                                
                                <a class="fa-brands fa-telegram fa-2xl my-1 brand_custom"></a>
                                
                                <a class="fa-brands fa-discord fa-2xl my-1 brand_custom"></a>
                            </div>                            
                        </div>
                        <div class="row">
                            <p class="text-light footer_container fw-bolder">Stay tuned</p>
                        </div>
                        <div class="row ">
                            <div class="col">
                                <a class="fa-brands fa-facebook fa-2xl my-1 brand_custom"></a>
                                <a class="fa-brands fa-twitter  fa-2xl my-1 brand_custom"></a>
                                <a class="fa-brands fa-facebook-messenger  fa-2xl my-1 brand_custom"></a>
                            </div>                            
                        </div>
                        
                    </div>
                

            </div>
        </footer>

    </main>
    <script src="https://kit.fontawesome.com/d7d0e3dd38.js" crossorigin="anonymous"></script>
    <script src="/Content/custom.js"></script>
    
   

</asp:Content>
