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
    public class FunctionalOrganizationsController : Controller
    {
        private FunctionalOrganizationsServices _functionalOrganizationService;
        private FunctionalOrganizationViewModel _functionalOrganizationViewModel;

        public FunctionalOrganizationsController()
        {
            _functionalOrganizationService = new FunctionalOrganizationsServices();
        }

        public FunctionalOrganizationsController(FunctionalOrganizationsServices _functionalOrganizationService)
        {
            this._functionalOrganizationService = _functionalOrganizationService;
        }

        private bool GetAuthorization(int company_id)
        {
            return new SharedAdminAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                        new CompaniesServices().GetById(company_id)).isAuthorizated();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult Create(int? type_id)
        {
            if (type_id == 0)
                type_id = null;
            InitializeViews(null,type_id);
            return View(_functionalOrganizationViewModel);
        }
        
        [HttpPost]
        public ActionResult Create(FunctionalOrganization functionalOrganization)
        {
            ValidateFunctionalOrganizationModel(functionalOrganization);
            if (ModelState.IsValid)
            {
                if (_functionalOrganizationService.Add(functionalOrganization))
                    return RedirectToAction("Index", new { @type_id = functionalOrganization.Type_Id });
            }
            InitializeViews(null, null);
            return View(_functionalOrganizationViewModel);
        }

        public ActionResult Delete(int id)
        {
            InitializeViews(id, null);
            return View(_functionalOrganizationViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_functionalOrganizationService.GetById(id).FunctionalOrganizationType.Company_Id))
            {
                try
                {
                    _functionalOrganizationService.Delete(id);
                    _functionalOrganizationService.SaveChanges();
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
            if (GetAuthorization(_functionalOrganizationService.GetById(id).FunctionalOrganizationType.Company_Id))
            {
                InitializeViews(id, null);
                return View(_functionalOrganizationViewModel);
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_functionalOrganizationService.GetById(id).FunctionalOrganizationType.Company_Id))
            {
                InitializeViews(id,null);
                return View(_functionalOrganizationViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_functionalOrganizationService.GetById(id).FunctionalOrganizationType.Company_Id))
            {
                try
                {
                    FunctionalOrganization functionalOrganization = _functionalOrganizationService.GetById(id);
                    UpdateModel(functionalOrganization, "functionalOrganization");
                    _functionalOrganizationService.SaveChanges();
                    return RedirectToAction("Index", new { @type_id = functionalOrganization.Type_Id });
                }
                catch
                {
                    InitializeViews(id,null);
                    return View(_functionalOrganizationViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters, int? type_id)
        {
            if (type_id != null)
            {
                //User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
                object resultado = _functionalOrganizationService.RequestList(sidx, sord, page, rows, filters, (int)type_id);
                return Json(resultado);
            }
            else
                return null;
        }

        public ActionResult Index(int? type_id)
        {
            InitializeViews(null,type_id);
            return View(_functionalOrganizationViewModel);
        }

        [HttpPost]
        public JsonResult GetFunctionalOrganizationsByType(int type_id)
        {
            List<object> functionalOrganizations = new List<object>();
            foreach (var functionalOrganization in _functionalOrganizationService.GetFunctionalOrganizationsByTypeForDropDownList(type_id))
            {
                functionalOrganizations.Add(
                    new
                    {
                        optionValue = functionalOrganization.Key,
                        optionDisplay = functionalOrganization.Value
                    });
            }

            return Json(functionalOrganizations);
        }

        private void InitializeViews(int? functionalOrganization_id, int? functionalOrganizationType_id)
        {
            FunctionalOrganization functionalOrganization;
            SelectList typesList;
            SelectList typesParentList;
            SelectList fosParentList;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());

            if (functionalOrganization_id != null)
            {
                functionalOrganization = _functionalOrganizationService.GetById((int)functionalOrganization_id);
                if(functionalOrganization.FOParent_Id.HasValue)
                {
                    typesParentList = new SelectList(new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(user.Company_Id), "Key", "Value", functionalOrganization.Parent.Type_Id);
                    typesList = new SelectList(new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesChildrenByTypeForDropDownList(functionalOrganization.Parent.Type_Id), "Key", "Value", functionalOrganization.Type_Id);
                    fosParentList = new SelectList(_functionalOrganizationService.GetFunctionalOrganizationsByTypeForDropDownList(functionalOrganization.Parent.Type_Id), "Key", "Value", functionalOrganization.FOParent_Id);
                }
                else
                {
                    typesList = new SelectList(new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(user.Company_Id), "Key", "Value", functionalOrganization.Type_Id);
                    typesParentList = new SelectList(new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(user.Company_Id), "Key", "Value");
                    fosParentList = new SelectList(new Dictionary<int, string>(), "Key", "Value");
                }
            }
            else
            {
                functionalOrganization = new FunctionalOrganization();
                typesParentList = new SelectList(new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(user.Company_Id), "Key", "Value");
                fosParentList = new SelectList(new Dictionary<int, string>(), "Key", "Value");
                if (functionalOrganizationType_id != null)
                {
                    typesList = new SelectList(new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(user.Company_Id), "Key", "Value", functionalOrganizationType_id.Value);
                    functionalOrganization.Type_Id = functionalOrganizationType_id.Value;
                }
                else
                    typesList = new SelectList(new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(user.Company_Id), "Key", "Value");
            }
            _functionalOrganizationViewModel = new FunctionalOrganizationViewModel(functionalOrganization, typesList, typesParentList, fosParentList);
        }

        private void ValidateFunctionalOrganizationModel(FunctionalOrganization functionalOrganization)
        {
            if (_functionalOrganizationService.IsNameDuplicated(functionalOrganization.Type_Id, functionalOrganization.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
        }

    }
}
