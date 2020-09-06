using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FixesApp.Models
{
    public class WorkDetails
    {
        [Key]
        public int WorkDetailsId { get; set; }

        public string WorkerMobile { get; set; }

        public string UserMobile { get; set; }
        public int UserId { get; set; }

        public int WorkerId { get; set; }
        public string WorkerName { get; set; }
        public string UserName { get; set; }
        public int RequestStatus { get; set; }
        public int WorkStatus { get; set; }
        public DateTime? RequestDT { get; set; }
        public DateTime? WorkdoneDT { get; set; }
        public string ServiceName { get; set; }

        public int Feedbackstatus { get; set; }
       
    
        public int cost { get; set; }
        public virtual User User { get; set; }
        public virtual Worker Worker { get; set; }
    }
}