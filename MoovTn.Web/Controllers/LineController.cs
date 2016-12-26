using MoovTn.Domain.Models;
using MoovTn.Service.Classes;
using MoovTn.Service.Interfaces;
using MoovTn.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MoovTn.Web.Controllers
{
    public class LineController : Controller
    {
        ILinestationsService linestationSvc = null;
        ILineService service = null;
        IStationService stationSvc = null;
        LineViewModel _model;
        public LineController()
        {
            linestationSvc = new LinestationsService();
            service = new LineService();
            stationSvc = new StationService();
            _model = new LineViewModel();
        }
        // GET: Line
        public ActionResult Index()
        {
            return View(service.GetMany());
        }

        // GET: Line/Details/5
        public ActionResult Details(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Line line = service.GetById(id);
            LineListModel model = new LineListModel { Id = line.id, Name = line.name, Path = line.path, Type = line.type };
            List<Station> stations = stationSvc.GetManyByLine(line.id).ToList();
            model.Stations = stationSvc.StationsListToJson(stations);
            if (line == null) return HttpNotFound();
            return View(model);
        }



        [Authorize(Roles = "Admin")]
        // GET: Line/Create
        public ActionResult Create()
        {
            IStationService stationSvc = new StationService();
            string[] liste = (string[])Session["stations"];
            foreach (string i in liste)
            {
                _model.SelectedStations.Add(stationSvc.GetById(int.Parse(i)));
            }
            _model.StationsJson = stationSvc.StationsListToJson(_model.SelectedStations);
            return View(_model);
        }
        [Authorize(Roles = "Admin")]
        // POST: Line/Create
        [HttpPost]
        public ActionResult Create(LineViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    int i = 1;
                    List<int> station = new List<int>();
                    List<int> ordre = new List<int>();
                    List<int> line = new List<int>();
                    String type = (string)Session["type"];
                    String nom = (string)Session["name"];
                    string[] liste = (string[])Session["stations"];

                    Line lineAdd = new Line { name = nom, type = type, path = model.Path };
                    service.Add(lineAdd);
                    service.Commit();
                    Line FindeLine = service.Get(l => l.name.Equals(nom));
                    int idLine = FindeLine.id;
                    foreach (string l in liste)
                    {

                        station.Add(int.Parse(l));
                        line.Add(idLine);
                        ordre.Add(i);
                        i = i + 1;

                    }

                    for (int j = 0; j < station.Count(); j++)
                    {
                        Linestation linestation = new Linestation { line_fk = line[j], ordre = ordre[j], station_fk = station[j] };
                        linestationSvc.Add(linestation);
                        linestationSvc.Commit();



                    }


                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CreateLine()
        {
            LineViewModel model = new LineViewModel();
            IStationService stationSvc = new StationService();
            model.AvailableStations = stationSvc.GetStationsByType("").ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateLine(string type,string name, string[] liste)
        {
            try
            {
               

                IStationService stationSvc = new StationService();
                _model.Name = name;
                _model.Type = type;
               

                Session["stations"] = liste;
                Session["name"] = name;
                Session["type"] = type;

                return Json(new { result = "Redirect", url = Url.Action("Create", "Line") });
               

                }
                catch
                {
                    return Json(new { result = "Error", url = Url.Action("Create", "Line") });
                }


               

        }

        public List<StationListModel> GetStationsModel(List<Station> liste)
        {
            List<StationListModel> stations = new List<StationListModel>();
            StationListModel station = null;
            foreach (Station s in liste)
            {
                station = new StationListModel { Id = s.id, Name = s.name };
                stations.Add(station);

            }

            return stations;
        }

        [HttpGet]

        public JsonResult GetStationsJson(string type)
        {
            
            IStationService stationSvc = new StationService();
            List<Station> stations = stationSvc.GetStationsByType(type).ToList();
            List<StationListModel> liste = GetStationsModel(stations);
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

        // GET: Line/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Line line = service.GetById(id);
            if (line == null) return HttpNotFound();
            return View(line);
        }

        // POST: Line/Edit/5
        [HttpPost]
        public ActionResult Edit(Line line)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    service.Update(line);
                    service.Commit();
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Line/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Line line = service.GetById(id);
            if (line == null) return HttpNotFound();
            service.Delete(line);
            service.Commit();


            return RedirectToAction("Index");
        }

        // POST: Line/Delete/5
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

        public ActionResult IndexFront()
        {
            LineMapModel model = new LineMapModel();

            return View(model);
        }

        public List<LineListModel> GetLinesModel(List<Line> liste)
        {
            List<LineListModel> lines = new List<LineListModel>();
            LineListModel line = null;
            foreach (Line l in liste)
            {
                List<Station> stationsList = stationSvc.GetManyByLine(l.id).ToList();
                string stations = stationSvc.StationsListToJson(stationsList);
                line = new LineListModel { Id = l.id, Name = l.name, Path = l.path, Stations = stations, Type = l.type };
                lines.Add(line);

            }

            return lines;
        }

        [HttpGet]

        public JsonResult GetLinesJson(string type, string name)
        {
            List<Line> lines = new List<Line>();
            if (name == "")
            {
                lines = service.GetLinesByType(type).ToList();
            }
            else
            {
                lines = service.GetLinesByName(type, name).ToList();
            }
            
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


        

    }
}
