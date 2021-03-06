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
using Scrypt;
using System.Data.Entity;

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
        public ActionResult Signup(Admin admin)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            if (db.Admins.Any(x => x.UserNameUs == admin.UserNameUs))
            {
                ViewBag.Notification = "This account already exists";
                return View();
            }
            else if (String.IsNullOrEmpty(admin.UserNameUs) || String.IsNullOrEmpty(admin.PasswordUs)|| String.IsNullOrEmpty(admin.RePasswordUs))
            {
                ViewBag.Notification = "Username and password is required";
                return View();
            }
            else if(admin.PasswordUs!=admin.RePasswordUs)
            {
                //ViewBag.Notification = "Password and Repassword does n't match";
                return View();
            }
            else if(admin.PasswordUs.ToString().Length<8 || admin.PasswordUs.ToString().Length > 15)
            {
                //ViewBag.Notification = "Password should be minimum 8 characters and less than 15 characters";
                return View();
            }
            else
            {

                //admin.PasswordUs = encoder.Encode(LoginVM.Password);
                db.Admins.Add(admin);
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
            ViewBag.msg1 = employee_Login.password;
            ViewBag.msg2 = employee_Login.repassword;
            ScryptEncoder encoder = new ScryptEncoder();
            
            if (String.IsNullOrEmpty(employee_Login.id)|| String.IsNullOrEmpty(employee_Login.password)|| String.IsNullOrEmpty(employee_Login.repassword))
            {
                
                return View();
            }
            else if (employee_Login.password != employee_Login.repassword)
            {
                //ViewBag.Notification = "Password and Repassword does n't match";
                return View();
            }
            else if (employee_Login.password.ToString().Length < 6 )
            {
                //ViewBag.Notification = "Password should be minimum 8 characters and less than 15 characters";
                return View();
            }
            else if (db.Employee_Login.Any(x => x.id == employee_Login.id))
            {
                ViewBag.Notification = "This employee with id " + employee_Login.id + " already registered!";
                return View();
            }
            else if(db.Employees.All(x => x.id.ToString() != employee_Login.id))
            {
                ViewBag.Notification = "Registration with employee id "+ employee_Login.id+" is not allowed. Please contact admin!";
                return View();
            }
            else
            {
                String k1 = employee_Login.password;
                String k2 = encoder.Encode(k1);
                db.Employee_Login.Add(new Employee_Login()
                {
                    id=employee_Login.id,
                    password= k2,
                    repassword=k2
                });
                db.SaveChanges();
                //ViewBag.Notification1 = "The account has been successfully registered!Please login to continue";
                //Session["IdUsSS1"] = employee_Login.id.ToString();
                TempData["message"] = "Employee with id " + employee_Login.id + " has been created successfully!";
                return RedirectToAction("Signupe", "Home");
                //return View();
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
        public ActionResult Passupd(Admin admin)
        {
            using (DBuserSignupLoginEntities3 db=new DBuserSignupLoginEntities3())
            {
                ViewBag.msg1 = admin.PasswordUs;
                ViewBag.msg2 = admin.NewPasswordUs;
                ViewBag.msg3 = admin.ReNewPasswordUs;
               if(String.IsNullOrEmpty(admin.IdUs.ToString()) || String.IsNullOrEmpty(admin.PasswordUs) || String.IsNullOrEmpty(admin.NewPasswordUs) || String.IsNullOrEmpty(admin.ReNewPasswordUs)|| admin.NewPasswordUs!=admin.ReNewPasswordUs)
                {
                    return View(admin);
                }
                else if (db.Admins.All(x => x.IdUs != admin.IdUs))
                {
                    ViewBag.Message1 = "The admin with id "+admin.IdUs+" does not exists";
                    return View(admin);
                }
                var valid = (from c in db.Admins where c.IdUs.Equals(admin.IdUs) select c).SingleOrDefault();
                ScryptEncoder encoder = new ScryptEncoder();
                bool isvalid = encoder.Compare(admin.PasswordUs, valid.PasswordUs);
                if (valid != null )
                {
                    Admin admin1 = db.Admins.Where(x => x.IdUs == admin.IdUs).FirstOrDefault();
                    
                    if (admin1 != null && isvalid && admin.NewPasswordUs == admin.ReNewPasswordUs)
                    {
                        Admin k = db.Admins.Find(admin1.IdUs);
                        String s1 = admin1.UserNameUs;
                        db.Admins.Remove(k);
                        db.SaveChanges();
                        String k1 = admin.NewPasswordUs;
                        String k2 = encoder.Encode(k1);
                        db.Admins.Add(new Admin()
                        {
                            IdUs=admin.IdUs,
                            UserNameUs=s1,
                            PasswordUs=k2,
                            NewPasswordUs=k2,
                            RePasswordUs=k2,
                            ReNewPasswordUs=k2
                        });
            
                        db.SaveChanges();
                        TempData["message"] = "Password for admin Id " + admin.IdUs + " has been updated successfully!";
                        return RedirectToAction("Passupd", "Home");
                    }
                    else
                    {
                        ViewBag.Message1 = "Old password is incorrect";
                    }
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin admin)
        {
            ViewBag.msg1 = admin.PasswordUs;
            if (String.IsNullOrEmpty(admin.UserNameUs) || String.IsNullOrEmpty(admin.PasswordUs))
            {

                return View();
            }
            ScryptEncoder encoder = new ScryptEncoder();
            var valid = (from c in db.Admins where c.UserNameUs.Equals(admin.UserNameUs) select c).SingleOrDefault();
            if (db.Admins.All(x => x.UserNameUs != admin.UserNameUs))
            {
                ViewBag.Message1 = "The username "+admin.UserNameUs+" does not exists";
                return View();
            }
            bool isvalid = encoder.Compare(admin.PasswordUs, valid.PasswordUs);
           
            if (valid != null&&isvalid==true)
            {
                Session["IdUsSS"] = admin.IdUs.ToString();
                Session["UserNameSS"] = admin.UserNameUs.ToString();
                return RedirectToAction("LoginInfo", "Home");
            }
            else if(admin.PasswordUs.Length<6)
            {
                return View();
            }
            else
            {
                ViewBag.Message1 = "Wrong Username or Password";
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logine(Employee_Login employee_Login)
        {
            ViewBag.msg1 = employee_Login.password;
            ScryptEncoder encoder = new ScryptEncoder();
            if (String.IsNullOrEmpty(employee_Login.id) || String.IsNullOrEmpty(employee_Login.password))
            {
                return View();
            }
            else if (db.Employee_Login.All(x => x.id != employee_Login.id))
            {
                ViewBag.Notification1 = "This employee id "+employee_Login.id+" does not exists";
                return View();
            }
            var valid = (from c in db.Employee_Login where c.id.Equals(employee_Login.id) select c).SingleOrDefault();
            var checkLogin = db.Employee_Login.Where(x => x.id.Equals(employee_Login.id)).FirstOrDefault();
            bool isvalid = encoder.Compare(employee_Login.password,valid.password);
            if (checkLogin != null&&isvalid==true)
            {
                Session["IdUsSS1"] = employee_Login.id.ToString();
                //TempData["mydata"] =Session["IdUsSS1"];
                return RedirectToAction("emphome", "Home");
            }
            else if(employee_Login.password.Length<6)
            {
                return View();
            }
            else
            {
                ViewBag.Notification1 = "Incorrect Id or Password";
            }
            return View();
        }
    }
}
