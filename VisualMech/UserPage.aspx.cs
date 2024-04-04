﻿using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Content.Classes;

namespace VisualMech
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        private string sessionEmail;
        private string sessionAboutMe;
        private List<string> visitedPagesList = new List<string>();
        private List<Card> tempCardList;
        private int totalLearnPages;
        private int totalVisitedPages;

        protected void Page_Load(object sender, EventArgs e)
        {
            tempCardList = Session["CardList"] as List<Card>;

            GetUserInfos();
            EditBtn.Click += EditBtn_Click;
        }

        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            if (customFile.HasFile)
            {
                try
                {
                    int maxFileSizeKB = 2000; 
                    int fileSizeKB = customFile.PostedFile.ContentLength / 1024; 

                    if (fileSizeKB > maxFileSizeKB)
                    {
                        lblMessage.Text = "File size exceeds the maximum limit (2MB).";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    string fileName = Path.GetFileName(customFile.FileName);
                    string filePath = Server.MapPath("~/Avatars/") + fileName;
                    customFile.SaveAs(filePath);

                    string avatarPath = "Avatars/" + fileName;
                    UpdateAvatarPathInDatabase(avatarPath);
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error uploading file: " + ex.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblMessage.Text = "Please select a file to upload.";
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void UpdateAvatarPathInDatabase(string avatarPath)
        {
            DeletePreviousAvatarFile();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string updateQuery = "UPDATE Avatar SET avatar_path = @AvatarPath WHERE user_id = @UserId";

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {

                        command.Parameters.AddWithValue("@AvatarPath", avatarPath);
                        command.Parameters.AddWithValue("@UserId", Session["Current_ID"]);
                        command.ExecuteNonQuery();
                    }

                    connection.Close();

                    Response.Redirect(Request.RawUrl);


                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error uploading file: " + ex.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;

                }
            }
        }

        private string GetUserAvatarPath()
        {
            string query = @"SELECT avatar_path FROM avatar WHERE user_id = @UserId;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", Session["Current_ID"]);

                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) 
                            {
                                int pathIndex = reader.GetOrdinal("avatar_path");
                                if (!reader.IsDBNull(pathIndex)) 
                                {
                                    string path = reader.GetString(pathIndex);
                                    string imageHtml = $@"<img src=""{path}"" alt=""User"" class=""rounded-circle"" width=""150"">";

                                    return imageHtml;
                                }
                                else
                                {
                                    return ""; 
                                }
                            }
                            else
                            {
                                return ""; 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return ""; // Return empty string on error
                }
            }
        }

        private void DeletePreviousAvatarFile()
        {
            string query = @"SELECT avatar_path FROM avatar WHERE user_id = @UserId;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", Session["Current_ID"]);

                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) 
                            {
                                int pathIndex = reader.GetOrdinal("avatar_path");
                                if (!reader.IsDBNull(pathIndex)) 
                                {
                                    string oldPath = reader.GetString(pathIndex);

                                    if (!string.IsNullOrEmpty(oldPath) && File.Exists(Server.MapPath(oldPath)))
                                    {
                                        try
                                        {
                                            File.Delete(Server.MapPath(oldPath));
                                        }
                                        catch (Exception ex)
                                        {
                                            lblMessage.Text = "Error deleting avatar file: " + ex.Message;
                                            lblMessage.Visible = true;
                                            lblMessage.ForeColor = System.Drawing.Color.Red;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void GetUserInfos()
        {
            string query = @"SELECT * FROM usertable WHERE user_id = @UserId;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", Session["Current_ID"]);

                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Session["CurrentUser"] = reader.IsDBNull(reader.GetOrdinal("username")) ? "" : reader.GetString(reader.GetOrdinal("username"));
                                sessionEmail = reader.IsDBNull(reader.GetOrdinal("email")) ? "": reader.GetString(reader.GetOrdinal("email"));
                                sessionAboutMe = reader.IsDBNull(reader.GetOrdinal("about_me")) ? "" : reader.GetString(reader.GetOrdinal("about_me"));



                                if (reader != null)
                                {
                                    UserInfosLit.Text = $@"<div class=""row"">
                                        <div class=""col-sm-3"">
                                          <h6 class=""mb-0"">Username</h6>
                                        </div>
                                        <div class=""col-sm-9 text-secondary"">
                                          {Session["CurrentUser"]}
                                        </div>
                                      </div>
                                      <hr>
                                      <div class=""row"">
                                        <div class=""col-sm-3"">
                                          <h6 class=""mb-0"">Email</h6>
                                        </div>
                                        <div class=""col-sm-9 text-secondary"">
                                          {sessionEmail}
                                        </div>
                                      </div>
                                      <hr>
                                      <div class=""row"">
                                        <div class=""col-sm-3"">
                                          <h6 class=""mb-0"">About Me</h6>
                                        </div>
                                        <div class=""col-sm-9 text-secondary"">
                                          {sessionAboutMe}
                                        </div>
                                      </div>
                                      <hr>";

                                    UserAvatarLit.Text = GetUserAvatarPath();
                                    UsernameLit.Text = $@"<h4>{Session["CurrentUser"]}</h4>";
                                    VisitedPagesLit.Text = GetVisitedPages();
                                    RecomenddedPagesLit.Text = GetRecommendedPages();
                                    UserProgressLit.Text = GetUserProgress();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private string GetUserProgress()
        {
            totalLearnPages = tempCardList.Count;

            float temp = (float)totalVisitedPages / totalLearnPages;
            double progressPercent = Math.Round((temp * 100), MidpointRounding.AwayFromZero);
            
            return $@"<div class=""progress"">
                            <div class=""progress-bar progress-bar-striped progress-bar-animated bg-danger"" role=""progressbar"" aria-valuenow=""{progressPercent}"" aria-valuemin=""0"" aria-valuemax=""100"" style=""width: {progressPercent}%""></div>
                        </div>
                        <div class=""text-center"">
                            {progressPercent}% of all Learn Mechanics Completed
                        </div>";
        }

        private string GetVisitedPages()
        {
            string content = "";

            string query = "SELECT mechanic_title, visited_timestamp FROM visited_pages WHERE user_id = @UserId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", Session["Current_ID"]);

                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string mechanicTitle = reader.GetString("mechanic_title");
                                string recordDate = reader["visited_timestamp"].ToString();
                                DateTime parsedDateTime = DateTime.Parse(recordDate);
                                string visitedDateTime = parsedDateTime.ToString("M/d/yyyy h:mm tt");

                                visitedPagesList.Add(mechanicTitle);


                                foreach (Card card in tempCardList)
                                {
                                    if (card.Title.ToUpper() == mechanicTitle.ToUpper())
                                    {
                                        Debug.WriteLine(mechanicTitle.ToUpper());
                                        content += $@"<li class=""list-group-item"">Date visited: {visitedDateTime} <br /><a class=""learn-link"" href=""SamplePage.aspx"" data-card-id =""{card.CardID}"">{mechanicTitle}</a></li>";
                                    }
                                }
                                totalVisitedPages++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ex.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return ""; // Return empty string on error
                }
            }

            return content;
        }

        private string GetRecommendedPages()
        {
            string content = "";

            List<string> titleList = (Session["CardList"] as List<Card>).Select(card => card.Title).ToList();

            titleList = titleList.Except(visitedPagesList).ToList();

            foreach(string title in titleList)
            {
                foreach (Card card in tempCardList)
                {
                    if (card.Title.ToUpper() == title.ToUpper())
                    {
                        content += $@"<li class=""list-group-item""><a href=""SamplePage.aspx"" class=""learn-link"" data-card-id =""{card.CardID}"">{title.ToUpper()}</a></li>";
                    }
                }
            }

            return content;

        }

        [WebMethod]
        public static void ProcessLink(int linkId)
        {
            HttpContext context = HttpContext.Current;

            if (context != null)
            {
                context.Session["LearnId"] = linkId;
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            InputUsername.Text = Session["CurrentUser"].ToString();
            InputEmail.Text = sessionEmail;
            InputAboutMe.Text = sessionAboutMe;

            UserInfosLit.Visible = false;
            UserInfosEditPanel.Visible = true;
            EditBtn.Visible = false;
            ChangePassBtn.Visible = false;
            CancelEditBtn.Visible = true;
            ConfirmBtn.Visible = true;
        }

        protected void EditBtn_ClickBack(object sender, EventArgs e)
        {
            UserInfosLit.Visible = true;
            UserInfosEditPanel.Visible = false;

            EditBtn.Visible = true;
            ChangePassBtn.Visible = true;
            CancelEditBtn.Visible = false;
            ConfirmBtn.Visible = false;
        }

        protected void ChangePassBtn_Click(object sender, EventArgs e)
        {

        }

        protected void ConfirmBtn_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string updateQuery = "UPDATE usertable SET username = @Username, email = @Email, about_me = @AboutMe WHERE user_id = @UserId";

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {

                        command.Parameters.AddWithValue("@Username", InputUsername.Text);
                        command.Parameters.AddWithValue("@Email", InputEmail.Text);
                        command.Parameters.AddWithValue("@AboutMe", InputAboutMe.Text);
                        command.Parameters.AddWithValue("@UserId", Session["Current_ID"]);
                        command.ExecuteNonQuery();
                    }

                    connection.Close();

                    //GetUserInfos();


                    lblMessage.Text = "User information successfully updated";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;

                    Response.Redirect(Request.RawUrl);



                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error uploading file: " + ex.Message;
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;

                }
            }
        }

        


    }
}