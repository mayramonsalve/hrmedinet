using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.Models;
using Medinet.Models.ViewModels;
using MedinetClassLibrary.AuthorizationModels;
using System.Web.Security;
using System.Collections;
using MedinetClassLibrary.Classes;

namespace Medinet.Controllers
{
    [HandleError]
    public class FeedbacksController : Controller
    {
        private FeedbacksServices feedbackService;
        private FeedbackViewModel feedbackViewModel;

        public FeedbacksController()
        {
            feedbackService = new FeedbacksServices();
        }

        public FeedbacksController(FeedbacksServices feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        //[Authorize(Roles = "HRAdministrator, HRCompany, CompanyManager")]
        public ActionResult SendFeedback()
        {
            int type_id;
            type_id = GetTypeIdByUserAuthenticated();
            InitializeViews(null, "Send", type_id);
            return View(feedbackViewModel);
        }

        private int GetTypeIdByUserAuthenticated()
        {
            int type_id;
            if (Session["Type"] != null)
                type_id = Convert.ToInt32(Session["Type"].ToString());
            else
            {
                List<string> roles = new List<string>();
                roles.Add("HRAdministrator"); roles.Add("HRCompany"); roles.Add("CompanyManager");
                if (!User.Identity.IsAuthenticated)
                {
                    if (Session["Type"] != null)
                        type_id = Convert.ToInt32(Session["Type"].ToString());
                    else
                        type_id = 3;
                }
                else
                {
                    if (roles.Contains(new UsersServices().GetByUserName(User.Identity.Name).Role.Name))
                        type_id = 1;
                    else
                        type_id = 3;
                }
            }
            return type_id;
        }
        
        [HttpPost]
        //[Authorize(Roles = "HRAdministrator, HRCompany, CompanyManager")]
        public ActionResult SendFeedback(FormCollection collection)
        {
            Feedback feedback = GenerateFeedbackObject(collection);
            ValidateFFeedbackModel(feedback, Convert.ToBoolean(collection["Add"]));
            int? type = null;
            if (ModelState.IsValid)
            {
                if (feedbackService.Add(feedback))
                {
                    InsertScoresByFeature(collection, feedback.Id);
                    //SendMail(feedback);
                    type = 0;
                    //return RedirectToAction("Index", "Home");
                    if (Request.Browser.IsMobileDevice)
                    {
                        return View("SendFeedbackSucceded");
                    }
                }
            }
            InitializeViews(null, "Send", type);
            return View(feedbackViewModel);
        }

        private void SendMail(Feedback feedback)
        {
            string Body = "Feedback enviado: \n \n";
                   if(feedback.User_Id.HasValue)
                   {
                       Body += "Usuario: " + feedback.User.FirstName + " " + feedback.User.LastName + "\n" +
                               "Compañía: " + feedback.User.Company.Name + "\n";
                   }
                   Body += "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy") + "\n" +
                       "Tipo de feedback: " + feedback.FeedbackType.Name + "\n";
                   if (feedback.AddComments != "")
                   {
                       Body += "Sugerencias: " + feedback.AddComments + "\n";
                   }
                   if (feedback.Comments != "")
                   {
                       Body += "Comentarios: " + feedback.Comments + "\n";
                   }
                   foreach (Feature feature in feedback.FeedbackType.Features)
                   {
                       Score score = feedback.Scores.Where(f => f.Feature_Id == feature.Id).FirstOrDefault();
                       if(score != null)
                           Body += "Puntuación para '" + feature.Name + "': " + score.Value + "\n";
                   }
            try
            {
                EmailBroadcaster.SendEmail("Feedback enviado desde HRMedinet", Body, "mayra.monsalve@hrmedinet.com, info@hrmedinet.com");
            }
            catch
            {
                ModelState.AddModelError(ViewRes.Controllers.Account.UserName, new Exception());
            }
        }

