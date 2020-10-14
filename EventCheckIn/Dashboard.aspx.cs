using System;
using System.Net.Mail;
using System.Linq;
//using System.Collections.Generic;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

namespace EventCheckIn
{
    public partial class Dashboard : System.Web.UI.Page
    {
        EventCheckInDataClassesDataContext kiosk = new EventCheckInDataClassesDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["WSUID"] == null) // To prevent NullReferenceException on postback
                Response.Redirect("Default.aspx");

            
            if (!Page.IsPostBack)
            {
                var IsInAdminList = from users in kiosk.AdminLists
                                    where (users.WSUID == Session["WSUID"].ToString() || users.WSUID == "0" + Session["WSUID"].ToString() ||
                                    users.WSUID == "00" + Session["WSUID"].ToString()) && (users.IsValid == true) && (users.SuperAdmin == true)
                                    select users;
                if (IsInAdminList.Any())
                {
                    //Show the workshop creation Details to the user
                    btnSubmitEvent.Visible = false;
                    btnSubmitWorkshop.Visible = true;
                    tblTime.Visible = true;
                    pnlEventDesc.Visible = true;
                    tblWorskhopDetails.Visible = true;
                    checkIsVisible.Visible = true;
                }
                else //Hide the workshop creation details from the user
                {
                    btnSubmitEvent.Visible = true;
                    btnSubmitWorkshop.Visible = false;
                    tblTime.Visible = false;
                    pnlEventDesc.Visible = false;
                    tblWorskhopDetails.Visible = false;
                    checkIsVisible.Visible = false;
                }
            }
          
        }

        // Insert event details into Event table
        protected void btnSubmitEvent_Click(object sender, EventArgs e)
        {
            //Event newEvent = new Event();
            SubmitNewEventPresenterEmail();
            IfCategorySelectedEmail();
        }

        private void IfCategorySelectedEmail()
        {
            Response.Redirect("CheckIn.aspx?EventID=" + (Session["EventID"]));          
        }

        private void SubmitNewEventPresenterEmail()
        {
            if(Session["mail"] != null)
            {
                lblError.Text = Session["mail"].ToString();
                return;
            }
            if(Session["wsuHostedStudentEmailAddress"] != null)
            {
                lblError.Text = Session["wsuHostedStudentEmailAddress"].ToString();
                return;
            }
            if(Session["NID"] != null)
            {
                lblError.Text = Session["NID"].ToString();
                return;
            }
            try
            {
                MailAddress check = new MailAddress(tbEmail.Text.ToString());
                if (!tbEmail.Text.ToString().Contains("wsu.edu"))
                {
                    lblError.Text = "Please use your WSU email Address";
                }
            }
            catch
            {
                lblError.Text = "Invalid Email Address entered";
            }
            // Insert event info
            Event newEvent = new Event();
            if (rbListCategory.SelectedValue != "CSF" && rbListCategory.SelectedValue != "LSAMP")
            {
                
                newEvent.EventCategory = rbListCategory.SelectedValue;
                newEvent.EventDate = Convert.ToDateTime(tbEventDate.Text);
                newEvent.UserNID = Session["WSUID"].ToString();
                newEvent.EventName = tbEventName.Text;
                newEvent.SubmitTime = DateTime.Now;
                newEvent.Term = Convert.ToInt16(ddlTerm.SelectedItem.Text);
                newEvent.SecurityEnabled = cbSecurePin.Checked;
                //newEvent.Term = kiosk.AcademicTerms.Max(x => x.Term);  // default max term in AcademicTerm table as the current term
                newEvent.Reinstatement = cbReinstatement.Checked;
                if (cbSecurePin.Checked)
                {
                    if(tbPassword.Text != tbPasswordConfirmation.Text && tbPassword.Text.Length < 4)
                    {
                        lblError.Text = "The passwords don't match";
                        if (tbPassword.Text.Length < 4)
                        {
                            lblError.Text = "The Password is not long enough";
                        }
                        return;
                    }

                }
                if (cbSecurePin.Checked)
                {
                    if(tbPassword.Text != tbPasswordConfirmation.Text)
                    {
                        lblError.Text = "Passwords Don't Match";
                        return;
                    }
                }
                if (rbQualtricsYes.Checked == true)
                {
                    newEvent.QualtricsRedirectURL = tbQualtrics.Text;
                }
                else
                {
                    newEvent.QualtricsRedirectURL = "N/A";
                }

                newEvent.QualtricsRedirectURL = tbQualtrics.Text;
                if (cbDefaultNumPad.Checked == true)
                {
                    newEvent.DefaultNumPad = true;
                }
                else
                {
                    newEvent.DefaultNumPad = false;
                }
                if (cbShowDateLabel.Checked == true)
                {
                    newEvent.ShowDateLabel = true;
                }
                else
                {
                    newEvent.ShowDateLabel = false;
                }

                kiosk.Events.InsertOnSubmit(newEvent);
                kiosk.SubmitChanges();
                Session["EventID"] = Convert.ToString(newEvent.EventID);

                // Insert presenters
                EventPresenter newPresenter = new EventPresenter();
                newPresenter.EventID = newEvent.EventID;
                newPresenter.PresenterName = ddlPresenter.SelectedValue;
                kiosk.EventPresenters.InsertOnSubmit(newPresenter);

                // Add the second presenter if the panel is visible
                if (pnlNewPresenter.Visible)
                {
                    EventPresenter newPresenter1 = new EventPresenter();
                    newPresenter1.EventID = newEvent.EventID;
                    newPresenter1.PresenterName = ddlPresenter1.SelectedValue;
                    kiosk.EventPresenters.InsertOnSubmit(newPresenter1);
                }
                if (newEvent.SecurityEnabled == true)
                {
                    if (tbPassword.Text == tbPasswordConfirmation.Text && tbPassword.Text.Length >= 4)
                    {
                        EventSecurity eventSecurity = new EventSecurity()
                        {
                            EventID = newEvent.EventID,
                            Event = newEvent,
                            pin = tbPassword.Text,
                        };
                        kiosk.EventSecurities.InsertOnSubmit(eventSecurity);
                    }
                }

                kiosk.SubmitChanges();
            }
            else
            {
                newEvent.SecurityEnabled = cbSecurePin.Checked;

                if (rbListCategory.SelectedValue == "CSF")
                {
                    newEvent.EventCategory = "CSF";
                    newEvent.EventName = "CSF_Check-in_" + ddlTerm.SelectedValue;
                }
                else
                {
                    newEvent.EventCategory = rbListCategory.SelectedValue;
                    if (string.IsNullOrEmpty(tbEventName.Text))
                    {
                        newEvent.EventName = "LSAMP_CheckIn_" + ddlTerm.SelectedItem.Text;
                        newEvent.Reinstatement = false;
                        newEvent.ShowDateLabel = false;
                        newEvent.EventDate = DateTime.Today;
                    }
                    else
                    {
                        newEvent.EventName = tbEventName.Text.Trim();
                        newEvent.Reinstatement = cbReinstatement.Checked;
                        newEvent.ShowDateLabel = cbShowDateLabel.Checked;
                        try { Convert.ToDateTime(tbEventDate.Text); }
                        catch { lblError.Text = "Please use a valid date in the form mm/dd/yyyy."; }
                        newEvent.EventDate = Convert.ToDateTime(tbEventDate.Text);
                    }
                }
                newEvent.Term = Convert.ToInt16(ddlTerm.SelectedItem.Text);
                newEvent.SubmitTime = DateTime.Now;
                newEvent.UserNID = Session["WSUID"].ToString();
                



                if (rbQualtricsYes.Checked == true)
                {
                    newEvent.QualtricsRedirectURL = tbQualtrics.Text;
                }
                else
                {
                    newEvent.QualtricsRedirectURL = "N/A";
                }

                newEvent.QualtricsRedirectURL = tbQualtrics.Text;
                if (cbDefaultNumPad.Checked == true)
                {
                    newEvent.DefaultNumPad = true;
                }
                else
                {
                    newEvent.DefaultNumPad = false;
                }

                kiosk.Events.InsertOnSubmit(newEvent);
                if (newEvent.SecurityEnabled == true)
                {
                    if (tbPassword.Text == tbPasswordConfirmation.Text && tbPassword.Text.Length >= 4)
                    {
                        EventSecurity eventSecurity = new EventSecurity()
                        {
                            EventID = newEvent.EventID,
                            Event = newEvent,
                            pin = tbPassword.Text,
                        };
                        kiosk.EventSecurities.InsertOnSubmit(eventSecurity);
                    }
                }
                kiosk.SubmitChanges();
                Session["EventID"] = Convert.ToString(newEvent.EventID);
                int counter = 1, reason = 1, type = 1;
                var parent = tbScholarOp1.Parent;
                //Inserts the scholar types into the database: Each text box for the scholars are in the form tbScholarOp#,
                //This allows for the loop to search for the name based on the counter and insert their contents to the database
                //The scholar reasons is used for CSF kiosks.
                if(rbListCategory.SelectedValue == "CSF")
                {
                    while (counter <= 5)
                    {

                        var control = parent.FindControl("tbScholarOp" + counter.ToString()) as System.Web.UI.WebControls.TextBox;
                        if (control != null && !String.IsNullOrEmpty(control.Text))
                        {
                            KioskReason newReason = new KioskReason();
                            newReason.KioskID = newEvent.EventID;
                            newReason.ReasonNum = reason;
                            newReason.ReasonType = type;
                            newReason.ReasonString = control.Text.Trim();
                            kiosk.KioskReasons.InsertOnSubmit(newReason);
                            kiosk.SubmitChanges();
                        }
                        counter++;
                        reason++;
                    }
                }
                parent = tbActivityOp1.Parent;
                counter = 1; reason = 1; type = 0;

                //Inserts the Activities into the database, this allows for students to select what brought them in.
                //It is used for the CSF, and LSAMP kiosks. Each text box for the options is in the form tbActivityOp#
                //which allows for the loop to find each control and insert the contents of the text box.

                while(counter <= 9)
                {
                    var control = parent.FindControl("tbActivityOp" + counter.ToString()) as System.Web.UI.WebControls.TextBox;
                    if(control != null && !string.IsNullOrEmpty(control.Text))
                    {
                        KioskReason newReason = new KioskReason();
                        newReason.KioskID = newEvent.EventID;
                        newReason.ReasonNum = reason;
                        newReason.ReasonType = type;
                        newReason.ReasonString = control.Text.Trim();
                        kiosk.KioskReasons.InsertOnSubmit(newReason);
                        kiosk.SubmitChanges();
                    }
                    counter++;
                    reason++;
                }
            }



            //create link
            //string EventLink = "ID.aspx?Org=" + (rbListCategory.SelectedItem.Text);
            //string CategoryLink = "CheckIn.aspx?EventID=" + (Session["EventID"]);
           // string NewEmailURL = "";            
            //NewEmailURL = "https://kiosk.ascc.wsu.edu/" + EventLink;            

            // Email event details
            if (tbEmail.Text != "                                      @" && tbEmail.Text != "" && tbEmail.Text != null )
            {
                MailMessage objEmail = new MailMessage();
                objEmail.From = new MailAddress("ascc.tech.support@wsu.edu");
                objEmail.To.Add(tbEmail.Text);
                objEmail.Subject = newEvent.EventName + " Details";
                objEmail.Body =
                    "<ul>" +
                    "<li>" +
                    "Event Name: " + newEvent.EventName +
                    "</li>" +
                    "<li>" +
                    "Event Category: " + newEvent.EventCategory +
                    "</li>" +
                    "<li>" +
                    "Event Date: " + tbEventDate.Text +
                    "</li>" +
                    "<li>" +
                    "Presenter: " + ddlPresenter.SelectedValue +
                    "</li>" +

                     "<li>Event URL: <a href=" + "https://kiosk.ascc.wsu.edu/CheckIn.aspx?EventID=" + newEvent.EventID + ">" + "https://kiosk.ascc.wsu.edu/CheckIn.aspx?EventID=" + newEvent.EventID + "</a>" +
                    "</li>" +
                    "</ul>";

                objEmail.IsBodyHtml = true;

                //mail server
                SmtpClient sC = new SmtpClient("smtp.wsu.edu");

                try
                {
                    sC.Send(objEmail);
                }
                catch (Exception exc)
                {
                    Response.Write("Send failure: " + exc.ToString());
                }
            }
        }
        /// <summary>
        /// Function to add another presenter to the event. It will show the add presenter panel
        /// if it is not currently visible, otherwise it will hide the add presenter panel.
        /// </summary>
        /// <param name="sender"> btnAnotherPresenter, Not used in the function. </param>
        /// <param name="e"> Not used.</param>
        protected void btnAnotherPresenter_Click(object sender, EventArgs e)
        {
            if (btnAnotherPresenter.Text == "Add another presenter")
            {
                pnlNewPresenter.Visible = true;
                btnAnotherPresenter.Text = "Remove";
            }
            else // btnAnotherPresenter.Text == "Remove"
            {
                pnlNewPresenter.Visible = false;
                btnAnotherPresenter.Text = "Add another presenter";
            }
        }

        protected void btnSubmitEventEnterAnother_Click(object sender, EventArgs e)
        {
            SubmitNewEventPresenterEmail();
            rbListCategory.ClearSelection();
            cbReinstatement.Checked = false;
            tbEventDate.Text = string.Empty;
            tbEventDate.Text = string.Empty;
            tbEventName.Text = string.Empty;
            ddlPresenter.Items.Clear();
            tbEmail.Text = string.Empty;
            Response.Redirect(Request.RawUrl);
        }

        protected void rbQualtricsYes_CheckedChanged(object sender, EventArgs e)
        {
            QualtricsCheck();
        }


        protected void rbQualtricsNo_CheckedChanged(object sender, EventArgs e)
        {
            QualtricsCheck();
        }

        private void QualtricsCheck()
        {
            if (rbQualtricsYes.Checked == true)
            {
                tbQualtrics.Visible = true;
                lblQualtrics.Visible = true;
            }
            else
            {
                tbQualtrics.Visible = false;
                lblQualtrics.Visible = false;
            }
        }

        /// <summary>
        /// When the radio buttons for creating a workshop as a super admin are changed
        /// the proper fields should be shown. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SRadioButtonSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SRadioButtonSelection.SelectedValue == "1") //hide the workshop creation Details from the user
            {
                btnSubmitEvent.Visible = true;
                btnSubmitWorkshop.Visible = false;
                tblTime.Visible = false;
                pnlEventDesc.Visible = false;
                tblWorskhopDetails.Visible = false;
                checkIsVisible.Visible = false;
            }
            else //Show all of the workshop creation details to the user
            {
                btnSubmitEvent.Visible = false;
                btnSubmitWorkshop.Visible = true;
                tblTime.Visible = true;
                pnlEventDesc.Visible = true;
                tblWorskhopDetails.Visible = true;
                checkIsVisible.Visible = true;
            }
        }

        /* New Workshops can be entered both here and in the Event Details Page within the 
         * Kiosk Portal. If any changes are made here, please be sure to make the necessary changes there as well
         */
        protected void btnSubmitWorkshop_Click(object sender, EventArgs e)
        {
            int capacity = 0;
            if(!(txtEventCapacity.Text == ""))
            {
                try
                {
                    capacity = Int32.Parse(txtEventCapacity.Text);
                }
                catch
                {
                    lblError.Text = "Please enter an integer for the Event Capacity";
                    return;
                }
                capacity = Int32.Parse(txtEventCapacity.Text);
            }
            if(tbEventDate.Text == "")
            {
                tbEventDate.Text = "01/01/2099";
            }

            SubmitNewEventPresenterEmail();
            string userNID = Session["WSUID"].ToString();
            string eventID = Session["EventID"].ToString();
            workshop newWorkshop = new workshop();
            newWorkshop.workshopDate = Convert.ToDateTime(tbEventDate.Text + " " + DDLtime.SelectedItem + ":00 " + AMPMbutton.SelectedItem);
            newWorkshop.workshopLocation = txtEventLocation.Text.Trim();
            newWorkshop.workshopName = tbEventName.Text.Trim();
            newWorkshop.Capacity = capacity;
            newWorkshop.workshopDescription = txtWorkshopDesc.Text.Trim();
            newWorkshop.KioskID = Int32.Parse(Session["EventID"].ToString());
            newWorkshop.StudentsRegistered = 0;
            if (checkIsVisible.Checked)
                newWorkshop.IsVisible = true;
            else
                newWorkshop.IsVisible = false;
            try
            {
                kiosk.workshops.InsertOnSubmit(newWorkshop);
                kiosk.SubmitChanges();
            }
            catch
            {
                lblError.Text = "There was an error while submitting the workshop";
                return;
            }

            rbListCategory.ClearSelection();
            cbReinstatement.Checked = false;
            tbEventDate.Text = string.Empty;
            tbEventDate.Text = string.Empty;
            tbEventName.Text = string.Empty;
            ddlPresenter.Items.Clear();
            tbEmail.Text = string.Empty;
            txtEventCapacity.Text = string.Empty;
            txtEventLocation.Text = string.Empty;
            txtWorkshopDesc.Text = string.Empty;
            
            Response.Redirect(Request.RawUrl);


        }

        /// <summary>
        /// Function to show, or hide information depending on the selected
        /// kiosk category, if the selected category is CSF, or LSAMP, then the
        /// kiosk needs the additional fields for the check-in questions, thus
        /// the web page shows the information entry portion for each of those selections if they are
        /// selected.
        /// </summary>
        /// <param name="sender">The web-page</param>
        /// <param name="e">The event arguments</param>
        protected void rbListCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(rbListCategory.SelectedValue == "CSF")
            {
                tblTime.Visible = false;
                Activities.Visible = true;
                Scholars.Visible = true;
                btnSubmitEvent.Visible = true;
                btnSubmitWorkshop.Visible = false;
                tblDateAndName.Visible = false;
                tblWorskhopDetails.Visible = false;
               
 
            }
            else if(rbListCategory.SelectedValue == "LSAMP")
            {
                tblTime.Visible = false;
                tblWorskhopDetails.Visible = false;
                tblEventDetails.Visible = true;
                Scholars.Visible = false;
                tblDateAndName.Visible = true;
                Activities.Visible = true;
                btnSubmitEvent.Visible = true;
                btnSubmitWorkshop.Visible = false;
            }
            else
            {
                tblEventDetails.Visible = true;
                tblDateAndName.Visible = true;
                Activities.Visible = false;
                Scholars.Visible = false;
                SRadioButtonSelection_SelectedIndexChanged(sender, e);
            }
        }

        /// <summary>
        /// Event to show or hide the additional security pin if the user wants
        /// to verify the current session.
        /// </summary>
        /// <param name="sender">The CheckBox on the web page</param>
        /// <param name="e">The event arguments, not used</param>
        protected void cbSecurePin_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSecurePin.Checked)
            {
                pnlAddedSecurity.Visible = true;

            }
            else
            {
                pnlAddedSecurity.Visible = false;
            }
        }
    }
}