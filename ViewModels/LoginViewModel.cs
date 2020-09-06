using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FixesApp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [RegularExpression(@"^[9876]{1}[0-9]{9}$", ErrorMessage = "Enter Valid Mobile Number")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}