using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Medinet.Controllers
{
    public class MobileController : Controller
    {
        //
        // GET: /Mobile/
        

        //
        // Cambiar a la vista de escritorio por parte del movil
        public ActionResult SwitchToDesktopVersion()
        {
            //ViewEngines.Engines.Remove(ViewEngines.Engines.OfType<MobileCapableWebFormViewEngine>().First());
            HttpContext.SetOverriddenBrowser(BrowserOverride.Desktop);
            //return RedirectToAction("Index", "Home"); 
            return View("/Views/Home/Index.aspx");
        }
    }
}
