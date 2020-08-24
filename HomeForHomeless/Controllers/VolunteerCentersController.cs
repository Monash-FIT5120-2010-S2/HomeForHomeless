using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeForHomeless.Models;

namespace HomeForHomeless.Controllers
{
    public class VolunteerCentersController : Controller
    {
        private HFH_dbEntities db = new HFH_dbEntities();

        // GET: VolunteerCenters
        public ActionResult Index()
        {
            return View(db.VolunteerCenters.ToList());
        }

        // GET: VolunteerCenters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerCenter volunteerCenter = db.VolunteerCenters.Find(id);
            if (volunteerCenter == null)
            {
                return HttpNotFound();
            }
            return View(volunteerCenter);
        }

        // GET: VolunteerCenters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VolunteerCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VolunteerCenter_Id,Country,Name,Address,Suburb,Postcode,State,Business_Category,LGA,Region")] VolunteerCenter volunteerCenter)
        {
            if (ModelState.IsValid)
            {
                db.VolunteerCenters.Add(volunteerCenter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(volunteerCenter);
        }

        // GET: VolunteerCenters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerCenter volunteerCenter = db.VolunteerCenters.Find(id);
            if (volunteerCenter == null)
            {
                return HttpNotFound();
            }
            return View(volunteerCenter);
        }

        // POST: VolunteerCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VolunteerCenter_Id,Country,Name,Address,Suburb,Postcode,State,Business_Category,LGA,Region")] VolunteerCenter volunteerCenter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteerCenter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volunteerCenter);
        }

        // GET: VolunteerCenters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VolunteerCenter volunteerCenter = db.VolunteerCenters.Find(id);
            if (volunteerCenter == null)
            {
                return HttpNotFound();
            }
            return View(volunteerCenter);
        }

        // POST: VolunteerCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VolunteerCenter volunteerCenter = db.VolunteerCenters.Find(id);
            db.VolunteerCenters.Remove(volunteerCenter);
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
