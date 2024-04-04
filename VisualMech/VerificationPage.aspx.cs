using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VisualMech.Classes;

namespace VisualMech
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void OTPbtn_Click(object sender, EventArgs e)
        {
            if (EmailSender.OTP == OTPtb.Text)
            {
                Session["CurrentActivation"] = "1";
                EmailSender.OTP = null;
                Response.Redirect("HomePage.aspx");
            }
            else
            {
                Session["CurrentUser"] = null;
                Session["Current_ID"] = null;
                Session["CurrentEmail"] = null;
            }
        }



        

        protected async void ResendBtn_Click(object sender, EventArgs e)
        {
            int currentValue = Convert.ToInt32(hfCountdownValue.Value);

            if (currentValue > 1)
            {
                hfCountdownValue.Value = (currentValue - 1).ToString();
                ResendBtn.Text = $@"Resend OTP (Limit - {hfCountdownValue.Value} / 5)";
                
                await ResendOTP();
                ResendBtn.Enabled = true;
            }
            else
            {
                hfCountdownValue.Value = (currentValue - 1).ToString();
                ResendBtn.Text = $@"Resend OTP (Limit - {hfCountdownValue.Value} / 5)";
                ResendBtn.Enabled = false;
            }
        }

        private async Task ResendOTP()
        {
            await EmailSender.SendOTPEmailAsync(Session["CurrentEmail"].ToString());
        }

    }
}