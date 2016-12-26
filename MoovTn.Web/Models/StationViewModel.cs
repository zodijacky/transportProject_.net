using MoovTn.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoovTn.Web.Models
{
    public class StationViewModel
    {
        public String Stations { get; set; }
        public Station Station { get; set; }

        public String nom { get; set; }

        public String type { get; set; }

    }
}