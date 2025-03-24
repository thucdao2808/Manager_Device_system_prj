using DataAccess;
using DataAccess.QLThietBi.BO;
using DataAccess.QLThietBi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLiThietBi.FormThietBi
{
    public partial class NhomThietBi : System.Web.UI.Page
    {
        private NhomThietBiBO nhomthietbi = new NhomThietBiBO();
        private QuanLyThietBiEntities db = new QuanLyThietBiEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNhomThietBi();
            }
        }
        #region Logic
        public void LoadNhomThietBi()
        {
            NhomThietBiBO nhomthietbi = new NhomThietBiBO();
            var Data = nhomthietbi.GetNhomThietBi();
            grvNhomThietBi.DataSource = Data;
            grvNhomThietBi.DataBind();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            pnlMain.Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            
        }
        protected void btnDong_Click(object sender, EventArgs e)
        {
            pnlMain.Visible = false;
        }

        protected void Export_Click(object sender, EventArgs e)
        {
            NhomThietBiBO NhomThietBi = new NhomThietBiBO();
            var grv = grvNhomThietBi;
            NhomThietBi.export("NhomThietBi", grv);
        }

        protected void btnGhiVaThem_Click(object sender, EventArgs e)
        {

            DataAccess.QLThietBi.Model.NhomThietBi ntb = new DataAccess.QLThietBi.Model.NhomThietBi()
            {
                TenNhomThietBi = txtTenNhom.Text,
                GhiChu = txtGhiChu.Text,
                ThuTu = Convert.ToInt16(txtThuTu.Text),
                isSuDung = chkSuDung.Checked,
                isHeThong = chkHeThong.Checked,
                NhomThietBiChaID = 1

            };
            NhomThietBiBO.Insert(ntb);
            Response.Redirect(Request.RawUrl);

        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            foreach(GridViewRow row in grvNhomThietBi.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDelNhomThietBi");
                if(chk !=null && chk.Checked)
                {
                    int rowDel = Convert.ToInt16(grvNhomThietBi.DataKeys[row.RowIndex].Value);
                    NhomThietBiBO ntb = new NhomThietBiBO();
                    ntb.deleted(rowDel);

                }
            }    
        }

        protected void btnGhiVaThemEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(hdfEdit.Value);
                DataAccess.QLThietBi.Model.NhomThietBi ntb = new DataAccess.QLThietBi.Model.NhomThietBi()
                {
                    ID = id,
                    TenNhomThietBi = txtTenNhomEdit.Text,
                    GhiChu = txtGhiChuEdit.Text,
                    ThuTu = Convert.ToInt16(txtThuTuEdit.Text),
                    isHeThong = chkHeThongEdit.Checked,
                    isSuDung = chkSuDungEdit.Checked
                };

                NhomThietBiBO.Updates(ntb);
                pnlMainEdit.Visible = false;
                LoadNhomThietBi();
            }
            catch (Exception ex)
            {

            }
           
        }

        protected void btnEditNhomThietBi_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ImageButton btnI = (ImageButton)sender;
                pnlMainEdit.Visible = true;
                var id = int.Parse(btnI.CommandArgument);
                var GrDevice = db.NhomThietBis.Find(id);
                if (GrDevice != null)
                {
                    pnlMainEdit.Visible = true;
                    hdfEdit.Value = id.ToString();
                    txtTenNhomEdit.Text = GrDevice.TenNhomThietBi;
                    txtGhiChuEdit.Text = GrDevice.GhiChu;
                    txtThuTuEdit.Text = Convert.ToString(GrDevice.ThuTu);
                    chkHeThongEdit.Checked = GrDevice.isHeThong;
                    chkSuDungEdit.Checked = GrDevice.isSuDung;

                }
            }
            catch
            {

            }
        }

        protected void btnDongEdit_Click(object sender, EventArgs e)
        {
            pnlMainEdit.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var name = txtSearch.Text.Trim();
            var dt = db.NhomThietBis.Where(n => n.TenNhomThietBi.Contains(name)).ToList();
            grvNhomThietBi.DataSource = dt;
            grvNhomThietBi.DataBind();
            txtSearch.Text = string.Empty;
        }
        #endregion
    }
}