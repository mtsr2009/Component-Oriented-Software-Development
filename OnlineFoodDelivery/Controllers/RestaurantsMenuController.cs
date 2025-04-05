using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFoodDelivery.Controllers
{
    public class RestaurantsMenuController : Controller
    {
        // GET: RestaurantsMenu
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateMenu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult fnNewMenu(FormCollection data)
        {
            return RedirectToAction("Index", "RestaurantsMenu");
        }
        [HttpGet]
        public ActionResult UpdateMenu()
        {
            return View();
        }

        [HttpPost]
        public ActionResult fnUpdateMenu(FormCollection data)
        {
            return RedirectToAction("Index", "RestaurantsMenu");
        }
    }
}