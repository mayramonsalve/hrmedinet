using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{
    public class StateViewModel
    {
        public State state { get; private set; }
        public SelectList countriesList { get; private set; }

        public StateViewModel(State state, SelectList countriesList)
        {
            this.state = state;
            this.countriesList = countriesList;
        }
    }
}