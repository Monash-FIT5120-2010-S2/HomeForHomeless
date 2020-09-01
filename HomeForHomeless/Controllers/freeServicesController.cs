using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeForHomeless.Models;
using PagedList;
using HomeForHomeless.ViewModel;


namespace HomeForHomeless.Controllers
{
    public class freeServicesController : Controller
    {
        private HFH_dbEntities db = new HFH_dbEntities();

        // GET: freeServices
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {
            var results = from x in db.freeServices
                          select x;
            int pagesize = 9, pageindex = 1;
            FreeServiceList temp = new FreeServiceList();
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Showing data based on the search query string and the star rating selected from the dropdown.

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                results = results.Where(s => s.Name.Contains(searchString) || s.Phone.Contains(searchString) || s.Address.Contains(searchString) || s.Category.Contains(searchString) || s.Tram_routes.Contains(searchString) || s.Suburb.Contains(searchString) || s.Website.Contains(searchString));

            }
            else
            {
                results = results.Where(x => x.Suburb == "Melbourne");
            }

            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = results.ToList();
            temp.freeServices = list.ToPagedList(pageindex, pagesize);
            return View(temp);

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

        public ActionResult FindService(int? id)
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

    //    // GET: freeServices/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: freeServices/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create([Bind(Include = "service_Id,Name,Address,Suburb,Phone,Website,Tram_routes,Category,Longitude,Latitude")] freeService freeService)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.freeServices.Add(freeService);
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }

    //        return View(freeService);
    //    }

    //    // GET: freeServices/Edit/5
    //    public ActionResult Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        freeService freeService = db.freeServices.Find(id);
    //        if (freeService == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(freeService);
    //    }

    //    // POST: freeServices/Edit/5
    //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit([Bind(Include = "service_Id,Name,Address,Suburb,Phone,Website,Tram_routes,Category,Longitude,Latitude")] freeService freeService)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Entry(freeService).State = EntityState.Modified;
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        return View(freeService);
    //    }

    //    // GET: freeServices/Delete/5
    //    public ActionResult Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        freeService freeService = db.freeServices.Find(id);
    //        if (freeService == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(freeService);
    //    }

    //    // POST: freeServices/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        freeService freeService = db.freeServices.Find(id);
    //        db.freeServices.Remove(freeService);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    }
}
