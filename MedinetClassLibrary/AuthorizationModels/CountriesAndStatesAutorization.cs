using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;

namespace MedinetClassLibrary.AuthorizationModels
{
    public class CountriesAndStatesAutorization
    {
        private User userLogged;

        public CountriesAndStatesAutorization(User userLogged)
        {
            this.userLogged = userLogged;
        }

        public bool isAuthorizated()
        {
            return userLogged.Company.CompaniesType.Name == "Owner";
        }
    }
}
