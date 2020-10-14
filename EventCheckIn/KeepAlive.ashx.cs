using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCheckIn
{
    /// <summary>
    /// Summary description for KeepAlive
    /// </summary>
    public class KeepAlive : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Session["KeepSessionAlive"] = DateTime.Now.ToString();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}