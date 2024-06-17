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
using System.Web.UI;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.ComponentModel;

namespace VisualMech
{
    public class MyHub : Hub
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        public override Task OnConnected()
        {
            // Get the page context from the query string
            var pageContext = Context.QueryString["page"];
            Groups.Add(Context.ConnectionId, pageContext);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // Get the page context from the query string
            var pageContext = Context.QueryString["page"];
            Groups.Remove(Context.ConnectionId, pageContext);
            return base.OnDisconnected(stopCalled);
        }

        public async Task UpdateLeaderboards(string[] miniGameInfo)
        {
            string[] leaderboardsData = await RetrieveLeaderboardsDataAsync(miniGameInfo);
            Clients.Group(miniGameInfo[0]).updateLeaderboards(leaderboardsData);
        }

        //Send Comment to group only (Notify them to fetch comments only on caller to prevent order problems)
        public void CallForGroupSend(string cardTitle, string insertedID)
        {
            Clients.Group(cardTitle).sendCommentGroup(insertedID);
        }

        public async Task BuildIncomingComment (string insertedID, string sessionUser)
        {
            string[] commentsData = await GetNewComment(insertedID, sessionUser);
            Clients.Caller.receiveComment(commentsData);
        }

        public void CallForGroupDelete(string cardTitle, string idToDelete)
        {
            Clients.Group(cardTitle).deleteCommentGroup(idToDelete);
        }

        

        //FetchComments when connecting to hub only on Caller
        public async Task FetchComments(string[] stringArr)
        {
            string[] commentsData = await RetrieveCommentsData(stringArr);
            Clients.Caller.fetchCommentSolo(commentsData);
        }

        public async Task LoadMoreComments(string[] stringArr)
        {
            string[] commentsData = await RetrieveCommentsData(stringArr);
            Clients.Caller.loadMoreSolo(commentsData);
        }

        public async Task LoadMoreReplies(string[] stringArr)
        {
            string[] commentsData = await RetrieveRepliesData(stringArr);
            Clients.Caller.loadMoreRepliesSolo(commentsData);
        }

