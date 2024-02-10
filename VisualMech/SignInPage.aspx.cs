using System;
using System.Collections;
using System.Collections.Generic;
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
        public static SqlConnection connection;
        public static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\VGMechDatabase.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_btn_Click(object sender, EventArgs e)
        {
            string query = $@"
                        SELECT *
                        FROM UserTable 
                        WHERE UserTable.username = '{Username_tb.Text}'";

            string result = "";

            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows) // This means it got some match
                        {
                            No_Username_lbl.Visible = false;
                            Incorrect_lbl.Visible = false;
                            reader.Read();

                            string username = reader.GetString(reader.GetOrdinal("username"));
                            string password = reader.GetString(reader.GetOrdinal("password"));

                            PasswordHasher passwordHasher = new PasswordHasher();


                            // Verify the password
                            bool isMatch = passwordHasher.VerifyPassword(Password_tb.Text, password);

                            if (!isMatch)
                            {
                                Incorrect_lbl.Visible = true;
                                
                            }
                            else
                            {
                                Username_tb.Text = $"hi {username}";
                            }
                        }
                        else
                        {
                            No_Username_lbl.Visible = true;

                        }

                        connection.Close();
                    }
                }

                catch (Exception ex)
                {
                    result = ex.Message;
                    Response.Write(result);
                }

            }
        }
    }
}