using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Classes;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace VisualMech
{
    public partial class SignInPage : System.Web.UI.Page
    {

        public static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SignInPanel.Visible = true;
                ForgotPasswordPanel.Visible = false;
                ChangePasswordPanel.Visible = false;
                OTPCheckPanel.Visible = false;

            }
        }

        protected void Login_btn_Click(object sender, EventArgs e)
        {

            if (captchacode.Text == Session["sessionCaptcha"].ToString())
            {
                string query = $@"
                SELECT *
                FROM user 
                WHERE user.username = @Username and role = 'user'";


                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Username", Username_tb.Text);

                            connection.Open();
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    No_Username_lbl.Visible = false;
                                    Incorrect_lbl.Visible = false;
                                    reader.Read();

                                    int user_id = reader.GetInt32(reader.GetOrdinal("user_id"));
                                    string username = reader.GetString(reader.GetOrdinal("username"));
                                    string password = reader.GetString(reader.GetOrdinal("password"));
                                    string email = reader.GetString(reader.GetOrdinal("email"));

                                    PasswordHasher passwordHasher = new PasswordHasher();

                                    // Verify the password
                                    bool isMatch = passwordHasher.VerifyPassword(Password_tb.Text, password);

                                    if (!isMatch)
                                    {
                                        Incorrect_lbl.Visible = true;
                                    }
                                    else
                                    {

                                        Session["CurrentUser"] = username;
                                        Session["Current_ID"] = user_id;
                                        Session["CurrentEmail"] = email;

                                        Session["CurrentAvatarPath"] = UserDataGather.GetUserAvatarPath(user_id.ToString());
                                        
                                        Session["Message"] = $@"Welcome to VGMech, {username}";
                                        
                                        Response.Redirect("HomePage.aspx");
                                        
                                        

                                    }
                                }
                                else
                                {
                                    No_Username_lbl.Visible = true;
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
            else
            {
                lblCaptchaErrorMsg.Text = "Incorrect Captcha";
                lblCaptchaErrorMsg.ForeColor = System.Drawing.Color.White;
                captchacode.Text = "";
            }
        }

        protected void ResendOTPBtn_Click(object sender, EventArgs e)
        {

            if (InputValidator.CheckEmail(EmailTb.Text))
            {
                CustomValidator.Text = "No Email found";
                CustomValidator.Visible = true;
                return;
            }

            CustomValidator.Visible = false;
            OTPCheckPanel.Visible = true;

            EmailHidden.Value = EmailTb.Text;
            int currentValue = Convert.ToInt32(hfCountdownValue.Value);

            if (currentValue > 1)
            {
                hfCountdownValue.Value = (currentValue - 1).ToString();
                ResendOTPBtn.Text = $@"Send OTP (Limit - {hfCountdownValue.Value} / 5)";

                EmailSender.SendOTPEmail(EmailTb.Text, "VGMech Forgot Password", "To change your account password, enter the OTP");
                ResendOTPBtn.Enabled = true;
            }
            else
            {
                hfCountdownValue.Value = (currentValue - 1).ToString();
                ResendOTPBtn.Text = $@"Send OTP (Limit - {hfCountdownValue.Value} / 5)";
                ResendOTPBtn.Enabled = false;
            }
        }

        protected void ForgotPassBtn_Click(object sender, EventArgs e)
        {
            SignInPanel.Visible = false;
            ForgotPasswordPanel.Visible = true;
        }

        protected void ConfirmOTPBtn_Click(object sender, EventArgs e)
        {
            
            if (OTPtb.Text == EmailSender.OTP)
            {
                EmailSender.OTP = null;
                ForgotPasswordPanel.Visible = false;
                ChangePasswordPanel.Visible = true;
            }
            else
            {
                OTPtb.Text = null;
                CustomValidator.Text = "Invalid OTP";
                CustomValidator.Visible = true;
            }
            


            
        }

        [WebMethod]
        public static string ChangePassByEmail(string email, string newPassword)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string updateQuery = "UPDATE user SET password = @NewPassword WHERE email = @Email";

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        PasswordHasher passwordHasher = new PasswordHasher();
                        string hashPassword = passwordHasher.HashPassword(newPassword);

                        command.Parameters.AddWithValue("@NewPassword", hashPassword);
                        command.Parameters.AddWithValue("@Email", email);
                        command.ExecuteNonQuery();
                    }

                    string selectQuery = $@"
                    SELECT *
                    FROM user 
                    WHERE user.email = @Email and role = 'user'";


                    
                    using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();

                                int user_id = reader.GetInt32(reader.GetOrdinal("user_id"));
                                string username = reader.GetString(reader.GetOrdinal("username"));
                                string currEmail = reader.GetString(reader.GetOrdinal("email"));

                                HttpContext context = HttpContext.Current;


                                context.Session["CurrentUser"] = username;
                                context.Session["Current_ID"] = user_id;
                                context.Session["CurrentEmail"] = email;

                                context.Session["CurrentAvatarPath"] = UserDataGather.GetUserAvatarPath(user_id.ToString());

                                context.Session["Message"] = $@"Your password has been updated, {username}.<br/> Welcome back!";
                                
                            }
                            else
                            {
                                Exception noUser = new Exception("Error: User not found");
                                return noUser.ToString();
                            }
                        }
                    }
                        

                    connection.Close();

                    return "Successfully changed user password";
                }
                catch (Exception ex)
                {
                    return ex.ToString();

                }
            }

        }

        protected void ChangePassBtn_Click(object sender, EventArgs e)
        {
            string changePassString = @"
                document.addEventListener('DOMContentLoaded', function() {
                    var modalElement = document.getElementById('confirmModal');
                    if (modalElement) {
                        var modal = new bootstrap.Modal(modalElement, {
                            keyboard: false,
                        });
                        modal.show();
                    } else {
                        console.error('Modal element not found.');
                    }
                });
            ";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript4", changePassString, true);


        }
    }
}