        private Feedback GenerateFeedbackObject(FormCollection collection)
        {
            int? user_id;
            int type = GetTypeIdByUserAuthenticated();
            if (User.Identity.IsAuthenticated)
                user_id = new UsersServices().GetByUserName(User.Identity.Name).Id;
            else
                user_id = null;
            Feedback feedback = new Feedback();
            feedback.AddComments = collection["feedback.AddComments"];
            feedback.Comments = collection["feedback.Comments"];
            feedback.User_Id = user_id;
            feedback.FeedbackType_Id = type;
            feedback.Show = false;
            return feedback;
        }

        private void InsertScoresByFeature(FormCollection collection, int feedback_id)
        {
            ScoresServices scoreService = new ScoresServices();
            foreach (Feature feature in new FeaturesServices().GetAllRecords())
            {
                string featureInFeedback = collection[feature.Id.ToString()];
                if (featureInFeedback != "" && featureInFeedback != null)
                {
                    Score score = new Score();
                    score.Feedback_Id = feedback_id;
                    score.Value = Int32.Parse(featureInFeedback);
                    score.Feature_Id = feature.Id;
                    score.CreationDate = DateTime.Now;
                    scoreService.Add(score);
                }
            }
        }

        [HttpPost]
        [Authorize(Users = "Administrator")]
        public ActionResult Delete(int id, FormCollection collection)
        {
                try
                {
                    feedbackService.Delete(id);
                    feedbackService.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    InitializeViews(id, "", null);
                    return RedirectToAction("Index");
                }
        }

        [Authorize(Users = "Administrator")]
        public ActionResult Details(int id)
        {
            InitializeViews(id, "", null);
            return View(feedbackViewModel);
        }

        [Authorize(Users = "Administrator")]
        public ActionResult Index(int? type_id)
        {
            if (type_id == 0)
                type_id = null;
            InitializeViews(null, "Index", type_id);
            return View(feedbackViewModel);
        }

        [Authorize(Users = "Administrator")]
        public ActionResult ShowFeedbacks(int? type_id)
        {
            if (type_id == 0)
                type_id = null;
            InitializeViews(null, "Show", type_id);
            return View(feedbackViewModel);
        }

        [Authorize(Users = "Administrator")]
        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters, int? type_id)
        {
            if (type_id != null)
            {
                object resultado = feedbackService.RequestList(sidx, sord, page, rows, filters, (int)type_id);
                return Json(resultado);
            }
            else
                return null;
        }

        private Dictionary<int, int> GenerateScoreByFeature(Feedback feedback)
        {
            Dictionary<int, int> ScoreByFeature = new Dictionary<int, int>();
            ScoresServices scoreService = new ScoresServices();
            foreach (Feature feature in new FeaturesServices().GetAllRecords())
            {
                int score = scoreService.GetScoreValueByFeedbackAndFeature(feedback.Id, feature.Id);
                if (score != 0)
                    ScoreByFeature.Add(feature.Id, score);
            }
            return ScoreByFeature;
        }

        private Dictionary<int, List<int>> GenerateScoreAverageByFeature(int type_id)
        {
            Dictionary<int, List<int>> ScoreByFeature = new Dictionary<int, List<int>>();
            ScoresServices scoreService = new ScoresServices();
            foreach (Feature feature in new FeaturesServices().GetFeaturesByType(type_id))
            {
                List<int> score = scoreService.GetScoresByFeature(feature.Id);
                if (score != null)
                    ScoreByFeature.Add(feature.Id, score);
            }
            return ScoreByFeature;
        }

        [HttpPost]
        public JsonResult UpdateDivs(string stringComment, int type_id)
        {
            return Json(feedbackService.GetStringsByType(stringComment, type_id));
        }

        [HttpPost]
        public JsonResult UpdateStars(int type_id)
        {
            List<object> features = new List<object>();
            foreach (Feature feature in new FeaturesServices().GetFeaturesByType(type_id))
            {
                double avg = feature.Scores.Count > 0 ? feature.Scores.Select(s => s.Value).Average() : 0;
                features.Add(
                    new
                    {
                        featureId = feature.Id,
                        featureName = feature.Name,
                        featureAverage = avg,
                        featureCount = feature.Scores.Count,
                        featureScore = Convert.ToInt32(avg * 4)
                    });
            }
            return Json(features);
        }

