using Microsoft.AspNet.SignalR;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
using System.Threading;
using System.Text;
using System.Runtime.CompilerServices;
using VisualMech.Classes;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using MySql.Data.Types;
using System.Text.RegularExpressions;

namespace VisualMech
{
    public class MyHub : Hub
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        
        DateTime currentDateTime = DateTime.Now;


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
            List<Comment> commentList = new List<Comment>();

            string mechanicTitle = info[0];
            string sessionUsername = info[1];

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = $@"
            SELECT c1.*, UserTable.username 
            FROM CommentTable c1
            INNER JOIN UserTable ON c1.user_id = UserTable.user_id 
            WHERE c1.mechanic_title = '{mechanicTitle}'
        ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await connection.OpenAsync();

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int commentId = reader.GetInt32("comment_id");
                            string username = reader["username"].ToString();
                            DateTime sqlDate = (DateTime)reader["comment_date"];
                            string raw_comment = reader["comment"].ToString();
                            int? parentCommentId = reader.IsDBNull(reader.GetOrdinal("parent_comment_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("parent_comment_id"));



                            string dateCommented = GetTimeAgo(sqlDate);
                            string comment = MakeNameBold(raw_comment);


                            if (parentCommentId != null)
                            {
                                Comment parentComment = commentList.FirstOrDefault(c => c.CommentId == parentCommentId);
                                if (parentComment != null)
                                {
                                    parentComment.RepliesList.Add(new Comment(commentId, username, dateCommented, comment));
                                }
                                else
                                {
                                    //Disregard, let the replyClick have the rootparent as parentComment
                                }
                            }
                            else
                            {
                                commentList.Add(new Comment(commentId, username, dateCommented, comment));
                            }


                        }
                    }
                }
            }

            string allCommentString = SortComment(commentList, sessionUsername);

            return allCommentString.ToString();
        }

        private string SortComment(List<Comment> commentList, string sessionUser)
        {
            StringBuilder allCommentString = new StringBuilder();

            foreach (Comment comment in commentList)
            {
                string replyButton = "";
                string viewRepliesButton = "";
                string replyContainer = "";
                string replyContainerDiv = "";

                if (comment.RepliesList.Count != 0)
                {
                    replyContainerDiv = $@"
                        <div id=""reply-container-{comment.CommentId}"" class=""reply-container"" aria-labelledby=""toggle-replies-btn-{comment.CommentId}"" aria-hidden=""true"">
                            <div class=""reply mt-4 text-justify float-left"" >";

                    foreach (Comment replyComment in comment.RepliesList)
                    {
                        replyButton = sessionUser != null ? $@"
                        <button id=""toggle-respond-btn-{replyComment.CommentId}"" class=""reply-button text-start"" type=""button"" aria-expanded=""false"" aria-controls=""respond-container-{replyComment.CommentId}"" onclick=""toggleRespond({replyComment.CommentId})"">
                         Reply
                        </button>                        " : $@"
                        <button id=""toggle-respond-btn-{replyComment.CommentId}"" class=""reply-button text-start"" type=""button"" aria-expanded=""false"" aria-controls=""respond-container-{replyComment.CommentId}"" onclick=""sign_in_comment()"">
                         Reply
                        </button>
                        ";

                        replyContainer = sessionUser != null ? $@"
                        <div id=""respond-container-{replyComment.CommentId}"" class=""respond-container"" aria-labelledby=""toggle-replies-btn-{replyComment.CommentId}"" aria-hidden=""true"">
                            <div class=""row container mb-3"">
                                <textarea placeholder=""Type your reply here {sessionUser}"" id=""replybox-{replyComment.CommentId}"" rows=""5"" class=""form-control"" style=""background-color: white; resize: none;"" draggable=""false"">@{replyComment.Username}</textarea>
                                <button type=""button"" id=""button-addon-reply-{replyComment.CommentId}"" class=""comment_button my-2 bg-danger"" onclick=""innerReply_Click({comment.CommentId}, {replyComment.CommentId})"">Reply</button>
                            </div>
                        </div>
                        " : "";




                        replyContainerDiv += $@"
                            <div class=""comment mt-4 float-left"" >
                                <div class=""row"">
                                    <div class=""col-1 text-end"">
                                        <img src= ""Images/person_icon.png"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                                    </div>
                                    <div class =""col-11"">
                                        <div class=""row"">
                                            <div class=""col"">
                                                <span class=""fw-bold"">{replyComment.Username}</span>
                                                <span class="""">{replyComment.DateCommented}</span>
                                            </div>
                                        </div>
                                        <div class=""row"">
                                            <p class="""">{replyComment.CommentContent}</p>
                                        </div>
                                        <div class=""row text-start"">
                                            <div class=""dropdown"">
                                                {replyButton}
                                                {replyContainer}
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>";


                    }

                    replyContainerDiv += " </div></div>";

                    viewRepliesButton = $@"
                        <button id=""toggle-replies-btn-{comment.CommentId}"" class=""toggle-replies-btn"" type=""button"" aria-expanded=""false"" aria-controls=""reply-container-{comment.CommentId}"" onclick=""toggleReplies({comment.CommentId})"">
                            view {comment.RepliesList.Count} replies
                        </button>
                        ";
                }

                

                replyContainer = sessionUser != null ? $@"
                    <div id=""respond-container-{comment.CommentId}"" class=""respond-container"" aria-labelledby=""toggle-replies-btn-{comment.CommentId}"" aria-hidden=""true"">
                        <div class=""row container mb-3"">
                            <textarea placeholder=""Type your reply here {sessionUser}"" id=""replybox-{comment.CommentId}"" rows=""5"" class=""form-control"" style=""background-color: white; resize: none;"" draggable=""false"">@{comment.Username}</textarea>
                            <button type=""button"" id=""button-addon-reply-{comment.CommentId}"" class=""comment_button my-2 bg-danger"" onclick=""reply_Click({comment.CommentId})"">Reply</button>
                        </div>
                    </div>
                " : "";


                replyButton = sessionUser != null ? $@"
                        <button id=""toggle-respond-btn-{comment.CommentId}"" class=""reply-button text-start"" type=""button"" aria-expanded=""false"" aria-controls=""respond-container-{comment.CommentId}"" onclick=""toggleRespond({comment.CommentId})"">
                         Reply
                        </button>                        " : $@"
                        <button id=""toggle-respond-btn-{comment.CommentId}"" class=""reply-button text-start"" type=""button"" aria-expanded=""false"" aria-controls=""respond-container-{comment.CommentId}"" onclick=""sign_in_comment()"">
                         Reply
                        </button>
                        ";

                allCommentString.Append($@"
                    <div class=""comment mt-4 float-left"" >
                        <div class=""row"">
                            <div class=""col-1 text-end"">
                                <img src= ""Images/person_icon.png"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                            </div>
                            <div class =""col-11"">
                                <div class=""row"">
                                    <div class=""col"">
                                        <span class=""fw-bold"">{comment.Username}</span>
                                        <span class="""">{comment.DateCommented}</span>
                                    </div>
                                </div>
                                <div class=""row"">
                                    <p class="""">{comment.CommentContent}</p>
                                </div>
                                <div class=""row text-start"">
                                    <div class=""dropdown"">
                                        {replyButton}
                                        {replyContainer}
                                    </div>
                                    <div>
                                        {viewRepliesButton}
                                        {replyContainerDiv}
                                    </div>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    
                ");
                
            }


            return allCommentString.ToString();
        }

        private static string GetTimeAgo(DateTime inputDate)
        {
            TimeSpan timeDifference = DateTime.Now - inputDate;

            if (timeDifference.TotalSeconds < 60)
            {
                return $"{(int)timeDifference.TotalSeconds} second{((int)timeDifference.TotalSeconds == 1 ? "" : "s")} ago";
            }
            else if (timeDifference.TotalMinutes < 60)
            {
                return $"{(int)timeDifference.TotalMinutes} minute{((int)timeDifference.TotalMinutes == 1 ? "" : "s")} ago";
            }
            else if (timeDifference.TotalHours < 24)
            {
                return $"{(int)timeDifference.TotalHours} hour{((int)timeDifference.TotalHours == 1 ? "" : "s")} ago";
            }
            else if (timeDifference.TotalDays < 30)
            {
                return $"{(int)timeDifference.TotalDays} day{((int)timeDifference.TotalDays == 1 ? "" : "s")} ago";
            }
            else if (timeDifference.TotalDays < 365)
            {
                int months = (int)(timeDifference.TotalDays / 30);
                return $"{months} month{(months == 1 ? "" : "s")} ago";
            }
            else
            {
                int years = (int)(timeDifference.TotalDays / 365);
                return $"{years} year{(years == 1 ? "" : "s")} ago";
            }
        }

        private static string MakeNameBold(string commentText)
        {
            
            string pattern = @"(@\w+)";

            
            string formattedComment = Regex.Replace(commentText, pattern, "<strong>$1</strong>");

            return formattedComment;
        }
    }
}
