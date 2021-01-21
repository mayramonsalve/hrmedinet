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
    public class PositionLevelsController : Controller
    {
        private PositionLevelsServices _positionLevelService;
        private PositionLevelViewModel _positionLevelViewModel;

        public PositionLevelsController()
        {
            _positionLevelService = new PositionLevelsServices();
        }

        public PositionLevelsController(PositionLevelsServices _positionLevelService)
        {
            this._positionLevelService = _positionLevelService;
        }

        private bool GetAuthorization(PositionLevel level)
        {
            return new SharedAdminAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                        new CompaniesServices().GetById(level.Company_Id)).isAuthorizated();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError( ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult Create()
        {
            InitializeViews(null);
            return View(_positionLevelViewModel);
        }
        
        [HttpPost]
        public ActionResult Create(PositionLevel level)
        {
            level.Company_Id = (int)new UsersServices().GetByUserName(User.Identity.Name.ToString()).Company_Id;
            ValidatePositionLevelModel(level);
            if (ModelState.IsValid)
            {
                if (_positionLevelService.Add(level))
                    return RedirectToAction("Index");
            }
            InitializeViews(null);
            return View(_positionLevelViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_positionLevelService.GetById(id)))
            {
                try
                {
                    _positionLevelService.Delete(id);
                    _positionLevelService.SaveChanges();
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
            if (GetAuthorization(_positionLevelService.GetById(id)))
            {
                InitializeViews(id);
                return View(_positionLevelViewModel);
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_positionLevelService.GetById(id)))
            {
                InitializeViews(id);
                return View(_positionLevelViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_positionLevelService.GetById(id)))
            {
                try
                {
                    PositionLevel positionLevel = _positionLevelService.GetById(id);
                    UpdateModel(positionLevel, "Level");
                    _positionLevelService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id);
                    return View(_positionLevelViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            object resultado = _positionLevelService.RequestList(sidx, sord, page, rows, filters, (int)user.Company_Id);
            return Json(resultado);
        }

        public ActionResult Index()
        {
            InitializeViews(null);
            return View(_positionLevelViewModel);
        }

        [HttpPost]
        public JsonResult GetPositionLevelsByCompany(int? company_id)
        {
            List<object> positionLevels = new List<object>();
            //AgesServices ageService = new AgesServices();
            foreach (var level in _positionLevelService.GetPositionLevelsForDropDownList((int)company_id))
            {
                positionLevels.Add(
                    new
                    {
                        optionValue = level.Key,
                        optionDisplay = level.Value
                    });
            }

            return Json(positionLevels);
        }

        private void InitializeViews(int? level_id)
        {
            PositionLevel level;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());

            if (level_id != null)
                level = _positionLevelService.GetById((int)level_id);
            else
                level = new PositionLevel();
            _positionLevelViewModel = new PositionLevelViewModel(level);
        }

        private void ValidatePositionLevelModel(PositionLevel level)
        {
            if (_positionLevelService.IsNameDuplicated(level.Company_Id, level.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
            if (_positionLevelService.IsLevelDuplicated(level.Company_Id, level.Level))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Level, ViewRes.Controllers.Shared.LevelText);
        }

    }
}
