using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Services;

namespace MedinetClassLibrary.AuthorizationModels
{
    public class UsersAuthorization
    {
        private User userLogged;
        private string role;
        private Company company;

        public UsersAuthorization(User userLogged, int role_id, Company company)
        {
            this.userLogged = userLogged;
            this.role = new RolesServices().GetById(role_id).Name;
            this.company = company;
        }

        public bool isAuthorizated()
        {
            if (userLogged.Role.Name == "Administrator")
            {
                if (userLogged.Company.CompaniesType.Name == "Owner")
                {
                    if (validateCompanies())
                        return true;
                }
                else
                    if (company.CompaniesType.Name != "Owner")
                        if (validateCompanies())
                            return true;
            }
            else
                if (role != "Administrator" && role != "HRAdministrator")
                    return validateOwn();
            return false;
        }

        private bool validateOwn()
        {
            return company.Id == userLogged.Company_Id;
        }

        private bool validateCompanies()
        {
            return company.CompanyAssociated_Id == userLogged.Company_Id || validateOwn();
        }

    }
}
