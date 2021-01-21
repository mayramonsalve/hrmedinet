using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ContactUsServices: IRepositoryServices<ContactUs>
    {
        private IRepository<ContactUs> _repository;

        public ContactUsServices()
            : this(new Repository<ContactUs>())
        {
        }

        public ContactUsServices(IRepository<ContactUs> repository)
        {
            _repository = repository;
        }

        public bool Add(ContactUs entity)
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

        public IQueryable<ContactUs> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public ContactUs GetById(int id)
        {
            return _repository.GetById(id);
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<ContactUs> contact = GetAllRecords();
            contact = JqGrid<ContactUs>.GetFilteredContent(sidx, sord, page, rows, filters, contact, ref totalPages, ref totalRecords);
            var rowsModel = (
                from cont in contact.ToList()
                select new
                {
                    i = cont.Id,
                    cell = new string[] { cont.Id.ToString(), 
                            cont.Name,
                            cont.Company,
                            cont.Date.ToString(ViewRes.Views.Shared.Shared.Date),
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/ContactUs/Details/"+cont.Id+"\"><span id=\""+cont.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+cont.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Age>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }



    }
}
