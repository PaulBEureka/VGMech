using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Classes;

namespace VisualMech
{
    public partial class AdminLoginPage : System.Web.UI.Page
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            if(UsernameTb.Text == "" || PasswordTb.Text == "")
            {
                InputLbl.Text = "Invalid Input";
                InputLbl.Visible = true;
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
                        command.Parameters.AddWithValue("@Username", UsernameTb.Text);

                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();

                                int user_id = reader.GetInt32(reader.GetOrdinal("user_id"));
                                string username = reader.GetString(reader.GetOrdinal("username"));
                                string password = reader.GetString(reader.GetOrdinal("password"));

                                PasswordHasher passwordHasher = new PasswordHasher();

                                // Verify the password
                                bool isMatch = passwordHasher.VerifyPassword(PasswordTb.Text, password);

                                if (!isMatch)
                                {
                                    InputLbl.Text = "Invalid Input";
                                    InputLbl.Visible = true;
                                }
                                else
                                {
                                    Session["CurrentUser"] = username;
                                    Session["Current_ID"] = user_id;

                                    Response.Redirect("AdminDashboard.aspx");



                                }
                            }
                            else
                            {
                                InputLbl.Text = "Invalid Input";
                                InputLbl.Visible = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    InputLbl.Text = "Error: " + ex;
                    InputLbl.Visible = true;
                }
            }
        }
    }
}