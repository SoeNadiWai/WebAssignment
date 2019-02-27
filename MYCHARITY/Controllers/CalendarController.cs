using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MYCHARITY.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            using (CharityEntities dc = new CharityEntities())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        [HttpPost]
        [Authorize(Roles = "Organization")]
        public JsonResult SaveEvent(Event e)
        {

            var status = false;
            using (CharityEntities dc = new CharityEntities())
            {

                if (e.EventID > 0)
                {
                    //update event
                    var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Startdate = Convert.ToDateTime(e.Startdate);
                        v.Enddate = Convert.ToDateTime(e.Enddate);
                        v.Startdate = e.Startdate;
                        v.Enddate = e.Enddate;
                        v.Description = e.Description;
                        v.Isfullday = e.Isfullday;
                        v.Themecolor = e.Themecolor;
                        v.RequestedOrgName = e.RequestedOrgName;
                    }
                }
                else
                {
                    JsonResult b = GetEvents();
                    int d = ((ICollection)b.Data).Count;
                //dynamically insert ID
                    e.EventID = d + 1;
                    
                    dc.Events.Add(e);
                }
                dc.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        [Authorize(Roles = "Organization")]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (CharityEntities dc = new CharityEntities())
            {
                var v = dc.Events.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
                else
                {

                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}