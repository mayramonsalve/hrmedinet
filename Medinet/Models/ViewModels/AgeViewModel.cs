using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{

    public class AgeViewModel {

        public Age age { get; private set; }
     
        public AgeViewModel (Age age){
            this.age= age;
        }
    }
}
    
