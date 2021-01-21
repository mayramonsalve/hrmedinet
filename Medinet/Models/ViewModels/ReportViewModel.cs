using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{
    public class ReportViewModel
    {
        public int company_id { get; private set; }

        public ReportViewModel( int? company_id)
        {
                this.company_id = company_id.Value;
        }
    }
}