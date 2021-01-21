using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MedinetClassLibrary.Models.ReportsModels
{
    public class TestByCompanyReport
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Code { get; set; }
        public int EvaluationNumber { get; set; }
        public string CompanyName { get; set; }
        public int CompanyID { get; set; }
        public string QuestionnaireName { get; set; }
        public int QuestionnaireID { get; set; }
    }

    public class HeaderDataByCompanyReport
    {
        public string AssociatedName { get; set; }
        public byte[] AssociatedImage { get; set; }
        public string OwnerName { get; set; }
        public byte[] OwnerImage { get; set; }
        public string SelectedCompanyName { get; set; }
        public string SelectedQuestionnaireName { get; set; }

        public HeaderDataByCompanyReport(string asName, byte[] asImage, string owName, byte[] owImage, string seCoNa, string seQuNa)
        {
            AssociatedName = asName;
            AssociatedImage = asImage;
            OwnerName = owName;
            OwnerImage = owImage;
            SelectedCompanyName = seCoNa;
            SelectedQuestionnaireName = seQuNa;
        }
    }

    public class CompaniesForTestByCompanyReport
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
    }

    public class QuestionnairesForTestByCompanyReport
    {
        public int QuestionnaireID { get; set; }
        public string QuestionnaireName { get; set; }
    }
}
