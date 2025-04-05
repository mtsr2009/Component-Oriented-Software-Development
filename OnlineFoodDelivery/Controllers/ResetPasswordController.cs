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
    public class ResetPasswordController : Controller
    {
        // GET: ResetPassword

        MyConnection db = new MyConnection();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Resetpassword(FormCollection data)
        {
            int userId = Convert.ToInt32(Session["userID"]);
            var rslt = db.Users.Find(userId);
            rslt.Password = data["Password"]; 
            db.Entry(rslt).State = EntityState.Modified;
            db.Entry(rslt).Property(p => p.Password).IsModified = true;

            db.Entry(rslt).Property(p => p.RestaurantID).IsModified = false;
            db.Entry(rslt).Property(p => p.StaffFullName).IsModified = false;
            db.Entry(rslt).Property(p => p.Address).IsModified = false;
            db.Entry(rslt).Property(p => p.Contact).IsModified = false;
            db.Entry(rslt).Property(p => p.Email).IsModified = false;
            db.SaveChanges();
            return RedirectToAction("Logout", "Login");
        }
    }
}