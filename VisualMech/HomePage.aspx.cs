using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Content.Classes;

namespace VisualMech
{
    public partial class _Default : Page
    {
        private string imageSource = "Images/jump_card_bg.png";
        private string thumbSource = "Images/movement_icon.png";
        
        private List<Tuple<string, string, string, string, string, string, string>> cardContents = new List<Tuple<string, string, string, string, string, string, string>> //Populate this to add game mechanic cards in homepage
        {
            //Format: Title, Description, Page name of the aspx to be linked
            Tuple.Create("MOVEMENT MECHANIC", "Get to know movement integration, variations, and more!", "CodeText here", "Horror, PVP", "Platforming", "Any Mechanic","https://almers5.github.io/Game-Mechanics/MovementMechanic"),
            Tuple.Create("SHOOTING MECHANIC", "Learn how to add shooting elements!", "CodeText here", "Horror, PVP", "Platforming", "Any Mechanic", "https://almers5.github.io/Game-Mechanics/ShootingMechanic/"),
            Tuple.Create("COLLECTING MECHANIC", "Get to know movement integration, variations, and more!", "CodeText here", "Horror, PVP", "Platforming", "Any Mechanic", "https://paulbeureka.github.io/UnityGame1/Game_1/")
        };

        private string cardString = "";

        public List<Card> cardList = new List<Card>();

        protected void Page_Load(object sender, EventArgs e)
        {
            int cardIndex = 0;

            foreach (Tuple<string, string, string, string, string, string, string> content in cardContents)
            {
                var card = new Card
                {
                    CardID = cardIndex.ToString(),
                    ImageSource = imageSource,
                    ThumbSource = thumbSource,
                    Title = content.Item1,
                    Status = "Click to learn",
                    Description = content.Item2,
                    ConnectedPageName = "SamplePage",
                    UnityLink = content.Item7,
                    CodeText = content.Item3,
                    CommonGenres = content.Item4,
                    PossibleVariations = content.Item5,
                    PossibleCombinations = content.Item6
                };

                cardIndex++;
                cardString += card.GetHtml();
                cardList.Add(card);
                
            }

            Session["CardList"] = cardList;

            if (!IsPostBack)
            {
                litCardHtml.Text = cardString;
            }
        }


        [WebMethod]
        public static void ProcessIT(int cardId)
        {
            // Get the current HttpContext
            HttpContext context = HttpContext.Current;

            // Check if HttpContext is available
            if (context != null)
            {
                // Use the session from the context to set the variable
                context.Session["CardId"] = cardId;
            }
        }
    }

}
