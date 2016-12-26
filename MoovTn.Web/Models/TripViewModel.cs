using MoovTn.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoovTn.Web.Models
{
    public class TripViewModel
    {
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime DepartureTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime ArrivalDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime ArrivalTime { get; set; }
        public IEnumerable<SelectListItem> Lines { get; set; }

        public int Line { get; set; }

       
        public string Type { get; set; }

        public IEnumerable<SelectListItem> TransportMeans { get; set; }
        public string Transport { get; set; }

        public TripViewModel()
        {
            DepartureDate = DateTime.Now;
            DepartureTime = DateTime.Now;
            ArrivalDate = DateTime.Now;
            ArrivalTime = DateTime.Now.AddHours(1);
        }
    }
}