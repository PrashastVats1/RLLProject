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
    public class MembersController : Controller
    {
        private OnlineVaccineEntities db = new OnlineVaccineEntities();

        // GET: Members
        public ActionResult Index()
        {
            var members = db.Members.Include(m => m.Login_Users).Include(m => m.VaccineCentre).Include(m => m.Slot).Include(m => m.VaccineCentre1).Include(m => m.Slot1);
            return View(members.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            ViewBag.RefID = new SelectList(db.Login_Users, "UserId", "EmailID");
            ViewBag.Dose1CentreID = new SelectList(db.VaccineCentres, "CentreID", "Hospital_Name");
            ViewBag.Dose1SlotCentreID = new SelectList(db.Slots, "CentreID", "CentreID");
            ViewBag.Dose2CentreID = new SelectList(db.VaccineCentres, "CentreID", "Hospital_Name");
            ViewBag.Dose2SlotCentreID = new SelectList(db.Slots, "CentreID", "CentreID");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AadharNumber,RefID,PhoneNumber,Name,DOB,Age,Gender,Dose1Status,Dose2Status,Dose1CentreID,Dose2CentreID,Dose1Data,Dose2Data,Dose1SlotCentreID,Dose1SlotDate,Dose2SlotCentreID,Dose2SlotDate,Dose1VaccineName,Dose2VaccineName")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefID = new SelectList(db.Login_Users, "UserId", "EmailID", member.RefID);
            ViewBag.Dose1CentreID = new SelectList(db.VaccineCentres, "CentreID", "Hospital_Name", member.Dose1CentreID);
            ViewBag.Dose1SlotCentreID = new SelectList(db.Slots, "CentreID", "CentreID", member.Dose1SlotCentreID);
            ViewBag.Dose2CentreID = new SelectList(db.VaccineCentres, "CentreID", "Hospital_Name", member.Dose2CentreID);
            ViewBag.Dose2SlotCentreID = new SelectList(db.Slots, "CentreID", "CentreID", member.Dose2SlotCentreID);
            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefID = new SelectList(db.Login_Users, "UserId", "EmailID", member.RefID);
            ViewBag.Dose1CentreID = new SelectList(db.VaccineCentres, "CentreID", "Hospital_Name", member.Dose1CentreID);
            ViewBag.Dose1SlotCentreID = new SelectList(db.Slots, "CentreID", "CentreID", member.Dose1SlotCentreID);
            ViewBag.Dose2CentreID = new SelectList(db.VaccineCentres, "CentreID", "Hospital_Name", member.Dose2CentreID);
            ViewBag.Dose2SlotCentreID = new SelectList(db.Slots, "CentreID", "CentreID", member.Dose2SlotCentreID);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AadharNumber,RefID,PhoneNumber,Name,DOB,Age,Gender,Dose1Status,Dose2Status,Dose1CentreID,Dose2CentreID,Dose1Data,Dose2Data,Dose1SlotCentreID,Dose1SlotDate,Dose2SlotCentreID,Dose2SlotDate,Dose1VaccineName,Dose2VaccineName")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefID = new SelectList(db.Login_Users, "UserId", "EmailID", member.RefID);
            ViewBag.Dose1CentreID = new SelectList(db.VaccineCentres, "CentreID", "Hospital_Name", member.Dose1CentreID);
            ViewBag.Dose1SlotCentreID = new SelectList(db.Slots, "CentreID", "CentreID", member.Dose1SlotCentreID);
            ViewBag.Dose2CentreID = new SelectList(db.VaccineCentres, "CentreID", "Hospital_Name", member.Dose2CentreID);
            ViewBag.Dose2SlotCentreID = new SelectList(db.Slots, "CentreID", "CentreID", member.Dose2SlotCentreID);
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
