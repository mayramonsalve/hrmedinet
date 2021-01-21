using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;
using MedinetClassLibrary.Services;

namespace Medinet.Models.ViewModels
{
    public class CompanyViewModel
    {
        public Company company { get; private set; }
        public SelectList companiesTypesList { get; private set; }
        public SelectList companySectorsList { get; private set; }
        public SelectList countryList { get; private set; }
        public SelectList languageList { get; private set; }
        public string companyType { get; private set; }

        public CompanyViewModel(Company company, SelectList companiesTypesList, SelectList companySectorsList,
                                string companyType)
        {
            this.company = company;
            this.companiesTypesList = companiesTypesList;
            this.companyType = companyType;
            this.companySectorsList = companySectorsList;
            this.countryList = new SelectList(new CountriesServices().GetCountriesForDropDownList(), "Key", "Value");
            this.languageList = new SelectList(GetLanguages(), "Key", "Value");
        }

        private Dictionary<int, string> GetLanguages()
        {
            Dictionary<int, string> languages = new Dictionary<int, string>();
            languages.Add(1, "Español");
            languages.Add(2, "English");
            return languages;
        }
    }
}
