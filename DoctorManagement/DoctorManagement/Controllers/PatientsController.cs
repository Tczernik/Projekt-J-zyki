using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorManagement.Models;

namespace DoctorManagement.Controllers
{
    public class PatientsController : Controller
    {
        private DoctorDatabaseEntities db = new DoctorDatabaseEntities();

        // GET: Patients
        public ActionResult Index()
        {
            var patient = db.Patient.Include(p => p.Doctor).Include(p => p.BloodGroup).Include(p => p.Hospital);
            return View(patient.ToList());
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            ViewBag.ID_Doctor = new SelectList(db.Doctor, "ID", "DoctorName");
            ViewBag.ID_Blood = new SelectList(db.BloodGroup, "ID", "BloodName");
            ViewBag.ID_Hospital = new SelectList(db.Hospital, "ID", "HospitalName");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PatientName,PatientSurname,SSN,Date,ID_Doctor,ID_Blood,ID_Hospital")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patient.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Doctor = new SelectList(db.Doctor, "ID", "DoctorName", patient.ID_Doctor);
            ViewBag.ID_Blood = new SelectList(db.BloodGroup, "ID", "BloodName", patient.ID_Blood);
            ViewBag.ID_Hospital = new SelectList(db.Hospital, "ID", "HospitalName", patient.ID_Hospital);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Doctor = new SelectList(db.Doctor, "ID", "DoctorName", patient.ID_Doctor);
            ViewBag.ID_Blood = new SelectList(db.BloodGroup, "ID", "BloodName", patient.ID_Blood);
            ViewBag.ID_Hospital = new SelectList(db.Hospital, "ID", "HospitalName", patient.ID_Hospital);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PatientName,PatientSurname,SSN,Date,ID_Doctor,ID_Blood,ID_Hospital")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Doctor = new SelectList(db.Doctor, "ID", "DoctorName", patient.ID_Doctor);
            ViewBag.ID_Blood = new SelectList(db.BloodGroup, "ID", "BloodName", patient.ID_Blood);
            ViewBag.ID_Hospital = new SelectList(db.Hospital, "ID", "HospitalName", patient.ID_Hospital);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patient.Find(id);
            db.Patient.Remove(patient);
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
