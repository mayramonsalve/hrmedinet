using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class OptionBySelectionAnswersServices : IRepositoryServices<OptionBySelectionAnswer>
    {
        private IRepository<OptionBySelectionAnswer> _repository;

        public OptionBySelectionAnswersServices()
            : this(new Repository<OptionBySelectionAnswer>())
        {
        }

        public OptionBySelectionAnswersServices(IRepository<OptionBySelectionAnswer> repository)
        {
            _repository = repository;
        }

        public bool Add(OptionBySelectionAnswer entity)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("demographic", "OPTIONBYSELECTIONANSWER");
                parameters.Add("option", entity.Option_Id);
                parameters.Add("selectionanswer", entity.SelectionAnswer_Id);
                List<object[]> aux = new Commands("Add", parameters).GetData();
                //return aux.Count == 0;

                //_repository.Add(entity);
                //_repository.SaveChanges();

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

        public IQueryable<OptionBySelectionAnswer> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<OptionBySelectionAnswer> GetBySelectionAnswer(int selectionanswer_id)
        {
            return _repository.GetAllRecords().Where(t => t.SelectionAnswer_Id == selectionanswer_id);
        }

        public IQueryable<OptionBySelectionAnswer> GetByOption(int option_id)
        {
            return _repository.GetAllRecords().Where(t => t.Option_Id == option_id);
        }

        public OptionBySelectionAnswer GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}