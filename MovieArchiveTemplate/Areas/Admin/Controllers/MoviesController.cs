using PagedList;
using MovieArchiveTemplate.Helpers;
using MovieArchiveTemplate.Models;
using MovieArchiveTemplate.Repositories;
using MovieArchiveTemplate.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace MovieArchiveTemplate.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Uye")]
    public class MoviesController : Controller
    {
        FilmRepository _repository = new FilmRepository();
        MovieArchiveDB db = new MovieArchiveDB();

        public ActionResult Index(int? page, string sorting)
        {
            var movies = _repository.GetirTumu();
            if (!movies.IsSuccessful)
                return View();


            #region [SIRALAMA]
            // ------------------- SIRALAMA --------------------

            List<Movie> contentMovies = movies.Data;

            // Burada controller çağrılırken "?sorting=xxx_ifade" şeklinde parametre gelir.
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = "ReleaseDateDesc";
            }
            ViewBag.ReleaseDateSortParam = sorting == "ReleaseDateAsc" ? "ReleaseDateDesc" : "ReleaseDateAsc";
            ViewBag.RaitingSortParam = sorting == "RaitingDesc" ? "RaitingAsc" : "RaitingDesc";


            switch (sorting)
            {
                case "ReleaseDateAsc":
                    contentMovies = contentMovies.OrderBy(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateDesc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "ReleaseDateDesc":
                    contentMovies = contentMovies.OrderByDescending(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateAsc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                case "RaitingAsc":
                    contentMovies = contentMovies.OrderBy(m => m.Raiting).ToList();
                    ViewBag.CurrentSortParam = "RaitingDesc";
                    ViewBag.CurrentDDLText = "Raiting";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "RaitingDesc":
                    contentMovies = contentMovies.OrderByDescending(m => m.Raiting).ToList();
                    ViewBag.CurrentSortParam = "RaitingAsc";
                    ViewBag.CurrentDDLText = "Raiting";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                default:
                    contentMovies = contentMovies.OrderByDescending(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateAsc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
            }
            // ------------------- SIRALAMA --------------------
            #endregion


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View("Index", contentMovies.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Details(int movieID)
        {
            var movieWrapper = _repository.Getir(movieID);
            if (!movieWrapper.IsSuccessful)
                return RedirectToAction("Index", "Movies");

            var movie = movieWrapper.Data;
            var movieDetail = new NMovieDetails()
            {
                Movie = new Movie()
                {
                    Title = movie.Title,
                    Description = movie.Description,
                    Poster = movie.Poster,
                    ReleaseDate = movie.ReleaseDate,
                    ReleaseCountry = movie.ReleaseCountry,
                    TrailerLink = movie.TrailerLink,
                    Raiting = movie.Raiting,
                    Budget = movie.Budget
                },
                Directors = new DirectorRepository().GetDirector(movieID).Data,
                Actors = new ActorRepository().GetActor(movieID).Data,
                Genres = new KategoriTipRepository().GetGenre(movieID).Data
            };

            return View(movieDetail);
        }


        public ActionResult Create()
        {
            var genreList = new KategoriTipRepository().GetirTumu();
            var directorList = new DirectorRepository().GetAllDirector();
            var actorList = new ActorRepository().GetAllActor();

            #region [HATA KONTROL]
            if (!genreList.IsSuccessful)
            {
                ModelState.AddModelError("", genreList.Message);
            }
            if (!directorList.IsSuccessful)
            {
                ModelState.AddModelError("", directorList.Message);
            }
            if (!actorList.IsSuccessful)
            {
                ModelState.AddModelError("", actorList.Message);
            }
            if (!(genreList.IsSuccessful && directorList.IsSuccessful && actorList.IsSuccessful))
            {
                return Create();
            }
            #endregion


            MultiSelectList genreSelectList = new MultiSelectList(genreList.Data, "GenreID", "GenreName");
            MultiSelectList directorSelectList = new MultiSelectList(directorList.Data, "DirectorID", "DirectorName");
            MultiSelectList actorSelectList = new MultiSelectList(actorList.Data, "ActorID", "ActorName");

            ViewData["GenreList"] = genreSelectList;
            ViewData["DirectorList"] = directorSelectList;
            ViewData["ActorList"] = actorSelectList;


            return View();
        }

        [HttpPost]
        public ActionResult Create(Movie movie, HttpPostedFileBase file, FormCollection formColl)
        {
            if (!ModelState.IsValid)
            {
                return Create();
            }


            if (file != null && file.ContentLength > 0)
            {
                MemoryStream memoryStream = file.InputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    file.InputStream.CopyTo(memoryStream);
                }
                movie.Poster = memoryStream.ToArray();
            }

            var islemSonuc = _repository.Kaydet(movie);
            if (!islemSonuc.IsSuccessful)
            {
                ModelState.AddModelError("", islemSonuc.Message);

                return Create();
            }

            MovieArchiveDB db = new MovieArchiveDB();
            var tempSelectedGenres = (formColl["GenreList"] as string).Split(',').ToList();
            var tempSelectedDirectors = (formColl["DirectorList"] as string).Split(',').ToList();
            var tempSelectedActors = (formColl["ActorList"] as string).Split(',').ToList();

            foreach (var item in tempSelectedGenres)
            {
                var tempGenreID = Convert.ToInt32(item);
                var tempMovie = db.Movie.Where(m => m.MovieID == islemSonuc.Data).FirstOrDefault();
                var tempGenre = db.Genre.Where(g => g.GenreID == tempGenreID).FirstOrDefault();
                tempGenre.Movie.Add(tempMovie);
                tempMovie.Genre.Add(tempGenre);
            }

            foreach (var item in tempSelectedDirectors)
            {
                var tempDirectorID = Convert.ToInt32(item);
                var tempMovie = db.Movie.Where(m => m.MovieID == islemSonuc.Data).FirstOrDefault();
                var tempDirector = db.Director.Where(d => d.DirectorID == tempDirectorID).FirstOrDefault();
                tempDirector.Movie.Add(tempMovie);
                tempMovie.Director.Add(tempDirector);
            }

            foreach (var item in tempSelectedActors)
            {
                var tempActorID = Convert.ToInt32(item);
                var tempMovie = db.Movie.Where(m => m.MovieID == islemSonuc.Data).FirstOrDefault();
                var tempActor = db.Actor.Where(a => a.ActorID == tempActorID).FirstOrDefault();
                tempActor.Movie.Add(tempMovie);
                tempMovie.Actor.Add(tempActor);
            }

            db.SaveChanges();


            return RedirectToAction("Index", "Movies");
        }


        public ActionResult Edit(int movieID)
        {
            MovieArchiveDB db = new MovieArchiveDB();

            var movieWrapper = _repository.Getir(movieID);
            var genreList = new KategoriTipRepository().GetirTumu();
            var directorList = new DirectorRepository().GetAllDirector();
            var actorList = new ActorRepository().GetAllActor();

            #region [HATA KONTROL]
            if (!movieWrapper.IsSuccessful)
            {
                ModelState.AddModelError("", movieWrapper.Message);
            }
            if (!genreList.IsSuccessful)
            {
                ModelState.AddModelError("", genreList.Message);
            }
            if (!directorList.IsSuccessful)
            {
                ModelState.AddModelError("", directorList.Message);
            }
            if (!actorList.IsSuccessful)
            {
                ModelState.AddModelError("", actorList.Message);
            }
            if (!(movieWrapper.IsSuccessful && genreList.IsSuccessful && directorList.IsSuccessful && actorList.IsSuccessful))
            {
                return Create();
            }
            #endregion


            MultiSelectList genreSelectList = new MultiSelectList(genreList.Data, "GenreID", "GenreName");
            MultiSelectList directorSelectList = new MultiSelectList(directorList.Data, "DirectorID", "DirectorName");
            MultiSelectList actorSelectList = new MultiSelectList(actorList.Data, "ActorID", "ActorName");

            ViewData["GenreList"] = genreSelectList;
            ViewData["DirectorList"] = directorSelectList;
            ViewData["ActorList"] = actorSelectList;

            return View(movieWrapper.Data);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie, HttpPostedFileBase file, FormCollection formColl)
        {
            if (!ModelState.IsValid)
            {
                return Create();
            }


            if (file != null && file.ContentLength > 0)
            {
                MemoryStream memoryStream = file.InputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    file.InputStream.CopyTo(memoryStream);
                }
                movie.Poster = memoryStream.ToArray();
            }
            MovieArchiveDB db = new MovieArchiveDB();


            var tempSelectedGenres = (formColl["GenreList"] as string).Split(',').ToList();
            var tempSelectedDirectors = (formColl["DirectorList"] as string).Split(',').ToList();
            var tempSelectedActors = (formColl["ActorList"] as string).Split(',').ToList();


            var delMovie = db.Movie.Where(m => m.MovieID == movie.MovieID).FirstOrDefault();
            foreach (var item in db.Genre.ToList())
            {
                item.Movie.Remove(delMovie);
            }
            delMovie.Genre.Clear();

            foreach (var item in tempSelectedGenres)
            {
                var tempGenreID = Convert.ToInt32(item);
                var tempMovie = db.Movie.Where(m => m.MovieID == movie.MovieID).FirstOrDefault();
                var tempGenre = db.Genre.Where(g => g.GenreID == tempGenreID).FirstOrDefault();
                tempGenre.Movie.Add(tempMovie);
                tempMovie.Genre.Add(tempGenre);
            }



            var delDirec = db.Movie.Where(m => m.MovieID == movie.MovieID).FirstOrDefault();
            foreach (var item in db.Director.ToList())
            {
                item.Movie.Remove(delDirec);
            }
            delDirec.Genre.Clear();

            foreach (var item in tempSelectedDirectors)
            {
                var tempDirectorID = Convert.ToInt32(item);
                var tempMovie = db.Movie.Where(m => m.MovieID == movie.MovieID).FirstOrDefault();
                var tempDirector = db.Director.Where(d => d.DirectorID == tempDirectorID).FirstOrDefault();
                tempDirector.Movie.Add(tempMovie);
                tempMovie.Director.Add(tempDirector);
            }



            var delActor = db.Movie.Where(m => m.MovieID == movie.MovieID).FirstOrDefault();
            foreach (var item in db.Actor.ToList())
            {
                item.Movie.Remove(delActor);
            }
            delActor.Genre.Clear();

            foreach (var item in tempSelectedActors)
            {
                var tempActorID = Convert.ToInt32(item);
                var tempMovie = db.Movie.Where(m => m.MovieID == movie.MovieID).FirstOrDefault();
                var tempActor = db.Actor.Where(a => a.ActorID == tempActorID).FirstOrDefault();
                tempActor.Movie.Add(tempMovie);
                tempMovie.Actor.Add(tempActor);
            }

            db.SaveChanges();


            return RedirectToAction("Index", "Movies");
        }


        public ActionResult Delete(int? movieID)
        {
            Movie tempMovie = db.Movie.Find(movieID);

            try
            {
                foreach (var item in db.Genre.ToList())
                {
                    item.Movie.Remove(tempMovie);
                }
                foreach (var item in db.Director.ToList())
                {
                    item.Movie.Remove(tempMovie);
                }
                foreach (var item in db.Actor.ToList())
                {
                    item.Movie.Remove(tempMovie);
                }
                tempMovie.Genre.Clear();
                tempMovie.Director.Clear();
                tempMovie.Actor.Clear();
                db.Movie.Remove(tempMovie);
                db.SaveChanges();
            }
            catch (Exception) { return new HttpStatusCodeResult(HttpStatusCode.NotModified); }

            return RedirectToAction("Index", "Movies");
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