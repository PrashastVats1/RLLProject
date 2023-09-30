using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RLLProjPractice.Filters;
using RLLProjPractice.Models;

namespace RLLProjPractice.Controllers
{
    public class UsersController : Controller
    {
        private VaccineMgmtEntities db = new VaccineMgmtEntities();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.UserType);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [AuthorizeUserType("Admin")]
        public ActionResult Create()
        {
            ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "UserTypeDescription");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,LastName,Email,Password,UserTypeID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "UserTypeDescription", user.UserTypeID);
            return View(user);
        }

        // GET: Users/Edit/5
        [AuthorizeUserType("Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "UserTypeDescription", user.UserTypeID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,LastName,Email,Password,UserTypeID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserTypeID = new SelectList(db.UserTypes, "UserTypeID", "UserTypeDescription", user.UserTypeID);
            return View(user);
        }

        // GET: Users/Delete/5
        [AuthorizeUserType("Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Users/Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                // Validate reCAPTCHA first
                var recaptchaResponse = Request["g-recaptcha-response"];
                if (!ValidateRecaptcha(recaptchaResponse))
                {
                    ModelState.AddModelError(string.Empty, "Captcha validation failed");
                    return View(model);
                }

                var userInDb = db.Users.FirstOrDefault(u => u.Email == model.Email);

                // Basic password verification; Note: It's crucial to hash and salt passwords for production apps.
                if (userInDb != null && userInDb.Password == model.Password)
                {
                    // User is authenticated. Store user in session/cookie or use some other mechanism.
                    Session["AuthenticatedUser"] = userInDb;

                    // Redirect based on UserType
                    if (userInDb.UserType.UserTypeDescription == "Admin")
                    {
                        return RedirectToAction("Index", "AdminDashboard");
                    }
                    else
                    {
                        return RedirectToAction("Index", "UserDashboard");
                    }
                }
                else
                {
                    // Authentication failed
                    ModelState.AddModelError(string.Empty, "Invalid email or password");
                    return View(model);
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }
        private bool ValidateRecaptcha(string response)
        {
            // Use reCAPTCHA's API to validate the response
            // You will need to make an HTTP request to Google's service
            // and check if the user's response was valid.
            // Note: Please don't use hardcoded API keys or secrets; use configuration settings
            string secretKey = "YOUR_SECRET_KEY";
            var client = new System.Net.WebClient();
            string reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<RecaptchaResponse>(reply);
            return captchaResponse.Success;
        }

        public class RecaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
            // ... Other properties based on Google's response
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
