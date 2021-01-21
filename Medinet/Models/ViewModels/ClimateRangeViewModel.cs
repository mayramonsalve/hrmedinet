using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{
    public class ClimateRangeViewModel
    {
        public ClimateRange ClimateRange { get; private set; }
        public SelectList ClimateScalesList { get; private set; }

        public ClimateRangeViewModel(ClimateRange ClimateRange, SelectList ClimateScalesList)
        {
            this.ClimateRange = ClimateRange;
            this.ClimateScalesList = ClimateScalesList;
        }

    }
}
