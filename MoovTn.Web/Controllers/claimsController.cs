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
using Newtonsoft.Json;
using MoovTn.Data;
using Microsoft.AspNet.Identity;
using MyTransport.Web.Models;

namespace MoovTn.Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class claimsController : Controller
    {

        private transportdsContext db = new transportdsContext();

        // GET: claims
        public async Task<ActionResult> Index()
        {   //User Id consumption
           string currentUserId = User.Identity.GetUserId();
//User currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            var claims = db.claims.Include(c => c.user).Where(c=>c.users_id== currentUserId);
            return View(await claims.ToListAsync());
        }

        // GET: claims/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim claim = await db.claims.FindAsync(id);
            claim.questionRead = true;
             if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // GET: claims/Create
        public ActionResult Create()
        {
            ViewBag.users_id = new SelectList(db.Users, "id", "email");
            return View();
        }

        // POST: claims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,claimDate,question,questionRead,response,responseRead,users_id")] Claim claim)
        {
            if (ModelState.IsValid)
            {




                var response = Request["g-recaptcha-response"];
                //secret that was generated in key value pair
                const string secret = "6LcMtxATAAAAAJvIdvzr56jx2J9bBVREmOJDF07X";

                var client = new WebClient();
                var reply =
                    client.DownloadString(
                        string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

                var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

                //when response is false check for the error message
                if (!captchaResponse.Success)
                {
                    if (captchaResponse.ErrorCodes.Count <= 0) return View();

                    var error = captchaResponse.ErrorCodes[0].ToLower();
                    switch (error)
                    {
                        case ("missing-input-secret"):
                            ViewBag.Message = "The secret parameter is missing.";
                            break;
                        case ("invalid-input-secret"):
                            ViewBag.Message = "The secret parameter is invalid or malformed.";
                            break;

                        case ("missing-input-response"):
                            ViewBag.Message = "Captcha .... ";
                            break;
                        case ("invalid-input-response"):
                            ViewBag.Message = "The response parameter is invalid or malformed.";
                            break;

                        default:
                            ViewBag.Message = "Error occured. Please try again";
                            break;
                    }
                }
                else
                {
                    ViewBag.Message = "Valid";

                    //user id consumption
                    claim.users_id = User.Identity.GetUserId();
                    claim.claimDate = DateTime.Now;
                    claim.questionRead = false;
                    db.claims.Add(claim);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }

            ViewBag.users_id = new SelectList(db.Users, "id", "email", claim.users_id);
            return View(claim);
        }

        // GET: claims/Edit/5
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

        // POST: claims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,claimDate,question,questionRead,response,responseRead,users_id")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(claim).State = EntityState.Modified;
                //id user consumption
                                claim.users_id = User.Identity.GetUserId();
                claim.claimDate = DateTime.Now;
               
                claim.questionRead = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.users_id = new SelectList(db.Users, "id", "email", claim.users_id);
            return View(claim);
        }

        // GET: claims/Delete/5
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

        // POST: claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
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
