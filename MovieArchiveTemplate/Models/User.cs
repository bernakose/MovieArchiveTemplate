using MovieArchiveTemplate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace MovieArchiveTemplate.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
        }

        [Key]
        public int UserID { get; set; }

        [Column(TypeName = "image")]
        [Display(Name = "Kullan�c� Foto")]
        public byte[] ProfilPicture { get; set; }

        [Required(ErrorMessage = "Kullan�c� ismi giriniz !")]
        [MinLength(3, ErrorMessage = "L�tfen en az 3 harfli isim Giriniz !")]
        [MaxLength(20, ErrorMessage = "L�tfen en fazla 20 harfli �sim Giriniz !")]
        [Display(Name = "Kullan�c� Ad�")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Kullan�c� soyad�n� giriniz !")]
        [MinLength(3, ErrorMessage = "L�tfen SoyAd�n�z� Giriniz !")]
        [MaxLength(10, ErrorMessage = "L�tfen SoyAd�n�z� Giriniz !")]
        [Display(Name = "Soy Ad�n�z")]
        public string LName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Ge�erli bir E-Posta giriniz !")]
        [Display(Name = "E-Posta Adresi")]
        public string EMail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "�ifre")]
        public string Password { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "L�tfen Telefon Numaran�z� Giriniz !")]
        [MaxLength(14, ErrorMessage = "L�tfen Telefon Numaran�z� Giriniz !")]
        [Display(Name = "Telefon Numaran�z")]
        public string Telephone { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Kay�t Tarihi")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "Kullan�c� T�r�")]
        public MemberType MemberType { get; set; }
    }
}
