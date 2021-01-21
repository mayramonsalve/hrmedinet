using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{
    public class UserViewModel
    {

        public User user { get; private set; }
        public SelectList companiesList { get; private set; }
        public SelectList locationsList { get; private set; }
        public SelectList rolesList { get; private set; }
        public string companyType { get; private set; }


        public UserViewModel(User user, SelectList companiesList, SelectList locationsList, SelectList rolesList, string companyType)
        {
            this.user = user;
            this.companiesList = companiesList;
            this.locationsList = locationsList;
            this.rolesList = rolesList;
            this.companyType = companyType;
        }
    }
}
