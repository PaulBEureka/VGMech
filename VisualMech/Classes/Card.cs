using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMech.Content.Classes
{
    public class Card
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


        
        public string GetHtml()
        {
            return $@"
            <li class=""align-content-center justify-content-center m-auto"" >
                <a href=""SamplePage.aspx"" class=""card"" data-card-id =""{CardID}"">
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





    }
}