using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Services;

namespace MedinetClassLibrary.AuthorizationModels
{
    public class SharedAdminAuthorization
    {
        private User userLogged;
        private Company company;

        public SharedAdminAuthorization(User userLogged, Company company)
        {
            this.userLogged = userLogged;
            this.company = company;
        }
        public bool isAuthorizated()
        {
            return company.Id == userLogged.Company_Id;
        }
    }
}