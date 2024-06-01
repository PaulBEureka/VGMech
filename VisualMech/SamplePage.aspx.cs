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

namespace VisualMech
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        private static string cardTitle = "";
        private static List<LearnCard> cardList;

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
                recordVisitedPage();
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

            context.Session["CommentOffset"] = offset;
        }

        [WebMethod]
        public static void SetReplyOffset(string commentID, string offset)
        {
            HttpContext context = HttpContext.Current;

            context.Session["ReplyOffset" + commentID] = offset;
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
            string result = "";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Get the current HttpContext
                    HttpContext context = HttpContext.Current;

                    if (message.Length <= 0)
                    {
                        throw new Exception("Comment cannot be empty");
                    }
                    else
                    {
                        
                        if (context.Session["Current_ID"] != null)
                        {
                            ClearOffsets();
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
                            }

                            result = "Comment Posted Successfully";
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
                    result = ex.Message;
                }
            }


            return result;
        }

        
        [WebMethod]
        public static string post_Click(string message)
        {
            if (message == null || message.Length < 1 || String.IsNullOrWhiteSpace(message))
            {
                return null;
            }
            string result = "";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Get the current HttpContext
                    HttpContext context = HttpContext.Current;


                    if (message.Length <= 0)
                    {
                        throw new Exception("Comment cannot be empty");
                    }
                    else
                    {
                        if (context.Session["Current_ID"] != null)
                        {

                            ClearOffsets();
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
                            }

                            result = "Comment Posted Successfully";
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
                    result = ex.Message;
                }
            }


            return result;
        }


        [WebMethod]
        public static string DeleteComment(string commentID)
        {
            ClearOffsets();

            string result = "";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "DELETE FROM comment WHERE comment_id = @CommentId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CommentId", commentID);
                        command.ExecuteNonQuery();
                    }

                    result = "Comment Deleted Successfully";
                    connection.Close();
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }

            return result;
        }


        public void recordVisitedPage()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT COUNT(*) FROM visited_pages WHERE user_id = @UserId AND mechanic_title = @MechanicTitle";
                    int count;

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@UserId", Session["Current_ID"]);
                        selectCommand.Parameters.AddWithValue("@MechanicTitle", cardTitle);
                        count = Convert.ToInt32(selectCommand.ExecuteScalar());
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

                        Session["Message"] = $@"Have fun learning all about {cardTitle},<br/>Collaborate with other learners in the comment section!";

                        
                    }
                    else
                    {
                        // Similar record found, skip insertion of new record
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message); 
                }
            }
        
        }


    }
}