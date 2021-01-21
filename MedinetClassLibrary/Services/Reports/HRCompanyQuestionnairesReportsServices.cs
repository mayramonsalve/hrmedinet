using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MedinetClassLibrary.Models.ReportsModels;
using System.Web;
using System.IO;
using MedinetClassLibrary.Models;

namespace MedinetClassLibrary.Services.Reports
{
    public class HRCompanyQuestionnairesReportsServices
    {
        private User userLogged = new UsersServices().GetByUserName(HttpContext.Current.User.Identity.Name);

        public IQueryable<HRCompanyQuestionnaireReport> GetQuestionnaires()
        {
            var questionnaires = from q in new QuestionnairesServices().GetQuestionnairesForCustomer(userLogged.Company)
                                 select new HRCompanyQuestionnaireReport
                                 {
                                     Name = q.Name,
                                     Template = new QuestionnairesServices().GetTemplateString(q.Template),
                                     Categories = q.Categories.Count,
                                     Questions = new QuestionnairesServices().GetQuestionsCount(q.Id),
                                     CompanyName = userLogged.Company.Name,
                                     CompanyImage = ImageConversion(userLogged.Company.Image, "Customer"),
                                     OwnerName = userLogged.Company.CompanyAssociated.Name,
                                     OwnerImage = ImageConversion(userLogged.Company.CompanyAssociated.Image, userLogged.Company.CompanyAssociated.CompaniesType.Name)
                                 };
            return questionnaires;
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
