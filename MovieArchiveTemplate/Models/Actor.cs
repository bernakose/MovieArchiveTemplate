using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MovieArchiveTemplate.Models
{
    [Table("Actor")]
    public partial class Actor
    {
        public Actor()
        {
            Movie = new HashSet<Movie>();
        }

        [Key]
        public int ActorID { get; set; }

        [Required(ErrorMessage = "Akt�r ismini giriniz !")]
        [MinLength(3, ErrorMessage = "L�tfen en az 3 harfli isim Giriniz !")]
        [MaxLength(30, ErrorMessage = "L�tfen en fazla 30 harfli �sim Giriniz !")]
        [Display(Name = "Akt�r Ad�")]
        public string ActorName { get; set; }

        public virtual ICollection<Movie> Movie { get; set; }
    }
}
