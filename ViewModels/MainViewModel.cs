using FixesApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FixesApp.ViewModels
{
    public class MainViewModel
    {
        public List<Services> Services { get; set; }

        [Required]
        public string Location { get; set; }

        public IEnumerable<Worker> Worker { get; set; }
    }
}