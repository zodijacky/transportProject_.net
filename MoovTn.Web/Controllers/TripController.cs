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
    public class TripController : Controller
    {

        ITripService service;

        public TripController()
        {
            service = new TripService();
        }
        // GET: Trip
        public ActionResult Index()
        {
            return View(service.GetMany());
        }

        // GET: Trip/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        private IEnumerable<SelectListItem> GetLines()
        {
            ILineService lineSvc = new LineService();
            var lines = lineSvc.GetLinesByType("").Select(x =>
                        new SelectListItem
                        {
                            Value = x.id.ToString(),
                            Text = x.name
                        });
            

            return new SelectList(lines, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetTransportMeans()
        {
            ITransportMeanService transportSvc = new TransportMeanService();
            var liste = transportSvc.GetAllByType("").Select(x =>
                        new SelectListItem
                        {
                            Value = x.serial,
                            Text = x.serial
                        });


            return new SelectList(liste, "Value", "Text");
        }



        // GET: Trip/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            
            TripViewModel model = new TripViewModel();

            model.Lines = GetLines();
            model.TransportMeans = GetTransportMeans();
            
            return View(model);
        }

        // POST: Trip/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(TripViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime departure = new DateTime(model.DepartureDate.Year,model.DepartureDate.Month,model.DepartureDate.Day,model.DepartureTime.Hour,model.DepartureTime.Minute,model.DepartureTime.Second);
                    DateTime arrival = new DateTime(model.ArrivalDate.Year, model.ArrivalDate.Month, model.ArrivalDate.Day, model.ArrivalTime.Hour, model.ArrivalTime.Minute, model.ArrivalTime.Second);
                    
                    
                
                    Trip trip = new Trip ();
                    trip.departureTime = departure;
                    trip.arrivalTime = arrival;
                    trip.line_id = model.Line;
                    trip.transportMean_serial = model.Transport;
                    service.Add(trip);
                    service.Commit();
                    
                }
                

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
        }

        public List<LineListModel> GetLinesModel(List<Line> liste)
        {
            List<LineListModel> lines = new List<LineListModel>();
            LineListModel line = null;
            foreach(Line l in liste)
            {
                line = new LineListModel { Id = l.id, Name = l.name };
                lines.Add(line);

            }

            return lines;
        }

        [HttpGet]
        
        public JsonResult GetLinesJson(string type)
        {
            ILineService lineSvc = new LineService();
            

            List<Line> lines = lineSvc.GetLinesByType(type).ToList();
            List<LineListModel> liste = GetLinesModel(lines);


            if (Request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    Data = liste,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = "Not valid request",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpGet]

        public JsonResult GetTransportMeansJson(string type, DateTime departureDate, DateTime departureTime, DateTime arrivalDate, DateTime arrivalTime)
        {
            ITransportMeanService transportSvc = new TransportMeanService();
            IRentService rentSvc = new RentService();

            DateTime departure = new DateTime(departureDate.Year, departureDate.Month, departureDate.Day, departureTime.Hour, departureTime.Minute, departureTime.Second);
            DateTime arrival = new DateTime(arrivalDate.Year, arrivalDate.Month, arrivalDate.Day, arrivalTime.Hour, arrivalTime.Minute, arrivalTime.Second);

            List<TransportMean> transports = transportSvc.GetAllByType(type).ToList();
            List<string> liste = new List<string>();
            foreach(TransportMean t in transports)
            {
                if(!service.isOnTrip(t,departure,arrival) && !rentSvc.isRent(t, departure))
                {
                    liste.Add(t.serial);

                }
            }

            if (Request.IsAjaxRequest())
            {
                return new JsonResult
                {
                    Data = liste,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = "Not valid request",
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        // GET: Trip/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Trip/Edit/5
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

        // GET: Trip/Delete/5
        public ActionResult Delete(int id)
        {
            Trip trip = service.GetById(id);
            service.Delete(trip);
            service.Commit();
            return RedirectToAction("Index");
        }

        // POST: Trip/Delete/5
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
    }
}
