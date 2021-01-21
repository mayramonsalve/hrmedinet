using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ClimateRangesServices : IRepositoryServices<ClimateRange>
    {
        private IRepository<ClimateRange> _repository;

        public ClimateRangesServices()
            : this(new Repository<ClimateRange>())
        {
        }

        public ClimateRangesServices(IRepository<ClimateRange> repository)
        {
            _repository = repository;
        }

        public bool Add(ClimateRange entity)
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

        public IQueryable<ClimateRange> GetAllRecords()
        {
            return _repository.GetAllRecords().OrderByDescending(o => o.MaxValue);
        }

        public ClimateRange GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ClimateRange GetByValue(decimal value, int climateScale_id)
        {
            return _repository.GetAllRecords().Where(c => c.ClimateScale_Id == climateScale_id && (value > c.MinValue && value <= c.MaxValue)).FirstOrDefault();
        }

        public IQueryable<ClimateRange> GetByClimateScale(int climateScale_id)
        {
            return _repository.GetAllRecords().Where(q => q.ClimateScale_Id == climateScale_id).OrderByDescending(c => c.MaxValue);
        }

        public Dictionary<string, string> GetEmptyDictionary()
        {
            return _repository.GetEmptyDictionary();
        }

        public object RequestList(int climateScale_id, string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<ClimateRange> climateRanges = GetByClimateScale(climateScale_id);
            climateRanges = JqGrid<ClimateRange>.GetFilteredContent(sidx, sord, page, rows, filters, climateRanges, ref totalPages, ref totalRecords);
            var rowsModel = (
                from climateRange in climateRanges.ToList()
                select new
                {
                    i = climateRange.Id,
                    cell = new string[] { climateRange.Id.ToString(), 
                            "<a href=\"/ClimateRanges/Edit/"+climateRange.Id+"\">" + 
                            climateRange.Name + "</a>",
                            climateRange.MinValue.ToString(),
                            climateRange.MaxValue.ToString(),
                            climateRange.Action,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/ClimateRanges/Edit/"+climateRange.Id+"\"><span id=\""+climateRange.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/ClimateRanges/Details/"+climateRange.Id+"\"><span id=\""+climateRange.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+climateRange.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();
            return JqGrid<ClimateRange>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public bool IsValueDuplicated(int climateScale_id, decimal value)
        {
            return GetByValue(value, climateScale_id) != null;
        }

        public bool IsNameDuplicated(int climateScale_id, string name)
        {
            return GetByClimateScale(climateScale_id).Where(c => c.Name == name).Count() > 0;
        }

        public bool IsColorDuplicated(int climateScale_id, string color)
        {
            return GetByClimateScale(climateScale_id).Where(c => c.Color == color).Count() > 0;
        }

    }
}