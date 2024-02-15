using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisualMech
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\VGMechDatabase.mdf;Integrated Security=True";
        public string allLeaderboardsString = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            GetLeaderboards();
        }



        private void GetLeaderboards()
        {

            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TOP 5 *, UserTable.username FROM GameRecordTable " +
                    "INNER JOIN UserTable ON GameRecordTable.user_id = UserTable.user_id WHERE game_title = 'Block Breaker' ORDER BY game_score DESC;";
               


                // Step 3: Execute Query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Step 4: Process Data

                        int rankingNum = 1;
                        int addColor = 20;
                        while (reader.Read())
                        {
                            string username = reader["username"].ToString();
                            string ranking_date = reader["ranking_date"].ToString();
                            string score = reader["game_score"].ToString();


                            // Parse the string into a DateTime object
                            DateTime dateTime = DateTime.Parse(ranking_date);

                            // Format the DateTime object to a string with just the date part
                            string dateString = dateTime.ToString("M/d/yyyy"); // or "MM/dd/yyyy" for leading zeros in month and day


                            allLeaderboardsString += $@"<div class=""row mt-3"" style=""background-color: rgb({91 + addColor},{7 + addColor}, {21 + addColor});"">
                                <div class=""col-md-1 d-grid"">
                                    <p class=""fw-bolder text-white fs-3 my-3"">{rankingNum.ToString()}.</p>
                                </div>
                                <div class=""col-md-1 image-container"">
                                    <img src=""Images/person_icon_white.png"" alt="""" class=""rounded-circle"" width=""60"" height=""60"">
                                </div>
                                <div class=""col-md-9 leaderboard_white_rec_round"">
                                    <div class=""row"">
                                        <div class = ""col-4 my-3"">
                                            <p class=""fw-bolder text-start"">{username.ToUpper()}</p>
                                        </div>
                                        <div class ="" col-4 my-3"">
                                            <p class=""fw-bolder text-end"">{dateString}</p>
                                        </div>
                                        <div class ="" col-4 my-3"">
                                            <p class=""fw-bolder text-end"">SCORE: {score}</p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div>
                                <hr / class=""text-white"">
                            </div>"";";

                            rankingNum++;
                            addColor += 20; ;
                        }
                    }
                }
            }

            LeaderboardHTML1.Text = allLeaderboardsString;

        }
    }
}