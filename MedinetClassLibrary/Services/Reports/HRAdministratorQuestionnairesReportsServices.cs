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
    public class HRAdministratorQuestionnairesReportsServices
    {
        private User userLogged = new UsersServices().GetByUserName(HttpContext.Current.User.Identity.Name);

        public IQueryable<HRAdministratorQuestionnaireReport> GetQuestionnaires()
        {
            if (userLogged.Company.CompaniesType.Name == "Owner")
            {
                var questionnairesForOwner = from q in new QuestionnairesServices().GetAllRecords()
                                     select new HRAdministratorQuestionnaireReport
                                     {
                                         Name = q.Name,
                                         Template = new QuestionnairesServices().GetTemplateString(q.Template),
                                         Categories = q.Categories.Count,
                                         Questions = new QuestionnairesServices().GetQuestionsCount(q.Id),
                                         CompanyName = q.User.Company.Name,
                                         AssociatedName = userLogged.Company.Name, //q.User.Company.Name,
                                         AssociatedImage = ImageConversion(userLogged.Company.Image, "Owner"), //ImageConversion(q.User.Company.Image),
                                         OwnerName = " ",//userLogged.Company.Name,
                                         OwnerImage = ImageConversion("","Empty") //ImageConversion(userLogged.Company.Image)
                                     };
                return questionnairesForOwner;
            }
            else
            {
                var questionnairesForAssociated = from q in new QuestionnairesServices().GetQuestionnairesForAssociated(userLogged.Company_Id)
                                     select new HRAdministratorQuestionnaireReport
                                     {
                                         Name = q.Name,
                                         Template = new QuestionnairesServices().GetTemplateString(q.Template),
                                         Categories = q.Categories.Count,
                                         Questions = new QuestionnairesServices().GetQuestionsCount(q.Id),
                                         CompanyName = q.User.Company.Name,
                                         AssociatedName = userLogged.Company.Name, //q.User.Company.Name,
                                         AssociatedImage = ImageConversion(userLogged.Company.Image, "Associated"), //ImageConversion(q.User.Company.Image),
                                         OwnerName = userLogged.Company.CompanyAssociated.Name,//userLogged.Company.Name,
                                         OwnerImage = ImageConversion(userLogged.Company.CompanyAssociated.Image, "Owner") //ImageConversion(userLogged.Company.Image)
                                     };
                return questionnairesForAssociated;
            }
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
