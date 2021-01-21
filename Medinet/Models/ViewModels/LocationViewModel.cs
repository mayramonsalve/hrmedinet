using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{
    public class LocationViewModel
    {
        public Location location { get; private set; }
        public SelectList statesList { get; private set; }
        public SelectList countriesList { get; private set; }
        public SelectList regionsList { get; private set; }

        public LocationViewModel(Location location, SelectList statesList, SelectList countriesList, SelectList regionsList)
        {
            this.location = location;
            this.statesList = statesList;
            this.countriesList = countriesList;
            this.regionsList = regionsList;
        }
    }
}
