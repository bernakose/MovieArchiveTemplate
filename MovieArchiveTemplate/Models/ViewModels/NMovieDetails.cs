using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieArchiveTemplate.Models.ViewModels
{
    //public class NMovieDetails
    //{
    //    public string Title { get; set; }

    //    public string Description { get; set; }

    //    public byte[] Poster { get; set; }

    //    public DateTime ReleaseDate { get; set; }

    //    public string ReleaseCountry { get; set; }

    //    public string TrailerLink { get; set; }

    //    public double Raiting { get; set; }

    //    public string Budget { get; set; }


    //    public List<Director> Directors { get; set; }

    //    public List<Actor> Actors { get; set; }

    //    public List<Genre> Genres { get; set; }

    //    public List<Category> Categories { get; set; }
    //}

    public class NMovieDetails
    {
        public Movie Movie { get; set; }

        [Display(Name = "Yönetmen")]
        public List<Director> Directors { get; set; }

        [Display(Name = "Aktörler")]
        public List<Actor> Actors { get; set; }

        [Display(Name = "Tür")]
        public List<Genre> Genres { get; set; }
    }
}