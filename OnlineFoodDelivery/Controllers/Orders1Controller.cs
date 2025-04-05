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
    public class Orders1Controller : Controller
    {
        private MyConnection db = new MyConnection();

        [HttpGet]
        public ActionResult Index([Bind(Include = "RestaurantID")] Restaurant data)
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            if (data.RestaurantID == 0)
            {
                var rslt = db.vOrders;
                return View(rslt.ToList());
            }
            else
            {
                var rslt = db.vOrders.Where(r => r.RestaurantID == data.RestaurantID);
                return View(rslt.ToList());
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerFullName");
            ViewBag.mySubMenuID = new SelectList(db.SubMenus, "mySubMenuID", "SubMenu1");
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            return View();
        }

    
        [HttpPost]
      
        public ActionResult Create([Bind(Include = "OrderID,RestaurantID,CustomerID,MainMenuID,mySubMenuID,Date,Time,Quantity,SentToKitchenDate,SentToKitchenTime,CancellationDate,CancellationTime,CancelaltionReason,DeliveryDate,DeliveryTime,OrderStatus")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderStatus = 1; 
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index", "Orders1");
            }
            return View(order);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "CustomerFullName", order.CustomerID);
            ViewBag.mySubMenuID = new SelectList(db.SubMenus, "mySubMenuID", "SubMenu1", order.mySubMenuID);
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName", order.RestaurantID);
            return View(order);
        }


        [HttpPost]
       
        public ActionResult Edit([Bind(Include = "OrderID,RestaurantID,CustomerID,MainMenuID,mySubMenuID,Date,Time,Quantity,SentToKitchenDate,SentToKitchenTime,CancellationDate,CancellationTime,CancelaltionReason,DeliveryDate,DeliveryTime,OrderStatus")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderStatus = 1;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Orders1");
            }

            return View(order);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

    
        [HttpPost]
        public ActionResult DeleteConfirmed([Bind(Include = "OrderID")] Order data)
        {
            Order order = db.Orders.Find(data.OrderID);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        }
}
