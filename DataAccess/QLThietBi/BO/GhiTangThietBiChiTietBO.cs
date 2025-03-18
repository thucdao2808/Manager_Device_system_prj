using DataAccess.QLThietBi.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace DataAccess.QLThietBi.BO

{
    public class GhiTangThietBiChiTietBO
    {
        public GhiTangThietBiChiTietBO() { }

        #region Get
        public string GetMoney(int id)
        {
            var CacheTbct = new DefaultCacheProvider();
            string cacheKeyGttbct = CacheTbct.BuildCachedKey("GhiTangThietBiChiTiet", "GetMoney", id.ToString());
            string thanhTien = string.Empty;

            if (!CacheTbct.IsSet(cacheKeyGttbct))
            {
                using (var db = new QuanLyThietBiEntities())
                {
                    var data = db.GhiTangThietBiChiTiets
                                 .Where(tt => tt.ID == id)
                                 .Select(tt => new {tt.SoLuong,tt.DonGia})
                                 .FirstOrDefault();
                    if (data != null)
                    {
                        thanhTien = (data.SoLuong * data.DonGia).ToString();
                    }
                    
                }
                CacheTbct.Set(cacheKeyGttbct, thanhTien, DefaultCacheProvider.CACHING_TIME_DEFAULT_IN_1_MINUTES);
            }
            else
            {
                thanhTien = CacheTbct.Get(cacheKeyGttbct) as string;
            }

            return thanhTien;
        }

        public List<GhiTangThietBiChiTiet> GetGhiTang()
        {
            var cacheGhiTang = new DefaultCacheProvider();
            string cacheKeyGhiTang = cacheGhiTang.BuildCachedKey("GhiTangThietBiChiTiet","GetGhiTang");
            List<GhiTangThietBiChiTiet> dataGhiTang = new List<GhiTangThietBiChiTiet>();
            if (!cacheGhiTang.IsSet(cacheKeyGhiTang))
            {
using (var db = new QuanLyThietBiEntities())
            {
                var data = db.GhiTangThietBiChiTiets.Select(s => new
                {
                    s.ID,
                    s.ThietBiID,
                    s.KhoPhongID,
                    s.SoLuong,
                    s.SoHieuTu,
                    s.SoHieuDen,
                    s.DonGia,
                    s.ThanhTien,
                    
                }).ToList();
                foreach(var item in data)
                {
                    GhiTangThietBiChiTiet gt = new GhiTangThietBiChiTiet();
                    gt.ID = item.ID;
                    gt.ThietBiID = item.ThietBiID;
                    gt.KhoPhongID = item.KhoPhongID;
                    gt.SoLuong = item.SoLuong;
                    gt.SoHieuTu = item.SoHieuTu;
                    gt.SoHieuDen = item.SoHieuDen;
                    gt.DonGia = item.DonGia;
                        gt.ThanhTien = decimal.Parse(GetMoney(item.ID));
                        dataGhiTang.Add(gt);
                }
                
            } cacheGhiTang.RemoveByFirstName(cacheKeyGhiTang);
               
            }
            
             
            return dataGhiTang; 

        }


        #endregion

        #region Set
        public void InsertPhieu(string soPhieu, DateTime ngayLapPhieu, string ghiChu)
        {
            var cache_soPhieu = new DefaultCacheProvider();
            string cacheKey = cache_soPhieu.BuildCachedKey("GhiTangThietBi", "InsertPhieu");
            if (!cache_soPhieu.IsSet(cacheKey))
            {
                using (var db = new QuanLyThietBiEntities())
                {
                    var qr = "INSERT INTO GhiTangThietBi (SoPhieu, NgayLapPhieu, GhiChu,DonViID) VALUES (@sp, @nl, @gc,@dv)";
                    db.Database.ExecuteSqlCommand(qr,
                        new SqlParameter("@sp", soPhieu),
                        new SqlParameter("@nl", ngayLapPhieu),
                        new SqlParameter("@gc", ghiChu),
                        new SqlParameter("@dv", 1)
                    );
                    cache_soPhieu.RemoveByFirstName(cacheKey);
                    cache_soPhieu.RemoveByFirstName(cache_soPhieu.BuildCachedKey("GhiTangThietBi", "GetGhiTang"));


                }
                cache_soPhieu.Set(cacheKey, true, DefaultCacheProvider.CACHING_TIME_DEFAULT_IN_1_MINUTES);
            }

            

            
        }

        public static void InsertDetailForm(GhiTangThietBiChiTiet gt)
        {
            var cacheGtct = new DefaultCacheProvider();
            string CacheKeyGtct = cacheGtct.BuildCachedKey("GhiTangThietBiChiTiet", "InsertDetailForm");
            if (!cacheGtct.IsSet(CacheKeyGtct))
            {
                using(var db = new QuanLyThietBiEntities())
                {
                    var dbAdd = db.GhiTangThietBiChiTiets.Select(a=>new
                    {
                        a.ID,
                        a.NhaCungCapID,
                        a.NgayMua,
                        a.DonGia,
                        a.GhiChu,
                        a.KhoPhongID,
                        a.SoHieuTu,
                        a.SoHieuDen,
                        a.ThanhTien,
                        a.ThietBiID,
                        a.SoLuong,
                        a.NgayHetBaoHanh,

                    }).ToList();
                    foreach( var item in dbAdd)
                    {
                        GhiTangThietBiChiTiet tbct = new GhiTangThietBiChiTiet();
                        tbct.ID = item.ID;
                        tbct.NhaCungCapID = item.NhaCungCapID;
                        tbct.NgayMua = item.NgayMua;
                        tbct.DonGia= item.DonGia; ;
                        tbct.GhiChu = item.GhiChu;
                        tbct.KhoPhongID = item.KhoPhongID;
                        tbct.SoHieuTu = item.SoHieuTu;
                        tbct.SoHieuDen = item.SoHieuDen;
                        tbct.ThanhTien = item.ThanhTien;
                        tbct.ThietBiID = item.ThietBiID;
                        tbct.SoLuong = item.SoLuong;
                        tbct.NgayHetBaoHanh = item.NgayHetBaoHanh;
                        db.GhiTangThietBiChiTiets.Add(tbct);

                    }
                }
            }
        }

        #endregion
    }


}
