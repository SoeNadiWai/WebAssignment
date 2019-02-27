using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MYCHARITY.Controllers
{
    public class CharitablePlacesController : Controller
    {
        // GET: CharitablePlaces
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            List<CharitablePlace> events = new List<CharitablePlace>();
            using (CharityEntities dc = new CharityEntities())
            {
                events = dc.CharitablePlaces.ToList();
            }
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult GetAllPlaces()
        {
            List<CharitablePlace> allUser = new List<CharitablePlace>();

            // Here "MyDatabaseEntities " is dbContext, which is created at time of model creation.

            using (CharityEntities dc = new CharityEntities())
            {
                allUser = dc.CharitablePlaces.ToList();
            }

            return new JsonResult { Data = allUser, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetUserWithParameter(string prefix)
        {
            List<CharitablePlace> allUser = new List<CharitablePlace>();


            // Here "MyDatabaseEntities " is dbContext, which is created at time of model creation.

            using (CharityEntities dc = new CharityEntities())
            {
                allUser = dc.CharitablePlaces.Where(a => a.Addresss.Contains(prefix)).ToList();
            }

            return new JsonResult { Data = allUser, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}