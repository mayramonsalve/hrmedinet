using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using Medinet.Models;
using MedinetClassLibrary.Models;
using Medinet.Models.ViewModels;
using System.Net.Mail;
using MedinetClassLibrary.Classes;
using System.Net;

namespace Medinet.Controllers
{
    public class ErrorsController : Controller
    {

        public ActionResult PageNotFound()
        {
            //Response.TrySkipIisCustomErrors = true;
            //Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }

        public ActionResult GeneralError()
        {
            return View();
        }

    }
}
