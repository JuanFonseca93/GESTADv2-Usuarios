using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GESTADv2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index( int? num)
        {
            string path = Path.Combine(Server.MapPath("~/Gestad"));
            Directory.CreateDirectory(path);
            
            int log = (num ?? 0);

            if (log > 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}