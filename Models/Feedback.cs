using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FixesApp.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        public int WorkerId { get; set; }
        public int WorkId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

        
        public string Comments { get; set; }
        [Required]
        public int Rating { get; set; }

        public virtual Worker Workers { get; set; }
        public virtual Work Works { get; set; }
        public virtual User Users { get; set; }

    }
}