using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class DemographicsInTestsServices : IRepositoryServices<DemographicsInTest>
    {
        private IRepository<DemographicsInTest> _repository;

        public DemographicsInTestsServices()
            : this(new Repository<DemographicsInTest>())
        {
        }

        public DemographicsInTestsServices(IRepository<DemographicsInTest> repository)
        {
            _repository = repository;
        }

        public bool Add(DemographicsInTest entity)
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

        public bool deletebydemogandtest(DemographicsInTest entity)///borra de la tabla demographicsInTest aquellos campos que tengan el id de la medicion y el id del demografico enviado
        {
            try
            {
                /*_repository.Delete(entity.Id);
                  select Id from DemographicSelectorDetails
  where Questionnaire_Id=31 AND Test_Id=134 AND Demographic_Id=2;
                 
                 */
                DemographicsInTestsServices demo = new DemographicsInTestsServices();
                List<DemographicsInTest> demlist = demo.GetAllRecords().ToList();
                List<DemographicsInTest> listfinal = new List<DemographicsInTest>();
                int i = 0, contador = 0;
                var sql = (
                        from dem in demlist
                        where dem.Test_Id == entity.Test_Id && dem.Demographic_Id == entity.Demographic_Id //&& dem.Selector == 0
                        select new { dem.Id }
                     ).ToList();///obtiene los id de demografico que se tiene en DemographicSelectorDetails por medicion(Test_Id) y cuestionario(Questionnaire_Id) sin repetirse

                contador = sql.Count();//cuenta los demografico que trajo la consulta anterior

                int[] num = new int[contador];

                foreach (var qe in sql)
                {
                    _repository.Delete(qe.Id);//borra uno a uno los demograficos en el test
                }

                SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public IQueryable<DemographicsInTest> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<DemographicsInTest> GetByTest(int test_id)
        {
            int aux = test_id;
            var vacio = _repository.GetAllRecords().Where(t => t.Test_Id == test_id).OrderBy(l => l.Demographic.Name);
            return _repository.GetAllRecords().Where(t => t.Test_Id == test_id).OrderBy(l => l.Demographic.Name);
        }

        public int[] GetByTestDemographic_Id(int test_id)
        {//////////obtiene el Id del demografico segun el test(rossana)

            /*  Select Demographic_Id from DemographicsInTest where Test_Id=135
             */

            DemographicsInTestsServices demo = new DemographicsInTestsServices();
            List<DemographicsInTest> demlist = demo.GetAllRecords().ToList();
            List<DemographicsInTest> listfinal = new List<DemographicsInTest>();
            int i = 0, contador = 0;
            var sql = (
                    from dem in demlist
                    where dem.Test_Id == test_id
                    select new { dem.Demographic_Id }
                 ).Distinct().ToList();///obtiene los id de demografico que se tiene en DemographicsInTest por medicion(Test_Id) sin repetirse
                                       
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

        public bool GetDemographicIsSelector(int test_id, int dem_id)
        {
            return GetByTest(test_id).Where(d => d.Demographic_Id == dem_id && d.Selector).Count() > 0;
        }

        public DemographicsInTest GetSelector(int test_id)
        {
            var demoselec = GetByTest(test_id).Where(d => d.Selector).FirstOrDefault();
            return GetByTest(test_id).Where(d => d.Selector).FirstOrDefault();
        }

        public DemographicsInTest GetById(int id)
        {
            return _repository.GetById(id);
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters, int test_id)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<DemographicsInTest> demographicsInTests = GetByTest(test_id);
            demographicsInTests = JqGrid<DemographicsInTest>.GetFilteredContent(sidx, sord, page, rows, filters, demographicsInTests, ref totalPages, ref totalRecords);
            var rowsModel = (
                from demographicsInTest in demographicsInTests.ToList()
                select new
                {
                    i = demographicsInTest.Id,
                    cell = new string[] { demographicsInTest.Id.ToString(), 
                            "<a href=\"/DemographicsInTest/Edit/"+demographicsInTest.Id+"\">" + 
                            demographicsInTest.Demographic.Name + "</a>",
                            demographicsInTest.Test.Name,
                            demographicsInTest.Selector ? ViewRes.Classes.Services.True : ViewRes.Classes.Services.False,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Ages/Edit/"+demographicsInTest.Id+"\"><span id=\""+demographicsInTest.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Ages/Details/"+demographicsInTest.Id+"\"><span id=\""+demographicsInTest.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+demographicsInTest.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<DemographicsInTest>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

    }
}
