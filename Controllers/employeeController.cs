using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class employeeController : ApiController
    {
        public IHttpActionResult insertemployees(Employee sc)
        {
            DBuserSignupLoginEntities2 nd = new DBuserSignupLoginEntities2();
            nd.Employees.Add(new Employee()
            {
                id = sc.id,
                Name = sc.Name,
                Date_of_birth = sc.Date_of_birth,
                Father_Name = sc.Father_Name,
                Mother_Name = sc.Mother_Name,
                Address = sc.Address,
                Salary = sc.Salary,
                Fresher = sc.Fresher,
                Role = sc.Role,
                Notes = sc.Notes
            });
            nd.SaveChanges();
            return Ok();


        }
    }
}
