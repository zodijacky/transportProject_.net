using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoovTn.Web.Models
{
    public class RentModel
    {


        public int idRent { get; set; }
        public int members { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateDeparture { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dateArrival { get; set; }
        public String Status { get; set; }
        public double latitudeDeparture { get; set; }
        public double longitudeDeparture { get; set; }

        public Double  Days { get; set; }

        public double latitudeArrival { get; set; }
        public double longitudeArrival { get; set; }
        
    }
}