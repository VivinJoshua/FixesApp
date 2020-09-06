using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FixesApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[9876]{1}[0-9]{9}$", ErrorMessage = "Enter Valid Mobile Number")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        public byte[] userImage { get; set; }
        
        public string Address { get; set; }

        
        public string Location { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<Work> Work { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}