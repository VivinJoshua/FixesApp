using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FixesApp.Models
{
    public class Work
    {
        [Key]
        public int WorkId { get; set; }

      
        public int UserId { get; set; }

        public int WorkerId { get; set; }
        public int RequestStatus { get; set; }
        public int WorkStatus { get; set; }
        public DateTime RequestDT { get; set; }
        public DateTime WorkdoneDT { get; set; }

        public virtual User Users { get; set; }

        public virtual Worker Workers { get; set; }
        public virtual Feedback Feedbacks { get; set; }
    }
}