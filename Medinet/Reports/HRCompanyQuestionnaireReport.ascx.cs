using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Medinet.Reports
{
    public partial class HRCompanyQuestionnaireReport1 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLink1.Text = ViewRes.Views.Shared.Shared.GoHome;
            if (!IsUrl())
                Response.Redirect("/Home/Index");
            else
                ReportViewer1.LocalReport.ReportPath = ViewRes.Reports.Files.HRCompanyQuestionnaire;
        }

        //The method is used to check whether the page is opend by typing the url in browser 
        bool IsUrl()
        {
            string str1 = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            string str2 = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            return ((str1 != null) && (str1.IndexOf(str2) == 7));
        } 
    }
}