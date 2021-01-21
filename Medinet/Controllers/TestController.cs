using MedinetClassLibrary.CustomClasses;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medinet.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Test()
        {
            //string guid = System.Guid.NewGuid().ToString();
            //ShortGuid short_guid = ShortGuid.NewGuid();
            //DateTime datetime = DateTime.Now;
            //string date = datetime.ToString("Hmmssfff");

            return View();
        }

        public JsonResult GetPieInfo(int test_id, string demographic)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            Dictionary<string, double> data = new ChartsServices().GetGraphicDataForPopulation("Population", parameters);
            Dictionary<string, object> options = new Dictionary<string, object>()
            {
                { "title", "Sample received" },
                { "is3D", true },
                { "width", 400 },
                { "height", 300 },
                { "colors", new string[] {"#FF69B4", "#00BFFF" } }
            };
            object[] info = new object[] { data, options };
            return Json(info);
        }

        public JsonResult GetBarInfo(int test_id, string demographic)
        {
            Test test = new TestsServices().GetById(test_id);
            Dictionary<string, object> data_aux = test.GetAvgAndMedByAgeRanges(null, null, null, false);
            Dictionary<string, double> data = (Dictionary<string, double>) data_aux["Average"];
            Dictionary<string, object> options = new Dictionary<string, object>()
            {
                { "title", "Sample received" },
                { "is3D", true },
                { "width", 400 },
                { "height", 300 },
                { "colors", new string[] {"#FF69B4", "#00BFFF" } }
            };
            object[] info = new object[] { data, options };
            return Json(info);
        }
    }
}