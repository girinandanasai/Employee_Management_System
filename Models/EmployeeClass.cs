using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class EmployeeClass
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Date_of_birth { get; set; }
        public string Father_Name { get; set; }
        public string Mother_Name { get; set; }
        public string Address { get; set; }
        public int Salary { get; set; }
        public int Fresher { get; set; }
        public string Role { get; set; }
        public string Notes { get; set; }
    }
}
