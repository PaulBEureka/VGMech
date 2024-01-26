<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="VisualMech._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <script src="/Content/custom_js.js"></script>
    <main>
        <section class="intro-section-color" aria-labelledby="aspnetTitle">
            <div class="container">
                <div class="row my-3 py-5">
                    <div class="col align-content-center justify-content-center text-center my-3 py-5">
                        <h1 class=" display-3 text-center text-light fw-bolder">Redefining <br /> Learning of Game<br /> Mechanics!</h1>
                    </div>
                    <div class="col align-content-center justify-content-center text-center ">
                        <img class="w-100" src="Images/game-mechanics-cover.png" alt=" ">
                    </div>
                </div>
            </div>
        </section>

        <section class="container" >
            <div id="product-card">
                <div id="product-front">
                  <div class="shadow"></div>
                    <img class="w-100" src="Images/jump_card_bg.png" alt="" />
                    <div class="image_overlay"></div>
                    <a id="view_details">View details</a>
                    <div class="stats">         
                        <div class="stats-container">
                            <span class="product_name">Movement</span>    
                            <p>Click to learn!</p>                                            
                    
                            <div class="product-options">
                                <strong>DESCRIPTION:</strong>
                                <span>Get to know movement integration, variations, and more!</span>
                            </div>                       
                        </div>
                     </div>
                    </div>
                </div>
                 
             
        </section>  

          <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>

        
    </main>
   

</asp:Content>
