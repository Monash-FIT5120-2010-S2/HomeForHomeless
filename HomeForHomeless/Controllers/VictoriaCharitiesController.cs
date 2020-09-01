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
    public class VictoriaCharitiesController : Controller
    {
        private HFH_dbEntities db = new HFH_dbEntities();

        // GET: VictoriaCharities
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {
            var results = from x in db.VictoriaCharities
                          select x;
            int pagesize = 9, pageindex = 1;
            CharityList temp = new CharityList();
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
                results = results.Where(s => s.charity_name.Contains(searchString) || s.Registration_Status.Contains(searchString) || s.address_line_1.Contains(searchString) || s.address_line_1.Contains(searchString) || s.town_city.Contains(searchString) || s.state.Contains(searchString) || s.country.Contains(searchString) || s.postcode.ToString().Contains(searchString));

            }
            else
            {
                results = results.Where(x => x.state == "VIC");
            }

            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = results.ToList();
            temp.Charities = list.ToPagedList(pageindex, pagesize);
            return View(temp);

        }

        // GET: VictoriaCharities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VictoriaCharity victoriaCharity = db.VictoriaCharities.Find(id);
            if (victoriaCharity == null)
            {
                return HttpNotFound();
            }
            return View(victoriaCharity);
        }

        //// GET: VictoriaCharities/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: VictoriaCharities/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "charity_Id,charity_name,Registration_Status,address_line_1,address_line_2,town_city,state,postcode,country,charity_size,main_activity,main_beneficiaries")] VictoriaCharity victoriaCharity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.VictoriaCharities.Add(victoriaCharity);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(victoriaCharity);
        //}

        //// GET: VictoriaCharities/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VictoriaCharity victoriaCharity = db.VictoriaCharities.Find(id);
        //    if (victoriaCharity == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(victoriaCharity);
        //}

        //// POST: VictoriaCharities/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "charity_Id,charity_name,Registration_Status,address_line_1,address_line_2,town_city,state,postcode,country,charity_size,main_activity,main_beneficiaries")] VictoriaCharity victoriaCharity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(victoriaCharity).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(victoriaCharity);
        //}

        //// GET: VictoriaCharities/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VictoriaCharity victoriaCharity = db.VictoriaCharities.Find(id);
        //    if (victoriaCharity == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(victoriaCharity);
        //}

        //// POST: VictoriaCharities/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    VictoriaCharity victoriaCharity = db.VictoriaCharities.Find(id);
        //    db.VictoriaCharities.Remove(victoriaCharity);
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
