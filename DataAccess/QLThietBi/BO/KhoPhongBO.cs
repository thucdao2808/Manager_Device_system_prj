using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.Caching;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using System.Web.UI;
using DataAccess.QLThietBi.Model;


namespace DataAccess.QLThietBi.BO
{ 
    public class KhoPhongBO
    {
        public KhoPhongBO() { }

        private QuanLyThietBiEntities db = new QuanLyThietBiEntities();
        #region Get

        public List<KhoPhong> GetKhoPhong()
        {
            var CacheKhoPhong = new DefaultCacheProvider();
            string CacheKey = CacheKhoPhong.BuildCachedKey("KhoPhong", "GetKhoPhong");
            List<KhoPhong> dataKhoPhong = new List<KhoPhong>();
            if (!CacheKhoPhong.IsSet(CacheKey))
            {
                using (var db = new QuanLyThietBiEntities())
                {
                    var query = db.KhoPhongs
                                  .Select(kp => new
                                  {
                                      kp.ID,
                                      kp.TenKhoPhong,
                                      kp.XepLoaiKhoPhongID,
                                      kp.NguoiPhuTrachID,
                                      kp.isPhongChucNang,
                                      kp.isSuDung
                                  }).ToList();

                    dataKhoPhong = query.Select(kp => new KhoPhong
                    {
                        ID = kp.ID,
                        TenKhoPhong = kp.TenKhoPhong,
                        XepLoaiKhoPhongID = kp.XepLoaiKhoPhongID,
                        NguoiPhuTrachID = kp.NguoiPhuTrachID,
                        isPhongChucNang = kp.isPhongChucNang,
                        isSuDung = kp.isSuDung
                    }).ToList();

                }
                CacheKhoPhong.Set(CacheKey, dataKhoPhong, DefaultCacheProvider.CACHING_TIME_DEFAULT_IN_1_MINUTES);
            }
            else
            {
                dataKhoPhong = CacheKhoPhong.Get(CacheKey) as List<KhoPhong>;
            }
            return dataKhoPhong;
        }
        #endregion
        #region Set
        public static void Inset(KhoPhong khophong)
        {
            var Cache_nguoiDung = new DefaultCacheProvider();
            string Cache_key_khoPhong = Cache_nguoiDung.BuildCachedKey("KhoPhong", "Insert");
            using (var db = new QuanLyThietBiEntities())
            {
                db.KhoPhongs.Add(khophong);
                db.SaveChanges();
                Cache_nguoiDung.RemoveByFirstName(Cache_key_khoPhong);
                Cache_nguoiDung.RemoveByFirstName(Cache_nguoiDung.BuildCachedKey("KhoPhong", "GetKhoPhong")); // Xóa cache list phòng để get dữ liệu từ DB
            }
        }

        public static void Updates(KhoPhong khophong)
        {
            var Cache_nguoiDung = new DefaultCacheProvider();
            string Cache_key_khoPhong = Cache_nguoiDung.BuildCachedKey("KhoPhong", "Update");
            string sql = " UPDATE KhoPhong SET TenKhoPhong = @TenKhoPhong, XepLoaiKhoPhongID=@xepLoai,NguoiPhuTrachID=@nguoiPhutrach,isPhongChucNang = @isPhongChucNang,isSuDung = @isSuDung WHERE ID=@ID";
            using (var db = new QuanLyThietBiEntities())
            {
               
                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("@ID",khophong.ID),
                    new SqlParameter("@TenKhoPhong", khophong.TenKhoPhong),
                    new SqlParameter("@xepLoai", khophong.XepLoaiKhoPhongID),
                    new SqlParameter("@nguoiPhutrach", khophong.NguoiPhuTrachID),
                    new SqlParameter("@isPhongChucNang", khophong.isPhongChucNang),
                    new SqlParameter("@isSuDung", khophong.isSuDung)
                    );
            }
            Cache_nguoiDung.RemoveByFirstName(Cache_key_khoPhong);
            Cache_nguoiDung.RemoveByFirstName(Cache_nguoiDung.BuildCachedKey("KhoPhong", "GetKhoPhong")); 
        }

        public bool deleted(int ID)
        {
            bool isDeleted = false;
            var Cache_KhoPhongDel = new DefaultCacheProvider();
            string Cache_keyKhoPhongDel = Cache_KhoPhongDel.BuildCachedKey("KhoPhong", "deleted");
            using (var db = new QuanLyThietBiEntities())
            {
                string sql = "DELETE FROM  KhoPhong where ID = @ID";
                int rowDel = db.Database.ExecuteSqlCommand(sql, new SqlParameter("@id", ID));

                if (rowDel > 0)
                {
                    isDeleted = true;
                    Cache_KhoPhongDel.RemoveByFirstName(Cache_keyKhoPhongDel);
                    Cache_KhoPhongDel.RemoveByFirstName(Cache_KhoPhongDel.BuildCachedKey("KhoPhong", "GetKhoPhong")); // Xóa cache list phòng để get dữ liệu từ DB
                }
            }
            return isDeleted;

        }
        public void Export(string filename, GridView grv)
        {
            filename = filename.EndsWith(".xls") ? filename : filename + ".xls";

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", $"attachment;filename={filename}");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Server.ScriptTimeout = 300;

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    hw.Write("<table><tr><td style='font-size:16px; font-weight:bold;font-size:40px; text-align:center;' colspan='" + grv.Columns.Count + "'>");
                    hw.Write(Path.GetFileNameWithoutExtension(filename));
                    hw.Write("</td></tr></table>");
                    // Lưu trữ các cột cần ẩn
                    List<int> hiddenColumns = new List<int> { 0, 2 };
                    List<bool> visibility = new List<bool>();

                    // Ẩn các cột không cần thiết
                    foreach (int colIndex in hiddenColumns)
                    {
                        visibility.Add(grv.Columns[colIndex].Visible);
                        grv.Columns[colIndex].Visible = false;
                    }


                    GridView gvExport = new GridView();
                    gvExport = grv;

                    foreach (GridViewRow row in gvExport.Rows)
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

                    gvExport.RenderControl(hw);
                    HttpContext.Current.Response.Output.Write(sw.ToString());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();


                    for (int i = 0; i < hiddenColumns.Count; i++)
                    {
                        grv.Columns[hiddenColumns[i]].Visible = visibility[i];
                    }
                }
            }
        }
       
        #endregion
    }
}