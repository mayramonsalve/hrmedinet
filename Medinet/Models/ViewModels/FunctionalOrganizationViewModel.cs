using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{

    public class FunctionalOrganizationViewModel {

        public FunctionalOrganization functionalOrganization { get; private set; }
        public SelectList typesList { get; private set; }
        public SelectList typesParentList { get; private set; }
        public SelectList fosParentList { get; private set; }

        public FunctionalOrganizationViewModel(FunctionalOrganization functionalOrganization, SelectList typesList,
                                                SelectList typesParentList, SelectList fosParentList)
        {
            this.functionalOrganization = functionalOrganization;
            this.typesList = typesList;
            this.typesParentList = typesParentList;
            this.fosParentList = fosParentList;
        }
    }
}
    
