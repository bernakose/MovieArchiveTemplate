using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MovieArchiveTemplate.Models;
using MovieArchiveTemplate.Models.ViewModels;
using MovieArchiveTemplate.Repositories;
using System.Web.Mvc;

namespace MovieArchiveTemplate.Helpers
{
    public class SelectListHelper
    {
        #region Değişkenler

        private static KategoriTipRepository kategoritiprepository = new KategoriTipRepository();
        private static FilmRepository filmrepository = new FilmRepository();
                
        #endregion

        public static IEnumerable<SelectListItem> Kategoritipler
        {
            get
            {
                var kategoritipler = kategoritiprepository.GetirTumu_SelectList();
                if (kategoritipler.IsSuccessful)
                    return kategoritipler.Data;
                return new SelectList(null);
            }
        }
        
        public static IEnumerable<SelectListItem> Filmler
        {
            get
            {
                var donemler = filmrepository.GetirTumu_SelectList();
                if (donemler.IsSuccessful)
                    return donemler.Data;
                return new SelectList(null);

            
            }
        }

        
        public static List<NSelectListItem> GetirKategoriFilmleri(int kategoritipıd)
        {
            var dersler = filmrepository.GetirTumu_SelectList(kategoritipıd);
            if (dersler.IsSuccessful)
                return dersler.Data;
           
            return null;
        }


    }
}