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
    public class Login_UsersController : Controller
    {
        private OnlineVaccineEntities db = new OnlineVaccineEntities();

        // GET: Login_Users
        public ActionResult Index()
        {
            return View(db.Login_Users.ToList());
        }

        // GET: Login_Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login_Users login_Users = db.Login_Users.Find(id);
            if (login_Users == null)
            {
                return HttpNotFound();
            }
            return View(login_Users);
        }

        // GET: Login_Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login_Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,EmailID,Password,ConfirmPassword,NumberOfDependents")] Login_Users login_Users)
        {
            if (ModelState.IsValid)
            {
                db.Login_Users.Add(login_Users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(login_Users);
        }

        // GET: Login_Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login_Users login_Users = db.Login_Users.Find(id);
            if (login_Users == null)
            {
                return HttpNotFound();
            }
            return View(login_Users);
        }

        // POST: Login_Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,EmailID,Password,NumberOfDependents")] Login_Users login_Users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login_Users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(login_Users);
        }

        // GET: Login_Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login_Users login_Users = db.Login_Users.Find(id);
            if (login_Users == null)
            {
                return HttpNotFound();
            }
            return View(login_Users);
        }

        // POST: Login_Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Login_Users login_Users = db.Login_Users.Find(id);
            db.Login_Users.Remove(login_Users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Login_Users/Login
        public ActionResult Login()
        {
            return View(); // This will return the Login.cshtml view
        }

        // POST: Login_Users/Login
        [HttpPost]
        public ActionResult Login(Login_Users model)
        {
            // Here you'll validate the user's credentials.
            // If valid, redirect to Dashboard or wherever you'd like.
            // If not valid, return the Login view with an error message.

            // Sample code:
            if (IsValidLogin(model.EmailID, model.Password))
            {
                // Maybe set some session values or use an authentication framework

                return RedirectToAction("Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login credentials.");
                return View();
            }
        }

        private bool IsValidLogin(string email, string password)
        {
            // This is just a placeholder. 
            // You'd check your database or whatever you're using for authentication.
            // NEVER store or compare plain-text passwords. Always use proper hashing techniques.
            return true;
        }
        //... Other code ...

        // GET: Login_Users/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View(); // This will return the ForgotPassword.cshtml view
        }

        // POST: Login_Users/ForgotPasswordAction
        [HttpPost]
        public ActionResult ForgotPasswordAction(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                ModelState.AddModelError("", "Please provide an email address.");
                return View("ForgotPassword");
            }

            var user = db.Login_Users.FirstOrDefault(u => u.EmailID == Email);
            if (user == null)
            {
                ModelState.AddModelError("", "There is no user registered with the provided email.");
                return View("ForgotPassword");
            }

            // Generate reset password link
            // Note: This is a simplified example. In a real-world scenario, you should generate a secure token, store it (e.g., in the database), 
            // and then send a link containing this token to the user's email address. 
            // The user then clicks on the link, and you validate the token, allowing them to reset their password.

            string resetLink = "http://www.yourwebsite.com/ResetPassword?token=some_generated_token"; // Placeholder link

            // Send reset link to the user's email (use an email sending service or library)
            // For simplicity, we're just going to pretend we sent an email.

            TempData["Message"] = "Password reset link has been sent to your email!";
            return RedirectToAction("Login");
        }

        //... Other code ...

        public ActionResult Dashboard()
        {
            var userModel = new Login_Users();
            // Populate the userModel properties from the database or wherever you're getting the data
            return View(userModel);
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
