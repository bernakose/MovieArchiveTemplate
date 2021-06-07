using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieArchiveTemplate.Models;
using MovieArchiveTemplate.Models.HelperModels;
using System.Web.Mvc;
using MovieArchiveTemplate.Models.ViewModels;


namespace MovieArchiveTemplate.Repositories
{
    public class FilmRepository : BaseRepository
    {
        public FilmRepository() : base() { }
     
        public NResult<List<Movie>> GetirTumu()
        {
            try
            {
                return new NResult<List<Movie>>
                {
                    IsSuccessful = true,
                    Data = movieArchiveDB.Movie.OrderBy(o => o.Title).ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<Movie>> { Message = hata.Message }; }

        }
      
        //kategoritipıd ye göre filmleri getir !!!
        public NResult<List<Movie>> GetirFilmler(int kategoritipID)
        {
            try
            {
                return new NResult<List<Movie>>
                {
                    IsSuccessful = true,
                    Data = movieArchiveDB.Movie.Where(d => d.Genre.Any(c => c.GenreID == kategoritipID)).OrderBy(o => o.Title).ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<Movie>> { Message = hata.Message }; }
        }

        public NResult<Movie> Getir(int id)
        {
            try
            {
                var filmler = (from d in movieArchiveDB.Movie
                                where d.MovieID== id
                                select d);
                if (filmler.Count() > 0)
                {
                    return new NResult<Movie>
                    {
                        IsSuccessful = true,
                        Data = filmler.FirstOrDefault()
                    };
                }
                else
                {
                    return new NResult<Movie>();
                }
            }
            catch (Exception hata) { return new NResult<Movie> { Message = hata.Message }; }
        }

        public NResult<int> Kaydet(Movie kayit)
        {
            try
            {
                movieArchiveDB.Movie.Add(kayit);
                movieArchiveDB.SaveChanges();
                
                return new NResult<int>
                {
                    IsSuccessful = true,
                    Data = kayit.MovieID
                };
            }
            catch (Exception hata)
            {
                return new NResult<int>()
                {
                    IsSuccessful = false,
                    Message = hata.Message
                };
            }
        }

        public NResult Guncelle(Movie kayit)
        {
            try
            {
                var duzenlenecekKayitlar = movieArchiveDB.Movie.Where(d => d.MovieID == kayit.MovieID);
                if (duzenlenecekKayitlar.Count() > 0)
                {
                    var duzenlenecekKayit = duzenlenecekKayitlar.FirstOrDefault();
                    duzenlenecekKayit.Title = kayit.Title;
                    duzenlenecekKayit.Description = kayit.Description;
                    duzenlenecekKayit.TrailerLink = kayit.TrailerLink;
                    duzenlenecekKayit.Raiting = kayit.Raiting;
                    duzenlenecekKayit.ReleaseDate = kayit.ReleaseDate;
                    duzenlenecekKayit.Poster = kayit.Poster;

                    movieArchiveDB.SaveChanges();
                    return new NResult { IsSuccessful = true };
                }
                else
                {
                    return new NResult
                    {
                        IsSuccessful = false,
                        Message = "Kayıt bulunamadı"
                    };
                }
            }
            catch (Exception hata)
            {
                return new NResult
                {
                    IsSuccessful = false,
                    Message = hata.Message
                };
            }
        }
   
        public NResult Sil(int id)
        {
            try
            {
                var silinecekKayitlar = movieArchiveDB.Movie.Where(d => d.MovieID == id);
                if (silinecekKayitlar.Count() > 0)
                {
                    var silinecekKayit = silinecekKayitlar.FirstOrDefault();
                    movieArchiveDB.Movie.Remove(silinecekKayit);
                    movieArchiveDB.SaveChanges();
                    return new NResult { IsSuccessful = true };
                }
                else
                {
                    return new NResult
                    {
                        IsSuccessful = false,
                        Message = "Kayıt bulunamadı"
                    };
                }
            }
            catch (Exception hata)
            {
                return new NResult<int>()
                {
                    IsSuccessful = false,
                    Message = hata.Message
                };
            }
        }

        public NResult<SelectList> GetirTumu_SelectList()
        {
            var filmler = (from d in movieArchiveDB.Movie
                            orderby d.Title
                           
                            select new SelectListItem
                            {
                                Text = d.Title,
                                Value = d.MovieID.ToString()
                            }).ToList();

            return new NResult<SelectList>
            {
                IsSuccessful = true,
                Data = new SelectList(filmler, "Value", "Text")
            };
        }
  
        public NResult<List<NSelectListItem>> GetirTumu_SelectList(int seciliFilmID=0)
        {
            var filmler = (from f in movieArchiveDB.Movie
                                orderby f.Title
                                select new NSelectListItem
                                {
                                    Text = f.Title,
                                    Value = f.MovieID.ToString()
                                }).ToList();
            if (seciliFilmID > 0)
            {
                for (int i = 0; i < filmler.Count; i++)
                {
                    if (filmler[i].Value == seciliFilmID.ToString())
                    {
                        filmler[i].Selected = true;
                        break;
                    }
                }
            }

            return new NResult<List<NSelectListItem>>
            {
                IsSuccessful = true,
                Data = filmler
            };
        }
    }
}

