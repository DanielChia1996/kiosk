using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace EventCheckIn
{
    public partial class WorkshopDetails : System.Web.UI.Page
    {
        EventCheckInDataClassesDataContext db = new EventCheckInDataClassesDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(Request.QueryString["workshopID"]);
            var query = from rsvps in db.WorkshopRSVPs
                        where id == rsvps.workshopNum
                        select rsvps;

            var workshop = (from workshops in db.workshops
                            where workshops.workshopID == id
                            select workshops).ToList();

            Panel loggedIn = (Panel)this.FormView1.FindControl("pnlRSVP");
            Panel notloggedIn = (Panel)this.FormView1.FindControl("pnlnotSignedin");
            Panel RSVPSent = (Panel)this.FormView1.FindControl("pnlAlreadyRSVP");
            Label lblAttendance = (Label)this.FormView1.FindControl("lblAttendance");
            if (workshop.Any())
            {
                if (workshop[0].Capacity == 0)
                {
                    try
                    { loggedIn.Visible = false; }
                    catch { }
                    try { notloggedIn.Visible = false; }
                    catch { }
                    try { RSVPSent.Visible = false; }
                    catch { }
                    return;

                }


            }
            if (Session["WSUID"] == null) // not signed in
            {
                try
                { loggedIn.Visible = false; }
                catch { }
                try { notloggedIn.Visible = true; }
                catch { }
                try { RSVPSent.Visible = false; }
                catch { }
                return;

            }
            else
            {
                if (query.Any())
                {
                    foreach(var rsvp in query)
                    {
                        if (rsvp.WSUID == Session["WSUID"].ToString())
                        {
                            if (workshop[0].workshopDate < DateTime.Now)
                            {
                                if (Attended(id))
                                {
                                    lblAttendance.Text = "Marked as Attended";
                                }
                                else
                                {
                                    lblAttendance.Text = "Marked as Missed";
                                }
                                try
                                { loggedIn.Visible = false; }
                                catch { }
                                try { notloggedIn.Visible = false; }
                                catch { }
                                try { RSVPSent.Visible = false; }
                                catch { }
                                return;
                            }
                            try
                            { loggedIn.Visible = false; }
                            catch { }
                            try { notloggedIn.Visible = false; }
                            catch { }
                            try { RSVPSent.Visible = true; }
                            catch { }
                            return;

                        }
                    }
                    if(workshop[0].Capacity == workshop[0].StudentsRegistered)
                    {
                        try
                        { loggedIn.Visible = false; }
                        catch { }
                        try { notloggedIn.Visible = false; }
                        catch { }
                        try { RSVPSent.Visible = false; }
                        catch { }
                        return;
                    }
                }
                try //If the workshop is not found, this will throw an error
                { notloggedIn.Visible = false;  
                }
                catch {  }
                try
                {
                    loggedIn.Visible = true;
                }
                catch { }
                return;
            }
        }

        protected void btnRSVP_Click(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(Request.QueryString["workshopID"]);
            var query = (from workshops in db.workshops
                         where workshops.workshopID == id
                         select workshops).ToList();
            if(query[0].Capacity <= query[0].StudentsRegistered)
            {
                lblError.Text = "There is no more seats available for this workshop";
                return;
            }
            if(Session["WSUID"] != null && Session["Name"] != null && Session["Email"] != null)
            {
                WorkshopRSVP n = new WorkshopRSVP();
                n.WSUID = Session["WSUID"].ToString();
                n.FullName = Session["Name"].ToString();
                n.email = Session["Email"].ToString();
                n.workshopNum = id;
                query[0].StudentsRegistered++;
                
                db.WorkshopRSVPs.InsertOnSubmit(n);
                db.SubmitChanges();
                SendEmailConfirmation(id);
            }
            else
            {
                lblError.Text = "Session Timeout Error";
                return;
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void SendEmailConfirmation(int workshopID)
        {
            var query = (from workshops in db.workshops
                         where workshopID == workshops.workshopID
                         select workshops).ToList();
            string email = Session["Email"].ToString();
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress("ascc@wsu.edu");
            mail.Subject = "RSVP Sent for " + query[0].workshopName + " on " + Convert.ToDateTime(query[0].workshopDate).ToShortDateString() + " " + Convert.ToDateTime(query[0].workshopDate).ToShortTimeString();

            mail.Body = $@"Thank you for sending an RSVP for the following workshop: <br />
                           Workshop Name: {query[0].workshopName} <br />
                           Date and time: {Convert.ToDateTime(query[0].workshopDate).ToShortDateString()} {Convert.ToDateTime(query[0].workshopDate).ToShortTimeString()} <br />
                           Location: {query[0].workshopLocation}<br />
                           You will be notified on any changes regarding this workshop.";
            mail.IsBodyHtml = true;

            SmtpClient sC = new SmtpClient("smtp.wsu.edu");
            try { sC.Send(mail); }
            catch
            {
                lblError.Text = "There was an error sending Confirmation Email. The RSVP has been successfully submitted however";
                return;
            }
            
        }

        protected void btnUnRSVP_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["workshopID"]);
            if(Session["WSUID"] == null)
            {
                Response.Redirect(Request.RawUrl);
            }
            string WSUID = Session["WSUID"].ToString();
            var query = from rsvps in db.WorkshopRSVPs
                        where WSUID == rsvps.WSUID
                        select rsvps;
            if (query.Any())
            {
                var workshop = (from workshops in db.workshops
                                where workshops.workshopID == id
                                select workshops).ToList();
                foreach(var rsvp in query)
                {
                    if(rsvp.workshopNum == id)
                    {
                        workshop[0].StudentsRegistered--;
                        db.WorkshopRSVPs.DeleteOnSubmit(rsvp);
                    }
                }
                try
                {
                    db.SubmitChanges();
                }
                catch
                {
                    lblError.Text = "There was an error while removing the RSVP";
                    return;
                }
                Response.Redirect(Request.RawUrl);
            }

        }


        protected bool Attended(int ID)
        {
            var workshopCheckin = (from workshops in db.workshops
                                   join Checkins in db.CheckIns on workshops.KioskID equals Checkins.CIEventID
                                   where workshops.workshopID == ID
                                   select Checkins.CIWSUID).ToList();
         

            foreach(var Checkin in workshopCheckin)
            {
                if(Session["WSUID"].ToString() == Checkin)
                {
                    return true;
                }
            }
            return false;


        }
    }
}