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
    public class FeaturesController : Controller
    {
        private FeaturesServices _featureService;
        private FeatureViewModel _featureViewModel;

        public FeaturesController()
        {
            _featureService = new FeaturesServices();
        }

        public FeaturesController(FeaturesServices _featureService)
        {
            this._featureService = _featureService;
        }

        public ActionResult Create(int? type_id)
        {
            if (type_id == 0)
                type_id = null;
            InitializeViews(null, type_id);
            return View(_featureViewModel);
        }
        
        [HttpPost]
        public ActionResult Create(Feature feature)
        {
            ValidateFeatureModel(feature);
            if (ModelState.IsValid)
            {
                if (_featureService.Add(feature))
                    return RedirectToAction("Index", new { @type_id = feature.FeedbackType_Id });
            }
            InitializeViews(null, null);
            return View(_featureViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _featureService.Delete(id);
                _featureService.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                InitializeViews(id, null);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int id)
        {
            InitializeViews(id, null);
            return View(_featureViewModel);
        }

        public ActionResult Edit(int id)
        {
            InitializeViews(id, null);
            return View(_featureViewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Feature feature = _featureService.GetById(id);
                UpdateModel(feature, "feature");
                _featureService.SaveChanges();
                return RedirectToAction("Index", new { @type_id = feature.FeedbackType_Id });
            }
            catch
            {
                InitializeViews(id, null);
                return View(_featureViewModel);
            }
        }

        public ActionResult GridData(string sidx, string sord, int page, int rows, string filters, int? type_id)
        {
            if (type_id != null)
            {
                object resultado = _featureService.RequestList(sidx, sord, page, rows, filters, (int)type_id);
                return Json(resultado);
            }
            else
                return null;
        }

        public ActionResult Index(int? type_id)
        {
            if (type_id == 0)
                type_id = null;
            InitializeViews(null, type_id);
            return View(_featureViewModel);
        }

        [HttpPost]
        public JsonResult GetFeaturesByType(int type_id)
        {
            List<object> features = new List<object>();
            foreach (var feature in _featureService.GetFeauresForDropDownList(type_id))
            {
                features.Add(
                    new
                    {
                        optionValue = feature.Key,
                        optionDisplay = feature.Value
                    });
            }

            return Json(features);
        }

        private void InitializeViews(int? feature_id, int? type_id)
        {
            Feature feature;
            SelectList typesList;
            User user = new UsersServices().GetByUserName(User.Identity.Name.ToString());

            if (feature_id != null)
            {
                feature = _featureService.GetById((int)feature_id);
                typesList = new SelectList(new FeedbackTypesServices().GetFeedbackTypesForDropDownList(), "Key", "Value", feature.FeedbackType_Id);
            }
            else
            {
                feature = new Feature();
                if (type_id.HasValue)
                {
                    feature.FeedbackType_Id = type_id.Value;
                    typesList = new SelectList(new FeedbackTypesServices().GetFeedbackTypesForDropDownList(), "Key", "Value", type_id.Value);
                }
                else
                    typesList = new SelectList(new FeedbackTypesServices().GetFeedbackTypesForDropDownList(), "Key", "Value");
            }
            _featureViewModel = new FeatureViewModel(feature, typesList);
        }

        private void ValidateFeatureModel(Feature feature)
        {
            if (_featureService.IsNameDuplicated(feature.FeedbackType_Id, feature.Name))
                ModelState.AddModelError(ViewRes.Controllers.Shared.Name, ViewRes.Controllers.Shared.NameText);
        }

    }
}
