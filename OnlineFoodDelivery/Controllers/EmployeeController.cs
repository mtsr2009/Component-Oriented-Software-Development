using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using OnlineFoodDelivery.Models;

namespace OnlineFoodDelivery.Controllers
{
    public class EmployeeController : Controller
    {
        private MyConnection db = new MyConnection();
        // GET: Employee

        [HttpGet]
        public ActionResult Index([Bind(Include = "RestaurantID")] Restaurant data)
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            if(data.RestaurantID==0)
            {
              var users = db.Users.Include(u => u.Restaurant);
                return View(users.ToList());
            }
            else
            {
                var users = db.Users.Where(e=>e.RestaurantID==data.RestaurantID).Include(u => u.Restaurant);
                return View(users.ToList());
            }

            
        }

        [HttpGet]
        public ActionResult CreateEmployee()
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            return View();
        }

        [HttpPost]
        public ActionResult nEmployee([Bind(Include = "StaffID,RestaurantID,StaffFullName,Address,Contact,Email,Password")] User user)
        {
            var rslt = user.StaffFullName;
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index", "Employee");
        }
        [HttpGet]
        public ActionResult UpdateEmployee(int? usrId)
        {
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName");
            var rslt = db.Users.Find(usrId);
            ViewBag.RestaurantID = new SelectList(db.Restaurants, "RestaurantID", "RestuarantName", rslt.RestaurantID);
            return View(rslt);
        }
        [HttpPost]
        public ActionResult fnUpdateEmployee([Bind(Include = "StaffID,RestaurantID,StaffFullName,Address,Contact,Email,Password")] User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Employee");
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

       
        [HttpPost]
        public ActionResult DeleteConfirmed([Bind(Include = "StaffID")] User user)
        {
            User rwlt = db.Users.Find(user.StaffID);
            db.Users.Remove(rwlt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}