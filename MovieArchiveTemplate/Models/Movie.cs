using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MovieArchiveTemplate.Models
{
    [Table("Movie")]
    public partial class Movie
    {
        public Movie()
        {
            Actor = new HashSet<Actor>();
            Director = new HashSet<Director>();
            Genre = new HashSet<Genre>();
        }

        [Key]
        public int MovieID { get; set; }

        [Required(ErrorMessage = "L�tfen Film Ad�n� Giriniz !")]
        [Display(Name = "Film Ad�")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Konu giriniz !")]
        [MinLength(20, ErrorMessage = "L�tfen Film Konusunu Giriniz !")]
        [Display(Name = "Film Konusu")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        //[Required(ErrorMessage = "L�tfen Film Foto�raf�n� Y�kleyiniz !")]
        [Column(TypeName = "image")]
        [Display(Name = "Film Foto")]
        public byte[] Poster { get; set; }

        [Required(ErrorMessage = "L�tfen Filmin ��k�� Tarihini Giriniz !")]
        [Display(Name = "��k�� Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "L�tfen Filmin ��k�� Yerini Giriniz !")]
        [Display(Name = "Film Yay�nlanma �ehri")]
        public string ReleaseCountry { get; set; }

        [Required(ErrorMessage = "L�tfen Filmin Fragman Linkini Giriniz !")]
        [Display(Name = "Fragman")]
        [DataType(DataType.Url)]
        public string TrailerLink { get; set; }

        [Required(ErrorMessage = "L�tfen Film Puan�n� Giriniz !")]
        [Display(Name = "Film Puan�")]
        public double Raiting { get; set; }

        [Required(ErrorMessage = "L�tfen Filmin B�t�esini Giriniz !")]
        [Display(Name = "Film B�t�e")]
        public string Budget { get; set; }

        public virtual ICollection<Actor> Actor { get; set; }

        public virtual ICollection<Director> Director { get; set; }

        public virtual ICollection<Genre> Genre { get; set; }
    }
}
