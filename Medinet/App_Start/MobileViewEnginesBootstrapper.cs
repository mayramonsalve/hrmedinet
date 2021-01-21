using System.Web.Mvc;
using MobileViewEngines.MVC3;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Medinet.MobileViewEngines), "Start")]
namespace Medinet
{
    public static class MobileViewEngines
    {
        public static void Start()
        {
            ViewEngines.Engines.Insert(0, new MobileCapableWebFormViewEngine());
        }
    }
}