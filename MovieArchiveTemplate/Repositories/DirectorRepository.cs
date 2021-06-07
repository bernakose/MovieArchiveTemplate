using MovieArchiveTemplate.Models;
using MovieArchiveTemplate.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieArchiveTemplate.Repositories
{
    public class DirectorRepository : BaseRepository
    {
        public DirectorRepository() : base() { }

        public NResult<List<Director>> GetAllDirector()
        {
            try
            {
                return new NResult<List<Director>>
                {
                    IsSuccessful = true,
                    Data = movieArchiveDB.Director.OrderBy(d => d.DirectorName).ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<Director>> { Message = hata.Message }; }
        }

        public NResult<List<Director>> GetDirector(int movieID)
        {
            try
            {
                return new NResult<List<Director>>
                {
                    IsSuccessful = true,
                    Data = movieArchiveDB.Director.Where(d => d.Movie.Any(m => m.MovieID == movieID)).OrderBy(o => o.DirectorName).ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<Director>> { Message = hata.Message }; }
        }
    }
}