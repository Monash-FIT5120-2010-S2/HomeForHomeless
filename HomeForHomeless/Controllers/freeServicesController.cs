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
        public ActionResult Index(int? page, string searchString, string currentFilter, string Category, string currentCategory)
        {
            decimal category;
            if (!String.IsNullOrEmpty(Category))
            {
                category = decimal.Parse(Category);
            }
            else
            if (!String.IsNullOrEmpty(currentCategory))
            {
                category = decimal.Parse(currentCategory);
            }
            else
            {
                category = -1;
            }
            var results = from x in db.freeServices
                          select x;
            int pagesize = 9, pageindex = 1;
            FreeServiceList temp = new FreeServiceList();
            if (searchString != null || Category != null)
            {
                page = 1;
            }
            else
            {
                Category = currentCategory;
                searchString = currentFilter;
            }

            // Showing data based on the search query string and the star rating selected from the dropdown.

            ViewData["CurrentFilter"] = searchString;
            ViewData["currentCategory"] = Category;


            if (!String.IsNullOrEmpty(searchString) && category != -1)
            {
                switch(category)
                {
                    case 1: results = results.Where(s => (s.Name.Contains(searchString) || s.Suburb.Contains(searchString))
                    && (s.Category.Equals("Accommodation")||s.Category.Equals("Clothes and Blankets") || s.Category.Equals("Food")
                    || s.Category.Equals("Showers / Laundry") || s.Category.Equals("Tenancy Assistance"))
                    );
                        break;
                    case 2:
                        results = results.Where(s => (s.Name.Contains(searchString) || s.Suburb.Contains(searchString))
                    && s.Category.Equals("Employment Assistance") 
                );
                        break;
                    case 3:
                        results = results.Where(s => (s.Name.Contains(searchString) || s.Suburb.Contains(searchString))
                   && (s.Category.Equals("Counselling and Psychiatric Services") || s.Category.Equals("Drug and Alcohol") || s.Category.Equals("Health Services / Pharmacy")
                    || s.Category.Equals("Hospitals / Emergency") || s.Category.Equals("Needle Exchange"))
                );
                        break;
                    case 4:
                        results = results.Where(s => (s.Name.Contains(searchString) || s.Suburb.Contains(searchString))
                && (s.Category.Equals("Helpful phone number") || s.Category.Equals("Legal / Financial Advice") || s.Category.Equals("Travel Assistance"))
                );
                        break;
                }
            }
            else
            if (!String.IsNullOrEmpty(searchString) && category == -1)
            {
                results = results.Where(s => s.Name.Contains(searchString) || s.Suburb.Contains(searchString));
            }
            else
            if (String.IsNullOrEmpty(searchString) && category != -1)
            {
                switch (category)
                {
                    case 1:
                        results = results.Where(s => s.Category.Equals("Accommodation") || s.Category.Equals("Clothes and Blankets") || s.Category.Equals("Food")
                    || s.Category.Equals("Showers / Laundry") || s.Category.Equals("Tenancy Assistance"));
                        break;
                    case 2:
                        results = results.Where(s => s.Category.Equals("Employment Assistance"));
                        break;
                    case 3:
                        results = results.Where(s => s.Category.Equals("Counselling and Psychiatric Services") || s.Category.Equals("Drug and Alcohol") || s.Category.Equals("Health Services / Pharmacy")
                    || s.Category.Equals("Hospitals / Emergency") || s.Category.Equals("Needle Exchange"));
                        break;
                    case 4:
                        results = results.Where(s => s.Category.Equals("Helpful phone number") || s.Category.Equals("Legal / Financial Advice") || s.Category.Equals("Travel Assistance"));
                        break;
                }
            }
            else
            {
                results = from x in db.freeServices
                          select x;
            }

            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = results.ToList();
            temp.freeServices = list.ToPagedList(pageindex, pagesize);

            //adding the values in dropdown list
            List<SelectListItem> Category_list = new List<SelectListItem>();
            Category_list.Add(new SelectListItem() { Text = "All Categories", Value = "-1" });
            Category_list.Add(new SelectListItem() { Text = "Basic Necessities", Value = "1" });
            Category_list.Add(new SelectListItem() { Text = "Employment Assistance", Value = "2" });
            Category_list.Add(new SelectListItem() { Text = "Health Services", Value = "3" });
            Category_list.Add(new SelectListItem() { Text = "Other Services", Value = "4" });
            this.ViewBag.Category = new SelectList(Category_list, "Value", "Text", currentCategory);

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
