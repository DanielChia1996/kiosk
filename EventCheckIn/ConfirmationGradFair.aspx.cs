using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventCheckIn
{
    public partial class ConfirmationGradFair : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            litFirstName.Text = Session["FirstName"].ToString();
            //litLastName.Text = Session["LastName"].ToString();
            //Response.AddHeader("REFRESH", "3;URL=CheckIn.aspx?EventID=" + Request.QueryString["EventID"]);
        }

        protected void lbRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckIn.aspx?EventID=" + Request.QueryString["EventID"]);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            EventCheckInDataClassesDataContext kiosk = new EventCheckInDataClassesDataContext();
            CheckIn newCheckIn = new CheckIn();

            newCheckIn.CIEventID = Convert.ToInt32(Request.QueryString["EventID"].ToString());
            newCheckIn.CIWSUID = Session["WSUID"].ToString();
            newCheckIn.CIFirstName = Session["FirstName"].ToString();
            newCheckIn.CILastName = Session["LastName"].ToString();
            newCheckIn.CIApproved = true;

            newCheckIn.CIErrorMessage = "";
            newCheckIn.CISubmitDate = DateTime.Now;


            kiosk.CheckIns.InsertOnSubmit(newCheckIn);
            kiosk.SubmitChanges();
                            
            Response.Redirect("https://wsu.co1.qualtrics.com/SE/?SID=SV_9Nqmi1KYUYIj4I5&WSUID=" + Session["WSUID"].ToString() + "&W_CLASS_LEVEL=" + Session["W_CLASS_LEVEL"].ToString());
            //Response.Redirect("https://wsu.co1.qualtrics.com/SE/?SID=SV_1Ba38DT55YcpxYx&WSUID=" + Session["WSUID"].ToString() + "&W_CLASS_LEVEL=" + Session["W_CLASS_LEVEL"].ToString());
        }

        protected void btnStartOver_Click(object sender, EventArgs e)
        {
            Response.Redirect("GradFair.aspx?EventID=" + Request.QueryString["EventID"]);
        }
    }
}