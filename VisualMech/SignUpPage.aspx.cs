using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Classes;
using BCryptNet = BCrypt.Net.BCrypt; // If this has error, run this in package manager console: Install-Package BCrypt.Net
using static System.Net.WebRequestMethods;
using System.Text;
using System.Threading.Tasks;

namespace VisualMech
{
    public partial class SignupPage : System.Web.UI.Page
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        protected async void Register_btn_Click(object sender, EventArgs e)
        {
            string captchaCode = captchacode.Text;
            string sessionCaptcha = Session["sessionCaptcha"].ToString();

            if (captchaCode != sessionCaptcha)
            {
                lblCaptchaErrorMsg.Text = "Captcha code is incorrect. Please enter correct captcha code.";
                lblCaptchaErrorMsg.ForeColor = System.Drawing.Color.White;
                captchacode.Text = "";
                return;
            }

            if (!CheckUsername())
            {
                taken_lbl.Visible = true;
                return;
            }

            taken_lbl.Visible = false;

            if (!CheckEmail())
            {
                email_lbl.Visible = true;
                return;
            }

            await RegisterUser();
        }


        private async Task RegisterUser()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string insertQuery = "INSERT INTO UserTable (username, password, email) VALUES (@Username, @Password, @Email)";
                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        PasswordHasher passwordHasher = new PasswordHasher();
                        string hashPassword = passwordHasher.HashPassword(New_Password_tb.Text);

                        command.Parameters.AddWithValue("@Username", New_Username_tb.Text);
                        command.Parameters.AddWithValue("@Password", hashPassword);
                        command.Parameters.AddWithValue("@Email", Email_tb.Text);
                        await command.ExecuteNonQueryAsync();
                    }

                    await connection.CloseAsync();

                    LoginUser();

                    await EmailSender.SendOTPEmailAsync(Session["CurrentEmail"].ToString());
                    Response.Redirect("VerificationPage.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }


        private void LoginUser()
        {
            string selectQuery = "SELECT user_id, username FROM UserTable WHERE username = @Username";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", New_Username_tb.Text);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int user_id = reader.GetInt32("user_id");
                            string username = reader.GetString("username");

                            Session["CurrentUser"] = username;
                            Session["Current_ID"] = user_id;
                            Session["CurrentEmail"] = Email_tb.Text;
                            RecordDefaultAvatar();
                        }
                    }
                }

                connection.Close();
            }

                
        }

        private void RecordDefaultAvatar()
        {
            string defaultAvatarPath = "Images/person_icon.png";
            string insertAvatarQuery = "INSERT INTO Avatar (user_id, avatar_path) VALUES (@UserId, @AvatarPath)";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(insertAvatarQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserId", Session["Current_ID"]);
                    command.Parameters.AddWithValue("@AvatarPath", defaultAvatarPath);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

                
        }

        

        private bool CheckUsername()
        {
            string query = "SELECT COUNT(*) FROM UserTable WHERE username = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", New_Username_tb.Text);
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count == 0; 
                }
            }
        }

        private bool CheckEmail()
        {
            string query = "SELECT COUNT(*) FROM UserTable WHERE email = @Email";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email_tb.Text);
                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count == 0;
                }
            }
        }

        

    }


}



