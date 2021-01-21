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
    //[Authorize(Roles = "Administrator")]
    public class StatesController : Controller
    {
        private StatesServices _stateService;
        private StateViewModel _stateViewModel;

        public StatesController()
        {
            _stateService = new StatesServices();
        }

        public StatesController(StatesServices _stateService)
        {
            this._stateService = _stateService;
        }

        private bool GetAuthorization()
        {
            return new CountriesAndStatesAutorization(new UsersServices().GetByUserName(User.Identity.Name)).isAuthorizated();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create(int? country_id)
        {
            if (country_id == 0)
                country_id = null;
            InitializeViews(null, country_id);
            return View(_stateViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(State state)
        {
            if (GetAuthorization())
            {
                ValidateStateModel(state);
                if (ModelState.IsValid)
                {
                    if (_stateService.Add(state))
                        return RedirectToAction("Index", new { @country_id = state.Country_Id });
                }
                InitializeViews(null, null);
                return View(_stateViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            InitializeViews(id, null);
            return View(_stateViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization())
            {
                try
                {
                    _stateService.Delete(id);
                    _stateService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id, null);
                    return RedirectToAction("Index");
                }
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            if (GetAuthorization())
            {
                InitializeViews(id, null);
                return View(_stateViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization())
            {
                try
                {
                    State state = _stateService.GetById(id);
                    UpdateModel(state, "State");
                    _stateService.SaveChanges();
                    return RedirectToAction("Index", new { @country_id = state.Country_Id });
                }
                catch
                {
                    InitializeViews(id, null);
                    return View(_stateViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Index(int? country_id)
        {
            InitializeViews(null, country_id);
            return View(_stateViewModel);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int id)
        {
            if(GetAuthorization())
            {
                InitializeViews(id,null);
                return View(_stateViewModel);
            }
            else
                return RedirectToLogOn();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, CompanyAppManager")]
        public JsonResult GetStatesByCountry(int country_id)
        {
            List<object> states = new List<object>();
            StatesServices stateService = new StatesServices();
            foreach (var state in stateService.GetStatesForDropDownList(country_id))
            {
                states.Add(
                    new
                    {
                        optionValue = state.Key,
                        optionDisplay = state.Value
                    });
            }

            return Json(states);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult GridData(int? country_id, string sidx, string sord, int page, int rows, string filters)
        {

            if (country_id != null)
            {
                object resultado = _stateService.RequestList(country_id.Value, sidx, sord, page, rows, filters);
                return Json(resultado);
            }
            else
            {
                return null;    
            }
   
        }

        private void InitializeViews(int? state_id, int? country_id)
        {
            State state;
            SelectList countriesList;

            if (state_id != null)
            {
                state = _stateService.GetById((int)state_id);
                countriesList = new SelectList(new CountriesServices().GetCountriesForDropDownList(), "Key", "Value", state.Country_Id);
            }
            else
            {
                state = new State();
                if (country_id != null)
                {
                    countriesList = new SelectList(new CountriesServices().GetCountriesForDropDownList(), "Key", "Value", country_id);
                    state.Country_Id = country_id.Value;
                }
                else
                    countriesList = new SelectList(new CountriesServices().GetCountriesForDropDownList(), "Key", "Value");
            }
            _stateViewModel = new StateViewModel(state,countriesList);
        }

        private void ValidateStateModel(State state)
        {
            if (_stateService.IsNameDuplicated(state.Country_Id, state.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
        }
    }
}
