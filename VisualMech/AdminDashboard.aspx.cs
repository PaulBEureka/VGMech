using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Classes;
using VisualMech.Content.Classes;

namespace VisualMech
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        private readonly CardManager<LearnCard> learnCardManager = new CardManager<LearnCard>("Learn.json");
        private readonly CardManager<MiniGameCard> miniGameCardManager = new CardManager<MiniGameCard>("MiniGame.json");

        protected void Page_Load(object sender, EventArgs e)
        {
            InformationCardsPanel.Text = GenerateCardInfos();
        }

        private string GenerateCardInfos()
        {
            string temp = "";
            string userCount = GetTotalUsers();

            //Total Users
            temp += $@"<div class=""col-xl-3 col-md-6 mb-4"">
                            <div class=""card border-left-primary shadow h-100 py-2"">
                                <div class=""card-body"">
                                    <div class=""row no-gutters align-items-center"">
                                        <div class=""col mr-2"">
                                            <div class=""text-xs font-weight-bold text-primary text-uppercase mb-1"">
                                                Total number of users</div>
                                            <div class=""h5 mb-0 font-weight-bold text-gray-800"">{userCount}</div>
                                        </div>
                                        <div class=""col-auto"">
                                            <i class=""fa-regular fa-circle-user fa-2x text-black""></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>";

            //Total Learn Cards
            int learnCount = learnCardManager.GetAllCards().Count;

            temp += $@"<div class=""col-xl-3 col-md-6 mb-4"">
                            <div class=""card border-left-success shadow h-100 py-2"">
                                <div class=""card-body"">
                                    <div class=""row no-gutters align-items-center"">
                                        <div class=""col mr-2"">
                                            <div class=""text-xs font-weight-bold text-success text-uppercase mb-1"">
                                                Total Learn cards created</div>
                                            <div class=""h5 mb-0 font-weight-bold text-gray-800"">{learnCount}</div>
                                        </div>
                                        <div class=""col-auto"">
                                            <i class=""fa-solid fa-book fa-2x text-black""></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>";

            //Total Mini Game Cards
            int miniCount = miniGameCardManager.GetAllCards().Count;

            temp += $@"<div class=""col-xl-3 col-md-6 mb-4"">
                            <div class=""card border-left-info  shadow h-100 py-2"">
                                <div class=""card-body"">
                                    <div class=""row no-gutters align-items-center"">
                                        <div class=""col mr-2"">
                                            <div class=""text-xs font-weight-bold text-info  text-uppercase mb-1"">
                                                Total Minigame cards created</div>
                                            <div class=""h5 mb-0 font-weight-bold text-gray-800"">{miniCount}</div>
                                        </div>
                                        <div class=""col-auto"">
                                            <i class=""fa-solid fa-gamepad fa-2x text-black""></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>";


            //Total number of comments 
            string totalComments = GetTotalNumberComments();

            temp += $@"<div class=""col-xl-3 col-md-6 mb-4"">
                            <div class=""card border-left-warning   shadow h-100 py-2"">
                                <div class=""card-body"">
                                    <div class=""row no-gutters align-items-center"">
                                        <div class=""col mr-2"">
                                            <div class=""text-xs font-weight-bold text-warning   text-uppercase mb-1"">
                                                Total number of comments</div>
                                            <div class=""h5 mb-0 font-weight-bold text-gray-800"">{totalComments}</div>
                                        </div>
                                        <div class=""col-auto"">
                                            <i class=""fa-solid fa-comment fa-2x text-black""></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>";


            return temp;
        }
    
    
        private string GetTotalUsers()
        {
            string temp = "";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT COUNT(*) FROM user where role = 'user'";
                    int count;

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        count = Convert.ToInt32(selectCommand.ExecuteScalar());
                        temp = count.ToString();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            return temp;
        }
        
        private string GetTotalNumberComments()
        {
            string temp = "";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT COUNT(*) FROM comment";
                    int count;

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        count = Convert.ToInt32(selectCommand.ExecuteScalar());
                        temp = count.ToString();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            return temp;
        }
        
        
    }
}