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

        [Required(ErrorMessage = "Yönetmen ismini giriniz !")]
        [MinLength(3, ErrorMessage = "Lütfen en az 3 harfli isim Giriniz !")]
        [MaxLength(30, ErrorMessage = "Lütfen en fazla 30 harfli Ýsim Giriniz !")]
        [Display(Name = "Yönetmen Adý")]
        public string DirectorName { get; set; }

        public virtual ICollection<Movie> Movie { get; set; }
    }
}
