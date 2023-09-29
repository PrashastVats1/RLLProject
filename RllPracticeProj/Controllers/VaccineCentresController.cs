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
    public class VaccineCentresController : Controller
    {
        private OnlineVaccineEntities db = new OnlineVaccineEntities();

        // GET: VaccineCentres
        public ActionResult Index()
        {
            return View(db.VaccineCentres.ToList());
        }

        // GET: VaccineCentres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccineCentre vaccineCentre = db.VaccineCentres.Find(id);
            if (vaccineCentre == null)
            {
                return HttpNotFound();
            }
            return View(vaccineCentre);
        }

        // GET: VaccineCentres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VaccineCentres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CentreID,Hospital_Name,Address,District,Sales,Pin_code,VaccineName,VaccineCost")] VaccineCentre vaccineCentre)
        {
            if (ModelState.IsValid)
            {
                db.VaccineCentres.Add(vaccineCentre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vaccineCentre);
        }

        // GET: VaccineCentres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccineCentre vaccineCentre = db.VaccineCentres.Find(id);
            if (vaccineCentre == null)
            {
                return HttpNotFound();
            }
            return View(vaccineCentre);
        }

        // POST: VaccineCentres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CentreID,Hospital_Name,Address,District,Sales,Pin_code,VaccineName,VaccineCost")] VaccineCentre vaccineCentre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaccineCentre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vaccineCentre);
        }

        // GET: VaccineCentres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccineCentre vaccineCentre = db.VaccineCentres.Find(id);
            if (vaccineCentre == null)
            {
                return HttpNotFound();
            }
            return View(vaccineCentre);
        }

        // POST: VaccineCentres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VaccineCentre vaccineCentre = db.VaccineCentres.Find(id);
            db.VaccineCentres.Remove(vaccineCentre);
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
