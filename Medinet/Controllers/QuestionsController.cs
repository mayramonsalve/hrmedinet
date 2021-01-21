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
    public class QuestionsController : Controller
    {
        private QuestionsServices _questionsServices;
        private QuestionViewModel _questionsViewModel;

        public QuestionsController()
        {
            _questionsServices = new QuestionsServices();
        }

        public QuestionsController(QuestionsServices _questionsServices)
        {
            this._questionsServices = _questionsServices;
        }

        private bool GetAuthorization(Question question)
        {
            return new SharedHrAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                new CompaniesServices().GetById(new UsersServices().GetById(new QuestionnairesServices().GetById(new CategoriesServices().GetById(question.Category_Id).Questionnaire_Id.Value).User_Id).Company_Id), true).isAuthorizated();
        }
        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Create(int? questionnaire_id)
        {
            if (questionnaire_id == 0)
                questionnaire_id = null;
            InitializeViews(null, questionnaire_id);
            return View(_questionsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Create(Question question)
        {
            ValidateQuestionModel(question);
            question.CreationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (_questionsServices.Add(question))
                    return RedirectToAction("Index", new { @questionnaire_id = question.Category.Questionnaire_Id });
            }
            InitializeViews(null, null);
            return View(_questionsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_questionsServices.GetById(id)))
            {
                try
                {
                    _questionsServices.Delete(id);
                    _questionsServices.SaveChanges();
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

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Details(int id)
        {
            if (GetAuthorization(_questionsServices.GetById(id)))
            {
                InitializeViews(id, null);
                return View(_questionsViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_questionsServices.GetById(id)))
            {
                InitializeViews(id, null);
                return View(_questionsViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_questionsServices.GetById(id)))
            {
                try
                {
                    Question question = _questionsServices.GetById(id);
                    UpdateModel(question, "Question");
                    _questionsServices.SaveChanges();
                    return RedirectToAction("Index", new { @questionnaire_id = question.Category.Questionnaire_Id });
                }
                catch
                {
                    InitializeViews(id, null);
                    return View(_questionsViewModel);
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

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
        public JsonResult GetQuestionsByCategory(int category_id)
        {
            List<object> questions = new List<object>();
            QuestionsServices questionService = new QuestionsServices();
            foreach (var question in questionService.GetQuestionsByCategory(category_id))
            {
                questions.Add(
                    new
                    {
                        optionValue = question.Key,
                        optionDisplay = question.Value
                    });
            }

            return Json(questions);
        }

        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
        public JsonResult GetQuestionsByCategoryAndType(int category_id, int type_id)
        {
            List<object> questions = new List<object>();
            QuestionsServices questionService = new QuestionsServices();
            foreach (var question in questionService.GetQuestionsByCategoryAndType(category_id, type_id))
            {
                questions.Add(
                    new
                    {
                        optionValue = question.Key,
                        optionDisplay = question.Value
                    });
            }

            return Json(questions);
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public JsonResult GetQuestionsForOptionsByCategory(int category_id)
        {
            List<object> questions = new List<object>();
            QuestionsServices questionService = new QuestionsServices();
            foreach (var question in questionService.GetQuestionsForOptionsByCategory(category_id))
            {
                questions.Add(
                    new
                    {
                        optionValue = question.Key,
                        optionDisplay = question.Value
                    });
            }

            return Json(questions);
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult GridData(int? questionnaire_id, string sidx, string sord, int page, int rows, string filters)
        {
            if (questionnaire_id != null)
            {
                object resultado = _questionsServices.RequestList((int)questionnaire_id, sidx, sord, page, rows, filters);
                return Json(resultado);
            }
            else
            {
                return null;
            }
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Index(int? questionnaire_id)
        {
            InitializeViews(null,questionnaire_id);
            return View(_questionsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator, CompanyManager")]
        public JsonResult GetQuestionByCategory(int category_id)
        {
            List<object> questions = new List<object>();

            foreach (var question in _questionsServices.GetQuestionByCategory(category_id))
            {
                questions.Add(
                    new
                    {
                        optionValue = question.Key,
                        optionDisplay = question.Value
                    });
            }

            return Json(questions);
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public int? GetNextSortOrderByQuestionnaire(int? id)
        {
            if (id.HasValue)
                return _questionsServices.GetNextSortOrderByQuestionnaire(id.Value);
            else
                return null;
        }

        private void InitializeViews(int? question_id, int? questionnaire_id)
        {
            Question question;
            SelectList categoriesList;
            SelectList questionnairesList;
            SelectList questionsTypeList;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());

            if (question_id != null)
            {
                question = _questionsServices.GetById((int)question_id);
                if (user.Role.Name == "HRCompany")
                    questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesForCustomerForDropDownList(user.Company_Id, user), "Key", "Value", question.Category.Questionnaire_Id);
                else
                    questionnairesList = new SelectList(new QuestionnairesServices().GetTemplatesByAssociatedForDropDownList(user.Company_Id), "Key", "Value", question.Category.Questionnaire_Id);
                categoriesList = new SelectList(new CategoriesServices().GetCategoriesForDropDownList(question.Category.Questionnaire.Id), "Key", "Value", question.Category.Id);
                questionsTypeList = new SelectList(new QuestionsTypeServices().GetQuestionsTypesForDropDownList(), "Key", "Value", question.QuestionType_Id);
            }
            else
            {
                question = new Question();
                question.Positive = true;
                if (questionnaire_id != null)
                {
                    Category category = new Category();
                    category.Questionnaire_Id = questionnaire_id.Value;
                    question.Category = category;
                    question.SortOrder = _questionsServices.GetNextSortOrderByQuestionnaire(questionnaire_id.Value);
                    if (user.Role.Name == "HRCompany")
                        questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesForCustomerForDropDownList(user.Company_Id, user), "Key", "Value", questionnaire_id);
                    else
                        questionnairesList = new SelectList(new QuestionnairesServices().GetTemplatesByAssociatedForDropDownList(user.Company_Id), "Key", "Value", questionnaire_id);
                    categoriesList = new SelectList(new CategoriesServices().GetCategoriesForDropDownList(questionnaire_id), "Key", "Value");
                }
                else
                {
                    if (user.Role.Name == "HRCompany")
                        questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesForCustomerForDropDownList(user.Company_Id, user), "Key", "Value");
                    else
                        questionnairesList = new SelectList(new QuestionnairesServices().GetTemplatesByAssociatedForDropDownList(user.Company_Id), "Key", "Value");
                    categoriesList = new SelectList(new CategoriesServices().GetEmptyDictionary(), "Key", "Value");
                }
                questionsTypeList = new SelectList(new QuestionsTypeServices().GetQuestionsTypesForDropDownList(), "Key", "Value");
            }

            _questionsViewModel = new QuestionViewModel(question, categoriesList, questionnairesList, questionsTypeList);
        }

        private void ValidateQuestionModel(Question question)
        {
            if (question.Category_Id != 0)
            {
                Category category = new CategoriesServices().GetById(question.Category_Id);
                if (_questionsServices.IsTextDuplicated(category.Questionnaire_Id.Value, question.Text))
                    ModelState.AddModelError(ViewRes.Controllers.Questions.Text, ViewRes.Controllers.Questions.TextText);
                if (_questionsServices.IsSortOrderValid(category.Questionnaire_Id.Value, (int)question.SortOrder))
                    ModelState.AddModelError(ViewRes.Controllers.Questions.SortOrder, ViewRes.Controllers.Questions.SortOrderText);
            }
        }
    }
}
