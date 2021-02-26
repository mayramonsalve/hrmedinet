using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.Models;
using Medinet.Models.ViewModels;
using MedinetClassLibrary.AuthorizationModels;
using System.IO;
using System.Configuration;
using System.Data;
using Medinet.Class.CustomExcelClass;
using Medinet.Models.CustomExcelModels;

namespace Medinet.Controllers
{
    [HandleError]
    //[Authorize(Roles = "HRCompany, HRAdministrator")]
    public class QuestionnairesController : Controller
    {
        
        private QuestionnairesServices _questionnaireService;
        private CategoriesServices _categoryService;
        private QuestionsServices _questionsServices;
        private QuestionsTypeServices _questionsType;
        private OptionsServices _optionsServices; 
        private QuestionnaireViewModel _questionnaireViewModel;
        private LoadExcelViewModel _loadExcelViewModel;

        public QuestionnairesController()
        {
            _questionnaireService = new QuestionnairesServices();
            _categoryService = new CategoriesServices();
            _questionsServices = new QuestionsServices();
            _questionsType = new QuestionsTypeServices();
            _optionsServices = new OptionsServices();
        }

        public QuestionnairesController(QuestionnairesServices _questionnaireService,
                                                    CategoriesServices _categoryService,
                                                    QuestionsServices _questionsServices,
                                                    QuestionsTypeServices _questionsType,
                                                    OptionsServices _optionsServices)
        {
            this._questionnaireService = _questionnaireService;
            this._categoryService = _categoryService;
            this._questionsServices = _questionsServices;
            this._questionsType = _questionsType;
            this._optionsServices = _optionsServices;
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

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult LoadExcel()
        {
            //return View(new LoadExcelViewModel(new LoadExcel()));
            return View(new LoadExcelViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult LoadExcel(HttpPostedFileBase postedFile)
        {
           
            if (postedFile != null && postedFile.ContentLength > (1024 * 1024 * 50))  // 50MB limit  
            {
                ModelState.AddModelError("postedFile", "Your file is to large. Maximum size allowed is 50MB !");
            }

            if (postedFile != null)
            {
                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };
                string filePath = string.Empty;
                string path = Server.MapPath("~/Uploads/");
                ExcelRead dt = new ExcelRead();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                string conString = string.Empty;
                if (validFileTypes.Contains(extension))
                {
                    if (extension == ".csv")
                    {
                        dt = Utility.ConvertCSVtoDataTable(filePath);
                        ViewBag.Data = dt;
                    }
                    //Connection String to Excel Workbook  
                    else if (extension.Trim() == ".xls")
                    {
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        dt = Utility.ConvertXSLXtoDataTable(filePath, conString);
                        ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        dt = Utility.ConvertXSLXtoDataTable(filePath, conString);
                        ViewBag.Data = dt;
                    }
                }
                else
                {
                    ModelState.AddModelError("postedFile","");
                }

                try
                {

                    foreach (var ex in dt.excelContent)
                    {
                        int id = _questionsType.GetAllRecords().Where(x => x.Name == ex.Type).FirstOrDefault().Id;

                        if (string.IsNullOrEmpty(ex.Category) || string.IsNullOrEmpty(ex.Question))
                        {
                            //return View(new LoadExcelViewModel(new LoadExcel()));
                            return View(new LoadExcelViewModel());
                        }

                        if (id == 1)
                        {
                            if (string.IsNullOrEmpty(ex.Option) || string.IsNullOrEmpty(ex.Value))
                            {
                                //return View(new LoadExcelViewModel(new LoadExcel()));
                                return View(new LoadExcelViewModel());
                            }
                        }

                        if (id == 3)
                        {
                            if (string.IsNullOrEmpty(ex.Option) || string.IsNullOrEmpty(ex.Value))
                            {
                                //return View(new LoadExcelViewModel(new LoadExcel()));
                                return View(new LoadExcelViewModel());
                            }

                            if (dt.excelContent.Where(x => x.Question == ex.Question && x.Type == ex.Type).Count() != 2)
                            {
                                //return View(new LoadExcelViewModel(new LoadExcel()));
                                return View(new LoadExcelViewModel());
                            }
                        }
                    }

                    Questionnaire questionnaire = new Questionnaire();
                    Category category;

                    List<Question> Listquestion = new List<Question>();
                    List<Category> Listcategory = new List<Category>();
                    List<Option> Listoption = new List<Option>();

                    Option option;

                    questionnaire.Name = dt.Name;
                    questionnaire.Description = dt.Name;
                    questionnaire.CreationDate = DateTime.Now;
                    questionnaire.User_Id = new UsersServices().GetByUserName(User.Identity.Name).Id;
                    questionnaire.Template = true;
                    questionnaire.Instructions = dt.Name;

                    _questionnaireService.Add(questionnaire);

                    string LastCategory = "";
                    string LastQuestion = "";

                    foreach (var ex in dt.excelContent.Select(x => x.Category).Distinct())
                    {

                        if (LastCategory != ex)
                        {
                            category = new Category();
                            int i = 1;
                            category.Name = ex;
                            category.Questionnaire_Id = questionnaire.Id;
                            category.CreationDate = DateTime.Now;
                            category.User_Id = new UsersServices().GetByUserName(User.Identity.Name).Id;
                            category.Description = ex;

                            _categoryService.Add(category);
                            Listcategory.Add(category);

                            LastCategory = ex;

                            foreach (var ez in dt.excelContent.Where(x => x.Category == ex).Select(x => new { x.Question, x.Positive, x.Type }).Distinct())
                            {
                                if (LastQuestion != ez.Question)
                                {

                                    Question question = new Question();

                                    question.Category_Id = category.Id;
                                    question.Text = ez.Question;
                                    question.CreationDate = DateTime.Now;
                                    question.Positive = (ez.Positive == "VERDADERO" || ez.Positive == int.Parse("1").ToString()) ? true : false;
                                    question.QuestionType_Id = _questionsType.GetAllRecords().Where(x => x.Name == ez.Type).FirstOrDefault().Id;
                                    question.SortOrder = i;

                                    _questionsServices.Add(question);
                                    Listquestion.Add(question);

                                    LastQuestion = ez.Question;
                                    i += 1;
                                }
                            }
                        }
                    }

                    foreach (var q in Listquestion)
                    {
                        if (q.QuestionType_Id == 1)
                        {
                            dt.excelContent.Where(x => x.Question == q.Text).ToList().ForEach(x =>
                            {
                                option = new Option();
                                option.Text = x.Option;
                                option.CreationDate = DateTime.Now;
                                option.Value = int.Parse(x.Value);
                                option.Question_Id = q.Id;
                                option.Questionnaire_Id = questionnaire.Id;

                                _optionsServices.Add(option);
                            });
                        }

                        if (q.QuestionType_Id == 3)
                        {
                            if (!string.IsNullOrEmpty(dt.excelContent.Where(x => x.Question == q.Text).FirstOrDefault().Option) &&

                                !string.IsNullOrEmpty(dt.excelContent.Where(x => x.Question == q.Text).FirstOrDefault().Value))
                            {
                                dt.excelContent.Where(x => x.Question == q.Text).ToList().ForEach(x =>
                                {
                                    option = new Option();
                                    option.Text = x.Option;
                                    option.CreationDate = DateTime.Now;
                                    option.Value = int.Parse(x.Value);
                                    option.Question_Id = q.Id;
                                    option.Questionnaire_Id = questionnaire.Id;

                                    _optionsServices.Add(option);
                                });
                            }
                        }

                    }


                }
                catch (Exception e)
                {
                    return Json("error" + e.Message);
                }
                //return RedirectToAction("Index");
            }

            //return View(postedFile);  
            //return View(new LoadExcelViewModel(new LoadExcel()));
            return RedirectToAction("Index");


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
