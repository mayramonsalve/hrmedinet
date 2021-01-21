using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Services;

namespace MedinetClassLibrary.AuthorizationModels
{
    public class SharedHrAuthorization
    {
        private User userLogged;
        private Company company;
        private bool own;

        public SharedHrAuthorization(User userLogged, Company company, bool own)
        {
            this.userLogged = userLogged;
            this.company = company;
            this.own = own;
        }

        public bool isAuthorizated()
        {
            if (own)
                return true; //validateOwn()
            else
                return validateOwn() || validateCompanies();
        }

        private bool validateCompanies()
        {
            return company.CompanyAssociated_Id == userLogged.Company_Id
                    && company.CompaniesType.Name == "Customer";
        }

        private bool validateOwn()
        {
            return company.Id == userLogged.Company_Id;
        }
    }
}
