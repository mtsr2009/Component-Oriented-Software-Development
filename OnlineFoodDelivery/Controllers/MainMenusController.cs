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
    public class MainMenusController : Controller
    {
        private MyConnection db = new MyConnection();

        // GET: MainMenus
        [HttpGet]
        public ActionResult Index([Bind(Include = "RestaurantID")] Restaurant data)
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            if (data.RestaurantID == 0)
            {
                var rslt = db.MainMenus.Include(c => c.Restaurant);
                return View(rslt.ToList());
            }
            else
            {
                var rslt = db.MainMenus.Where(r => r.RestaurantID == data.RestaurantID).Include(c => c.Restaurant);
                return View(rslt.ToList());
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "MainMenuID,RestaurantID,MainMenuName")] MainMenu mainMenu)
        {
            if (ModelState.IsValid)
            {
                db.MainMenus.Add(mainMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mainMenu);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainMenu mainMenu = db.MainMenus.Find(id);
            if (mainMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName", mainMenu.RestaurantID);
            return View(mainMenu);
        }
        [HttpPost]
          public ActionResult Edit([Bind(Include = "MainMenuID,RestaurantID,MainMenuName")] MainMenu mainMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mainMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mainMenu);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainMenu mainMenu = db.MainMenus.Find(id);
            if (mainMenu == null)
            {
                return HttpNotFound();
            }
            return View(mainMenu);
        }

        // POST: MainMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MainMenu mainMenu = db.MainMenus.Find(id);
            db.MainMenus.Remove(mainMenu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

   
    }
}