        private async Task<string[]> GetNewComment(string insertedID, string sessionUser)
        {

            List<Comment> commentList = new List<Comment>();
            int? parentCommentID = 0;

            string query = $@"
                SELECT 
                    c1.*, 
                    user.username, 
                    user.about_me, 
                    avatar.avatar_path, 
                    COUNT(c2.comment_id) AS ReplyCount
                    FROM comment c1
                    INNER JOIN user ON c1.user_id = user.user_id 
                    INNER JOIN avatar ON user.user_id = avatar.user_id
                    LEFT JOIN comment c2 ON c1.comment_id = c2.parent_comment_id
                    WHERE c1.comment_id = {insertedID};
            ";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                DateTime commentDate = DateTime.MinValue;

                                int commentId = reader.GetInt32("comment_id");
                                string username = reader["username"].ToString();
                                DateTime localTimestamp = (DateTime)reader["comment_date"];
                                string raw_comment = reader["comment"].ToString();
                                int userId = reader.GetInt32("user_id");
                                string about_me = reader["about_me"].ToString();
                                int replyCountValue = reader.IsDBNull(reader.GetOrdinal("ReplyCount")) ? 0 : reader.GetInt32(reader.GetOrdinal("ReplyCount"));

                                parentCommentID = reader.IsDBNull(reader.GetOrdinal("parent_comment_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("parent_comment_id"));

                                commentDate = localTimestamp.ToLocalTime();

                                string comment = MakeNameBold(raw_comment);
                                string commentAvatarPath = reader["avatar_path"].ToString();

                                commentList.Add(new Comment(commentId, username, commentDate, comment, commentAvatarPath, about_me, replyCountValue));
                            }
                        }
                    }
                }


                if (parentCommentID == 0)
                {
                    return SortComment(commentList, sessionUser, null, 0, 0, true);
                }
                else
                {
                    return SortReplies(commentList, sessionUser, 0, parentCommentID.ToString(), true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task UpdateCommentsOrder(string[] stringArr)
        {
            string[] commentsData = await RetrieveCommentsData(stringArr);
            Clients.Caller.updateCommentsOrder(commentsData);
        }


        private async Task<string[]> RetrieveLeaderboardsDataAsync(string[] miniGameInfo)
        {
            string allLeaderboardsString = "";
            string congratsScript = null;
            string miniGameTitle = miniGameInfo[0];
            string sessionPlayerName = miniGameInfo[1];
            string currentRank = miniGameInfo[2];
            string currrentScore = miniGameInfo[3];

            string returnRank = null;
            string returnScore = null;


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = $@"
                    SELECT 
                        game_record.*, 
                        user.username, 
                        user.user_id,
                        avatar.avatar_path 
                    FROM 
                        game_record 
                    INNER JOIN 
                        user ON game_record.user_id = user.user_id 
                    INNER JOIN 
                        avatar ON user.user_id = avatar.user_id 
                    WHERE 
                        game_record.game_title = '{miniGameTitle}' 
                    ORDER BY 
                        game_record.game_score DESC, 
                        game_record.time_finished ASC 
                    LIMIT 5";


                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    await connection.OpenAsync();

                    using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                    {
                        int rankingNum = 1;

                        while (await reader.ReadAsync())
                        {
                            string username = reader["username"].ToString();
                            int userId = reader.GetInt32("user_id");
                            string time_finished = reader["time_finished"].ToString();
                            string score = reader["game_score"].ToString();
                            string avatarPath = reader["avatar_path"].ToString();

                            allLeaderboardsString += $@"
                                                <div class=""row my-3"">
                                                    <div class=""col-md-1 d-grid"">
                                                        <p class=""fw-bolder text-white fs-3 my-3"">{rankingNum.ToString()}.</p>
                                                    </div>
                                                    <div class=""col-md-1 image-container me-3 mb-2 custom-leaderboard-avatar rounded-circle"">
                                                        <img src=""{avatarPath}"" alt="""" class=""rounded-circle record_border"" width=""60"" height=""60"">
                                                    </div>
                                                    <div class=""col-md-9 leaderboard_white_rec_round gap-3"">
                                                        
                                                            <div class=""mt-3"">
                                                                <p class="" text-start""><strong>Player: </strong> {username}</p>
                                                            </div>
                                                            <div class=""mt-3"">
                                                                <p class="" text-end""><strong>Score:</strong> {score} pts</p>
                                                            </div>
                                                            <div class=""mt-3"">
                                                                <p class="" text-end""><strong>Time finished:</strong> {time_finished}</p>
                                                            </div>
                                                    </div>
                                                </div>

                                                <div>
                                                    <hr / class=""text-white"">
                                                </div>";
                            
                            
                            

                            if (username == sessionPlayerName) // Record in the database is the same with session user/player
                            {
                                int newScore = int.Parse(score);
                                int currentScoreInt = int.Parse(currrentScore);
                                int currentRankInt = int.Parse(currentRank);

                                
                                if (currrentScore != "0")
                                {
                                    if (newScore > currentScoreInt)
                                    {
                                        congratsScript = $@"
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
                                            toastr['success']('You made a new personal highscore in {miniGameTitle}!', 'Congratulations, {username}!');
                                    ";
                                    }
                                    else if (rankingNum < currentRankInt)
                                    {
                                        congratsScript = $@"
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
                                            toastr['success']('Your rank in {miniGameTitle} increased!', 'Congratulations, {username}, you rank #{rankingNum}!');
                                    ";
                                    }

                                }


                                returnScore = score;
                                returnRank = rankingNum.ToString();
                            }

                            

                            rankingNum++;
                        }
                    }
                }
            }

            
            return new string[] { allLeaderboardsString, congratsScript, returnRank, returnScore };
        }

        private async Task<string[]> RetrieveRepliesData(string[] info)
        {
            List<Comment> commentList = new List<Comment>();

            string mechanicTitle = info[0];
            string sessionUsername = info[1];
            int repliesOffset = int.Parse(info[2]);
            string parentCommentID = info[3];

            int offsetNextValue = repliesOffset;
            string queryOrder = "ASC";//Have replies to always be in oldest first order to retain context
            string query;

            query = $@"
                SELECT 
                    c1.*, 
                    user.username, 
                    user.about_me, 
                    avatar.avatar_path, 
                    COUNT(c2.comment_id) AS ReplyCount 
                FROM comment c1
                INNER JOIN user ON c1.user_id = user.user_id 
                INNER JOIN avatar ON user.user_id = avatar.user_id
                LEFT JOIN comment c2 ON c1.comment_id = c2.parent_comment_id
                WHERE c1.mechanic_title = @MechanicTitle
                AND c1.parent_comment_id = {parentCommentID}
                GROUP BY c1.comment_id, user.username, user.about_me, avatar.avatar_path
                ORDER BY c1.comment_date {queryOrder}
                LIMIT 11 OFFSET @CommentOffset;
            ";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MechanicTitle", mechanicTitle);
                        command.Parameters.AddWithValue("@CommentOffset", repliesOffset);

                        using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                DateTime commentDate = DateTime.MinValue;

                                int commentId = reader.GetInt32("comment_id");
                                string username = reader["username"].ToString();
                                DateTime localTimestamp = (DateTime)reader["comment_date"];
                                string raw_comment = reader["comment"].ToString();
                                int userId = reader.GetInt32("user_id");
                                string about_me = reader["about_me"].ToString();
                                int replyCountValue = reader.IsDBNull(reader.GetOrdinal("ReplyCount")) ? 0 : reader.GetInt32(reader.GetOrdinal("ReplyCount"));
                                
                                commentDate = localTimestamp.ToLocalTime();

                                string comment = MakeNameBold(raw_comment);
                                string commentAvatarPath = reader["avatar_path"].ToString();

                                commentList.Add(new Comment(commentId, username, commentDate, comment, commentAvatarPath, about_me, replyCountValue));
                            }
                        }
                    }
                }

                // Sort and return comments
                return SortReplies(commentList, sessionUsername, offsetNextValue, parentCommentID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string[] SortReplies(List<Comment> commentList, string sessionUser, int offset, string parentCommentID, bool isNewComment = false)
        {
            // Check if there are more than 10 reply comments, indication of there are more comments to be loaded
            bool hasMore = commentList.Count > 10;

            if (hasMore)
            {
                commentList.RemoveAt(commentList.Count - 1);
            }

            StringBuilder allCommentString = new StringBuilder();
            allCommentString.Clear();

            foreach (Comment comment in commentList)
            {
                string replyButton = "";
                string replyContainer = "";
                int replyCount = comment.ReplyCount;


                string dateCommented = GetTimeAgo(comment.DateCommented);

                replyContainer = sessionUser != null ? $@"
                    <div id=""respond-container-{comment.CommentId}"" class=""respond-container"" aria-labelledby=""toggle-replies-btn-{comment.CommentId}"" aria-hidden=""true"">
                        <div class=""row container mb-3 gap-3"">
                            <textarea placeholder=""Type your reply here {sessionUser}"" id=""replybox-{comment.CommentId}"" rows=""5"" class=""form-control"" style=""background-color: white; resize: none;"" draggable=""false""></textarea>
                            <div class=""row gap-3"">
                                <div class=""col-4 me-3"">
                                    <button type=""button"" class=""comment_button_2 bg-danger"" onclick=""toggleRespond({comment.CommentId})"">Cancel</button>
                                </div>
                                <div class=""col-4"">
                                    <button type=""button"" id=""button-addon-reply-{comment.CommentId}"" class=""comment_button_2 bg-danger"" onclick=""innerReply_Click({parentCommentID}, {comment.CommentId})"">Reply</button>
                                </div>
                            </div>
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
                <div class=""comment mt-4 float-left"" data-comment-id=""{comment.CommentId}"" data-parent-id=""{parentCommentID}"">");
                


                allCommentString.Append($@"
                        <div class=""row"">
                            <div class=""col-md-2 text-md-end"">
                                <img src= ""{comment.AvatarPath}"" alt="""" role=""button"" class=""rounded-circle comment-avatar mt-2"" width=""40"" height=""40"" data-bs-toggle=""popover"" title=""About {comment.Username}"" data-bs-content=""{comment.AboutMe}"">
                               
                            </div>
                            <div class =""col-md-10 text-start"">
                                <div class=""row"">
                                    <div class=""col"">
                                        <span class=""fw-bold comment-header-style"">{comment.Username}</span>
                                        <span class=""text-muted comment-header-style"">{dateCommented}</span>
                                    </div>

                ");

                if (comment.Username == sessionUser)
                {
                    allCommentString.Append($@"
                                    <div class=""col"">
                                      <div class=""dropdown text-end"">
                                         <i class=""fa-solid fa-ellipsis-vertical btn"" id=""dropdownMenuButton-{comment.CommentId}"" data-bs-toggle=""dropdown"" aria-expanded=""false""></i>
           
                                        <ul class=""dropdown-menu"" aria-labelledby=""dropdownMenuButton-{comment.CommentId}"">
                                          <li><a class=""dropdown-item deleteOption"" data-comment-id=""{comment.CommentId}"" href=""#"">Delete</a></li>
                                        </ul>
                                      </div>
                                    </div>");

                }



                allCommentString.Append($@"
                                </div>
                                <div class=""row"">
                                    <p class=""comment-content-style"">{comment.CommentContent}</p>
                                </div>
                                <div class=""row text-start"">
                                    <div class=""dropdown"">
                                        {replyButton}
                                        {replyContainer}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                ");

            }

            if (!isNewComment)
            {
                if (hasMore)
                {
                    allCommentString.Append($@"
                    <div id=""insertNextCommentDiv-{parentCommentID}"">
                        <div class=""row d-grid"">
                            <div class=""col-5 text-center m-auto"" id=""loadMoreReplies-{parentCommentID}"">
                                <button class=""toggle-replies-btn text-center"" type=""button"" onclick=""loadMoreReplies({parentCommentID})"">
                                    load more
                                </button>      
                            </div>
                        </div>
                    </div>
                ");
                }
                return new string[] { allCommentString.ToString(), parentCommentID, offset.ToString() };
            }
            else
            {
                return new string[] { allCommentString.ToString(), parentCommentID};
            }

            
        }


        private async Task<string[]> RetrieveCommentsData(string[] info)
        {
            List<Comment> commentList = new List<Comment>();

            string mechanicTitle = info[0];
            string sessionUsername = info[1];
            string order = info[2];
            int commentOffset = int.Parse(info[3]);
            int offsetNextValue = commentOffset;
            string queryOrder = (order == "Newest") ? "DESC" : "ASC";
            int totalComments = 0;

            string query;

            if (sessionUsername != null)
            {
                query = $@"
                    SELECT 
                    c1.*, 
                    user.username, 
                    user.about_me, 
                    avatar.avatar_path, 
                    COUNT(c2.comment_id) AS ReplyCount,
                    (SELECT COUNT(*) 
                     FROM comment 
                     WHERE mechanic_title = @MechanicTitle) AS TotalComments
                    FROM comment c1
                    INNER JOIN user ON c1.user_id = user.user_id 
                    INNER JOIN avatar ON user.user_id = avatar.user_id
                    LEFT JOIN comment c2 ON c1.comment_id = c2.parent_comment_id
                    WHERE c1.mechanic_title = @MechanicTitle
                    AND c1.parent_comment_id IS NULL
                    GROUP BY c1.comment_id, user.username, user.about_me, avatar.avatar_path
                    ORDER BY 
                        CASE 
                            WHEN user.username = '{sessionUsername}' THEN 0 
                            ELSE 1 
                        END, 
                        c1.comment_date {queryOrder}
                    LIMIT 11 OFFSET @CommentOffset;
                ";

            }
            else
            {
                query = $@"
                SELECT 
                    c1.*, 
                    user.username, 
                    user.about_me, 
                    avatar.avatar_path, 
                    COUNT(c2.comment_id) AS ReplyCount,
                    (SELECT COUNT(*) 
                     FROM comment 
                     WHERE mechanic_title = @MechanicTitle) AS TotalComments
                    FROM comment c1
                    INNER JOIN user ON c1.user_id = user.user_id 
                    INNER JOIN avatar ON user.user_id = avatar.user_id
                    LEFT JOIN comment c2 ON c1.comment_id = c2.parent_comment_id
                    WHERE c1.mechanic_title = @MechanicTitle
                    AND c1.parent_comment_id IS NULL
                    GROUP BY c1.comment_id, user.username, user.about_me, avatar.avatar_path
                    ORDER BY c1.comment_date {queryOrder}
                    LIMIT 11 OFFSET @CommentOffset;
                ";
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MechanicTitle", mechanicTitle);
                        command.Parameters.AddWithValue("@CommentOffset", commentOffset);

                        using (MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                DateTime commentDate = DateTime.MinValue;

                                int commentId = reader.GetInt32("comment_id");
                                string username = reader["username"].ToString();
                                DateTime localTimestamp = (DateTime)reader["comment_date"];
                                string raw_comment = reader["comment"].ToString();
                                int userId = reader.GetInt32("user_id");
                                string about_me = reader["about_me"].ToString();
                                int replyCountValue = reader.IsDBNull(reader.GetOrdinal("ReplyCount")) ? 0 : reader.GetInt32(reader.GetOrdinal("ReplyCount"));
                                totalComments = reader.IsDBNull(reader.GetOrdinal("TotalComments")) ? 0 : reader.GetInt32(reader.GetOrdinal("TotalComments"));



                                commentDate = localTimestamp.ToLocalTime();

                                string comment = MakeNameBold(raw_comment);
                                string commentAvatarPath = reader["avatar_path"].ToString();

                                commentList.Add(new Comment(commentId, username, commentDate, comment, commentAvatarPath, about_me, replyCountValue));
                            }
                        }
                    }
                }

                // Sort and return comments
                return SortComment(commentList, sessionUsername, order, offsetNextValue, totalComments);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string[] SortComment(List<Comment> commentList, string sessionUser, string order, int offset, int totalComments, bool isNewComment = false)
        {

            // Check if there are more than 10 comments, indication of there are more comments to be loaded
            bool hasMore = commentList.Count > 10;
            if (hasMore)
            {
                commentList.RemoveAt(commentList.Count - 1);
            }


            StringBuilder allCommentString = new StringBuilder();
            allCommentString.Clear();


            bool IsFirstTag = false;

            string newCommentUser = null;

            foreach (Comment comment in commentList)
            {
                string replyButton = "";
                string viewRepliesButton = "";
                string replyContainer = "";
                string replyContainerDiv = "";
                int replyCount = comment.ReplyCount;
                newCommentUser = comment.Username;

                replyContainerDiv = $@"
                    <div id=""reply-container-{comment.CommentId}"" class=""reply-container"" aria-labelledby=""toggle-replies-btn-{comment.CommentId}"" aria-hidden=""true"">
                        <div class=""reply mt-4 text-justify float-left"" id=""inner-reply-{comment.CommentId}"" >
                            <div class=""row d-grid"" id=""initial-spinner-{comment.CommentId}"">
                                <div class=""col-5 text-center m-auto"">
                                    <div class=""spinner-border text-danger text-center"" role=""status"">
                                        <span class=""visually-hidden"">Loading...</span>
                                    </div>   
                                </div>
                            </div>
                        </div>
                    </div>
                    ";

                if(replyCount > 1)
                {
                    viewRepliesButton = $@"
                    <button id=""toggle-replies-btn-{comment.CommentId}"" class=""toggle-replies-btn"" type=""button"" aria-expanded=""false"" aria-controls=""reply-container-{comment.CommentId}"" onclick=""toggleReplies({comment.CommentId})""><i class=""fa-solid fa-chevron-down"" id=""toggle-replies-btn-icon-{comment.CommentId}""></i>{replyCount} replies</button>
                    ";

                }
                else
                {
                    viewRepliesButton = $@"
                    <button id=""toggle-replies-btn-{comment.CommentId}"" class=""toggle-replies-btn"" type=""button"" aria-expanded=""false"" aria-controls=""reply-container-{comment.CommentId}"" onclick=""toggleReplies({comment.CommentId})""><i class=""fa-solid fa-chevron-down"" id=""toggle-replies-btn-icon-{comment.CommentId}""></i>{replyCount} reply</button>
                    ";
                }


                

                string dateCommented = GetTimeAgo(comment.DateCommented);

                replyContainer = sessionUser != null ? $@"
                    <div id=""respond-container-{comment.CommentId}"" class=""respond-container"" aria-labelledby=""toggle-replies-btn-{comment.CommentId}"" aria-hidden=""true"">
                        <div class=""row container mb-3 gap-3"">
                            <textarea placeholder=""Type your reply here {sessionUser}"" id=""replybox-{comment.CommentId}"" rows=""5"" class=""form-control"" style=""background-color: white; resize: none;"" draggable=""false""></textarea>
                            <div class=""row gap-2"">
                                <div class=""col-4"">
                                    <button type=""button"" class=""comment_button_2 my-2 bg-danger"" onclick=""toggleRespond({comment.CommentId})"">Cancel</button>
                                </div>
                                <div class=""col-4"">
                                    <button type=""button"" id=""button-addon-reply-{comment.CommentId}"" class=""comment_button_2 my-2 bg-danger"" onclick=""reply_Click({comment.CommentId})"">Reply</button>
                                </div>
                            </div>
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

                //Put a data-comment-type = "First" to the first comment that is not the user if the offset is 0
                if(!IsFirstTag && offset == 0 && comment.Username != sessionUser)
                {
                    allCommentString.Append($@"
                        <div class=""comment mt-4 float-left"" data-comment-type='First' data-comment-id=""{comment.CommentId}"">");
                    IsFirstTag = true;
                }
                else
                {
                    allCommentString.Append($@"
                    <div class=""comment mt-4 float-left"" data-comment-id=""{comment.CommentId}"">");
                }


                allCommentString.Append($@"
                        <div class=""row"">
                            <div class=""col-md-2 text-md-end"">
                                <img src= ""{comment.AvatarPath}"" alt="""" role=""button"" class=""rounded-circle comment-avatar mt-2"" width=""40"" height=""40"" data-bs-toggle=""popover"" title=""About {comment.Username}"" data-bs-content=""{comment.AboutMe}"">
                               
                            </div>
                            <div class =""col-md-10 text-start"">
                                <div class=""row"">
                                    <div class=""col"">
                                        <span class=""fw-bold comment-header-style"">{comment.Username}</span>
                                        <span class=""text-muted comment-header-style"">{dateCommented}</span>
                                    </div>

                ");

                if (comment.Username == sessionUser)
                {
                    allCommentString.Append($@"
                                    <div class=""col"">
                                      <div class=""dropdown text-end"">
                                         <i class=""fa-solid fa-ellipsis-vertical btn"" id=""dropdownMenuButton-{comment.CommentId}"" data-bs-toggle=""dropdown"" aria-expanded=""false""></i>
           
                                        <ul class=""dropdown-menu"" aria-labelledby=""dropdownMenuButton-{comment.CommentId}"">
                                          <li><a class=""dropdown-item deleteOption"" data-comment-id=""{comment.CommentId}"" href=""#"">Delete</a></li>
                                        </ul>
                                      </div>
                                </div>");
                            
                }

                

                if(comment.ReplyCount != 0)
                {
                    allCommentString.Append($@"
                                    </div>
                                    <div class=""row"">
                                        <p class=""comment-content-style"">{comment.CommentContent}</p>
                                    </div>
                                    <div class=""row text-start"">
                                        <div class=""dropdown"">
                                            {replyButton}
                                            {replyContainer}
                                        </div>
                                        <div id=""ReplyContainerDivHidden-{comment.CommentId}"" >
                                            {viewRepliesButton}
                                            {replyContainerDiv}
                                        </div>
                                    
                                    </div>
                                </div>
                            </div>
                        </div>
                    
                    ");
                }
                else
                {
                    allCommentString.Append($@"
                                    </div>
                                    <div class=""row"">
                                        <p class=""comment-content-style"">{comment.CommentContent}</p>
                                    </div>
                                    <div class=""row text-start"">
                                        <div class=""dropdown"">
                                            {replyButton}
                                            {replyContainer}
                                        </div>
                                        <div id=""ReplyContainerDivHidden-{comment.CommentId}"" style=""display: none;"">
                                            {viewRepliesButton}
                                            {replyContainerDiv}
                                        </div>
                                    
                                    </div>
                                </div>
                            </div>
                        </div>
                    
                    ");
                }
                
            }

            if (!isNewComment) 
            {
                string commentCountString;
                if (totalComments <= 1)
                {
                    commentCountString = totalComments.ToString() + " comment";
                }
                else
                {
                    commentCountString = totalComments.ToString() + " comments";
                }

                string sortByFormat = $@"<div class=""dropdown"">
                                            <button class=""sortby_button"" type=""button"" id=""sortDropdown"" data-bs-toggle=""dropdown"" aria-expanded=""false"">
                                                <i class=""fa fa-sort"" aria-hidden=""true""></i> Sort by: {order}
                                            </button>
                                            <ul class=""dropdown-menu"" aria-labelledby=""sortDropdown"">
                                                <li><a class=""dropdown-item"" onclick=""handleItemClick('Newest')"">Newest first</a></li>
                                                <li><a class=""dropdown-item"" onclick=""handleItemClick('Oldest')"">Oldest first</a></li>
                                            </ul>
                                        </div>";

                if (hasMore)
                {
                    allCommentString.Append($@"
                    <div id=""insertNextCommentDiv"">
                        <div class=""row d-grid"">
                            <div class=""col-5 text-center m-auto"" id=""loadMoreDiv"">
                                <button class=""toggle-replies-btn text-center"" type=""button"" onclick=""loadMoreComments()"">
                                    load more
                                </button>      
                            </div>
                        </div>
                    </div>
                ");
                }

                return new string[] { allCommentString.ToString(), commentCountString, sortByFormat, offset.ToString() };
            }
            else
            {

                return new string[] { allCommentString.ToString(), "Ignore", newCommentUser };
            }

            
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
