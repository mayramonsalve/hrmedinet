using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class DemographicSelectorDetailsServices : IRepositoryServices<DemographicSelectorDetail>
    {
        private IRepository<DemographicSelectorDetail> _repository;

        public DemographicSelectorDetailsServices()
            : this(new Repository<DemographicSelectorDetail>())
        {
        }

        public DemographicSelectorDetailsServices(IRepository<DemographicSelectorDetail> repository)
        {
            _repository = repository;
        }

        public bool Add(DemographicSelectorDetail entity)
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

        public bool deletebytest(int id)
        {//borra de la tabla DemographicSelectorDetail los campos con el test dado
            try
            {
                /*select Id from DemographicSelectorDetails
                where Test_Id=134*/
                DemographicSelectorDetailsServices demo = new DemographicSelectorDetailsServices();
                List<DemographicSelectorDetail> demlist = demo.GetAllRecords().ToList();
                List<DemographicSelectorDetail> listfinal = new List<DemographicSelectorDetail>();

                var sql = (
                        from dem in demlist
                        where dem.Test_Id == id
                        select new { dem.Id }
                     ).ToList();///obtiene los id de demografico que se tiene en DemographicSelectorDetails por medicion(Test_Id)

                foreach (var qe in sql)
                {
                    _repository.Delete(qe.Id);//borra uno a uno los demograficos
                }

                SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool deletebydemogandquestionandtest(DemographicSelectorDetail entity)
        {//borra de la tabla DemographicSelectorDetail los campos con los valores de demografico,cuestionario y test dados
            try
            {
                /*_repository.Delete(entity.Id);
                  select Id from DemographicSelectorDetails
  where Questionnaire_Id=31 AND Test_Id=134 AND Demographic_Id=2;
                 
                 */
                DemographicSelectorDetailsServices demo = new DemographicSelectorDetailsServices();
                List<DemographicSelectorDetail> demlist = demo.GetAllRecords().ToList();
                List<DemographicSelectorDetail> listfinal = new List<DemographicSelectorDetail>();
                int i = 0, contador = 0;
                var sql = (
                        from dem in demlist
                        where dem.Test_Id == entity.Test_Id && dem.Questionnaire_Id == entity.Questionnaire_Id && dem.Demographic_Id == entity.Demographic_Id
                        select new { dem.Id }
                     ).ToList();///obtiene los id de demografico que se tiene en DemographicSelectorDetails por medicion(Test_Id) y cuestionario(Questionnaire_Id) sin repetirse

                contador = sql.Count();//cuenta los demografico que trajo la consulta anterior

                int[] num = new int[contador];

                foreach (var qe in sql)
                {
                    _repository.Delete(qe.Id);//borra uno a uno los demograficos
                }

                SaveChanges();                
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IQueryable<DemographicSelectorDetail> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<DemographicSelectorDetail> GetByTest(int test_id)//////////obtiene el detalle del demografico selector seleccionado(mayra)
        {
            var una= _repository.GetAllRecords().Where(t => t.Questionnaire_Id==t.Questionnaire.Id && t.Test_Id==test_id).Distinct();
            return _repository.GetAllRecords().Where(t => t.Questionnaire_Id == t.Questionnaire.Id && t.Test_Id == test_id);
           
        }

        public int[] GetByTestDemog_Id(int test_id, int quest)
        {//////////obtiene el Id del demografico selector segun el cuestionario y el test(rossana)

            /*  select Demographic_Id from DemographicSelectorDetails
  where Questionnaire_Id=1 AND Test_Id=134;*/
            
            DemographicSelectorDetailsServices demo = new DemographicSelectorDetailsServices();
            List<DemographicSelectorDetail> demlist = demo.GetAllRecords().ToList();
            List<DemographicSelectorDetail> listfinal = new List<DemographicSelectorDetail>();
            int i = 0, contador = 0;
            var sql = (
                    from dem in demlist
                    where dem.Test_Id == test_id && dem.Questionnaire_Id == quest
                    select new { dem.Demographic_Id}
                 ).ToList();///obtiene los id de demografico que se tiene en DemographicSelectorDetails por medicion(Test_Id) y cuestionario(Questionnaire_Id) sin repetirse

            contador = sql.Count();//cuenta los demografico que trajo la consulta anterior

            int[] num = new int[contador];

            foreach (var qe in sql)
            {
                num[i] = qe.Demographic_Id;
                //listfinal.Add(num[i]);
                i++;
            }

            //select dem.Questionnaire_Id
            return num;

        }

        public int[] GetByTestR(int test_id)
        {//////////obtiene el detalle del demografico selector seleccionado(rossana)

            DemographicSelectorDetailsServices demo = new DemographicSelectorDetailsServices();
            List<DemographicSelectorDetail> demolist = demo.GetAllRecords().ToList();            
            List<DemographicSelectorDetail> listfinal = new List<DemographicSelectorDetail>();
            int i = 0,contador=0;
            var sql = (
                    from dem in demolist
                    where dem.Test_Id == test_id
                    select new { dem.Questionnaire_Id }
                 ).Distinct().ToList();///obtiene los id de cuestionarios que se tiene en DemographicSelectorDetails por medicion(Test_Id) sin repetirse
            
            contador=sql.Count();//cuenta los cuestionarios que trajo la consulta anterior

            int[] num = new int[contador];

            foreach (var qe in sql)
            {
                num[i] = qe.Questionnaire_Id;
                //listfinal.Add(num[i]);
                i++;
            }

            //select dem.Questionnaire_Id
            return num;
        
        }

        public Dictionary<int, string> GetQuestionnairesByTestForDropDownList(int test_id)
        {
            Dictionary<int, string> questionnaires = new Dictionary<int, string>();
            foreach (Questionnaire q in _repository.GetAllRecords().Where(t => t.Test_Id == test_id).Select(q => q.Questionnaire).OrderBy(l => l.Name))
            {
                questionnaires.Add(q.Id, q.Name);
            }
            return questionnaires;
        }

        public int GetQuestionnaireIdByDemographicSelectorDetailValues(int test_id, int dem_id, int selector_id)
        {
            return GetByTest(test_id).Where(d => d.Demographic_Id == dem_id && d.SelectorValue_Id == selector_id).FirstOrDefault().Questionnaire_Id;
        }

        public DemographicSelectorDetail GetById(int id)
        {
            return _repository.GetById(id);
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters, int test_id)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<DemographicSelectorDetail> demographicSelectorDetails = GetByTest(test_id);
            demographicSelectorDetails = JqGrid<DemographicSelectorDetail>.GetFilteredContent(sidx, sord, page, rows, filters, demographicSelectorDetails, ref totalPages, ref totalRecords);
            var rowsModel = (
                from demographicSelectorDetail in demographicSelectorDetails.ToList()
                select new
                {
                    i = demographicSelectorDetail.Id,
                    cell = new string[] { demographicSelectorDetail.Id.ToString(), 
                            "<a href=\"/DemographicsInTest/Edit/"+demographicSelectorDetail.Id+"\">" + 
                            demographicSelectorDetail.Demographic.Name + "</a>",
                            demographicSelectorDetail.Test.Name,
                            demographicSelectorDetail.Questionnaire.Name,
                            demographicSelectorDetail.SelectorValue_Id.ToString(),
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/DemographicSelectorDetails/Edit/"+demographicSelectorDetail.Id+"\"><span id=\""+demographicSelectorDetail.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/DemographicSelectorDetails/Details/"+demographicSelectorDetail.Id+"\"><span id=\""+demographicSelectorDetail.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+demographicSelectorDetail.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<DemographicSelectorDetail>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public bool IsQuestionnaireDuplicated(int test_id, int dem_id, int selector_id)
        {
            return GetQuestionnaireIdByDemographicSelectorDetailValues(test_id, dem_id, selector_id) != null;
        }

        

    }
}
