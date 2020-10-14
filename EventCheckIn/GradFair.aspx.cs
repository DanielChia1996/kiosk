using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Net;
using System.Text;

namespace EventCheckIn
{
    public partial class GradFair : System.Web.UI.Page
    {
        EventCheckInDataClassesDataContext kiosk = new EventCheckInDataClassesDataContext();
        int eventID;

        protected void Page_Load(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.ToString();
            if (url.StartsWith("http:") && !url.Contains("localhost"))
            {
                HttpContext.Current.Response.Redirect("https" + url.Remove(0, 4));
            }

          
            //if (Request.QueryString["EventID"] == null)
            //    Response.Redirect("Dashboard.aspx");
            //else
                eventID = 1107;
                    //Convert.ToInt32(Request.QueryString["EventID"]); // so we don't have to request the query string every time

            // Get event name and date as title
            var title = (from eventList in kiosk.Events
                         where eventList.EventID == eventID
                         select new
                         {
                             eventName = eventList.EventName,
                             eventDate = Convert.ToDateTime(eventList.EventDate)
                         }
                            ).First();

            // Format the date to MM/dd/yyyy
            lbEventTitle.Text = title.eventName + " " + title.eventDate.Month + "/" + title.eventDate.Day + "/" + title.eventDate.Year;

            // Make the WSU ID input box editable at PC and not editable at ipad
            bool isIpad = Request.UserAgent.ToLower().Contains("ipad");
            if (isIpad)
                txtWSUID.Attributes.Add("readOnly", "readOnly");

            // clears Sessions on load so errors don't report the name incorrectly.
            
            if (!IsPostBack)
            {
                //Session["FirstName"] = "";
                //Session["LastName"] = "";
                //Session["WSUID"] = "";
                //Session["W_CLASS_LEVEL"] = "";
                Session["IDFail"] = "No";
            }

            txtWSUID.Focus();
        }

