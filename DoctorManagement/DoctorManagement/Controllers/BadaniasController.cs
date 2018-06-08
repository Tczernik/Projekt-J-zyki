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
    public class BadaniasController : Controller
    {
        private DoctorDatabaseEntities db = new DoctorDatabaseEntities();

        // GET: Badanias
        public ActionResult Index()
        {
            var badania = db.Badania.Include(b => b.Patient);
            return View(badania.ToList());
        }

        // GET: Badanias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Badania badania = db.Badania.Find(id);
            if (badania == null)
            {
                return HttpNotFound();
            }
            return View(badania);
        }

        // GET: Badanias/Create
        public ActionResult Create()
        {
            ViewBag.ID_Patient = new SelectList(db.Patient, "ID", "PatientName");
            return View();
        }

        // POST: Badanias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Badania1,ID_Patient")] Badania badania)
        {
            if (ModelState.IsValid)
            {
                db.Badania.Add(badania);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Patient = new SelectList(db.Patient, "ID", "PatientName", badania.ID_Patient);
            return View(badania);
        }

        // GET: Badanias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Badania badania = db.Badania.Find(id);
            if (badania == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Patient = new SelectList(db.Patient, "ID", "PatientName", badania.ID_Patient);
            return View(badania);
        }

        // POST: Badanias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Badania1,ID_Patient")] Badania badania)
        {
            if (ModelState.IsValid)
            {
                db.Entry(badania).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Patient = new SelectList(db.Patient, "ID", "PatientName", badania.ID_Patient);
            return View(badania);
        }

        // GET: Badanias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Badania badania = db.Badania.Find(id);
            if (badania == null)
            {
                return HttpNotFound();
            }
            return View(badania);
        }

        // POST: Badanias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Badania badania = db.Badania.Find(id);
            db.Badania.Remove(badania);
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
