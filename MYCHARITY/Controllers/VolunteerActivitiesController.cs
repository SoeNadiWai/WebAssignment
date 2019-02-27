using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MYCHARITY.Controllers
{
    public class VolunteerActivitiesController : Controller
    {
        // GET: VolunteerActivities
        public ActionResult Index()
        {
            return View();
        }


        private CharityEntities db = new CharityEntities();

        // GET: Donations

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
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,OrgName,DonateAmount")] Donation donation)
        {

            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
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
            else
            {
                ViewBag.ErrorMessage = "You need to Login first!";
                return RedirectToAction("Login", "AccountController");
            }
        }

    }
}