using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventCheckIn
{
    public partial class WorkshopBrowser : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["WSUID"] != null)
            {
                pnlmyWorkshops.Visible = true;
                pnlLogout.Visible = true;
                pnlLogin.Visible = false;
            }
            
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("workshopSearch.aspx?SearchString=" + txtWorkshopSearch.Text);
        }
    }
}