        protected void btnCheckIn_Click(object sender, EventArgs e)
        {
            string WSUID = txtWSUID.Value;
            bool IsApproved = true;

            // Get student information by WSU web service
            try
            {
                WSUID = WSUID.Trim().PadLeft(9, '0');

                /// use SoapUI 3.6 to get the correct soap for use with Peoplesoft.  
                /// Copy it exactly here.
                /// SoapUI provides a quick way to test the web service, too.
                /// use the XML view here and the Raw view for the POST information below
                /// Note:  as of Dec/2011 Version 4.1 of SoapUI will not work to test the WS, use 3.6.1
                /// 
                /// Also, need to obfuscate the username/password.
                /// 
                /// http://csprdpr.wsu.edu/PSIGW/PeopleSoftServiceListeningConnector/W_BIOD_WEBSERVICES.1.wsdl
                /// 

                string soapStream = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:w=\"http://xmlns.oracle.com/Enterprise/Tools/schemas/W_BIOD_REQUEST.1\">";
                soapStream += "<soapenv:Header>";
                soapStream += "<wsse:Security soapenv:mustUnderstand=\"1\" xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">";
                soapStream += "<wsse:UsernameToken wsu:Id=\"UsernameToken-1\" xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\">";
                soapStream += "<wsse:Username>WSU_NSP</wsse:Username>";
                soapStream += "<wsse:Password Type=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText\">";
                soapStream += "zzusis$$NSP(</wsse:Password>";
                soapStream += "<wsse:Nonce EncodingType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\">TY/PXFoqeFZjwD1kwoAYfg==</wsse:Nonce>";
                soapStream += "<wsu:Created>" + DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss");
                soapStream += "</wsu:Created></wsse:UsernameToken></wsse:Security></soapenv:Header>";
                soapStream += "<soapenv:Body>";
                soapStream += "<w:W_BIOD_REQUEST>";
                soapStream += "<w:MsgData>";
                soapStream += "<!--Optional:-->";
                soapStream += "<w:Transaction>";
                soapStream += "<!--You may enter the following 2 items in any order-->";
                soapStream += "<!--Optional:-->";
                soapStream += "<w:W_BIOD_REQUEST class=\"R\">";
                soapStream += "<!--Optional:-->";
                soapStream += "<w:WSUID IsChanged=\"?\">" + WSUID + "</w:WSUID>";
                soapStream += "</w:W_BIOD_REQUEST>";
                soapStream += "</w:Transaction>";
                soapStream += "</w:MsgData>";
                soapStream += "</w:W_BIOD_REQUEST>";
                soapStream += "</soapenv:Body>";
                soapStream += "</soapenv:Envelope>";
                System.Net.HttpWebRequest req;
                //  if (type == "test")
                //     req = (HttpWebRequest)WebRequest.Create("https://csprd92pr.wsu.edu/PSIGW/PeopleSoftServiceListeningConnector");
                //  else
                req = (HttpWebRequest)WebRequest.Create("https://csprd92pr.wsu.edu/PSIGW/PeopleSoftServiceListeningConnector");

                byte[] reqBytes;
                // reqBytes = StrToByteArray(soapStream);
                reqBytes = Encoding.ASCII.GetBytes(soapStream);
                /// use the RAW information from SoapUI here
                req.Method = "POST";
                req.Headers.Add("Accept-Encoding", "gzip,deflate");
                req.ContentType = "text/xml;charset=UTF-8";
                req.Headers.Add("SOAPAction", "GetBioDemoData.v1");
                req.UserAgent = "Jakarta Commons-HttpClient/3.1";
                req.AllowAutoRedirect = false;
                req.ContentLength = reqBytes.Length;

                System.IO.Stream reqStream = req.GetRequestStream();
                reqStream.Write(reqBytes, 0, reqBytes.Length);

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                //here is a sample response:
                //POST https://csprd92pr.wsu.edu/PSIGW/PeopleSoftServiceListeningConnector HTTP/1.1
                //POST https://csprd92pr.wsu.edu/PSIGW/PeopleSoftServiceListeningConnector HTTP/1.1

                if (req.HaveResponse)
                {
                    string status = res.StatusCode.ToString();
                    string statusDescription = res.StatusDescription;
                    if (status == "OK")
                    {
                        XmlDocument xmlDoc = new XmlDocument(); /// create an xml document object. 
                        xmlDoc.Load(res.GetResponseStream()); /// load the XML document from the specified file. 
                        //   XmlNode root = xmlDoc.FirstChild;

                        //* Get elements. 
                        XmlNodeList wsuID = xmlDoc.GetElementsByTagName("WSUID");
                        XmlNodeList fName = xmlDoc.GetElementsByTagName("FirstName");
                        XmlNodeList LName = xmlDoc.GetElementsByTagName("LastName");
                        //XmlNodeList ExtSysId1 = xmlDoc.GetElementsByTagName("ExtSysId");
                        //XmlNodeList MidName = xmlDoc.GetElementsByTagName("MiddleName");
                        //XmlNodeList Nsuffix = xmlDoc.GetElementsByTagName("NameSuffix");
                        //XmlNodeList DateofBirth = xmlDoc.GetElementsByTagName("BirthDate");
                        //XmlNodeList NID = xmlDoc.GetElementsByTagName("NID");
                        //XmlNodeList SEX = xmlDoc.GetElementsByTagName("Sex");
                        //XmlNodeList MCountry = xmlDoc.GetElementsByTagName("Mail_Country");
                        //XmlNodeList MailAddr1 = xmlDoc.GetElementsByTagName("MailAddr1");
                        //XmlNodeList MailAddr2 = xmlDoc.GetElementsByTagName("MailAdrr2");
                        //XmlNodeList MailAddr3 = xmlDoc.GetElementsByTagName("MailAddr3");
                        //XmlNodeList MailAddr4 = xmlDoc.GetElementsByTagName("MailAddr4");
                        //XmlNodeList MailCity = xmlDoc.GetElementsByTagName("MailCity");
                        //XmlNodeList MailSt = xmlDoc.GetElementsByTagName("MailSt");
                        //XmlNodeList MailPostal = xmlDoc.GetElementsByTagName("MailPostal");
                        //XmlNodeList LocalPhone = xmlDoc.GetElementsByTagName("LocalPhone");
                        //XmlNodeList HomeCountry = xmlDoc.GetElementsByTagName("HomeCountry");
                        //XmlNodeList HomeAddr1 = xmlDoc.GetElementsByTagName("HomeAddr1");
                        //XmlNodeList HomeAdrr2 = xmlDoc.GetElementsByTagName("HomeAddr2");
                        //XmlNodeList HomeAddr3 = xmlDoc.GetElementsByTagName("HomeAddr3");
                        //XmlNodeList HomeAddr4 = xmlDoc.GetElementsByTagName("HomeAddr4");
                        //XmlNodeList HomeCity = xmlDoc.GetElementsByTagName("HomeCity");
                        //XmlNodeList HomeSt1 = xmlDoc.GetElementsByTagName("HomeSt");
                        //XmlNodeList HomePostal = xmlDoc.GetElementsByTagName("HomePostal");
                        //XmlNodeList HomePhone = xmlDoc.GetElementsByTagName("HomePhone");
                        //XmlNodeList MobilePhone = xmlDoc.GetElementsByTagName("MobilePhone");
                        //XmlNodeList OffcEmail1 = xmlDoc.GetElementsByTagName("OffcEmail");
                        //XmlNodeList PersEmail = xmlDoc.GetElementsByTagName("PersEmail");
                        //XmlNodeList PrefEmail = xmlDoc.GetElementsByTagName("PrefEmail");
                        //XmlNodeList VisaType = xmlDoc.GetElementsByTagName("VisaType");
                        //XmlNodeList VisaStat = xmlDoc.GetElementsByTagName("VisaStat");
                        //XmlNodeList VisaStatDescr = xmlDoc.GetElementsByTagName("VisaStatDescr");
                        //XmlNodeList ReturnCd1 = xmlDoc.GetElementsByTagName("ReturnCd");
                        //XmlNodeList ReturnMsg1 = xmlDoc.GetElementsByTagName("ReturnMsg");

                        int intTerm = 2183;
                        string type = "2183";

                        try
                        {
                            //strWSUID = strWSUID.Trim().PadLeft(9, '0');

                            /// use SoapUI 3.6 to get the correct soap for use with Peoplesoft.  
                            /// Copy it exactly here.
                            /// SoapUI provides a quick way to test the web service, too.
                            /// use the XML view here and the Raw view for the POST information below
                            /// Note:  as of Dec/2011 Version 4.1 of SoapUI will not work to test the WS, use 3.6.1
                            /// 
                            /// Also, need to obfuscate the username/password.
                            /// 
                            /// https://csprd92pr.wsu.edu/PSIGW/PeopleSoftServiceListeningConnector/W_GET_ADMIT_INFO.1.wsdl
                            /// 
                            string zzusisSoapStream = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:w=\"http://xmlns.oracle.com/Enterprise/Tools/schemas/W_ADMT_INFO_REQ.1\">";
                            zzusisSoapStream += "<soapenv:Header>";
                            zzusisSoapStream += "<wsse:Security soapenv:mustUnderstand=\"1\" xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">";
                            zzusisSoapStream += "<wsse:UsernameToken wsu:Id=\"UsernameToken-1\" xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\">";
                            zzusisSoapStream += "<wsse:Username>WSU_NSP</wsse:Username>";
                            zzusisSoapStream += "<wsse:Password Type=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText\">";
                            zzusisSoapStream += "zzusis$$NSP(</wsse:Password>";
                            zzusisSoapStream += "<wsse:Nonce EncodingType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\">TY/PXFoqeFZjwD1kwoAYfg==</wsse:Nonce>";
                            zzusisSoapStream += "<wsu:Created>" + DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss");
                            zzusisSoapStream += "</wsu:Created></wsse:UsernameToken></wsse:Security></soapenv:Header>";
                            zzusisSoapStream += "<soapenv:Body>";
                            zzusisSoapStream += "<w:W_ADMT_INFO_REQ>";
                            zzusisSoapStream += "<w:MsgData>";
                            zzusisSoapStream += "<!--Zero or more repetitions:-->";
                            zzusisSoapStream += "<w:Transaction>";
                            zzusisSoapStream += "<!--Optional:-->";
                            zzusisSoapStream += "<w:W_ADMT_INFO_REQ class=\"R\">";
                            zzusisSoapStream += "<!--You may enter the following 2 items in any order-->";
                            zzusisSoapStream += "<w:EMPLID IsChanged=\"?\">" + WSUID + "</w:EMPLID>";
                            zzusisSoapStream += "<!--Optional:-->";
                            zzusisSoapStream += "<w:TERM IsChanged=\"?\">" + intTerm + "</w:TERM>";
                            zzusisSoapStream += "</w:W_ADMT_INFO_REQ>";
                            zzusisSoapStream += "</w:Transaction>";
                            zzusisSoapStream += "</w:MsgData>";
                            zzusisSoapStream += "</w:W_ADMT_INFO_REQ>";
                            zzusisSoapStream += "</soapenv:Body>";
                            zzusisSoapStream += "</soapenv:Envelope>";

                            System.Net.HttpWebRequest zzusisReq;
                            if (type == "test")
                                zzusisReq = (HttpWebRequest)WebRequest.Create("https://csprd92pr.wsu.edu/PSIGW/PeopleSoftServiceListeningConnector");
                            else
                                zzusisReq = (HttpWebRequest)WebRequest.Create("https://csprd92pr.wsu.edu/PSIGW/PeopleSoftServiceListeningConnector");

                            byte[] zzusisReqBytes;
                            // reqBytes = StrToByteArray(soapStream);
                            zzusisReqBytes = Encoding.ASCII.GetBytes(zzusisSoapStream);
                            /// use the RAW information from SoapUI here
                            zzusisReq.Method = "POST";
                            zzusisReq.Headers.Add("Accept-Encoding", "gzip,deflate");
                            zzusisReq.ContentType = "text/xml;charset=UTF-8";
                            zzusisReq.Headers.Add("SOAPAction", "W_GET_ADMIT_INFO_SO.v1");
                            zzusisReq.UserAgent = "Jakarta Commons-HttpClient/3.1";
                            zzusisReq.AllowAutoRedirect = false;
                            zzusisReq.ContentLength = zzusisReqBytes.Length;

                            System.IO.Stream zzusisReqStream = zzusisReq.GetRequestStream();
                            zzusisReqStream.Write(zzusisReqBytes, 0, zzusisReqBytes.Length);

                            HttpWebResponse zzusisRes = (HttpWebResponse)zzusisReq.GetResponse();
                            // here is a sample response:
                            // POST https://csprd92pr.wsu.edu/PSIGW/PeopleSoftServiceListeningConnector HTTP/1.1
                            //POST https://csprd92pr.wsu.edu/PSIGW/PeopleSoftServiceListeningConnector HTTP/1.1

                            if (zzusisReq.HaveResponse)
                            {
                                string zzusisStatus = zzusisRes.StatusCode.ToString();
                                string zzusisStatusDescription = zzusisRes.StatusDescription;
                                if (zzusisStatus == "OK")
                                {
                                    XmlDocument zzusisXmlDoc = new XmlDocument(); /// create an xml document object. 
                                    zzusisXmlDoc.Load(zzusisRes.GetResponseStream()); /// load the XML document from the specified file. 

                                    //* Get elements. 
                                    //XmlNodeList admitType = zzusisXmlDoc.GetElementsByTagName("ADMIT_TYPE");
                                    //XmlNodeList admitTerm = zzusisXmlDoc.GetElementsByTagName("W_CLASS_LEVEL");
                                    XmlNodeList class_Level = zzusisXmlDoc.GetElementsByTagName("W_CLASS_LEVEL");

                                    Session["FirstName"] = fName[1].InnerText;
                                    Session["LastName"] = LName[1].InnerText;
                                    Session["W_CLASS_LEVEL"] = class_Level[1].InnerText;

                                    //Response.Redirect("ConfirmationGradFair.aspx?EventID=" + Request.QueryString["EventID"]);
                                }
                            }
                            else
                            {
                                lbError.Text = "status not ok";
                            }
                            reqStream.Close();

                        }
                        catch (Exception ex)
                        {
                            lbError.Text = ex.Message;
                        }

                        //Display 
                        if (fName[1].InnerText == null || fName[1].InnerText == "")
                        {
                            if (Session["IDFail"].ToString() == "Yes")
                            {
                                Response.Redirect("https://wsu.co1.qualtrics.com/SE/?SID=SV_9Nqmi1KYUYIj4I5");
                            }
                            else
                            {
                                IsApproved = false;
                                lbError.Text = "Unable to find the WSU ID.  Please Try again.";
                                lbError.Visible = true;
                                txtWSUID.Value = "";
                                Session["IDFail"] = "Yes";
                            }

                            

                        }
                        else
                        {
                            lbError.Visible = false;
                            Session["FirstName"] = fName[1].InnerText;
                            Session["LastName"] = LName[1].InnerText;
                            //  litWSUId.Text = wsuID[1].InnerText;
                            //   litLastName.Text = LName[1].InnerText;
                            //ExtSysid.Text = ExtSysId1[1].InnerText;
                            //     litMName.Text = MidName[1].InnerText;
                            //NameSuffix.Text = Nsuffix[1].InnerText;
                            //Sex.Text = SEX[1].InnerText;
                            //lNID.Text = NID[1].InnerText;
                            //BirthDate.Text = DateofBirth[1].InnerText;
                            //Mail_Country.Text = MCountry[1].InnerText;
                            //MailAdd1.Text = MailAddr1[1].InnerText;
                            //MailAdd2.Text = MailAddr2[1].InnerText;
                            //MailAdd3.Text = MailAddr3[1].InnerText;
                            //MailAdd4.Text = MailAddr4[1].InnerText;
                            //lMailCity.Text = MailCity[1].InnerText;
                            //lMailPostal.Text = MailPostal[1].InnerText;
                            //lMialSt.Text = MailSt[1].InnerText;
                            //lLocalPhone.Text = LocalPhone[1].InnerText;
                            //lHomeCountry.Text = HomeCountry[1].InnerText;
                            //lHomeAdd1.Text = HomeAddr1[1].InnerText;
                            //lHomeAdd2.Text = HomeAdrr2[1].InnerText;
                            //lHomeAdd3.Text = HomeAddr3[1].InnerText;
                            //lHomeAdd4.Text = HomeAddr4[1].InnerText;
                            //lhomecity.Text = HomeCity[1].InnerText;
                            //lHomePostal.Text = HomePostal[1].InnerText;
                            //lHomeSt.Text = HomeSt1[1].InnerText;
                            //lHomePhone.Text = HomePhone[1].InnerText;
                            //lMoblilePhone.Text = MobilePhone[1].InnerText;
                            //lVisaState.Text = VisaStat[1].InnerText;
                            //lVisaType.Text = VisaType[1].InnerText;
                            //lOfficeEmail.Text = OffcEmail1[1].InnerText;
                            //lpersonalemail.Text = PersEmail[1].InnerText;
                            //    litStudentEmail.Text = PrefEmail[1].InnerText;
                            //lReturncd.Text = ReturnCd1[1].InnerText;
                            //lreturnmsg.Text = ReturnMsg1[1].InnerText;
                        }
                    }
                    else
                    {
                        IsApproved = false;
                        lbError.Text = "Http status not OK" + statusDescription;
                        lbError.Visible = true;
                        txtWSUID.Value = "";
                    }

                    reqStream.Close();
                }
            }
            catch
            {
                IsApproved = false;
                lbError.Text = "We are encountering technical difficulties. Please try again later.";
                lbError.Visible = true;
                txtWSUID.Value = "";
            }          

            CheckIn newCheckIn = new CheckIn();

            newCheckIn.CIEventID = eventID = 1107;
                //Convert.ToInt32(Request.QueryString["EventID"].ToString());
            newCheckIn.CIWSUID = WSUID;
            newCheckIn.CIFirstName = Session["FirstName"].ToString();
            newCheckIn.CILastName = Session["LastName"].ToString();
            newCheckIn.CIApproved = IsApproved;

            if (!IsApproved)
                newCheckIn.CIErrorMessage = lbError.Text;
            else
                newCheckIn.CIErrorMessage = "";
            newCheckIn.CISubmitDate = DateTime.Now;


            kiosk.CheckIns.InsertOnSubmit(newCheckIn);
            kiosk.SubmitChanges();

            // Display check in confirmation
            if (lbError.Visible == false)
            {
                if (Session["IDFail"].ToString() == "Yes")
                {
                    btnNotStudent.Visible = false;
                    btnNotStudent2ndX.Visible = true;
                }
                else
                {
                }

                pnlCheckIn.Visible = false;
             
                litFirst.Text = "<div class='auto-style2'>" + Session["FirstName"].ToString();                  
                litLast.Text = Session["LastName"].ToString() + "</div>";                      
                pnlCheckName.Visible = true;
                             
            }
        }

        protected void btnFinalSubmit_Click(object sender, EventArgs e)
        { //vancouver link:  https://wsu.co1.qualtrics.com/SE/?SID=SV_2nUI4s7SxCovJA1
           //Response.Redirect("ConfirmationGradFair.aspx?EventID=" + Request.QueryString["EventID"]);
            Response.Redirect("https://wsu.co1.qualtrics.com/SE/?SID=SV_2nUI4s7SxCovJA1&WSUID=" + txtWSUID.Value);

            // ASCC Link Response.Redirect("https://wsu.co1.qualtrics.com/SE/?SID=SV_9Nqmi1KYUYIj4I5&WSUID=" + txtWSUID.Value + "&W_CLASS_LEVEL=" + Session["W_CLASS_LEVEL"].ToString());
        }

        protected void btnNotStudent_Click(object sender, EventArgs e)
        {
            pnlCheckName.Visible = false;
            pnlCheckIn.Visible = true;
            Session["IDFail"] = "Yes";
        }

        protected void btnNotStudent2ndX_Click(object sender, EventArgs e)
        {
            Response.Redirect("https://wsu.co1.qualtrics.com/SE/?SID=SV_2nUI4s7SxCovJA1");
        }
    }
}