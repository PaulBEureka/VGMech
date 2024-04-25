using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using VisualMech.Classes;

namespace VisualMech.Content.Classes
{
    public class LearnCard: Card
    {
        public string CardID { get; set; }
        public string ImageSource { get; set; }
        public string ThumbSource { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string UnityLink { get; set; }
        public string CodeText { get;set; }
        public string CommonGenres { get; set; }
        public string PossibleVariations { get; set; }
        public string PossibleCombinations { get; set; }
        public string InteractiveControls { get; set; }


        
        public override string GetCardHtml()
        {
            return $@"
            <li class=""align-content-center justify-content-center m-auto"" >
                <a href=""SamplePage.aspx?Learn={CardID}"" class=""card"">
                    <img src=""{ImageSource}"" class=""card__image"" alt="""" />
                    <div class=""card__overlay"">
                        <div class=""card__header"">
                            <svg class=""card__arc"" xmlns=""http://www.w3.org/2000/svg""></svg>
                            <img class=""card__thumb"" src=""{ThumbSource}"" alt="""" />
                            <div class=""card__header-text"">
                                <h3 class=""card__title"">{Title}</h3>
                                <span class=""card__status"">Click to learn</span>
                            </div>
                        </div>
                        <p class=""fw-bolder card__title_description"">Description:</p>
                        <p class=""card__description"">{Description}</p>
                    </div>
                </a>
            </li>";
        }

        public override string GetContentHtml(string sessionUserID = null)
        {
            return $@"
            <div  class=""row text-center "">
                <h1 class=""display-4 mini_custom_padding fw-bolder text-white"">{Title}</h1>
                
            </div>

            <!--Interactive Demonstration and Coding Implementation layout -->
            
            <div class=""row d-grid text-center"">
                <div class=""row justify-content-center text-center m-auto"">
                    <div class=""col-md-6 justify-content-center text-center mx-md-3 gameMech-section-holders my-5"">
                        <div class=""row"">
                            <h2 class=""text-light fw-bolder"">Interactive Demonstration</h2>
                            
                    </div>
                    <div class=""ratio ratio-16x9 d-grid m-auto mb-3 pb-3"">
                        <iframe src=""{UnityLink}"" class=""unityLayout m-auto"" allowfullscreen=""allowfullscreen"" title=""{Title.ToUpper()}"" scrolling=""no""></iframe>
                    </div>
                    <div>
                        <h4 class=""text-light fw-bolder gameMech-padding-Title pb-3"">INTERACTIVE CONTROLS</h4>
                        <p class=""text-light m-0 gameMech-padding-text fs-6"">{InteractiveControls}</p>
                    </div>
                    </div>
                    <div class=""col-md-6 d-grid gameMech-section-holders mx-md-3 my-5 "">
                        <div class=""row"">
                            <h2 class=""text-light fw-bolder"">Coding Implementation</h2>
                            <div class=""col align-items-end text-end"">
                                <button class=""copy_button text-end"" type=""button"" onclick=""copyCodeText()"">
                                    <i class=""fa-solid fa-copy"" aria-hidden=""true""></i> Copy text
                                </button>
                            </div>
                        </div>
                        <div class=""row justify-content-center m-auto gameMech-code-holder"">
                            <p class= ""text-start"" id=""codeText"">{CodeText}</>
                        </div>
                    </div>
                </div>
            </div>


            <!-- Information layout -->
            <div class=""row d-grid gameMech-layout"">
                <div class=""container gameMech-information-holder m-auto p-3"">
                    <!-- Game genres layout -->
                    <h5 class=""text-light fw-bolder gameMech-padding-Title"">Commonly Used Game Genres:</h5>
                    <p class=""text-light m-0 gameMech-padding-text"">{CommonGenres}</p>
                    
                    <!-- Possible Variation layout -->
                    <h5 class=""text-light fw-bolder gameMech-padding-Title"">Possible Variation of this Game Mechanic:</h5>
                    <p class=""text-light m-0 gameMech-padding-text"">{PossibleVariations}</p>

                    <!-- Possible Combination layout -->
                    <h5 class=""text-light fw-bolder gameMech-padding-Title"">Possible Game Mechanics Combination:</h5>
                    <p class=""text-light m-0 gameMech-padding-text"">{PossibleCombinations}</p>

                </div>
            </div>";
        }


        public override string GetAdminCardPreviewHtml()
        {
            return $@"
            <div class=""align-content-center justify-content-center m-auto py-3 "" >
                <a class=""card shadow"">
                    <img src=""{ImageSource}"" class=""card__image"" alt="""" />
                    <div class=""card__overlay"">
                        <div class=""card__header"">
                            <svg class=""card__arc"" xmlns=""http://www.w3.org/2000/svg""></svg>
                            <img class=""card__thumb"" src=""{ThumbSource}"" alt="""" />
                            <div class=""card__header-text"">
                                <h3 class=""card__title"">{Title}</h3>
                                <span class=""card__status"">Click to learn</span>
                            </div>
                        </div>
                        <p class=""fw-bolder card__title_description"">Description:</p>
                        <p class=""card__description"">{Description}</p>
                    </div>
                </a>
            </div>";
        }
    }



}