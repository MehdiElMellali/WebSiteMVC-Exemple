using JobOffersSOA.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace JobOffersSOA.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult Details(int jobId)
        {
            var job = db.Jobs.Find(jobId); 
            if(job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = jobId;
            return View(job);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel conatct)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("mail@mail.com", "Password");
            mail.From = new MailAddress(conatct.Email);
            mail.To.Add(new MailAddress("mail@mail.com"));
            mail.Subject = conatct.Subject;
            mail.IsBodyHtml = true;
            mail.Body = conatct.Message;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Applay()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Applay(string Message)
        {
            var userId = User.Identity.GetUserId();
            var jobId = (int)Session["JobId"];

            var check = db.ApplyForJobs.Where(a => a.jobId == jobId && a.UserId == userId).ToList();
            if(check.Count < 1)
            {
                var applayforJob = new ApplyForJob();

                applayforJob.jobId = jobId;
                applayforJob.Message = Message;
                applayforJob.UserId = userId;
                applayforJob.applayDate = DateTime.Now;

                db.ApplyForJobs.Add(applayforJob);
                db.SaveChanges();
                ViewBag.Result = "Apply succsuffly";
            }
            else
            {
                ViewBag.Result = "already Exist";
            }
            return View();
        }
        [Authorize]
        public ActionResult GetJobByUser()
        {
            var userId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(a => a.UserId == userId);
            return View(jobs.ToList());
        }
        [Authorize]
        public ActionResult DetailsGetJobByUser(int id)
        {
            var applyjob = db.ApplyForJobs.Find(id);
            if (applyjob == null)
            {
                return HttpNotFound();
            }
            return View(applyjob);
        }
        // GET: Roles/Edit/5
        public ActionResult Edit(int id)
        {

            var applyjob = db.ApplyForJobs.Find(id);
            if (applyjob == null)
            {
                return HttpNotFound();
            }

            return View(applyjob);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplyForJob applyjob)
        {

            if (ModelState.IsValid)
            {

                applyjob.applayDate = DateTime.Now;
                db.Entry(applyjob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobByUser");
            }


            return View(applyjob);

        }
        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {

            var applyjob = db.ApplyForJobs.Find(id);
            if (applyjob == null)
            {
                return HttpNotFound();
            }

            return View(applyjob);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob applyjob)
        {
            try
            {
                var myApplyjob = db.ApplyForJobs.Find(applyjob.Id);
                db.ApplyForJobs.Remove(myApplyjob);
                db.SaveChanges();

                return RedirectToAction("GetJobByUser");
            }
            catch
            {
                return View(applyjob);
            }
        }

        [Authorize]
        public ActionResult GetJobByPublisher()
        {
            var userId = User.Identity.GetUserId();
            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.jobId equals job.Id
                       where job.User.Id == userId
                       select app;
            var grouped = from j in Jobs
                          group j by j.Job.JobTitle
                          into gr
                          select new JobViewModel
                          {
                              TitleJob = gr.Key,
                              Items = gr

                          };

            return View(grouped.ToList());
        }
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchName)
                                       || a.JobContent.Contains(searchName)
                                       || a.Category.CategoryName.Contains(searchName)
                                       || a.Category.CategoryDescription.Contains(searchName)
            ).ToList();


            return View(result);
        }
    }
}