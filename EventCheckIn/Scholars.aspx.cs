using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventCheckIn
{
    public partial class Scholars : System.Web.UI.Page
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
            if(Request.QueryString["EventID"] == null || Request.QueryString["CheckID"] == null)
            {
                Response.Redirect("Dashboard.aspx");
            }
            Response.AddHeader("REFRESH", "60;URL=Checkin.aspx?EventID=" + Request.QueryString["EventID"]);
            eventID = Convert.ToInt32(Request.QueryString["EventID"]);
             CheckInID = Convert.ToInt32(Request.QueryString["CheckID"]);

            var scholarTypes = (from reasons in db.KioskReasons
                                where (eventID == reasons.KioskID) && (reasons.ReasonType == 1)
                                select reasons).ToList();
            int typeCount = scholarTypes.Count;
            var parent = litOp1.Parent;
            for(var i = 1; i <= typeCount; ++i)
            {
                var listItem = Page.FindControl("li" + i);
                listItem.Visible = true;
                var control = parent.FindControl("litOp" + i) as Literal;
                if (control != null)
                {
                    control.Text = scholarTypes[i - 1].ReasonString;
                }
            }
            if(typeCount < 1 || typeCount > 5)
            {
                lblError.Text = "There was a technical Error";
                Response.Redirect("Confirmation.aspx?EventID=" + Request.QueryString["EventID"]);
            }



        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           
            if (!cbOp1.Checked && !cbOp2.Checked && !cbOp3.Checked && !cbOp4.Checked && !cbOp5.Checked)
            {
                lblError.Text = "Please Select At least One";
                return;
            }

            CheckInReason Ci = new CheckInReason();
            Ci.CheckInID = CheckInID;
            

            for (var i = 1; i <= 5; ++i)
            {
                var checkBox = Page.FindControl("cbOp" + i) as CheckBox;
                var literal = Page.FindControl("litOp" + i) as Literal;
                if(checkBox != null && literal != null)
                {
                    if (checkBox.Checked)
                    {
                        if (!(string.IsNullOrEmpty(Ci.Scholar)))
                        {
                            Ci.Scholar += ", " + literal.Text.Trim();
                        }
                        else
                        {
                            Ci.Scholar = literal.Text.Trim();
                        }
                    } 
                }
            }

            db.CheckInReasons.InsertOnSubmit(Ci);
            try
            {
                db.SubmitChanges();
            }
            catch
            {
                lblError.Text = "There was a technical Error";
                return;

            }
            

           Response.Redirect("WhatBringsYouIn.aspx?EventID=" + eventID.ToString() + "&CheckID=" + CheckInID.ToString());
        }
    }

}