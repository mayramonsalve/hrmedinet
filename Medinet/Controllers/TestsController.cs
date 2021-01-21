using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.Models;
using Medinet.Models.ViewModels;
using MedinetClassLibrary.AuthorizationModels;
using MedinetClassLibrary.Classes;
using System.Management;
using MedinetClassLibrary.CustomClasses;
using System.Threading;
using System.Globalization;

namespace Medinet.Controllers
{
    [HandleError]
    //[Authorize(Roles = "HRCompany, HRAdministrator")]
    public class TestsController : Controller
    {

        private TestsServices _testService;
        private TestViewModel _testViewModel;
        private DemographicSelectorDetailsServices _dseledeservice;
        public TestsController()
        {
            _testService = new TestsServices();
        }

        public TestsController(TestsServices _testService)
        {
            this._testService = _testService;
        }

        private bool GetAuthorization(Test test)
        {
            return new SharedHrAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                new CompaniesServices().GetById(test.Company_Id), false).isAuthorizated();
        }

        public JsonResult GetWeighingsByTest(int test_id)
        {
            List<object> weighings = new List<object>();
            WeighingsServices weighingService = new WeighingsServices();
            foreach (var weighing in weighingService.GetByTest(test_id))
            {
                weighings.Add(
                    new
                    {
                        categoryId = weighing.Category_Id,
                        weighingValue = weighing.Value,
                        weighingCategory = weighing.Category.Name,
                    });
            }
            return Json(weighings);
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Create(int? company_id)
        {
            if (company_id == 0)
                company_id = null;
            InitializeViews(null, company_id);
            return View(_testViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Create(FormCollection collection)
        {
            Test test = GenerateTestObject(collection, null);            

            if (GetAuthorization(test))
            {
                ValidateTestModel(test,collection);
                if(test.Weighted)
                    ValidateWeighingModels(collection, new QuestionnairesServices().GetById(test.Questionnaire_Id.Value));
                if (ModelState.IsValid)
                {
                    if (_testService.Add(test))
                    {
                        InsertDemographicsInTest(collection, test);
                        if(!test.OneQuestionnaire)
                            InsertDemographicSelectorDetails(collection, test);
                        if (test.Weighted)
                            InsertWeighings(collection, test);
                        if (new UsersServices().GetByUserName(User.Identity.Name.ToString()).Role.Name == "HRCompany")
                            return RedirectToAction("TestCode", new { @code = test.Code});
                        else
                            return RedirectToAction("TestCode", new { @code = test.Code });
                        
                    }
                }
                InitializeViews(null, null);
                return View(_testViewModel);
            }
            else
                return RedirectToLogOn();
        }

        private void InsertDemographicsInTest(FormCollection collection, Test test)
        {
            DemographicsInTestsServices dts = new DemographicsInTestsServices();
            if (test.PreviousTest_Id.HasValue)
            {
                foreach (DemographicsInTest dt in test.PreviousTest.DemographicsInTests)
                {
                    DemographicsInTest newDT = new DemographicsInTest();
                    newDT.Test_Id = test.Id;
                    newDT.Demographic_Id = dt.Demographic_Id;
                    newDT.FOT_Id = dt.FOT_Id;
                    dts.Add(newDT);
                }
            }
            else
            {
                int FOTId = new DemographicsServices().GetIdFromDemographicName("FunctionalOrganizationType");
                int fot = 0;
                int selectorD = 0;
                if (!test.OneQuestionnaire)//si son varios cuestionarios
                {
                    if (collection["DemographicSelector"].Contains("fot-"))
                    {
                        fot = Int32.Parse(collection["DemographicSelector"].Split('-')[1]);
                        selectorD = FOTId;
                    }
                    else
                        selectorD = Int32.Parse(collection["DemographicSelector"]);
                }
                
                string[] vectorD = collection["Demographics"].Split(',');

                    foreach (string demo in vectorD)
                    {
                        int demoId = demo.Contains("fot-") ? FOTId : Int32.Parse(demo);
                        DemographicsInTest dt = new DemographicsInTest();
                        dt.Demographic_Id = demoId;
                        dt.Test_Id = test.Id;
                        if (demo.Contains("fot-"))
                            dt.FOT_Id = Int32.Parse(demo.Split('-')[1]);
                        dt.Selector = demo.Contains("fot-") ? (fot == demoId ? true : false) : (selectorD == demoId ? true : false);
                        dts.Add(dt);
                    }
                
            }
        }

/******/private void EditDemographicsInTest(FormCollection collection, Test test)////edita los demograficos que salen en el dropdownlist de editar medicion (los q salen en el form de editar NO en el dialog)
        {
            DemographicsInTestsServices dts = new DemographicsInTestsServices();
            int[] vec = new int[100];
            int[] vec2 = new int[100];
            if (test.PreviousTest_Id.HasValue)
            {
                foreach (DemographicsInTest dt in test.PreviousTest.DemographicsInTests)
                {
                    DemographicsInTest newDT = new DemographicsInTest();
                    newDT.Test_Id = test.Id;
                    newDT.Demographic_Id = dt.Demographic_Id;
                    newDT.FOT_Id = dt.FOT_Id;
                    dts.Add(newDT);
                }
            }
            else
            {
                int FOTId = new DemographicsServices().GetIdFromDemographicName("FunctionalOrganizationType");
                int fot = 0, flag = 0, i = 0, finalnum = 0,numberdem = 0;
                int selectorD = 0;
                int[] num = new int[100];
                DemographicsInTestsServices dis = new DemographicsInTestsServices();

                if (!test.OneQuestionnaire)//si son varios cuestionarios
                {
                    var col = collection["DemographicSelector"];
                    if (col != null)
                    {

                        if (collection["DemographicSelector"].Contains("fot-"))
                        {
                            fot = Int32.Parse(collection["DemographicSelector"].Split('-')[1]);
                            selectorD = FOTId;
                        }
                        else
                            selectorD = Int32.Parse(collection["DemographicSelector"]);                  
                    }                   
                }

                string[] vectorD = collection["Demographics"].Split(',');//vector con los demograficos seleccionados en el form de editar

                vec=dts.GetByTestDemographic_Id(test.Id);//obtine los demograficos (del test enviado) que estan en la base de datos

                foreach (string demo in vectorD)
                {
                    /*En las siguientes lineas se consiguen los valores necesarios para guardar un demografico*/
                    int demoId = demo.Contains("fot-") ? FOTId : Int32.Parse(demo);
                    DemographicsInTest dt = new DemographicsInTest();
                    dt.Demographic_Id = demoId;
                    dt.Test_Id = test.Id;
                    if (demo.Contains("fot-"))
                        dt.FOT_Id = Int32.Parse(demo.Split('-')[1]);
                    dt.Selector = demo.Contains("fot-") ? (fot == demoId ? true : false) : (selectorD == demoId ? true : false);

                    if (dt.Selector != true)//si es diferente a demografico selector (selector!=1)se puede borrar para volver a crear el demografico
                    {
                        dis.deletebydemogandtest(dt);
                    }
                    dts.Add(dt);//crea el demografico

                }//foreach (string demo in vectorD)

                vec2 = dts.GetByTestDemographic_Id(test.Id);//carga de nuevo el demografico de la bd ya que anteriormente se pudieron guardar nuevos datos

                foreach (string demo in vectorD)
                {
                    /*En las siguientes lineas se consiguen los valores necesarios para guardar un demografico*/
                    int demoId = demo.Contains("fot-") ? FOTId : Int32.Parse(demo);
                    DemographicsInTest dt = new DemographicsInTest();
                    dt.Demographic_Id = demoId;
                    dt.Test_Id = test.Id;
                    if (demo.Contains("fot-"))
                        dt.FOT_Id = Int32.Parse(demo.Split('-')[1]);
                    dt.Selector = demo.Contains("fot-") ? (fot == demoId ? true : false) : (selectorD == demoId ? true : false);                    
                    
                    foreach (int demog in vec2)
                    {
                        if (!vectorD.Contains(demog.ToString()))//chequeo si el vector que trae los demograficos del form NO contiene alguno de los demograficos que se reciben de la bd
                        {
                            dt.Demographic_Id = demog;
                            dis.deletebydemogandtest(dt);
                        }
                    }//foreach (int demog in vec)
                    
                }//foreach (string demo in vectorD)   
                             
            }//else        
    
        }

        private void InsertDemographicSelectorDetails(FormCollection collection, Test test)
        {
            int FOTId = new DemographicsServices().GetIdFromDemographicName("FunctionalOrganizationType");
            int fot = 0;
            int selectorD = 0;
            DemographicSelectorDetailsServices dsds = new DemographicSelectorDetailsServices();
            if (collection["DemographicSelector"].Contains("fot-"))
            {
                fot = Int32.Parse(collection["DemographicSelector"].Split('-')[1]);
                selectorD = FOTId;
            }
            else
                selectorD = Int32.Parse(collection["DemographicSelector"]);
            string[] vectorQ = collection["QuestionnairesInTest"].Split(',');//vector con los cuestionarios seleccionados
            //string[] vectorQuestion = new string[100];

           int num_questionnaires=vectorQ.Count();//cantidad de cuestionarios seleccionados

            
                foreach (string quest in vectorQ)
                {
                    string[] selectorsId = collection["Selector_" + quest].Split(',');
                    foreach (string selector in selectorsId)
                    {
                        DemographicSelectorDetail dsd = new DemographicSelectorDetail();
                        dsd.Questionnaire_Id = Int32.Parse(quest);
                        dsd.Test_Id = test.Id;
                        dsd.Demographic_Id = selectorD;
                        dsd.SelectorValue_Id = Int32.Parse(selector); // Int32.Parse(collection["test.Selector_" + quest]);
                        dsds.Add(dsd);
                    }
                }
            

        }
        
/******/private void EditDemographicSelectorDetails(FormCollection collection, Test test)///para obtener los questionarios con sus demograficos y editarlos
        {
            int FOTId = new DemographicsServices().GetIdFromDemographicName("FunctionalOrganizationType");
            int fot = 0;
            int selectorD = 0, flag = 0;
            int[] num = new int[100];
            int[] demogbd = new int[100];
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            DemographicSelectorDetailsServices dsds = new DemographicSelectorDetailsServices();
            DemographicSelectorDetailsServices dsdS = new DemographicSelectorDetailsServices();
            Test tes;

            if (collection["DemographicSelector"].Contains("fot-"))
            {
                fot = Int32.Parse(collection["DemographicSelector"].Split('-')[1]);
                selectorD = FOTId;
            }
            else
                selectorD = Int32.Parse(collection["DemographicSelector"]);
            string[] vectorQ = collection["QuestionnairesInTest"].Split(',');//vector con los cuestionarios seleccionados
            //string[] vectorQuestion = new string[100];

            int num_questionnaires = vectorQ.Count();//cantidad de cuestionarios seleccionados

            num = dsdS.GetByTestR(test.Id);//obtiene el detalle del demografico selector seleccionado

            tes = _testService.GetById((int)test.Id);
            DemographicSelectorDetail dsd = new DemographicSelectorDetail();
            foreach (int valor in num)//recorre el demografico selector seleccionado
            {
                    foreach (string quest in vectorQ)//recorre cuestionarios que recibe del dialog
                    {
                        if (Int32.Parse(quest) == valor) //si el id del cuestionario seleccionado es igual al id del cuestionario en BD
                        {
                            demogbd = dsdS.GetByTestDemog_Id(test.Id, Int32.Parse(quest));//obtiene de bd el Id del demografico selector segun el cuestionario y el test
                            string[] selectorsId = collection["Selector_" + quest].Split(',');
                            foreach (string selector in selectorsId)//recorre los valores del demografico selector que han sido seleccionado
                            {
                                int band = 0;
                                foreach (int demg in demogbd)//recorre los demograficos obtenidos segun el cuestionario y test dados
                                {
                                    if (tes.Id == test.Id && demg == selectorD)//si el test de la bd = al id del test que trae el form && si el valor del demog en bd = al valor del demog obtenido del form
                                    {

                                        dsd.Questionnaire_Id = Int32.Parse(quest);
                                        dsd.Test_Id = test.Id;
                                        dsd.Demographic_Id = selectorD;
                                        dsd.SelectorValue_Id = Int32.Parse(selector);

                                        dsds.deletebydemogandquestionandtest(dsd);////borra de la tabla DemographicSelectorDetail los campos con los valores de demografico,cuestionario y test dados
                                        dsds.Add(dsd);

                                    }
                                    else
                                    {
                                        if (band == 0)
                                        {

                                            dsd.Questionnaire_Id = Int32.Parse(quest);
                                            dsd.Test_Id = test.Id;
                                            dsd.Demographic_Id = selectorD;
                                            dsd.SelectorValue_Id = Int32.Parse(selector);
                                            dsds.Add(dsd);

                                            band = band + 1;
                                        }

                                    }
                                }
                            }//foreach (string selector in selectorsId)
                            flag = 5000;
                        }//if (valor == Int32.Parse(quest)) 
                        else{                           
                            flag = flag + 1;
                        }///else
                    }//foreach (string quest in vectorQ)
            }//foreach (int valor in num)
            if(flag!=0 && flag<5000)
            {
                foreach (string quest in vectorQ)//recorre cuestionarios que recibe del dialog
                    {                       
                            string[] selectorsId = collection["Selector_" + quest].Split(',');
                            foreach (string selector in selectorsId)
                            {                                  
                                        dsd.Questionnaire_Id = Int32.Parse(quest);
                                        dsd.Test_Id = test.Id;
                                        dsd.Demographic_Id = selectorD;
                                        dsd.SelectorValue_Id = Int32.Parse(selector);
                                        dsds.Add(dsd);                        
                            }//foreach (string selector in selectorsId)                                                   
                    }//foreach (string quest in vectorQ)      
            }//if(flag!=0 && flag<5000)

        }

        private void InsertWeighings(FormCollection collection, Test test)
        {
            foreach (Category category in test.Questionnaire.Categories)
            {
                Weighing w = new Weighing();
                w.Test_Id = test.Id;
                w.Value = Int32.Parse(collection["weighing." + category.Id ]);
                w.Category_Id = category.Id;
                new WeighingsServices().Add(w);
            }
        }

        public string GenerateUniqueGuid()
        {
            List<string> guids = _testService.GetAllRecords().Select(g => g.Code).ToList();
            ShortGuid guid = ShortGuid.NewGuid();

            while (guids.Contains(guid))
            {
                guid = ShortGuid.NewGuid().ToString();
            }

            return guid;
        }

        private Test GenerateTestObject(FormCollection collection, int? test_id)
        {
            string[] aux;
            Test test;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            EvaluationsServices es = new EvaluationsServices();

            var evaluations = es.GetByTest(test_id);//obtiene las evaluaciones que tiene x medicion

            int cont = evaluations.Count();

            if (cont == 0)//si la medicion no contiene evaluaciones
            {
                if (test_id.HasValue)
                {
                    test = _testService.GetById(test_id.Value);
                    test.EvaluationsLefts = int.Parse(collection["test.EvaluationNumber"]) - test.CurrentEvaluations;
                }
                else
                {
                    test = new Test();
                    test.CreationDate = DateTime.Now;
                    test.CurrentEvaluations = 0;
                    test.EvaluationsLefts = int.Parse(collection["test.EvaluationNumber"]);
                    test.User_Id = user.Id;
                    test.Code = GenerateUniqueGuid();
                    test.Finished = false;
                }

                
                if (user.Role.Name == "HRCompany")
                    test.Company_Id = user.Company_Id;
                else
                    test.Company_Id = int.Parse(collection["test.Company_Id"]);
                if (collection["test.PreviousTest_Id"].ToString() != "")
                    test.PreviousTest_Id = int.Parse(collection["test.PreviousTest_Id"]);
                test.Name = collection["test.Name"];
                test.StartDate = DateTime.Parse(collection["StartDate"].ToString(), (CultureInfo)Session["Culture"]);
                test.EndDate = DateTime.Parse(collection["EndDate"].ToString(), (CultureInfo)Session["Culture"]);
                test.Text = collection["test.Text"];
                test.EvaluationNumber = int.Parse(collection["test.EvaluationNumber"]);
                test.ClimateScale_Id = int.Parse(collection["test.ClimateScale_Id"]);
                // test.DemographicsInTests = collection["test.DemographicsInTests"];
                aux = collection["test.OneQuestionnaire"].Split(',');
                test.OneQuestionnaire = Convert.ToBoolean(aux[0]);
                if (test.OneQuestionnaire)
                {
                    if (collection["test.Questionnaire_Id"].ToString() != "")
                    {
                        test.Questionnaire_Id = int.Parse(collection["test.Questionnaire_Id"]);
                    }
                    else
                    {

                    }
                }
                else
                {
                    var col = collection["DemographicSelector"];
                    if (col != null)
                    {
                        EditDemographicSelectorDetails(collection, test);/////////////////    
                    }
                }

                aux = collection["test.GroupByCategories"].Split(',');
                test.GroupByCategories = Convert.ToBoolean(aux[0]);
                test.RecordsPerPage = int.Parse(collection["test.RecordsPerPage"]);
                test.MinimumPeople = int.Parse(collection["test.MinimumPeople"]);
                aux = collection["test.Disordered"].Split(',');
                test.Disordered = Convert.ToBoolean(aux[0]);
                aux = collection["test.ResultBasedOn100"].Split(',');
                test.ResultBasedOn100 = Convert.ToBoolean(aux[0]);
                aux = collection["test.Weighted"].Split(',');
                test.Weighted = Convert.ToBoolean(aux[0]);
                aux = collection["test.ConfidenceLevel_Id"].Split(',');
                if (aux[0] == "") test.ConfidenceLevel_Id = 1;
                else test.ConfidenceLevel_Id = int.Parse(aux[0]);
                aux = collection["test.StandardError_Id"].Split(',');
                if (aux[0] == "") test.StandardError_Id = 6;
                else test.StandardError_Id = int.Parse(aux[0]);
                aux = collection["test.NumberOfEmployees"].Split(',');
                if (aux[0] == "") test.NumberOfEmployees = null;
                else test.NumberOfEmployees = int.Parse(aux[0]);
               
            }//if(evaluations==null)
            else {//es decir, si la medicion contiene evaluaciones

                if (test_id.HasValue)
                {
                    test = _testService.GetById(test_id.Value);
                    test.EvaluationsLefts = int.Parse(collection["test.EvaluationNumber"]) - test.CurrentEvaluations;
                }
                else
                {
                    test = new Test();
                    test.CreationDate = DateTime.Now;
                    test.CurrentEvaluations = 0;
                    test.EvaluationsLefts = int.Parse(collection["test.EvaluationNumber"]);
                    test.User_Id = user.Id;
                    test.Code = ShortGuid.NewGuid().ToString();
                }
                
                test.Name = collection["test.Name"];
                test.EndDate = Convert.ToDateTime(collection["EndDate"]);
                test.Text = collection["test.Text"];
                test.EvaluationNumber = int.Parse(collection["test.EvaluationNumber"]);
                aux = collection["test.GroupByCategories"].Split(',');
                test.GroupByCategories = Convert.ToBoolean(aux[0]);
                test.RecordsPerPage = int.Parse(collection["test.RecordsPerPage"]);
                test.MinimumPeople = int.Parse(collection["test.MinimumPeople"]);
                aux = collection["test.Disordered"].Split(',');
                test.Disordered = Convert.ToBoolean(aux[0]);
                aux = collection["test.ResultBasedOn100"].Split(',');
                test.ResultBasedOn100 = Convert.ToBoolean(aux[0]);
                aux = collection["test.Weighted"].Split(',');
                test.Weighted = Convert.ToBoolean(aux[0]);
                aux = collection["test.ConfidenceLevel_Id"].Split(',');
                if (aux[0] == "") test.ConfidenceLevel_Id = 1;
                else test.ConfidenceLevel_Id = int.Parse(aux[0]);
                aux = collection["test.StandardError_Id"].Split(',');
                if (aux[0] == "") test.StandardError_Id = 6;
                else test.StandardError_Id = int.Parse(aux[0]);
                aux = collection["test.NumberOfEmployees"].Split(',');
                if (aux[0] == "") test.NumberOfEmployees = null;
                else test.NumberOfEmployees = int.Parse(aux[0]);            
            
            }
            return test;  
        }

        public decimal GetConfidenceLevelValueById(int id)
        {
            return new ConfidenceLevelsServices().GetById(id).Value;
        }

        public decimal GetStandardErrorValueById(int id)
        {
            return new StandardErrorsServices().GetById(id).Value;
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Delete(int id)
        {
            InitializeViews(id, null);
            return View(_testViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            DemographicSelectorDetailsServices demodelete = new DemographicSelectorDetailsServices();
            if (GetAuthorization(_testService.GetById(id)))
            {
                try
                {
                    DeleteAnswersOnCascade(id);
                    if (_testService.GetById(id).Weighted)
                        DeleteWeighingsOnCascade(id);
                    demodelete.deletebytest(id);
                    _testService.Delete(id);
                    _testService.SaveChanges();
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

        private void DeleteWeighingsOnCascade(int test_id)
        {
            WeighingsServices weighingService = new WeighingsServices();
            foreach (Weighing w in weighingService.GetByTest(test_id))
            {
                weighingService.Delete(w.Id);
                weighingService.SaveChanges();
            }
        }

        public void DeleteAnswersOnCascade(int test_id)
        {
            TextAnswersServices textAnswerService = new TextAnswersServices();
            DichotomousAnswersServices dichotomousAnswerService = new DichotomousAnswersServices();
            SelectionAnswersServices selectionAnswerService = new SelectionAnswersServices();
            var evaluations = new EvaluationsServices().GetByTest(test_id);
            foreach(var e in evaluations)
            {
                foreach (var textAnswer in e.TextAnswers)
                {
                    textAnswerService.Delete(textAnswer.Id);
                    textAnswerService.SaveChanges();
                }
                foreach (var selectiontAnswer in e.SelectionAnswers)
                {
                    selectionAnswerService.Delete(selectiontAnswer.Id);
                    selectionAnswerService.SaveChanges();
                }
                foreach (var dichotomousAnswer in e.DichotomousAnswers)
                {
                    dichotomousAnswerService.Delete(dichotomousAnswer.Id);
                    dichotomousAnswerService.SaveChanges();
                }
            }
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Details(int id)
        {
            if (GetAuthorization(_testService.GetById(id)))
            {
                InitializeViews(id, null);
                return View(_testViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Finish(int id)
        {
            if (GetAuthorization(_testService.GetById(id)))
            {
                InitializeViews(id, null);
                return View(_testViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Finish(int id, FormCollection collection)
        {
            Test test = _testService.GetById(id);
            if (GetAuthorization(test))
            {
                try
                {
                    //string error = BuildExecutiveReportOptions(test);
                    test.Finished = true;
                    _testService.SaveChanges();
                    if (test.User.Role.Name == "HRCompany")
                        return RedirectToAction("Index");
                    else
                        return RedirectToAction("Index", new { @company_id = test.Company_Id });
                }
                catch
                {
                    InitializeViews(id, null);
                    return View(_testViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        public ActionResult TestsJob()
        {
            List<Test> testAll = _testService.GetTestsToFinish();
            List<Test> testFinish = new List<Test>();
            List<Test> testError = new List<Test>();
            foreach (Test test in testAll)
            {
                string error = BuildExecutiveReportOptions(test);
                if (String.IsNullOrEmpty(error))
                    testFinish.Add(test);
                else
                    testError.Add(test);
            }
            return View();
        }

        private string BuildExecutiveReportOptions(Test test)
        {
            string error = "";
            try
            {
                User user = new UsersServices().GetByUserName("hradministrator");
                //Actualizamos el verdadero número de mediciones realizadas al momento
                test.CurrentEvaluations = test.Evaluations.Count;
                //List<User> users = new List<MedinetClassLibrary.Models.User>();
                //User us;
                //if(test.Company.CompanyAssociated_Id.HasValue)
                //{
                //    us = test.Company.CompanyAssociated.Users.Where(u => u.Role.Name.ToLower() == "hradministrator").FirstOrDefault();
                //    if(us != null)
                //        users.Add(us);
                //    else
                //    {
                //        us = new UsersServices().GetByUserName("hradministrator");
                //        users.Add(us);
                //    }
                //}
                //us = test.Company.Users.Where(u => u.Role.Name.ToLower() == "hrcompany").FirstOrDefault();
                //if(us != null)
                //    users.Add(us);
                List<int[]> states = (from s in test.Company.Locations.Select(l => l.State).Distinct()
                                      select new int[] { s.Country_Id, s.Id }).ToList();
                List<int[]> states_aux = (from c in test.Company.Locations.Select(l => l.State.Country).Distinct()
                                          select new int[] { c.Id, 0 }).ToList();
                states.AddRange(states_aux);
                //List<int[]> states = new List<int[]>();
                states.Add(new int[] { 0, 0 });
                List<int?> regions = test.Company.Locations.Select(l => l.Region_Id).Distinct().ToList();
                if (!regions.Contains(null)) regions.Add(null);

                //foreach (User user in users)
                //{
                foreach (int[] st in states)
                {
                    int? country = st[0];
                    int? state = st[1];
                    AnalyticalReport analyticalClass = new AnalyticalReport(test, user, country == 0 ? null : country, state == 0 ? null : state, null);
                    SaveExecutiveReportDB(analyticalClass);
                }
                foreach (int? rg in regions)
                {
                    AnalyticalReport analyticalClass = new AnalyticalReport(test, user, null, null, rg == 0 ? null : rg);
                    SaveExecutiveReportDB(analyticalClass);
                }
                //}
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return error;
        }

        private static void SaveExecutiveReportDB(AnalyticalReport analyticalClass)
        {
            if (analyticalClass.evaluationsByUbicationCount > analyticalClass.Test.MinimumPeople)
            {
                double SatisfiedCountPercentage = analyticalClass.GetSatisfiedCountPercentage();//porcentaje de resultados favorables
                double GeneralClimate = analyticalClass.GetGeneralClimate();//obtener clima general
                string ColourByClimate = analyticalClass.GetColourByClimate(GeneralClimate);//obtener el color del clima
                List<string> DemographicsWhereThereIsAssociation = analyticalClass.GetDemographicsWhereThereIsAssociation();//demograficos donde hay asociacion
                Dictionary<string, string> Ubication = analyticalClass.GetUbication();//ubicacion es el nombre de la medicion mas la medicion
                Dictionary<string, double> ClimateByCategories = analyticalClass.GetClimateByCategories();//clima por categorias,que devuleve los dos primeros y los dos ultimos
                Dictionary<string, double> PositiveAnswersByPositionLevels = analyticalClass.GetPositiveAnswersByPositionLevels();//respuestas positivas por nivel de cargo
                Dictionary<string, double> PositiveAnswersByFOTypes = analyticalClass.GetPositiveAnswersByFOTypes();//obtener respuestas positivas por la estructura funcional principal o padreS
                Dictionary<string, double> StepwiseValues = analyticalClass.GetStepwiseValues();//esto es para decir por ejemplo por categorias del cuestionario q por lo general se llaman dimensiones el % de lo que afectaba esa categoria en el contento o descontento de los empleados.el trabajo:trabajo en equipo aporto el 30%,comunicacion 30%.porcentaje de lo que aporto para que esto no llegara a 100
                Dictionary<string, double> ClimateByBranches = analyticalClass.GetClimateByBranches();//clima por sucursales
                Dictionary<string, object> SatNotSat = analyticalClass.GetSatisfiedAndNonSatisfied(null);//son las tablas de satisfechos y no satisfechos por general,categorias y sucursal

                //ExecutiveReport
                ExecutiveReport er = new ExecutiveReport();
                er.Date = DateTime.Now;
                er.Test_Id = analyticalClass.Test.Id;
                er.Role_Id = analyticalClass.UserLogged.Role_Id;
                er.Country_Id = analyticalClass.country;
                er.State_Id = analyticalClass.state;
                er.Region_Id = analyticalClass.region;
                er.SatisfiedCountPercentage = (decimal)SatisfiedCountPercentage;
                er.GeneralClimate = (decimal)GeneralClimate;
                er.ColourByClimate = ColourByClimate;
                er.DemographicsWhereThereIsAssociation = DemographicsWhereThereIsAssociation == null ? "" : string.Join(",", DemographicsWhereThereIsAssociation.ToArray());
                er.Ubication = string.Join(";", Ubication.Select(x => x.Key + ":" + x.Value).ToArray());
                
                if (new ExecutiveReportsServices().Add(er))
                {
                    //ER_AnswersByFOType-PositiveAnswersByFOTypes
                    if (PositiveAnswersByFOTypes != null)
                    {
                        ER_AnswersByFOTypesServices fots = new ER_AnswersByFOTypesServices();
                        foreach (KeyValuePair<string, double> pair in PositiveAnswersByFOTypes)
                        {
                            ER_AnswersByFOType fot = new ER_AnswersByFOType();
                            fot.Text = pair.Key;
                            fot.Value = (decimal)pair.Value;
                            fot.ExecutiveReport_Id = er.Id;
                            fots.Add(fot);
                        }
                    }
                    //ER_AnswersByPositionLevel-PositiveAnswersByPositionLevels
                    if (PositiveAnswersByPositionLevels != null)
                    {
                        ER_AnswersByPositionLevelsServices pls = new ER_AnswersByPositionLevelsServices();
                        foreach (KeyValuePair<string, double> pair in PositiveAnswersByPositionLevels)
                        {
                            ER_AnswersByPositionLevel pl = new ER_AnswersByPositionLevel();
                            pl.Text = pair.Key;
                            pl.Value = (decimal)pair.Value;
                            pl.ExecutiveReport_Id = er.Id;
                            pls.Add(pl);
                        }
                    }
                    //ER_CategoriesSatEmployee-SatNotSat
                    if (SatNotSat != null && SatNotSat["Category"] != null)
                    {
                        ER_CategoriesSatEmployeesServices cses = new ER_CategoriesSatEmployeesServices();
                        foreach (KeyValuePair<string, double[]> pair in (IDictionary<string, double[]>)SatNotSat["Category"])
                        {

                            ER_CategoriesSatEmployee cse = new ER_CategoriesSatEmployee();
                            cse.Text = pair.Key;
                            cse.Satisfied = (int)pair.Value[0];
                            cse.PctgSatisfied = (decimal)pair.Value[1];
                            cse.NotSatisfied = (int)pair.Value[2];
                            cse.PctgNotSatisfied = (decimal)pair.Value[3];
                            if (pair.Value.Count() > 4)
                            {
                                cse.Average = (decimal)pair.Value[4];
                                cse.Median = (decimal)pair.Value[5];
                            }
                            cse.ExecutiveReport_Id = er.Id;
                            cses.Add(cse);
                        }
                    }
                    //ER_ClimateByBranch-ClimateByBranches
                    if (ClimateByBranches != null)
                    {
                        ER_ClimateByBranchesServices cbs = new ER_ClimateByBranchesServices();
                        foreach (KeyValuePair<string, double> pair in ClimateByBranches)
                        {
                            ER_ClimateByBranch cb = new ER_ClimateByBranch();
                            cb.Text = pair.Key;
                            cb.Value = (decimal)pair.Value;
                            cb.ExecutiveReport_Id = er.Id;
                            cbs.Add(cb);
                        }
                    }
                    //ER_ClimateByCategory-ClimateByCategories
                    if (ClimateByCategories != null)
                    {
                        ER_ClimateByCategoriesServices ccs = new ER_ClimateByCategoriesServices();
                        foreach (KeyValuePair<string, double> pair in ClimateByCategories)
                        {
                            ER_ClimateByCategory cc = new ER_ClimateByCategory();
                            cc.Text = pair.Key;
                            cc.Value = (decimal)pair.Value;
                            cc.ExecutiveReport_Id = er.Id;
                            ccs.Add(cc);
                        }
                    }
                    //ER_GeneralSatEmployee-SatNotSat
                    if (SatNotSat != null && SatNotSat["General"] != null)
                    {
                        ER_GeneralSatEmployeesServices gses = new ER_GeneralSatEmployeesServices();
                        foreach (KeyValuePair<string, double[]> pair in (IDictionary<string, double[]>)SatNotSat["General"])
                        {
                            ER_GeneralSatEmployee gse = new ER_GeneralSatEmployee();
                            gse.Text = pair.Key;
                            gse.Satisfied = (int)pair.Value[0];
                            gse.PctgSatisfied = (decimal)pair.Value[1];
                            gse.NotSatisfied = (int)pair.Value[2];
                            gse.PctgNotSatisfied = (decimal)pair.Value[3];
                            gse.ExecutiveReport_Id = er.Id;
                            gses.Add(gse);
                        }
                    }
                    //ER_LocationsSatEmployee-SatNotSat
                    if (SatNotSat != null && SatNotSat["Location"] != null)
                    {
                        ER_LocationsSatEmployeesServices lses = new ER_LocationsSatEmployeesServices();
                        foreach (KeyValuePair<string, double[]> pair in (IDictionary<string, double[]>)SatNotSat["Location"])
                        {
                            ER_LocationsSatEmployee lse = new ER_LocationsSatEmployee();
                            lse.Text = pair.Key;
                            lse.Satisfied = (int)pair.Value[0];
                            lse.PctgSatisfied = (decimal)pair.Value[1];
                            lse.NotSatisfied = (int)pair.Value[2];
                            lse.PctgNotSatisfied = (decimal)pair.Value[3];
                            if (pair.Value.Count() > 4)
                            {
                                lse.Average = (decimal)pair.Value[4];
                                lse.Median = (decimal)pair.Value[5];
                            }
                            lse.ExecutiveReport_Id = er.Id;
                            lses.Add(lse);
                        }
                    }
                    //ER_StepwiseValue-StepwiseValues
                    if (StepwiseValues != null)
                    {
                        ER_StepwiseValuesServices svs = new ER_StepwiseValuesServices();
                        foreach (KeyValuePair<string, double> pair in StepwiseValues)
                        {
                            ER_StepwiseValue sv = new ER_StepwiseValue();
                            sv.Text = pair.Key;
                            sv.Value = (decimal)pair.Value;
                            sv.ExecutiveReport_Id = er.Id;
                            svs.Add(sv);
                        }
                    }
                }
                
                //Dictionary<string, string> ChartSources = analyticalClass.GetChartSources();//obtener las acciones de donde voy a buscar los charts q tengo en los reportes
                //int graphicIdPopulationGender = new GraphicsServices().GetByDemographicAndType(analyticalClass.genderPopulation ? "Gender" : "General", "Population").Id;//busco el id del grafico de muestreo ya sea de genero o general
                //int[] PositionAndCompaniesCount = analyticalClass.GetPositionAndCompaniesCount();//ranking
            }
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Edit(int id)
        {
            if (GetAuthorization(_testService.GetById(id)))
            {
                InitializeViews(id, null);
                return View(_testViewModel);
            }
            else
                return RedirectToLogOn();
        }

        [HttpPost]
        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (GetAuthorization(_testService.GetById(id)))
            {
                try
                {
                    bool lastW = _testService.GetById(id).Weighted;
                    Test test = GenerateTestObject(collection, id);
                    EvaluationsServices es = new EvaluationsServices();

                    var evaluations = es.GetByTest(id);//obtiene las evaluaciones que tiene x medicion

                    int cont = evaluations.Count();

                    if(test.Weighted)
                        ValidateWeighingModels(collection, test.Questionnaire);
                    if (ModelState.IsValid)
                    {

                        //test.StartDate = DateTime.Parse(test.StartDate.ToString(), new CultureInfo("en-US"));
                        //test.EndDate = DateTime.Parse(test.EndDate.ToString(), new CultureInfo("en-US"));
                        if (lastW != test.Weighted || (lastW && test.Weighted))
                            UpdateOrCreateWeighings(lastW, test, collection);
                       if(cont==0){ EditDemographicsInTest(collection, test);}
                        _testService.SaveChanges();
                        if (test.User.Role.Name == "HRCompany")
                            return RedirectToAction("Index");
                        else
                            return RedirectToAction("Index", new { @company_id = test.Company_Id });
                    }
                    InitializeViews(id, null);
                    return View(_testViewModel);
                }
                catch
                {
                    InitializeViews(id, null);
                    return View(_testViewModel);
                }
            }
            else
                return RedirectToLogOn();
        }

        private void UpdateOrCreateWeighings(bool lastW, Test newTest, FormCollection collection)
        {
            if (lastW != newTest.Weighted)
            {
                if (!lastW) //Crear Weighings
                    InsertWeighings(collection, newTest);
                else //Eliminar Weighings
                    DeleteWeighingsOnCascade(newTest.Id);
            }
            else
                UpdateWeighings(collection, newTest);

        }

        private void UpdateWeighings(FormCollection collection, Test newTest)
        {
            //foreach(Category categoty in newTest.Questionnaire.Categories)
            //{
            //    newTest.Weighings.Where(w => w.Category_Id==categoty.Id).FirstOrDefault().Value = decimal.Parse(collection["weighing." + categoty.Id]);
            //}
        }

        private ActionResult RedirectToLogOn()
        {
            ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult GridData(int? company_id,string sidx, string sord, int page, int rows, string filters)
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            if (user.Role.Name == "HRAdministrator")
            {
                if (company_id != null)
                {
                    object resultado = _testService.RequestList(company_id, sidx, sord, page, rows, filters);
                    return Json(resultado);
                }
                else
                {
                    return null;
                }

            }
            else {
                object resultado = _testService.RequestList(user.Company_Id, sidx, sord, page, rows, filters);
                return Json(resultado);
            }
        }

       

        [Authorize(Roles = "HRCompany, HRAdministrator")]
        public ActionResult Index(int? company_id)
        {
            InitializeViews(null, company_id);
            return View(_testViewModel);
        }

        public ActionResult TestCode(string code) {

            return View(new TestViewModel(code));
        }

        //[HttpPost]
        //public ActionResult TestCode(string code) {
        //    User user = new UsersServices().GetByUserName(User.Identity.Name);
        //    EmailBroadcaster.SendEmail("Medinet - Evaluations Code", "Here is your Test Code :" + code, user.Email);
        //    return RedirectToAction("Index");
        //}

        public ActionResult CheckCode()
        {
            return View();
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

        [HttpPost]
        public ActionResult CheckCode(FormCollection collection)
        {
            var code = collection["Code"];
            Test test = new TestsServices().GetByCode(code);
            ValidateEvaluationModel(test);
            if(ModelState.IsValid)
            {
                if (test != null)
                    return RedirectToAction("TestInstructions", "Evaluations", new { @code = code });
                else
                {
                    ModelState.AddModelError(ViewRes.Controllers.Tests.Code, ViewRes.Controllers.Tests.CodeText);
                    return View(collection);
                }
            }
            return View(collection);
        }

        public ActionResult MobileCheckCode()
        {
            return View("/Views/Home/Index.Mobile.aspx");
        }



        [HttpPost]
        public ActionResult MobileCheckCode(FormCollection collection)
        {
            var code = collection["Code"];
            Test test = new TestsServices().GetByCode(code);
            MobileValidateEvaluationModel(test);
            if (ModelState.IsValid)
            {
                if (test != null)
                    return RedirectToAction("TestInstructions", "Evaluations", new { @code = code });
                else
                {
                    ViewData["Code"] = ViewRes.Controllers.Tests.CodeText;
                    ModelState.AddModelError(ViewRes.Controllers.Tests.Code, ViewRes.Controllers.Tests.CodeText);
                    //return View(collection);
                }
            }
            //return View(collection);
            return View("/Views/Home/Index.Mobile.aspx");
        }

        public JsonResult GetSelectorsByDemographicAndCompany(string demographic_id, int company_id)
        {
            List<object> demographics = new List<object>();
            string demo = demographic_id.Contains("fot-") ? "FunctionalOrganizationType" :
                            new DemographicsServices().GetById(Int32.Parse(demographic_id)).Name;
            int fot = demographic_id.Contains("fot-") ? Int32.Parse((demographic_id.Split('-')[1])) : 0;
            Dictionary<int, string> items;
            switch (demo)
            {
                case "AgeRange":
                    items = new AgesServices().GetAgesForDropDownList(company_id);
                    break;
                case "Country":
                    items = new LocationsServices().GetCountriesByCompanyForDropDownList(company_id);
                    break;
                case "Gender":
                    items = new Dictionary<int, string>();
                    items.Add(0, ViewRes.Classes.ChiSquare.FemaleGender);
                    items.Add(1, ViewRes.Classes.ChiSquare.MaleGender);
                    break;
                case "InstructionLevel":
                    items = new InstructionLevelsServices().GetInstructionLevelsForDropDownList(company_id);
                    break;
                case "Location":
                    items = new LocationsServices().GetLocationsForDropDownList(company_id);
                    break;
                case "Performance":
                    items = new PerformanceEvaluationsServices().GetPerformanceEvaluationsForDropDownList(company_id);
                    break;
                case "PositionLevel":
                    items = new PositionLevelsServices().GetPositionLevelsForDropDownList(company_id);
                    break;
                case "Region":
                    items = new LocationsServices().GetRegionsByCompanyForDropDownList(company_id);
                    break;
                case "Seniority":
                    items = new SenioritiesServices().GetSenioritiesForDropDownList(company_id);
                    break;
                case "State":
                    items = new LocationsServices().GetStatesByCompanyForDropDownList(company_id);
                    break;  
                default: // FOT
                    items = new FunctionalOrganizationsServices().GetFunctionalOrganizationsByTypeForDropDownList(fot);
                    break;
            }
            foreach (var demog in items)
            {
                demographics.Add(
                    new
                    {
                        optionValue = demog.Key,
                        optionDisplay = demog.Value,
                    });
            }
            return Json(demographics);
        }

        public JsonResult GetDemographicsByCompany(int company_id)
        {
            List<object> demographics = new List<object>();
            foreach (var demog in new DemographicsServices().GetDemographicsByCompanyForDropDownList(company_id))
            {
                demographics.Add(
                    new
                    {
                        optionValue = demog.Key,
                        optionDisplay = demog.Value,
                    });
            }
            return Json(demographics);
        }

        public JsonResult GetNotStartedTestsByCompany(int company_id, int? test_id)
        {
            List<object> tests = new List<object>();
            foreach (var test in _testService.GetNotStartedByCompany(company_id, test_id))
            {
                tests.Add(
                    new
                    {
                        optionValue = test.Key,
                        optionDisplay = test.Value,
                    });
            }
            return Json(tests);
        }

        private void InitializeViews(int? test_id, int? company_id)
        {
            Test test;
            SelectList questionnairesList;
            SelectList scalesList;
            SelectList companiesList = null;
            SelectList testsList = new SelectList(new Dictionary<int, string>(), "Key", "Value");
            SelectList CL, SE;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            MultiSelectList demographics;

            if (test_id != null)//editar
            {
                test = _testService.GetById((int)test_id);
                List<int> demog = test.DemographicsInTests.Select(d => d.Demographic_Id).ToList();
                if (demog.Contains(11))
                {
                    demog.Remove(11);
                    demog.Add(test.DemographicsInTests.Where(d => d.Demographic_Id == 11).Select(f => f.FOT_Id.Value).FirstOrDefault());
                }
                demographics = new MultiSelectList(new DemographicsServices().GetDemographicsByCompanyForDropDownList(test.Company_Id), "Key", "Value", demog);
                scalesList = new SelectList(new ClimateScalesServices().GetClimateScalesForDropDownListByCompanyAndAssociated(user.Company_Id, user), "Key", "Value", test.ClimateScale_Id);
                testsList = new SelectList(_testService.GetNotStartedByCompany(user.Company_Id, test.Id), "Key", "Value", test.PreviousTest_Id);

                companiesList = new SelectList(new CompaniesServices().GetCustomersByAssociatedForDropDownList(user.Company_Id), "Key", "Value", test.Company_Id);

                if (user.Role.Name == "HRCompany")
                    questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesForCustomerForDropDownList(user.Company), "Key", "Value", test.Company_Id);
                else////////////////////////////////////////////////////////////////
                {
                    companiesList = new SelectList(new CompaniesServices().GetCustomersByAssociatedForDropDownList(user.Company_Id), "Key", "Value", test.Company_Id);
                    questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesForCustomerForDropDownList2(test.Company_Id, user), "Key", "Value", test.Company_Id);//////GetQuestionnairesForCustomerForDropDownList(test.Company_Id),
                }
                if (test.ConfidenceLevel != null && test.StandardError != null)
                {
                    CL = new SelectList(new ConfidenceLevelsServices().GetConfidenceLevelsForDropDownList(), "Key", "Value", test.ConfidenceLevel_Id);
                    SE = new SelectList(new StandardErrorsServices().GetStandardErrorsForDropDownList(), "Key", "Value", test.StandardError_Id);
                }
                else
                {
                    CL = new SelectList(new ConfidenceLevelsServices().GetConfidenceLevelsForDropDownList(), "Key", "Value", 1);
                    SE = new SelectList(new StandardErrorsServices().GetStandardErrorsForDropDownList(), "Key", "Value", 6);
                }
            }
            else {//crear
                demographics = (user.Role.Name == "HRCompany") ?
                                            new MultiSelectList(new DemographicsServices().GetDemographicsByCompanyForDropDownList(user.Company_Id), "Key", "Value")
                                            : ((company_id.HasValue) ?
                                            new MultiSelectList(new DemographicsServices().GetDemographicsByCompanyForDropDownList(company_id.Value), "Key", "Value")
                                            : (new MultiSelectList(_testService.GetEmptyDictionary(), "Key", "Value")));
                test = new Test();
                test.StartDate = DateTime.Now;
                test.EndDate = DateTime.Now.AddDays(7);
                test.RecordsPerPage = 10;
                test.MinimumPeople = 5;
                test.GroupByCategories = false;
                test.Disordered = true;
                test.Company_Id = (user.Role.Name == "HRCompany") ? user.Company_Id : ((company_id.HasValue) ? company_id.Value : 0);
                test.OneQuestionnaire = true;
                scalesList = new SelectList(new ClimateScalesServices().GetClimateScalesForDropDownListByCompanyAndAssociated(user.Company_Id,user), "Key", "Value");
                CL = new SelectList(new ConfidenceLevelsServices().GetConfidenceLevelsForDropDownList(), "Key", "Value", 1);
                SE = new SelectList(new StandardErrorsServices().GetStandardErrorsForDropDownList(), "Key", "Value", 6);
                if (company_id != null)
                {
                    if (user.Role.Name == "HRAdministrator")
                        companiesList = new SelectList(new CompaniesServices().GetCustomersByAssociatedForDropDownList(user.Company_Id), "Key", "Value", company_id);
                    else
                        testsList = new SelectList(_testService.GetNotStartedByCompany(user.Company_Id, null), "Key", "Value");
                    test.Company_Id = company_id.Value;
                }
                else
                {
                    if (user.Role.Name == "HRAdministrator")
                        companiesList = new SelectList(new CompaniesServices().GetCustomersByAssociatedForDropDownList(user.Company_Id), "Key", "Value");
                }
                if (user.Role.Name == "HRCompany")
                    questionnairesList = new SelectList(new QuestionnairesServices().GetQuestionnairesForCustomerForDropDownList(user.Company), "Key", "Value");
                else
                    questionnairesList = new SelectList(new QuestionnairesServices().GetTemplatesByAssociatedForDropDownList(user.Company_Id), "Key", "Value");
            }
            _testViewModel = new TestViewModel(test, questionnairesList, companiesList, user.Role.Name, CL, SE, scalesList, demographics, testsList);
        }

        public JsonResult GetCompaniesForHRAdministrator()
        {
            List<object> companies = new List<object>();
            //companiesList = new CompaniesServices().GetCustomersByAssociatedForDropDownList(new UsersServices().GetByUserName(User.Identity.Name).Company_Id);
            foreach (var company in new CompaniesServices().GetCustomersByAssociatedForDropDownList(new UsersServices().GetByUserName(User.Identity.Name).Company_Id))
            {
                companies.Add(
                    new
                    {
                        optionValue = company.Key,
                        optionDisplay = company.Value,
                    });
            }
            return Json(companies);
        }

        public JsonResult GetTestsForGlobalClimate()
        {
            User user = new UsersServices().GetByUserName(User.Identity.Name);
            List<object> tests = new List<object>();
            foreach (var test in new TestsServices().GetTestsByUserForDropDownList(user, true))
            {
                tests.Add(
                    new
                    {
                        optionValue = test.Key,
                        optionDisplay = test.Value
                    });
            }
            return Json(tests);
        }
        private void ValidateTestModel(Test test, FormCollection collection)
        {
            string[] aux;
            aux = collection["test.OneQuestionnaire"].Split(',');
            test.OneQuestionnaire = Convert.ToBoolean(aux[0]);    
            var col = collection["DemographicSelector"];

            if (_testService.IsNameDuplicated(test.Company_Id, test.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
            if (test.StartDate > test.EndDate)
                ModelState.AddModelError(ViewRes.Controllers.Tests.EndDate, ViewRes.Controllers.Tests.EndDateTestText);
            if (test.StartDate < Convert.ToDateTime(DateTime.Now.ToString(ViewRes.Views.Shared.Shared.Date)))
                ModelState.AddModelError(ViewRes.Controllers.Tests.StartDate, ViewRes.Controllers.Tests.StartDateTestText);
            if (_testService.IsMinimumInvalid(test.MinimumPeople, test.EvaluationNumber))
                ModelState.AddModelError(ViewRes.Controllers.Tests.MinimumPeople, ViewRes.Controllers.Tests.MinimumPeopleText);
            if (collection["Demographics"] == null)//si demografico no trae valor del form
                ModelState.AddModelError(ViewRes.Controllers.Tests.Demograph, ViewRes.Controllers.Tests.DemographicsText);
            if (test.OneQuestionnaire == true && collection["test.Questionnaire_Id"].ToString() == "")//si cuestionario no trae valor del form
                ModelState.AddModelError(ViewRes.Controllers.Tests.Questionnaire, ViewRes.Controllers.Tests.QuestionnaireText);
            if (col != null)//si el demografico selector trae valor quiere decir que se selecciono el boton varios cuestionarios
            {
                 string[] vectorQ = collection["QuestionnairesInTest"].Split(',');//vector con los cuestionarios seleccionados
                 int num_questionnaires = vectorQ.Count();

                 if (num_questionnaires < 2)//si los cuestionarios seleccionados fueron menos de 2
                     ModelState.AddModelError(ViewRes.Controllers.Tests.Questionnaires, ViewRes.Controllers.Tests.QuestionnairesText);
            }

        }

        private void ValidateWeighingModels(FormCollection collection, Questionnaire questionnaire)
        {

            if (new WeighingsServices().IsWeighingNull(collection, questionnaire))
                ModelState.AddModelError(ViewRes.Controllers.Tests.WeighingNull, ViewRes.Controllers.Tests.WeighingNullText);
            else
            {
                if (!(new WeighingsServices().AreNumerics(collection, questionnaire)))
                    ModelState.AddModelError(ViewRes.Controllers.Tests.Weighing, ViewRes.Controllers.Tests.WeighingInvalid);
                else if (new WeighingsServices().GetWeighingCount(collection, questionnaire) > 100 || new WeighingsServices().GetWeighingCount(collection, questionnaire) < 100)
                    ModelState.AddModelError(ViewRes.Controllers.Tests.Weighing, ViewRes.Controllers.Tests.WeighingText);
            }
        }

        private bool IsInHRRole()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("HRCompany") || User.IsInRole("HRAdministrator"))
                    return true;
            }
            return false;
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
                if (test.Finished)
                    ModelState.AddModelError("Finished", "Esta medición ya ha sido finalizada");
            }
            else {
                ModelState.AddModelError(ViewRes.Controllers.Tests.CodeNotFound, ViewRes.Controllers.Tests.CodeNotFoundText);
            }
            
        }

        private void MobileValidateEvaluationModel(Test test)
        {

            if (test != null)
            {
                if (test.EvaluationNumber <= test.CurrentEvaluations /*test.Evaluations.Count*/)
                {
                    ViewData["EvaluationNumber"] = ViewRes.Controllers.Tests.EvaluationNumberText;
                    ModelState.AddModelError(ViewRes.Controllers.Tests.EvaluationNumber, ViewRes.Controllers.Tests.EvaluationNumberText);
                }
                //if (new TestsServices().IsIPDuplicated(test, GetMACAddress()) && !IsInHRRole())
                //    ModelState.AddModelError(ViewRes.Controllers.Tests.IPDuplicated, ViewRes.Controllers.Tests.IPDuplicatedText);
                if (test.StartDate >= DateTime.Now)
                {
                    ViewData["StartDate"] = ViewRes.Controllers.Tests.StartDateText;
                    ModelState.AddModelError(ViewRes.Controllers.Tests.StartDate, ViewRes.Controllers.Tests.StartDateText);
                }
                if (DateTime.Now >= test.EndDate)
                {
                    ViewData["EndDate"] = ViewRes.Controllers.Tests.EndDateText;
                    ModelState.AddModelError(ViewRes.Controllers.Tests.EndDate, ViewRes.Controllers.Tests.EndDateText);
                }
            }
            else
            {
                ViewData["CodeNotFound"] = ViewRes.Controllers.Tests.CodeNotFoundText;
                ModelState.AddModelError(ViewRes.Controllers.Tests.CodeNotFound, ViewRes.Controllers.Tests.CodeNotFoundText);
            }

        }
    }
}
