using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Content.Classes;

namespace VisualMech
{
    public partial class _Default : Page
    {
        private string imageSource = "Images/jump_card_bg.png";
        private string thumbSource = "Images/movement_icon.png";
        
        private List<Tuple<string, string, string>> cardContents = new List<Tuple<string, string, string>> //Populate this to add game mechanic cards in homepage
        {
            //Format: Title, Description, Page name of the aspx to be linked
            Tuple.Create("MOVEMENT MECHANIC", "Get to know movement integration, variations, and more!", "SamplePage"),
            Tuple.Create("SHOOTING MECHANIC", "Learn how to add shooting elements!", "SamplePage"),
            Tuple.Create("MOVEMENT MECHANIC", "Get to know movement integration, variations, and more!", "SamplePage"),
            Tuple.Create("SHOOTING MECHANIC", "Learn how to add shooting elements!", "SamplePage"),
            Tuple.Create("MOVEMENT MECHANIC", "Get to know movement integration, variations, and more!", "SamplePage"),
            Tuple.Create("SHOOTING MECHANIC", "Learn how to add shooting elements!", "SamplePage"),
            Tuple.Create("MOVEMENT MECHANIC", "Get to know movement integration, variations, and more!", "SamplePage"),
            Tuple.Create("SHOOTING MECHANIC", "Learn how to add shooting elements!", "SamplePage")
        };

        private string cardString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            foreach (Tuple<string, string, string> content in cardContents)
            {
                var card = new Card
                {
                    ImageSource = imageSource,
                    ThumbSource = thumbSource,
                    Title = content.Item1,
                    Status = "Click to learn",
                    Description = content.Item2,
                    ConnectedPageName = content.Item3
                };

                cardString += card.GetHtml();
            }

            if (!IsPostBack)
            {
                litCardHtml.Text = cardString;
            }
        }
    }
}