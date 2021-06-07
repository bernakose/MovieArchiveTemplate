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
        [Display(Name = "Kullanýcý Foto")]
        public byte[] ProfilPicture { get; set; }

        [Required(ErrorMessage = "Kullanýcý ismi giriniz !")]
        [MinLength(3, ErrorMessage = "Lütfen en az 3 harfli isim Giriniz !")]
        [MaxLength(20, ErrorMessage = "Lütfen en fazla 20 harfli Ýsim Giriniz !")]
        [Display(Name = "Kullanýcý Adý")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Kullanýcý soyadýný giriniz !")]
        [MinLength(3, ErrorMessage = "Lütfen SoyAdýnýzý Giriniz !")]
        [MaxLength(10, ErrorMessage = "Lütfen SoyAdýnýzý Giriniz !")]
        [Display(Name = "Soy Adýnýz")]
        public string LName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Geçerli bir E-Posta giriniz !")]
        [Display(Name = "E-Posta Adresi")]
        public string EMail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Þifre")]
        public string Password { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Lütfen Telefon Numaranýzý Giriniz !")]
        [MaxLength(14, ErrorMessage = "Lütfen Telefon Numaranýzý Giriniz !")]
        [Display(Name = "Telefon Numaranýz")]
        public string Telephone { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Kayýt Tarihi")]
        public DateTime RegisterDate { get; set; }

        [Display(Name = "Kullanýcý Türü")]
        public MemberType MemberType { get; set; }
    }
}
