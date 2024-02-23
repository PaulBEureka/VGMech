using Microsoft.AspNet.SignalR;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
using System.Threading;

namespace VisualMech
{
    public class MyHub : Hub
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        
        

        public async Task UpdateLeaderboards()
        {
            string leaderboardsData = await RetrieveLeaderboardsDataAsync();
            Clients.All.updateLeaderboards(leaderboardsData);
        }


        public async Task UpdateComments(string mecanicTitle)
        {
            string commentsData = await RetrieveCommentsData(mecanicTitle);
            Clients.All.updateComments(commentsData);
        }

        private async Task<string> RetrieveLeaderboardsDataAsync()
        {
            string allLeaderboardsString = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT *, UserTable.username FROM GameRecordTable " +
                               "INNER JOIN UserTable ON GameRecordTable.user_id = UserTable.user_id " +
                               "WHERE game_title = 'Block Breaker' ORDER BY game_score DESC LIMIT 5;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await connection.OpenAsync();

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        int rankingNum = 1;
                        int addColor = 20;

                        while (await reader.ReadAsync())
                        {
                            string username = reader["username"].ToString();
                            string ranking_date = reader["ranking_date"].ToString();
                            string score = reader["game_score"].ToString();

                            DateTime dateTime;
                            if (DateTime.TryParse(ranking_date, out dateTime))
                            {
                                string dateString = dateTime.ToString("M/d/yyyy");

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
                                                    </div>";
                                rankingNum++;
                                addColor += 20;
                            }
                        }
                    }
                }
            }

            return allLeaderboardsString;
        }



        private async Task<string> RetrieveCommentsData(string mechanicTitle)
        {
            string allCommentString = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Step 2: Write SQL Query
                string query = $@"
                SELECT CommentTable.*, UserTable.username 
                FROM CommentTable 
                INNER JOIN UserTable ON CommentTable.user_id = UserTable.user_id 
                WHERE CommentTable.mechanic_title = '{mechanicTitle}'
            ";

                // Step 3: Execute Query
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await connection.OpenAsync();

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        // Step 4: Process Data
                        while (await reader.ReadAsync())
                        {
                            string username = reader["username"].ToString();
                            string dateCommented = reader["comment_date"].ToString();
                            string comment = reader["comment"].ToString();

                            allCommentString += $@"
                        <div class=""comment mt-4 text-justify float-left"" >
                            <img src=""Images/person_icon.png"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                            <h4>{username}</h4>
                            <span>- {dateCommented}</span>
                            <br>
                            <p>{comment}</p>
                        </div>
                        <div>
                            <hr />
                        </div>";
                        }
                    }
                }
            }

            return allCommentString;
        }




        
    }
}
