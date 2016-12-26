using MoovTn.Domain.Models;
using MoovTn.Service.Classes;
using MoovTn.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MoovTn.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TransportMeanController : Controller
    {

        ITransportMeanService service = null;

        public TransportMeanController()
        {
            service = new TransportMeanService();
        }
        // GET: TransportMean
        public ActionResult Index()
        {
            return View(service.GetMany());
        }

        // GET: TransportMean/Details/5
        public ActionResult Details(String id)
        {
            TransportMean transport = service.Get(t => t.serial == id);
            return View(transport);
        }

        // GET: TransportMean/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: TransportMean/Create
        [HttpPost]
        public ActionResult Create(TransportMean transport)
        {
           

            try
            {
                if (ModelState.IsValid)
                {
                    service.Add(transport);
                    service.Commit();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TransportMean/Edit/5
        public ActionResult Edit(String id)
        {
            TransportMean transport = service.Get(t => t.serial == id);
            return View(transport);
        }

        // POST: TransportMean/Edit/5
        [HttpPost]
        public ActionResult Edit(TransportMean transport)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    service.Update(transport);
                    service.Commit();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            service.Dispose();
        }

        // GET: TransportMean/Delete/5
        public ActionResult Delete(String id)
        {
            TransportMean transport = service.Get(t => t.serial == id);
            service.Delete(transport);
            service.Commit();
            return RedirectToAction("Index");
        }

        // POST: TransportMean/Delete/5
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
