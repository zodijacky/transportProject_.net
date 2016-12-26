using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoovTn.Domain.Models;
using MoovTn.Data;

namespace MoovTn.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class claimsAdminController : Controller
    {

        private transportdsContext db = new transportdsContext();






        // GET: claimsAdmin
        public async Task<ActionResult> Index()
        {
            var claims = db.claims.Include(c => c.user);
            return View(await claims.ToListAsync());
        }

        // GET: claimsAdmin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = await db.claims.FindAsync(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // GET: claimsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.users_id = new SelectList(db.Users, "id", "email");
            return View();
        }

        // POST: claimsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,claimDate,question,questionRead,response,responseRead,users_id")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                db.claims.Add(claim);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.users_id = new SelectList(db.Users, "id", "email", claim.users_id);
            return View(claim);
        }

        // GET: claimsAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = await db.claims.FindAsync(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            ViewBag.users_id = new SelectList(db.Users, "id", "email", claim.users_id);
            return View(claim);
        }

        // POST: claimsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,claimDate,question,questionRead,response,responseRead,users_id")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                claim.questionRead = true;
                claim.responseRead = false;
                
                db.Entry(claim).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.users_id = new SelectList(db.Users, "id", "email", claim.users_id);
            return View(claim);
        }

        // GET: claimsAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = await db.claims.FindAsync(id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // POST: claimsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Claim claim = await db.claims.FindAsync(id);
            db.claims.Remove(claim);
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
