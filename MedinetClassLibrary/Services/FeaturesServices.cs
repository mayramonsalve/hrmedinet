using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class FeaturesServices: IRepositoryServices<Feature>
    {
        private IRepository<Feature> _repository;

        public FeaturesServices()
            : this(new Repository<Feature>())
        {
        }

        public FeaturesServices(IRepository<Feature> repository)
        {
            _repository = repository;
        }

        public bool Add(Feature entity)
        {
            try
            {
                _repository.Add(entity);
                _repository.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IQueryable<Feature> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<Feature> GetFeaturesByType(int type_id)
        {
            return GetAllRecords().Where(t => t.FeedbackType_Id == type_id).OrderBy(t => t.Name);
        }

        public Dictionary<int, string> GetFeauresForDropDownList(int? type)
        {
            IQueryable<Feature> features;
            if(type.HasValue)
                features = GetFeaturesByType(type.Value).OrderBy(d => d.Name);
            else
                features = GetAllRecords().OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (Feature feat in features)
            {
                Dictionary.Add(feat.Id, feat.Name);
            }
            return Dictionary;
        }

        public Feature GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool IsNameDuplicated(int type, string name)
        {
            return GetAllRecords().Where(d => d.Name == name && d.FeedbackType_Id == type).Count() > 0;
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters, int type)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Feature> features = GetFeaturesByType(type);
            features = JqGrid<Feature>.GetFilteredContent(sidx, sord, page, rows, filters, features, ref totalPages, ref totalRecords);
            var rowsModel = (
                from feature in features.ToList()
                select new
                {
                    i = feature.Id,
                    cell = new string[] { feature.Id.ToString(), 
                            "<a href=\"/Features/Edit/"+feature.Id+"\">" + 
                            feature.Name + "</a>",
                            feature.Scores.Count > 0 ? GetAverageByFeature(feature.Id).ToString() : "-",
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Features/Edit/"+feature.Id+"\"><span id=\""+feature.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Features/Details/"+feature.Id+"\"><span id=\""+feature.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+feature.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Feature>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        private double GetAverageByFeature(int feature_id)
        {
            return GetById(feature_id).Scores.Select(s => s.Value).ToList().Average();
        }

    }
}
