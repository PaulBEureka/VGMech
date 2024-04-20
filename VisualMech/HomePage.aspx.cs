using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Classes;
using VisualMech.Content.Classes;

namespace VisualMech
{
    public partial class _Default : Page
    {
        
        private string learnCardString = "", miniGameCardString = "";
        private readonly CardManager<LearnCard> learnCardManager = new CardManager<LearnCard>("Learn.json");
        private readonly CardManager<MiniGameCard> miniGameCardManager = new CardManager<MiniGameCard>("MiniGame.json");
        private static List<LearnCard> learnCardList;
        private static List<MiniGameCard> miniGameCardList;

        protected void Page_Load(object sender, EventArgs e)
        {

            learnCardList = learnCardManager.GetAllCards();
            miniGameCardList = miniGameCardManager.GetAllCards();

            foreach (LearnCard card in learnCardList)
            {
                learnCardString += card.GetCardHtml();
            }

            foreach(MiniGameCard card in miniGameCardList)
            {
                miniGameCardString += card.GetCardHtml();
            }


            Session["CardList"] = learnCardList;
            Session["MiniGameCardList"] = miniGameCardList;

            if (!IsPostBack)
            {
                litCardHtml.Text = learnCardString;
                litCardMiniGameHtml.Text = miniGameCardString;

                if (Session["Message"] != null && Session["CurrentUser"] == null) 
                {
                    string script = $@"
                            toastr.options = {{
                              ""closeButton"": false,
                              ""debug"": false,
                              ""newestOnTop"": false,
                              ""progressBar"": false,
                              ""positionClass"": ""toast-top-right"",
                              ""preventDuplicates"": false,
                              ""onclick"": null,
                              ""showDuration"": ""300"",
                              ""hideDuration"": ""1000"",
                              ""timeOut"": ""10000"",
                              ""extendedTimeOut"": ""1000"",
                              ""showEasing"": ""swing"",
                              ""hideEasing"": ""linear"",
                              ""showMethod"": ""fadeIn"",
                              ""hideMethod"": ""fadeOut""
                            }}
                            toastr['success']('{Session["Message"].ToString()}', 'Notification');
                    ";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", script, true);
                    Session["Message"] = null;
                }
                else if (Session["Message"] != null && Session["CurrentUser"] != null)
                {
                    string script = $@"
                            toastr.options = {{
                              ""closeButton"": false,
                              ""debug"": false,
                              ""newestOnTop"": false,
                              ""progressBar"": false,
                              ""positionClass"": ""toast-top-right"",
                              ""preventDuplicates"": false,
                              ""onclick"": null,
                              ""showDuration"": ""300"",
                              ""hideDuration"": ""1000"",
                              ""timeOut"": ""10000"",
                              ""extendedTimeOut"": ""1000"",
                              ""showEasing"": ""swing"",
                              ""hideEasing"": ""linear"",
                              ""showMethod"": ""fadeIn"",
                              ""hideMethod"": ""fadeOut""
                            }}
                            toastr['success']('{Session["Message"].ToString()}', 'Notification');
                    ";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", script, true);
                    Session["Message"] = null;
                }
            }
        
        
            
        
        }


        

    }

}
