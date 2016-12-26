
using MoovTn.Data.Infrastructure;
using MoovTn.Domain.Models;
using MoovTn.Service.Classes;
using MoovTn.Service.Interfaces;
using MoovTn.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoovTn.Web.Controllers
{
    
    public class TripStationController : Controller
    {
        private ITripService tripSvc;
        private ILineService linesvc;
        private ILinestationsService linestationsvc;
        private IEnumerable<Trip> Trips1 = null;
        private List<Trip> TripByTime;
        private List<Trip> TripsOfOneLine= new List<Trip>();
        private List<Trip> TripsBySandD = new List<Trip>();
        
    

        private static IDatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);

        public TripStationController()
        {
            tripSvc = new TripService();
            linesvc = new LineService();
            linestationsvc = new LinestationsService();
        }

        // GET: TripStation
        public ActionResult Index()
        {
            TripStationModel model = new TripStationModel();
             model.trips = tripSvc.GetTripByTime(model.Date).ToList();
           
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(TripStationModel viewmodel)
        {
            DateTime DT = new DateTime(viewmodel.Date.Year, viewmodel.Date.Month, viewmodel.Date.Day, viewmodel.Time.Hour, viewmodel.Time.Minute, viewmodel.Time.Second);
              TripByTime = tripSvc.GetTripByTime(DT).ToList();

            List<Line> lines1 = linesvc.TestStationInLine(viewmodel.SArrivId).ToList();
            List<Line> lines2 = linesvc.TestStationInLine(viewmodel.SDepId).ToList();
            var LineIdList = from dep in lines1
                             join arv in lines2 on dep.id equals arv.id 
                             select dep.id;
            

            var Line = LineIdList.ToArray();

            foreach (var LineId in LineIdList)
            {
                
                 Trips1= ut.getRepository<Trip>().GetMany(s => s.line_id == LineId);
                 TripsOfOneLine = Trips1.Concat(TripsOfOneLine).ToList();
                
            }
          

            viewmodel.trips = new List<Trip>();
            foreach(Trip t1 in TripsOfOneLine)
            {
                foreach(Trip t2 in TripByTime)
                {
                    if(t1.id == t2.id)
                    {

                        TripsBySandD.Add(t1);
                    }
                }
            }
          

            foreach (Trip t1 in TripsBySandD)
            {
                
                if (t1.line_id != null)
                {
                    int? id = t1.line_id;
                    Line line = linesvc.GetById((int)id);
                    int Stationcount = line.linestations.Count();
                    int orderS1 = linestationsvc.GetStationOrder(line.id, viewmodel.SArrivId);
                    int orderS2 = linestationsvc.GetStationOrder(line.id, viewmodel.SDepId);
                    string TimeEstimation = GetEstimation((DateTime)t1.arrivalTime, orderS1, Stationcount);
                    viewmodel.EstimationTime = TimeEstimation;
                    viewmodel.ArrivalTime = CalculateArrivalTime((DateTime)t1.departureTime, (DateTime)t1.arrivalTime, orderS1,Stationcount);
                    if (orderS1>orderS2)
                    {  
                        viewmodel.trips.Add(t1);
                    }
                }
                
            }


                return View(viewmodel);

        }
        public ActionResult values()
        {
           

            return View();
        }
        // GET: TripStation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TripStation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TripStation/Create
        [HttpPost]
        
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TripStation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TripStation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TripStation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TripStation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public String GetEstimation(DateTime d, int order, int count)
        {
           // if (d == DateTime.Now)
            
                DateTime now = DateTime.Now;
                TimeSpan diff = (TimeSpan)(now - d);
                double diffMin = diff.TotalMinutes;
                double timeEstimationd = diffMin * order / count;
                TimeSpan timeEstimationt = TimeSpan.FromMinutes(timeEstimationd);

                return timeEstimationt.ToString(@"dd\.hh\:mm");
            
            
        }
        public String CalculateArrivalTime(DateTime d, DateTime a, int order, int count)
        {
            TimeSpan diff = a - d;
       
            double diffMin = diff.TotalMinutes;
            double timeEstimationd = diffMin * order / count;
            TimeSpan timeEstimationt = TimeSpan.FromMinutes(timeEstimationd);
           TimeSpan arrivaltime =d.TimeOfDay.Add(timeEstimationt);
            return arrivaltime.ToString();
        }

    }
}
