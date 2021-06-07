using System;
using System.Web;
using System.Linq;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

using MovieArchiveTemplate.Models;
using MovieArchiveTemplate.Models.HelperModels;
using MovieArchiveTemplate.Models.ViewModels;

namespace MovieArchiveTemplate.Repositories
{
   public class UyelikRepository 
    {
        #region Değiskenler

        MovieArchiveDB.MovieIdentityContext db = new MovieArchiveDB.MovieIdentityContext();

        UserManager<ApplicationUser> UserManager { get; set; }
        IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }

        }

        #endregion

        #region Constructor
        public UyelikRepository()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new MovieArchiveDB.MovieIdentityContext()));

        }
        #endregion

        #region Kullanıcı İşlemleri

        public NResult<string> KullaniciEkle(string kullaniciAd, string rol)
        {
            //girişler boş bırakılamaz kontrolü... !!!

            if (string.IsNullOrEmpty(kullaniciAd))
                return new NResult<string> { Message = "Lütfen kişinin kullanıcı adını belirtiniz" };
            if (string.IsNullOrEmpty(rol))
                return new NResult<string> { Message = "Lütfen kişinin rolünü belirtiniz" };
            try
            {
                //aynı kullanıcı var mı kontrolü... !!!

                var kontrolKullanici = UserManager.FindByNameAsync(kullaniciAd);
                if (kontrolKullanici != null)
                    return new NResult<string> { Message = "Bu kişi sistemde kayıtlıdır" };

                //Kullanıcıyı ekle
                ApplicationUser kullanici = new ApplicationUser
                {
                    UserName = kullaniciAd
                };
                db.Users.Add(kullanici);
                db.SaveChanges();

                UserManager.AddPassword(kullanici.Id, kullaniciAd);

                //Kullanıcıya rol ekle
                var rolEklemeSonuc = UserManager.AddToRole(kullanici.Id, rol);
                if (rolEklemeSonuc.Succeeded)
                    return new NResult<string> { IsSuccessful = true };
                else
                    return new NResult<string> { IsSuccessful = false, Message = "Kullanıcıya rol tanımlaması yapılamadı" };
            }
            catch (Exception hata)
            {
                return new NResult<string> { Message = hata.ToString() };
            }
        }

        public NResult<string> KullaniciRoleEkle(string kullaniciAd, string rol)
        {
            if (string.IsNullOrEmpty(kullaniciAd))
                return new NResult<string> { Message = "Lütfen kişinin kullanıcı adını belirtiniz" };
            if (string.IsNullOrEmpty(rol))
                return new NResult<string> { Message = "Lütfen kişinin rolünü belirtiniz" };
            try
            {
                var kullanici = UserManager.FindByName(kullaniciAd);
                var sonuc = UserManager.AddToRole(kullanici.Id, rol);
                if (sonuc.Succeeded)
                    return new NResult<string> { IsSuccessful = true };
                else
                    return new NResult<string> { Message = sonuc.Errors.ToString() };
            }
            catch (Exception hata)
            {
                return new NResult<string> { Message = hata.ToString() };
            }

        }

        public NResult<NSession> GirisYap(string kullaniciAd, string sifre, MemberType tip)
        {
            
            NSession session = new NSession();
            if (tip == MemberType.User)
            {
                UserRepository uyeRepository = new UserRepository(false);
                var uye = uyeRepository.GetUser(kullaniciAd);
                if (!uye.IsSuccessful)
                    return new NResult<NSession> { Message = "Lütfen öğrenci bilgilerini kontrol ediniz" };

                session.ID = uye.Data.UserID;
              
                // session.KategoriID = uye.Veri.KategoriID;
            }

         
            else if (tip == MemberType.Admin)//Bilgi işlem kullanıcısı öğrenci veya öğretim görevlisi olamaz
            {
                UserRepository ogrenciRepository = new UserRepository(false);
                var uye = ogrenciRepository.GetUser(kullaniciAd);
                if (uye.IsSuccessful)
                    return new NResult<NSession> { Message = "Lütfen üye bilgilerini kontrol ediniz" };

              
            }

            var kullanici = UserManager.Find(kullaniciAd, sifre);
            if (kullanici != null)
            {
                AuthenticationManager.SignOut();
                var kimlik = UserManager.CreateIdentity(kullanici, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties(), kimlik);
                return new NResult<NSession> { IsSuccessful = true, Data = session };
            }
            else
            {
                return new NResult<NSession> { Message = "Kullanıcı adı ve şifrenizi kontrol ediniz." };
            }
        }
   
        public NResult CikisYap()
        {
            try
            {
                AuthenticationManager.SignOut();
                return new NResult { IsSuccessful = true };
            }
            catch (Exception hata)
            {
                return new NResult { Message = hata.ToString() };
            }
        }
       
        public NResult<string> GirisYapanKullanici()
        {
            try
            {
                bool kullaniciGirisYaptiMi = AuthenticationManager.User.Identity.IsAuthenticated;
                if (kullaniciGirisYaptiMi)
                {
                    return new NResult<string> { IsSuccessful = true, Data = AuthenticationManager.User.Identity.Name };
                }
            }
            catch { }
            return new NResult<string>();
        }

        #endregion
    }
}
