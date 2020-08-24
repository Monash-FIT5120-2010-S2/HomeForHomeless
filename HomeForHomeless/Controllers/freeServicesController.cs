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
    public class freeServicesController : Controller
    {
        private HFH_dbEntities db = new HFH_dbEntities();

        // GET: freeServices
        public ActionResult Index()
        {
            return View(db.freeServices.ToList());
        }

        // GET: freeServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            freeService freeService = db.freeServices.Find(id);
            if (freeService == null)
            {
                return HttpNotFound();
            }
            return View(freeService);
        }

        // GET: freeServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: freeServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "service_Id,Name,Address,Suburb,Phone,Website,Tram_routes,Category,Longitude,Latitude")] freeService freeService)
        {
            if (ModelState.IsValid)
            {
                db.freeServices.Add(freeService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(freeService);
        }

        // GET: freeServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            freeService freeService = db.freeServices.Find(id);
            if (freeService == null)
            {
                return HttpNotFound();
            }
            return View(freeService);
        }

        // POST: freeServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "service_Id,Name,Address,Suburb,Phone,Website,Tram_routes,Category,Longitude,Latitude")] freeService freeService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(freeService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(freeService);
        }

        // GET: freeServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            freeService freeService = db.freeServices.Find(id);
            if (freeService == null)
            {
                return HttpNotFound();
            }
            return View(freeService);
        }

        // POST: freeServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            freeService freeService = db.freeServices.Find(id);
            db.freeServices.Remove(freeService);
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
