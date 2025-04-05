using OnlineFoodDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineFoodDelivery.Controllers
{
    public class LoginController : Controller
    {
        private MyConnection db = new MyConnection();
        // GET: Login
        [HttpGet]
        public ActionResult LoginIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LoginMe(User data)
        {
            var user = db.Users.Where(u => u.Email == data.Email && u.Password == data.Password).FirstOrDefault();
            if (user != null)
            {
                Session["userID"] = user.StaffID;
                Session["IsUserLoggedIn"] = 1;
                Session["enableLogout"] = 1;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["IsUserLoggedIn"] = 0;
                Session["enableLogout"] = 0;
                return RedirectToAction("LoginIndex", "Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("LoginIndex", "Login");

        }
    }
}