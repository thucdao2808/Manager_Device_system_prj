using DataAccess.QLThietBi.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataAccess.QLThietBi.BO
{
    public class DangNhapBO
    {
        public List<string> GetUser()
        {
            var CacheDangNhap = new DefaultCacheProvider();
            string CacheKeyDangNhap = CacheDangNhap.BuildCachedKey("DangNhap", "GetUser");
            using (var db = new QuanLyThietBiEntities())
            {
                if (CacheDangNhap.IsSet(CacheKeyDangNhap))
                {
                    return CacheDangNhap.Get(CacheKeyDangNhap) as List<string>;
                }
                var DangNhap = db.DangNhaps.Select(x=>x.UserName).ToList();
                if (DangNhap != null)
                {
                    CacheDangNhap.Set(CacheKeyDangNhap, DangNhap, 30);
                }
                return DangNhap;
            }
        }
        public List<string> GetPassword()
        {
            var CachePassword = new DefaultCacheProvider();
            string CacheKeyPassword = CachePassword.BuildCachedKey("DangNhap", "GetPassword");
            using (var db = new QuanLyThietBiEntities())
            {
                if (CachePassword.IsSet(CacheKeyPassword))
                {
                    return CachePassword.Get(CacheKeyPassword) as List<string>;
                }
                var pass = db.DangNhaps.Select(x => x.UserName).ToList();
                if (pass != null)
                {
                    CachePassword.Set(CacheKeyPassword, pass, 30);
                }
                return pass;
            }
        }


        public  void InsertUser(string username, string password)
        {
            var cache_dangki = new DefaultCacheProvider();
            string CacheKey = cache_dangki.BuildCachedKey("Dangnhap", "Insert");
            string sql = "Insert into DangNhap (UserName,Password) values (@username,@password)";
            using (var db = new QuanLyThietBiEntities())
            {
                var newUser = new DangNhap
                {
                    UserName = username,
                    Password = password
                };

                // Thêm vào database
                db.DangNhaps.Add(newUser);
                db.SaveChanges();
                cache_dangki.RemoveByFirstName(CacheKey);
                cache_dangki.RemoveByFirstName(cache_dangki.BuildCachedKey("DangNhap", "GetUser"));
            }
        }
    }
}
