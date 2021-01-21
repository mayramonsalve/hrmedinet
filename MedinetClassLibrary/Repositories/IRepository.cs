using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace MedinetClassLibrary.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllRecords();
        T GetById(int id);
        void SaveChanges();
        void Add(T entidad);
        void Delete(int id);
        Dictionary<string, string> GetEmptyDictionary();
    }
}