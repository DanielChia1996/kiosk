using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace EventCheckIn
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            if (url.StartsWith("http:") && !url.Contains("localhost"))
            {
                HttpContext.Current.Response.Redirect("https" + url.Remove(0, 4), false);
            }
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        // This event solves back button issue after logout
        // Instead of redirect to login page directly, we need a timer to allow page load completely
        protected void TimerLogout_Tick(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            // Redirect to time out page if it's session time out
            if (Request.QueryString["TimeOut"] == "True")
                Response.Redirect("TimeOut.aspx");
            // Otherwise, go to log in page
            Response.Redirect("Default.aspx");
        }
    }
}