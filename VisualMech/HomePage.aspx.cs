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
       
        private List<Card> cardList = new List<Card>() // Add Cards here and edit contents based on field, also increment the CardID each entry of card
        {
            new Card(){CardID="0", Title="MOVEMENT MECHANIC", ImageSource = "Images/movement_bg.png", ThumbSource = "Images/movement_icon.png", Description = "Get to know movement integration, variations, and more!", UnityLink = "https://almers5.github.io/Game-Mechanics/MovementMechanic",
            CodeText = "CodeText here", CommonGenres = "Horror, PVP", PossibleVariations = "Platforming", PossibleCombinations="Any Mechanic"},
            new Card(){CardID="1", Title="SHOOTING MECHANIC", ImageSource = "Images/shooting_bg.png", ThumbSource = "Images/shooting_thumb.png", Description = "Learn how to add shooting elements!", UnityLink = "https://almers5.github.io/Game-Mechanics/ShootingMechanic/",
            CodeText = "CodeText here", CommonGenres = "Horror, PVP", PossibleVariations = "Platforming", PossibleCombinations="Any Mechanic"},
            new Card(){CardID="2", Title="COLLECTING MECHANIC", ImageSource = "Images/jump_card_bg.png", ThumbSource = "Images/collecting_thumb.png", Description = "Want your game to have collecting mechanics? Go here!", UnityLink = "https://paulbeureka.github.io/UnityGame1/Game_1/",
            CodeText = "CodeText here", CommonGenres = "Horror, PVP", PossibleVariations = "Platforming", PossibleCombinations="Any Mechanic"},
        };
        
        private string cardString = "";


        protected void Page_Load(object sender, EventArgs e)
        {

            foreach (Card card in cardList)
            {
                cardString += card.GetHtml();
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
