using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MovieArchiveTemplate.Models
{
    [Table("Genre")]
    public partial class Genre
    {
        public Genre()
        {
            Movie = new HashSet<Movie>();
        }

        [Key]
        public int GenreID { get; set; }

        [Required(ErrorMessage = "L�tfen Film T�r� Giriniz !")]
        [Display(Name = "Film T�r�")]
        public string GenreName { get; set; }

        public virtual ICollection<Movie> Movie { get; set; }
    }
}
