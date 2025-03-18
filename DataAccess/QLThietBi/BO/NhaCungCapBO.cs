using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using DataAccess.QLThietBi.Model;
using System.Web.Caching;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data.SqlClient;
namespace DataAccess.QLThietBi.BO
{
    public class NhaCungCapBO
    {

        public NhaCungCapBO() { }

        private ThietBiBO thietbi;
        #region GetNhaCungCap
        public List<NhaCungCap> GetNhaCungCap()
        {
            var CacheNhaCungCap = new DefaultCacheProvider();
            string CacheKey = CacheNhaCungCap.BuildCachedKey("NhaCungCap", "GetNhaCungCap");
            List<NhaCungCap> dataNhaCungCap = new List<NhaCungCap>();
            if (!CacheNhaCungCap.IsSet(CacheKey))
            {
                using (var db = new QuanLyThietBiEntities())
                {
                    var sql = db.NhaCungCaps.Select(Nhacungcap => new
                    {
                        Nhacungcap.ID,
                        Nhacungcap.TenNhaCungCap,
                        Nhacungcap.isSuDung,
                        Nhacungcap.GhiChu,
                    }).ToList();



                    foreach (var item in sql)
                    {
                        NhaCungCap ncc = new NhaCungCap();
                        ncc.ID = item.ID;
                        ncc.TenNhaCungCap = item.TenNhaCungCap;
                        ncc.isSuDung = item.isSuDung;
                        ncc.GhiChu = item.GhiChu;

                        dataNhaCungCap.Add(ncc);
                    }
                }
                CacheNhaCungCap.Set(CacheKey, dataNhaCungCap, DefaultCacheProvider.CACHING_TIME_DEFAULT_IN_1_MINUTES);
            }
            else
            {
                dataNhaCungCap = CacheNhaCungCap.Get(CacheKey) as List<NhaCungCap>;
            }

            return dataNhaCungCap;
        }

        #endregion

        #region SetNhaCungCap
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

        public static void Insert(NhaCungCap ncc, Model.Extent.NhaCungCap nccExt)
        {
            var CacheNhaCungCap = new DefaultCacheProvider();
            string CacheKeyNhaCungCap = CacheNhaCungCap.BuildCachedKey("NhaCungCap", "GetNhaCungCap");

            using (var db = new QuanLyThietBiEntities())
            {
                ncc.GhiChu = nccExt.GhiChu;
                db.NhaCungCaps.Add(ncc);
                db.SaveChanges();
                CacheNhaCungCap.RemoveByFirstName(CacheKeyNhaCungCap);
                CacheNhaCungCap.RemoveByFirstName(CacheNhaCungCap.BuildCachedKey("NhaCungCap", "GetNhaCungCap"));
            }

        }
        public static void Updated(NhaCungCap ncc)
        {
            var CacheUp = new DefaultCacheProvider();
           string CacheKey = CacheUp.BuildCachedKey("NhaCungCap","Updated");
            string sql = "UPDATE NhaCungCap Set TenNhaCungCap = @tncc, GhiChu= @gc ,isSuDung =@sd where ID = @id";
            using (var db = new QuanLyThietBiEntities())
            {
                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("@id",ncc.ID),
                    new SqlParameter("@tncc",ncc.TenNhaCungCap),
                    new SqlParameter("@gc",ncc.GhiChu
),
                    new SqlParameter("@sd",ncc.isSuDung)
                    );
            }
            CacheUp.RemoveByFirstName(CacheKey);
            CacheUp.RemoveByFirstName(CacheUp.BuildCachedKey("NhaCungCap","GetNhaCungCap"));
        }
        public bool Delete(int id)
        {
            bool isdel = false;
            var cacheDel = new DefaultCacheProvider();
            string keyCacheDelNhaCC = cacheDel.BuildCachedKey("NhaCungCap", "Delete");

            using (var db = new QuanLyThietBiEntities())
            {
                var sql = "delete NhaCungCap where ID =@ID";
                int isRow = db.Database.ExecuteSqlCommand(sql, new SqlParameter("@ID", id));
                if (isRow > 0)
                {
                    isdel = true;
                    cacheDel.RemoveByFirstName(keyCacheDelNhaCC);
                    cacheDel.RemoveByFirstName(cacheDel.BuildCachedKey("NhaCungCap", "GetNhaCungCap"));
                }
            }
            return isdel;

        }
        #endregion
    }


}
