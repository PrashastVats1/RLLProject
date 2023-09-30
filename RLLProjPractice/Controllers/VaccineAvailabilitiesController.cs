using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RLLProjPractice.Models;

namespace RLLProjPractice.Controllers
{
    public class VaccineAvailabilitiesController : Controller
    {
        private VaccineMgmtEntities db = new VaccineMgmtEntities();

        // GET: VaccineAvailabilities
        public ActionResult Index()
        {
            var vaccineAvailabilities = db.VaccineAvailabilities.Include(v => v.Vaccine);
            return View(vaccineAvailabilities.ToList());
        }

        // GET: VaccineAvailabilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccineAvailability vaccineAvailability = db.VaccineAvailabilities.Find(id);
            if (vaccineAvailability == null)
            {
                return HttpNotFound();
            }
            return View(vaccineAvailability);
        }

        // GET: VaccineAvailabilities/Create
        public ActionResult Create()
        {
            ViewBag.VaccineID = new SelectList(db.Vaccines, "VaccineID", "VaccineName");
            return View();
        }

        // POST: VaccineAvailabilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AvailabilityID,VaccineID,DateAvailable,QuantityAvailable")] VaccineAvailability vaccineAvailability)
        {
            if (ModelState.IsValid)
            {
                db.VaccineAvailabilities.Add(vaccineAvailability);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VaccineID = new SelectList(db.Vaccines, "VaccineID", "VaccineName", vaccineAvailability.VaccineID);
            return View(vaccineAvailability);
        }

        // GET: VaccineAvailabilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccineAvailability vaccineAvailability = db.VaccineAvailabilities.Find(id);
            if (vaccineAvailability == null)
            {
                return HttpNotFound();
            }
            ViewBag.VaccineID = new SelectList(db.Vaccines, "VaccineID", "VaccineName", vaccineAvailability.VaccineID);
            return View(vaccineAvailability);
        }

        // POST: VaccineAvailabilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AvailabilityID,VaccineID,DateAvailable,QuantityAvailable")] VaccineAvailability vaccineAvailability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaccineAvailability).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VaccineID = new SelectList(db.Vaccines, "VaccineID", "VaccineName", vaccineAvailability.VaccineID);
            return View(vaccineAvailability);
        }

        // GET: VaccineAvailabilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccineAvailability vaccineAvailability = db.VaccineAvailabilities.Find(id);
            if (vaccineAvailability == null)
            {
                return HttpNotFound();
            }
            return View(vaccineAvailability);
        }

        // POST: VaccineAvailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VaccineAvailability vaccineAvailability = db.VaccineAvailabilities.Find(id);
            db.VaccineAvailabilities.Remove(vaccineAvailability);
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
