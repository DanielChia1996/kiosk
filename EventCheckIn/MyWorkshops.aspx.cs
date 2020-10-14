using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventCheckIn
{
    public partial class MyWorkshops : System.Web.UI.Page
    {
        EventCheckInDataClassesDataContext db = new EventCheckInDataClassesDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["WSUID"] == null)
            {
                Response.Redirect("WorkshopDashboard.aspx");

            }

            var myRSVPs = (from RSVPs in db.WorkshopRSVPs
                           join workshops in db.workshops on RSVPs.workshopNum equals workshops.workshopID
                           where (RSVPs.WSUID == Session["WSUID"].ToString() || RSVPs.WSUID == "00" + Session["WSUID"].ToString()
                           || RSVPs.WSUID == "0" + Session["WSUID"].ToString()) && (workshops.workshopDate > DateTime.Now)
                           orderby workshops.workshopDate ascending
                           select workshops);

            var pastWorkshopsAttended = (from workshops in db.workshops
                             join checkIns in db.CheckIns on workshops.KioskID equals checkIns.CIEventID 
                             where (checkIns.CIWSUID == Session["WSUID"].ToString()
                             || checkIns.CIWSUID == "0" + Session["WSUID"] || checkIns.CIWSUID == "00" + Session["WSUID"].ToString()
                             ) && (workshops.workshopDate <= DateTime.Now)
                             orderby workshops.workshopDate ascending
                             select workshops);

            var RSVPsAndCheckins = (from RSVPs in db.WorkshopRSVPs
                                    join checkIns in db.CheckIns on RSVPs.WSUID equals checkIns.CIWSUID
                                    where (RSVPs.WSUID == Session["WSUID"].ToString() || RSVPs.WSUID == "00" + Session["WSUID"].ToString()
                                    || RSVPs.WSUID == "0" + Session["WSUID"].ToString()) 
                                    select RSVPs);
           
            var pastRSVPs = (from RSVPs in db.WorkshopRSVPs
                             join workshops in db.workshops on RSVPs.workshopNum equals workshops.workshopID
                             where (RSVPs.WSUID == Session["WSUID"].ToString() || RSVPs.WSUID == "00" + Session["WSUID"].ToString()
                             || RSVPs.WSUID == "0" + Session["WSUID"].ToString()) && (workshops.workshopDate < DateTime.Now)
                             orderby workshops.workshopDate ascending
                             select workshops);
            var NotWent = pastRSVPs.Except(pastWorkshopsAttended);

            PastWorkshopGV.DataSource = pastWorkshopsAttended;
            PastWorkshopGV.DataBind();
            MissedWorkshopsGV.DataSource = NotWent;
            MissedWorkshopsGV.DataBind();
            WorkshopGV.DataSource = myRSVPs;
            WorkshopGV.DataBind();

            if (!pastWorkshopsAttended.Any())
            {
                pnlAttended.Visible = false;
            }
            if (!NotWent.Any())
            {
                pnlMissed.Visible = false;
            }
            if (!myRSVPs.Any())
            {
                pnlUpcoming.Visible = false;
            }

        }
    }
}