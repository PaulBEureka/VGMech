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

namespace VisualMech
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        private static string cardTitle = "";
        private static List<LearnCard> cardList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CommentOrder"] == null){
                Session["CommentOrder"] = "Newest";
            }

            cardList = Session["CardList"] as List<LearnCard>;

            if (!IsPostBack)
            {
                if (Request.QueryString["Learn"] != null)
                {
                    string learnID = Request.QueryString["Learn"];

                    LearnCard selectedCard = cardList.FirstOrDefault(card => card.CardID == learnID);
                    cardTitle = selectedCard.Title;
                    gameMechLit.Text = selectedCard.GetContentHtml();

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


        [WebMethod]
        public static string changeCommentOrder(string newOrder)
        {
            HttpContext context = HttpContext.Current;

            context.Session["CommentOrder"] = newOrder;
            return newOrder;
        }

        [WebMethod]
        public static string[] get_Comments()
        {
            HttpContext context = HttpContext.Current;

            string[] strings;

            if (context.Session["CurrentUser"] != null)
            {
                strings = new string[3] { cardTitle, context.Session["CurrentUser"].ToString(), context.Session["CommentOrder"].ToString() };
            }
            else
            {
                strings = new string[3] { cardTitle, null, context.Session["CommentOrder"].ToString() };
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
        public static string innerReply_Click(int parentCommentId, string message)
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
                        string insertQuery = "INSERT INTO visited_pages (user_id, mechanic_title) VALUES (@UserId, @MechanicTitle)";
                        using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@UserId", Session["Current_ID"]);
                            insertCommand.Parameters.AddWithValue("@MechanicTitle", cardTitle);
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