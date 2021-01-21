using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;

namespace MedinetClassLibrary.AuthorizationModels
{
    public class ReportsAuthorization
    {
        private User userLogged;
        private Company company;
        private string report;

        public ReportsAuthorization(User userLogged, string report, Company company)
        {
            this.userLogged = userLogged;
            this.company = company;
            this.report = report;
        }

        public bool isAuthorizated()
        {
            if (userLogged.Role.Name == "Administrator")
            {
                if (report == "TestByCompanyReport")
                    return true;
            }
            else
            {
                if (userLogged.Role.Name == "HRAdministrator")
                {
                    if (report == "QuestionnaireReport")
                        return true;
                }
                else
                {
                    if (userLogged.Role.Name == "HRCompany")
                        if (report == "EvaluationReport" || report == "QuestionnaireReport")
                            //if (userLogged.Company_Id == company.Id)
                            return true;
                }
            }
            return false;
        }

        private bool validateCompanies()
        {
            return company.CompanyAssociated_Id == userLogged.Company_Id
                    && company.CompaniesType.Name == "Customer";
        }

    }
}
