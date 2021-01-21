using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.Models;
using Medinet.Models.ViewModels;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.CustomClasses;
using System.Management;
using System.Web.Services;
using System.Dynamic;
using System.Net.Mail;
using System.Net;

namespace Medinet.Controllers
{
    [HandleError]
    public class EvaluationsController : Controller
    {
        private EvaluationsServices _evaluationService;
        private EvaluationViewModel _evaluationViewModel;
        private int[][] Selected_FO = new int [2][];

        public EvaluationsController()
        {
            _evaluationService = new EvaluationsServices();
        }
        
        public EvaluationsController(EvaluationsServices _evaluationService)
        {
            this._evaluationService = _evaluationService;
        }


        public ActionResult AnswerTest(string code, int? evaluation_id)
        {
            InitializeViewsForDemographics(code, evaluation_id);
            //if(_evaluationViewModel.test.GroupByCategories)
            //    return View("GroupByCategories", _evaluationViewModel);
            //else
                return View("GroupBySortOrder", _evaluationViewModel);
        }

        public bool ValidateAnswerCount(FormCollection collection, Evaluation evaluation)
        {
            bool contains;
            int questionnaireId;
            if (new TestsServices().GetById(evaluation.Test_Id).OneQuestionnaire)
                questionnaireId = new TestsServices().GetById(evaluation.Test_Id).Questionnaire_Id.Value;
            else
            {
                questionnaireId = GetQuestionnaireIdFromEvaluation(evaluation);
            }
            int count = collection.AllKeys.ToList().FindAll(findQ).Count;
            contains = new QuestionsServices().GetQuestionsCountByQuestionnaire(questionnaireId) == count;
            return contains;
        }

        private static bool findQ(string question)
        {
            return question.StartsWith("q[");
        }

        private static int GetQuestionnaireIdFromEvaluation(Evaluation evaluation)
        {
            Test test = new TestsServices().GetById(evaluation.Test_Id);
            DemographicsInTest demographicInTest = new DemographicsInTestsServices().GetSelector(test.Id);
            int selectorFromEvaluation = GetSelectorId(evaluation, test, demographicInTest);
            int questionnaire_id = new DemographicSelectorDetailsServices().GetQuestionnaireIdByDemographicSelectorDetailValues(test.Id, demographicInTest.Demographic_Id, selectorFromEvaluation);
            return questionnaire_id;
        }

        private static int GetSelectorId(Evaluation evaluation, Test test, DemographicsInTest demographicInTest)
        {
            switch (demographicInTest.Demographic.Name)
            {
                case "Gender":
                    return evaluation.Sex == "Female" ? 0 : 1;
                case "AgeRange":
                    return evaluation.Age_Id.Value;
                case "InstructionLevel":
                    return evaluation.InstructionLevel_Id.Value;
                case "Location":
                    return evaluation.Location_Id.Value;
                case "Performance":
                    return  evaluation.Performance_Id.Value;
                case "PositionLevel":
                    return evaluation.PositionLevel_Id.Value;
                case "Seniority":
                    return evaluation.Seniority_Id.Value;
                case "Country":
                    return evaluation.Location.State.Country_Id;
                case "State":
                    return evaluation.Location.State_Id;
                case "Region":
                    return evaluation.Location.Region_Id.Value;
                case "FunctionalOrganizationType":
                    return evaluation.EvaluationFOs.Where(f => f.FunctionalOrganization.Type_Id == demographicInTest.FOT_Id.Value).FirstOrDefault().FunctionalOrganization_Id;
                default:
                    return 0;
            }
        }

