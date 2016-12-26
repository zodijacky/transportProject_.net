using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Terradue.ServiceModel.Syndication;
using MyTransport.Web.Models;
using MoovTn.Data.Infrastructure;
using MoovTn.Data;
using MoovTn.Domain.Models;
using Microsoft.AspNet.Identity;

namespace MyTransport.Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class notificationsController : Controller
    {
        private static IDatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);
        private transportdsContext db = new transportdsContext();

        public ActionResult Posts()
        {
            
            var notif =db.notifications.ToList().Select(p => new SyndicationItem(p.line.name, p.description,new Uri("http://localhost:32607/notifications/Details/"+p.id.ToString())));

            var feed = new SyndicationFeed(notif)
            {
                
                Language = "en-US"
            };

            return new FeedResult(new Rss20FeedFormatter(feed));
        }

        public ActionResult NbrNotification()
        {

           
            //
            List<Line> lista = new List<Line>();
            int no=0;
            no = ut.getRepository<Notification>().GetMany(x => x.broadcast == true).OrderByDescending(p => p.creationDate).Count();
            return Json(no, JsonRequestBehavior.AllowGet);

        }


        // GET: notifications
        public async Task<ActionResult> Index()
        {   //user consumption
            string currentUserId = User.Identity.GetUserId();
            User currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
           
            //
            List<Line> lista = new List<Line>();
            IEnumerable<Notification> no = new List<Notification>();
            no = ut.getRepository<Notification>().GetMany(x => x.broadcast == true).OrderByDescending(p=>p.creationDate);
            foreach (var item in db.lines.ToList())
            {
                foreach (var item2 in item.users)
                {//
                    if (item2.Id.Equals(currentUser))
                    {
                        lista.Add(item);
                    }
                }

            }

            List<Notification> notif = new List<Notification>();
            foreach (var item in db.notifications.Include(n => n.line).ToList())
            {
                foreach (var item2 in lista)
                {

                    if ((item.line.Equals(item2)) & (item.broadcast== false)) 
                    {
                        notif.Add(item);

                    }

                }
                
            }
            notif.AddRange(no);
           // var notifications = db.notifications.Include(n => n.line);
            return View(notif);
        }

        // GET: notifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await db.notifications.FindAsync(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // GET: notifications/Create
        public ActionResult Create()
        {
            ViewBag.idLine = new SelectList(db.lines, "id", "name");
            return View();
        }

        // POST: notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,broadcast,creationDate,description,level,idLine")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                notification.creationDate = DateTime.Now;
                db.notifications.Add(notification);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idLine = new SelectList(db.lines, "id", "name", notification.idLine);
            return View(notification);
        }

        // GET: notifications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await db.notifications.FindAsync(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            ViewBag.idLine = new SelectList(db.lines, "id", "name", notification.idLine);
            return View(notification);
        }

        // POST: notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,broadcast,creationDate,description,level,idLine")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notification).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idLine = new SelectList(db.lines, "id", "name", notification.idLine);
            return View(notification);
        }

        // GET: notifications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = await db.notifications.FindAsync(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Notification notification = await db.notifications.FindAsync(id);
            db.notifications.Remove(notification);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
