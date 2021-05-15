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

    public partial class Employee_Login
    {
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Id")]
        public string id { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]

        public string password { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Confirm password does n't match, type again!")]
        [NotMapped]
        public string repassword { get; set; }
    }
}
