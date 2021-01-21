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
    public class FunctionalOrganizationTypesController : Controller
    {
        private FunctionalOrganizationTypesServices _typeService;
        private FunctionalOrganizationTypeViewModel _typeViewModel;

        public FunctionalOrganizationTypesController()
        {
            _typeService = new FunctionalOrganizationTypesServices();
        }

        public FunctionalOrganizationTypesController(FunctionalOrganizationTypesServices _typeService)
        {
            this._typeService = _typeService;
        }

        private bool GetAuthorization(FunctionalOrganizationType type)
        {
            return new SharedAdminAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                        new CompaniesServices().GetById(type.Company_Id)).isAuthorizated();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult Create()
        {
            InitializeViews(null);
            return View(_typeViewModel);
        }
        
        [HttpPost]
        public ActionResult Create(FunctionalOrganizationType type)
        {
            type.Company_Id = (int)new UsersServices().GetByUserName(User.Identity.Name.ToString()).Company_Id;
            ValidateTypeModel(type);
            if (ModelState.IsValid)
            {
                if (_typeService.Add(type))
                    return RedirectToAction("Index");
            }
            InitializeViews(null);
            return View(_typeViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_typeService.GetById(id)))
            {
                try
                {
                    _typeService.Delete(id);
                    _typeService.SaveChanges();
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
            if (GetAuthorization(_typeService.GetById(id)))
            {
                InitializeViews(id);
                return View(_typeViewModel);
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_typeService.GetById(id)))
            {
                InitializeViews(id);
                return View(_typeViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_typeService.GetById(id)))
            {
                try
                {
                    FunctionalOrganizationType type = _typeService.GetById(id);
                    UpdateModel(type, "type");
                    _typeService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id);
                    return View(_typeViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            object resultado = _typeService.RequestList(sidx, sord, page, rows, filters, (int)user.Company_Id);
            return Json(resultado);
        }

        public ActionResult Index()
        {
            InitializeViews(null);
            return View(_typeViewModel);
        }

        [HttpPost]
        public JsonResult GetFunctionalOrganizationTypesByCompany(int? company_id)
        {
            List<object> types = new List<object>();
            foreach (var type in _typeService.GetFunctionalOrganizationTypesForDropDownList((int)company_id))
            {
                types.Add(
                    new
                    {
                        optionValue = type.Key,
                        optionDisplay = type.Value
                    });
            }

            return Json(types);
        }

        [HttpPost]
        public JsonResult GetFunctionalOrganizationTypesChildrenByType(int? type_id)
        {
            User UserLogged = new UsersServices().GetByUserName(User.Identity.Name);
            List<object> functionalOrganizationTypes = new List<object>();
            Dictionary<int, string> types = type_id.HasValue ?
                                    _typeService.GetFunctionalOrganizationTypesChildrenByTypeForDropDownList(type_id.Value) :
                                    _typeService.GetFunctionalOrganizationTypesForDropDownList(UserLogged.Company_Id);
            foreach (var functionalOrganizationType in types)
            {
                functionalOrganizationTypes.Add(
                    new
                    {
                        optionValue = functionalOrganizationType.Key,
                        optionDisplay = functionalOrganizationType.Value
                    });
            }

            return Json(functionalOrganizationTypes);
        }

        private void InitializeViews(int? type_id)
        {
            FunctionalOrganizationType type;
            SelectList typesList;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());

            if (type_id != null)
            {
                type = _typeService.GetById((int)type_id);
                typesList = new SelectList(new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(user.Company_Id), "Key", "Value", type.FOTParent_Id);
            }
            else
            {
                type = new FunctionalOrganizationType();
                typesList = new SelectList(new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(user.Company_Id), "Key", "Value");
            }
            _typeViewModel = new FunctionalOrganizationTypeViewModel(type, typesList);
        }

        private void ValidateTypeModel(FunctionalOrganizationType type)
        {
            if (_typeService.IsNameDuplicated(type.Company_Id, type.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
        }

    }
}
