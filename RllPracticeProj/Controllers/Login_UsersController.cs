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
        public ActionResult Create([Bind(Include = "UserId,EmailID,Password,NumberOfDependents")] Login_Users login_Users)
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
