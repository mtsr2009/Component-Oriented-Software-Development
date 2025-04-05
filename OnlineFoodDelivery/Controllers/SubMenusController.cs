using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineFoodDelivery.Models;

namespace OnlineFoodDelivery.Controllers
{
    public class SubMenusController : Controller
    {
        private MyConnection db = new MyConnection();

        // GET: SubMenus

        [HttpGet]
        public ActionResult Index([Bind(Include = "RestaurantID")] Restaurant data)
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            if (data.RestaurantID == 0)
            {
                var subMenus = db.SubMenus.Include(s => s.MainMenu).Include(s => s.MainMenu.Restaurant);
                return View(subMenus.ToList());
            }
            else
            {
                var rslt = db.SubMenus.Where(r => r.MainMenu.RestaurantID == data.RestaurantID).Include(s => s.MainMenu).Include(s => s.MainMenu.Restaurant);
                return View(rslt.ToList());
            }
        }



        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MainMenuID = new SelectList(db.MainMenus, "MainMenuID", "MainMenuName");
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            return View();
        }

        [HttpPost]
      
        public ActionResult Create([Bind(Include = "mySubMenuID, RestaurantID,MainMenuID,SubMenu1,Size,Quantity,Price")] SubMenu subMenu)
        {
          
          db.SubMenus.Add(subMenu);
          db.SaveChanges();
          return RedirectToAction("Index", "SubMenus");
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenu subMenu = db.SubMenus.Find(id);
            var rstId = db.MainMenus.Find(subMenu.MainMenuID); 
            if (subMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.MainMenuID = new SelectList(db.MainMenus, "MainMenuID", "MainMenuName", subMenu.MainMenuID);
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName", rstId.RestaurantID);
            return View(subMenu);
        }

 
        [HttpPost]
        public ActionResult Edit([Bind(Include = "mySubMenuID, RestaurantID,MainMenuID,SubMenu1,Size,Quantity,Price")] SubMenu subMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "SubMenus");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubMenu subMenu = db.SubMenus.Find(id);
            if (subMenu == null)
            {
                return HttpNotFound();
            }
            return View(subMenu);
        }

       
        [HttpPost]
        public ActionResult DeleteConfirmed([Bind(Include = "mySubMenuID")] SubMenu data)
        {
            SubMenu subMenu = db.SubMenus.Find(data.mySubMenuID);
            db.SubMenus.Remove(subMenu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    
    }
}
