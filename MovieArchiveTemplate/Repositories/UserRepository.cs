using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieArchiveTemplate.Models;
using MovieArchiveTemplate.Models.HelperModels;
using MovieArchiveTemplate.Models.ViewModels;

namespace MovieArchiveTemplate.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(){ }

        public NResult<List<User>> GetirTumu()
        {
            try
            {
                return new NResult<List<User>>
                {
                    IsSuccessful = true,
                    Data = movieArchiveDB.User.OrderBy(o => o.FName).ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<User>> { IsSuccessful = false, Message = hata.Message }; }
        }
    
        //public NIslemSonuc<List<Kullanici>> GetirUyeler(int filmıd)
        //{
        //    try
        //    {
        //        return new NIslemSonuc<List<Kullanici>>
        //        {
        //            BasariliMi = true,
        //            Veri = veritabani.Kullanicilar.Where(o => o.FilmID == filmıd).OrderBy(o => o.KullaniciAdi).ToList()
        //        };
        //    }
        //    catch (Exception hata) { return new NIslemSonuc<List<Kullanici>> { Mesaj = hata.Message }; }
        //}

        public NResult<User> GetUser(int userID)
        {
            try
            {
                var users = (from u in movieArchiveDB.User
                            where u.UserID == userID
                            orderby u.FName, u.LName
                            select u);
                if (users.Count() > 0)
                {
                    return new NResult<User>
                    {
                        IsSuccessful = true,
                        Data = users.FirstOrDefault()
                    };
                }
                else
                {
                    return new NResult<User>() { IsSuccessful = false, Message = "Böyle bir kullanıcı bulunmamaktadır." };
                }
            }
            catch (Exception hata) { return new NResult<User> { IsSuccessful = false, Message = hata.Message }; }
        }

        public NResult<User> GetUser(string EMail)
        {
            try
            {
                var users = (from u in movieArchiveDB.User
                            where u.EMail == EMail
                            orderby u.FName, u.LName
                            select u);
                if (users.Count() > 0)
                {
                    return new NResult<User>
                    {
                        IsSuccessful = true,
                        Data = users.FirstOrDefault()
                    };
                }
                else
                {
                    return new NResult<User>() { IsSuccessful = false, Message = "Böyle bir kullanıcı bulunmamaktadır." };
                }
            }
            catch (Exception hata) { return new NResult<User> { IsSuccessful = false, Message = hata.Message }; }
        }

        public NResult<User> GetUser(string EMail, string Password)
        {
            try
            {
                var users = (from u in movieArchiveDB.User
                             where u.EMail == EMail && u.Password == Password
                             orderby u.FName, u.LName
                             select u);
                if (users.Count() > 0)
                {
                    return new NResult<User>
                    {
                        IsSuccessful = true,
                        Data = users.FirstOrDefault()
                    };
                }
                else
                {
                    return new NResult<User>() { IsSuccessful = false, Message = "Böyle bir kullanıcı bulunmamaktadır." };
                }
            }
            catch (Exception hata) { return new NResult<User> { IsSuccessful = false, Message = hata.Message }; }
        }


        public NResult SaveUser(User newUser)
        {
            try
            {
                movieArchiveDB.User.Add(newUser);
                movieArchiveDB.SaveChanges();

                return new NResult() { IsSuccessful = true };
            }
            catch (Exception hata)
            {
                return new NResult()
                {
                    IsSuccessful = false,
                    Message = hata.Message
                };
            }
        }

        public NResult UpdateUser(User newUser)
        {
            try
            {
                var updateUserDatas = movieArchiveDB.User.Where(o => o.UserID == newUser.UserID);
                if (updateUserDatas.Count() > 0)
                {
                    var updateUser = updateUserDatas.FirstOrDefault();
                    updateUser.FName = newUser.FName;
                    updateUser.LName = newUser.LName;
                    updateUser.Password = newUser.Password;
                    updateUser.ProfilPicture = newUser.ProfilPicture;
                    updateUser.RegisterDate = newUser.RegisterDate;
                    updateUser.Telephone = newUser.Telephone;
                    updateUser.EMail = newUser.EMail;

                    movieArchiveDB.SaveChanges();
                    return new NResult { IsSuccessful = true };
                }
                else
                {
                    return new NResult
                    {
                        IsSuccessful = false,
                        Message = "Kayıt bulunamadı"
                    };
                }
            }
            catch (Exception hata)
            {
                return new NResult
                {
                    IsSuccessful = false,
                    Message = hata.Message
                };
            }
        }
        
        public NResult DeleteUser(int userID)
        {
            try
            {
                var deleteUserData = movieArchiveDB.User.Where(o => o.UserID == userID);
                if (deleteUserData.Count() > 0)
                {
                    var deleteUser = deleteUserData.FirstOrDefault();
                    movieArchiveDB.User.Remove(deleteUser);
                    movieArchiveDB.SaveChanges();
                    return new NResult { IsSuccessful = true };
                }
                else
                {
                    return new NResult
                    {
                        IsSuccessful = false,
                        Message = "Kayıt bulunamadı"
                    };
                }
            }
            catch (Exception hata)
            {
                return new NResult<int>()
                {
                    IsSuccessful = false,
                    Message = hata.Message
                };
            }
        }
    }
}
