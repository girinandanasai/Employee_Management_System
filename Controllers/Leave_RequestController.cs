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
    public class Leave_RequestController : Controller
    {
        DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3();
        // GET: Leave_Request
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile, Leave_request leave_Request)
        {
            
            DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3();
            if (String.IsNullOrEmpty(leave_Request.emp_id.ToString()) || String.IsNullOrEmpty(leave_Request.emp_name) || String.IsNullOrEmpty(leave_Request.start_date) || String.IsNullOrEmpty(leave_Request.end_date) || String.IsNullOrEmpty(leave_Request.ref_no) || String.IsNullOrEmpty(leave_Request.reason))
            {
                return View();
            }
            else if (String.IsNullOrEmpty(leave_Request.start_date) || String.IsNullOrEmpty(leave_Request.end_date) || String.IsNullOrEmpty(leave_Request.leave_type))
            {
                ViewBag.Notification = "start date, end date and leave type are required";
                return View();
            }
            else if (db.Employees.All(x => x.id != leave_Request.emp_id)
                || db.Employees.All(x => x.Name != leave_Request.emp_name))
            {
                ViewBag.Notification = "This employee id or name does not exists";
                return View();
            }
            else if (db.Employees.Any(x => x.id != leave_Request.emp_id && x.Name == leave_Request.emp_name)||
                db.Employees.Any(x => x.id == leave_Request.emp_id && x.Name != leave_Request.emp_name))
            {
                ViewBag.Notification = "This employee id and name does not match";
                return View();
            }
            else if(Convert.ToInt32(Session["IdUsSS1"]) != leave_Request.emp_id)
            {
                ViewBag.Notification = "Give your employee id";
                return View();
            }
            else
            {
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
                db.Leave_request.Add(new Leave_request
                {
                    id = leave_Request.id,
                    emp_id = leave_Request.emp_id,
                    emp_name = leave_Request.emp_name,
                    start_date = leave_Request.start_date,
                    end_date = leave_Request.end_date,
                    leave_type = leave_Request.leave_type,
                    reason = leave_Request.reason,
                    ref_no = leave_Request.ref_no,
                    status = "submitted",
                    file_name = Path.GetFileName(postedFile.FileName),
                    Content_type = postedFile.ContentType,
                    file_data = bytes,
                    Active=1,
                    Comments=""
                });
                db.SaveChanges();
                ViewBag.Notification1 = "Your leave request has created. Please wait for approval/rejection";
                return View();
            }
            
        }

        [HttpPost]
        public FileResult DownloadFile(int? fileId)
        {
            DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3();
            Leave_request file = db.Leave_request.ToList().Find(model =>model.id == fileId.Value);
            return File(file.file_data, file.Content_type, file.file_name);
        }
        //user
        [HttpGet]
        public ActionResult Index2(string searchBy,string search)
        {
            DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3();
            if(searchBy=="Employee Id")
            {
                var insert1 = from Leave_request in db.Leave_request select Leave_request;
                if (searchBy == "Employee Id" && !String.IsNullOrEmpty(search) && search != "")
                {
                    insert1 = insert1.Where(x => x.emp_id.ToString() == search && x.Active == 1 && x.status == "submitted");
                    Session["insert1"] = insert1.ToList<Leave_request>();
                }
                else if(search=="" || String.IsNullOrEmpty(search))
                {
                    insert1 = insert1.Where(x => x.Active == 1 && x.status == "submitted");
                    Session["insert1"] = insert1.ToList<Leave_request>();
                }

                return View(db.Leave_request.Where(x => x.Active == 1 && x.status == "submitted" && (x.emp_id.ToString() == search || search == null|| search=="")).ToList());
            }
            else if(searchBy == "Employee Name")
            {
                var insert1 = from Leave_request in db.Leave_request select Leave_request;
                if (searchBy == "Employee Name" && !String.IsNullOrEmpty(search) && search != "")
                {
                    insert1 = insert1.Where(x => x.emp_name.StartsWith(search) && x.Active == 1 && x.status == "submitted");
                    Session["insert1"] = insert1.ToList<Leave_request>();
                }
                else if (search == "" || String.IsNullOrEmpty(search))
                {
                    insert1 = insert1.Where(x => x.Active == 1 && x.status == "submitted");
                    Session["insert1"] = insert1.ToList<Leave_request>();
                }
                return View(db.Leave_request.Where(x => x.Active == 1 && x.status == "submitted" && (x.emp_name.StartsWith(search) || search == null|| search == "")).ToList());
            }
            else if (searchBy == "Start Date")
            {
                var insert1 = from Leave_request in db.Leave_request select Leave_request;
                if (searchBy == "Start Date" && !String.IsNullOrEmpty(search) && search != "")
                {
                    insert1 = insert1.Where(x => x.start_date == search && x.Active == 1 && x.status == "submitted");
                    Session["insert1"] = insert1.ToList<Leave_request>();
                }
                else if (search == "" || String.IsNullOrEmpty(search))
                {
                    insert1 = insert1.Where(x => x.Active == 1 && x.status == "submitted");
                    Session["insert1"] = insert1.ToList<Leave_request>();
                }
                return View(db.Leave_request.Where(x => x.Active == 1 && x.status == "submitted" && (x.start_date ==search || search == null || search == "")).ToList());
            }
            else if (searchBy == "End Date")
            {
                var insert1 = from Leave_request in db.Leave_request select Leave_request;
                if (searchBy == "End Date" && !String.IsNullOrEmpty(search) && search != "")
                {
                    insert1 = insert1.Where(x => x.end_date == search && x.Active == 1 && x.status == "submitted");
                    Session["insert1"] = insert1.ToList<Leave_request>();
                }
                else if (search == "" || String.IsNullOrEmpty(search))
                {
                    insert1 = insert1.Where(x => x.Active == 1 && x.status == "submitted");
                    Session["insert1"] = insert1.ToList<Leave_request>();
                }
                return View(db.Leave_request.Where(x => x.Active == 1 && x.status == "submitted" && (x.end_date == search || search == null || search == "")).ToList());
            }
            else if (searchBy == "Leave Type")
            {
                var insert1 = from Leave_request in db.Leave_request select Leave_request;
                if (searchBy == "Leave Type" && !String.IsNullOrEmpty(search) && search != "")
                {
                    insert1 = insert1.Where(x => x.leave_type.StartsWith(search) && x.Active == 1 && x.status == "submitted");
                    Session["insert1"] = insert1.ToList<Leave_request>();
                }
                else if (search == "" || String.IsNullOrEmpty(search))
                {
                    insert1 = insert1.Where(x => x.Active == 1 && x.status == "submitted");
                    Session["insert1"] = insert1.ToList<Leave_request>();
                }
                return View(db.Leave_request.Where(x => x.Active == 1 && x.status == "submitted" && (x.leave_type.StartsWith(search) || search == null || search == "")).ToList());
            }
            return View(db.Leave_request.ToList().Where(x => x.Active==1 && x.status=="submitted"));
        }
        //admin
        public ActionResult Index1()
        {
            DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3();
            return View(db.Leave_request.ToList().Where(x => x.Active == 1 && x.emp_id == Convert.ToInt32(Session["IdUsSS1"])));
        }


        // GET: Leave_Request/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Leave_Request/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Leave_Request/Edit/5
        public ActionResult Edit(int id)
        {
            using (DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3())
            {
                return View(db.Leave_request.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Leave_Request/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Leave_request leave_Request,int id)
        {
            try
            {
                var user = db.Leave_request.Single(model => model.id == id);
                user.emp_id = leave_Request.emp_id;
                user.emp_name = leave_Request.emp_name;
                user.start_date = leave_Request.start_date;
                user.end_date = leave_Request.end_date;
                user.leave_type = leave_Request.leave_type;
                user.reason = leave_Request.reason;
                user.ref_no = leave_Request.ref_no;
                db.SaveChanges();
                ViewBag.Notification1 = "The record is updated successfully";
            }
            catch
            {
                ViewBag.Notification = "The record is not updated";
            }
            return View();
        }

        public ActionResult Edit1(int id)
        {
            using (DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3())
            {
                return View(db.Leave_request.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Leave_Request/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit1(Leave_request leave_Request, int id)
        {
            try
            {
                var user = db.Leave_request.Single(model => model.id == id);
                user.status = leave_Request.status;
                user.Comments = leave_Request.Comments;
                db.SaveChanges();
                ViewBag.Notification1 = "The record is updated successfully";
            }
            catch
            {
                ViewBag.Notification = "The record is not updated";
            }
            return View();
        }

        // GET: Leave_Request/Delete/5
        public ActionResult Delete(int id)
        {
            using (DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3())
            {
                return View(db.Leave_request.Where(x => x.id == id).FirstOrDefault());
            }
        }

        // POST: Leave_Request/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,Leave_request leave_Request)
        {
            try
            {
                using (DBuserSignupLoginEntities3 db = new DBuserSignupLoginEntities3())
                {
                    var user = db.Leave_request.Single(model => model.id == id);
                    user.Active = 0;
                    
                    //db.Task_report.Remove(task_Report);
                    db.SaveChanges();
                }
                ViewBag.Notification = "The record is deleted successfully";
                return View();
            }
            catch
            {
                ViewBag.Notification = "The record is not deleted";
                return View();
            }
        }

        [HttpPost]
        public FileResult ExportToExcel(string searchBy,string search)
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[9]
            {
                new DataColumn("emp_id"),
                new DataColumn("emp_name"),
                new DataColumn("statrt_date"),
                new DataColumn("end_date"),
                new DataColumn("leave_type"),
                new DataColumn("reason"),
                new DataColumn("ref_no"),
                new DataColumn("status"),
                new DataColumn("Comments")
            });
            var insert1 = (List<Leave_request>)Session["insert1"];
           
            foreach (var emp in insert1)
            {
                dt.Rows.Add(emp.emp_id, emp.emp_name, emp.start_date, emp.end_date, emp.leave_type,
                    emp.reason, emp.ref_no, emp.status, emp.Comments);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Leave_Request.xlsx");
                }

            }
        }
    }
}
