using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
            if (String.IsNullOrEmpty(task_Report.emp_id.ToString()) || String.IsNullOrEmpty(task_Report.team_name) || String.IsNullOrEmpty(task_Report.summary))
            {
                //ViewBag.Notification = "Id, role and password are required";
                return View();
            }
            else if (String.IsNullOrEmpty(task_Report.start_date) || String.IsNullOrEmpty(task_Report.end_date))
            {
                ViewBag.Notification = "start date and end date are required";
                return View();
            }
            else if (db.Employees.All(x => x.id != task_Report.emp_id))
            {
                ViewBag.Notification = "This employee id does not exists";
                return View();
            }
            else if(Convert.ToInt32(Session["IdUsSS1"]) != task_Report.emp_id)
            {
                ViewBag.Notification = "Give your employee id";
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
                    ViewBag.Notification1 = "The task report has been successfully created!";
                    return View();
                }
                
            }
            return View();
           
        }
        // GET: EmployeeInsert
        public ActionResult Index1()
        {
            return View(db.Task_report.ToList().Where(x => x.Active == 1 && x.emp_id==Convert.ToInt32(Session["IdUsSS1"])));
        }
        public ActionResult Index2()
        {
            return View(db.Task_report.ToList().Where(x => x.Active ==1));
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
                if (String.IsNullOrEmpty(task_Report.emp_id.ToString()) || String.IsNullOrEmpty(task_Report.start_date) || String.IsNullOrEmpty(task_Report.end_date) || String.IsNullOrEmpty(task_Report.team_name) || String.IsNullOrEmpty(task_Report.summary) || String.IsNullOrEmpty(task_Report.task_duration.ToString()))
                {
                    ViewBag.Notification = "Id/start date/end date/summary/team name are required";
                    return View();
                }
                else if (String.IsNullOrEmpty(task_Report.start_date) || String.IsNullOrEmpty(task_Report.end_date))
                {
                    ViewBag.Notification = "start date and end date are required";
                    return View();
                }
                else if (db.Employees.All(x => x.id != task_Report.emp_id))
                {
                    ViewBag.Notification = "This employee id does not exists";
                    return View();
                }
                else if (Convert.ToInt32(Session["IdUsSS1"]) != task_Report.emp_id)
                {
                    ViewBag.Notification = "employee id can't be changed";
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
                    ViewBag.Notification1 = "The report has been successfully updated!";
                    return View();

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
                    task_Report.Active = 0;
                    //db.Task_report.Remove(task_Report);
                    db.SaveChanges();
                }
                return RedirectToAction("Index1");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public FileResult ExportToExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[10]
            {
                new DataColumn("emp_id"),
                new DataColumn("task_name"),
                new DataColumn("start_date"),
                new DataColumn("end_date"),
                new DataColumn("task_duration"),
                new DataColumn("team_name"),
                new DataColumn("summary"),
                new DataColumn("risk"),
                new DataColumn("risk_details"),
                new DataColumn("risk_resolution")
            });
            var insertEmployeedet = from Task_report in db.Task_report select Task_report;
            foreach (var emp in insertEmployeedet)
            {
                dt.Rows.Add(emp.emp_id, emp.task_name, emp.start_date, emp.end_date, emp.task_duration,
                    emp.team_name, emp.summary, emp.risk, emp.risk_details, emp.risk_resolution);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Task_report.xlsx");
                }

            }
        }
    }
}
