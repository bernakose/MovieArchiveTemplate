using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using MovieArchiveTemplate.Models;
using MovieArchiveTemplate.Models.HelperModels;
namespace MovieArchiveTemplate.Repositories
{
    public class KategoriTipRepository : BaseRepository
    {
        public NResult<List<Genre>> GetirTumu()
        {
            try
            {
                return new NResult<List<Genre>>
                {
                    IsSuccessful = true,
                    Data = movieArchiveDB.Genre.ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<Genre>> { Message = hata.Message }; }

        }

        public NResult<Genre> Getir(int id)
        {
            try
            {
                var kategoritipler = (from b in movieArchiveDB.Genre
                                      where b.GenreID == id
                                select b);
                if (kategoritipler.Count() > 0)
                {
                    return new NResult<Genre>
                    {
                        IsSuccessful = true,
                        Data = kategoritipler.FirstOrDefault()
                    };
                }
                else
                {
                    return new NResult<Genre>();
                }
            }
            catch (Exception hata) { return new NResult<Genre> { Message = hata.Message }; }
        }

        public NResult<int> Kaydet(Genre kayit)
        {
            try
            {
                movieArchiveDB.Genre.Add(kayit);
                movieArchiveDB.SaveChanges();
                return new NResult<int>
                {
                    IsSuccessful = true,
                    Data = kayit.GenreID
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

        public NResult Guncelle(Genre kayit)
        {
            try
            {
                var duzenlenecekKayitlar = movieArchiveDB.Genre.Where(b => b.GenreID == kayit.GenreID);
                if (duzenlenecekKayitlar.Count() > 0)
                {
                    var duzenlenecekKayit = duzenlenecekKayitlar.FirstOrDefault();

                    duzenlenecekKayit.GenreName = kayit.GenreName;

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
                var silinecekKayitlar = movieArchiveDB.Genre.Where(b => b.GenreID == id);
                if (silinecekKayitlar.Count() > 0)
                {
                    var silinecekKayit = silinecekKayitlar.FirstOrDefault();
                    movieArchiveDB.Genre.Remove(silinecekKayit);
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

        //tüm kategoritip bilgilerini slectlist den alıp dropdownliste göndermke için !!!

        public NResult<SelectList> GetirTumu_SelectList()
        {
            var bolumler = (from f in movieArchiveDB.Genre
                            orderby f.GenreName
                            select new SelectListItem
                            {
                                Text = f.GenreName,
                                Value = f.GenreID.ToString()
                            }).ToList();

            return new NResult<SelectList>
            {
                IsSuccessful = true,
                Data = new SelectList(bolumler, "Value", "Text")
            };
        }

        public NResult<List<Genre>> GetGenre(int movieID)
        {
            try
            {
                return new NResult<List<Genre>>
                {
                    IsSuccessful = true,
                    Data = movieArchiveDB.Genre.Where(d => d.Movie.Any(m => m.MovieID == movieID)).OrderBy(o => o.GenreName).ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<Genre>> { Message = hata.Message }; }
        }
    }
}
