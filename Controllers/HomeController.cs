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
        DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Signupe()
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
        public ActionResult LoginInfo1()
        {
            return View();
        }
        public ActionResult LoginInfo2()
        {
            return View();
        }
        public ActionResult emphome()
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
                //Session["IdUsSS"] = tBLUserInfo.IdUs.ToString();
                //Session["UserNameSS"] = tBLUserInfo.UserNameUs.ToString();
                return RedirectToAction("LoginInfo", "Home");
            }

        }
        [HttpPost]
        public ActionResult Signupe(Employee_Login employee_Login,Employee employee)
        {
            if (db.Employee_Login.Any(x => x.id == employee_Login.id))
            {
                ViewBag.Notification = "This account already exists";
                return View();
            }
            else if (String.IsNullOrEmpty(employee_Login.id) || String.IsNullOrEmpty(employee_Login.password) || String.IsNullOrEmpty(employee_Login.repassword)|| String.IsNullOrEmpty(employee_Login.role))
            {
                ViewBag.Notification = "Id, role and password are required";
                return View();
            }
            else if (employee_Login.password != employee_Login.repassword)
            {
                //ViewBag.Notification = "Password and Repassword does n't match";
                return View();
            }
            else if (employee_Login.password.ToString().Length < 8 || employee_Login.password.ToString().Length > 15)
            {
                //ViewBag.Notification = "Password should be minimum 8 characters and less than 15 characters";
                return View();
            }
            else if(db.Employees.All(x => x.id.ToString() != employee_Login.id))
            {
                ViewBag.Notification = "Registration with employee id "+ employee_Login.id+" is not allowed. Please contact admin!";
                return View();
            }
            else
            {
                db.Employee_Login.Add(employee_Login);
                db.SaveChanges();
                ViewBag.Notification = "The account has been successfully registered!Please login to continue";
                Session["IdUsSS1"] = employee_Login.id.ToString();
                return View() ;
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
        public ActionResult Logine()
        {
            return View();
        }
        public ActionResult Passupd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Passupd(TBLUserInfo tBLUserInfo)
        {
            using (DBuserSignupLoginEntities3 db=new DBuserSignupLoginEntities3())
            {
                var detail = db.TBLUserInfoes.Where(x => x.PasswordUs == tBLUserInfo.PasswordUs).FirstOrDefault();
                if (detail != null)
                {
                    var userdetail = db.TBLUserInfoes.FirstOrDefault(x => x.PasswordUs == tBLUserInfo.PasswordUs);
                    var userdetail1 = db.TBLUserInfoes.FirstOrDefault(x => x.UserNameUs == tBLUserInfo.UserNameUs);
                    if (userdetail != null&& userdetail1 != null)
                    {
                        userdetail.PasswordUs = tBLUserInfo.NewPasswordUs;
                        userdetail.RePasswordUs = tBLUserInfo.NewPasswordUs;
                        db.SaveChanges();
                        ViewBag.Message = "Password changed successfully";
                    }
                    else
                    {
                        ViewBag.Message = "Password not updated";
                    }
                }
            }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logine(Employee_Login employee_Login)
        {
            var checkLogin = db.Employee_Login.Where(x => x.id.Equals(employee_Login.id) && x.password.Equals(employee_Login.password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["IdUsSS1"] = employee_Login.id.ToString();
                return RedirectToAction("emphome", "Home");
            }
            else if (String.IsNullOrEmpty(employee_Login.id) || String.IsNullOrEmpty(employee_Login.password))
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