        public bool SendMail(string name, string mail, int evaluation_id)
        {
            try
            {
                var message = new MailMessage("info.hrmedinet@gmail.com", mail + ", info.hrmedinet@gmail.com")
                {
                    Subject = "Ticket info",
                    Body = "<h3>Su opinión es muy importante para nosotros.</h3> " +
                            "<h3>¡Gracias por su participación!</h3> " +
                            "Los datos almacenados son: <br /><br />" +
                           "Nombre / Name: " + name + "<br />" +
                           "Email: " + mail + "<br />" +
                           "Id: " + evaluation_id + "<br /><br />" +
                           "<a href\"http://www.hrmedinet.com\"><img src=\"http://www.hrmedinet.com/Content/Images/bannerCigeh.jpg\"></a>",
                    IsBodyHtml = true
                };
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("info.hrmedinet@gmail.com", "t3n1d3mHr21");
                client.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public ActionResult AnswerTest(FormCollection collection)
        {
            Evaluation evaluation = collection["EvaluationId"].ToString() == "0" ?
                                    GenerateEvaluationObject(collection) : GetEvaluationFromExisting(collection);
            Test test = new TestsServices().GetById(evaluation.Test_Id);
            if (User.Identity.Name.ToLower() == "cigeh" && test.Questionnaire_Id == 32 && existEmailInTicket(collection["q[10033739]"].ToString()))
            {
                ViewData["emailError"] = "El Email ya ha sido utilizado";
                return RedirectToAction("ErrorEvaluation");
            }
            if (ModelState.IsValid)
            {
                if (ValidateAnswerCount(collection, evaluation))
                {
                    if (_evaluationService.Add(evaluation))
                    {
                        new TestsServices().IncreaseCurrentEvaluationsAndDecreaseEvaluationsLeft(evaluation.Test_Id);
                        InsertAnswers(collection, evaluation);
                        InsertFunctionalOrganizations(evaluation);
                        if (User.Identity.Name.ToLower() == "cigeh" && evaluation.Test.Questionnaire_Id == 32)
                        {
                            //Validar correo y guardar ticket
                            GenerateTicket(collection, evaluation.Id);
                            SendMail(collection["q[10033738]"].ToString(), collection["q[10033739]"].ToString(), evaluation.Id);
                        }
                        return RedirectToAction("EvaluationSucceeded", new { @evaluation_id = evaluation.Id });
                    }
                }
                else
                    return RedirectToAction("ErrorEvaluation");
            }
            return RedirectToAction("ErrorClosedEvaluation");
        }

        public ActionResult ErrorClosedEvaluation()
        {
            return View();
        }

        public ActionResult EvaluationSucceeded(int evaluation_id)
        {
            InitializeViewForNextTest(new EvaluationsServices().GetById(evaluation_id));
            return View(_evaluationViewModel);
        }

        public ActionResult ErrorEvaluation()
        {
            return View();
        }

        //public ActionResult RedirectToFeedback()
        //{
        //    Session["Type"] = 2;
        //    System.Threading.Thread.Sleep(5000);
        //    return RedirectToAction("SendFeedback", "Feedbacks");
        //}

        public void InsertFunctionalOrganizations(Evaluation evaluation){
            EvaluationsFOServices _evaluationsFOServices = new EvaluationsFOServices();
            for (int i = 0; i < Selected_FO[0].Count(); i++) {
                EvaluationFO evaluationFO_object = new EvaluationFO();
                evaluationFO_object.Evaluation_Id = evaluation.Id;
                evaluationFO_object.FunctionalOrganization_Id = Selected_FO[1][i];
                _evaluationsFOServices.Add(evaluationFO_object);
            }
        }

        private static TextAnswer GenerateTextAnswer(FormCollection collection, Evaluation evaluation, Question question, string aux)
        {
            TextAnswer t = new TextAnswer();
            t.CreationDate = DateTime.Now;
            t.Evaluation_Id = evaluation.Id;
            t.Question_Id = question.Id;
            t.Text = collection["q[" + question.Id + aux + "]"];
            return t;
        }

        private static DichotomousAnswer GenerateDichotomousAnswer(FormCollection collection, Evaluation evaluation, Question question, string aux)
        {
            DichotomousAnswer d = new DichotomousAnswer();
            d.CreationDate = DateTime.Now;
            d.Evaluation_Id = evaluation.Id;
            d.Question_Id = question.Id;
            d.Affirmative = collection["q[" + question.Id + aux + "]"].ToString() == "1";
            return d;
        }

        public static String GetMACAddress()
        {
            #region Get MAC Address
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                string MACAddress = String.Empty;
                foreach (ManagementObject mo in moc)
                {
                    if (MACAddress == String.Empty) // only return MAC Address from first card  
                    {
                        if ((bool)mo["IPEnabled"] == true) MACAddress = mo["MacAddress"].ToString();
                    }
                    mo.Dispose();
                }

                MACAddress = MACAddress.Replace(":", "");
                return MACAddress;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            #endregion
        }

        private string GetMacAddress2()
        {
            System.Management.ManagementClass mc = new System.Management.ManagementClass("Win32_NetworkAdapterConfiguration");
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            string MACAddress = String.Empty;
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (MACAddress == String.Empty) // only return MAC Address from first card   
                {
                    if ((bool)mo["IPEnabled"] == true) MACAddress = mo["MacAddress"].ToString();
                }
                mo.Dispose();
            }

            MACAddress = MACAddress.Replace(":", "");
            return MACAddress;
        }

        private Evaluation GetEvaluationFromExisting(FormCollection collection)
        {
            Evaluation evaluation = new Evaluation();
            Evaluation existing = new EvaluationsServices().GetById(int.Parse(collection["EvaluationId"]));
            evaluation.Test_Id = int.Parse(collection["evaluation.Test_Id"]);
            evaluation.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
            evaluation.RemoteHostName = GetMACAddress();//collection["MacAddress"];//GetMACAddress(); //Request.ServerVariables["REMOTE_HOST"];
            evaluation.RemoteUserName = GetMacAddress2();//Request.ServerVariables["REMOTE_USER"];
            Test test = new TestsServices().GetById(evaluation.Test_Id);
            List<string> demographics = test.DemographicsInTests.Select(d => d.Demographic.Name).ToList();
            if (demographics.Contains("AgeRange"))
                evaluation.Age_Id = existing.Age_Id;
            if (demographics.Contains("InstructionLevel"))
                evaluation.InstructionLevel_Id = existing.InstructionLevel_Id;
            if (demographics.Contains("PositionLevel"))
                evaluation.PositionLevel_Id = existing.PositionLevel_Id;
            if (demographics.Contains("Seniority"))
                evaluation.Seniority_Id = existing.Seniority_Id;
            if (demographics.Contains("Location"))
                evaluation.Location_Id = existing.Location_Id;
            if (demographics.Contains("Performance"))
                evaluation.Performance_Id = existing.Performance_Id;
            if (demographics.Contains("Gender"))
                evaluation.Sex = existing.Sex;
            Dictionary<int, string> FO = new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(test.Company_Id);
            for (int i = 0; i < 2; i++)
            {
                Selected_FO[i] = new int[FO.Count];
            }
            int j = 0;
            foreach (var v in FO)
            {
                if (test.DemographicsInTests.Where(f => f.FOT_Id == v.Key).Count() == 1)
                {
                    Selected_FO[0][j] = v.Key;
                    Selected_FO[1][j] = existing.EvaluationFOs.Where(t => t.FunctionalOrganization.Type_Id == v.Key).FirstOrDefault().FunctionalOrganization_Id; // Select Functional Organization
                    j++;
                }
            }
            evaluation.CreationDate = DateTime.Now;
            ValidateEvaluationModel(evaluation);
            return evaluation;
        }

        private Evaluation GenerateEvaluationObject(FormCollection collection)
        {       
            Evaluation evaluation = new Evaluation();
            evaluation.Test_Id = int.Parse(collection["evaluation.Test_Id"]);
            evaluation.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
            evaluation.RemoteHostName = GetMACAddress();//collection["MacAddress"];//GetMACAddress(); //Request.ServerVariables["REMOTE_HOST"];
            evaluation.RemoteUserName = GetMacAddress2();//Request.ServerVariables["REMOTE_USER"];
            Test test = new TestsServices().GetById(evaluation.Test_Id);
            List<string> demographics = test.DemographicsInTests.Select(d => d.Demographic.Name).ToList();
            if (demographics.Contains("AgeRange"))
                evaluation.Age_Id = int.Parse(collection["evaluation.Age_Id"]);
            if (demographics.Contains("InstructionLevel"))
                evaluation.InstructionLevel_Id = int.Parse(collection["evaluation.InstructionLevel_Id"]);
            if (demographics.Contains("PositionLevel"))
                evaluation.PositionLevel_Id = int.Parse(collection["evaluation.PositionLevel_Id"]);
            if (demographics.Contains("Seniority"))
                evaluation.Seniority_Id = int.Parse(collection["evaluation.Seniority_Id"]);
            if (demographics.Contains("Location"))
                evaluation.Location_Id = int.Parse(collection["evaluation.Location_Id"]);
            if (demographics.Contains("Performance"))
                evaluation.Performance_Id = int.Parse(collection["evaluation.Performance_Id"]);
            if (demographics.Contains("Gender"))
                evaluation.Sex = GetGender(collection["evaluation.Sex"].ToString());
            Dictionary<int,string> FO = new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(test.Company_Id);
            for (int i = 0; i < 2;i++ ){
                Selected_FO [i] = new int[FO.Count];
            }
            int j=0;
            foreach (var v in FO) 
            {
                if (test.DemographicsInTests.Where(f => f.FOT_Id == v.Key).Count() == 1)
                {
                    if (collection["evaluation_FunctionalOrganization_Type" + v.Value + "_Id"] != "")
                    {
                        Selected_FO[0][j] = v.Key; // Functional Organization Id
                        Selected_FO[1][j] = int.Parse(collection["evaluation_FunctionalOrganizationType_" + v.Value + "_Id"]); // Select Functional Organization
                        j++;
                    }
                }
            }
            evaluation.CreationDate = DateTime.Now;
            ValidateEvaluationModel(evaluation);
            return evaluation;
        }

        private static string GetGender(string gender_collection)
        {
            string gender;
            string aux_gender = gender_collection.ToLowerInvariant();
            if (aux_gender == "female" || aux_gender == "male")
                gender = gender_collection;
            else
            {
                if (aux_gender == "femenino")
                    gender = "Female";
                else
                    gender = "Male";
            }
            return gender;
        }

        private static SelectionAnswer GenerateSelectionAnswer(FormCollection collection, Evaluation evaluation, Question question, string aux)
        {
            SelectionAnswer s = new SelectionAnswer();
            s.Evaluation_Id = evaluation.Id;
            s.CreationDate = DateTime.Now;
            s.Option_Id = Int32.Parse(collection["q[" + question.Id + aux + "]"]);
            int value = new OptionsServices().GetById(s.Option_Id).Value;
            if (!question.Positive)
            {
                value = (new OptionsServices().GetOptionsCount(question.Category.Questionnaire_Id.Value) + 1) - value;
                int questionnaire = (evaluation.Test.Questionnaire_Id.HasValue) ? evaluation.Test.Questionnaire_Id.Value : GetQuestionnaireIdFromEvaluation(evaluation);
                s.Option_Id = new OptionsServices().GetByValue(value, questionnaire).Id;
            }
            s.Question_Id = question.Id;
            return s;
        }

        private void InitializeViewForNextTest(Evaluation evaluation)
        {
            _evaluationViewModel = new EvaluationViewModel(evaluation);
        }

        private void InitializeViewForQuestionsViews(Test test, int idSelectorValue)
        {
            _evaluationViewModel = new EvaluationViewModel(test, idSelectorValue);
        }

        private void InitializeViewsForDemographics(string code, int? previousEvaluationId)
        {
            Evaluation evaluation;
            Test test;
            SelectList agesList;
            SelectList senioritiesList;
            SelectList instructionLevelsList;
            SelectList positionLevelsList;
            SelectList locationsList;
            SelectList performanceEvaluationsList;
            IQueryable<FunctionalOrganizationType> FOTypes;
            SelectList FOrganizations;
            Evaluation previousEvaluation = previousEvaluationId.HasValue ? _evaluationService.GetById(previousEvaluationId.Value) : null;
            evaluation = new Evaluation();
            //if (previousEvaluationId.HasValue)
            //{
            //    evaluation.Age_Id = previousEvaluation.Age_Id;
            //    evaluation.InstructionLevel_Id = previousEvaluation.InstructionLevel_Id;
            //    evaluation.Location_Id = previousEvaluation.Location_Id;
            //    evaluation.Performance_Id = previousEvaluation.Performance_Id;
            //    evaluation.PositionLevel_Id = previousEvaluation.PositionLevel_Id;
            //    evaluation.Seniority_Id = previousEvaluation.Seniority_Id;
            //    evaluation.Sex = previousEvaluation.Sex;
            //    evaluation.EvaluationFOs = previousEvaluation.EvaluationFOs;
            //}
            test = new TestsServices().GetByCode(code);
            evaluation.Test_Id = test.Id;
            agesList = new SelectList(new AgesServices().GetAgesForDropDownList(test.Company_Id), "Key", "Value");
            senioritiesList = new SelectList(new SenioritiesServices().GetSenioritiesForDropDownList(test.Company_Id), "Key", "Value");
            instructionLevelsList = new SelectList(new InstructionLevelsServices().GetInstructionLevelsForDropDownList(test.Company_Id), "Key", "Value");
            positionLevelsList = new SelectList(new PositionLevelsServices().GetPositionLevelsForDropDownList(test.Company_Id), "Key", "Value");
            locationsList = new SelectList(new LocationsServices().GetLocationsForDropDownList(test.Company_Id), "Key", "Value");
            FOTypes = new FunctionalOrganizationTypesServices().GetByCompanyToDoTest(test.Company_Id);
            FOTypes = new FunctionalOrganizationTypesServices().OrderByCompany(FOTypes);
            FOrganizations = new SelectList(new FunctionalOrganizationsServices().GetEmptyDictionary());
            performanceEvaluationsList = new SelectList(new PerformanceEvaluationsServices().GetPerformanceEvaluationsForDropDownList(test.Company_Id), "Key", "Value");
            _evaluationViewModel = new EvaluationViewModel(evaluation, agesList, senioritiesList, instructionLevelsList, positionLevelsList,
                                    locationsList, FOTypes, FOrganizations, performanceEvaluationsList, test, previousEvaluation);
        }

        private void InitializeViewsForTestInstructions(Test test)
        {
            _evaluationViewModel = new EvaluationViewModel(test);
        }

        private void ValidateEvaluationModel(Test test)
        {

            if (test != null)
            {
                if (test.EvaluationNumber <= test.CurrentEvaluations /*test.Evaluations.Count*/)
                    ModelState.AddModelError(ViewRes.Controllers.Tests.EvaluationNumber, ViewRes.Controllers.Tests.EvaluationNumberText);
                //if (new TestsServices().IsIPDuplicated(test, GetMACAddress()) && !IsInHRRole())
                //    ModelState.AddModelError(ViewRes.Controllers.Tests.IPDuplicated, ViewRes.Controllers.Tests.IPDuplicatedText);
                if (test.StartDate >= DateTime.Now)
                    ModelState.AddModelError(ViewRes.Controllers.Tests.StartDate, ViewRes.Controllers.Tests.StartDateText);
                if (DateTime.Now >= test.EndDate)
                    ModelState.AddModelError(ViewRes.Controllers.Tests.EndDate, ViewRes.Controllers.Tests.EndDateText);
            }
            else
            {
                ModelState.AddModelError(ViewRes.Controllers.Tests.CodeNotFound, ViewRes.Controllers.Tests.CodeNotFoundText);
            }

        }

        private static void InsertAnswers(FormCollection collection, Evaluation evaluation)
        {
            int questionnaire = (evaluation.Test.Questionnaire_Id.HasValue) ? evaluation.Test.Questionnaire_Id.Value : GetQuestionnaireIdFromEvaluation(evaluation);
            foreach (Question question in new QuestionsServices().GetByQuestionnaire(questionnaire))
            {
                switch(question.QuestionType_Id)
                {
                    case 1:
                        InsertSelectionAnswer(collection, evaluation, question, "");
                        break;
                    case 2:
                        InsertTextAnswer(collection, evaluation, question, "");
                        break;
                    case 3:
                        InsertDichotomousAnswer(collection, evaluation, question, "");
                        break;
                    case 4:
                        InsertMultipleSelectionAnswer(collection, evaluation, question, "");
                        break;
                    case 5:
                        InsertMixedAnswer(collection, evaluation, question);
                        break;
                }
            }
        }

        private static void InsertMixedAnswer(FormCollection collection, Evaluation evaluation, Question question)
        {
            foreach (int questionType in question.QuestionTypeByQuestions.Select(t => t.QuestionType_Id))
            {
                switch (questionType)
                {
                    case 1:
                        InsertSelectionAnswer(collection, evaluation, question, '-' + questionType.ToString());
                        break;
                    case 2:
                        InsertTextAnswer(collection, evaluation, question, '-' + questionType.ToString());
                        break;
                    case 3:
                        InsertDichotomousAnswer(collection, evaluation, question, '-' + questionType.ToString());
                        break;
                    case 4:
                        InsertMultipleSelectionAnswer(collection, evaluation, question, '-' + questionType.ToString());
                        break;
                }
            }
        }

        private static void InsertMultipleSelectionAnswer(FormCollection collection, Evaluation evaluation, Question question, string aux)
        {
            string options = collection["q[" + question.Id + aux + "]"];
            options = options.Replace(",false", "");
            options = options.Replace("false,", "");
            new SelectionAnswersServices().AddMultiple(evaluation.Id, question.Id, options);
        }

        private static void InsertSelectionAnswer(FormCollection collection, Evaluation evaluation, Question question, string aux)
        {
            SelectionAnswer s = GenerateSelectionAnswer(collection, evaluation, question, aux);
            new SelectionAnswersServices().Add(s);
        }

        private static void InsertTextAnswer(FormCollection collection, Evaluation evaluation, Question question, string aux)
        {
            TextAnswer t = GenerateTextAnswer(collection, evaluation, question, aux);
            new TextAnswersServices().Add(t);
        }

        private static void InsertDichotomousAnswer(FormCollection collection, Evaluation evaluation, Question question, string aux)
        {
            DichotomousAnswer d = GenerateDichotomousAnswer(collection, evaluation, question, aux);
            new DichotomousAnswersServices().Add(d);
        }

        [HttpGet]
        public ActionResult TestInstructions(string code)
        {
            Test test = new TestsServices().GetByCode(code);
            ValidateEvaluationModel(test);
            if (ModelState.IsValid)
            {
                InitializeViewsForTestInstructions(test);
                return View(_evaluationViewModel);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        private void ValidateEvaluationModel(Evaluation evaluation)
        {
            if (new TestsServices().GetById(evaluation.Test_Id).EvaluationNumber <= new TestsServices().GetById(evaluation.Test_Id).Evaluations.Count)
                ModelState.AddModelError(ViewRes.Controllers.Evaluations.EvaluationNumber, ViewRes.Controllers.Evaluations.EvaluationNumberText);
        }

        [HttpPost]
        public ActionResult LoadDivQuestions(int test_id, int idSelectorValue)
        {
            Test test = new TestsServices().GetById(test_id);
            InitializeViewForQuestionsViews(test, idSelectorValue);
            if (test.GroupByCategories)
                return PartialView("GroupByCategoriesPartial", _evaluationViewModel);
            else
                return PartialView("GroupBySortOrderPartial", _evaluationViewModel);
        }

        public JsonResult GetFOTChildren(int foParent_id)
        {
            List<object> data = new List<object>();
            FunctionalOrganization parent = new FunctionalOrganizationsServices().GetById(foParent_id);
            var types = parent.FunctionalOrganizations.Select(g => g.FunctionalOrganizationType).OrderBy(g => g.Name).GroupBy(g => g.Id);
            foreach (var type in types)
            {
                var children = parent.FunctionalOrganizations.Where(i => i.Type_Id == type.First().Id);
                Dictionary<string, string> dicFOChildrenByType = new Dictionary<string, string>();
                foreach (var child in children)
                {
                    dicFOChildrenByType.Add(child.Id.ToString(), child.Name);
                }
                data.Add(
                    new
                    {
                        childId = type.First().Id,
                        childName = type.First().Name,
                        childDictionary = dicFOChildrenByType
                    });                
            }

            return Json(data);
        }

        #region Print

        public ViewResult Prueba()
        {
            return View();
        }
        [HttpGet]
        public ViewResult PrintTest(int id)
        {
            //string code = "code123";
            InitializeViewsForDemographics(new TestsServices().GetById(id).Code, null);
            if (_evaluationViewModel.test.GroupByCategories)
                return View("PrintGroupByCategories", _evaluationViewModel);
            else
                return View("PrintGroupBySortOrder", _evaluationViewModel);
        }
        [HttpGet]
        private string GetHttpUrl()
        {
            string http = "http://" + Request.ServerVariables["SERVER_NAME"];
            if (Request.ServerVariables["SERVER_NAME"] != "www.hrmedinet.com")
                http += ":" + Request.ServerVariables["SERVER_PORT"];
            return http;
        }
        public PdfResult GetMyPdf(int id)
        {
            return new PdfResult(GetHttpUrl() + "/Evaluations/PrintTest?id=" + id, "Test", true);
        }
        #endregion
        public JsonResult CheckEmail(string email)
        {
            return Json(!existEmailInTicket(email));
        }


        //Devuelve true si el email ya fue utilizado en algun ticket
        //De lo contrario false
        public Boolean existEmailInTicket(string email)
        {
            Ticket tick = new TicketsServices().GetByEmail(email);
            if (tick == null)
                return false;
            return true;
        }

        public void GenerateTicket(FormCollection collection,int id)
        {
            
            string name = collection["q[10033738]"].ToString();
            string email = collection["q[10033739]"].ToString();
            Ticket t = new Ticket();
            t.Name = name;
            t.Email = email;
            t.Evaluation_Id = id;
            new TicketsServices().Add(t);
        }

        public ActionResult AllTickets()
        {
            TicketViewModel tvm = new TicketViewModel();
            tvm.tickets = new TicketsServices().GetAllRecords().ToList();
            return View(tvm);
        }

        public ActionResult MobileDemographicsAnswerTest(string code, int? evaluation_id)
        {
            if (!evaluation_id.HasValue)
            {
                MobileInitializeViewsForDemographics(code, evaluation_id);
                if (_evaluationViewModel.test.DemographicsInTests.Count == 0)
                    return RedirectToAction("MobileWithoutDemographicAnswerTest", new { Code = code });
                return View("Demographics.Mobile", _evaluationViewModel);
            }
            else
                return RedirectToAction("MobileWithDemographicAnswerTest", new { Code = code, Id = evaluation_id });
        }

        //Se invoca esta accion cuando se va a responder una encuesta en la cual
        //se ha llenado un test previo y por ende, no hace falta completar el test
        public ActionResult MobileWithoutDemographicAnswerTest(string Code)
        {

            FormCollection collection = new FormCollection();
            collection["evaluation.Test_Id"] = new TestsServices().GetByCode(Code).Id.ToString();
            Evaluation evaluation = GenerateEvaluationObject(collection);
            Test test = new TestsServices().GetById(evaluation.Test_Id);
            int idQuestionnaireToUse;
            Questionnaire questionnaireToUse;
            if (test.OneQuestionnaire)
            {
                idQuestionnaireToUse = test.Questionnaire_Id.Value;
                questionnaireToUse = test.Questionnaire;
                _evaluationViewModel = new EvaluationViewModel(test, evaluation, Selected_FO, idQuestionnaireToUse, questionnaireToUse);
            }
            return View("Questionnaire.Mobile", _evaluationViewModel);
        }


        public ActionResult MobileWithDemographicAnswerTest(string Code, int Id)
        {

            FormCollection collection = new FormCollection();
            collection["EvaluationId"] = Id.ToString();
            collection["evaluation.Test_Id"] = new TestsServices().GetByCode(Code).Id.ToString();
            Evaluation evaluation =  GetEvaluationFromExisting(collection);
            Test test = new TestsServices().GetById(evaluation.Test_Id);
            int idQuestionnaireToUse;
            Questionnaire questionnaireToUse;
            if (test.OneQuestionnaire)
            {
                idQuestionnaireToUse = test.Questionnaire_Id.Value;
                questionnaireToUse = test.Questionnaire;
                _evaluationViewModel = new EvaluationViewModel(test, evaluation, Selected_FO, idQuestionnaireToUse, questionnaireToUse);
            }
            else
            {
                int idSelectorValue = MobileGetSelectorValue(evaluation);
                InitializeViewForQuestionsViews(test, idSelectorValue);
                _evaluationViewModel.SetEvaluation(evaluation);
                _evaluationViewModel.SetSelectedFo(Selected_FO);
            }
            return View("Questionnaire.Mobile", _evaluationViewModel);
        }



        [HttpPost]
        // Se invoca este metodo cuando se responde los datos demograficos
        public ActionResult MobileDemographicsAnswerTest(FormCollection collection)
        {

            if (ModelState.IsValid)
            {

                Evaluation evaluation = collection["evaluation.Id"].ToString() == "0" ?
                            GenerateEvaluationObject(collection) : GetEvaluationFromExisting(collection);
                Test test = new TestsServices().GetById(evaluation.Test_Id);
                int idQuestionnaireToUse;
                Questionnaire questionnaireToUse;
                if (test.OneQuestionnaire)
                {
                    idQuestionnaireToUse = test.Questionnaire_Id.Value;
                    questionnaireToUse = test.Questionnaire;
                    _evaluationViewModel = new EvaluationViewModel(test, evaluation, Selected_FO, idQuestionnaireToUse, questionnaireToUse);
                }
                else
                {
                    int idSelectorValue = MobileGetSelectorValue(evaluation);
                    InitializeViewForQuestionsViews(test, idSelectorValue);
                    _evaluationViewModel.SetEvaluation(evaluation);
                    _evaluationViewModel.SetSelectedFo(Selected_FO);
                }
                return View("Questionnaire.Mobile", _evaluationViewModel);
            }
            return RedirectToAction("ErrorClosedEvaluation");
        }

        // Get the selector value
        private int MobileGetSelectorValue(Evaluation evaluation)
        {
            DemographicsInTest d = new DemographicsInTestsServices().GetSelector(evaluation.Test_Id);
            List<int> listOfId;
            IQueryable<DemographicSelectorDetail> listDSD = new DemographicSelectorDetailsServices().GetByTest(evaluation.Test_Id);
            if (d.FOT_Id.HasValue)
            {
                listOfId = new List<int>();
                for (int k = 0; k < Selected_FO[0].Length; k++){
                    listOfId.Add(Selected_FO[k][1]);
                }
            }
            else
            {
                 listOfId = MobileGeneratesID(evaluation);
            }
            foreach (DemographicSelectorDetail dsd in listDSD)
            {
                    if (listOfId.Contains(dsd.SelectorValue_Id))
                        return dsd.SelectorValue_Id;
            }
            return 0;
        }

        // generate a list of Id of all the demographic selected by the user without the Functional organization
        private List<int> MobileGeneratesID(Evaluation evaluation)
        {
            List<int> lista = new List<int>();
            Test test = new TestsServices().GetById(evaluation.Test_Id);
            List<string> demographics = test.DemographicsInTests.Select(d => d.Demographic.Name).ToList();
            if (demographics.Contains("AgeRange"))
                lista.Add(evaluation.Age_Id.Value);
            if (demographics.Contains("InstructionLevel"))
                lista.Add(evaluation.InstructionLevel_Id.Value);
            if (demographics.Contains("PositionLevel"))
                lista.Add(evaluation.PositionLevel_Id.Value);
            if (demographics.Contains("Seniority"))
                lista.Add(evaluation.Seniority_Id.Value);
            if (demographics.Contains("Location"))
                lista.Add(evaluation.Location_Id.Value);
            if (demographics.Contains("Performance"))
                lista.Add(evaluation.Performance_Id.Value);
            return lista;
        }

        [HttpPost]
        public ActionResult MobileAnswerTest(FormCollection collection)
        {

            Evaluation evaluation = MobileGenerateEvaluationObject(collection);
            Test test = new TestsServices().GetById(evaluation.Test_Id);
            if (User.Identity.Name.ToLower() == "cigeh" && test.Questionnaire_Id == 32 && existEmailInTicket(collection["q[10033739]"].ToString())){
                ViewData["emailError"] = "El Email ya ha sido utilizado";
                return RedirectToAction("ErrorEvaluation");
            }
            if (ModelState.IsValid)
            {
                if (ValidateAnswerCount(collection, evaluation))
                {
                    if (_evaluationService.Add(evaluation))
                    {
                        new TestsServices().IncreaseCurrentEvaluationsAndDecreaseEvaluationsLeft(evaluation.Test_Id);
                        InsertAnswers(collection, evaluation);
                        InsertFunctionalOrganizations(evaluation);
                        if (User.Identity.Name.ToLower() == "cigeh" && test.Questionnaire_Id == 32)
                        {
                            GenerateTicket(collection, evaluation.Id);
                            SendMail(collection["q[10033738]"].ToString(), collection["q[10033739]"].ToString(), evaluation.Id);
                        }
                        return RedirectToAction("EvaluationSucceeded", new { @evaluation_id = evaluation.Id });
                    }
                }
                else
                    return RedirectToAction("ErrorEvaluation");
            }
            return View("Questionnaire.Mobile", _evaluationViewModel);
        }

        private void MobileInitializeViewsForDemographics(string code, int? previousEvaluationId)
        {
            //Evaluation previousEvaluation = previousEvaluationId.HasValue ? _evaluationService.GetById(previousEvaluationId.Value) : null;
            Evaluation previousEvaluation = new Evaluation();
            Evaluation evaluation = new Evaluation();
            Test test = new TestsServices().GetByCode(code);
            evaluation.Test_Id = test.Id;
            SelectList agesList = new SelectList(new AgesServices().GetAgesForDropDownList(test.Company_Id), "Key", "Value");
            SelectList senioritiesList = new SelectList(new SenioritiesServices().GetSenioritiesForDropDownList(test.Company_Id), "Key", "Value");
            SelectList instructionLevelsList = new SelectList(new InstructionLevelsServices().GetInstructionLevelsForDropDownList(test.Company_Id), "Key", "Value");
            SelectList positionLevelsList = new SelectList(new PositionLevelsServices().GetPositionLevelsForDropDownList(test.Company_Id), "Key", "Value");
            SelectList locationsList = new SelectList(new LocationsServices().GetLocationsForDropDownList(test.Company_Id), "Key", "Value");

            IQueryable<DemographicsInTest> dem = new DemographicsInTestsServices().GetByTest(test.Id).Where(d => d.FOT_Id.HasValue);
            List<int> dems= new List<int>();
            foreach (DemographicsInTest d in dem)
            {
                dems.Add(d.FOT_Id.Value);
            }
            IQueryable<FunctionalOrganizationType> FOTypes = new FunctionalOrganizationTypesServices().GetByCompany(test.Company_Id).Where(d=> dems.Contains(d.Id));
            SelectList FOrganizations = new SelectList(new FunctionalOrganizationsServices().GetEmptyDictionary());
            SelectList performanceEvaluationsList = new SelectList(new PerformanceEvaluationsServices().GetPerformanceEvaluationsForDropDownList(test.Company_Id), "Key", "Value");
            _evaluationViewModel = new EvaluationViewModel(evaluation, agesList, senioritiesList, instructionLevelsList, positionLevelsList,
                                    locationsList, FOTypes, FOrganizations, performanceEvaluationsList, test, previousEvaluation);
        }

        private Evaluation MobileGenerateEvaluationObject(FormCollection collection)
        {
            Evaluation evaluation = new Evaluation();
            evaluation.Test_Id = int.Parse(collection["evaluation.Test_Id"]);
            evaluation.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
            evaluation.RemoteHostName = GetMACAddress();//collection["MacAddress"];//GetMACAddress(); //Request.ServerVariables["REMOTE_HOST"];
            evaluation.RemoteUserName = GetMacAddress2();//Request.ServerVariables["REMOTE_USER"];
            Test test = new TestsServices().GetById(evaluation.Test_Id);
            List<string> demographics = test.DemographicsInTests.Select(d => d.Demographic.Name).ToList();
            if (demographics.Contains("AgeRange"))
                evaluation.Age_Id = int.Parse(collection["evaluation.Age_Id"]);
            if (demographics.Contains("InstructionLevel"))
                evaluation.InstructionLevel_Id = int.Parse(collection["evaluation.InstructionLevel_Id"]);
            if (demographics.Contains("PositionLevel"))
                evaluation.PositionLevel_Id = int.Parse(collection["evaluation.PositionLevel_Id"]);
            if (demographics.Contains("Seniority"))
                evaluation.Seniority_Id = int.Parse(collection["evaluation.Seniority_Id"]);
            if (demographics.Contains("Location"))
                evaluation.Location_Id = int.Parse(collection["evaluation.Location_Id"]);
            if (demographics.Contains("Performance"))
                evaluation.Performance_Id = int.Parse(collection["evaluation.Performance_Id"]);
            if (demographics.Contains("Gender"))
                evaluation.Sex = GetGender(collection["evaluation.Sex"].ToString());
            Dictionary<int, string> FO = new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(test.Company_Id);
            for (int i = 0; i < 2; i++)
            {
                Selected_FO[i] = new int[FO.Count];
            }
            int j = 0;
            foreach (var v in FO)
            {
                Selected_FO[0][j] = v.Key; // Functional Organization Id
                Selected_FO[1][j] = int.Parse(collection["FO" + j + ""]); // Select Functional Organization
                j++;
            }
            evaluation.CreationDate = DateTime.Now;
            ValidateEvaluationModel(evaluation);
            return evaluation;
        }   

        /*
         *  
         */
        public JsonResult MobileGetFOTChildren(int foParent_id)
        {
            List<FunctionalOrganization> data = new List<FunctionalOrganization>();
            //FunctionalOrganization parent = new FunctionalOrganizationsServices().GetById(foParent_id);
            //var types = parent.FunctionalOrganizations.Select(g => g.FunctionalOrganizationType).OrderBy(g => g.Name).GroupBy(g => g.Id);
            //foreach (var type in types)
            //{
            //    var children = parent.FunctionalOrganizations.Where(i => i.Type_Id == type.First().Id);
            var children = new FunctionalOrganizationsServices().GetAllRecords().Where(i => i.FOParent_Id == foParent_id);
            foreach (var child in children)
            {
                data.Add(
                    new FunctionalOrganization
                    {
                        Id = child.Id,
                        Name = child.Name,
                        Type_Id = child.Type_Id
                    });
            }

            //}
            return Json(data);
        }

        /*
         * 
         */
        public JsonResult MobileRemoveFOTChildren(int fotId)
        {
            List<FunctionalOrganizationType> data = new List<FunctionalOrganizationType>();
            var FOTS = new FunctionalOrganizationTypesServices().GetAllRecords().Where(g => g.FOTParent_Id == fotId);
            foreach (var child in FOTS)
            {
                data.Add(
                    new FunctionalOrganizationType
                    {
                        Id = child.Id
                    });
            }
            return Json(data);
        }
    }
}
