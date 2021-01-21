using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using MedinetClassLibrary.Repositories;

namespace MedinetClassLibrary.Classes
{
    public static class JqGrid<T> where T : class, new() 
    {
        private static IRepository<T> _repository = new Repository<T>();

        public static IQueryable<T> GetFilteredContent( string sidx, string sord, int page, 
                                                        int rows, string filters, IQueryable<T> myModelSet,
                                                        ref int totalPages, ref int totalRecords)
        {
            Hashtable hashData = new Hashtable();
            ArrayList rules = new ArrayList();
            string operador = "";
            string key = "";
            string op = "";
            string field = "";
            string data = "";
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            string where = "";

            SetFilters(filters, ref hashData, ref rules, ref operador, ref key, ref op, ref field, ref data, ref where, ref myModelSet);
            OrderModelSet(ref myModelSet, sidx, sord, page, rows, ref totalPages, ref totalRecords);

            return myModelSet;
        }

        public static void OrderModelSet(ref IQueryable<T> modelSet, string sidx, string sord, int page, int rows, ref int totalPages, ref int totalRecords)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            totalRecords = modelSet.Count();
            totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            modelSet = modelSet.OrderBy(sidx + " " + sord).Skip(pageIndex * pageSize).Take(pageSize);
        }

        private static void SetFilters(string filters, ref Hashtable hashData, ref ArrayList rules, ref string operation, ref string key, ref string op, ref string field, ref string data, ref string where, ref IQueryable<T> modelSet)
        {
            if (String.IsNullOrEmpty(filters) == false)
            {
                hashData = (Hashtable)Json.JsonDecode(filters);

                IDictionaryEnumerator en = hashData.GetEnumerator();
                while (en.MoveNext())
                {
                    key = en.Key.ToString();

                    if (key == "rules")
                    {
                        rules = (ArrayList)en.Value;
                    }
                    else
                    {
                        operation = en.Value.ToString();
                    }
                }

                if (operation.ToUpper() == "AND")
                    operation = " && ";
                else
                    operation = " || ";
            }
            if (rules.Count != 0)
            {
                int cont = 0;
                foreach (Hashtable rule in rules)
                {
                    IDictionaryEnumerator en = rule.GetEnumerator();
                    while (en.MoveNext())
                    {
                        key = en.Key.ToString();

                        if (key == "op")
                        {
                            op = en.Value.ToString();

                        }
                        else if (key == "data")
                        {
                            data = en.Value.ToString();
                        }
                        else
                        {
                            field = en.Value.ToString();
                        }
                    }
                    if (cont > 0)
                        where += operation;
                    
                    if (op == "eq")
                        where += field + "==" + data;
                    else if (op == "ne")
                        where += field + "<>" + data;
                    else if (op == "lt")
                        where += field + "<" + data;
                    else if (op == "le")
                        where += field + "<=" + data;
                    else if (op == "gt")
                        where += field + ">" + data;
                    else if (op == "ge")
                        where += field + ">=" + data;
                    else if (op == "cn")
                        where += field + ".Contains(\"" + data + "\")";
                    cont++;
                }
                modelSet = modelSet.Where(where);
            }
        }

        public static object SetJsonData(int totalPages, int totalRecords, int page, object myRowsModel)
        {
            object jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = myRowsModel
            };

            return jsonData;
        }        
    }
}
