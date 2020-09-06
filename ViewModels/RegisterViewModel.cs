using FixesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixesApp.ViewModels
{
    public class RegisterViewModel
    {
        public IEnumerable<Services> Services { get; set; }
        public Worker Worker { get; set; }
        
    }
}