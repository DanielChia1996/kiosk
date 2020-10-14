using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventCheckIn
{
    public partial class WorkshopDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Form.DefaultButton = btnLogIn.UniqueID;
            if (!IsPostBack)
            {
                ViewState["PreviousPage"] = Request.UrlReferrer;
            }
            string url = HttpContext.Current.Request.Url.ToString();
            if(url.StartsWith("htt;") && !url.Contains("localhost"))
            {
                HttpContext.Current.Response.Redirect("https" + url.Remove(0, 4));
            }
            if(Session["WSUID"] != null)
            {

            }
            tbUserName.Focus();
        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            //use ADAuthentication Web Services
            edu.wsu.aiswebservice.WSUADAuthenticateUser_WS ADWS = new edu.wsu.aiswebservice.WSUADAuthenticateUser_WS();
            edu.wsu.aiswebservice.WSUAuthenticateUserGetADIdentityAttributesGetADGroupMembershipsReturnObj AuthWithData;

            //pass items to web service
            try
            {
                AuthWithData = ADWS.WSUAuthenticateUserGetADIdentityAttributesGetADGroupMemberships(tbUserName.Text, tbPassword.Text);

                // User has to either have CACD group membership or in access list 
                if (AuthWithData.UserIsAuthenticated)
                {
                    Session["WSUID"] = AuthWithData.UserWSUID; // No leading 0
                    Session["Email"] = AuthWithData.UserEmail;
                    Session["Name"] = AuthWithData.UserFullName;

                    if (ViewState["PreviousPage"] != null)
                    {

                        Response.Redirect(ViewState["PreviousPage"].ToString());
                    }
                    else
                    {
                        Response.Redirect("~/WorkshopDashboard.aspx");
                    }
                   
  
                }
                else
                {
                    lblError.Text = "Username or password is incorrect.  Please try again.  If you continue to have issues, please contact Javier @ 509-335-8876.";
                }
            }
            catch
            {
                lblError.Text = "We are currently experiencing technical difficulties.  Please try again later.";
            }
        }
    }
}