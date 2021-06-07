using MovieArchiveTemplate.Models.ViewModels;
using MovieArchiveTemplate.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieArchiveTemplate.Controllers
{
    public class MoviesController : Controller
    {
        MovieArchiveDB db = new MovieArchiveDB();

        public ActionResult MoviesThisWeek(int? page, string sorting)
        {
            var contentMovies = db.Movie.OrderByDescending(m => m.ReleaseDate).ToList();
            NMainPageDTO obj = new NMainPageDTO();
            obj.Categories = db.Genre.OrderBy(c => c.GenreName).ToList();
            obj.FutureMovies = db.Movie.OrderByDescending(m => (m.ReleaseDate >= DateTime.Now)).Take(3).ToList();
            obj.RandomMovies = (from result in contentMovies.AsEnumerable() orderby Guid.NewGuid() select result).Take(3).ToList();

            ViewBag.MainPageDTO = obj;

            #region [SIRALAMA]
            // ------------------- SIRALAMA --------------------

            // Burada controller çağrılırken "?sorting=xxx_ifade" şeklinde parametre gelir.
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = "ReleaseDateDesc";
            }
            ViewBag.ReleaseDateSortParam = sorting == "ReleaseDateAsc" ? "ReleaseDateDesc" : "ReleaseDateAsc";
            ViewBag.RaitingSortParam = sorting == "RaitingDesc" ? "RaitingAsc" : "RaitingDesc";

            // Gelen sıralama ifadesine göre sıralama yapılır.
            DateTime temp = DateTime.Now.AddDays(-7);
            switch (sorting)
            {
                case "ReleaseDateAsc":
                    contentMovies = db.Movie.Where(m => (m.ReleaseDate >= temp && m.ReleaseDate <= DateTime.Now)).OrderBy(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateDesc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "ReleaseDateDesc":
                    contentMovies = db.Movie.Where(m => (m.ReleaseDate >= temp && m.ReleaseDate <= DateTime.Now)).OrderByDescending(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateAsc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                case "RaitingAsc":
                    contentMovies = db.Movie.Where(m => (m.ReleaseDate >= temp && m.ReleaseDate <= DateTime.Now)).OrderBy(m => m.Raiting).ToList();
                    ViewBag.CurrentSortParam = "RaitingDesc";
                    ViewBag.CurrentDDLText = "Raiting";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "RaitingDesc":
                    contentMovies = db.Movie.Where(m => (m.ReleaseDate >= temp && m.ReleaseDate <= DateTime.Now)).OrderByDescending(m => m.Raiting).ToList();
                    ViewBag.CurrentSortParam = "RaitingAsc";
                    ViewBag.CurrentDDLText = "Raiting";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                default:
                    contentMovies = db.Movie.Where(m => (m.ReleaseDate >= temp && m.ReleaseDate <= DateTime.Now)).OrderByDescending(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateAsc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
            }
            // ------------------- SIRALAMA --------------------
            #endregion


            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View("MoviesThisWeek", contentMovies.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult MoviesComingSoon(int? page, string sorting)
        {
            var contentMovies = db.Movie.Where(m => m.ReleaseDate >= DateTime.Now).ToList();
            NMainPageDTO obj = new NMainPageDTO();
            obj.Categories = db.Genre.OrderBy(c => c.GenreName).ToList();
            obj.FutureMovies = db.Movie.Where(m => (m.ReleaseDate >= DateTime.Now)).Take(3).ToList();
            obj.RandomMovies = (from result in contentMovies.AsEnumerable() orderby Guid.NewGuid() select result).Take(3).ToList();

            ViewBag.MainPageDTO = obj;

            #region [SIRALAMA]
            // ------------------- SIRALAMA --------------------

            // Burada controller çağrılırken "?sorting=xxx_ifade" şeklinde parametre gelir.
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = "ReleaseDateDesc";
            }
            ViewBag.ReleaseDateSortParam = sorting == "ReleaseDateAsc" ? "ReleaseDateDesc" : "ReleaseDateAsc";
            ViewBag.RaitingSortParam = sorting == "RaitingDesc" ? "RaitingAsc" : "RaitingDesc";

            // Gelen sıralama ifadesine göre sıralama yapılır.
            switch (sorting)
            {
                case "ReleaseDateAsc":
                    contentMovies = db.Movie.Where(m => (m.ReleaseDate >= DateTime.Now)).OrderBy(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateDesc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "ReleaseDateDesc":
                    contentMovies = db.Movie.Where(m => (m.ReleaseDate >= DateTime.Now)).OrderByDescending(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateAsc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                case "RaitingAsc":
                    contentMovies = db.Movie.Where(m => (m.ReleaseDate >= DateTime.Now)).OrderBy(m => m.Raiting).ToList();
                    ViewBag.CurrentSortParam = "RaitingDesc";
                    ViewBag.CurrentDDLText = "Raiting";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "RaitingDesc":
                    contentMovies = db.Movie.Where(m => (m.ReleaseDate >= DateTime.Now)).OrderByDescending(m => m.Raiting).ToList();
                    ViewBag.CurrentSortParam = "RaitingAsc";
                    ViewBag.CurrentDDLText = "Raiting";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                default:
                    contentMovies = db.Movie.Where(m => (m.ReleaseDate >= DateTime.Now)).OrderByDescending(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateAsc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
            }
            // ------------------- SIRALAMA --------------------
            #endregion

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View("MoviesComingSoon", contentMovies.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult MovieDetails(int movieID)
        {
            var contentMovies = db.Movie.Where(m => m.ReleaseDate >= DateTime.Now).ToList();
            NMainPageDTO obj = new NMainPageDTO();
            obj.Categories = db.Genre.OrderBy(c => c.GenreName).ToList();
            obj.FutureMovies = db.Movie.Where(m => (m.ReleaseDate >= DateTime.Now)).Take(3).ToList();
            obj.RandomMovies = (from result in contentMovies.AsEnumerable() orderby Guid.NewGuid() select result).Take(3).ToList();

            ViewBag.MainPageDTO = obj;

            var movie = db.Movie.Where(m => m.MovieID == movieID).FirstOrDefault();

            return View(movie);
        }


        public ActionResult MovieList(int genreID, int? page, string sorting)
        {
            var movies = db.Movie.Where(m => m.ReleaseDate >= DateTime.Now).ToList();
            NMainPageDTO obj = new NMainPageDTO();
            obj.Categories = db.Genre.OrderBy(c => c.GenreName).ToList();
            obj.FutureMovies = db.Movie.Where(m => (m.ReleaseDate >= DateTime.Now)).Take(3).ToList();
            obj.RandomMovies = (from result in movies.AsEnumerable() orderby Guid.NewGuid() select result).Take(3).ToList();

            ViewBag.MainPageDTO = obj;

            var tempGenre = db.Genre.Where(g => g.GenreID == genreID).First();
            ViewBag.Title = tempGenre.GenreName;

            var contentMovies = tempGenre.Movie.ToList();
            #region [SIRALAMA]
            // ------------------- SIRALAMA --------------------

            // Burada controller çağrılırken "?sorting=xxx_ifade" şeklinde parametre gelir.
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = "ReleaseDateDesc";
            }
            ViewBag.ReleaseDateSortParam = sorting == "ReleaseDateAsc" ? "ReleaseDateDesc" : "ReleaseDateAsc";
            ViewBag.RaitingSortParam = sorting == "RaitingDesc" ? "RaitingAsc" : "RaitingDesc";

            // Gelen sıralama ifadesine göre sıralama yapılır.
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

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View("MovieList", contentMovies.ToPagedList(pageNumber, pageSize));
        }
    }
}