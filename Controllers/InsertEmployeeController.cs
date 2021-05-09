using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class InsertEmployeeController : Controller
    {
        // GET: InsertEmployee
        public ActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Index(Employee sc)
        {
            DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3();
            if (db.Employees.Any(x => x.id == sc.id))
            {
                ViewBag.Notification = "This Employee already exists";
                return View();
            }
            else
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44327/api/employee");
                var insertemployee = hc.PostAsJsonAsync<Employee>("employee", sc);
                var saveemployee = insertemployee.Result;
                if (saveemployee.IsSuccessStatusCode)
                {
                    ViewBag.message = "Record is saved successfully";
                }
                else
                {
                    ViewBag.message = "Record is not saved successfully";
                }
                return View();
            }
        }
    }
}