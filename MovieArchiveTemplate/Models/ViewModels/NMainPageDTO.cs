using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieArchiveTemplate.Models.ViewModels
{
    public class NMainPageDTO
    {
        public List<byte[]> Posters { get; set; }

        public List<Genre> Categories { get; set; }

        public List<Movie> FutureMovies { get; set; }

        public List<Movie> RandomMovies { get; set; }
    }
}