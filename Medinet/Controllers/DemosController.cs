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
    [Authorize(Roles = "Administrator")]
    public class DemosController : Controller
    {
        private DemosServices demoService;
        private DemoViewModel demoViewModel;

        public DemosController()
        {
            demoService = new DemosServices();
        }

        public DemosController(DemosServices demoService)
        {
            this.demoService = demoService;
        }

        public ActionResult Create()
        {
            InitializeViews(null, "Create");
            return View(demoViewModel);
        }
        
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            DemographicTemplate demoTemplate = new DemographicTemplate(collection["Company"], collection["Email"],
                                                    Int32.Parse(collection["Weeks"]), Int32.Parse(collection["Employees"]));
            if (demoTemplate.Ok)
                {
                    Demo demo = new Demo();
                    demo.Company_Id = demoTemplate.Company.Id;
                    demo.Weeks = Int32.Parse(collection["Weeks"]);
                    if (demoService.Add(demo))
                        return RedirectToAction("Details", new { @id = demo.Id });
                    else
                    {
                        new CompaniesServices().Delete(demoTemplate.Company.Id);
                        new CompaniesServices().SaveChanges();
                    }
                }

            //}
            InitializeViews(null, "Create");
            return View(demoViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
                try
                {
                    demoService.Delete(id);
                    demoService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id, "Index");
                    return RedirectToAction("Index");
                }
        }

        public ActionResult Details(int id)
        {
                InitializeViews(id, "Details");
                return View(demoViewModel);
        }

        public ActionResult Edit(int id)
        {
                InitializeViews(id, "Edit");
                return View(demoViewModel);
        }

        //[HttpPost]
        public int CreateDemographics(int companyId, int countryId, int languageId)
        {
            DemographicTemplate demoTemplate = new DemographicTemplate(companyId, countryId, languageId);
            if (demoTemplate.Ok)
                return 1;
            else
                return 0;
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
                try
                {
                    Demo demo = demoService.GetById(id);
                    if (Int32.Parse(collection["Weeks"]) != demo.Weeks)
                        demo.Weeks = Int32.Parse(collection["Weeks"]);
                    new TestsServices().AddWeeksAndEmployeesToTest(demo.Company.Tests.FirstOrDefault().Id,
                        Int32.Parse(collection["Weeks"])-demo.Weeks, Int32.Parse(collection["Employees"]));
                    UpdateModel(demo);
                    demoService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id, "Edit");
                    return View(demoViewModel);
                }
        }

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
        {
            object resultado = demoService.RequestList(sidx, sord, page, rows, filters);
            return Json(resultado);
        }

        public ActionResult Index()
        {
            InitializeViews(null, "Index");
            return View(demoViewModel);
        }

        private Dictionary<int, string> GetLanguages()
        {
            Dictionary<int, string> languages = new Dictionary<int, string>();
            languages.Add(1, "Español");
            languages.Add(2, "English");
            return languages;
        }

        private void InitializeViews(int? demo_id, string action)
        {
            Demo demo;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());
            switch (action)
            {
                case "Create":
                    SelectList languageList = new SelectList(GetLanguages(), "Key", "Value");
                    SelectList countryList = new SelectList(new CountriesServices().GetCountriesForDropDownList(),"Key", "Value");
                    demo = new Demo();
                    demoViewModel = new DemoViewModel(countryList, languageList, 1, 10);
                    break;

                case "Edit":
                    demo = demoService.GetById((demo_id.Value));
                    demoViewModel = new DemoViewModel(demo.Weeks, demo.Company.Tests.FirstOrDefault().EvaluationNumber);
                    break;

                case "Details":
                    demo = demoService.GetById((demo_id.Value));
                    Test test = demo.Company.Tests.FirstOrDefault();
                    User user_test = demo.Company.Users.FirstOrDefault();
                    demoViewModel = new DemoViewModel(demo.Company.Name, demo.Weeks,
                        test.EvaluationNumber, test.CurrentEvaluations, test.Code,
                        user_test.Email, user_test.UserName, test.CreationDate.ToString("ddHmmss"),
                        test.StartDate.ToString(ViewRes.Views.Shared.Shared.Date),
                        test.EndDate.ToString(ViewRes.Views.Shared.Shared.Date));
                    break;

                case "Index":
                    demo = new Demo();
                    demoViewModel = new DemoViewModel(demo);
                    break;
            }
        }

    }
}
