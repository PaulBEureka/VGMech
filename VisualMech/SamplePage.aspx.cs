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
        public static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        public static List<Comment> comments = new List<Comment>();
        public static string cardTitle = "";
        private static string order = "Newest";
        public static List<Card> cardList;

        public static string Order
        {
            get { return order; }
            set { order = value; }
        }
       

        protected void Page_Load(object sender, EventArgs e)
        {
            cardList = Session["CardList"] as List<Card>;
            int cardInt = (int)Session["LearnId"];

            int temp = 0;
            foreach (Card card in cardList)
            {
                if (temp == cardInt)
                {
                    cardTitle = card.Title;
                    gameMechLit.Text = card.GetLearnHtml();
                }
                temp++;
            }
        
            if(Session["Current_ID"] != null)
            {
                recordVisitedPage();
            }
        
        }


        [WebMethod]
        public static string changeCommentOrder(string newOrder)
        {
            Order = newOrder;
            return Order;
        }

        [WebMethod]
        public static string[] get_Comments()
        {
            HttpContext context = HttpContext.Current;

            string[] strings;

            if (context.Session["CurrentUser"] != null)
            {
                strings = new string[3] { cardTitle, context.Session["CurrentUser"].ToString(), Order };
            }
            else
            {
                strings = new string[3] { cardTitle, null, Order };
            }



            return strings;
        }

        [WebMethod]
        public static string reply_Click(int parentCommentId, string message)
        {
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

                            string query = "INSERT INTO comment (user_id, mechanic_title, comment, parent_comment_id) VALUES (@UserId, @MechanicTitle, @CommentText, @Parent_comment_id)";

                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@UserId", context.Session["Current_ID"]);
                                command.Parameters.AddWithValue("@MechanicTitle", cardTitle);
                                command.Parameters.AddWithValue("@CommentText", message);
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

                            string query = "INSERT INTO comment (user_id, mechanic_title, comment, parent_comment_id) VALUES (@UserId, @MechanicTitle, @CommentText, @Parent_comment_id)";

                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@UserId", context.Session["Current_ID"]);
                                command.Parameters.AddWithValue("@MechanicTitle", cardTitle);
                                command.Parameters.AddWithValue("@CommentText", message);
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

                            string query = "INSERT INTO comment (user_id, mechanic_title, comment) VALUES (@UserId, @MechanicTitle, @CommentText)";

                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@UserId", context.Session["Current_ID"]);
                                command.Parameters.AddWithValue("@MechanicTitle", cardTitle);
                                command.Parameters.AddWithValue("@CommentText", message);

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