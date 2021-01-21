using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;
using MedinetClassLibrary.Services;

namespace Medinet.Models.ViewModels
{
    public class TestViewModel
    {
        public Test test { get; private set; }
        public SelectList questionnairesList { get; private set; }
        public SelectList companiesList { get; private set; }
        public string userRole;
        public string newCode { get; set; }
        public SelectList CL { get; set; }
        public SelectList SE { get; set; }
        public SelectList climateScalesList { get; private set; }
        public SelectList testsList { get; private set; }
        public MultiSelectList demographicsList { get; private set; }

        public TestViewModel(Test test, SelectList questionnairesList, SelectList companiesList, string userRole, SelectList CL,
                                SelectList SE, SelectList climateScalesList, MultiSelectList demographicsList, SelectList testsList)
        {
            this.test = test;
            this.testsList = testsList;
            this.questionnairesList = questionnairesList;
            this.companiesList = companiesList;
            this.userRole = userRole;
            this.CL = CL;
            this.SE = SE;
            this.climateScalesList = climateScalesList;
            this.demographicsList = demographicsList;
        }

        public TestViewModel(Test test)
        {
            this.test = test;
        }

        public TestViewModel(string code)
        {
            this.newCode = code;
        }

    }
}
