using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{

    public class InstructionLevelViewModel {

        public InstructionLevel level { get; private set; }

        public InstructionLevelViewModel(InstructionLevel level)
        {
            this.level = level;
        }
    }
}
    
