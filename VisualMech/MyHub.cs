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
using System.Data.SqlTypes;
using System.Xml.Linq;

namespace VisualMech
{
    public class MyHub : Hub
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        
        string[] info;



        public async Task UpdateLeaderboards()
        {
            string leaderboardsData = await RetrieveLeaderboardsDataAsync();
            Clients.All.updateLeaderboards(leaderboardsData);
        }


        public async Task UpdateComments(string[] stringArr)
        {
            info = stringArr;
            string[] commentsData = await RetrieveCommentsData();
            Clients.All.updateComments(commentsData);
        }

        

        private async Task<string> RetrieveLeaderboardsDataAsync()
        {
            string allLeaderboardsString = "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = $@"SELECT *, user.username, avatar.avatar_path FROM game_record 
                               INNER JOIN user ON game_record.user_id = user.user_id 
                               INNER JOIN avatar ON user.user_id = avatar.user_id
                               WHERE game_record.game_title = 'Block Breaker' ORDER BY game_record.game_score DESC LIMIT 5";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await connection.OpenAsync();

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        int rankingNum = 1;

                        while (await reader.ReadAsync())
                        {
                            string username = reader["username"].ToString();
                            string ranking_date = reader["ranking_date"].ToString();
                            string score = reader["game_score"].ToString();
                            string avatarPath = reader["avatar_path"].ToString();
                            string dateString = DateTime.Parse(ranking_date).ToString("M/d/yyyy");

                            allLeaderboardsString += $@"<div class=""row mt-3"" >
                                                    <div class=""col-md-1 d-grid"">
                                                        <p class=""fw-bolder text-white fs-3 my-3"">{rankingNum.ToString()}.</p>
                                                    </div>
                                                    <div class=""col-md-1 image-container"">
                                                        <img src=""{avatarPath}"" alt="""" class=""rounded-circle"" width=""60"" height=""60"">
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
                                                                <p class=""fw-bolder text-end"">{score} pts</p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div>
                                                    <hr / class=""text-white"">
                                                </div>";
                            rankingNum++;
                            
                        }
                    }
                }
            }

            return allLeaderboardsString;
        }



        private async Task<string[]> RetrieveCommentsData()
        {
            List<Comment> commentList = new List<Comment>();

            string mechanicTitle = info[0];
            string sessionUsername = info[1];
            string order = info[2];

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = $@"
            SELECT c1.*, user.username, avatar.avatar_path 
            FROM comment c1
            INNER JOIN user ON c1.user_id = user.user_id 
            INNER JOIN avatar ON user.user_id = avatar.user_id
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
                            string rawDate = reader["comment_date"].ToString();
                            string raw_comment = reader["comment"].ToString();
                            int? parentCommentId = reader.IsDBNull(reader.GetOrdinal("parent_comment_id")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("parent_comment_id"));
                            int userId = reader.GetInt32("user_id");

                            
                            DateTime commentDate = DateTime.Parse(rawDate);

                            string comment = MakeNameBold(raw_comment);
                            string commentAvatarPath = reader["avatar_path"].ToString();

                            if (parentCommentId != null)
                            {
                                Comment parentComment = commentList.FirstOrDefault(c => c.CommentId == parentCommentId);
                                if (parentComment != null)
                                {
                                    parentComment.RepliesList.Add(new Comment(commentId, username, commentDate, comment, commentAvatarPath));
                                }
                                else
                                {
                                    //Disregard, let the replyClick have the rootparent as parentComment
                                }
                            }
                            else
                            {
                                commentList.Add(new Comment(commentId, username, commentDate, comment, commentAvatarPath));
                            }


                        }
                    }
                }
            }


            return SortComment(commentList, sessionUsername, order);
        }

        private string[] SortComment(List<Comment> commentList, string sessionUser, string order)
        {
            // Sort the list by oldest DateCommented first
            commentList = commentList.OrderBy(c => c.DateCommented).ToList();

            if (order == "Newest")
            {
                commentList.Reverse(); // Sort the list by newest first
            }


            StringBuilder allCommentString = new StringBuilder();
            allCommentString.Clear();



            int commentCount = 0;
            foreach (Comment comment in commentList)
            {
                string replyButton = "";
                string viewRepliesButton = "";
                string replyContainer = "";
                string replyContainerDiv = "";
                commentCount++;

                if (comment.RepliesList.Count != 0)
                {
                    replyContainerDiv = $@"
                        <div id=""reply-container-{comment.CommentId}"" class=""reply-container"" aria-labelledby=""toggle-replies-btn-{comment.CommentId}"" aria-hidden=""true"">
                            <div class=""reply mt-4 text-justify float-left"" >";

                    foreach (Comment replyComment in comment.RepliesList)
                    {
                        commentCount++;
                        string replydateCommented = GetTimeAgo(replyComment.DateCommented);


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
                                        <img src= ""{replyComment.AvatarPath}"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                                    </div>
                                    <div class =""col-11"">
                                        <div class=""row"">
                                            <div class=""col"">
                                                <span class=""fw-bold"">{replyComment.Username}</span>
                                                <span class="""">{replydateCommented}</span>
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

                string dateCommented = GetTimeAgo(comment.DateCommented);

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
                                <img src= ""{comment.AvatarPath}"" alt="""" class=""rounded-circle"" width=""40"" height=""40"">
                            </div>
                            <div class =""col-11"">
                                <div class=""row"">
                                    <div class=""col"">
                                        <span class=""fw-bold"">{comment.Username}</span>
                                        <span class="""">{dateCommented}</span>
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

            string commentCountString = commentCount.ToString() + " Comments";

            string sortByFormat = $@"<div class=""dropdown"">
                                            <button class=""sortby_button"" type=""button"" id=""sortDropdown"" data-bs-toggle=""dropdown"" aria-expanded=""false"">
                                                <i class=""fa fa-sort"" aria-hidden=""true""></i> Sort by: {order}
                                            </button>
                                            <ul class=""dropdown-menu"" aria-labelledby=""sortDropdown"">
                                                <li><a class=""dropdown-item"" onclick=""handleItemClick('Newest')"">Newest first</a></li>
                                                <li><a class=""dropdown-item"" onclick=""handleItemClick('Oldest')"">Oldest first</a></li>
                                            </ul>
                                        </div>";

            return new string[] { allCommentString.ToString(), commentCountString, sortByFormat };
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
