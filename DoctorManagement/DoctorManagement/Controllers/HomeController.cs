using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoctorManagement.Models;
using DoctorManagement.ViewModel;


namespace DoctorManagement.Controllers
{
    public class HomeController : Controller
    {

        private DoctorDatabaseEntities db = new DoctorDatabaseEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<StatisticDateGroup> data =
                from patient in db.Patient
                group patient by patient.Date into dateGroup
                select new StatisticDateGroup()
                {
                    Date = dateGroup.Key,
                    PatientCount = dateGroup.Count()
                };
            return View(data.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}