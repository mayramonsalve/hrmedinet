using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class FeedbacksServices: IRepositoryServices<Feedback>
    {
        private IRepository<Feedback> _repository;

        public FeedbacksServices()
            : this(new Repository<Feedback>())
        {
        }

        public FeedbacksServices(IRepository<Feedback> repository)
        {
            _repository = repository;
        }

        public bool Add(Feedback entity)
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

        public IQueryable<Feedback> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public Feedback GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<string> GetStringsByType(string type, int type_id)
        {
            if (type == "AddComments")
                return _repository.GetAllRecords().Where(f => f.AddComments != null && f.AddComments != "" && f.FeedbackType_Id == type_id).Select(ac => ac.AddComments).ToList();
            else
                return _repository.GetAllRecords().Where(f => f.Comments != null && f.Comments != "" && f.FeedbackType_Id == type_id).Select(c => c.Comments).ToList();
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters, int type_id)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Feedback> feedbacks = _repository.GetAllRecords().Where(f => f.FeedbackType_Id == type_id);
            feedbacks = JqGrid<Feedback>.GetFilteredContent(sidx, sord, page, rows, filters, feedbacks, ref totalPages, ref totalRecords);
            var rowsModel = (
                from feedback in feedbacks.ToList()
                select new
                {
                    i = feedback.Id,
                    cell = new string[] { feedback.Id.ToString(), 
                            feedback.User == null ? "-" : feedback.User.FirstName + " " + feedback.User.LastName,
                            feedback.Comments == "" ? "-" : feedback.Comments,
                            feedback.AddComments== "" ? "-" : feedback.AddComments,
                            feedback.Scores == null ? "0" : feedback.Scores.Count.ToString(),
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Feedbacks/Details/"+feedback.Id+"\"><span id=\""+feedback.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+feedback.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Feedback>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }



    }
}
