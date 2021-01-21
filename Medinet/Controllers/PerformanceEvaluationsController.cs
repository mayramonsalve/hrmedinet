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
    public class PerformanceEvaluationsController : Controller
    {
        
        public PerformanceEvaluationViewModel _performanceEvaluationViewModel;
        public PerformanceEvaluationsServices _performanceEvaluationService;

        public PerformanceEvaluationsController()
        {
            _performanceEvaluationService = new PerformanceEvaluationsServices();
        }

        public PerformanceEvaluationsController(PerformanceEvaluationsServices _performanceEvaluationService)
        {
            this._performanceEvaluationService = _performanceEvaluationService;
        }

        private bool GetAuthorization(PerformanceEvaluation performance)
        {
            return new SharedAdminAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                        new CompaniesServices().GetById(performance.Company_Id)).isAuthorizated();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult Create()
        {
            InitializeViews(null);
            return View(_performanceEvaluationViewModel);
        }


        [HttpPost]
        public ActionResult Create(PerformanceEvaluation performanceEvaluation)
        {
            performanceEvaluation.Company_Id = (int)new UsersServices().GetByUserName(User.Identity.Name.ToString()).Company_Id;
            ValidatePerformanceEvaluationModel(performanceEvaluation);
            if (ModelState.IsValid)
            {
                if (_performanceEvaluationService.Add(performanceEvaluation))
                {
                    return RedirectToAction("Index");
                }
            }
            InitializeViews(null);
            return View(_performanceEvaluationViewModel);
        }
        
        public ActionResult Index()
        {
             return View();
        }


        public ActionResult Details(int id)
        {
            if (GetAuthorization(_performanceEvaluationService.GetById(id)))
            {
                InitializeViews(id);
                return View(_performanceEvaluationViewModel);
            }
            else
                return RedirectToLogOn();
        }
 
        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_performanceEvaluationService.GetById(id)))
            {
                InitializeViews(id);
                return View(_performanceEvaluationViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_performanceEvaluationService.GetById(id)))
            {
                try
                {
                    PerformanceEvaluation performanceEvaluation = _performanceEvaluationService.GetById(id);
                    UpdateModel(performanceEvaluation, "PerformanceEvaluation");
                    _performanceEvaluationService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id);
                    return View(_performanceEvaluationViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_performanceEvaluationService.GetById(id)))
            {
                try
                {
                    _performanceEvaluationService.Delete(id);
                    _performanceEvaluationService.SaveChanges();
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

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            object resultado = _performanceEvaluationService.RequestList(sidx, sord, page, rows, filters,(int)user.Company_Id);
            return Json(resultado);
        }

        private void InitializeViews(int? performanceEvaluation_id)
        {
            PerformanceEvaluation performanceEvaluation;
            if (performanceEvaluation_id != null)
                performanceEvaluation = _performanceEvaluationService.GetById((int)performanceEvaluation_id);
            else
                performanceEvaluation = new PerformanceEvaluation();
            _performanceEvaluationViewModel = new PerformanceEvaluationViewModel(performanceEvaluation);
        }

        private void ValidatePerformanceEvaluationModel(PerformanceEvaluation performanceEvaluation)
        {
            if (_performanceEvaluationService.IsNameDuplicated(performanceEvaluation.Name,performanceEvaluation.Company_Id))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
            if (_performanceEvaluationService.IsLevelDuplicated(performanceEvaluation.Company_Id, performanceEvaluation.Level.Value))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Level, ViewRes.Controllers.Shared.LevelText);
        }
    }
}
