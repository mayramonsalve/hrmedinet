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
    public class AgesController : Controller
    {
        private AgesServices _ageService;
        private AgeViewModel _ageViewModel;

        public AgesController()
        {
            _ageService = new AgesServices();
        }

        public AgesController(AgesServices _ageService)
        {
            this._ageService = _ageService;
        }

        private bool GetAuthorization(Age age)
        {
            return new SharedAdminAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                        new CompaniesServices().GetById(age.Company_Id)).isAuthorizated();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError( ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult Create()
        {
            InitializeViews(null);
            return View(_ageViewModel);
        }
        
        [HttpPost]
        public ActionResult Create(Age age)
        {
            age.Company_Id = (int)new UsersServices().GetByUserName(User.Identity.Name.ToString()).Company_Id;
            ValidateAgeModel(age);
            if (ModelState.IsValid)
            {
                if (_ageService.Add(age))
                    return RedirectToAction("Index");
            }
            InitializeViews(null);
            return View(_ageViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_ageService.GetById(id)))
            {
                try
                {
                    _ageService.Delete(id);
                    _ageService.SaveChanges();
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
            if (GetAuthorization(_ageService.GetById(id)))
            {
                InitializeViews(id);
                return View(_ageViewModel);
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_ageService.GetById(id)))
            {
                InitializeViews(id);
                return View(_ageViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_ageService.GetById(id)))
            {
                try
                {
                    Age age = _ageService.GetById(id);
                    UpdateModel(age, "Age");
                    _ageService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id);
                    return View(_ageViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            object resultado = _ageService.RequestList(sidx, sord, page, rows, filters, (int)user.Company_Id);
            return Json(resultado);
        }

        public ActionResult Index()
        {
            InitializeViews(null);
            return View(_ageViewModel);
        }

        [HttpPost]
        public JsonResult GetAgesByCompany(int? company_id)
        {
            List<object> ages = new List<object>();
            AgesServices ageService = new AgesServices();
            foreach (var age in ageService.GetAgesForDropDownList((int)company_id))
            {
                ages.Add(
                    new
                    {
                        optionValue = age.Key,
                        optionDisplay = age.Value
                    });
            }

            return Json(ages);
        }

        private void InitializeViews(int? age_id)
        {
            Age age;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());

            if (age_id != null)
                age = _ageService.GetById((int)age_id);
            else
                age = new Age();
            _ageViewModel = new AgeViewModel(age);
        }

        private void ValidateAgeModel(Age age)
        {
            if (_ageService.IsNameDuplicated(age.Company_Id, age.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
            if (_ageService.IsLevelDuplicated(age.Company_Id, age.Level.Value))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Level, ViewRes.Controllers.Shared.LevelText);
        }

    }
}
