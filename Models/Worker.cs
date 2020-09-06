using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FixesApp.Models
{
    public class Worker
    {

        [Key]
        public int WorkerId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string WorkerName { get; set; }

        [Required]
        [RegularExpression(@"^[9876]{1}[0-9]{9}$", ErrorMessage = "Enter Valid Mobile Number")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required]
        public int ServicesId { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Cost Per Hour")]
        public int CostPerHour { get; set; }
        public Services Servicess { get; set; }

        [Required]
        public string Password { get; set; }

        public int Rating { get; set; }

        [Display(Name = "Profile Image")]
        public byte[] workerImage { get; set; }

        public virtual ICollection<WorkDetails> WorkDetails { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}