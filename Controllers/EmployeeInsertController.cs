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
    public class EmployeeInsertController : Controller
    {
        DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3();
        // GET: EmployeeInsert
        public ActionResult Index()
        { 
            return View(db.Employees.ToList());
        }

        // GET: Crud/Edit/5
        public ActionResult Edit(int id)
        {
            using(DBuserSignupLoginEntities3 db=new DBuserSignupLoginEntities3())
            {
                return View(db.Employees.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Crud/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,Employee employee)
        {
            try
            {
                using (DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3())
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Crud/Delete/5
        public ActionResult Delete(int id)
        {
            using (DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3())
            {
                return View(db.Employees.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Crud/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3())
                {
                    Employee employee = db.Employees.Where(x => x.id == id).FirstOrDefault();
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
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
                new DataColumn("id"),
                new DataColumn("Name"),
                new DataColumn("Date_of_birth"),
                new DataColumn("Father_Name"),
                new DataColumn("Mother_Name"),
                new DataColumn("Address"),
                new DataColumn("Salary"),
                new DataColumn("Fresher"),
                new DataColumn("Role"),
                new DataColumn("Notes")
            });
            var insertEmployeedet = from Employee in db.Employees select Employee;
            foreach (var emp in insertEmployeedet)
            {
                dt.Rows.Add(emp.id, emp.Name, emp.Date_of_birth, emp.Father_Name, emp.Mother_Name,
                    emp.Address, emp.Salary, emp.Fresher, emp.Role, emp.Notes);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
                }

            }
        }
    }
}