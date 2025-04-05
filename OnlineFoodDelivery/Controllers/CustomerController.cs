using OnlineFoodDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;



namespace OnlineFoodDelivery.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        MyConnection db = new MyConnection();
        [HttpGet]
        public ActionResult Index([Bind(Include = "RestaurantID")] Restaurant data)
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            if (data.RestaurantID == 0)
            {
                var rslt = db.Customers.Include(e=>e.Restaurant);
                return View(rslt.ToList());
            }
            else
            {
                var rslt = db.Customers.Where(r => r.RestaurantID == data.RestaurantID).Include(e => e.Restaurant);
                return View(rslt.ToList());
            }
        }

        [HttpGet]
        public ActionResult CreateCustomer()
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            return View();
        }

        [HttpPost]
        public ActionResult nCustomer([Bind(Include = "CustomerID,RestaurantID,CustomerFullName,Address,Contact,Email")] Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
        [HttpGet]
        public ActionResult UpdateCustomer(int? id)
        {
            Customer customer = db.Customers.Find(id);
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName", customer.RestaurantID);
            return View(customer);
        }
        [HttpPost]
        public ActionResult fnUpdateCustomer([Bind(Include = "CustomerID,RestaurantID,CustomerFullName,Address,Contact,Email")] Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed([Bind(Include = "CustomerID")] Customer data)
        {
            Customer customer = db.Customers.Find(data.CustomerID);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}