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

        [Required(ErrorMessage = "Lütfen Film Adýný Giriniz !")]
        [Display(Name = "Film Adý")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Konu giriniz !")]
        [MinLength(20, ErrorMessage = "Lütfen Film Konusunu Giriniz !")]
        [Display(Name = "Film Konusu")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Lütfen Film Fotoðrafýný Yükleyiniz !")]
        [Column(TypeName = "image")]
        [Display(Name = "Film Foto")]
        public byte[] Poster { get; set; }

        [Required(ErrorMessage = "Lütfen Filmin Çýkýþ Tarihini Giriniz !")]
        [Display(Name = "Çýkýþ Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Lütfen Filmin Çýkýþ Yerini Giriniz !")]
        [Display(Name = "Film Yayýnlanma Þehri")]
        public string ReleaseCountry { get; set; }

        [Required(ErrorMessage = "Lütfen Filmin Fragman Linkini Giriniz !")]
        [Display(Name = "Fragman")]
        [DataType(DataType.Url)]
        public string TrailerLink { get; set; }

        [Required(ErrorMessage = "Lütfen Film Puanýný Giriniz !")]
        [Display(Name = "Film Puaný")]
        public double Raiting { get; set; }

        [Required(ErrorMessage = "Lütfen Filmin Bütçesini Giriniz !")]
        [Display(Name = "Film Bütçe")]
        public string Budget { get; set; }

        public virtual ICollection<Actor> Actor { get; set; }

        public virtual ICollection<Director> Director { get; set; }

        public virtual ICollection<Genre> Genre { get; set; }
    }
}
