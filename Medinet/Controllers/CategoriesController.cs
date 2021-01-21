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
    [Authorize(Roles = "HRCompany, HRAdministrator")]
    public class CategoriesController : Controller
    {

        private CategoriesServices _categoryService;
        private CategoryViewModel _categoryViewModel;

        public CategoriesController()
        {
            _categoryService = new CategoriesServices();
        }

        public CategoriesController(CategoriesServices _categoryService)
        {
            this._categoryService = _categoryService;
        }

        private bool GetAuthorization(Category category)
        {
            return new SharedHrAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                new CompaniesServices().GetById(new UsersServices().GetById(new QuestionnairesServices().GetById(category.Questionnaire_Id.Value).User_Id).Company_Id), true).isAuthorizated();
        }

        public ActionResult Create(int? questionnaire_id)
        {
            if (questionnaire_id == 0)
                questionnaire_id = null;
            InitializeViews(null, questionnaire_id);
            return View(_categoryViewModel);
        }

        [HttpPost]
        public ActionResult Create(Category category, FormCollection collection)
        {
            ValidateCategoryModel(category, collection);
            category.CreationDate = DateTime.Now;
            category.User_Id = new UsersServices().GetByUserName(User.Identity.Name.ToString()).Id;
            //if (collection["category.Questionnaire_Id"].ToString() != "" && collection["agrupacion"] == null)
               // category.Questionnaire_Id = null;
            if (ModelState.IsValid)
            {
                if (_categoryService.Add(category))
                    return RedirectToAction("Index", new { @questionnaire_id = category.Questionnaire_Id });
            }
            InitializeViews(null, null);
            return View(_categoryViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_categoryService.GetById(id)))
            {
                try
                {
                    _categoryService.Delete(id);
                    _categoryService.SaveChanges();
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
            if (GetAuthorization(_categoryService.GetById(id)))
            {
                InitializeViews(id, null);
                return View(_categoryViewModel);
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_categoryService.GetById(id)))
            {
                InitializeViews(id, null);
                return View(_categoryViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_categoryService.GetById(id)))
            {
                try
                {
                    Category category = _categoryService.GetById(id);
                    if (collection["category.Questionnaire_Id"].ToString() != "" && collection["agrupacion"].ToString() != "")
                        category.Questionnaire_Id = null;
                    UpdateModel(category, "Category");
                    _categoryService.SaveChanges();
                    return RedirectToAction("Index", new { @questionnaire_id = category.Questionnaire_Id });
                }
                catch
                {
                    InitializeViews(id, null);
                    return View(_categoryViewModel);
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

        public ActionResult Index(int? questionnaire_Id)
        {
            InitializeViews(null, questionnaire_Id);
            return View(_categoryViewModel);
        }


        public JsonResult GetEmptyCategoriesByCompanyByQuestionnaireId(int questionnaire_id)
        {
            List<object> categories = new List<object>();
            int company_id = new QuestionnairesServices().GetById(questionnaire_id).User.Company_Id;
            foreach (var category in _categoryService.GetGroupingCategoriesByCompanyForDropDownList(company_id))
            {
                categories.Add(
                    new
                    {
                        optionValue = category.Key,
                        optionDisplay = category.Value,
                    });
            }
            return Json(categories);
        }

        public JsonResult GetEmptyCategoriesByCompanyId(int company_id)
        {
            List<object> categories = new List<object>();
            foreach (var category in _categoryService.GetGroupingCategoriesByCompanyForDropDownList(company_id))
            {
                categories.Add(
                    new
                    {
                        optionValue = category.Key,
                        optionDisplay = category.Value,
                    });
            }
            return Json(categories);
        }


        public JsonResult GetCategoriesByCompanyTest(int test_id)
        {
            List<object> categories = new List<object>();
            int company_id = new TestsServices().GetById(test_id).Company_Id;
            foreach (var category in _categoryService.GetGroupingCategoriesByCompanyForDropDownList(company_id))
            {
                categories.Add(
                    new
                    {
                        optionValue = category.Key,
                        optionDisplay = category.Value,
                    });
            }
            return Json(categories);
        }


        public JsonResult GetCategoriesByQuestionnaire(int questionnaire_id)
        {
            List<object> categories = new List<object>();
            CategoriesServices categorieService = new CategoriesServices();
            foreach (var category in categorieService.GetCategoriesForDropDownList(questionnaire_id))
            {
                categories.Add(
                    new
                    {
                        optionValue = category.Key,
                        optionDisplay = category.Value,
                    });
            }
            return Json(categories);
        }

        //public IQueryable<Category> GetCategoriesByQuestionnaire(int? questionnaire_id)
        //{
        //    if (questionnaire_id.HasValue)
        //        return new CategoriesServices().GetByQuestionnaire(questionnaire_id);
        //    else
        //        return null;
        //}

        //public string GetWeightedeByTestId(int id)
        //{
        //    return new TestsServices().GetById(id).Weighted.ToString().ToUpper();
        //}

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters, int? questionnaire_id)
        {
            if (questionnaire_id != null)
            {
                object resultado = _categoryService.RequestList(sidx, sord, page, rows, filters, (int)questionnaire_id);
                return Json(resultado);
            }
            else
            {
                return null;
            }
        }

        private void InitializeViews(int? category_id, int? questionnaire_id)
        {
            bool isGroupingCategory;
            Category category;
            SelectList questionnairesList;
            SelectList categoriesList;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            SelectList companiesList = (user.Role.Name != "HRAdministrator") ? new SelectList(new Dictionary<int, string>(), "Key", "Value") :
                                        new SelectList(new CompaniesServices().GetCustomersByAssociatedForDropDownList(user.Company_Id), "Key", "Value");
            //bool? weighted = null;
            if (category_id != null)
            {
                category = _categoryService.GetById((int)category_id);
                isGroupingCategory = !category.Questionnaire_Id.HasValue;
                categoriesList = new SelectList(_categoryService.GetGroupingCategoriesByCompanyForDropDownList(user.Company_Id), "Key", "Value", category.CategoryGroup_Id);
                //weighted = category.Questionnaire.Weighted;
                if (user.Role.Name == "HRCompany")
                    questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesForCustomerForDropDownList(user.Company_Id, user), "Key", "Value", category.Questionnaire_Id);
                else
                    questionnairesList = new SelectList(new QuestionnairesServices().GetTemplatesByAssociatedForDropDownList(user.Company_Id), "Key", "Value", category.Questionnaire_Id);
            }
            else
            {
                isGroupingCategory = false;
                category = new Category();
                categoriesList = new SelectList(_categoryService.GetGroupingCategoriesByCompanyForDropDownList(user.Company_Id), "Key", "Value");
                if (questionnaire_id != null)
                {
                    category.Questionnaire_Id = questionnaire_id.Value;
                    //weighted = new QuestionnairesServices().GetById(questionnaire_id.Value).Weighted;
                    if (user.Role.Name == "HRCompany")
                        questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesForCustomerForDropDownList(user.Company_Id, user), "Key", "Value", questionnaire_id);
                    else
                        questionnairesList = new SelectList(new QuestionnairesServices().GetTemplatesByAssociatedForDropDownList(user.Company_Id), "Key", "Value", questionnaire_id);
                }
                else
                {
                    if (user.Role.Name == "HRCompany")
                        questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesForCustomerForDropDownList(user.Company_Id, user), "Key", "Value");
                    else
                        questionnairesList = new SelectList(new QuestionnairesServices().GetTemplatesByAssociatedForDropDownList(user.Company_Id), "Key", "Value");
                }
            }
            _categoryViewModel = new CategoryViewModel(category, questionnairesList, categoriesList, companiesList, isGroupingCategory, user);
        }

        private void ValidateCategoryModel(Category category, FormCollection collection)
        {
            if (collection["category.Questionnaire_Id"].ToString() == "" && collection["agrupacion"].ToString() == "false")//si cuestionario no trae valor del form y la opcion es categoria de agrpacion? es vacia
                ModelState.AddModelError(ViewRes.Controllers.Tests.Questionnaire, ViewRes.Controllers.Tests.QuestionnaireText);
            if (collection["category.Questionnaire_Id"].ToString() != "" && collection["agrupacion"].ToString() != "false")//si cuestionario trae valor del form y la opcion es categoria de agrupacion? esta activa
                category.Questionnaire_Id = null;
            if (collection["category.Company_Id"].ToString() == "")//si compania no trae valor del form
                ModelState.AddModelError(ViewRes.Controllers.Categories.CompanyNull, ViewRes.Controllers.Categories.CompanyNullText);

            if (collection["category.Questionnaire_Id"].ToString() != "" && collection["category.Company_Id"].ToString() != "") //si cuestionario y compa;ia traen valor
            {
                if (_categoryService.IsNameDuplicated(category.Questionnaire_Id, category.Company_Id, category.Name))
                    ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);            
            }  

        }

    }
}
