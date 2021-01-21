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
    public class CompanyReportsServices
    {
        private User userLogged = new UsersServices().GetByUserName(HttpContext.Current.User.Identity.Name);

        public IQueryable<CompanyReport> GetCompanies()
        {
            if (userLogged.Company.CompaniesType.Name == "Owner")
            {
                return from c in new CompaniesServices().GetAssociatedCompanies()
                       select new CompanyReport
                       {
                           Name = c.Name,
                           OwnerName = userLogged.Company.Name,
                           //OwnerImage = ImageConversion(userLogged.Company.Image),
                           Address = c.Address,
                           Id = c.Id,
                           Contact= c.Contact,
                           Number= c.Number
                       };
            
            }
            else  { // associated
                return from c in new CompaniesServices().GetCustomersByAssociated(userLogged.Company_Id)
                                select new CompanyReport
                                {
                                    Name = c.Name,
                                    OwnerName = userLogged.Company.Name,
                                    //OwnerImage = ImageConversion(userLogged.Company.Image),
                                    Address = c.Address,
                                    Id = c.Id,
                                    Contact= c.Contact,
                                    Number=c.Number
                                };
            
            }
        }

        private byte[] ImageConversion(string imageName)
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
                FileStream fs = new FileStream(path + "/Content/Images/Companies/default.png", FileMode.Open, FileAccess.Read);
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