        public JsonResult GetOptionsList()
        {
            List<object> options = new List<object>();
            ScoresServices scoreService = new ScoresServices();
            foreach (var option in scoreService.GetAllOptionsForDropDownList())
            {
                options.Add(
                    new
                    {
                        optionValue = option.Key,
                        optionDisplay = option.Value,
                    });
            }
            return Json(options);
        }

        
        [HttpPost]
        public bool UpdateShow(int feedback_id)
        {
            Feedback feedback = feedbackService.GetById(feedback_id);
            if (feedback.Show)
                feedback.Show = false;
            else
                feedback.Show = true;
            feedbackService.SaveChanges();
            return feedback.Show;
        }

        private void InitializeViews(int? feedback_id, string action, int? type)
        {
            Feedback feedback;
            Dictionary<int, object> featuresByType = new Dictionary<int, object>();
            Dictionary<int, string> features;
            SelectList options;
            SelectList typesList;

            if (feedback_id.HasValue)
            {
                feedback = feedbackService.GetById(feedback_id.Value);
                Dictionary<int, int> scoreByFeature = GenerateScoreByFeature(feedback);
                features = new FeaturesServices().GetFeauresForDropDownList(feedback.FeedbackType_Id);
                options = new SelectList(new ScoresServices().GetOptionsForDropDownList(), "Key", "Value");
                feedbackViewModel = new FeedbackViewModel(feedback, features, options, feedback.Scores.Count, scoreByFeature);
            }
            else
            {
                switch(action)
                {
                    case "Send":
                        bool show = false;
                        if (type.HasValue)
                            if (type == 0)
                                show = true;
                        feedback = new Feedback();
                        if (type.HasValue)
                            features = new FeaturesServices().GetFeauresForDropDownList(type.Value);
                        else
                            features = new Dictionary<int, string>();
                        options = new SelectList(new ScoresServices().GetOptionsForDropDownList(), "Key", "Value");
                        feedbackViewModel = new FeedbackViewModel(feedback, features, options, show);
                        break;
                    case "Index":
                        if(type.HasValue)
                            typesList = new SelectList(new FeedbackTypesServices().GetFeedbackTypesForDropDownList(), "Key", "Value", type);
                        else
                            typesList = new SelectList(new FeedbackTypesServices().GetFeedbackTypesForDropDownList(), "Key", "Value");
                        feedbackViewModel = new FeedbackViewModel(typesList);
                        break;
                    case "Show":
                        string typeName;
                        Dictionary<int, List<int>> scoreAverageByFeature;
                        options = new SelectList(new ScoresServices().GetAllOptionsForDropDownList(), "Key", "Value");
                        List<string> addCommentsStrings;
                        List<string> commentsStrings;
                        if (type.HasValue)
                        {
                            typeName = new FeedbackTypesServices().GetById(type.Value).Name;
                            addCommentsStrings = feedbackService.GetStringsByType("AddComments", type.Value);
                            commentsStrings = feedbackService.GetStringsByType("Comments", type.Value);
                            scoreAverageByFeature = GenerateScoreAverageByFeature(type.Value);
                            features = new FeaturesServices().GetFeauresForDropDownList(type.Value);
                            typesList = new SelectList(new FeedbackTypesServices().GetFeedbackTypesForDropDownList(), "Key", "Value", type.Value);
                        }
                        else
                        {
                            typeName = "";
                            addCommentsStrings = new List<string>();
                            commentsStrings = new List<string>();
                            scoreAverageByFeature = new Dictionary<int, List<int>>();
                            features = new Dictionary<int,string>();
                            typesList = new SelectList(new FeedbackTypesServices().GetFeedbackTypesForDropDownList(), "Key", "Value");
                        }
                        feedbackViewModel = new FeedbackViewModel(features, options, scoreAverageByFeature, addCommentsStrings, commentsStrings, typesList, typeName);
                        break;
                }
            }
        }

        private void ValidateFFeedbackModel(Feedback feedback, bool Add)
        {
            if (feedback.AddComments =="" && feedback.Comments == "" && !Add)
                ModelState.AddModelError(ViewRes.Controllers.Feedbacks.EmptyFields, ViewRes.Controllers.Feedbacks.EmptyFieldsText);
        }

    }
}
