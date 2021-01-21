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
    public class RegionsController : Controller
    {
        private RegionsServices _regionService;
        private RegionViewModel _regionViewModel;

        public RegionsController()
        {
            _regionService = new RegionsServices();
        }

        public RegionsController(RegionsServices _locatonService)
        {
            this._regionService = _locatonService;
        }

        private bool GetAuthorization(Region region)
        {
            return new SharedAdminAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                        new CompaniesServices().GetById(region.Company_Id)).isAuthorizated();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult Create()
        {
            InitializeViews(null);
            return View(_regionViewModel);
        }

        [HttpPost]
        public ActionResult Create(Region region)
        {
            region.Company_Id = (int)new UsersServices().GetByUserName(User.Identity.Name.ToString()).Company_Id;
            ValidateRegionModel(region);
            if (ModelState.IsValid)
            {
                if (_regionService.Add(region))
                    return RedirectToAction("Index");
            }
            InitializeViews(null);
            return View(_regionViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_regionService.GetById(id)))
            {
                try
                {
                    _regionService.Delete(id);
                    _regionService.SaveChanges();
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
            if (GetAuthorization(_regionService.GetById(id)))
            {
                InitializeViews(id);
                return View(_regionViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_regionService.GetById(id)))
            {
                try
                {
                    Region region = _regionService.GetById(id);
                    UpdateModel(region, "Region");
                    _regionService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id);
                    return View(_regionViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Index()
        {
            InitializeViews(null);
            return View(_regionViewModel);
        }

        public ActionResult Details(int id)
        {
            if (GetAuthorization(_regionService.GetById(id)))
            {
                InitializeViews(id);
                return View(_regionViewModel);
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            object resultado = _regionService.RequestList((int)user.Company_Id, sidx, sord, page, rows, filters);
            return Json(resultado);
        }

        private void InitializeViews(int? region_id)
        {
            Region region;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());

            if (region_id != null)
                region = _regionService.GetById((int)region_id);
            else
                region = new Region();
            _regionViewModel = new RegionViewModel(region);
        }

        private void ValidateRegionModel(Region region)
        {
            if (_regionService.IsNameDuplicated(region.Company_Id, region.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
        }

    }
}
