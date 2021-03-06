﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Medinet.Reports
{
    public partial class EvaluationReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = ViewRes.Reports.Files.Test;
            ListItem Select = new ListItem(ViewRes.Scripts.Shared.ShowAll,"",true);
            if(!DropDownList1.Items.Contains(Select))
                DropDownList1.Items.Add(Select);
            HyperLink1.Text = ViewRes.Views.Shared.Shared.GoHome;
            //title.Text = ViewRes.Views.Shared.Shared.GenericTitle + " " + ViewRes.Reports.Files.EvaluationReportTitle;
            if (!IsUrl())
                Response.Redirect("/Home/Index");
            else
                ReportViewer1.LocalReport.ReportPath = ViewRes.Reports.Files.Evaluation;
        }

        //The method is used to check whether the page is opend by typing the url in browser 
        bool IsUrl()
        {
            string str1 = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            string str2 = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            return ((str1 != null) && (str1.IndexOf(str2) == 7));
        } 

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportViewer1.LocalReport.Refresh();
        }
    }
}