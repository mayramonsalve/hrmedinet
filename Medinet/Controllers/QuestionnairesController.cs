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
    //[Authorize(Roles = "HRCompany, HRAdministrator")]
    public class QuestionnairesController : Controller
    {
        
        private QuestionnairesServices _questionnaireService;
        private QuestionnaireViewModel _questionnaireViewModel;

        public QuestionnairesController()
        {
            _questionnaireService = new QuestionnairesServices();
        }

        public QuestionnairesController(QuestionnairesServices _questionnaireService)
        {
            this._questionnaireService = _questionnaireService;
        }

        private bool GetAuthorization(Questionnaire questionnaire)
        {
            User userLogged = new UsersServices().GetByUserName(User.Identity.Name);
            return new SharedHrAuthorization(userLogged,
                new CompaniesServices().GetById(new UsersServices().GetById(questionnaire.User_Id).Company_Id),
                userLogged.Company.CompaniesType.Name=="Owner").isAuthorizated();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Create(int? questionnaire_id)
        {
            if (questionnaire_id == 0)
                questionnaire_id = null;
            InitializeViews(questionnaire_id);
            return View(_questionnaireViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Create(Questionnaire questionnaire)
        {
                questionnaire.CreationDate = DateTime.Now;
                questionnaire.User_Id = new UsersServices().GetByUserName(User.Identity.Name).Id;
                User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
                if (user.Role.Name == "HRAdministrator")
                    questionnaire.Template = true;
                else
                    questionnaire.Template = false;
                ValidateQuestionnaireModel(questionnaire);
                if (ModelState.IsValid)
                {
                    if (_questionnaireService.Add(questionnaire))
                        return RedirectToAction("Index");
                }
                InitializeViews(null);
                return View(_questionnaireViewModel);
        }

        [Authorize(Roles = "HRCompany")]
        [HttpPost]
        public ActionResult CreateWithTemplate(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                int template_id = int.Parse(collection["Templates"]);
                User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
                Questionnaire template = _questionnaireService.GetById(template_id);
                Questionnaire new_questionnaire = new Questionnaire();
                new_questionnaire.User_Id = new UsersServices().GetByUserName(User.Identity.Name).Id;
                new_questionnaire.CreationDate = DateTime.Now;
                new_questionnaire.Template = false;
                new_questionnaire.Name = "New " + template.Name;
                new_questionnaire.Description = template.Description;
                new_questionnaire.Instructions = template.Instructions;
                ValidateQuestionnaireModel(new_questionnaire);
                if (_questionnaireService.Add(new_questionnaire))
                {
                    if (new CategoriesServices().AddCategoryTemplate(template_id, new_questionnaire.Id, user))
                        if (new OptionsServices().AddOptionsTemplate(template_id, new_questionnaire.Id, user))
                            return RedirectToAction("Edit", new { @id = new_questionnaire.Id });
                }
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_questionnaireService.GetById(id)))
            {
                try
                {
                    _questionnaireService.Delete(id);
                    _questionnaireService.SaveChanges();
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

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_questionnaireService.GetById(id)))
            {
                InitializeViews(id);
                return View(_questionnaireViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_questionnaireService.GetById(id)))
            {
                try
                {
                    Questionnaire questionnaire = _questionnaireService.GetById(id);
                    UpdateModel(questionnaire);
                    _questionnaireService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id);
                    return View(_questionnaireViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Index()
        {
            InitializeViews(null);         
            return View(_questionnaireViewModel);
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Details(int id)
        {
            if (GetAuthorization(_questionnaireService.GetById(id)))
            {
                InitializeViews(id);
                return View(_questionnaireViewModel);
            }
            else
                return RedirectToLogOn();
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            object resultado;
            if (user.Role.Name == "HRAdministrator")
            {
                if(user.Company.CompaniesType.Name == "Owner")
                    resultado = _questionnaireService.RequestList(sidx, sord, page, rows, filters);
                else
                    resultado = _questionnaireService.RequestList(user, sidx, sord, page, rows, filters);
            }
            else
                resultado = _questionnaireService.RequestList(user, sidx, sord, page, rows, filters);
            return Json(resultado);
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public JsonResult GetQuestionnairesByCompany(int company_id)
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name);
            List<object> questionnaires = new List<object>();
            QuestionnairesServices questionnaireService = new QuestionnairesServices();
            foreach (var questionnaire in questionnaireService.GetTemplatesByAssociatedForDropDownList(company_id))
            {
                questionnaires.Add(
                    new
                    {
                        optionValue = questionnaire.Key,
                        optionDisplay = questionnaire.Value
                    });
            }
            foreach (var questionnaire in questionnaireService.GetQuestionnairesForCustomerForDropDownList(company_id, user).Distinct())
            {
                questionnaires.Add(
                    new
                    {
                        optionValue = questionnaire.Key,
                        optionDisplay = questionnaire.Value
                    });
            }

            return Json(questionnaires);
        }

        private void InitializeViews(int? questionnaire_id)
        {
            Questionnaire questionnaire;
            SelectList templatesList = null;
            User user = new UsersServices().GetByUserName(User.Identity.Name);
            string role = user.Role.Name;
            if (user.Role.Name == "HRCompany")
                templatesList = new SelectList(_questionnaireService.GetTemplatesByAssociatedForDropDownList(user.Company.CompanyAssociated_Id.Value), "Key", "Value");

            if (questionnaire_id != null)
                questionnaire = _questionnaireService.GetById((int)questionnaire_id);
            else
                questionnaire = new Questionnaire();
            _questionnaireViewModel = new QuestionnaireViewModel(questionnaire, templatesList, role);
        } 

        private void ValidateQuestionnaireModel(Questionnaire questionnaire)
        {
            int company_id = new UsersServices().GetById(questionnaire.User_Id).Company_Id;
            if (_questionnaireService.IsNameDuplicated(company_id, questionnaire.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
        }

        //public ActionResult QuestionnaireError()
        //{
        //    return View();
        //}

        //private bool ValidateTemplate(User user, Questionnaire questionnaire)
        //{
        //    if (user.Role.Name.ToString() == "HRAdministrator" && questionnaire.Template == true)
        //        return true;
        //    return false;
        //}

        //private bool ValidateCompany(User user, Questionnaire questionnaire)
        //{
        //    if(user.Role.Name.ToString() == "HRCompany" && (questionnaire.User.Company_Id == user.Company_Id))
        //        return true;
        //    return false;
        //}
    
    }
}
