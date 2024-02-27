using Microsoft.AspNet.SignalR;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
using System.Threading;
using System.Text;
using System.Runtime.CompilerServices;

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


        public async Task UpdateComments(string[] stringArr)
        {

            string commentsData = await RetrieveCommentsData(stringArr);
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



        private async Task<string> RetrieveCommentsData(string[] info)
        {
            StringBuilder allCommentString = new StringBuilder();

            string mechanicTitle = info[0];
            string sessionUsername = info[1];

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Step 2: Write SQL Query
                string query = $@"
            SELECT c1.*, UserTable.username 
            FROM CommentTable c1
            INNER JOIN UserTable ON c1.user_id = UserTable.user_id 
            WHERE c1.mechanic_title = '{mechanicTitle}'
        ";

                // Step 3: Execute Query
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await connection.OpenAsync();

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        HttpContext context = HttpContext.Current;
                        // Step 4: Process Data
                        while (await reader.ReadAsync())
                        {
                            int commentId = reader.GetInt32("comment_id");
                            string username = reader["username"].ToString();
                            string dateCommented = reader["comment_date"].ToString();
                            string comment = reader["comment"].ToString();
                            int? parentCommentId = reader.IsDBNull(reader.GetOrdinal("parent_comment_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("parent_comment_id"));

                            


                            if (parentCommentId != null)
                            {
                                string currentString = allCommentString.ToString();
                                if (currentString.Contains("<!--CurrentReply"+parentCommentId+"-->")){
                                    if (sessionUsername != null)
                                    {
                                        allCommentString.Replace("<!--CurrentReply" + parentCommentId + "-->",
                                        $@"
                                                <img src=""Images/person_icon.png"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                                                <h4>{username}</h4>
                                                <span>- {dateCommented}</span>
                                                <br>
                                                <p>{comment}</p>

                                                        
                                                <div class=""dropdown"">
                                                    <button id=""toggle-respond-btn-{commentId}"" class=""reply-button w-25 text-start"" type=""button"" aria-expanded=""false"" aria-controls=""respond-container-{commentId}"" onclick=""toggleRespond({commentId})"">
                                                        Reply
                                                    </button>

                                                    <div id=""respond-container-{commentId}"" class=""respond-container"" aria-labelledby=""toggle-replies-btn-{commentId}"" aria-hidden=""true"">
                                                        <div class=""row container mb-3"">
                                                            <textarea placeholder=""Type your reply here"" id=""replybox-{commentId}"" rows=""5"" class=""form-control"" style=""background-color: white; resize: none;"" draggable=""false""></textarea>
                                                            <button type=""button"" id=""button-addon-reply-{commentId}"" class=""comment_button my-2 bg-danger"" onclick=""reply_Click({commentId})"">Reply</button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr />
                                                <!--CurrentReply{parentCommentId}-->");
                                    }
                                    else
                                    {
                                        allCommentString.Replace("<!--CurrentReply" + parentCommentId + "-->",
                                        $@"
                                                <img src=""Images/person_icon.png"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                                                <h4>{username}</h4>
                                                <span>- {dateCommented}</span>
                                                <br>
                                                <p>{comment}</p>

                                                        
                                                <div class=""dropdown"">
                                                    <button id=""toggle-respond-btn-{commentId}"" class=""reply-button w-25 text-start"" type=""button"" aria-expanded=""false"" aria-controls=""respond-container-{commentId}"" onclick=""sign_in_comment()"">
                                                       Reply
                                                    </button>
                                                </div>
                                                <hr />
                                                <!--CurrentReply{parentCommentId}-->");
                                    }
                                }
                                else
                                {
                                    int tempReplyCount = GetReplyCount((int)parentCommentId);


                                    if (sessionUsername != null)
                                    {
                                        allCommentString.Replace("<!--" + parentCommentId + "-->",
                                        $@"
                                        <div class=""replies"">
                                            <div class=""dropdown"">
                                                <button id=""toggle-replies-btn-{commentId}"" class=""toggle-replies-btn"" type=""button"" aria-expanded=""false"" aria-controls=""reply-container-{commentId}"" onclick=""toggleReplies({commentId})"">
                                                    view {tempReplyCount.ToString()} replies
                                                </button>

                                        
                                                <div id=""reply-container-{commentId}"" class=""reply-container"" aria-labelledby=""toggle-replies-btn-{commentId}"" aria-hidden=""true"">
                                                    <div class=""reply mt-4 text-justify float-left"" >
                                                        <img src=""Images/person_icon.png"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                                                        <h4>{username}</h4>
                                                        <span>- {dateCommented}</span>
                                                        <br>
                                                        <p>{comment}</p>

                                                        <div class=""dropdown"">
                                                            <button id=""toggle-respond-btn-{commentId}"" class=""reply-button w-25 text-start"" type=""button"" aria-expanded=""false"" aria-controls=""respond-container-{commentId}"" onclick=""toggleRespond({commentId})"">
                                                                Reply
                                                            </button>

                                                            <div id=""respond-container-{commentId}"" class=""respond-container"" aria-labelledby=""toggle-replies-btn-{commentId}"" aria-hidden=""true"">
                                                                <div class=""row container mb-3"">
                                                                    <textarea placeholder=""Type your reply here"" id=""replybox-{commentId}"" rows=""5"" class=""form-control"" style=""background-color: white; resize: none;"" draggable=""false""></textarea>
                                                                    <button type=""button"" id=""button-addon-reply-{commentId}"" class=""comment_button my-2 bg-danger"" onclick=""reply_Click({commentId})"">Reply</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <!--CurrentReply{parentCommentId}-->
                                                    
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                
                                    ");
                                    }
                                    else
                                    {
                                        allCommentString.Replace("<!--" + parentCommentId + "-->",
                                        $@"
                                        <div class=""replies"">
                                            <div class=""dropdown"">
                                                <button id=""toggle-replies-btn-{commentId}"" class=""toggle-replies-btn"" type=""button"" aria-expanded=""false"" aria-controls=""reply-container-{commentId}"" onclick=""toggleReplies({commentId})"">
                                                    view {tempReplyCount.ToString()} replies
                                                </button>

                                        
                                                <div id=""reply-container-{commentId}"" class=""reply-container"" aria-labelledby=""toggle-replies-btn-{commentId}"" aria-hidden=""true"">
                                                    <div class=""reply mt-4 text-justify float-left"" >
                                                        <img src=""Images/person_icon.png"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                                                        <h4>{username}</h4>
                                                        <span>- {dateCommented}</span>
                                                        <br>
                                                        <p>{comment}</p>

                                                        <div class=""dropdown"">
                                                            <button id=""toggle-respond-btn-{commentId}"" class=""reply-button w-25 text-start"" type=""button"" aria-expanded=""false"" aria-controls=""respond-container-{commentId}"" onclick=""sign_in_comment()"">
                                                               Reply
                                                            </button>
                                                        </div>
                                                        <hr />
                                                        <!--CurrentReply{parentCommentId}-->
                                                    </div>
                                                    
                                                    
                                                </div>
                                            </div>
                                        </div>
                                
                                    ");
                                    }

                                    
                                }

                            }
                            else
                            {
                                // Begin comment div
                                allCommentString.Append($@"
                            <div class=""comment mt-4 text-justify float-left"" >
                            <img src=""Images/person_icon.png"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                            <h4>{username}</h4>
                            <span>- {dateCommented}</span>
                            <br>
                            <p>{comment}</p>");

                                if (sessionUsername != null)
                                {
                                    allCommentString.Append($@"  
                                    <div class=""dropdown"">
                                        <button id=""toggle-respond-btn-{commentId}"" class=""reply-button w-25 text-start"" type=""button"" aria-expanded=""false"" aria-controls=""respond-container-{commentId}"" onclick=""toggleRespond({commentId})"">
                                            Reply
                                        </button>

                                        <div id=""respond-container-{commentId}"" class=""respond-container"" aria-labelledby=""toggle-replies-btn-{commentId}"" aria-hidden=""true"">
                                            <div class=""row container mb-3"">
                                                <textarea placeholder=""Type your reply here"" id=""replybox-{commentId}"" rows=""5"" class=""form-control"" style=""background-color: white; resize: none;"" draggable=""false""></textarea>
                                                <button type=""button"" id=""button-addon-reply-{commentId}"" class=""comment_button my-2 bg-danger"" onclick=""reply_Click({commentId})"">Reply</button>
                                            </div>
                                        </div>
                                    </div>


                                    <!--{commentId}-->
                            

                                    </div>
                                    <hr />"
                                        );
                                }
                                else
                                {
                                    allCommentString.Append($@"  
                                    <div class=""dropdown"">
                                        <button id=""toggle-respond-btn-{commentId}"" class=""reply-button w-25 text-start"" type=""button"" aria-expanded=""false"" aria-controls=""respond-container-{commentId}"" onclick=""sign_in_comment()"">
                                           Reply
                                        </button>
                                    </div>


                                    <!--{commentId}-->
                            

                                    </div>
                                    <hr />"
                                        );
                                }

                                    




                            }




                            
                        }
                    }
                }
            }

            return allCommentString.ToString();
        }



        private int GetReplyCount(int commentID)
        {
            int replyCount = 0;
            

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = $@"SELECT COUNT(*) AS reply_count FROM CommentTable WHERE parent_comment_id = {commentID};";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    // Execute the query and get the result
                    object result = command.ExecuteScalar();

                    // Check if the result is not null and can be converted to int
                    if (result != null && int.TryParse(result.ToString(), out replyCount))
                    {
                        // Conversion successful, replyCount contains the count
                    }
                }
            }

            return replyCount;
        }





    }
}
