using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;

namespace Medinet.Models.ViewModels
{
    public class CountryViewModel
    {
        public Country country { get; private set; }

        public CountryViewModel(Country country )
        {
            this.country = country;
        }
    }
}
