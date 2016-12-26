
using MoovTn.Domain.Models;
using MoovTn.Service.Classes;
using MoovTn.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MoovTn.Web.Models
{
    public class TripStationModel
    {
        public IEnumerable<SelectListItem> Stations { get; set; }


        public Station StationArrivee { get; set; }
        public Station StationDepart { get; set; }
        public List<Trip> trips { get; set; }
        [DataType(DataType.Date)]
     
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time{ get; set; }
        public int SArrivId { get; set; }
        public int SDepId { get; set; }
        public string EstimationTime { get; set; }
        public string ArrivalTime { get; set; }
        private IStationService stationSvc;
        private ITripService tripSvc;
        public TripStationModel()
        {
            stationSvc = new StationService();
            tripSvc = new TripService();
            Date = DateTime.Now;
            Time = DateTime.Now;
            Stations = GetStations();
            
            
        }

        private IEnumerable<SelectListItem> GetStations()
        {
            var stations = stationSvc.GetMany().Select(x => new SelectListItem { Value = x.id.ToString(), Text = x.name });
            return new SelectList(stations, "Value", "Text");
        }

    }
}
