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
                    Response.Write(result);

                    connection.Close();
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                    Response.Write(result);
                }
            }

            Response.Write(result);
        }
    }
}