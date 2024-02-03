using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace VisualMech
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection connection;
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\VGMechDatabase.mdf;Integrated Security=True";


        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void post_Click(object sender, EventArgs e)
        {
            
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Response.Write("<script>alert('Comment posted successfully')</script>");


                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error')</script>");
                }
            }

        }

        

}
}