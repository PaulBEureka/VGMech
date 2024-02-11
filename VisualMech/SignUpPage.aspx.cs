using System;
using System.Collections.Generic;
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
        public static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\VGMechDatabase.mdf;Integrated Security=True";
        public static SqlConnection connection;

        protected void Page_Load(object sender, EventArgs e)
		{

		}

        
        

        protected void Register_btn_Click(object sender, EventArgs e)
        {
            string result = "";


            if (!CheckUsername())
            {
                taken_lbl.Visible = true;
            }
            else
            {
                taken_lbl.Visible = false;
                using (connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string query = "INSERT INTO UserTable (username , password) VALUES (@Username, @Password)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            PasswordHasher passwordHasher = new PasswordHasher();

                            string hashPassword = passwordHasher.HashPassword(New_Password_tb.Text);


                            command.Parameters.AddWithValue("@Username", New_Username_tb.Text);
                            command.Parameters.AddWithValue("@Password", hashPassword);
                            command.ExecuteNonQuery();
                        }


                        result = "New User Registered Successfully";

                        loginUser();

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

        private bool CheckUsername()
        {
            string result = "";

            string query = $@"
                        SELECT *
                        FROM UserTable 
                        WHERE UserTable.username = '{New_Username_tb.Text}'";


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

                            connection.Close();
                            return false;
                        }
                        else
                        {

                            connection.Close();
                            return true;

                        }

                    }
                }

                catch (Exception ex)
                {
                    result = ex.Message;
                    Response.Write(result);
                    return false;
                }

            }




        }



        private void loginUser()
        {
            string query = $@"
                        SELECT *
                        FROM UserTable 
                        WHERE UserTable.username = '{New_Username_tb.Text}'";


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
                            reader.Read();

                            int user_id = reader.GetInt32(reader.GetOrdinal("user_id"));

                            string username = reader.GetString(reader.GetOrdinal("username"));
                            string password = reader.GetString(reader.GetOrdinal("password"));

                            string num = reader.FieldCount.ToString();

                            
                                Session["CurrentUser"] = username;
                                Session["Current_ID"] = user_id;
                                Response.Redirect("HomePage.aspx");
                            
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