using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventCheckIn
{
    public partial class Confirmation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            litFirstName.Text = Session["FirstName"].ToString();

            if (Session["QualtricsURL"] == null || Session["QualtricsURL"].ToString() == "" || Session["QualtricsURL"].ToString() == null)
            {
                Response.AddHeader("REFRESH", "3;URL=CheckIn.aspx?EventID=" + Request.QueryString["EventID"]);
            }

            else
            {   //https://wsu.co1.qualtrics.com/SE/?SID=SV_2nUI4s7SxCovJA1
                Response.Redirect(Session["QualtricsURL"].ToString() + "?&WSUID=" + Session["WSUID"].ToString() + "&W_CLASS_LEVEL=" + Session["W_CLASS_LEVEL"].ToString());
            }
        }

        protected void lbRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckIn.aspx?EventID=" + Request.QueryString["EventID"]);
        }
    }
}