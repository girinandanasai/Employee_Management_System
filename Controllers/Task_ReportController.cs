using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
namespace WebApplication2.Controllers
{
    public class Task_ReportController : Controller
    {
        DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3();
        // GET: TasskReport
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Task_report task_Report,Team_names team)
        {
            if(task_Report.risk=="Help")
            {
                task_Report.risk = "1";
            }
            else if(task_Report.risk == null)
            {
                task_Report.risk = "0";
            }
            if (db.Employees.All(x => x.id != task_Report.emp_id))
            {
                ViewBag.Notification = "This employee id does not exists";
                return View();
            }
            else if (String.IsNullOrEmpty(task_Report.start_date) || String.IsNullOrEmpty(task_Report.end_date))
            {
                ViewBag.Notification = "start date and end date are required";
                return View();
            }
            else if (String.IsNullOrEmpty(task_Report.emp_id.ToString()) || String.IsNullOrEmpty(task_Report.start_date) || String.IsNullOrEmpty(task_Report.end_date) || String.IsNullOrEmpty(task_Report.team_name) || String.IsNullOrEmpty(task_Report.summary) || String.IsNullOrEmpty(task_Report.task_duration.ToString()))
            {
                //ViewBag.Notification = "Id, role and password are required";
                return View();
            }
            else if (db.Team_names.All(x => x.Team_name != task_Report.team_name))
            {
                ViewBag.Notification = "The team name does not exists";
                return View();
            }
            else if (task_Report.risk.ToString() == "1")
            {
                if (String.IsNullOrEmpty(task_Report.risk_details) || String.IsNullOrEmpty(task_Report.risk_resolution))
                {
                    ViewBag.Notification = "Risk details and risk resolution are required";
                }
                return View();
            }
            else
            {
                DateTime startD = DateTime.Parse(task_Report.start_date);
                DateTime endD = DateTime.Parse(task_Report.end_date);
                double calcBusinessDays = 1 + ((endD - startD).TotalDays * 5 - (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

                if (endD.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
                if (startD.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;
                task_Report.task_duration = (int)calcBusinessDays;
                task_Report.Active = 1;
                ViewData["msg"] = task_Report;
                db.Task_report.Add(task_Report);
                db.SaveChanges();
                ViewBag.Notification = "The report has been successfully saved!";
                return View();
            }
           
        }
        // GET: EmployeeInsert
        public ActionResult Index1()
        {
            return View(db.Task_report.ToList());
        }


        // GET: TasskReport/Edit/5
        public ActionResult Edit(int id)
        {
            using (DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3())
            {
                return View(db.Task_report.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: TasskReport/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,Task_report task_Report,Team_names team)
        {
            try
            {
                if (task_Report.risk == "Help")
                {
                    task_Report.risk = "1";
                }
                else if (task_Report.risk == null)
                {
                    task_Report.risk = "0";
                }
                if (db.Employees.All(x => x.id != task_Report.emp_id))
                {
                    ViewBag.Notification = "This employee id does not exists";
                    return View();
                }
                else if (String.IsNullOrEmpty(task_Report.start_date) || String.IsNullOrEmpty(task_Report.end_date))
                {
                    ViewBag.Notification = "start date and end date are required";
                    return View();
                }
                else if (String.IsNullOrEmpty(task_Report.emp_id.ToString()) || String.IsNullOrEmpty(task_Report.start_date) || String.IsNullOrEmpty(task_Report.end_date) || String.IsNullOrEmpty(task_Report.team_name) || String.IsNullOrEmpty(task_Report.summary) || String.IsNullOrEmpty(task_Report.task_duration.ToString()))
                {
                    ViewBag.Notification = "Id/start date/end date/summary/team name are required";
                    return View();
                }
                else if (db.Team_names.All(x => x.Team_name != task_Report.team_name))
                {
                    ViewBag.Notification = "The team name does not exists";
                    return View();
                }
                else if (task_Report.risk.ToString() == "1")
                {
                    if (String.IsNullOrEmpty(task_Report.risk_details) || String.IsNullOrEmpty(task_Report.risk_resolution))
                    {
                        ViewBag.Notification = "Risk details and risk resolution are required";
                    }
                    return View();
                }
                else
                {
                    DateTime startD = DateTime.Parse(task_Report.start_date);
                    DateTime endD = DateTime.Parse(task_Report.end_date);
                    double calcBusinessDays = 1 + ((endD - startD).TotalDays * 5 - (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

                    if (endD.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
                    if (startD.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;
                    task_Report.task_duration = (int)calcBusinessDays;
                    task_Report.Active = 1;
                    db.Entry(task_Report).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Notification = "The report has been successfully saved!";
                    return RedirectToAction("Index1");

                }
            }
            catch
            {
              
                return View();
            }
        }

        // GET: TasskReport/Delete/5
        public ActionResult Delete(int id)
        {
            using (DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3())
            {
                return View(db.Task_report.Where(x => x.id == id).FirstOrDefault());
            }
         
        }

        // POST: TasskReport/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3())
                {
                    Task_report task_Report = db.Task_report.Where(x => x.id == id).FirstOrDefault();
                    db.Task_report.Remove(task_Report);
                    db.SaveChanges();
                }
                return RedirectToAction("Index1");
            }
            catch
            {
                return View();
            }
        }
    }
}
