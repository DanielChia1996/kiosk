using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventCheckIn
{
    public partial class workshopSearch : System.Web.UI.Page
    {
        EventCheckInDataClassesDataContext db = new EventCheckInDataClassesDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void workshopLinqDS_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            string queryString = Request.QueryString["SearchString"];

            var query = from workshops in db.workshops
                        where (workshops.IsVisible == true) && workshops.workshopName.Contains(queryString)
                        orderby workshops.workshopDate ascending
                        select workshops;
            e.Result = query;
        }
    }
}