using FixesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FixesApp.ViewModels
{
    public class FeedbackViewModel
    {
        public WorkDetails WorkDetails { get; set; }

        public Feedback Feedback { get; set; }

    }
}