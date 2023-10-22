using DatabaseAccessLayer;
using JobsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobsPortal.Controllers
{
    public class JobController : Controller
    {
        private JobsPortalDbEntities db = new JobsPortalDbEntities();
        // GET: Job
        public ActionResult PostJob()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            var job = new PostJobMV();
            ViewBag.JobCategoryID = new SelectList(
                                    db.JobCategoryTables.ToList(),
                                    "JobCategoryID",
                                    "JobCategory",
                                    "0");
            ViewBag.JobNatureID = new SelectList(
                                    db.JobNatureTables.ToList(),
                                    "JobNatureID",
                                    "JobNature",
                                    "0");
            return View(job);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostJob(PostJobMV postJobMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            int userId = 0;
            int companyId = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userId);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyId);
            postJobMV.UserID = userId;
            postJobMV.CompanyID = companyId;

            if(ModelState.IsValid)
            {
                var post = new PostJobTable();
                post.UserID = postJobMV.UserID;
                post.CompanyID = postJobMV.CompanyID;
                post.JobCategoryID = postJobMV.JobCategoryID;
                post.JobTitle = postJobMV.JobTitle;
                post.JobDescription = postJobMV.JobDescription;
                post.MinSalary = postJobMV.MinSalary;
                post.MaxSalary = postJobMV.MaxSalary;
                post.Location = postJobMV.Location;
                post.Vacancy = postJobMV.Vacancy;
                post.JobNatureID = postJobMV.JobNatureID;
                post.PostDate = DateTime.Now;
                post.ApplicationDeadline = postJobMV.ApplicationDeadline;
                post.LastDate = postJobMV.ApplicationDeadline;
                post.JobStatusID = 1;
                post.WebUrl = postJobMV.WebUrl;
                db.PostJobTables.Add(post);
                db.SaveChanges();
                return RedirectToAction("CompanyJobsList");
            }

            ViewBag.JobCategoryID = new SelectList(
                                    db.JobCategoryTables.ToList(),
                                    "JobCategoryID",
                                    "JobCategory",
                                    "0");
            ViewBag.JobNatureID = new SelectList(
                                    db.JobNatureTables.ToList(),
                                    "JobNatureID",
                                    "JobNature",
                                    "0");
            return View(postJobMV);
        }

        public ActionResult CompanyJobsList()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            int userId = 0;
            int companyId = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userId);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyId);
            var allPost = db.PostJobTables.ToList();
            return View(allPost);
        }
    }
}