using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{

    public class ContactUsViewModel {
        public ContactUs cont { get; private set; }
        public LogOnModel logOn { get; private set; }
        public SelectList countries { get; private set; } 

        public ContactUsViewModel(ContactUs cont, SelectList countries)
        {
            this.cont= cont;
            this.logOn = new LogOnModel();
            this.countries = countries;
        }
    }
}
    
