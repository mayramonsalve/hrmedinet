using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{
    public class OptionViewModel
    {
        public Option option { get; private set; }
        public SelectList questionnairesList { get; private set; }

        public OptionViewModel(Option option , SelectList questionnairesList)
        {
            this.option = option;
            this.questionnairesList = questionnairesList;
        }

    }
}
