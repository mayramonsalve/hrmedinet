using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetClassLibrary.Models.ReportsModels
{
    public class EvaluationReport
    {
        public int TestID { get; set; }
        public string TestName { get; set; }
        public string SelectedTestName { get; set; }
        public DateTime Date { get; set; }
        public string IpAddress { get; set; }
        public string Sex { get; set; }
        public string Age { get; set; }
        public string InstructionLevel { get; set; }
        public string PositionLevel { get; set; }
        public string Seniority { get; set; }
        public string Location { get; set; }
        public string CompanyName { get; set; }
        public byte[] CompanyImage { get; set; }
        public string OwnerName { get; set; }
        public byte[] OwnerImage { get; set; }
    }

    public class TestsForEvaluationReport
    {
        public int TestID { get; set; }
        public string TestName { get; set; }
    }
}
