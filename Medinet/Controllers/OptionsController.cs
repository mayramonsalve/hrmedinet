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
    [Authorize(Roles = "HRCompany, HRAdministrator")]
    public class OptionsController : Controller
    {
        private OptionsServices _optionService;
        private OptionViewModel _optionViewModel;

        public OptionsController() {
            _optionService = new OptionsServices();
        }

        public OptionsController(OptionsServices _optionService)
        {
            this._optionService = _optionService;
        }

        private bool GetAuthorization(Option option)
        {
            return new SharedHrAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                new CompaniesServices().GetById(new UsersServices().GetById(new QuestionnairesServices().GetById(option.Questionnaire_Id.Value).User_Id).Company_Id), true).isAuthorizated();
        }

        public ActionResult Create(int? questionnaire_id)
        {
            if (questionnaire_id == 0)
                questionnaire_id = null;
            InitializeViews(null,questionnaire_id);
            return View(_optionViewModel);
        }

        [HttpPost]
        public ActionResult Create(Option option)
        {
            option.CreationDate = DateTime.Now;
            ValidateOptionModel(option);
            ImageFileValidation();
            if (ModelState.IsValid)
            {
                if (_optionService.Add(option))
                {
                    UploadImageToServer(option.Id);
                    _optionService.SaveChanges();
                    return RedirectToAction("Index", new { @questionnaire_id = option.Questionnaire_Id });
                }
            }
            InitializeViews(null, null);
            return View(_optionViewModel);
        }

        private void ValidateOptionModel(Option option)
        {
            if (_optionService.IsValueDuplicated(option.Questionnaire_Id.Value, option.Value))
                ModelState.AddModelError(ViewRes.Controllers.Options.Value, ViewRes.Controllers.Options.ValueText);
            if (_optionService.IsTextDuplicated(option.Questionnaire_Id.Value, option.Text))
                ModelState.AddModelError(ViewRes.Controllers.Options.Text, ViewRes.Controllers.Options.TextText);
            if (_optionService.IsThereTenOptions(option.Questionnaire_Id.Value))
                ModelState.AddModelError(ViewRes.Controllers.Options.OptionsCount, ViewRes.Controllers.Options.OptionsCountText);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (GetAuthorization(_optionService.GetById(id)))
            {
                try
                {
                    DeleteAnswersOnCascade(id);
                    _optionService.Delete(id);
                    _optionService.SaveChanges();
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

        public void DeleteAnswersOnCascade(int option_id)
        {
            SelectionAnswersServices selectionAnswerService = new SelectionAnswersServices();
            var option = new OptionsServices().GetById(option_id);
            foreach (var selectiontAnswer in option.SelectionAnswers)
            {
                selectionAnswerService.Delete(selectiontAnswer.Id);
                selectionAnswerService.SaveChanges();
            }
        }

        public ActionResult Details(int id)
        {
            if (GetAuthorization(_optionService.GetById(id)))
            {
                InitializeViews(id, null);
                return View(_optionViewModel);
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_optionService.GetById(id)))
            {
                InitializeViews(id, null);
                return View(_optionViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_optionService.GetById(id)))
            {
                try
                {
                    Option option = _optionService.GetById(id);
                    UpdateModel(option, "Option");
                    if (option.Image != null)
                    {
                        ImageFileValidation();
                        UploadImageToServer(option.Id);
                    }
                    _optionService.SaveChanges();
                    return RedirectToAction("Index", new { @questionnaire_id = option.Questionnaire_Id });
                }
                catch
                {
                    InitializeViews(id, null);
                    return View(_optionViewModel);
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

        public ActionResult GridData(int? questionnaire_id, string sidx, string sord, int page, int rows, string filters)
        {
            if (questionnaire_id != null)
            {
                object resultado = _optionService.RequestList(questionnaire_id.Value, sidx, sord, page, rows, filters);
                return Json(resultado);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Index(int? questionnaire_id)
        {
            InitializeViews(null,questionnaire_id);
            return View(_optionViewModel);
        }

        private void UploadImageToServer(int option_id)
        {
            Option option = _optionService.GetById((int)option_id);

            foreach (string inputTagName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[inputTagName];
                string name = DateTime.Now.ToString("yyyyMMddhhmmss") + option.Id;
                if (file.ContentLength > 0)
                {
                    string[] extension = file.FileName.ToString().Split('.');
                    string filePath = Request.MapPath("~/Content/Images/Options/" + name + "." + extension.Last().ToString());
                    file.SaveAs(filePath);
                    string uImage = name + '.' + extension.Last().ToString();
                    option.Image = uImage;
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
                        ModelState.AddModelError(ViewRes.Controllers.Users.Photo, ViewRes.Controllers.Users.PhotoSizeText);
                    }
                    else if (!formatos.Contains(ctype))
                    {
                        ModelState.AddModelError(ViewRes.Controllers.Users.Photo, ViewRes.Controllers.Users.PhotoFormatText);
                    }
                }
            }
        }

        private void InitializeViews(int? option_id, int? questionnaire_id)
        {
            Option option;
            SelectList questionnairesList;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());

            if (option_id != null)
            {
                option = _optionService.GetById(option_id.Value);
                option.Questionnaire = new QuestionnairesServices().GetById(option.Questionnaire_Id.Value);
                if (user.Role.Name == "HRCompany")
                    questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesForCustomerForDropDownList(user.Company_Id, user), "Key", "Value", option.Questionnaire_Id);
                else
                    questionnairesList = new SelectList(new QuestionnairesServices().GetTemplatesByAssociatedForDropDownList(user.Company_Id), "Key", "Value", option.Questionnaire_Id);
            }
            else
            {
                option = new Option();
                if (questionnaire_id != null)
                {
                    if (user.Role.Name == "HRCompany")
                        questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesForCustomerForDropDownList(user.Company_Id, user), "Key", "Value", questionnaire_id);
                    else
                        questionnairesList = new SelectList(new QuestionnairesServices().GetTemplatesByAssociatedForDropDownList(user.Company_Id), "Key", "Value", questionnaire_id);
                    option.Questionnaire_Id = (int)questionnaire_id;
                }
                else
                {
                    if (user.Role.Name == "HRCompany")
                        questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesForCustomerForDropDownList(user.Company_Id, user), "Key", "Value");
                    else
                        questionnairesList = new SelectList(new QuestionnairesServices().GetTemplatesByAssociatedForDropDownList(user.Company_Id), "Key", "Value");
                }
            }
            _optionViewModel = new OptionViewModel(option, questionnairesList);
        }
    }
}
