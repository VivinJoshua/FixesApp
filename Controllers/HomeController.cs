using FixesApp.Extensions;
using FixesApp.Models;
using FixesApp.ViewModels;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace FixesApp.Controllers
{

    [HandleError]

    public class HomeController : Controller
    {
      public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel user)
        {
           
            ScryptEncoder encode = new ScryptEncoder();
            FixesAppContext context = new FixesAppContext();

            if (!ModelState.IsValid)
            {
               // return Content("call");
                return View("Index", user);
            }

            var userExistence = context.User.SingleOrDefault(x => x.MobileNumber == user.MobileNumber);


            if (userExistence == null)
            {
                ViewBag.usernotexist = "User Does Not Exists";
                return View("Index");
            }
            else if (!encode.Compare(user.Password, userExistence.Password))
            {
                ViewBag.incorrectpassword = "Incorrect Password";
                return View("Index");
            }
            else if (encode.Compare(user.Password, userExistence.Password))
            {
                Session["UserId"] = userExistence.UserId;
                // ViewBag.msg = userExistence.UserName;
                Session["Mobile"] = user.MobileNumber;
                return RedirectToAction("Main", "Home");
            }
            return View("Index");
        }
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user)
        {
            ScryptEncoder encode = new ScryptEncoder();
            FixesAppContext context = new FixesAppContext();
            if (!ModelState.IsValid)
            {
                return View("SignUp", user);
            }

            var userExistence = context.User.SingleOrDefault(x => x.MobileNumber == user.MobileNumber);
            if (userExistence != null)
            {
                ViewBag.UserAlreadyExists = "User Already Exists";
                return View("SignUp", user);
            }
            user.Password = encode.Encode(user.Password);
            context.User.Add(user);
            context.SaveChanges();
            // @ViewBag.success = "Account Created Succesfully Login to continue";
            this.AddNotification("Account Created Succesfully Login to continue . Make Sure to update your Location in your Profile", NotificationType.SUCCESS);
            return RedirectToAction("Index");

           
        }

        
        public ActionResult Main(int? id)
        {
            if(Session["Mobile"]==null)
            {
                return RedirectToAction("Index");
            }
            
            FixesAppContext context = new FixesAppContext();
            if(Session["location"]!=null)
            {
                ViewBag.location = Session["location"];
            }
            else
            {
                ViewBag.location = "All";
            }
            
           
            var service = context.Services.ToList();
           
                var worker = context.Worker.Include("Servicess")
                    .ToList().OrderByDescending(x => x.Rating);
                if (id == 1)
                {
                    worker = context.Worker.Include("Servicess")
                               .ToList().OrderBy(x => x.CostPerHour);
                }

            if (Session["location"] != null)
            {
                string location = Session["location"].ToString();
                worker = context.Worker.Include("Servicess").Where(x => x.Location == location)
                   .ToList().OrderByDescending(x => x.Rating);
                if (id == 1)
                {
                    worker = context.Worker.Include("Servicess").Where(x => x.Location == location)
                               .ToList().OrderBy(x => x.CostPerHour);
                }
            }
            MainViewModel mainViewModel = new MainViewModel
            {
                Services = service,
                Worker = worker
            };
            return View(mainViewModel);
        }

        public ActionResult WorkerProfile(int? id)
        {

            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            if (id == null)
                return RedirectToAction("Main");

            FixesAppContext context = new FixesAppContext();            
            var workerDetails = context.Worker.Include("Feedbacks").Include("Servicess").SingleOrDefault(x => x.WorkerId == id);           
         
            return View(workerDetails);
        }

        public ActionResult BookWorker(int? id)
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            string mobilenumber = Session["Mobile"].ToString();
            
            FixesAppContext context = new FixesAppContext();
            var user = context.User.SingleOrDefault(x => x.MobileNumber == mobilenumber);
            var worker = context.Worker.Include("Servicess").SingleOrDefault(x => x.WorkerId == id);
            
            if (user.Location == null || user.Address==null)
            {
                this.AddNotification("Please Fill Location and Address", NotificationType.ERROR);
                return RedirectToAction("EditUserProfile", user);
            }
            else if(user.Location!=worker.Location)
            {
                this.AddNotification("This worker cannot provide service to your location, Please Book worker of your location", NotificationType.ERROR);
                return RedirectToAction("WorkerProfile",new { id=id});
            }
            else if(user.Location== worker.Location)
            {
                WorkDetails workDetails = new WorkDetails
                {
                   UserMobile=Session["Mobile"].ToString(),
                   WorkerMobile=worker.MobileNumber,
                   UserId=user.UserId,
                   WorkerId=worker.WorkerId,
                   RequestDT = DateTime.Now,
                   ServiceName=worker.Servicess.ServiceName,
                   WorkerName=worker.WorkerName,
                   UserName=user.UserName,
                   cost=worker.CostPerHour


                };
                context.WorkDetails.Add(workDetails);
                context.SaveChanges();
                SendSms(mobilenumber,user.UserName);
                this.AddNotification("Booking Successful", NotificationType.SUCCESS);
                return RedirectToAction("WorkerProfile", new { id = id });
            }
            return RedirectToAction("Main");
        }

        public void SendSms(string mobileNumber,string Name)
        {
            const string accountSid = "XXXXXXXXXXXXXXXXXXXXXX";
            const string authToken = "XXXXXXXXXXXXXXXXXXXXXXd";
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber("+91" + mobileNumber);
            var message = MessageResource.Create(
                to,
                from: new PhoneNumber("+XXXXXXXXXXXXXXXXXXXXXX"), //  From number, must be an SMS-enabled Twilio number ( This will send sms from ur "To" numbers ).  
                body: $"Hello {Name} !! Your booking with FixesApp was successful. Thank you for your booking -FixesApp");

            ModelState.Clear();
        }
        public ActionResult CancelBooking(int? id)
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            FixesAppContext context = new FixesAppContext();
            try
            {
                var workdetails = context.WorkDetails.SingleOrDefault(x => x.WorkDetailsId == id);
                context.WorkDetails.Remove(workdetails);
                context.SaveChanges();
                this.AddNotification("Your Booking has been cancelled", NotificationType.ERROR);
            }
            catch(Exception e)
            {
                return RedirectToAction("MyBookings");
            }
            return RedirectToAction("MyBookings");
        }
        public ActionResult Service([Optional]string id)
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            FixesAppContext context = new FixesAppContext();
            Session["service"] = id.ToString();
            var workers = context.Worker.Where(x => x.Servicess.ServiceName == id).ToList();
            if (Session["location"] != null)
            {
                string location = Session["location"].ToString();
                workers = context.Worker.Where(x => x.Servicess.ServiceName == id && x.Location == location).ToList();
            }
            ViewBag.Service = id;
            return View(workers);
        }
        public ActionResult Services([Optional]string id)
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            FixesAppContext context = new FixesAppContext();
            string service = Session["service"].ToString();
            var workers = context.Worker.Where(x => x.Servicess.ServiceName == service).ToList().OrderBy(x=>x.CostPerHour);
            if (id == "2")
            {
                workers = context.Worker.Where(x => x.Servicess.ServiceName == service).ToList().OrderByDescending(x=>x.Rating);
            }
            if (Session["location"] != null)
            {
                string location = Session["location"].ToString();
                workers = context.Worker.Where(x => x.Servicess.ServiceName == service && x.Location == location).ToList().OrderBy(x => x.CostPerHour);
                if (id == "2")
                {
                    workers = context.Worker.Where(x => x.Servicess.ServiceName == service && x.Location == location).ToList().OrderByDescending(x => x.Rating);
                }
            }
                
            ViewBag.Service = service;
            return View(workers);
        }

     
        public ActionResult Location(MainViewModel mainView)
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            
            FixesAppContext context = new FixesAppContext();
            Session["location"] = mainView.Location;
            var service = context.Services.ToList();
            var worker = context.Worker.Include("Servicess").Where(x=>x.Location== mainView.Location)
                .ToList().OrderByDescending(x => x.Rating);           
            MainViewModel mainViewModel = new MainViewModel
            {
                Services = service,
                Worker = worker
                
            };
            return View(mainViewModel);
        }

        
        public ActionResult EditUserProfile()
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }

            string mobilenumber = Session["Mobile"].ToString();
            FixesAppContext context = new FixesAppContext();
            var userDetails = context.User.SingleOrDefault(x => x.MobileNumber == mobilenumber);
            return View(userDetails);
        }

        [HttpPost]
        public ActionResult EditUserProfile(User user)
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            string mobilenumber = Session["Mobile"].ToString();
            FixesAppContext context = new FixesAppContext();
            var userDetails = context.User.SingleOrDefault(x => x.MobileNumber == mobilenumber);
            userDetails.UserName = user.UserName;
            userDetails.Location = user.Location;
            userDetails.Address = user.Address;
            context.SaveChanges();
            var userr = context.WorkDetails.Where(x => x.UserMobile == mobilenumber).ToList();
            userr.ForEach(a => a.UserName = user.UserName);
            context.SaveChanges();
            return RedirectToAction("UserProfile");
        }

        public ActionResult UserProfile()
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            string mobilenumber = Session["Mobile"].ToString();
            FixesAppContext context = new FixesAppContext();
            var userDetails = context.User.SingleOrDefault(x => x.MobileNumber == mobilenumber);
            return View(userDetails);
        }

        public ActionResult MyBookings()
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            string mobilenumber = Session["Mobile"].ToString();
            FixesAppContext context = new FixesAppContext();
            var work = context.WorkDetails.Where(x => x.UserMobile == mobilenumber && x.WorkStatus==0).ToList();
            return View(work);
        }

        public ActionResult CompletedWorks()
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            string mobilenumber = Session["Mobile"].ToString();
            FixesAppContext context = new FixesAppContext();
            var work = context.WorkDetails.Where(x => x.UserMobile == mobilenumber && x.WorkStatus==1).OrderByDescending(x=>x.WorkdoneDT).ToList();
            
            return View(work);
            
        }

        public ActionResult Feedback(int? id)
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            Session["WorkdetailsId"] = id;
            FixesAppContext context = new FixesAppContext();
            var work = context.WorkDetails.Include("Worker").SingleOrDefault(x => x.WorkDetailsId == id);
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel
            {
                WorkDetails = work
            };
            return View(feedbackViewModel);
        }

        public ActionResult Feedbacksave(FeedbackViewModel feedbackViewModel)
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            int id = Convert.ToInt32(Session["WorkdetailsId"]);
            FixesAppContext context = new FixesAppContext();
            var worker = context.WorkDetails.SingleOrDefault(x => x.WorkDetailsId == id);
            Feedback feedback = new Feedback
            {
                WorkerId = worker.WorkerId,
                WorkId = id,
                UserId = worker.UserId,
                Comments = feedbackViewModel.Feedback.Comments,
                Rating = feedbackViewModel.Feedback.Rating,
                UserName = worker.UserName
            };
            context.Feedback.Add(feedback);
            context.SaveChanges();
            worker.Feedbackstatus = 1;
            context.SaveChanges();
            var wokerrating = context.Worker.SingleOrDefault(x=>x.WorkerId== worker.WorkerId);
            if(wokerrating.Rating==0)
            {
                wokerrating.Rating = feedbackViewModel.Feedback.Rating;
            }
            else
            {
                wokerrating.Rating += feedbackViewModel.Feedback.Rating;
                wokerrating.Rating = wokerrating.Rating / 2;
            }
            context.SaveChanges();
            this.AddNotification("Thank you for your Feedback", NotificationType.SUCCESS);
            return RedirectToAction("CompletedWorks");
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

    }
}
