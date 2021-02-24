using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using Medinet.Models.CustomExcelModels;

namespace Medinet.Class.CustomExcelClass
{
    public static class Utility
    {
        public static ExcelRead ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            ExcelRead ExcelRead = new ExcelRead();
            ExcelRead.Name = Path.GetFileName(strFilePath);
            ExcelRead.excelContent = new List<ExcelContent>();

            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }

            }

            foreach (DataRow dr in dt.Rows)
            {
                ExcelRead.excelContent.Add(new ExcelContent()
                {
                    Category = dr[0].ToString(),
                    Question = dr[1].ToString(),
                    Positive = dr[2].ToString(),
                    Type = dr[3].ToString(),
                    Option = dr[4].ToString(),
                    Value = dr[5].ToString()
                });
            }

            return ExcelRead;
        }

        public static ExcelRead ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ExcelRead ExcelRead = new ExcelRead();
            ExcelRead.Name = Path.GetFileName(strFilePath);
            ExcelRead.excelContent = new List<ExcelContent>();
            try
            {

                oledbConn.Open();
                using (DataTable Sheets = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null))
                {

                    for (int i = 0; i < Sheets.Rows.Count; i++)
                    {
                        string worksheets = Sheets.Rows[i]["TABLE_NAME"].ToString();
                        OleDbCommand cmd = new OleDbCommand(String.Format("SELECT * FROM [{0}]", worksheets), oledbConn);
                        OleDbDataAdapter oleda = new OleDbDataAdapter();
                        oleda.SelectCommand = cmd;

                        oleda.Fill(ds);
                    }

                    dt = ds.Tables[0];
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {

                oledbConn.Close();
            }

            foreach (DataRow dr in dt.Rows)
            {
                ExcelRead.excelContent.Add(new ExcelContent()
                {
                    Category = dr[0].ToString(),
                    Question = dr[1].ToString(),
                    Positive = dr[2].ToString(),
                    Type = dr[3].ToString(),
                    Option = dr[4].ToString(),
                    Value = dr[5].ToString()
                });
            }

            return ExcelRead;

        }
    }
}