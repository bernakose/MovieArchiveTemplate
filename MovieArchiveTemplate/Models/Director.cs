using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MovieArchiveTemplate.Models
{
    [Table("Director")]
    public partial class Director
    {
        public Director()
        {
            Movie = new HashSet<Movie>();
        }

        [Key]
        public int DirectorID { get; set; }

        [Required(ErrorMessage = "Y�netmen ismini giriniz !")]
        [MinLength(3, ErrorMessage = "L�tfen en az 3 harfli isim Giriniz !")]
        [MaxLength(30, ErrorMessage = "L�tfen en fazla 30 harfli �sim Giriniz !")]
        [Display(Name = "Y�netmen Ad�")]
        public string DirectorName { get; set; }

        public virtual ICollection<Movie> Movie { get; set; }
    }
}
