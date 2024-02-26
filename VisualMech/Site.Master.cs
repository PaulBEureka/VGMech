using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisualMech
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();
        }

        protected void btnCurrentUser_Click(object sender, EventArgs e)
        {
            Session["CurrentUser"] = null;
            Session["Current_ID"] = null;
            Session["sessionCaptcha"] = null;
            Response.Redirect(Request.Url.AbsoluteUri);
        }

    }
}