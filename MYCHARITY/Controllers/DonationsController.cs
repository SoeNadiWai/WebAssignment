using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MYCHARITY;
using System.Collections;
using Microsoft.AspNet.Identity;

namespace MYCHARITY.Controllers
{
    public class DonationsController : Controller
    {
        private CharityEntities db = new CharityEntities();

        // GET: Donations
        public ActionResult Index()
        {
            var donations = db.Donations.Include(d => d.AspNetUser);
            return View(donations.ToList());
        }
        public JsonResult GetDonations()
        {
            using (CharityEntities dc = new CharityEntities())
            {
                var donations = dc.Donations.ToList();
                return new JsonResult { Data = donations, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }
        public JsonResult GetSpepcificDonations()
        {

            // Donation donation = db.Donations.Find(id);
            using (CharityEntities dc = new CharityEntities())
            {
                var donations = dc.Donations.ToList();
                return new JsonResult { Data = donations, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        // GET: Donations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // GET: Donations/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,OrgName,DonateAmount")] Donation donation)
        {
            
            if (ModelState.IsValid)
            {

                using (CharityEntities dc = new CharityEntities())
                {
                    donation.UserID = User.Identity.GetUserId();
                    donation.DonateDate = DateTime.Today;
                    JsonResult b = GetDonations();
                    int d = ((ICollection)b.Data).Count;
                    donation.DonationID = d + 1;

                }
                db.Donations.Add(donation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donation);
        }

        // GET: Donations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", donation.UserID);
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonationID,UserID,UserName,OrgName,DonateAmount,DonateDate")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", donation.UserID);
            return View(donation);
        }

        // GET: Donations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donation donation = db.Donations.Find(id);
            db.Donations.Remove(donation);
            db.SaveChanges();
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
