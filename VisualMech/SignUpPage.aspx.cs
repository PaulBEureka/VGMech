using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Classes;
using BCryptNet = BCrypt.Net.BCrypt; // If this has error, run this in package manager console: Install-Package BCrypt.Net

namespace VisualMech
{
	public partial class SignupPage : System.Web.UI.Page
	{

        public static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
		{

		}




        protected void Register_btn_Click(object sender, EventArgs e)
        {

            if (captchacode.Text == Session["sessionCaptcha"].ToString())
            {
                string result = "";

                if (!CheckUsername())
                {
                    taken_lbl.Visible = true;
                }
                else
                {
                    taken_lbl.Visible = false;
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            string query = "INSERT INTO UserTable (username, password) VALUES (@Username, @Password)";

                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                PasswordHasher passwordHasher = new PasswordHasher();

                                string hashPassword = passwordHasher.HashPassword(New_Password_tb.Text);

                                command.Parameters.AddWithValue("@Username", New_Username_tb.Text);
                                command.Parameters.AddWithValue("@Password", hashPassword);
                                command.ExecuteNonQuery();
                            }

                            

                            result = "New User Registered Successfully";

                            LoginUser();

                            Response.Write(result);

                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            result = ex.Message;
                            Response.Write(result);
                        }
                    }
                }
            }
            else
            {
                lblCaptchaErrorMsg.Text = "Captcha code is incorrect.Please enter correct captcha code.";
                lblCaptchaErrorMsg.ForeColor = System.Drawing.Color.White;
                captchacode.Text = "";
            }




        }

        private bool CheckUsername()
        {
            string query = $@"
                SELECT *
                FROM UserTable 
                WHERE UserTable.username = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", New_Username_tb.Text);

                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return false; // Username exists
                            }
                            else
                            {
                                return true; // Username does not exist
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    return false;
                }
            }
        }




        private void LoginUser()
        {
            string query = $@"
                SELECT *
                FROM UserTable 
                WHERE UserTable.username = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", New_Username_tb.Text);

                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();

                                int user_id = reader.GetInt32(reader.GetOrdinal("user_id"));
                                string username = reader.GetString(reader.GetOrdinal("username"));

                                Session["CurrentUser"] = username;
                                Session["Current_ID"] = user_id;

                                RecordDefaultAvatar();

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }


        private void RecordDefaultAvatar()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string defaultAvatarPath = "~/Images/person_icon.png";

                    string avatar_query = "INSERT INTO Avatar (user_id, avatar_path) VALUES (@UserId, @AvatarPath)";

                    using (MySqlCommand command = new MySqlCommand(avatar_query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", Session["Current_ID"]);
                        command.Parameters.AddWithValue("@AvatarPath", defaultAvatarPath);
                        command.ExecuteNonQuery();
                    }

                    connection.Close();

                    Response.Redirect("HomePage.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write("An error occured " + ex);

                }
            }
        }

    }
}