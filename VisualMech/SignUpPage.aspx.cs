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


        protected void Register_btn_Click(object sender, EventArgs e)
        {
            string captchaCode = captchacode.Text;
            string sessionCaptcha = Session["sessionCaptcha"].ToString();
            bool tempClear = true;

            taken_lbl.Visible = false;
            email_lbl.Visible = false;

            if (captchaCode != sessionCaptcha)
            {
                lblCaptchaErrorMsg.Text = "Captcha code is incorrect. Please enter correct captcha code.";
                lblCaptchaErrorMsg.ForeColor = System.Drawing.Color.White;
                captchacode.Text = "";
                tempClear = false;
            }

            if (!InputValidator.CheckUsername(New_Username_tb.Text))
            {
                taken_lbl.Visible = true;
                tempClear = false;
            }


            if (!InputValidator.CheckEmail(Email_tb.Text))
            {
                email_lbl.Visible = true;
                tempClear = false;
            }

            if (tempClear)
            {
                Session["tempUsername"] = New_Username_tb.Text;
                Session["tempPassword"] = New_Password_tb.Text;
                Session["tempEmail"] = Email_tb.Text;

                EmailSender.SendOTPEmail(Session["tempEmail"].ToString(), "VGMech Account Activation", "Enter OTP to activate your account");


                SignUpPanel.Visible = false;
                VerifyPanel.Visible = true;
            }
        }

        private void RegisterUser()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO user (username, password, email) VALUES (@Username, @Password, @Email)";
                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        PasswordHasher passwordHasher = new PasswordHasher();
                        string hashPassword = passwordHasher.HashPassword(Session["tempPassword"].ToString());

                        command.Parameters.AddWithValue("@Username", Session["tempUsername"].ToString());
                        command.Parameters.AddWithValue("@Password", hashPassword);
                        command.Parameters.AddWithValue("@Email", Session["tempEmail"].ToString());
                        command.ExecuteNonQuery();
                    }

                    connection.Close();

                    Session["tempUsername"] = null;
                    Session["tempPassword"] = null;
                    Session["tempEmail"] = null;

                    LoginUser();

                    Response.Redirect("HomePage.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void LoginUser()
        {
            string selectQuery = "SELECT user_id, username, email FROM user WHERE username = @Username";
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
                            string email = reader.GetString("email");

                            Session["CurrentUser"] = username;
                            Session["Current_ID"] = user_id;
                            Session["CurrentEmail"] = email;
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

        
        protected void OTPbtn_Click(object sender, EventArgs e)
        {
            if (EmailSender.OTP == OTPtb.Text)
            {
                Session["CurrentActivation"] = "1";
                EmailSender.OTP = null;
                RegisterUser();
            }
            else
            {
                lblTimer.Text = "Invalid OTP, Note: Changing of email could be done via Change Credentials button";
            }
        }

        protected void ResendBtn_Click(object sender, EventArgs e)
        {
            int currentValue = Convert.ToInt32(hfCountdownValue.Value);

            if (currentValue > 1)
            {
                hfCountdownValue.Value = (currentValue - 1).ToString();
                ResendBtn.Text = $@"Resend OTP (Limit - {hfCountdownValue.Value} / 5)";

                EmailSender.SendOTPEmail(Session["tempEmail"].ToString(), "VGMech Account Activation", "Enter OTP to activate your account");
                ResendBtn.Enabled = true;
            }
            else
            {
                hfCountdownValue.Value = (currentValue - 1).ToString();
                ResendBtn.Text = $@"Resend OTP (Limit - {hfCountdownValue.Value} / 5)";
                ResendBtn.Enabled = false;
            }
        }

        protected void ChangeBtn_Click(object sender, EventArgs e)
        {
            SignUpPanel.Visible = true;
            VerifyPanel.Visible = false;
        }
    }


}



