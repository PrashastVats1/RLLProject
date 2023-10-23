using DatabaseAccessLayer;
using JobsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace JobsPortal.Controllers
{
    public class UserProfileController : Controller
    {

        private JobsPortalDbEntities db = new JobsPortalDbEntities();
        // GET: Job
        public ActionResult UserProfile()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            var employee = new EmployeeMV();
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile(EmployeeMV employeeMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }

            int userId = 0;
            int employeeId = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userId);
            int.TryParse(Convert.ToString(Session["EmployeeID"]), out employeeId);
            employeeMV.UserId = userId;
            employeeMV.EmployeeID = employeeId;

            if (ModelState.IsValid)
            {
                var employee = new EmployeesTable()
                {
                    UserId = employeeMV.UserId,
                    EmployeeName = employeeMV.EmployeeName,
                    Description = employeeMV.Description,
                    Gender = employeeMV.Gender,
                    Skills = employeeMV.Skills,
                    Qualification = employeeMV.Qualification,
                    EmailAddress = employeeMV.EmailAddress,
                    Photo = employeeMV.Photo,
                    JobReference = employeeMV.JobReference,
                    PermanentAddress = employeeMV.PermanentAddress,
                    ResumeName = employeeMV.ResumeName,
                    ResumeData = employeeMV.ResumeData,
                    ResumeType = employeeMV.ResumeType

                };

                db.EmployeesTables.Add(employee);
                db.SaveChanges();
                return RedirectToAction("MyProfile");
            }
            return View(employeeMV);
        }
        // GET: Profile
        public ActionResult MyProfile()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            int userId = 0;
            int employeeId = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userId);
            int.TryParse(Convert.ToString(Session["EmployeeID"]), out employeeId);
            var allPost = db.EmployeesTables.Where(c => c.EmployeeID == employeeId && c.UserId == userId).ToList();
            var employeeList = allPost.Select(e => new EmployeeMV 
            {
                EmployeeID = e.EmployeeID,
                UserId = e.UserId,
                EmployeeName = e.EmployeeName,
                DOB = e.DOB,
                Education = e.Education,
                WorkExperience = e.WorkExperience,
                Skills = e.Skills,
                EmailAddress = e.EmailAddress,
                Gender = e.Gender,
                Photo = e.Photo,
                Qualification = e.Qualification,
                PermanentAddress = e.PermanentAddress,
                JobReference = e.JobReference,
                Description = e.Description,
                ResumeName = e.ResumeName,
                ResumeData = e.ResumeData,
                ResumeType = e.ResumeType
            }).ToList();
            return View(employeeList);
        }
    }
}