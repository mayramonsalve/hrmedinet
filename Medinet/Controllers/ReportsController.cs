using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.Models;
using Medinet.Models.ViewModels;
using MedinetClassLibrary.AuthorizationModels;

namespace Medinet.Controllers
{
    [HandleError]
    public class ReportsController : Controller
    {
        private ReportViewModel _reportViewModel;

        public ReportsController(){ }
        
        [Authorize(Roles = "Administrator")]
        public ActionResult TestReport()
        {
                return Redirect("/Reports/TestByCompanyReport.aspx");
        }

        private bool GetAuthorization(string report)
        {
            return new ReportsAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                            report, new UsersServices().GetByUserName(User.Identity.Name).Company).isAuthorizated();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult QuestionnaireReport()
        {
                return Redirect("/Reports/QuestionnaireReport.aspx");
        }

        [Authorize(Roles = "HRCompany, CompanyManager")]
        public ActionResult EvaluationReport()
        {
                return Redirect("/Reports/EvaluationReport.aspx");
        }
    }
}
