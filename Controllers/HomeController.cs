using ClosedXML.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        DBuserSignupLoginEntities2 db = new DBuserSignupLoginEntities2();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult LoginInfo()
        {
            return View();
        }
       

        [HttpPost]
        public ActionResult Signup(TBLUserInfo tBLUserInfo)
        {
            if (db.TBLUserInfoes.Any(x => x.UserNameUs == tBLUserInfo.UserNameUs))
            {
                ViewBag.Notification = "This account already exists";
                return View();
            }
            else if (String.IsNullOrEmpty(tBLUserInfo.UserNameUs) || String.IsNullOrEmpty(tBLUserInfo.PasswordUs)|| String.IsNullOrEmpty(tBLUserInfo.RePasswordUs))
            {
                ViewBag.Notification = "Username and password is required";
                return View();
            }
            else if(tBLUserInfo.PasswordUs!=tBLUserInfo.RePasswordUs)
            {
                //ViewBag.Notification = "Password and Repassword does n't match";
                return View();
            }
            else if(tBLUserInfo.PasswordUs.ToString().Length<8 || tBLUserInfo.PasswordUs.ToString().Length > 15)
            {
                //ViewBag.Notification = "Password should be minimum 8 characters and less than 15 characters";
                return View();
            }
            else
            {
                db.TBLUserInfoes.Add(tBLUserInfo);
                db.SaveChanges();
                ViewBag.Notification = "The account has been successfully registered!";
                Session["IdUsSS"] = tBLUserInfo.IdUs.ToString();
                Session["UserNameSS"] = tBLUserInfo.UserNameUs.ToString();
                return RedirectToAction("LoginInfo", "Home");
            }

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TBLUserInfo tBLUserInfo)
        {
            var checkLogin = db.TBLUserInfoes.Where(x => x.UserNameUs.Equals(tBLUserInfo.UserNameUs) && x.PasswordUs.Equals(tBLUserInfo.PasswordUs)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["IdUsSS"] = tBLUserInfo.IdUs.ToString();
                Session["UserNameSS"] = tBLUserInfo.UserNameUs.ToString();
                return RedirectToAction("LoginInfo", "Home");
            }
            else if(String.IsNullOrEmpty(tBLUserInfo.UserNameUs)&& String.IsNullOrEmpty(tBLUserInfo.PasswordUs))
            {
                
            }
            else
            {
                ViewBag.Notification = "Wrong Username or Password";
            }
            return View();
        }
    }
}
