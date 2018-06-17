﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorManagement.Models;
using PagedList;

namespace DoctorManagement.Controllers
{
    public class DoctorsController : Controller
    {
        private DoctorDatabaseEntities db = new DoctorDatabaseEntities();

        // GET: Doctors
        public ViewResult Index(string sortOrder, string searchString, string searchString1, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;



            var doctors
                = from s in db.Doctor
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.DoctorName.Contains(searchString) );
            }
            if (!String.IsNullOrEmpty(searchString1))
            {
                doctors = doctors.Where(s => s.Speciality.Contains(searchString1));
            }
            switch (sortOrder)
            {
                case "name_desc":
                doctors= doctors.OrderByDescending(s => s.DoctorName);
                    break;
            
                default:
                    doctors = doctors.OrderBy(s => s.DoctorName);
                    break;

            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(doctors.ToPagedList(pageNumber,pageSize));
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            ViewBag.ID_Hospital = new SelectList(db.Hospital, "ID", "HospitalName");
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorName,Speciality,ID_Hospital")] Doctor doctor)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Doctor.Add(doctor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            

         
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Hospital = new SelectList(db.Hospital, "ID", "HospitalName", doctor.ID_Hospital);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DoctorName,Speciality,ID_Hospital")] Doctor doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(doctor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("","Unable to save changes.");
            }
            
            
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again.";
            }
            Doctor doctor = db.Doctor.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Doctor doctor = db.Doctor.Find(id);
                db.Doctor.Remove(doctor);
                db.SaveChanges();
            }
            catch(DataException)
            {
                return RedirectToAction("Delete", new { id, saveChagesError = true });
            }
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
