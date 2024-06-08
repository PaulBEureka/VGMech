using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using VisualMech.Classes;
using VisualMech.Content.Classes;
using MySql.Data.MySqlClient;
using Microsoft.AspNet.SignalR;
using System.Web.Script.Serialization;
using System.ComponentModel.Design;
using Microsoft.Ajax.Utilities;
using System.Runtime.Remoting.Contexts;
using Microsoft.AspNet.SignalR.Messaging;

namespace VisualMech
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        private static string cardTitle = "";
        private static List<LearnCard> cardList;
        private static List<Badge> badgeList;

        protected void Page_Load(object sender, EventArgs e)
        {
           

            cardList = Session["CardList"] as List<LearnCard>;
            Session["CommentOrder"] = "Newest";
            Session["CommentOffset"] = 0;

            List<string> keysToRemove = new List<string>();

            foreach (string key in Session.Keys)
            {
                if (key.Contains("ReplyOffset"))
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (string key in keysToRemove)
            {
                Session.Remove(key);
            }

            if (!IsPostBack)
            {
                

                if (Request.QueryString["Learn"] != null)
                {
                    badgeList = Session["BadgeList"] as List<Badge>;

                    string learnID = Request.QueryString["Learn"];

                    LearnCard selectedCard = cardList.FirstOrDefault(card => card.CardID == learnID);
                    cardTitle = selectedCard.Title;
                    gameMechLit.Text = selectedCard.GetContentHtml();
                    gameMechInfoLit.Text = selectedCard.GetLearnInformation();

                    string script = $@"
                            var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle=""tooltip""]'))
                            var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {{
                                return new bootstrap.Tooltip(tooltipTriggerEl)
                            }})
                    ";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", script, true);
                }
            }

            
            if (Session["Current_ID"] != null)
            {
                RecordVisitedPage();
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
                            toastr['success']('{Session["Message"]}', 'Notification');
                    ";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript2", script, true);
                Session["Message"] = null;
            }
        
        }

        public static void ClearOffsets()
        {
            // Get the current HttpContext
            HttpContext context = HttpContext.Current;

            context.Session["CommentOffset"] = 0;

            List<string> keysToRemove = new List<string>();

            foreach (string key in context.Session.Keys)
            {
                if (key.Contains("ReplyOffset"))
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (string key in keysToRemove)
            {
                context.Session.Remove(key);
            }
        }

        [WebMethod]
        public static string changeCommentOrder(string newOrder)
        {
            HttpContext context = HttpContext.Current;

            context.Session["CommentOrder"] = newOrder;
            ClearOffsets();

            return newOrder;
        }

        [WebMethod]
        public static void SetOffset(string offset)
        {
            HttpContext context = HttpContext.Current;

            context.Session["CommentOffset"] = (int.Parse(offset) + 10).ToString();
        }

        [WebMethod]
        public static void SetReplyOffset(string commentID, string offset)
        {
            HttpContext context = HttpContext.Current;

            context.Session["ReplyOffset" + commentID] = (int.Parse(offset) + 10).ToString();
        }

        [WebMethod]
        public static string GetCardTitle()
        {
            return cardTitle;
        }

        [WebMethod]
        public static string[] get_Comments()
        {
            HttpContext context = HttpContext.Current;


            string[] strings;

            if (context.Session["CurrentUser"] != null)
            {
                strings = new string[] { cardTitle, context.Session["CurrentUser"].ToString(), context.Session["CommentOrder"].ToString() 
                    , context.Session["CommentOffset"].ToString() };
            }
            else
            {
                strings = new string[] { cardTitle, null, context.Session["CommentOrder"].ToString(),
                context.Session["CommentOffset"].ToString()};
            }

            return strings;
        }

        [WebMethod]
        public static string[] get_Replies(string commentID)
        {
            HttpContext context = HttpContext.Current;


            string[] strings;

            if (context.Session["CurrentUser"] != null)
            {
                if (context.Session["ReplyOffset" + commentID] == null)
                {
                    context.Session["ReplyOffset" + commentID] = 0;
                }

                strings = new string[] { cardTitle, context.Session["CurrentUser"].ToString()
                    , context.Session["ReplyOffset" + commentID].ToString(), commentID };
            }
            else
            {
                if (context.Session["ReplyOffset" + commentID] == null)
                {
                    context.Session["ReplyOffset" + commentID] = 0;
                }
                strings = new string[] { cardTitle, null,
                context.Session["ReplyOffset" + commentID].ToString(), commentID};

            }

            return strings;
        }

        [WebMethod]
        public static string reply_Click(int parentCommentId, string message)
        {
            if (message == null || message.Length < 1 || String.IsNullOrWhiteSpace(message))
            {
                return null;
            }
            string lastInsertedId = null;
            // Get the current HttpContext
            HttpContext context = HttpContext.Current;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    if (message.Length <= 0)
                    {
                        throw new Exception("Comment cannot be empty");
                    }
                    else
                    {
                        
                        if (context.Session["Current_ID"] != null)
                        {
                            connection.Open();
                            DateTime timestampUtc = DateTime.UtcNow;

                            string query = "INSERT INTO comment (user_id, mechanic_title, comment, comment_date, parent_comment_id) VALUES (@UserId, @MechanicTitle, @CommentText, @CommentDate, @Parent_comment_id)";

                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@UserId", context.Session["Current_ID"]);
                                command.Parameters.AddWithValue("@MechanicTitle", cardTitle);
                                command.Parameters.AddWithValue("@CommentText", message);
                                command.Parameters.AddWithValue("@CommentDate", timestampUtc);
                                command.Parameters.AddWithValue("Parent_comment_id", parentCommentId);

                                command.ExecuteNonQuery();
                                // Get the last inserted ID
                                lastInsertedId = command.LastInsertedId.ToString();
                            }

                            connection.Close();


                        }
                        else
                        {
                            throw new Exception("Please Sign In First");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return lastInsertedId;
        }

        
        [WebMethod]
        public static string post_Click(string message)
        {
            if (message == null || message.Length < 1 || String.IsNullOrWhiteSpace(message))
            {
                return null;
            }
            string lastInsertedId;
            // Get the current HttpContext
            HttpContext context = HttpContext.Current;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    
                    if (message.Length <= 0)
                    {
                        throw new Exception("Comment cannot be empty");
                    }
                    else
                    {
                        if (context.Session["Current_ID"] != null)
                        {

                            connection.Open();
                            DateTime timestampUtc = DateTime.UtcNow;

                            string query = "INSERT INTO comment (user_id, mechanic_title, comment, comment_date) VALUES (@UserId, @MechanicTitle, @CommentText, @CommentDate)";
                            
                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@UserId", context.Session["Current_ID"]);
                                command.Parameters.AddWithValue("@MechanicTitle", cardTitle);
                                command.Parameters.AddWithValue("@CommentText", message);
                                command.Parameters.AddWithValue("@CommentDate", timestampUtc);

                                command.ExecuteNonQuery();

                                // Get the last inserted ID
                                 lastInsertedId = command.LastInsertedId.ToString();
                            }

                            
                            connection.Close();

                        }
                        else
                        {
                            throw new Exception("Please Sign In First");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return lastInsertedId;
        }


        [WebMethod]
        public static string DeleteComment(string commentID)
        {

            string result = "";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "DELETE FROM comment WHERE comment_id = @CommentId OR parent_comment_id = @CommentId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CommentId", commentID);
                        command.ExecuteNonQuery();
                    }

                    result = commentID;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }

        [WebMethod]
        public static string CheckCommentBadge()
        {
            string badgeScript = null;
            // Get the current HttpContext
            HttpContext context = HttpContext.Current;

            Badge FirstWordBadge = badgeList.FirstOrDefault(badge => badge.BadgeID == "4");
            bool isNewRecord = FirstWordBadge.RecordBadgeToUser(context.Session["Current_ID"].ToString());
            if (isNewRecord)//Only show badge script if this is a new record
            {
                badgeScript = FirstWordBadge.GetToastString();
            }
            return badgeScript;
        }

        private void RecordVisitedPage()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT COUNT(*) as recordCount, (SELECT COUNT(*) FROM visited_pages WHERE user_id = @UserId) as totalVisited FROM visited_pages WHERE user_id = @UserId AND mechanic_title = @MechanicTitle";
                    int count = 0;
                    int totalVisits = 0;

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@UserId", Session["Current_ID"]);
                        selectCommand.Parameters.AddWithValue("@MechanicTitle", cardTitle);
                        using (MySqlDataReader reader = (MySqlDataReader)selectCommand.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                count = reader.GetInt32("recordCount");
                                totalVisits = reader.GetInt32("totalVisited");
                            }
                            
                        }
                    }

                    if (count == 0) // No similar record found
                    {
                        DateTime timestampUtc = DateTime.UtcNow;

                        string insertQuery = "INSERT INTO visited_pages (user_id, mechanic_title, visited_timestamp) VALUES (@UserId, @MechanicTitle, @VistedTimestamp)";
                        using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@UserId", Session["Current_ID"]);
                            insertCommand.Parameters.AddWithValue("@MechanicTitle", cardTitle);
                            insertCommand.Parameters.AddWithValue("@VistedTimestamp", timestampUtc);
                            insertCommand.ExecuteNonQuery();
                        }

                        //Set the toast message to greet user
                        Session["Message"] = $@"Have fun learning all about {cardTitle},<br/>Collaborate with other learners in the comment section!";

                    }

                    connection.Close();

                    //Check badge condition
                    if(totalVisits >= 1) // 1 visited learn mechanic page is the condition for earning the badge
                    {
                        Badge BeginBadge = badgeList.FirstOrDefault(badge => badge.BadgeID == "0");
                        bool isNewRecord = BeginBadge.RecordBadgeToUser(Session["Current_ID"].ToString());
                        if (isNewRecord)
                        {
                            BeginBadge.ShowBadgeToast(ClientScript, this.GetType());
                        }
                        
                    }
                    if(totalVisits == cardList.Count()) // Check if all learn mechanic page are visited
                    {
                        Badge MasterBadge = badgeList.FirstOrDefault(badge => badge.BadgeID == "1");
                        bool isNewRecord = MasterBadge.RecordBadgeToUser(Session["Current_ID"].ToString());
                        if (isNewRecord)
                        {
                            MasterBadge.ShowBadgeToast(ClientScript, this.GetType());
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        
        }

        

    }
}