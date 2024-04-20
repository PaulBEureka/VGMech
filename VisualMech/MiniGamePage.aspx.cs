using Microsoft.AspNet.SignalR;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Classes;
using VisualMech.Content.Classes;

namespace VisualMech
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        private static string cardTitle = "";
        private static string sessionPlayer;
        private static List<MiniGameCard> miniGameCardList;


        protected void Page_Load(object sender, EventArgs e)
        {

            miniGameCardList = Session["MiniGameCardList"] as List<MiniGameCard>;

            if (!IsPostBack)
            {
                if (Request.QueryString["MiniGame"] != null)
                {
                    string MiniGameID = Request.QueryString["MiniGame"];

                    MiniGameCard selectedCard = miniGameCardList.FirstOrDefault(card => card.CardID == MiniGameID);
                    cardTitle = selectedCard.Title;

                    if (Session["Current_ID"] != null)
                    {
                        MiniGameLit.Text = selectedCard.GetContentHtml(Session["Current_ID"].ToString());
                        sessionPlayer = Session["CurrentUser"].ToString();
                        //recordVisitedPage() May be added to record played games;
                    }
                    else
                    {
                        MiniGameLit.Text = selectedCard.GetContentHtml();
                    }
                }
            }


            if (Session["Message"] != null)
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

        [WebMethod]
        public static string[] Get_Leaderboards()
        {
            string sessionPlayerCurrentRank = cardTitle + "playerRank";
            string sessionPlayerCurrentScore = cardTitle + "currentScore";

            string currentRank;
            string currentScore;

            HttpContext context = HttpContext.Current;

            if (context.Session[sessionPlayerCurrentRank] != null)
            {
                currentRank = context.Session[sessionPlayerCurrentRank].ToString();
                currentScore = context.Session[sessionPlayerCurrentScore].ToString();
            }
            else
            {
                currentRank = "10";
                currentScore = "0";
            }

            return new string[4] { cardTitle , sessionPlayer, currentRank, currentScore};
        }

        [WebMethod]
        public static void UpdateSessionInfo(string currentRank, string currentScore)
        {
            string sessionPlayerCurrentRank = cardTitle + "playerRank";
            string sessionPlayerCurrentScore = cardTitle + "currentScore";

            HttpContext context = HttpContext.Current;

            if (context != null)
            {
                context.Session[sessionPlayerCurrentRank] = currentRank;
                context.Session[sessionPlayerCurrentScore] = currentScore;
            }

            


        }





    }
}