using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetClassLibrary.Models.ReportsModels
{
    public class HRCompanyQuestionnaireReport
    {
        public string Name { get; set; }
        public string Template { get; set; }
        public int Categories { get; set; }
        public int Questions { get; set; }
        public string CompanyName { get; set; }
        public byte[] CompanyImage { get; set; }
        public string OwnerName { get; set; }
        public byte[] OwnerImage { get; set; }
    }
}
