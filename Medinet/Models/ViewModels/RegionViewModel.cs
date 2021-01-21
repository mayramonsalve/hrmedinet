using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{
    public class RegionViewModel
    {
        public Region region { get; private set; }

        public RegionViewModel(Region region)
        {
            this.region = region;
        }
    }
}
