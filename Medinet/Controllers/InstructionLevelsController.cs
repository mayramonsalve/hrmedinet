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
    public class InstructionLevelsController : Controller
    {
        private InstructionLevelsServices _instructionLevelService;
        private InstructionLevelViewModel _instructionLevelViewModel;

        public InstructionLevelsController()
        {
            _instructionLevelService = new InstructionLevelsServices();
        }

        public InstructionLevelsController(InstructionLevelsServices _instructionLevelService)
        {
            this._instructionLevelService = _instructionLevelService;
        }

        private bool GetAuthorization(InstructionLevel level)
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
            return View(_instructionLevelViewModel);
        }
        
        [HttpPost]
        public ActionResult Create(InstructionLevel level)
        {
            level.Company_Id = (int)new UsersServices().GetByUserName(User.Identity.Name.ToString()).Company_Id;
            ValidateInstructionLevelModel(level);
            if (ModelState.IsValid)
            {
                if (_instructionLevelService.Add(level))
                    return RedirectToAction("Index");
            }
            InitializeViews(null);
            return View(_instructionLevelViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_instructionLevelService.GetById(id)))
            {
                try
                {
                    _instructionLevelService.Delete(id);
                    _instructionLevelService.SaveChanges();
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
            if (GetAuthorization(_instructionLevelService.GetById(id)))
            {
                InitializeViews(id);
                return View(_instructionLevelViewModel);
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_instructionLevelService.GetById(id)))
            {
                InitializeViews(id);
                return View(_instructionLevelViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_instructionLevelService.GetById(id)))
            {
                try
                {
                    InstructionLevel instructionLevel = _instructionLevelService.GetById(id);
                    UpdateModel(instructionLevel, "Level");
                    _instructionLevelService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id);
                    return View(_instructionLevelViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            object resultado = _instructionLevelService.RequestList(sidx, sord, page, rows, filters, (int)user.Company_Id);
            return Json(resultado);
        }

        public ActionResult Index()
        {
            InitializeViews(null);
            return View(_instructionLevelViewModel);
        }

        [HttpPost]
        public JsonResult GetInstructionLevelsByCompany(int? company_id)
        {
            List<object> instructionLevels = new List<object>();
            //AgesServices ageService = new AgesServices();
            foreach (var level in _instructionLevelService.GetInstructionLevelsForDropDownList((int)company_id))
            {
                instructionLevels.Add(
                    new
                    {
                        optionValue = level.Key,
                        optionDisplay = level.Value
                    });
            }

            return Json(instructionLevels);
        }

        private void InitializeViews(int? level_id)
        {
            InstructionLevel level;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());

            if (level_id != null)
                level = _instructionLevelService.GetById((int)level_id);
            else
                level = new InstructionLevel();
            _instructionLevelViewModel = new InstructionLevelViewModel(level);
        }

        private void ValidateInstructionLevelModel(InstructionLevel level)
        {
            if (_instructionLevelService.IsNameDuplicated(level.Company_Id, level.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
            if (_instructionLevelService.IsLevelDuplicated(level.Company_Id, level.Level.Value))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Level, ViewRes.Controllers.Shared.LevelText);
        }

    }
}
