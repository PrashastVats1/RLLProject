using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RllPracticeProj.Models;


namespace RllPracticeProj.Controllers
{
    public class Login_AdminController : Controller
    {
        private OnlineVaccineEntities db = new OnlineVaccineEntities();

        // GET: Login_Admin
        public ActionResult Index()
        {
            var login_Admin = db.Login_Admin.Include(l => l.VaccineCentre);
            return View(login_Admin.ToList());
        }

        // GET: Login_Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login_Admin login_Admin = db.Login_Admin.Find(id);
            if (login_Admin == null)
            {
                return HttpNotFound();
            }
            return View(login_Admin);
        }

        // GET: Login_Admin/Create
        public ActionResult Create()
        {
            ViewBag.CentreID = new SelectList(db.VaccineCentres, "CentreID", "Hospital_Name");
            return View();
        }

        // POST: Login_Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Password,Position,CentreID")] Login_Admin login_Admin)
        {
            if (ModelState.IsValid)
            {
                db.Login_Admin.Add(login_Admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CentreID = new SelectList(db.VaccineCentres, "CentreID", "Hospital_Name", login_Admin.CentreID);
            return View(login_Admin);
        }

        // GET: Login_Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login_Admin login_Admin = db.Login_Admin.Find(id);
            if (login_Admin == null)
            {
                return HttpNotFound();
            }
            ViewBag.CentreID = new SelectList(db.VaccineCentres, "CentreID", "Hospital_Name", login_Admin.CentreID);
            return View(login_Admin);
        }

        // POST: Login_Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Password,Position,CentreID")] Login_Admin login_Admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login_Admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CentreID = new SelectList(db.VaccineCentres, "CentreID", "Hospital_Name", login_Admin.CentreID);
            return View(login_Admin);
        }

        // GET: Login_Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login_Admin login_Admin = db.Login_Admin.Find(id);
            if (login_Admin == null)
            {
                return HttpNotFound();
            }
            return View(login_Admin);
        }

        // POST: Login_Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Login_Admin login_Admin = db.Login_Admin.Find(id);
            db.Login_Admin.Remove(login_Admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login_Admin model)
        {
            if (ModelState.IsValid)
            {
                // Assuming your database has a method to validate the user. 
                var user = db.Login_Admin.FirstOrDefault(u => u.UserID == model.UserID && u.Password == model.Password);
                if (user != null)
                {
                    // Login successful, set sessions or cookies as needed and redirect to a secured page or dashboard.
                    return RedirectToAction("Dashboard"); // Or wherever you want to redirect after a successful login.
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return View();
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
