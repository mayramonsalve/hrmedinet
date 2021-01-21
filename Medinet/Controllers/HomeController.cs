using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using Medinet.Models;
using MedinetClassLibrary.Models;
using Medinet.Models.ViewModels;
using System.Net.Mail;
using MedinetClassLibrary.Classes;
using System.Net;
using MedinetClassLibrary.CustomClasses;
//using Medinet.Helpers;

namespace Medinet.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        LogOnModel lg = new LogOnModel();
        private ContactUsServices _contactUsService;
        private ContactUsViewModel _contactUsViewModel;

        //[RedirectMobileDevicesToMobileArea]
        public ActionResult Index()
        {
            //HttpContext.ClearOverriddenBrowser();
            if (!Request.IsAuthenticated)
            {
                ViewBag.Message = ViewRes.Views.Home.Index.Welcome;
            }
            else
            {
                ViewBag.Message = ViewRes.Views.Home.Index.Welcome
                    + new UsersServices().GetByUserName(User.Identity.Name).FirstName
                    + " "
                    + new UsersServices().GetByUserName(User.Identity.Name).LastName;
            }
            return View();
        }
        //[RedirectMobileDevicesToMobileAboutPage]
        public ActionResult AboutUs() {
            return View();
        }

        public ActionResult ContactUs()
        {
            ContactUs contactUs = new ContactUs();
            //SelectList countriesList = new SelectList(new CountriesServices().GetCountriesForDropDownList(), "Key", "Value");
            _contactUsViewModel = new ContactUsViewModel(contactUs, null);
            return View(_contactUsViewModel);
        }

        [HttpPost]
        public ActionResult ContactUs([Bind(Prefix = "cont")] ContactUs contactUs)
        {
            _contactUsService = new ContactUsServices();
            contactUs.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (_contactUsService.Add(contactUs))
                {
                    SendMail(contactUs);
                    return RedirectToAction("ContactSucceeded");
                }
            }
            contactUs = new ContactUs();
            SelectList countriesList = new SelectList(new CountriesServices().GetCountriesForDropDownList(), "Key", "Value");
            _contactUsViewModel = new ContactUsViewModel(contactUs, countriesList);
            return View(_contactUsViewModel);
        }

        public ActionResult ContactSucceeded()
        {
            return View();
        }

        public ActionResult ShowMap()
        {
            return View();
        }

        private void SendMail(ContactUs contactUs)
        {
            //string Body = "Message from Medinet Web: \n \n" +
            //           "Country: " + contactUs.Country.Name + "\n" +
            //           "Company: " + contactUs.Company + "\n" +
            //           "Date: " + contactUs.Date + "\n" +
            //           "Name: " + contactUs.Name + "\n" +
            //           "Email: " + contactUs.Email + "\n" +
            //           "Address: " + contactUs.Address + "\n" +
            //           "Phone: " + contactUs.Phone + "\n" +
            //           "Description: " + contactUs.Description + "\n";
            //try
            //{
            //    EmailBroadcaster.SendEmail("Contact Mail from Medinet Web", Body, "mayra.monsalve@hrmedinet.com, info@hrmedinet.com");
            //}
            //catch
            //{
            //    ModelState.AddModelError(ViewRes.Controllers.Account.UserName, new Exception());
            //}
            var message = new MailMessage("info.hrmedinet@gmail.com", "info@hrmedinet.com, jose.ardila@hrmedinet.com, luis.ramirez@hrmedinet.com")
            {
                Subject = "Contact Mail from Medinet Web",
                Body = "Message from Medinet Web: <br />" +
                       "-------------------------<br /><br />" +
                       "Country: " + contactUs.Country.Name + "<br />" +
                       "Company: " + contactUs.Company + "<br />" +
                       "Date: " + contactUs.Date + "<br />" +
                       "Name: " + contactUs.Name + "<br />" +
                       "Email: " + contactUs.Email + "<br />" +
                       "Address: " + contactUs.Address + "<br />" +
                       "Phone: " + contactUs.Phone + "<br />" +
                       "Description: " + contactUs.Description + "<br />",
                IsBodyHtml = true
            };
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("info.hrmedinet@gmail.com", "t3n1d3mHr21");
            client.Send(message);

            //var client = new SmtpClient("smtp.hrmedinet.com",25);
            //client.UseDefaultCredentials = true;
            //client.EnableSsl = true;
            //client.Send(message);
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult AssessmentCentres()
        {
            return View();
        }
        public ActionResult PerformanceEvaluations()
        {
            return View();
        }
        public ActionResult OrganizationalClimate()
        {
            return View();
        }

        public ActionResult HRMedinet()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult Event()
        {
            return View();
        }

        //public JsonResult GetInfoForMap(int? country_id, int company_id)
        //{
        //    int company;
        //    TestsServices testService = new TestsServices();
        //    if (company_id != 0)
        //        company = company_id;
        //    else
        //        company = new UsersServices().GetByUserName(User.Identity.Name).Company_Id;
        //    Dictionary<int, Test> lastTests = new Dictionary<int, Test>();
        //    if (!country_id.HasValue)
        //    {
        //        List<Country> countriesByCompany = new LocationsServices().GetCountriesByCompany(company);
        //        foreach (Country country in countriesByCompany)
        //        {
        //            Test aux = testService.GetLastTestByCompanyAndCountry(company, country.Id);
        //            if (aux != null)
        //                lastTests.Add(country.Id, aux);
        //        }
        //    }
        //    else
        //    {
        //        List<State> statesByCompany = new LocationsServices().GetStatesByCompany(company, country_id.Value);
        //        foreach (State state in statesByCompany)
        //        {
        //            Test aux = testService.GetLastTestByCompanyAndState(company, state.Id);
        //            if (aux != null)
        //                lastTests.Add(state.Id, aux);
        //        }
        //    }
        //    List<object> finalList = new List<object>();
        //    Dictionary<string[], double> listClimate = new Dictionary<string[], double>();
        //    if (country_id.HasValue)
        //    {
        //        foreach (Test t in lastTests.Values.Distinct())
        //        {
        //            List<int> statesInt = lastTests.Where(v => v.Value == t).Select(k => k.Key).ToList();
        //            listClimate = t.GetGeneralPctgByState(country_id.Value, statesInt);
        //            //listClimate = new Results(t).GetPositiveAnswersPercentageByState(country_id.Value, statesInt);
        //            AddToFinalList(finalList, t, listClimate);
        //        }
        //    }
        //    else
        //    {
        //        foreach (Test t in lastTests.Values.Distinct())
        //        {
        //            List<int> countriesInt = lastTests.Where(v => v.Value == t).Select(k => k.Key).ToList();
        //            //listClimate = new Results(t).GetPositiveAnswersPercentageByCountry(countriesInt);
        //            listClimate = t.GetGeneralPctgByCountry(countriesInt);
        //            AddToFinalList(finalList, t, listClimate);
        //        }
        //    }
        //    return Json(finalList);
        //}


        public JsonResult GetInfoForMap(int test_id, int? country_id)
        {
            Test test = new TestsServices().GetById(test_id);
            List<object> finalList = new List<object>();
            Dictionary<string[], double> listClimate = new Dictionary<string[], double>();
            if (country_id.HasValue)
            {
                List<int> statesInt = new EvaluationsServices().GetStatesByTest(test_id, country_id.Value);
                listClimate = test.GetGeneralPctgByState(country_id.Value, statesInt);
                AddToFinalList(finalList, test, listClimate);
            }
            else
            {
                List<int> countriesInt = new EvaluationsServices().GetCountriesByTest(test_id);
                listClimate = test.GetGeneralPctgByCountry(countriesInt);
                AddToFinalList(finalList, test, listClimate);
            }
            return Json(finalList);
        }

        private void AddToFinalList(List<object> finalList, Test test, Dictionary<string[], double> listClimate)
        {
            foreach (var place in listClimate)
            {
                string[] info = place.Key;
                finalList.Add(
                    new
                    {
                        test = test.Id,
                        code = info[0] != null ? info[0] : "-",
                        name = info[1],
                        climate = String.Format("{0:0.##}", place.Value) + "%",
                        color = GetColorByClimate(place.Value),
                        map = info[2],
                        id = info[3]
                    });
            }
        }

        [Authorize(Roles = "HRAdministrator, HRCompany, CompanyManager")]
        public ActionResult ClimateMap(int? test_id)
        {
            Session["test_id"] = test_id;
            return View();
        }

        private string GetColorByClimate(double climate)
        {
            string green = "#00B386";
            string amber = "#FECE00";
            string red = "#FF004C";
            string gray = "#CCCCCC";
            if (climate >= 80)
                return green;
            else if (climate >= 60 && climate < 80)
                return amber;
            else if (climate < 60)
                return red;
            else return gray;
        }


        [Authorize(Roles = "HRAdministrator, HRCompany, CompanyManager")]
        public string GetHRef(int test)
        {
            string href = "-";
            List<int> countries;
            Dictionary<int, string> maps = new Dictionary<int, string>()
            {
                { 2,"Venezuela" },
                { 23,"Argentina"},
                { 61, "Colombia"},
                { 247,"United States"}
            };;
            countries = new EvaluationsServices().GetCountriesByTest(test);
            if (countries.Count == 1 && maps.Keys.Contains(countries[0]))
            {
                href = "/Home/ClimateStateMap?country_id=" + countries.FirstOrDefault();
                href = href + "&test_id=" + test; 
            }
            return href;
        }

        [Authorize(Roles = "HRAdministrator, HRCompany, CompanyManager")]
        [HttpGet]
        public ActionResult ClimateStateMap(int country_id, int? test_id)
        {
            Country c = new CountriesServices().GetById(country_id);
            Session["test_id"] = test_id;
            Session["country_id"] = country_id;
            Session["country_name"] = c.Name;
            Session["urlMap"] = c.Map;
            return View();
        }

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            return Redirect(returnUrl);
        }

        public JsonResult SetVariable(int width)
        {
            if (width > 600)
            {
                Session["mobile"] = "tablet";
            }
            else
            {
                Session["mobile"] = "mobile";
            }

            return this.Json("asada");
        }

        public ActionResult MapUs()
        {
            return View();
        }


    }
}
