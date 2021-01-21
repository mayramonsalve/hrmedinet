using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Medinet.Reports
{
    public partial class QuestionnaireReport1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsUrl())
            {
                Response.Redirect("/Home/Index");
            }
            else
            {
                if (User.IsInRole("HRAdministrator"))
                {
                    HRCompanyQuestionnaireReport1.Visible = false;
                }
                else
                {
                    HRAdministratorQuestionnaireReport1.Visible = false;
                }
            }
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