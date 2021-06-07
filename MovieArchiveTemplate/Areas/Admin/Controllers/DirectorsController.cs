using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieArchiveTemplate.Models;
using MovieArchiveTemplate.Repositories;

namespace MovieArchiveTemplate.Areas.Admin.Controllers
{
    public class DirectorsController : Controller
    {
        private MovieArchiveDB db = new MovieArchiveDB();

        // GET: Admin/Directors
        public ActionResult Index(int? page, string sorting)
        {
            var directorList = db.Director.ToList();


            #region [SIRALAMA]
            // ------------------- SIRALAMA --------------------

            // Burada controller çağrılırken "?sorting=xxx_ifade" şeklinde parametre gelir.
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = "DirectorNameDesc";
            }
            ViewBag.DirectorNameSortParam = sorting == "DirectorNameAsc" ? "DirectorNameDesc" : "DirectorNameAsc";


            switch (sorting)
            {
                case "DirectorNameAsc":
                    directorList = directorList.OrderBy(m => m.DirectorName).ToList();
                    ViewBag.CurrentSortParam = "DirectorNameDesc";
                    ViewBag.CurrentDDLText = "Director Name";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "DirectorNameDesc":
                    directorList = directorList.OrderByDescending(m => m.DirectorName).ToList();
                    ViewBag.CurrentSortParam = "DirectorNameAsc";
                    ViewBag.CurrentDDLText = "Director Name";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                default:
                    directorList = directorList.OrderByDescending(m => m.DirectorName).ToList();
                    ViewBag.CurrentSortParam = "DirectorNameAsc";
                    ViewBag.CurrentDDLText = "Director Name";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
            }
            // ------------------- SIRALAMA --------------------
            #endregion

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View("Index", directorList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Directors/Details?directorID=id
        public ActionResult Details(int? directorID)
        {
            if (directorID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = db.Director.Find(directorID);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        // GET: Admin/Directors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Directors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Director director)
        {
            if (ModelState.IsValid)
            {
                db.Director.Add(director);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(director);
        }

        // GET: Admin/Directors/Edit?directorID=id
        public ActionResult Edit(int? directorID)
        {
            if (directorID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = db.Director.Find(directorID);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        // POST: Admin/Directors/Edit?directorID=id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Director director)
        {
            if (ModelState.IsValid)
            {
                db.Entry(director).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(director);
        }

        // GET: Admin/Directors/Delete?directorID=id
        public ActionResult Delete(int? directorID)
        {
            if (directorID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = db.Director.Find(directorID);
            if (director == null)
            {
                return HttpNotFound();
            }
            db.Director.Remove(director);
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
