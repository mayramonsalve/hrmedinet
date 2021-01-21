using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public interface IRepositoryServices<T> where T : class
    {
        bool Add(T entity);
        bool Delete(int id);
        IQueryable<T> GetAllRecords();
        T GetById(int id);
        void SaveChanges();
    }
}
