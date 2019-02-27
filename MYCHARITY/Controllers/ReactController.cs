using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MYCHARITY.Controllers
{
    public class ReactController : Controller
    {
        // GET: React
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetEmployeeData()
        {
            using (CharityEntities dc = new CharityEntities())
            {
                var data = dc.AspNetUsers.OrderBy(a => a.UserName).ToList();
                return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}