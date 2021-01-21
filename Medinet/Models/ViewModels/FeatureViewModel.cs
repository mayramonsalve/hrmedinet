using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{

    public class FeatureViewModel {

        public Feature feature { get; private set; }
        public SelectList typesList { get; private set; }
        public double average { get; private set; }

        public FeatureViewModel(Feature feature, SelectList typesList)
        {
            this.feature = feature;
            this.typesList = typesList;
            if (feature.Id!=0)
                this.average = feature.Scores.Select(s => s.Value).Average();
        }
    }
}
    
