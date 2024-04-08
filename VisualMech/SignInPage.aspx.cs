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

namespace VisualMech
{
    public partial class SignInPage : System.Web.UI.Page
    {

        public static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_btn_Click(object sender, EventArgs e)
        {

            if (captchacode.Text == Session["sessionCaptcha"].ToString())
            {


                string query = $@"
                SELECT *
                FROM user 
                WHERE user.username = @Username";


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
                lblCaptchaErrorMsg.Text = "Captcha code is incorrect.Please enter correct captcha code.";
                lblCaptchaErrorMsg.ForeColor = System.Drawing.Color.White;
                captchacode.Text = "";
            }
        }

        
    
    }
}