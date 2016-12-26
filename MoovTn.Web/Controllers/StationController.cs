using MoovTn.Domain.Models;
using MoovTn.Service.Classes;
using MoovTn.Service.Interfaces;
using MoovTn.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MoovTn.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StationController : Controller
    {
        IStationService service = null;
        public StationController()
        {
            service = new StationService();
        }
        public ActionResult Index()
        {
            return View(service.GetMany());
        }

        // GET: Station/Details/5
        public ActionResult Details(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Station station = service.GetById(id);
            StationViewModel model = new StationViewModel();
            var jsonSerialiser = new JavaScriptSerializer();
            model.Stations = service.findOneToJson(id);
            model.nom = station.name;
            model.type = station.type;
            if (station == null) return HttpNotFound();
            return View(model);
        }

        // GET: Station/Create
        public ActionResult Create()
        {
            StationViewModel model = new StationViewModel ();
            var jsonSerialiser = new JavaScriptSerializer();
            model.Stations = service.findAllToJson();
            


            return View(model);
        
        }

        // POST: Station/Create
        [HttpPost]
        public ActionResult Create(Station station)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    service.Add(station);
                    service.Commit();
                    return RedirectToAction("Index");
                }


                return View();


            }
            catch
            {
                return View();
            }
        }

        // GET: Station/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Station station = service.GetById(id);
            if (station == null) return HttpNotFound();
            return View(station);
        }

        // POST: Station/Edit/5
        [HttpPost]
        public ActionResult Edit(Station station)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    service.Update(station);
                    service.Commit();
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Station/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Station station = service.GetById(id);
            if (station == null) return HttpNotFound();
            service.Delete(station);
            service.Commit();


            return RedirectToAction("Index");
        }

        // POST: Station/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
