using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeForHomeless.Models;
using HomeForHomeless.ViewModel;
using PagedList;

namespace HomeForHomeless.Controllers
{

    public class VolunteerCentersController : Controller
    {
        private HFH_dbEntities db = new HFH_dbEntities();

        // GET: VolunteerCenters
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
            var results = from x in db.VolunteerCenters
                          select x;
            int pagesize = 9, pageindex = 1;
            VCList temp = new VCList();
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
                switch (category)
                {
                    case 1:
                        results = results.Where(s => (s.Name.Contains(searchString) || s.Suburb.Contains(searchString))
                && s.Business_Category.Contains("Disability")
                );
                        break;
                    case 2:
                        results = results.Where(s => (s.Name.Contains(searchString) || s.Suburb.Contains(searchString))
                    && s.Business_Category.Contains("Recreation Group")
                );
                        break;
                    case 3:
                        results = results.Where(s => (s.Name.Contains(searchString) || s.Suburb.Contains(searchString))
                    && s.Business_Category.Contains("State body")
                );
                        break;
                    case 4:
                        results = results.Where(s => (s.Name.Contains(searchString) || s.Suburb.Contains(searchString))
                    && s.Business_Category.Contains("Volunteering")
                );
                        break;
                    case 5:
                        results = results.Where(s => (s.Name.Contains(searchString) || s.Suburb.Contains(searchString))
                    && s.Business_Category.Contains("Walking Club/Group")
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
                        results = results.Where(s => s.Business_Category.Contains("Disability"));
                        break;
                    case 2:
                        results = results.Where(s => s.Business_Category.Contains("Recreation Group"));
                        break;
                    case 3:
                        results = results.Where(s => s.Business_Category.Contains("State body"));
                        break;
                    case 4:
                        results = results.Where(s => s.Business_Category.Contains("Volunteering"));
                        break;
                    case 5:
                        results = results.Where(s => s.Business_Category.Contains("Walking Club/Group"));
                        break;
                }
            }
            else
            {
                results = from x in db.VolunteerCenters
                          select x;
            }

            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = results.ToList();
            temp.VCs = list.ToPagedList(pageindex, pagesize);

            List<SelectListItem> Category_list = new List<SelectListItem>();
            Category_list.Add(new SelectListItem() { Text = "All Categories", Value = "-1" });
            Category_list.Add(new SelectListItem() { Text = "Disability", Value = "1" });
            Category_list.Add(new SelectListItem() { Text = "Recreation Group", Value = "2" });
            Category_list.Add(new SelectListItem() { Text = "State body", Value = "3" });
            Category_list.Add(new SelectListItem() { Text = "Volunteering", Value = "4" });
            Category_list.Add(new SelectListItem() { Text = "Walking Club/Group", Value = "5" });
            this.ViewBag.Category = new SelectList(Category_list, "Value", "Text", currentCategory);
            return View(temp);
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

        //// GET: VolunteerCenters/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: VolunteerCenters/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "VolunteerCenter_Id,Country,Name,Address,Suburb,Postcode,State,Business_Category,LGA,Region")] VolunteerCenter volunteerCenter)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.VolunteerCenters.Add(volunteerCenter);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(volunteerCenter);
        //}

        //// GET: VolunteerCenters/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VolunteerCenter volunteerCenter = db.VolunteerCenters.Find(id);
        //    if (volunteerCenter == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(volunteerCenter);
        //}

        //// POST: VolunteerCenters/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "VolunteerCenter_Id,Country,Name,Address,Suburb,Postcode,State,Business_Category,LGA,Region")] VolunteerCenter volunteerCenter)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(volunteerCenter).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(volunteerCenter);
        //}

        //// GET: VolunteerCenters/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VolunteerCenter volunteerCenter = db.VolunteerCenters.Find(id);
        //    if (volunteerCenter == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(volunteerCenter);
        //}

        //// POST: VolunteerCenters/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    VolunteerCenter volunteerCenter = db.VolunteerCenters.Find(id);
        //    db.VolunteerCenters.Remove(volunteerCenter);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
