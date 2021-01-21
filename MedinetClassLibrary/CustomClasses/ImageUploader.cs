using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedinetClassLibrary.Classes
{
    class ImageUploader
    {
        private static string UploadImageToServer(HttpRequestBase request, string rootFolder)
        {
            foreach (string inputTagName in request.Files)
            {
                Random random = new Random();
                HttpPostedFileBase file = request.Files[inputTagName];
                string nombre = DateTime.Now.ToString("yyyyMMddhhmmss") + random.Next(1000, 9999);

                if (file.ContentLength > 0)
                {
                    string[] extension = file.FileName.ToString().Split('.');
                    string filePath = request.MapPath(rootFolder + nombre + "." + extension.Last().ToString());
                    file.SaveAs(filePath);
                    return nombre + '.' + extension.Last().ToString();
                }
            }

            return null;
        }
    }
}
