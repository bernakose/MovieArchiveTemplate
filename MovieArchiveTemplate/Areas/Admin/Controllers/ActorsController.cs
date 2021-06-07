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
    public class ActorsController : Controller
    {
        private MovieArchiveDB db = new MovieArchiveDB();

        // GET: Admin/Actors
        public ActionResult Index(int? page, string sorting)
        {
            var actorList = db.Actor.ToList();


            #region [SIRALAMA]
            // ------------------- SIRALAMA --------------------

            // Burada controller çağrılırken "?sorting=xxx_ifade" şeklinde parametre gelir.
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = "ActorNameDesc";
            }
            ViewBag.ActorNameSortParam = sorting == "ActorNameAsc" ? "ActorNameDesc" : "ActorNameAsc";


            switch (sorting)
            {
                case "ActorNameAsc":
                    actorList = actorList.OrderBy(m => m.ActorName).ToList();
                    ViewBag.CurrentSortParam = "ActorNameDesc";
                    ViewBag.CurrentDDLText = "Actor Name";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "ActorNameDesc":
                    actorList = actorList.OrderByDescending(m => m.ActorName).ToList();
                    ViewBag.CurrentSortParam = "ActorNameAsc";
                    ViewBag.CurrentDDLText = "Actor Name";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                default:
                    actorList = actorList.OrderByDescending(m => m.ActorName).ToList();
                    ViewBag.CurrentSortParam = "ActorNameAsc";
                    ViewBag.CurrentDDLText = "Actor Name";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
            }
            // ------------------- SIRALAMA --------------------
            #endregion

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View("Index", actorList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Actors/Details?actorID=id
        public ActionResult Details(int? actorID)
        {
            if (actorID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actor.Find(actorID);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // GET: Admin/Actors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Actors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Actor actor)
        {
            if (ModelState.IsValid)
            {
                db.Actor.Add(actor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actor);
        }

        // GET: Admin/Actors/Edit?actorID=id
        public ActionResult Edit(int? actorID)
        {
            if (actorID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actor.Find(actorID);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: Admin/Actors/Edit?actorID=id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Actor actor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Actors");
            }
            return View(actor);
        }

        // GET: Admin/Actors/Delete?actorID=id
        public ActionResult Delete(int? actorID)
        {
            if (actorID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actor.Find(actorID);
            if (actor == null)
            {
                return HttpNotFound();
            }
            db.Actor.Remove(actor);
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
