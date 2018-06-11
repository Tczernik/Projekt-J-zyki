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
    public class MedicalExaminationsController : Controller
    {
        private DoctorDatabaseEntities db = new DoctorDatabaseEntities();

        // GET: MedicalExaminations
        public ActionResult Index()
        {
            var medicalExaminations = db.MedicalExaminations.Include(m => m.Patient);
            return View(medicalExaminations.ToList());
        }

        // GET: MedicalExaminations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalExaminations medicalExaminations = db.MedicalExaminations.Find(id);
            if (medicalExaminations == null)
            {
                return HttpNotFound();
            }
            return View(medicalExaminations);
        }

        // GET: MedicalExaminations/Create
        public ActionResult Create()
        {
            ViewBag.ID_Patient = new SelectList(db.Patient, "ID", "PatientName");
            return View();
        }

        // POST: MedicalExaminations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Examinations,ID_Patient")] MedicalExaminations medicalExaminations)
        {
            if (ModelState.IsValid)
            {
                db.MedicalExaminations.Add(medicalExaminations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Patient = new SelectList(db.Patient, "ID", "PatientName", medicalExaminations.ID_Patient);
            return View(medicalExaminations);
        }

        // GET: MedicalExaminations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalExaminations medicalExaminations = db.MedicalExaminations.Find(id);
            if (medicalExaminations == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Patient = new SelectList(db.Patient, "ID", "PatientName", medicalExaminations.ID_Patient);
            return View(medicalExaminations);
        }

        // POST: MedicalExaminations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Examinations,ID_Patient")] MedicalExaminations medicalExaminations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalExaminations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Patient = new SelectList(db.Patient, "ID", "PatientName", medicalExaminations.ID_Patient);
            return View(medicalExaminations);
        }

        // GET: MedicalExaminations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalExaminations medicalExaminations = db.MedicalExaminations.Find(id);
            if (medicalExaminations == null)
            {
                return HttpNotFound();
            }
            return View(medicalExaminations);
        }

        // POST: MedicalExaminations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalExaminations medicalExaminations = db.MedicalExaminations.Find(id);
            db.MedicalExaminations.Remove(medicalExaminations);
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
