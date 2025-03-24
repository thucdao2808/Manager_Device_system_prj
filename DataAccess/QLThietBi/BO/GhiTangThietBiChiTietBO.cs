using DataAccess.QLThietBi.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataAccess.QLThietBi.BO

{
    public class GhiTangThietBiChiTietBO
    {
        public GhiTangThietBiChiTietBO() { }

        #region Get
            public string GetMoney(long donViID)
            {
                var CacheTbct = new DefaultCacheProvider();
                string cacheKeyGttbct = CacheTbct.BuildCachedKey("GhiTangThietBiChiTiet", "GetMoney", donViID.ToString());
                string thanhTien = string.Empty;

                if (!CacheTbct.IsSet(cacheKeyGttbct))
                {
                    using (var db = new QuanLyThietBiEntities())
                    {
                        var data = db.GhiTangThietBiChiTiets
                                     .Where(tt => tt.DonViID == donViID)
                                     .Select(tt => new { tt.SoLuong, tt.DonGia })
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


            public List<GhiTangThietBiChiTiet> GetGhiTangCt()
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
                        s.DonViID
                    
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
                            gt.ThanhTien = decimal.Parse(GetMoney(item.DonViID) );
                            dataGhiTang.Add(gt);
                    }
                } cacheGhiTang.RemoveByFirstName(cacheKeyGhiTang);
                    cacheGhiTang.RemoveByFirstName(cacheGhiTang.BuildCachedKey("GhiTangThietBiChiTiet","GetGhiTang"));
               
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
                using (var db = new QuanLyThietBiEntities())
                {
                    db.GhiTangThietBiChiTiets.Add(gt);
                    db.SaveChanges();
                }

            }

        }

        public void ExportExcel(string fileName , GridView grv)
        {
            fileName = fileName.EndsWith(".xls") ? fileName : fileName + ".xls";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", $"attachment;filename={fileName}");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Server.ScriptTimeout = 300;

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GridView GrvEx = new GridView();
                    GrvEx = grv;
                    foreach (GridViewRow row in GrvEx.Rows)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            foreach (Control control in cell.Controls)
                            {
                                if (control is DropDownList DropDownList2)
                                {
                                    cell.Text = DropDownList2.Text;
                                }
                                if (cell.Controls.OfType<Image>().Any())
                                {

                                    Image img = cell.Controls.OfType<Image>().FirstOrDefault();
                                    if (img != null && img.Visible)
                                    {
                                        cell.Text = "true";
                                    }
                                    else
                                    {
                                        cell.Text = "false";
                                    }
                                }
                            }
                            cell.Text = HttpUtility.HtmlEncode(cell.Text);
                        }
                    }

                    GrvEx.RenderControl(hw);
                    HttpContext.Current.Response.Output.Write(sw.ToString());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
            

        }
        public static void UpdateGhiChu(GhiTangThietBi tbct)
        {
            var cacheUp = new DefaultCacheProvider();
            string cacheKeyUp = cacheUp.BuildCachedKey("GhiTangThietBiChiTiet", "UpdateGhiChu");
            using (var db = new QuanLyThietBiEntities())
            {
                db.GhiTangThietBis.AddOrUpdate(tbct);
                db.SaveChanges();
                cacheUp.RemoveByFirstName(cacheKeyUp);
                cacheUp.RemoveByFirstName(cacheUp.BuildCachedKey("GhiTangThietBiChiTiet", "GetGhiTang"));
            }
        }

        #endregion
    }


}
