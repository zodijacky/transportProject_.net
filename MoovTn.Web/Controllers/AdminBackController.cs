using MoovTn.Domain.Models;
using MoovTn.Service;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoovTn.Web.Controllers
{
    public class AdminBackController : Controller
    {
        IServiceClient usersrv = new ServiceClient();
     // IServiceAdminBack service= null;
        public AdminBackController()
        {
          //   service = new ServiceClient();
        }
        // GET: AdminBack
        public ActionResult Index()
            
        {
           List<User> users = usersrv.GetMany(null, null).ToList();

            return View(users);
        }

        // GET: AdminBack/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminBack/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminBack/Create
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

        // GET: AdminBack/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminBack/Edit/5
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

        // GET: AdminBack/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminBack/Delete/5
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
