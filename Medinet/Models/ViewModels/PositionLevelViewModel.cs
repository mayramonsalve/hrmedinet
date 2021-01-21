using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{

    public class PositionLevelViewModel {

        public PositionLevel level { get; private set; }

        public PositionLevelViewModel(PositionLevel level)
        {
            this.level = level;
        }
    }
}
    
