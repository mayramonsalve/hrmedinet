using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetClassLibrary.Models.ReportsModels
{
    public class CompanyReport
    {
            public string Name { get; set; }
            public string CompanyName { get; set; }
            public string OwnerName { get; set; }
            public byte[] OwnerImage { get; set; }
            public string Address { get; set; } 
            public string Contact {get;set;}
            public int Id { get; set; }
            public string Number { get; set; }
    }
}
