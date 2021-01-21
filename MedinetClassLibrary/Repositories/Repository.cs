using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using MedinetClassLibrary.Models;

namespace MedinetClassLibrary.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DataClassesDataContext db = new DataClassesDataContext();

        public void Add(T entity)
        {
            try
            {
                db.GetTable<T>().InsertOnSubmit(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var entity = this.GetById(id);
                db.GetTable<T>().DeleteOnSubmit(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IQueryable<T> GetAllRecords()
        {
            return db.GetTable<T>();
        }

        public T GetById(int id)
        {
            try
            {
                var table = GetAllRecords();
                var mapping = db.Mapping.GetTable(typeof(T));
                var pkfield = mapping.RowType.DataMembers.SingleOrDefault(d => d.IsPrimaryKey);
                if (pkfield == null)
                    throw new Exception(String.Format("Table {0} does not contain a Primary Key field", mapping.TableName));
                var param = Expression.Parameter(typeof(T), "e");
                var predicate = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(param, pkfield.Name), Expression.Constant(id)), param);
                return table.SingleOrDefault(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void SaveChanges()
        {
            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Dictionary<string, string> GetEmptyDictionary()
        {
            var dic = new Dictionary<string, string>();
            dic.Add("", "");
            return dic;
        }

    }
}