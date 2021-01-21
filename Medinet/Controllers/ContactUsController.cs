using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.Models;
using Medinet.Models.ViewModels;

namespace Medinet.Controllers
{
    [HandleError]
    [Authorize(Users = "Administrator")]
    public class ContactUsController : Controller
    {
        private ContactUsServices _contactUsService;
        private ContactUsViewModel _contactUsViewModel;

        public ContactUsController()
        {
            _contactUsService = new ContactUsServices();
        }

        public ContactUsController(ContactUsServices _contactUsService)
        {
            this._contactUsService = _contactUsService;
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (User.Identity.Name == "administrator")
                {
                    _contactUsService.Delete(id);
                    _contactUsService.SaveChanges();
                }
                return RedirectToAction("ShowContacts");
            }
            catch
            {
                InitializeViews(id);
                return RedirectToAction("ShowContacts");
            }
        }

        public ActionResult Details(int id)
        {
            if (User.Identity.Name == "administrator")
                InitializeViews(id);
            return View(_contactUsViewModel);
        }


        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            object resultado = null;
            if(User.Identity.Name == "administrator")
                resultado = _contactUsService.RequestList(sidx, sord, page, rows, filters);
            return Json(resultado);
        } 

        public ActionResult ShowContacts()
        {
            if(User.Identity.Name == "administrator")
                InitializeViews(null);
            return View();
        }

        private void InitializeViews(int? contact_id)
        {
            ContactUs contactUs;
            SelectList countriesList = new SelectList(new CountriesServices().GetCountriesForDropDownList(), "Key", "Value");
            if (contact_id != null)
                contactUs = _contactUsService.GetById((int)contact_id);
            else
                contactUs = new ContactUs();

            _contactUsViewModel = new ContactUsViewModel(contactUs, countriesList);
        }

    }
}
