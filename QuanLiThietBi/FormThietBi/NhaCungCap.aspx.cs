using DataAccess.QLThietBi.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DataAccess.QLThietBi.Model;
using System.ComponentModel;
using DataAccess.QLThietBi.Model.Extent;
namespace QuanLiThietBi.FormThietBi
{
    public partial class NhaCungCap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNhaCungCap();
            }
        }
        private NhaCungCapBO nhacungcap;
        private ThietBiBO thietbi;
        private QuanLyThietBiEntities db = new QuanLyThietBiEntities();

        #region Logic
        public void LoadNhaCungCap()
        {
            NhaCungCapBO nhacungcap = new NhaCungCapBO();
            var data = nhacungcap.GetNhaCungCap();
            grvNhaCungCap.DataSource = data;
            grvNhaCungCap.DataBind();


        }

        protected void grvNhaCungCap_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Lấy dữ liệu từ DataItem
                var ncc = (DataAccess.QLThietBi.Model.NhaCungCap)e.Row.DataItem;

                // Tìm TextBox trong TemplateField
                TextBox txtNote = (TextBox)e.Row.FindControl("txtNote");

                // Gán giá trị MoTa vào TextBox
                var x = new ThietBiBO().GetGhiChu();
               
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlFormAdd.Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void btnDong_Click(object sender, EventArgs e)
        {
            pnlFormAdd.Visible = false;
        }

        protected void Export_Click(object sender, EventArgs e)
        {
            NhaCungCapBO ncc = new NhaCungCapBO();
            var grv = grvNhaCungCap;
            ncc.Export("NhaCungCap", grv);
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            DataAccess.QLThietBi.Model.NhaCungCap nhacungcap = new DataAccess.QLThietBi.Model.NhaCungCap()
            {
                TenNhaCungCap = txtTenNhaCungCap.Text,
                isSuDung =chkSuDung.Checked,
                Diachi="",
                HoTenNguoiLienHe="",
                DienThoaiNguoiLienHe=""
            };
            DataAccess.QLThietBi.Model.Extent.NhaCungCap nhaCungCapExt = new DataAccess.QLThietBi.Model.Extent.NhaCungCap()
            {
                Mota = "",
                GhiChu = txtGhiChu.Text
            };
            NhaCungCapBO.Insert(nhacungcap, nhaCungCapExt);
            Response.Redirect(Request.RawUrl);


        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            foreach(GridViewRow row in grvNhaCungCap.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDel");
                if(chk !=null && chk.Checked)
                {
                    int id = Convert.ToInt32(grvNhaCungCap.DataKeys[row.RowIndex].Value);
                    NhaCungCapBO ncc = new NhaCungCapBO();
                    ncc.Delete(id);
                }    
            }
            LoadNhaCungCap();
        }

        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton ibt = (ImageButton)sender;
                var id = int.Parse(ibt.CommandArgument);
                var cungCap = db.NhaCungCaps.Find(id);
                if(cungCap != null)
                {
                   pnlFormEdit.Visible = true;
                    hdfID.Value = cungCap.ID.ToString();
                    txtNameSupply.Text = cungCap.TenNhaCungCap;
                    txtTakeNote.Text = cungCap.GhiChu;
                    chkEditUse.Checked = cungCap.isSuDung; 

                }
            }
            catch(Exception ex)
            {

            }
           
        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
          

                if (string.IsNullOrEmpty(hdfID.Value))
                {
                    throw new Exception("ID is missing.");
                }
                int idSelect = int.Parse(hdfID.Value);

                DataAccess.QLThietBi.Model.NhaCungCap ncc = new DataAccess.QLThietBi.Model.NhaCungCap()
                {
                    ID= idSelect,
                    TenNhaCungCap = txtNameSupply.Text,
                    GhiChu=txtTakeNote.Text,
                    isSuDung = chkEditUse.Checked
                };

                NhaCungCapBO.Updated(ncc);
                pnlFormEdit.Visible = false;
                LoadNhaCungCap();
           


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var nameNhaCungCap = txtSearch.Text.Trim();
            var dt = db.NhaCungCaps.Where(n => n.TenNhaCungCap.Contains(nameNhaCungCap)).ToList();
            grvNhaCungCap.DataSource = dt;
            grvNhaCungCap.DataBind();
            txtSearch.Text = string.Empty;
        }
        #endregion
    }
}