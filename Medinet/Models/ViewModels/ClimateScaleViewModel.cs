using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{
    public class ClimateScaleViewModel
    {
        public ClimateScale climateScale { get; private set; }

        public ClimateScaleViewModel(ClimateScale climateScale)
        {
            this.climateScale = climateScale;
        }

    }
}
