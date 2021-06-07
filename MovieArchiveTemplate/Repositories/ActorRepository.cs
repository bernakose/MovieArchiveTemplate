using MovieArchiveTemplate.Models;
using MovieArchiveTemplate.Models.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieArchiveTemplate.Repositories
{
    public class ActorRepository : BaseRepository
    {
        public ActorRepository() : base() { }

        public NResult<List<Actor>> GetAllActor()
        {
            try
            {
                return new NResult<List<Actor>>
                {
                    IsSuccessful = true,
                    Data = movieArchiveDB.Actor.OrderBy(d => d.ActorName).ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<Actor>> { Message = hata.Message }; }
        }

        public NResult<List<Actor>> GetActor(int movieID)
        {
            try
            {
                return new NResult<List<Actor>>
                {
                    IsSuccessful = true,
                    Data = movieArchiveDB.Actor.Where(d => d.Movie.Any(m => m.MovieID == movieID)).OrderBy(o => o.ActorName).ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<Actor>> { Message = hata.Message }; }
        }
    }
}