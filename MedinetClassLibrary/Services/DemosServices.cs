using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class DemosServices: IRepositoryServices<Demo>
    {
        private IRepository<Demo> _repository;

        public DemosServices()
            : this(new Repository<Demo>())
        {
        }

        public DemosServices(IRepository<Demo> repository)
        {
            _repository = repository;
        }

        public bool Add(Demo entity)
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

        public IQueryable<Demo> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public Demo GetById(int id)
        {
            return _repository.GetById(id);
        }

        private List<object> GetForGrid()
        {
            IQueryable<Company> companies = GetAllRecords().Select(c => c.Company);
            List<object> demos = new List<object>();
            foreach (Company comp in companies)
            {
                Test test = comp.Tests.FirstOrDefault();
                demos.Add(new
                {
                    company = comp.Name,
                    code = test.Code,
                    employees = test.CurrentEvaluations,
                    dates = test.StartDate + " - " + test.EndDate,
                    user = comp.Users.FirstOrDefault().UserName
                });
            }
            return demos;
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Demo> demos = GetAllRecords();
            demos = JqGrid<Demo>.GetFilteredContent(sidx, sord, page, rows, filters, demos, ref totalPages, ref totalRecords);
            var rowsModel = (
                from demo in demos.ToList()
                select new
                {
                    i = demo.Id,
                    cell = new string[] { demo.Id.ToString(), 
                            "<a href=\"/Demos/Edit/"+demo.Id+"\">" + 
                            demo.Company.Name + "</a>",
                            demo.Company.Tests.FirstOrDefault().Code,
                            demo.Company.Tests.FirstOrDefault().CurrentEvaluations.ToString(),
                            demo.Company.Tests.FirstOrDefault().StartDate.ToString(ViewRes.Views.Shared.Shared.Date) + " - " + demo.Company.Tests.FirstOrDefault().EndDate.ToString(ViewRes.Views.Shared.Shared.Date),
                            demo.Company.Users.FirstOrDefault().UserName,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Demos/Edit/"+demo.Id+"\"><span id=\""+demo.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Demos/Details/"+demo.Id+"\"><span id=\""+demo.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+demo.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Demo>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }



    }
}
