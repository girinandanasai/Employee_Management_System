//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class Task_report
    {
        public int id { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Employee Id")]
        public int emp_id { get; set; }
        [Display(Name = "Task Name")]
        public string task_name { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Start Date")]
        public string start_date { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "End Date")]
        public string end_date { get; set; }
        [Display(Name = "Task Duration")]
        public int task_duration { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Team Name")]
        public string team_name { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Summary")]
        public string summary { get; set; }
        public string risk { get; set; }
        [Display(Name = "Risk Details")]
        public string risk_details { get; set; }
        [Display(Name = "Risk Resolution")]
        public string risk_resolution { get; set; }
        public int Active { get; set; }
    }
}
