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
    [Authorize(Roles = "CompanyAppManager")]
    public class SenioritiesController : Controller
    {
        private SenioritiesServices _seniorityService;
        private SeniorityViewModel _seniorityViewModel;

        public SenioritiesController()
        {
            _seniorityService = new SenioritiesServices();
        }

        public SenioritiesController(SenioritiesServices _seniorityService)
        {
            this._seniorityService = _seniorityService;
        }

        private bool GetAuthorization(Seniority seniority)
        {
            return new SharedAdminAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                        new CompaniesServices().GetById(seniority.Company_Id)).isAuthorizated();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult Create()
        {
            InitializeViews(null);
            return View(_seniorityViewModel);
        }

        [HttpPost]
        public ActionResult Create(Seniority seniority)
        {
            seniority.Company_Id = (int)new UsersServices().GetByUserName(User.Identity.Name.ToString()).Company_Id;
            ValidateSeniorityModel(seniority);
            if (ModelState.IsValid)
            {
                if (_seniorityService.Add(seniority))
                    return RedirectToAction("Index");
            }
            InitializeViews(null);
            return View(_seniorityViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_seniorityService.GetById(id)))
            {
                try
                {
                    _seniorityService.Delete(id);
                    _seniorityService.SaveChanges();
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

        public ActionResult Details(int id)
        {
            if (GetAuthorization(_seniorityService.GetById(id)))
            {
                InitializeViews(id);
                return View(_seniorityViewModel);
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_seniorityService.GetById(id)))
            {
                InitializeViews(id);
                return View(_seniorityViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_seniorityService.GetById(id)))
            {
                try
                {
                    Seniority seniority = _seniorityService.GetById(id);
                    UpdateModel(seniority, "Seniority");
                    _seniorityService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id);
                    return View(_seniorityViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            object resultado = _seniorityService.RequestList(sidx, sord, page, rows, filters, (int)user.Company_Id);
            return Json(resultado);
        }

        public ActionResult Index()
        {
            InitializeViews(null);
            return View(_seniorityViewModel);
        }

        [HttpPost]
        public JsonResult GetSenioritiesByCompany(int? company_id)
        {
            List<object> seniorities = new List<object>();
            SenioritiesServices seniorityServices = new SenioritiesServices();
            foreach (var seniority in seniorityServices.GetSenioritiesForDropDownList((int)company_id))
            {
                seniorities.Add(
                    new
                    {
                        optionValue = seniority.Key,
                        optionDisplay = seniority.Value
                    });
            }

            return Json(seniorities);
        }

        private void InitializeViews(int? seniority_id)
        {
            Seniority seniority;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());

            if (seniority_id != null)
            {
                seniority = _seniorityService.GetById((int)seniority_id);
            }
            else
            {
                seniority = new Seniority();
            }
            _seniorityViewModel = new SeniorityViewModel(seniority);
        }

        private void ValidateSeniorityModel(Seniority seniority)
        {
            if (_seniorityService.IsNameDuplicated(seniority.Company_Id, seniority.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
            if (_seniorityService.IsLevelDuplicated(seniority.Company_Id, seniority.Level.Value))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Level, ViewRes.Controllers.Shared.LevelText);
        }

    }
}
