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
    public class ClimateScalesController : Controller
    {
        
        private ClimateScalesServices _climateScaleService;
        private ClimateScaleViewModel _climateScaleViewModel;

        public ClimateScalesController()
        {
            _climateScaleService = new ClimateScalesServices();
        }

        public ClimateScalesController(ClimateScalesServices _climateScaleService)
        {
            this._climateScaleService = _climateScaleService;
        }

        private bool GetAuthorization(ClimateScale climateScale)
        {
            return new SharedAdminAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                        new CompaniesServices().GetById(climateScale.Company_Id)).isAuthorizated();
        }

        public ActionResult Create()
        {
            InitializeViews(null);
            return View(_climateScaleViewModel);
        }

        [HttpPost]
        public ActionResult Create(ClimateScale climateScale)
        {
            climateScale.Company_Id = new UsersServices().GetByUserName(User.Identity.Name).Company_Id;
            if (GetAuthorization(climateScale))
            {
                ValidateClimateScaleModel(climateScale);
                if (ModelState.IsValid)
                {
                    if (_climateScaleService.Add(climateScale))
                        return RedirectToAction("Index");
                }
                InitializeViews(null);
                return View(_climateScaleViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_climateScaleService.GetById(id)))
            {
                try
                {
                    _climateScaleService.Delete(id);
                    _climateScaleService.SaveChanges();
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
            if (GetAuthorization(_climateScaleService.GetById(id)))
            {
                InitializeViews(id);
                return View(_climateScaleViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_climateScaleService.GetById(id)))
            {
                try
                {
                    ClimateScale climateScale = _climateScaleService.GetById(id);
                    UpdateModel(climateScale);
                    _climateScaleService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id);
                    return View(_climateScaleViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Index()
        {
            InitializeViews(null);         
            return View(_climateScaleViewModel);
        }

        public ActionResult Details(int id)
        {
            if (GetAuthorization(_climateScaleService.GetById(id)))
            {
                InitializeViews(id);
                return View(_climateScaleViewModel);
            }
            else
                return RedirectToLogOn();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            int company_id = new UsersServices().GetByUserName(User.Identity.Name).Company_Id;
            object resultado = _climateScaleService.RequestList(sidx, sord, page, rows, filters, company_id);
            return Json(resultado);
        }

        private void InitializeViews(int? climateScale_id)
        {
            ClimateScale climateScale;
            if (climateScale_id != null)
                climateScale = _climateScaleService.GetById((int)climateScale_id);
            else
                climateScale = new ClimateScale();
            _climateScaleViewModel = new ClimateScaleViewModel(climateScale);
        } 

        private void ValidateClimateScaleModel(ClimateScale climateScale)
        {
            if (_climateScaleService.IsNameDuplicated(climateScale.Company_Id, climateScale.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
        }
    
    }
}
