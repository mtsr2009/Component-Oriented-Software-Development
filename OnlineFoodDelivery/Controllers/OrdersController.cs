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
    public class OrdersController : Controller
    {
        private MyConnection db = new MyConnection();
        // GET: Orders
        [HttpGet]
        public ActionResult PendingOrder([Bind(Include = "RestaurantID")] Restaurant data)
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            if (data.RestaurantID == 0)
            {
                var rslt = db.vOrders.Where(s=>s.OrderStatus==1);
                return View(rslt.ToList());
            }
            else
            {
                var rslt = db.vOrders.Where(r => r.RestaurantID == data.RestaurantID && r.OrderStatus==1);
                return View(rslt.ToList());
            }
        }
        [HttpGet]
        public ActionResult OngoingOrder([Bind(Include = "RestaurantID")] Restaurant data)
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            if (data.RestaurantID == 0)
            {
                var rslt = db.vOrders.Where(s => s.OrderStatus == 2);
                return View(rslt.ToList());
            }
            else
            {
                var rslt = db.vOrders.Where(r => r.RestaurantID == data.RestaurantID && r.OrderStatus == 2);
                return View(rslt.ToList());
            }
        }
        [HttpGet]
        public ActionResult DeliveredOrder([Bind(Include = "RestaurantID")] Restaurant data)
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            if (data.RestaurantID == 0)
            {
                var rslt = db.vOrders.Where(s => s.OrderStatus == 3);
                return View(rslt.ToList());
            }
            else
            {
                var rslt = db.vOrders.Where(r => r.RestaurantID == data.RestaurantID && r.OrderStatus == 3);
                return View(rslt.ToList());
            }
        }
        [HttpGet]
        public ActionResult CancelledOrder([Bind(Include = "RestaurantID")] Restaurant data)
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            if (data.RestaurantID == 0)
            {
                var rslt = db.vOrders.Where(s => s.OrderStatus == 4);
                return View(rslt.ToList());
            }
            else
            {
                var rslt = db.vOrders.Where(r => r.RestaurantID == data.RestaurantID && r.OrderStatus == 4);
                return View(rslt.ToList());
            }
        }


        [HttpGet]
        public ActionResult DeliveranOrder(int? id)
        {
            var rslt = db.vOrders.Where(e => e.OrderID == id).FirstOrDefault();
            ViewBag.custmer = rslt.CustomerFullName;
            ViewBag.order = rslt.SubMenu1 + " " + rslt.Size;
            ViewBag.ordrId = id;
            return View();
        }
        [HttpPost]
        public ActionResult fnDeliverOrder(Order data)
        {
            data.OrderStatus = 3;
            db.Entry(data).State = EntityState.Modified;
            db.Entry(data).Property(p => p.CancellationDate).IsModified = true;
            db.Entry(data).Property(p => p.CancellationTime).IsModified = true;
            db.Entry(data).Property(p => p.CancelaltionReason).IsModified = true;
            db.Entry(data).Property(p => p.OrderStatus).IsModified = true;

            db.Entry(data).Property(p => p.SentToKitchenDate).IsModified = false;
            db.Entry(data).Property(p => p.SentToKitchenTime).IsModified = false;
            db.Entry(data).Property(p => p.RestaurantID).IsModified = false;
            db.Entry(data).Property(p => p.CustomerID).IsModified = false;
            db.Entry(data).Property(p => p.MainMenuID).IsModified = false;
            db.Entry(data).Property(p => p.mySubMenuID).IsModified = false;
            db.Entry(data).Property(p => p.Date).IsModified = false;
            db.Entry(data).Property(p => p.Time).IsModified = false;
            db.Entry(data).Property(p => p.Quantity).IsModified = false;
            db.SaveChanges();
            return RedirectToAction("Index", "Orders1");
        }

        [HttpGet]
        public ActionResult PrepareOrder(int? id)
        {
            var rslt = db.vOrders.Where(e=>e.OrderID== id).FirstOrDefault();
            ViewBag.custmer = rslt.CustomerFullName; 
            ViewBag.order = rslt.SubMenu1 + " " + rslt.Size; 
            ViewBag.ordrId = id; 
            return View();
        }
        [HttpPost]
        public ActionResult fnPrepareOrder(Order data)
        {
            data.OrderStatus = 2; 
            db.Entry(data).State = EntityState.Modified;
            db.Entry(data).Property(p => p.SentToKitchenDate).IsModified = true;
            db.Entry(data).Property(p => p.SentToKitchenTime).IsModified = true;
            db.Entry(data).Property(p => p.OrderStatus).IsModified = true;

            db.Entry(data).Property(p => p.RestaurantID).IsModified = false;
            db.Entry(data).Property(p => p.CustomerID).IsModified = false;
            db.Entry(data).Property(p => p.MainMenuID).IsModified = false;
            db.Entry(data).Property(p => p.mySubMenuID).IsModified = false;
            db.Entry(data).Property(p => p.Date).IsModified = false;
            db.Entry(data).Property(p => p.Time).IsModified = false;
            db.Entry(data).Property(p => p.Quantity).IsModified = false;
            db.SaveChanges();
            return RedirectToAction("Index", "Orders1");
        }
        [HttpGet]
        public ActionResult CancelOrder(int? id)
        {
            var rslt = db.vOrders.Where(e => e.OrderID == id).FirstOrDefault();
            ViewBag.custmer = rslt.CustomerFullName;
            ViewBag.order = rslt.SubMenu1 + " " + rslt.Size;
            ViewBag.ordrId = id;
            return View();
        }
        [HttpPost]
        public ActionResult fnCancellOrder(Order data)
        {
            data.OrderStatus = 4;
            db.Entry(data).State = EntityState.Modified;
            db.Entry(data).Property(p => p.CancellationDate).IsModified = true;
            db.Entry(data).Property(p => p.CancellationTime).IsModified = true;
            db.Entry(data).Property(p => p.CancelaltionReason).IsModified = true;
            db.Entry(data).Property(p => p.OrderStatus).IsModified = true;

            db.Entry(data).Property(p => p.SentToKitchenDate).IsModified = false;
            db.Entry(data).Property(p => p.SentToKitchenTime).IsModified = false;
            db.Entry(data).Property(p => p.RestaurantID).IsModified = false;
            db.Entry(data).Property(p => p.CustomerID).IsModified = false;
            db.Entry(data).Property(p => p.MainMenuID).IsModified = false;
            db.Entry(data).Property(p => p.mySubMenuID).IsModified = false;
            db.Entry(data).Property(p => p.Date).IsModified = false;
            db.Entry(data).Property(p => p.Time).IsModified = false;
            db.Entry(data).Property(p => p.Quantity).IsModified = false;
            db.SaveChanges();
            return RedirectToAction("Index", "Orders1");
        }
    }
}