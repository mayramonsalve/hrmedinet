using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{

    public class SeniorityViewModel {

        public Seniority seniority { get; private set; }
     
        public SeniorityViewModel (Seniority seniority){
            this.seniority= seniority;
        }
    }
}
    
