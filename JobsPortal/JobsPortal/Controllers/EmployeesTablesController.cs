using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccessLayer;
using log4net;

namespace JobsPortal.Controllers
{
    public class EmployeesTablesController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EmployeesTablesController));
        private JobsPortalDbEntities db = new JobsPortalDbEntities();

        // GET: EmployeesTables/Details
        public ActionResult Details()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            log.Info("Details action called");
            int employeeId = 0;
            int.TryParse(Convert.ToString(Session["EmployeeID"]), out employeeId);

            if (employeeId == 0)
            {
                log.Warn("EmployeeID session value is missing or zero");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmployeesTable employeesTable = db.EmployeesTables.Find(employeeId);
            if (employeesTable == null)
            {
                log.Error($"Employee with ID {employeeId} not found");
                return HttpNotFound();
            }
            return View(employeesTable);
        }

        // GET: EmployeesTables/Edit
        public ActionResult Edit()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            log.Info("Edit action called");
            int userId = 0;
            int employeeId = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userId);
            int.TryParse(Convert.ToString(Session["EmployeeID"]), out employeeId);

            if (employeeId == 0)
            {
                log.Warn("EmployeeID session value is missing or zero");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmployeesTable employeesTable = db.EmployeesTables.Find(employeeId);
            if (employeesTable == null)
            {
                log.Error($"Employee with ID {employeeId} not found");
                return HttpNotFound();
            }

            ViewBag.UserId = new SelectList(db.UserTables, "UserID", "UserName", employeesTable.UserId);
            return View(employeesTable);
        }

        // POST: EmployeesTables/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,UserId,EmployeeName,DOB,Education,WorkExperience,Skills,EmailAddress,Gender,Photo,Qualification,PermanentAddress,JobReference,Description,ResumeName,ResumeData,ResumeType")] EmployeesTable employeesTable)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            if (ModelState.IsValid)
            {
                db.Entry(employeesTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            ViewBag.UserId = new SelectList(db.UserTables, "UserID", "UserName", employeesTable.UserId);
            return View(employeesTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
