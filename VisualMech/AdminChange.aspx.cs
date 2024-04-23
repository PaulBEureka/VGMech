using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Classes;

namespace VisualMech
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void PassChangeBtn_Click(object sender, EventArgs e)
        {
            submitErrorLbl.Visible = false;
            submitSuccessLbl.Visible = false;

            if (CurrentPassTb.Text == "" || NewPasswordTb.Text == "")
            {
                submitErrorLbl.Text = "Invalid Input";
                submitErrorLbl.Visible = true;
                return;
            }

            string query = $@"
                SELECT *
                FROM user 
                WHERE user.username = @Username and role = 'admin'";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", Session["CurrentUser"]);

                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            
                            reader.Read();
                            string password = reader.GetString(reader.GetOrdinal("password"));

                            PasswordHasher passwordHasher = new PasswordHasher();

                            bool isMatch = passwordHasher.VerifyPassword(CurrentPassTb.Text, password);

                            if (!isMatch)
                            {
                                submitErrorLbl.Text = "Incorrect current password";
                                submitErrorLbl.Visible = true;
                            }
                            else
                            {
                                UpdateAdminPassword();

                                submitSuccessLbl.Text = "Password succesfully changed";
                                submitSuccessLbl.Visible = true;

                            }
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    submitErrorLbl.Text = "Error: " + ex;
                    submitErrorLbl.Visible = true;
                }
            }


        }




        private void UpdateAdminPassword()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string updateQuery = "UPDATE user SET password = @NewPassword WHERE user_id = @UserId";

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        PasswordHasher passwordHasher = new PasswordHasher();
                        string hashPassword = passwordHasher.HashPassword(NewPasswordTb.Text.ToString());

                        command.Parameters.AddWithValue("@NewPassword", hashPassword);
                        command.Parameters.AddWithValue("@UserId", Session["Current_ID"]);
                        command.ExecuteNonQuery();
                    }

                    connection.Close();


                }
                catch (Exception ex)
                {
                    submitSuccessLbl.Text = "Error: " + ex;
                    submitSuccessLbl.Visible = true;

                }
            }
        }
    }
}