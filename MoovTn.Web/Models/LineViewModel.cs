using MoovTn.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoovTn.Web.Models
{
    public class LineViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<Station> AvailableStations { get; set; }

        public List<Station> SelectedStations { get; set; }


        public string[] selectIds { get; set; }

        public string StationsJson { get; set; }

        public string Path { get; set; }

        public LineViewModel()
        {
            SelectedStations = new List<Station>();
        }


    }

    public class LineMapModel
    {
        public string SearchType { get; set; }
        public string SearchName { get; set; }

        public List<LineListModel> Lines { get; set; }
        
       

    }




   


}