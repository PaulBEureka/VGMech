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
                CodeText = "CodeText here Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce ullamcorper tellus ut eros placerat finibus. Vestibulum eleifend nulla ac lorem laoreet ornare. Nullam a venenatis felis, ut convallis purus. Proin at dignissim tortor. Etiam ut diam ante. Praesent volutpat, turpis ac vulputate finibus, massa felis consectetur arcu, a pellentesque erat nisl sit amet est. Pellentesque at imperdiet lacus. Integer ac suscipit erat. Morbi nibh augue, porttitor in congue ac, pulvinar sed augue.\r\n\r\nNullam sollicitudin egestas justo nec commodo. Nullam mollis sit amet velit eu cursus. Nam id dolor sollicitudin augue accumsan aliquam. Nullam mauris turpis, ornare at ullamcorper vel, sodales eu lorem. Duis at massa eu diam porta efficitur. Pellentesque molestie est libero, ac gravida elit sagittis vel. Nullam laoreet urna a turpis tincidunt accumsan. Integer malesuada dapibus mauris quis imperdiet. Sed ut elementum odio, at vehicula lectus.\r\n\r\nVivamus nec tellus interdum libero malesuada aliquam quis ac nisl. Mauris facilisis justo vitae nulla malesuada, eu egestas orci semper. Nam a nisl quis leo tristique aliquam. Duis ut sapien eget velit blandit viverra. Donec fermentum dolor vitae sem faucibus lobortis. Nulla lacinia pharetra diam et tincidunt. Mauris in maximus ligula. Aliquam dignissim enim et lectus hendrerit, a pellentesque lorem posuere. Sed suscipit risus aliquam, hendrerit mauris ut, fringilla massa. Mauris pulvinar vitae turpis nec hendrerit. Nulla et nunc blandit, tincidunt augue id, consectetur lacus.\r\n\r\nCras quis mi nisi. Praesent venenatis tristique imperdiet. Proin ac velit eget risus commodo vulputate et quis nibh. Fusce vulputate massa in est scelerisque accumsan. Pellentesque congue, leo eget tincidunt maximus, sem arcu malesuada mi, in pulvinar dolor lorem vitae diam. Cras aliquet massa eu lectus commodo, ac consequat lorem mollis. Morbi ipsum nisi, suscipit vitae pulvinar sed, mattis sit amet turpis. Duis dui nisl, condimentum vitae convallis non, malesuada vestibulum tellus. Donec at sapien egestas, molestie libero at, molestie libero. Etiam pellentesque ullamcorper elit, pulvinar tincidunt elit vehicula vel. Duis fringilla, metus quis condimentum ultricies, lectus felis vulputate augue, aliquam iaculis diam dui sit amet eros. Curabitur pretium quam risus, et accumsan massa lacinia ut. Nulla eleifend diam tellus, a imperdiet ex semper eget.", 
                CommonGenres = "Horror, PVP", PossibleVariations = "Platforming", PossibleCombinations="Any Mechanic"},
            new Card(){CardID="1", Title="SHOOTING MECHANIC", ImageSource = "Images/shooting_bg.png", ThumbSource = "Images/shooting_thumb.png", Description = "Learn how to add shooting elements!", UnityLink = "https://almers5.github.io/Game-Mechanics/ShootingMechanic/",
                CodeText = "CodeText here", 
                CommonGenres = "Horror, PVP", PossibleVariations = "Platforming", PossibleCombinations="Any Mechanic"},
            new Card(){CardID="2", Title="COLLECTING MECHANIC", ImageSource = "Images/jump_card_bg.png", ThumbSource = "Images/collecting_thumb.png", Description = "Want your game to have collecting mechanics? Go here!", UnityLink = "https://paulbeureka.github.io/UnityGame1/Game_1/",
                CodeText = "CodeText here", 
                CommonGenres = "Horror, PVP", PossibleVariations = "Platforming", PossibleCombinations="Any Mechanic"},
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
