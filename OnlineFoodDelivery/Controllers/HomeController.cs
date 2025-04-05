using OnlineFoodDelivery.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineFoodDelivery.Controllers
{
    public class HomeController : Controller
    {
        MyConnection db = new MyConnection();
        [HttpGet]
        public ActionResult Index([Bind(Include = "RestaurantID")] Restaurant data)
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            if (data.RestaurantID == 0)
            {
                var rslt = db.Restaurants;
                return View(rslt.ToList());
            }
            else
            {
                var rslt = db.Restaurants.Where(r=>r.RestaurantID==data.RestaurantID);
                return View(rslt.ToList());
            }
        }
        [HttpGet]
        public ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult submittCreate([Bind(Include = "RestaurantID,RestuarantName,Country,State,Address,Contact,EmailAddress")] Restaurant restaurant)
        {
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public ActionResult Update(int? id)
        {
            var rslt = db.Restaurants.Find(id);
            return View(rslt);
        }

        [HttpPost]
        public ActionResult submittUpdate([Bind(Include = "RestaurantID,RestuarantName,Country,State,Address,Contact,EmailAddress")] Restaurant restaurant)
        {
            db.Entry(restaurant).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed([Bind(Include = "RestaurantID")] Restaurant restaurant)
        {
            Restaurant rslt = db.Restaurants.Find(restaurant.RestaurantID);
            db.Restaurants.Remove(rslt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}