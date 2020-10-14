using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventCheckIn
{
    public partial class Kiosk : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            if (url.StartsWith("http:") && !url.Contains("localhost"))
            {
                HttpContext.Current.Response.Redirect("https" + url.Remove(0, 4));
            }

            if (Session["WSUID"] == null)
                Response.Redirect("Default.aspx");

            // Highlight the current page at navigation section by adding a "current" class to li
            if (url.Contains("Dashboard"))
                dashboard.Attributes["class"] = "current";
        }
    }
}