using DataAccess.QLThietBi.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataAccess.QLThietBi.BO
{
    public class NhomThietBiBO 
    {
       public NhomThietBiBO() { }
        #region get
        public List<NhomThietBi> GetNhomThietBi()
        {
            var CacheNhomThietBi = new DefaultCacheProvider();
            string CacheKeyNhomThietBi = CacheNhomThietBi.BuildCachedKey("NhomThietBi", "GetNhomThietBi");
            List<NhomThietBi> dataThietBi = new List<NhomThietBi>();
            if (!CacheNhomThietBi.IsSet(CacheKeyNhomThietBi))
            {
                using (var db = new QuanLyThietBiEntities())
                {
                    var sql = db.NhomThietBis.Select(ntb => new
                    {
                        ntb.ID,
                        ntb.TenNhomThietBi,
                        ntb.GhiChu,
                        ntb.ThuTu,
                        ntb.isSuDung,
                        ntb.isHeThong,
                    }).ToList();
                    foreach(var item in sql)
                    {
                        NhomThietBi ntb = new NhomThietBi();
                        ntb.ID = item.ID;
                        ntb.TenNhomThietBi = item.TenNhomThietBi;
                        ntb.GhiChu = item.GhiChu;
                        ntb.ThuTu = item.ThuTu;
                        ntb.isSuDung = item.isSuDung;
                        ntb.isHeThong = item.isHeThong;
                        dataThietBi.Add(ntb);
                    }
                }
            }
            return dataThietBi;
        }
        #endregion

        #region set
        public static void Insert(NhomThietBi ntb)
        {
            var CacheNhomThietBi = new DefaultCacheProvider();
            string CacheKeyNhomThietBi = CacheNhomThietBi.BuildCachedKey("NhomThietBi", "Insert");
            using (var db = new QuanLyThietBiEntities())
            {
                db.NhomThietBis.Add(ntb);
                db.SaveChanges();
                CacheNhomThietBi.RemoveByFirstName(CacheKeyNhomThietBi);
                CacheNhomThietBi.RemoveByFirstName(CacheNhomThietBi.BuildCachedKey("NhomThietBi", "GetNhomThietBi"));
            }
        }
        public void export(string filename,GridView grv )
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
        public static void Updates(NhomThietBi ntb)
        {
            var CacheNtb = new DefaultCacheProvider();
            string CacheKey = CacheNtb.BuildCachedKey("NhomThietBi","Updates");
            string sql = "UPDATE NhomThietBi SET TenNhomThietBi = @tntb,GhiChu = @gc,ThuTu=@tt,isSuDung=@isSd,isHeThong=@isHt WHERE ID =@ID ";
            using (var db = new QuanLyThietBiEntities())
            {
                db.Database.ExecuteSqlCommand(sql,new SqlParameter("@ID",ntb.ID),
                    new SqlParameter("@tntb",ntb.TenNhomThietBi),
                    new SqlParameter("@gc",ntb.GhiChu),
                    new SqlParameter("@tt",ntb.ThuTu),
                    new SqlParameter("@isSd",ntb.isSuDung),
                    new SqlParameter("@isHt",ntb.isHeThong)
                    );
                CacheNtb.RemoveByFirstName(CacheKey);
                CacheNtb.RemoveByFirstName(CacheNtb.BuildCachedKey("NhomThietBi", "GetNhomThietBi"));
            }    
        }
        public bool deleted(int ID)
        {
            bool isDeleted = false;
            var CacheNhomThietBiDel = new DefaultCacheProvider();
            string Cache_keyNhomThietBiDel = CacheNhomThietBiDel.BuildCachedKey("NhomThietBi", "deleted");
            using (var db = new QuanLyThietBiEntities())
            {
                string sql = "DELETE FROM  NhomThietBi where ID = @ID";
                int rowDel = db.Database.ExecuteSqlCommand(sql, new System.Data.SqlClient.SqlParameter("@id", ID));

                if (rowDel > 0)
                {
                    isDeleted = true;
                    CacheNhomThietBiDel.RemoveByFirstName(Cache_keyNhomThietBiDel);
                    CacheNhomThietBiDel.RemoveByFirstName(CacheNhomThietBiDel.BuildCachedKey("NhomThietBi", "GetNhomThietBi"));
                }
            }
            return isDeleted;

        }
        #endregion
    }
}
