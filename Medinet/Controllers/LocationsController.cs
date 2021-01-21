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
    //[Authorize(Roles = "CompanyAppManager")]
    public class LocationsController : Controller
    {
        private LocationsServices _locationService;
        private LocationViewModel _locationViewModel;

        public LocationsController()
        {
            _locationService = new LocationsServices();
        }

        public LocationsController(LocationsServices _locatonService)
        {
            this._locationService = _locatonService;
        }

        private bool GetAuthorization(Location location)
        {
            return new SharedAdminAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                        new CompaniesServices().GetById(location.Company_Id)).isAuthorizated();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize(Roles = "CompanyAppManager")]
        public ActionResult Create(int? country_id, int? state_id)
        {
            if (country_id == 0 || state_id == 0)
            {
                country_id = null;
                state_id = null;
            }
            InitializeViews(null, country_id, state_id);
            return View(_locationViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "CompanyAppManager")]
        public ActionResult Create(Location location)
        {
            location.Company_Id = (int)new UsersServices().GetByUserName(User.Identity.Name.ToString()).Company_Id;
            ValidateLocationModel(location);
            if (ModelState.IsValid)
            {
                if (_locationService.Add(location))
                    return RedirectToAction("Index", new { @country_id = location.State.Country_Id, @state_id = location.State_Id });
            }
            InitializeViews(null, null, null);
            return View(_locationViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "CompanyAppManager")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_locationService.GetById(id)))
            {
                try
                {
                    _locationService.Delete(id);
                    _locationService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id, null, null);
                    return RedirectToAction("Index");
                }
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "CompanyAppManager")]
        public ActionResult Details(int id)
        {
            if (GetAuthorization(_locationService.GetById(id)))
            {
                InitializeViews(id, null, null);
                return View(_locationViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "CompanyAppManager")]
        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_locationService.GetById(id)))
            {
                InitializeViews(id, null, null);
                return View(_locationViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        [Authorize(Roles = "CompanyAppManager")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_locationService.GetById(id)))
            {
                try
                {
                    Location location = _locationService.GetById(id);
                    UpdateModel(location, "Location");
                    _locationService.SaveChanges();
                    return RedirectToAction("Index", new { @country_id = location.State.Country_Id, @state_id = location.State_Id });
                }
                catch
                {
                    InitializeViews(id, null, null);
                    return View(_locationViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "CompanyAppManager")]
        public ActionResult Index(int? country_id, int? state_id)
        {
            InitializeViews(null, country_id, state_id);
            return View(_locationViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "CompanyAppManager")]
        public JsonResult GetLocationsByState(int state_id)
        {
            List<object> locations = new List<object>();
            LocationsServices locationService = new LocationsServices();
            foreach (var location in locationService.GetLocationsForDropDownList(state_id))
            {
                locations.Add(
                    new
                    {
                        optionValue = location.Key,
                        optionDisplay = location.Value
                    });
            }

            return Json(locations);
        }

        [Authorize(Roles = "CompanyAppManager")]
        public ActionResult GridData(int? state_id, string sidx, string sord, int page, int rows, string filters)
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            if (state_id != null)
            {
                object resultado = _locationService.RequestList((int)user.Company_Id,state_id.Value, sidx, sord, page, rows, filters);
                return Json(resultado);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, CompanyAppManager")]
        public JsonResult GetLocationsByCompany(int? company_id)
        {
            List<object> locations = new List<object>();
            LocationsServices locationService = new LocationsServices();
            foreach (var location in locationService.GetLocationsForDropDownList((int)company_id))
            {
                locations.Add(
                    new
                    {
                        optionValue = location.Key,
                        optionDisplay = location.Value
                    });
            }

            return Json(locations);
        }

        private void InitializeViews(int? locaton_id,int? country_id, int? state_id)
        {
            Location location;
            SelectList statesList;
            SelectList countriesList;
            SelectList regionsList;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());

            if (locaton_id != null)
            {
                location = _locationService.GetById((int)locaton_id);
                statesList = new SelectList(new StatesServices().GetStatesForDropDownList(location.State.Country_Id), "Key", "Value", location.State_Id);
                regionsList = new SelectList(new RegionsServices().GetRegionsForDropDownList(user.Company_Id), "Key", "Value", location.Region_Id);
                countriesList = new SelectList(new CountriesServices().GetCountriesForDropDownList(), "Key", "Value", location.State.Country_Id);
            }
            else
            {
                location = new Location();
                regionsList = new SelectList(new RegionsServices().GetRegionsForDropDownList(user.Company_Id), "Key", "Value");                
                statesList = new SelectList(new StatesServices().GetEmptyDictionary(), "Key", "Value");
                if (country_id != null)
                {
                    countriesList = new SelectList(new CountriesServices().GetCountriesForDropDownList(), "Key", "Value", country_id);
                    if (state_id != null)
                    {
                        location.State_Id = state_id.Value;
                        statesList = new SelectList(new StatesServices().GetStatesForDropDownList(country_id.Value), "Key", "Value", state_id);
                    }
                }
                else
                    countriesList = new SelectList(new CountriesServices().GetCountriesForDropDownList(), "Key", "Value");
            }

            _locationViewModel = new LocationViewModel(location, statesList, countriesList, regionsList);
        }

        private void ValidateLocationModel(Location location)
        {
            if (_locationService.IsNameDuplicated(location.Company_Id, location.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
        }

    }
}
