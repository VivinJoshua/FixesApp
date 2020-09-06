using FixesApp.Extensions;
using FixesApp.Migrations;
using FixesApp.Models;
using FixesApp.ViewModels;
using Scrypt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FixesApp.Controllers
{

    [HandleError]
    public class WorkerController : Controller
    {
        // GET: Worker
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel user)
        {

            ScryptEncoder encode = new ScryptEncoder();
            FixesAppContext context = new FixesAppContext();

            if (!ModelState.IsValid)
            {
                // return Content("call");
                return View("Login", user);
            }

            var userExistence = context.Worker.SingleOrDefault(x => x.MobileNumber == user.MobileNumber);


            if (userExistence == null)
            {
                ViewBag.usernotexist = "User Does Not Exists";
                return View("Login");
            }
            else if (!encode.Compare(user.Password, userExistence.Password))
            {
                ViewBag.incorrectpassword = "Incorrect Password";
                return View("Login");
            }
            else if (encode.Compare(user.Password, userExistence.Password))
            {
                Session["workerId"] = userExistence.WorkerId;
                // ViewBag.msg = userExistence.UserName;
                Session["Mobile"] = user.MobileNumber;
                return RedirectToAction("Main", "Worker");
            }
            return View("Login");
        }

        public ActionResult SignUp()
        {
            FixesAppContext context = new FixesAppContext();
            var serviceType = context.Services.ToList();
            var viewmodel = new RegisterViewModel
            {
                Services = serviceType
            };
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult SignUp(Worker worker)
        {
            ScryptEncoder encode = new ScryptEncoder();
            FixesAppContext context = new FixesAppContext();
            if (!ModelState.IsValid)
            {
                var viewmodel = new RegisterViewModel
                {
                    Services = context.Services.ToList(),
                    Worker = worker
                };
                return View("SignUp", viewmodel);
            }

            var userExistence = context.Worker.SingleOrDefault(x => x.MobileNumber == worker.MobileNumber);
            if (userExistence != null)
            {
                ViewBag.UserAlreadyExists = "User Already Exists";
                var viewmodel = new RegisterViewModel
                {
                    Services = context.Services.ToList(),
                    Worker = worker
                };
                return View("SignUp", viewmodel);
            }
            //worker.workerImage = getimage(worker.workerImage);
            HttpPostedFileBase image = Request.Files["workerImage"];
            byte[] bytes=null;
            using (BinaryReader br = new BinaryReader(image.InputStream))
            {
                bytes = br.ReadBytes(image.ContentLength);
            }
            worker.workerImage = bytes;
            worker.Password = encode.Encode(worker.Password);
            worker.Location = worker.Location.ToLower();
            context.Worker.Add(worker);
            context.SaveChanges();
            // @ViewBag.success = "Account Created Succesfully Login to continue";
            this.AddNotification("Account Created Succesfully Login to continue", NotificationType.SUCCESS);
            return RedirectToAction("Login");
        }

        public ActionResult Main()
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            string mobilenumber = Session["Mobile"].ToString();
            FixesAppContext context = new FixesAppContext();
            var work = context.WorkDetails.Include("User").Where(x => x.WorkerMobile == mobilenumber && x.WorkStatus==0).OrderBy(x=>x.RequestDT).ToList();
            return View(work);
        }

        public ActionResult MyWorks()
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            string mobilenumber = Session["Mobile"].ToString();
            FixesAppContext context = new FixesAppContext();
            var work = context.WorkDetails.Include("User").Where(x => x.WorkerMobile == mobilenumber && x.WorkStatus == 1).OrderByDescending(x => x.RequestDT).ToList();
            return View(work);
        }
        
        public ActionResult WorkerProfile()
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            string mobilenumber = Session["Mobile"].ToString();
            FixesAppContext context = new FixesAppContext();
            var worker = context.Worker.Include("Feedbacks").Include("Servicess").SingleOrDefault(x => x.MobileNumber == mobilenumber);
           
            return View(worker);
        }

        public ActionResult EditProfile()
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            string mobilenumber = Session["Mobile"].ToString();
            FixesAppContext context = new FixesAppContext();
            var workerDetails = context.Worker.SingleOrDefault(x => x.MobileNumber == mobilenumber);
            return View(workerDetails);
        }

        [HttpPost]
        public ActionResult EditProfile(Worker worker)
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            string mobilenumber = Session["Mobile"].ToString();
            FixesAppContext context = new FixesAppContext();
            var WorkerDetails = context.Worker.SingleOrDefault(x => x.MobileNumber == mobilenumber);

            WorkerDetails.WorkerName = worker.WorkerName;            
            WorkerDetails.CostPerHour = worker.CostPerHour;
            context.SaveChanges();
            var work = context.WorkDetails.Where(x=>x.WorkerMobile==mobilenumber).ToList();
            work.ForEach(a => a.WorkerName = worker.WorkerName);
            context.SaveChanges();
            
            return RedirectToAction("WorkerProfile");
        }

        public ActionResult WorkCompleted(int? id)
        {
            if (Session["Mobile"] == null)
            {
                return RedirectToAction("Index");
            }
            FixesAppContext context = new FixesAppContext();
            var WorkerDetails = context.WorkDetails.SingleOrDefault(x => x.WorkDetailsId == id);
            WorkerDetails.WorkStatus = 1;
            WorkerDetails.WorkdoneDT = DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("Main");
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
    }