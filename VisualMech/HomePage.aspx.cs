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
        
        private string cardString = "";
        private CardManager cardManager = new CardManager("Learn.json");


        protected void Page_Load(object sender, EventArgs e)
        {

            List<LearnCard> cardList = cardManager.GetAllCards();

            foreach (LearnCard card in cardList)
            {
                cardString += card.GetCardHtml();
            }

            Session["CardList"] = cardList;

            if (!IsPostBack)
            {
                litCardHtml.Text = cardString;
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


        [WebMethod]
        public static void ProcessIT(int cardId)
        {
            HttpContext context = HttpContext.Current;

            if (context != null)
            {
                context.Session["LearnId"] = cardId;
            }
        }
    }

}
