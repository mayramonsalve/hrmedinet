using System;
using System.Security.Principal;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models.ReportsModels;
using System.IO;
using System.Web;
using MedinetClassLibrary.Models;

namespace MedinetClassLibrary.Services.Reports
{
    public class TestsByCompaniesReportsServices
    {
        private User userLogged = new UsersServices().GetByUserName(HttpContext.Current.User.Identity.Name);

        public IQueryable<TestByCompanyReport> GetTests(int? company_id, int? questionnaire_id)
        {
            if (userLogged.Company.CompaniesType.Name == "Owner")
            {
                var testsForOwner = from t in new TestsServices().GetAllRecords()
                                    select new TestByCompanyReport
                                    {
                                        Name = t.Name,
                                        StartDate = t.StartDate,
                                        EndDate = t.EndDate,
                                        Code = t.Code,
                                        EvaluationNumber = t.EvaluationNumber,
                                        CompanyName = t.Company.Name,
                                        CompanyID = t.Company_Id,
                                        QuestionnaireName = t.Questionnaire.Name,
                                        QuestionnaireID = t.Questionnaire_Id.Value
                                    };

                if (company_id.HasValue)
                {
                    testsForOwner = testsForOwner.Where(a => a.CompanyID == company_id);
                }

                if (questionnaire_id.HasValue)
                {
                    testsForOwner = testsForOwner.Where(a => a.QuestionnaireID == questionnaire_id);
                }

                return testsForOwner;
            }
            else
            {
                var testsForAssociated = from t in new TestsServices().GetTestsByAssociated(userLogged.Company_Id)
                                    select new TestByCompanyReport
                                    {
                                        Name = t.Name,
                                        StartDate = t.StartDate,
                                        EndDate = t.EndDate,
                                        Code = t.Code,
                                        EvaluationNumber = t.EvaluationNumber,
                                        CompanyName = t.Company.Name,
                                        CompanyID = t.Company_Id,
                                        QuestionnaireName = t.Questionnaire.Name,
                                        QuestionnaireID = t.Questionnaire_Id.Value
                                    };

                if (company_id.HasValue)
                {
                    testsForAssociated = testsForAssociated.Where(a => a.CompanyID == company_id);
                }

                if (questionnaire_id.HasValue)
                {
                    testsForAssociated = testsForAssociated.Where(a => a.QuestionnaireID == questionnaire_id);
                }

                return testsForAssociated;
            }
        }

        public IQueryable<HeaderDataByCompanyReport> GetHeaderData(int? company_id, int? questionnaire_id)
        {
            User userLogged = new UsersServices().GetByUserName(HttpContext.Current.User.Identity.Name);
            List<HeaderDataByCompanyReport> headerData = new List<HeaderDataByCompanyReport>();
            headerData.Add(new HeaderDataByCompanyReport(userLogged.Company.Name,
                                        ImageConversion(userLogged.Company.Name, userLogged.Company.CompaniesType.Name),
                                        userLogged.Company.CompanyAssociated == null ? " " : userLogged.Company.CompanyAssociated.Name,
                                        ImageConversion(userLogged.Company.CompanyAssociated == null ? "" : userLogged.Company.CompanyAssociated.Name, userLogged.Company.CompanyAssociated == null ? "Empty" : "Owner"),
                                        company_id.HasValue ? new CompaniesServices().GetById(company_id.Value).Name : ViewRes.Reports.Files.AllF,
                                        questionnaire_id.HasValue ? new QuestionnairesServices().GetById(questionnaire_id.Value).Name : ViewRes.Reports.Files.AllM));
            return headerData.AsQueryable();
        }

        public IQueryable<CompaniesForTestByCompanyReport> GetCompanies()
        {
            var companiesForOwner = from c in new CompaniesServices().GetAllRecords().Where(c => c.CompaniesType.Name == "Customer" && c.Tests.Count > 0)
                            select new CompaniesForTestByCompanyReport
                            {
                                CompanyID = c.Id,
                                CompanyName = c.Name
                            };
            var companiesForAssociated = from c in new CompaniesServices().GetCustomersByAssociated(userLogged.Company_Id).Where(c => c.CompaniesType.Name == "Customer" && c.Tests.Count > 0)
                                    select new CompaniesForTestByCompanyReport
                                    {
                                        CompanyID = c.Id,
                                        CompanyName = c.Name
                                    };

            if (userLogged.Company.CompaniesType.Name == "Owner")
                return companiesForOwner;
            else
                return companiesForAssociated;

        }

        public IQueryable<QuestionnairesForTestByCompanyReport> GetQuestionnaires()
        {
            var questionnairesForOwner = from q in new QuestionnairesServices().GetAllRecords().Where(q => q.Tests.Count > 0)
                            select new QuestionnairesForTestByCompanyReport
                            {
                                QuestionnaireID = q.Id,
                                QuestionnaireName = q.Name
                            };
            var questionnairesForAssociated = from q in new QuestionnairesServices().GetQuestionnairesForAssociated(userLogged.Company_Id).Where(q => q.Tests.Count > 0)
                                         select new QuestionnairesForTestByCompanyReport
                                         {
                                             QuestionnaireID = q.Id,
                                             QuestionnaireName = q.Name
                                         };

            if (userLogged.Company.CompaniesType.Name == "Owner")
                return questionnairesForOwner;
            else
                return questionnairesForAssociated;
        }

        private byte[] ImageConversion(string imageName, string companyType)
        {
            string path = HttpContext.Current.Server.MapPath("..");
            try
            {
                //Declaramos fs para poder abrir la imagen.
                FileStream fs = new FileStream(path + "/Content/Images/Companies/" + imageName, FileMode.Open, FileAccess.Read);
                return InitializeImage(fs);
            }
            catch
            {
                FileStream fs = new FileStream(path + "/Content/Images/Companies/" + companyType + "Image.png", FileMode.Open, FileAccess.Read);
                return InitializeImage(fs);
            }
        }  

            // Declaramos un lector binario para pasar la imagen
            // a bytes
        private static byte[] InitializeImage(FileStream fs) {  
            BinaryReader br = new BinaryReader(fs);
            byte[] image = new byte[(int)fs.Length];
            br.Read(image, 0, (int)fs.Length);
            br.Close();
            fs.Close();
            return image;
        }

    }
}
