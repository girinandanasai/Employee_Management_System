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

    public partial class TBLUserInfo
    {
        public int IdUs { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Username")]
        public string UserNameUs { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string PasswordUs { get; set; }

        [Required(ErrorMessage = "This Field is Required")]
        [Display(Name = "RePassword")]
        [DataType(DataType.Password)]
        [NotMapped]
        [Compare("PasswordUs", ErrorMessage = "Confirm password does n't match, type again!")]
        public string RePasswordUs { get; set; }
    }
}