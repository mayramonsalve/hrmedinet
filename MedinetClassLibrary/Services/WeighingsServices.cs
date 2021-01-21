using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;
using System.Text.RegularExpressions;

namespace MedinetClassLibrary.Services
{
    public class WeighingsServices : IRepositoryServices<Weighing>
    {
        private IRepository<Weighing> _repository;

        public WeighingsServices()
            : this(new Repository<Weighing>())
        {
        }

        public WeighingsServices(IRepository<Weighing> repository)
        {
            _repository = repository;
        }

        public bool Add(Weighing entity)
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

        public IQueryable<Weighing> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public Weighing GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Weighing> GetByTest(int test_id)
        {
            return _repository.GetAllRecords().Where(w => w.Test_Id == test_id).OrderBy(w => w.Category_Id);
        }

        public decimal GetValueByTestAndCategory(int test_id, int category_id)
        {
            return _repository.GetAllRecords().Where(w => w.Test_Id == test_id && w.Category_Id == category_id).FirstOrDefault().Value;
        }

        public IQueryable<Weighing> GetByCategory(int category_id)
        {
            return _repository.GetAllRecords().Where(w => w.Category_Id == category_id).OrderBy(w => w.Category_Id);
        }

        public IQueryable<Weighing> GetByQuestionnaire(int questionnaire_id)
        {//ARREGLAR
            return _repository.GetAllRecords();//.Where(w => w.Category.Questionnaire_Id == questionnaire_id).OrderBy(w => w.Category_Id);
        }

        public decimal GetWeighingCount(int test_id)
        {
            if (GetByTest(test_id).Count() == 0)
                return 0;
            else
                return GetByTest(test_id).Select(w => w.Value).Sum();
        }

        public decimal GetWeighingCount(System.Web.Mvc.FormCollection collection, Questionnaire questionnaire)
        {
            decimal sum = 0;
            foreach(Category cat in questionnaire.Categories)
            {
                sum += decimal.Parse(collection["weighing." + cat.Id]);
            }
            return sum;
        }

        public bool IsWeighingNull(System.Web.Mvc.FormCollection collection, Questionnaire questionnaire)
        {
            bool validate = true;
            foreach (Category cat in questionnaire.Categories)
            {
                validate = validate && (collection["weighing." + cat.Id]!="");
            }
            return !validate;
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public bool AreNumerics(System.Web.Mvc.FormCollection collection, Questionnaire questionnaire)
        {
            bool validate = true;
            string regEx = @"^\d{1,3}(\.\d{1,3})?$";
            foreach (Category cat in questionnaire.Categories)
            {
                if (Regex.IsMatch(collection["weighing." + cat.Id], regEx))
                    validate = validate && true;
                else
                    validate = validate && false;
            }
            return validate;
        }
    }
}
