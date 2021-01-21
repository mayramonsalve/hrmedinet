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
    [Authorize(Roles = "Administrator")]
    public class CompaniesController : Controller
    {
        private CompaniesServices _companyService;
        private CompanyViewModel _companyViewModel;

        public CompaniesController()
        {
            _companyService = new CompaniesServices();
        }

        public CompaniesController(CompaniesServices _companyService)
        {
            this._companyService = _companyService;
        }

        private bool GetAuthorization(Company company, bool create)
        {
            return new CompaniesAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
            company, create).isAuthorizated();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            InitializeViews(null);
            return View(_companyViewModel);
        }

        [HttpPost]
        public ActionResult Create(Company company)
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name);
            if (user.Company.CompaniesType.Name == "Associated")
                company.CompanyType_Id = 3;
            if (GetAuthorization(company, true))
            {
                ValidateCompanyModel(company);
                company.CompanyAssociated_Id = user.Company_Id;
                if (ModelState.IsValid)
                {
                    if (_companyService.Add(company))
                    {
                        UploadImageToServer(company.Id);
                        _companyService.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                InitializeViews(null);
                return View(_companyViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            InitializeViews(id);
            return View(_companyViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_companyService.GetById(id), false))
            {
                try
                {
                    _companyService.Delete(id);
                    _companyService.SaveChanges();
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

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_companyService.GetById(id), false))
            {
                InitializeViews(id);
                return View(_companyViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_companyService.GetById(id), false))
            {
                try
                {
                    bool change = false;
                    Company company = _companyService.GetById(id);
                    ImageFileValidation();
                    if (collection["company.Image"] != null)
                        change = true;
                    UpdateModel(company, "Company");
                    if(change)
                        UploadImageToServer(company.Id);
                    _companyService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id);
                    return View(_companyViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            InitializeViews(null);
            return View(_companyViewModel);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int id)
        {
            if (GetAuthorization(_companyService.GetById(id), false))
            {
                InitializeViews(id);
                return View(_companyViewModel);
            }
            else
                return RedirectToLogOn();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        public string GetTypeById(int? id)
        {
            if (id.HasValue)
                return _companyService.GetById(id.Value).CompaniesType.Name.ToUpper();
            else
                return "";
        }

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            object resultado = _companyService.RequestList(new UsersServices().GetByUserName(User.Identity.Name),sidx, sord, page, rows, filters);
            return Json(resultado);
        }

        private void InitializeViews(int? company_id)
        {
            Company company;
            SelectList companiesTypesList;
            SelectList companySectorList;
            string companyType = new UsersServices().GetByUserName(User.Identity.Name).Company.CompaniesType.Name;
            
            if (company_id != null)
            {
                company = _companyService.GetById((int)company_id);
                companySectorList = new SelectList(new CompanySectorsServices().GetCompanySectorsForDropDownList(), "Key", "Value", company.CompanySector_Id);
                if(companyType=="Owner")
                    companiesTypesList = new SelectList(new CompaniesTypesServices().GetCompaniesTypesForOwner(), "Key", "Value", company.CompanyType_Id);
                else
                    companiesTypesList = new SelectList(new CompaniesTypesServices().GetEmptyDictionary(), "Key", "Value");
            }
            else
            {
                company = new Company();
                companySectorList = new SelectList(new CompanySectorsServices().GetCompanySectorsForDropDownList(), "Key", "Value");
                if (companyType == "Owner")
                    companiesTypesList = new SelectList(new CompaniesTypesServices().GetCompaniesTypesForOwner(), "Key", "Value");
                else
                    companiesTypesList = new SelectList(new CompaniesTypesServices().GetEmptyDictionary(), "Key", "Value");
            }

            _companyViewModel = new CompanyViewModel(company, companiesTypesList, companySectorList, companyType);
        }

        private void ValidateCompanyModel(Company company)
        {
            if (_companyService.IsNameDuplicated(company.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
            if (_companyService.IsNumberDuplicated(company.Number))
                ModelState.AddModelError(ViewRes.Controllers.Companies.Number, ViewRes.Controllers.Companies.NumberText);
            if (_companyService.IsPhoneDuplicated(company.Phone))
                ModelState.AddModelError(ViewRes.Controllers.Companies.Phone, ViewRes.Controllers.Companies.Phone);
            if (_companyService.IsUrlDuplicated(company.Url))
                ModelState.AddModelError(ViewRes.Controllers.Companies.Url, ViewRes.Controllers.Companies.Url);
            if (company.CompanySector_Id == null && company.ShowClimate == true)
                ModelState.AddModelError(ViewRes.Controllers.Companies.ShowClimate, ViewRes.Controllers.Companies.ShowClimateInvalid);
        }

        private void UploadImageToServer(int company_id)
        {
            Company company = _companyService.GetById((int)company_id);

            foreach (string inputTagName in Request.Files)
            {
                var file = Request.Files[inputTagName] as HttpPostedFileBase;
                string name = DateTime.Now.ToString("yyyyMMddhhmmss") + company.Id;
                if (file.ContentLength > 0)
                {
                    string[] extension = file.FileName.ToString().Split('.');
                    string filePath = Request.MapPath("~/Content/Images/Companies/" + name + "." + extension.Last().ToString());
                    file.SaveAs(filePath);
                    string cImage = name + '.' + extension.Last().ToString();
                    company.Image = cImage;
                }
            }
        }

        private void ImageFileValidation()
        {
            string[] formatos = { "image/pjpeg", "image/jpeg", "image/gif", "image/png" };

            foreach (string inputTagName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[inputTagName];

                if (file.ContentLength > 0)
                {
                    var ctype = file.ContentType;
                    int tamano = file.ContentLength / 1024;

                    if (tamano > 500)
                    {
                        ModelState.AddModelError(ViewRes.Controllers.Companies.Image, ViewRes.Controllers.Companies.ImageSizeText);
                    }
                    else if (!formatos.Contains(ctype))
                    {
                        ModelState.AddModelError(ViewRes.Controllers.Companies.Image, ViewRes.Controllers.Companies.ImageFormatText);
                    }
                }
            }
        }
    }
}
