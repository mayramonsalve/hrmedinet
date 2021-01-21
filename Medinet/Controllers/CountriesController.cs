using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.Models;
using Medinet.Models.ViewModels;
using MedinetClassLibrary.AuthorizationModels;

namespace Medinet.Controllers
{
    [HandleError]
    [Authorize(Roles = "Administrator")]
    public class CountriesController : Controller
    {
        private CountriesServices _countryService;
        private CountryViewModel _countryViewModel;

        public CountriesController()
        {
            _countryService = new CountriesServices();
        }

        public CountriesController(CountriesServices _countryService)
        {
            this._countryService = _countryService;
        }

        private bool GetAuthorization()
        {
            return new CountriesAndStatesAutorization(new UsersServices().GetByUserName(User.Identity.Name)).isAuthorizated();
        }

        public ActionResult Create()
        {
            InitializeViews(null);
            return View(_countryViewModel);
        }

        [HttpPost]
        public ActionResult Create(Country country)
        {
            if (GetAuthorization())
            {
                ValidateCountryModel(country);
                if (ModelState.IsValid)
                {
                    if (_countryService.Add(country))
                        return RedirectToAction("Index");
                }
                InitializeViews(null);
                return View(_countryViewModel);
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Delete(int id)
        {
            InitializeViews(id);
            return View(_countryViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization())
            {
                try
                {
                    _countryService.Delete(id);
                    _countryService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id);
                    return RedirectToAction("Index");
                }
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Edit(int id)
        {
            if (GetAuthorization())
            {
                InitializeViews(id);
                return View(_countryViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization())
            {
                try
                {
                    Country country = _countryService.GetById(id);
                    UpdateModel(country);
                    _countryService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id);
                    return View(_countryViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Details(int id)
        {
            if (GetAuthorization())
            {
                InitializeViews(id);
                return View(_countryViewModel);
            }
            else
                return RedirectToLogOn();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            object resultado = _countryService.RequestList(sidx, sord, page, rows, filters);
            return Json(resultado);
        }

        private void InitializeViews(int? country_id)
        {
            Country country;

            if (country_id != null)
            {
                country = _countryService.GetById((int)country_id);
            }
            else
            {
                country = new Country();
            }

            _countryViewModel = new CountryViewModel(country);
        }

        private void ValidateCountryModel(Country country)
        {
            if (_countryService.IsNameDuplicated(country.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
        }

    }
}
