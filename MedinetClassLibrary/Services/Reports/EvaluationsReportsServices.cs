using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models.ReportsModels;
using System.Web;
using System.IO;
using MedinetClassLibrary.Models;

namespace MedinetClassLibrary.Services.Reports
{
    public class EvaluationsReportsServices
    {
        private User userLogged = new UsersServices().GetByUserName(HttpContext.Current.User.Identity.Name);

        public IQueryable<EvaluationReport> GetEvaluations(int? test_id)
        {
            var evaluations = from e in new EvaluationsServices().GetEvaluationsByCompany(userLogged.Company_Id).Where(t => t.Test.CurrentEvaluations >= t.Test.MinimumPeople)
                              select new EvaluationReport
                                 {
                                     TestID = e.Test_Id,
                                     TestName = e.Test.Name,
                                     SelectedTestName = test_id.HasValue ? e.Test.Name : ViewRes.Reports.Files.AllF,
                                     Date = e.CreationDate,
                                     IpAddress = e.IpAddress,
                                     Sex =  GetGenre(e.Sex),
                                     Age = e.Age.Name,
                                     InstructionLevel = e.InstructionLevel.Name,
                                     PositionLevel = e.PositionLevel.Name,
                                     Seniority = e.Seniority.Name,
                                     Location = e.Location.Name,
                                     CompanyName = userLogged.Company.Name,
                                     CompanyImage = ImageConversion(e.Test.Company.Image, "Customer"),
                                     OwnerName = userLogged.Company.CompanyAssociated.Name,
                                     OwnerImage = ImageConversion(userLogged.Company.CompanyAssociated.Image, userLogged.Company.CompanyAssociated.CompaniesType.Name)
                                 };
            
            if (test_id.HasValue)
            {
                evaluations = evaluations.Where(a => a.TestID == test_id);
            }

            return evaluations;
        }

        private string GetGenre(string genre)
        {
            if (HttpContext.Current.Session["Culture"].ToString() == "en")
                return genre;
            else
            {
                if (genre == "Female")
                    return "Femenino";
                else
                    return "Masculino";
            }
        }

        public IQueryable<TestsForEvaluationReport> GetTests()
        {
            var tests = from t in new TestsServices().GetByCompany(userLogged.Company_Id).Where(t => t.CurrentEvaluations >= t.MinimumPeople)//.Where(t => t.CurrentEvaluations > 0)
                              select new TestsForEvaluationReport
                              {
                                  TestID = t.Id,
                                  TestName = t.Name
                              };

            return tests;
        }

        private byte[] ImageConversion(string imageName, string companyType)
        {
            try
            {
                //Declaramos fs para poder abrir la imagen.
                string path = HttpContext.Current.Server.MapPath("..");
                FileStream fs = new FileStream(path + "/Content/Images/Companies/" + imageName, FileMode.Open, FileAccess.Read);
                byte[] image = InitializeImage(fs);
                return image;
            }
            catch
            {
                string path = HttpContext.Current.Server.MapPath("..");
                FileStream fs = new FileStream(path + "/Content/Images/Companies/" + companyType + "Image.png", FileMode.Open, FileAccess.Read);
                byte[] image = InitializeImage(fs);
                return image;
            }
            // Declaramos un lector binario para pasar la imagen
            // a bytes

        }

        private static byte[] InitializeImage(FileStream fs)
        {
            BinaryReader br = new BinaryReader(fs);
            byte[] image = new byte[(int)fs.Length];
            br.Read(image, 0, (int)fs.Length);
            br.Close();
            fs.Close();
            return image;
        }

    }
}
