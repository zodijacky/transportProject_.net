using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoovTn.Data;
using MoovTn.Domain.Models;

namespace MyTransport.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class subscriptioncardsAdminController : Controller
    {
        private transportdsContext db = new transportdsContext();

        // GET: subscriptioncardsAdmin
        public async Task<ActionResult> Index()
        {
            var subscriptioncards = db.subscriptioncards.Include(s => s.user);
            foreach (var item in subscriptioncards)
            {
                if (item.validityEnd<=DateTime.Now)
                {
                    db.subscriptioncards.Remove(item);
                    await db.SaveChangesAsync();

                }
            }
            return View(await subscriptioncards.ToListAsync());
        }

        // GET: subscriptioncardsAdmin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionCard subscriptioncard = await db.subscriptioncards.FindAsync(id);
            if (subscriptioncard == null)
            {
                return HttpNotFound();
            }
            return View(subscriptioncard);
        }

        // GET: subscriptioncardsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.Users, "id", "email");
            return View();
        }

        // POST: subscriptioncardsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,expired,locked,validityEnd,validityStart,user_id")] SubscriptionCard subscriptioncard)
        {
            if (ModelState.IsValid)
            {
                db.subscriptioncards.Add(subscriptioncard);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.Users, "id", "email", subscriptioncard.user_id);
            return View(subscriptioncard);
        }

        // GET: subscriptioncardsAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionCard subscriptioncard = await db.subscriptioncards.FindAsync(id);
            if (subscriptioncard == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.Users, "id", "email", subscriptioncard.user_id);
            return View(subscriptioncard);
        }

        // POST: subscriptioncardsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,expired,locked,validityEnd,validityStart,user_id")] SubscriptionCard subscriptioncard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscriptioncard).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.Users, "id", "email", subscriptioncard.user_id);
            return View(subscriptioncard);
        }

        // GET: subscriptioncardsAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionCard subscriptioncard = await db.subscriptioncards.FindAsync(id);
            if (subscriptioncard == null)
            {
                return HttpNotFound();
            }
            return View(subscriptioncard);
        }

        // POST: subscriptioncardsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SubscriptionCard subscriptioncard = await db.subscriptioncards.FindAsync(id);
            db.subscriptioncards.Remove(subscriptioncard);
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
