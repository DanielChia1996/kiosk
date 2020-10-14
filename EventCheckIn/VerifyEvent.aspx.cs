using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventCheckIn
{
    public partial class VerifyEvent : System.Web.UI.Page
    {
        EventCheckInDataClassesDataContext db = new EventCheckInDataClassesDataContext();
        int KioskID = new int();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["WSUID"] = null;

            string url = HttpContext.Current.Request.Url.ToString();
            if (url.StartsWith("http:") && !url.Contains("localhost"))
            {
                HttpContext.Current.Response.Redirect("https" + url.Remove(0, 4));
            }

            if(Request.QueryString["ID"] == null)
            {
                lblError.Text = "Kiosk not found";
                Response.Redirect("Default.aspx");
            }
            this.KioskID = Convert.ToInt32(Request.QueryString["ID"]);

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

                    var SecurityDetails = (from details in db.EventSecurities
                                           where details.EventID == this.KioskID
                                           select details).FirstOrDefault();

                    var logCount = (from logs in db.SecurityLogs
                                    where (AuthWithData.UserWSUID == logs.WSUID ||
                                    "0" + AuthWithData.UserWSUID == logs.WSUID ||
                                    "00" + AuthWithData.UserWSUID == logs.WSUID) &&
                                    logs.LoginAllowed == false &&
                                    logs.kioskID == Convert.ToInt32(Request.QueryString["ID"]) &&
                                    Convert.ToDateTime(logs.OnDate).Date == DateTime.Today
                                    select logs).Count();
                    if(logCount >= 5)
                    {
                        lblError.Text = "You have made too many attempts to login to this event today. If you believe that you should be allowed to login please contact ascc.tech.support@wsu.edu.";
                        btnLogIn.Visible = false;

                        return;
                    }
                    if (tbKioskPassword.Text == SecurityDetails.pin)
                    {
                        SecurityLog log = new SecurityLog()
                        {
                            Event = SecurityDetails.Event,
                            kioskID = this.KioskID,
                            OnDate = DateTime.Now,
                            pinUsed = tbKioskPassword.Text,
                            User = AuthWithData.UserFullName,
                            LoginAllowed = true,
                            LoginIP = Request.ServerVariables["REMOTE_ADDR"],
                            WSUID = AuthWithData.UserWSUID,
                        };
                        db.SecurityLogs.InsertOnSubmit(log);
                        db.SubmitChanges();

                        Session["AllowCheckin"] = "true";
                        Response.Redirect("CheckIn.aspx?EventID=" + this.KioskID);
                    }
                    else
                    {
                        lblError.Text = "Username and password were entered correctly. However, the Kiosk pin was Incorrect.  To request access, please email ascc.tech.support@wsu.edu.";
                        SecurityLog log = new SecurityLog()
                        {
                            Event = SecurityDetails.Event,
                            kioskID = this.KioskID,
                            OnDate = DateTime.Now,
                            pinUsed = tbKioskPassword.Text,
                            User = AuthWithData.UserFullName,
                            LoginIP = Request.ServerVariables["REMOTE_ADDR"],
                            LoginAllowed = false,
                            WSUID = AuthWithData.UserWSUID,
                        };
                        db.SecurityLogs.InsertOnSubmit(log);
                        db.SubmitChanges();
                        Session.Abandon();
                        Session.Clear();
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