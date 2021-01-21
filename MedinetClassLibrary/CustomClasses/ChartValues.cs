using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetClassLibrary.CustomClasses
{
    public class ChartValues
    {
        public string label { get; set; }
        public decimal value { get; set; }

        public ChartValues(string label, decimal value)
        {
            this.label = label;
            this.value = value;
        }
    }
}
