using System;
using System.Linq;
using System.Web;
//using System.Collections.Generic;
//using System.Web.UI;
//using System.Web.UI.WebControls;

namespace EventCheckIn
{
    public partial class Default : System.Web.UI.Page
    {
        EventCheckInDataClassesDataContext kiosk = new EventCheckInDataClassesDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["WSUID"] = null;

            string url = HttpContext.Current.Request.Url.ToString();
            if (url.StartsWith("http:") && !url.Contains("localhost"))
            {
                HttpContext.Current.Response.Redirect("https" + url.Remove(0, 4));
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
                if (AuthWithData.UserIsAuthenticated == true)
                {
                    Session["WSUID"] = AuthWithData.UserWSUID; // No leading 0

                    if (AuthWithData.UserGroupMemberships.Contains("CACD"))
                    {
                        Response.Redirect("Dashboard.aspx");
                    }
                    
                    else
                    {
                        var IsInAccessList = (from users in kiosk.AccessLists
                                              where (users.WSUID == Session["WSUID"].ToString() || users.WSUID == "0" + Session["WSUID"].ToString() || users.WSUID == "00" + Session["WSUID"].ToString()) && users.IsActive == true
                                              select users.AccessListID);

                        if (IsInAccessList.Any())
                        {
                            Response.Redirect("Dashboard.aspx");
                        }
                            
                        else
                        {
                            lblError.Text = "Username and password were entered correctly. However, you do not access to this resource.  To request access, please email ascc.tech.support@wsu.edu.";
                            Session.Abandon();
                            Session.Clear();
                        }
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