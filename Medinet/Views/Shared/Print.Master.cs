using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Medinet.Views.Shared
{
    public partial class Print : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Browser["IsMobileDevice"] == "true")
            {
                Response.Redirect("www.unet.edu.ve");
            }
            else
            {
                Response.Redirect("www.ramar.com.ve");
            }
        }

    }
}