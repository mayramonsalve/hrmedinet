using System;
using System.Web.Mvc;
using System.Data.Linq;
using System.Collections;
using System.IO;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Collections.Generic;

namespace Medinet.Models.CustomExcelModels
{
    public class ExcelRead
    {
        public string Name { get; set; }

        public List<ExcelContent> excelContent;
       
    }

    public class ExcelContent
    {
        public string Category { get; set; }
        public string Question { get; set; }
        public string Positive { get; set; }
        public string Type { get; set; }
        public string Option { get; set; }
        public string Value { get; set; }
    }
}