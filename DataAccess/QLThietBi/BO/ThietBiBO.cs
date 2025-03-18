using DataAccess.QLThietBi.Model;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Data.Entity.Migrations;

namespace DataAccess.QLThietBi.BO
{
    public class ThietBiBO
    {
        public ThietBiBO() { }
        #region Get
        public List<ThietBi> GetThietBi()
        {
            var CacheThietBi = new DefaultCacheProvider();
            string CacheKeyThietBi = CacheThietBi.BuildCachedKey("ThietBi", "GetThietBi");
            List<ThietBi> dataThietbi = new List<ThietBi>();
            using (var db = new QuanLyThietBiEntities())
            {
                var query = db.ThietBis
                 .Select(tb => new
                 {
                     tb.ID,
                     tb.MaThietBi,
                     tb.TenThietBi,
                     tb.DonViTinhID,
                     tb.NhomThietBiID,
                     tb.NhaCungCapID,
                     tb.ThuongHieuID,
                     tb.isQuanLyTheoSoLuong,
                     tb.isQuanLyTheoSoHieu,
                     tb.isPhongBanSuDung,
                     tb.isNhanVienSuDung,
                     tb.isThietBiTieuHao,
                     tb.isSuDung,
                     tb.ThuTu,
                     tb.MucDichSuDung,
                     tb.MoTa,

                 })
                .ToList();
                foreach( var item in query)
                {
                    ThietBi tb = new ThietBi();
                    tb.ID = item.ID;
                    tb.MaThietBi = item.MaThietBi;
                    tb.TenThietBi = item.TenThietBi;
                    tb.DonViTinhID= item.DonViTinhID;
                    tb.NhomThietBiID= item.NhomThietBiID;
                    tb.NhaCungCapID= item.NhaCungCapID;
                    tb.ThuongHieuID= item.ThuongHieuID;
                    tb.isQuanLyTheoSoLuong = item.isQuanLyTheoSoLuong;
                    tb.isQuanLyTheoSoHieu = item.isQuanLyTheoSoHieu;
                    tb.isPhongBanSuDung= item.isPhongBanSuDung;
                    tb.isNhanVienSuDung=item.isNhanVienSuDung;
                    tb.isThietBiTieuHao = item.isThietBiTieuHao;
                    tb.isSuDung=item.isSuDung;
                    tb.ThuTu=item.ThuTu;
                    tb.MucDichSuDung=item.MucDichSuDung;
                    tb.MoTa=item.MoTa;
                    dataThietbi.Add(tb);
                }

            }

            return dataThietbi;
        }
        public List<ThietBi> GetGhiChu()
        {
            var CacheGetNote = new DefaultCacheProvider();
            string CacheKeyGetNote = CacheGetNote.BuildCachedKey("ThietBi", "GetGhiChu");
            List<ThietBi> dataGhiChu = new List<ThietBi>();

            if (CacheGetNote.IsSet(CacheKeyGetNote))
            {
                dataGhiChu = CacheGetNote.Get(CacheKeyGetNote) as List<ThietBi>;
            }
            else
            {
                using (var db = new QuanLyThietBiEntities())
                {
                   

                }
               
            }
            return dataGhiChu;
        }
        #endregion

        #region Set

        public  void export(string filename, GridView grv)
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

        public static void Insert(ThietBi tb)
        {
            var CacheThietBi = new DefaultCacheProvider();
            string KeyCacheThietBi = CacheThietBi.BuildCachedKey("ThietBi","Insert");
            using (var db = new QuanLyThietBiEntities())
            {
                db.ThietBis.Add(tb);
                db.SaveChanges();
                CacheThietBi.RemoveByFirstName(KeyCacheThietBi);
                CacheThietBi.RemoveByFirstName(CacheThietBi.BuildCachedKey("ThietBi","GetThietBi"));
                
            }
        }
        public static void Update(ThietBi tb)
        {
            var CacheThietBi = new DefaultCacheProvider();
            string KeyCacheThietBi = CacheThietBi.BuildCachedKey("ThietBi", "Insert");
            using (var db = new QuanLyThietBiEntities())
            {
                db.ThietBis.AddOrUpdate(tb);
                db.SaveChanges();
                CacheThietBi.RemoveByFirstName(KeyCacheThietBi);
                CacheThietBi.RemoveByFirstName(CacheThietBi.BuildCachedKey("ThietBi", "GetThietBi"));

            }
        }
        public bool Deleted(int Id)
        {
            bool isDel = false;
            var CacheNhomThietBiDel = new DefaultCacheProvider();
            string Cache_keyNhomThietBiDel = CacheNhomThietBiDel.BuildCachedKey("ThietBi", "Deleted");
            using (var db = new QuanLyThietBiEntities())
            {
                string sql = "DELETE FROM ThietBi where ID = @ID";
                int rowDel = db.Database.ExecuteSqlCommand(sql, new System.Data.SqlClient.SqlParameter("@ID", Id));

                if (rowDel > 0)
                {
                    isDel = true;
                    CacheNhomThietBiDel.RemoveByFirstName(Cache_keyNhomThietBiDel);
                    CacheNhomThietBiDel.RemoveByFirstName(CacheNhomThietBiDel.BuildCachedKey("ThietBi", "GetThietBi"));
                }
            }
            return isDel;
        }
        #endregion
    }
}
