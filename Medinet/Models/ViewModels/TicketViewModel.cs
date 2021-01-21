using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;
using MedinetClassLibrary.Services;

namespace Medinet.Models.ViewModels
{
    public class TicketViewModel : Controller
    {

        public List<Ticket> tickets { get; set; }

        public TicketViewModel()
        {
            tickets = new List<Ticket>();

        }

    }
}
