using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;

using System.Collections.Generic;
using MyTransport.Web.Models;
using MoovTn.Data.Infrastructure;
using MoovTn.Data;
using MoovTn.Domain.Models;
using Microsoft.AspNet.Identity;
using MoovTn.Web.Models;

namespace MoovTn.Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class subscriptioncardsController : Controller
    {
        private static IDatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);
        private transportdsContext db = new transportdsContext();

        // GET: subscriptioncards
        public async Task<ActionResult> Index()
        {
            var subscriptioncards = db.subscriptioncards.Include(s => s.lines);
            return View(await subscriptioncards.ToListAsync());
        }




        public ActionResult DownloadPDF( )
        {
           
                SubscriptionCard subs = db.subscriptioncards.ToList().Last();

                //////////////////////////////////////User Consumption//////////////////////////////////
                string currentUserId = User.Identity.GetUserId();
                User user = db.Users.FirstOrDefault(x => x.Id == currentUserId);
               


                var model = new GeneratePDFModel();

                model.DateCreation = DateTime.Today;
                model.Expire = DateTime.Today.AddYears(1);
              
                model.Name = user.firstName;
                model.Surname = user.lastName;
               model.Lines=db.subscriptioncards.Find(subs.id).lines;

                var logoFile = @"/Uploads/"+subs.id +".png";
                model.img = Server.MapPath(logoFile);
                //Use ViewAsPdf Class to generate pdf using GeneratePDF.cshtml view
                return new Rotativa.ViewAsPdf("test", model) { FileName = "firstPdf.pdf"};

        }






        // GET: subscriptioncards/Details/5
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

        // GET: subscriptioncards/Create
        public ActionResult Create()
        {
            SelectList maliste = new SelectList(db.lines, "name", "name");
            ViewBag.maliste = maliste;
            ViewBag.user_id = new SelectList(db.Users, "id", "email");
            return View();
        }

        // POST: subscriptioncards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create([Bind(Include = "id,expired,locked,validityEnd,validityStart,user_id")] SubscriptionCard subscriptioncard)
        {

            if (ModelState.IsValid)
            {
                string chaine =  Convert.ToString( Request.Form["maliste"].ToString().Trim());
                string currentUserId = User.Identity.GetUserId();
                User user = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                Line l=ut.getRepository<Line>().Get(x => x.name == chaine);
               // line l = db.lines.Where(p => p.name.Equals(Request.Form["maliste"].ToString().Trim())).ToList().Single<line>();
               // Console.WriteLine(Request.Form["maliste"]);
                subscriptioncard.lines.Add(l);
                subscriptioncard.validityStart = DateTime.Today;
                subscriptioncard.validityEnd = DateTime.Today.AddYears(1);
                subscriptioncard.user_id = currentUserId;
                ut.getRepository<SubscriptionCard>().Add(subscriptioncard);
                ut.Commit();
      
                          
                int id;
                id = db.subscriptioncards.ToList().Last().id;
                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=" + id + "%20Thank you for using our Platform", Server.MapPath("~/Uploads/") +subscriptioncard.id+".png");
                
                
                return RedirectToAction("PaymentWithPaypal","Paypal", new { id = db.subscriptioncards.ToList().Last().id});
            }
            SelectList maliste = new SelectList(db.lines,"id","name");
            ViewBag.maliste = maliste;
            ViewBag.user_id = new SelectList(db.Users, "id", "email", subscriptioncard.user_id);
            return View(subscriptioncard);
        }

        // GET: subscriptioncards/Edit/5
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

        // POST: subscriptioncards/Edit/5
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

        // GET: subscriptioncards/Delete/5
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

        // POST: subscriptioncards/Delete/5
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
