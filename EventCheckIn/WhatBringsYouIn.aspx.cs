using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventCheckIn
{
    public partial class WhatBringsYouIn : System.Web.UI.Page
    {
        EventCheckInDataClassesDataContext db = new EventCheckInDataClassesDataContext();

        protected int eventID { get; set; }
        protected int CheckInID { get; set; }
        private bool IsIOS
        {
            get
            {
                var userAgent = HttpContext.Current.Request.UserAgent.ToLower();
                return userAgent.Contains("ipad") || userAgent.Contains("iphone");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["EventID"] == null || Request.QueryString["CheckID"] == null)
            {
               
                Response.Redirect("Dashboard.aspx");
            }
            Response.AddHeader("REFRESH", "60;URL=Checkin.aspx?EventID=" + Request.QueryString["EventID"]);
            eventID = Convert.ToInt32(Request.QueryString["EventID"]);
            CheckInID = Convert.ToInt32(Request.QueryString["CheckID"]);

            var ReasonTypes = (from reasons in db.KioskReasons
                                where (eventID == reasons.KioskID) && (reasons.ReasonType == 0)
                                select reasons).ToList();

            int typeCount = ReasonTypes.Count, i;
            var parent = litOp1.Parent;
            for(i = 1; i <= typeCount; i++)
            {
                var listItem = Page.FindControl("li" + i);
                listItem.Visible = true;
                var control = parent.FindControl("litOp" + i) as Literal;

                if (control != null)
                {
                   control.Text = ReasonTypes[i - 1].ReasonString;
                }
            }

           
            if (typeCount < 1 || typeCount > 9)
            {
                lblError.Text = "There was a technical Error";
                Response.Redirect("Confirmation.aspx?EventID=" + Request.QueryString["EventID"]);
            }



        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!cbOp1.Checked && !cbOp2.Checked && !cbOp3.Checked && !cbOp4.Checked && 
                !cbOp5.Checked && !cbOp6.Checked && !cbOp7.Checked && !cbOp8.Checked && !cbOp9.Checked)
            {
                lblError.Text = "Please Select At least One";
                return;
            }
            var checkIn = (from checkins in db.CheckInReasons
                           where CheckInID == checkins.CheckInID
                           select checkins).FirstOrDefault();
            if (checkIn == null)
            {
                checkIn = new CheckInReason()
                {
                    CheckInID = CheckInID,
                    
                };
                db.CheckInReasons.InsertOnSubmit(checkIn);
                db.SubmitChanges();
            }
            int count = 1;
            var parent = litOp1.Parent;
            for (count = 1; count <= 9; count++) 
            {
                var control = parent.FindControl("litOp" + count) as Literal;
                var CheckBox = parent.FindControl("cbOp" + count) as CheckBox;
                
                if(control != null && CheckBox != null)
                {
              
                    if (CheckBox.Checked == true)
                    {
                        if (checkIn.ReasonString == null)
                        {
                            checkIn.ReasonString = control.Text.Trim();
                        }
                        else
                        {
                            if (count == 1)
                                checkIn.ReasonString += control.Text.Trim();
                            else
                                checkIn.ReasonString += ", " + control.Text.Trim();
                        }
                    }
                }
                else
                {
                    lblError.Text += "CB or Literal not found";
                }
            }
                try
                {
                    db.SubmitChanges();
                }
                catch
                {
                    lblError.Text = "There was a Technical Error";
                    return;

                }
            
           

            var CI = (from checkIns in db.CheckIns
                      where checkIns.CheckInID == CheckInID
                      select checkIns).First();
            Session["FirstName"] = CI.CIFirstName;
            Response.Redirect("Confirmation.aspx?EventID=" + Request.QueryString["EventID"]);

            // Response.Redirect(the address);
        }
       
    }
}