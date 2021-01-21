using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ScoresServices: IRepositoryServices<Score>
    {
        private IRepository<Score> _repository;

        public ScoresServices()
            : this(new Repository<Score>())
        {
        }

        public ScoresServices(IRepository<Score> repository)
        {
            _repository = repository;
        }

        public bool Add(Score entity)
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

        public IQueryable<Score> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<Score> GetByFeature(int feature_id)
        {
            return _repository.GetAllRecords().Where(s => s.Feature_Id == feature_id);
        }

        public IQueryable<Score> GetByFeedback(int feedback_id)
        {
            return _repository.GetAllRecords().Where(s => s.Feedback_Id == feedback_id);
        }

        public Score GetByFeedbackAndFeature(int feedback_id, int feature_id)
        {
            return _repository.GetAllRecords().Where(s => s.Feedback_Id == feedback_id && s.Feature_Id == feature_id).FirstOrDefault();
        }

        public int GetScoreValueByFeedbackAndFeature(int feedback_id, int feature_id)
        {
            int sco = 0;
            Score score = _repository.GetAllRecords().Where(s => s.Feedback_Id == feedback_id && s.Feature_Id == feature_id).FirstOrDefault();
            if (score != null)
                sco = score.Value;
            return sco;
        }

        public List<int> GetScoresByFeature(int feature_id)
        {
            List<int> scores = _repository.GetAllRecords().Where(s => s.Feature_Id == feature_id).Select(v => v.Value).ToList();
            return scores;
        }

        public Dictionary<int, string> GetOptionsForDropDownList()
        {
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            Dictionary.Add(1, "Not so great");
            Dictionary.Add(2, "Quite good");
            Dictionary.Add(3, "Good");
            Dictionary.Add(4, "Great!");
            Dictionary.Add(5, "Excellent!!");
            return Dictionary;
        }

        public Dictionary<int, string> GetAllOptionsForDropDownList()
        {
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            Dictionary.Add(1, "Very poor");
            Dictionary.Add(2, "Very poor");
            Dictionary.Add(3, "Poor");
            Dictionary.Add(4, "Poor");
            Dictionary.Add(5, "Not that bad");
            Dictionary.Add(6, "Not that bad");
            Dictionary.Add(7, "Fair");
            Dictionary.Add(8, "Fair");
            Dictionary.Add(9, "Average");
            Dictionary.Add(10, "Average");
            Dictionary.Add(11, "Almost good");
            Dictionary.Add(12, "Almost good");
            Dictionary.Add(13, "Good!");
            Dictionary.Add(14, "Good!");
            Dictionary.Add(15, "Very good!");
            Dictionary.Add(16, "Very good!");
            Dictionary.Add(17, "Excellent!!");
            Dictionary.Add(18, "Excellent!!");
            Dictionary.Add(19, "Perfect!!");
            Dictionary.Add(20, "Perfect!!");
            return Dictionary;
        }

        public Score GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }



    }
}
