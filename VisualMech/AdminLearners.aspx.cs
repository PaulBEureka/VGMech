using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using VisualMech.Classes;

namespace VisualMech
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowUserData();
            }
            
        }

        protected void LearnerGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            LearnerGridView.EditIndex = -1;
            ShowUserData();
        }

        protected void LearnerGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            LearnerGridView.EditIndex = e.NewEditIndex;
            ShowUserData();
        }

        protected void LearnerGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = LearnerGridView.Rows[e.RowIndex];
            string userID = ((Label)row.FindControl("lblUserID")).Text;
            string username = ((TextBox)row.FindControl("txtUsername")).Text;
            string email = ((TextBox)row.FindControl("txtEmail")).Text;


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string updateQuery = "UPDATE user SET username = @Username, email = @Email WHERE user_id = @UserId";

                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {

                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@UserId", userID);
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                finally
                {
                    LearnerGridView.EditIndex = -1;
                    ShowUserData();
                }

            }
        }

        protected void LearnerGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell cell = e.Row.Cells[0];

                foreach (Control control in cell.Controls)
                {
                    if (control is LinkButton)
                    {
                        LinkButton button = (LinkButton)control;
                        if (button.CommandName == "Delete")
                        {
                            string userID = LearnerGridView.DataKeys[e.Row.RowIndex]["user_id"].ToString();
                            button.OnClientClick = "return confirmDelete('" + userID + "');";
                        }
                    }
                }
            }
        }

        protected void LearnerGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string userID = LearnerGridView.DataKeys[e.RowIndex].Value.ToString();

            MySqlConnection con = new MySqlConnection(connectionString);

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM user WHERE user_id=@UserID", con);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                con.Close();
            }

            ShowUserData();
        }


        protected void ShowUserData()
        {
            DataTable dt = new DataTable();
            MySqlConnection con = new MySqlConnection(connectionString);

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user where role = 'user'", con);
                MySqlDataAdapter adapt = new MySqlDataAdapter(cmd);
                adapt.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    LearnerGridView.DataSource = dt;
                    LearnerGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
    
    
    }
}