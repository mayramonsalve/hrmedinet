using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{

    public class FunctionalOrganizationTypeViewModel {

        public FunctionalOrganizationType type { get; private set; }
        public SelectList typesList { get; private set; }

        public FunctionalOrganizationTypeViewModel(FunctionalOrganizationType type, SelectList typesList)
        {
            this.type= type;
            this.typesList = typesList;
        }
    }
}
    
