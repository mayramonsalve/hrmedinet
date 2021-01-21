using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Services;
using Medinet.Models.ViewModels;
using MedinetClassLibrary.AuthorizationModels;


namespace Medinet.Controllers
{
    [HandleError]
    [Authorize(Roles = "CompanyAppManager")]
    public class ClimateRangesController : Controller
    {
        private ClimateRangesServices _climateRangeService;
        private ClimateRangeViewModel _climateRangeViewModel;

        public ClimateRangesController() {
            _climateRangeService = new ClimateRangesServices();
        }

        public ClimateRangesController(ClimateRangesServices _climateRangeService)
        {
            this._climateRangeService = _climateRangeService;
        }

        private bool GetAuthorization(ClimateRange climateRange)
        {
            return new SharedAdminAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                        new CompaniesServices().GetById(climateRange.ClimateScale.Company_Id)).isAuthorizated();
        }

        public ActionResult Create(int? climateScale_id)
        {
            if (climateScale_id == 0)
                climateScale_id = null;
            InitializeViews(null, climateScale_id);
            return View(_climateRangeViewModel);
        }

        [HttpPost]
        public ActionResult Create(ClimateRange climateRange)
        {
            ValidateClimateRangeModel(climateRange);
            if (ModelState.IsValid)
            {
                if (_climateRangeService.Add(climateRange))
                {
                    return RedirectToAction("Index", new { @climateScale_id = climateRange.ClimateScale_Id });
                }
            }
            InitializeViews(null, null);
            return View(_climateRangeViewModel);
        }

        private void ValidateClimateRangeModel(ClimateRange climateRange)
        {
            if (_climateRangeService.IsValueDuplicated(climateRange.ClimateScale_Id, climateRange.MinValue))
                ModelState.AddModelError(ViewRes.Controllers.ClimateRanges.MinValue, ViewRes.Controllers.ClimateRanges.MinValueText);
            if (_climateRangeService.IsValueDuplicated(climateRange.ClimateScale_Id, climateRange.MaxValue))
                ModelState.AddModelError(ViewRes.Controllers.ClimateRanges.MaxValue, ViewRes.Controllers.ClimateRanges.MaxValueText);
            if (_climateRangeService.IsNameDuplicated(climateRange.ClimateScale_Id, climateRange.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
            if (_climateRangeService.IsColorDuplicated(climateRange.ClimateScale_Id, climateRange.Color))
                ModelState.AddModelError(ViewRes.Controllers.ClimateRanges.Color, ViewRes.Controllers.ClimateRanges.ColorText);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_climateRangeService.GetById(id)))
            {
                try
                {
                    _climateRangeService.Delete(id);
                    _climateRangeService.SaveChanges();
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

        public ActionResult Details(int id)
        {
            if (GetAuthorization(_climateRangeService.GetById(id)))
            {
                InitializeViews(id, null);
                return View(_climateRangeViewModel);
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_climateRangeService.GetById(id)))
            {
                InitializeViews(id, null);
                return View(_climateRangeViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_climateRangeService.GetById(id)))
            {
                try
                {
                    ClimateRange ClimateRange = _climateRangeService.GetById(id);
                    UpdateModel(ClimateRange, "ClimateRange");
                    _climateRangeService.SaveChanges();
                    return RedirectToAction("Index", new { @climateScale_id = ClimateRange.ClimateScale_Id });
                }
                catch
                {
                    InitializeViews(id, null);
                    return View(_climateRangeViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult GridData(int? climateScale_id, string sidx, string sord, int page, int rows, string filters)
        {
            if (climateScale_id != null)
            {
                object resultado = _climateRangeService.RequestList(climateScale_id.Value, sidx, sord, page, rows, filters);
                return Json(resultado);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Index(int? climateScale_id)
        {
            InitializeViews(null, climateScale_id);
            return View(_climateRangeViewModel);
        }

        private void InitializeViews(int? climateRange_id, int? climateScale_id)
        {
            ClimateRange ClimateRange;
            SelectList climateScalesList;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());

            if (climateRange_id != null)
            {
                ClimateRange = _climateRangeService.GetById(climateRange_id.Value);
                ClimateRange.ClimateScale = new ClimateScalesServices().GetById(ClimateRange.ClimateScale_Id);
                climateScalesList = new SelectList(new ClimateScalesServices().GetClimateScalesForDropDownListByCompany(user.Company_Id), "Key", "Value", ClimateRange.ClimateScale_Id);
            }
            else
            {
                ClimateRange = new ClimateRange();
                if (climateScale_id != null)
                {
                    ClimateRange.ClimateScale_Id = (int)climateScale_id;
                    climateScalesList = new SelectList(new ClimateScalesServices().GetClimateScalesForDropDownListByCompany(user.Company_Id), "Key", "Value", ClimateRange.ClimateScale_Id);
                }
                else
                {
                    climateScalesList = new SelectList(new ClimateScalesServices().GetClimateScalesForDropDownListByCompany(user.Company_Id), "Key", "Value");
                }
            }
            _climateRangeViewModel = new ClimateRangeViewModel(ClimateRange, climateScalesList);
        }
    }
}
