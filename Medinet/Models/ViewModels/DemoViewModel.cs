using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{

    public class DemoViewModel {
        public Demo demo { get; private set; }
        public string company { get; private set; }
        public SelectList countryList { get; private set; }
        public SelectList languageList { get; private set; }
        public string language { get; private set; }
        public string country { get; private set; }
        public int weeks { get; private set; }
        public int employees { get; private set; }
        public int evaluations { get; private set; }
        public string code { get; private set; }
        public string email { get; private set; }
        public string manager { get; private set; }
        public string password { get; private set; }
        public string startDate { get; private set; }
        public string endDate { get; private set; }

        public DemoViewModel(Demo demo)
        {
            this.demo = demo;
        }

        //Crear
        public DemoViewModel(SelectList countryList, SelectList languageList, int weeks, int employees)
        {
            this.countryList = countryList;
            this.languageList = languageList;
            this.weeks = weeks;
            this.employees = employees;
        }

        //Editar
        public DemoViewModel(int weeks, int employees)
        {
            this.weeks = weeks;
            this.employees = employees;
        }

        //Detalles
        public DemoViewModel(string company, string country, string language, int weeks, int employees,
                            int evaluations, string code, string manager, string password, string startDate, string endDate)
        {
            this.company = company;
            this.country = country;
            this.language = language;
            this.weeks = weeks;
            this.employees = employees;
            this.evaluations = evaluations;
            this.code = code;
            this.manager = manager;
            this.password = password;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public DemoViewModel(string company, int weeks, int employees, int evaluations, 
            string code, string email, string manager, string password, string startDate, string endDate)
        {
            this.company = company;
            this.email = email;
            this.weeks = weeks;
            this.employees = employees;
            this.evaluations = evaluations;
            this.code = code;
            this.manager = manager;
            this.password = password;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}
    
