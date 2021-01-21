using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Services;

namespace MedinetClassLibrary.AuthorizationModels
{
    public class CompaniesAuthorization
    {
        private User userLogged;
        private Company company;
        private bool create;

        public CompaniesAuthorization(User userLogged, Company company, bool create)
        {
            this.userLogged = userLogged;
            this.company = company;
            this.create = create;
        }

        public bool isAuthorizated()
        {
            if (userLogged.Company.CompaniesType.Name == "Owner")
            {
                if (!create)
                    if (validateCompanies())
                        return true;
                return true;
            }
            else
            {
                if (create)
                {
                    if (company.CompanyType_Id == 3)
                        return true;
                }
                else
                    if (validateCompanies())
                        return true;
            }
            return false;
        }

        private bool validateCompanies()
        {
            return company.CompanyAssociated_Id == userLogged.Company_Id
                    || company.Id == userLogged.Company_Id;
        }

    }
}
