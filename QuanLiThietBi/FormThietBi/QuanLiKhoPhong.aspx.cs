using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using DataAccess.QLThietBi.BO;
using DataAccess.QLThietBi.Model;

namespace QuanLiThietBi
{
    public partial class QuanLiKhoPhong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          if (!IsPostBack)
            {
                LoadData();
            }
        }
        private KhoPhongBO khophong;
        private QuanLyThietBiEntities db = new QuanLyThietBiEntities();

        #region Logic
        public void LoadData()
        {
            KhoPhongBO khophong = new KhoPhongBO();
            var data_grv = khophong.GetKhoPhong();
            gvKhoPhong.DataSource = data_grv;
            gvKhoPhong.DataBind();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void Export_Click(object sender, EventArgs e)
        {
            KhoPhongBO khophng = new KhoPhongBO();
            var grv = gvKhoPhong;
            khophng.Export("KhoPhong", grv);
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            DataAccess.QLThietBi.Model.KhoPhong khoPhong = new DataAccess.QLThietBi.Model.KhoPhong()
            {
                TenKhoPhong = txtTenKhoPhong.Text,
                XepLoaiKhoPhongID = Convert.ToInt16(txtXepLoai.Text),
                NguoiPhuTrachID = Convert.ToInt16(txtNguoiPhuTrach.Text),
                isPhongChucNang = chkPhongChucNang.Checked,
                isSuDung = chkSuDung.Checked,
                DonViID = 1,
                NguoiTaoID = 1,
                NgayTao = DateTime.Now,

            };
            KhoPhongBO.Inset(khoPhong);
            Response.Redirect(Request.RawUrl);


        }
        protected void btnDel_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvKhoPhong.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkDel");
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(gvKhoPhong.DataKeys[row.RowIndex].Value);
                    KhoPhongBO nguoiDung = new KhoPhongBO();
                    bool isDel = nguoiDung.deleted(id);
                }

            }
            LoadData();

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton btn = (ImageButton)sender;
                int id = int.Parse(btn.CommandArgument);
                var user = db.KhoPhongs.Find(id);
                if (user != null)
                {
                    PanelEdit.Visible = true;
                    hiddenFieldID.Value = id.ToString();
                    txtEditTenKhoPhong.Text = user.TenKhoPhong;
                    txtEditNguoiPhuTrach.Text = Convert.ToString(user.NguoiPhuTrachID);
                    txtEditXepLoai.Text = Convert.ToString(user.XepLoaiKhoPhongID);
                    chkEditPhongChucNang.Checked = user.isPhongChucNang;
                    chkEditSuDung.Checked = user.isSuDung;

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_cancel_edit_Click(object sender, EventArgs e)
        {
            PanelEdit.Visible = false;
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(hiddenFieldID.Value);
                KhoPhong khoPhong = new KhoPhong()
                {
                    ID = id,
                    TenKhoPhong = txtEditTenKhoPhong.Text,
                    XepLoaiKhoPhongID = Convert.ToInt16(txtEditXepLoai.Text),
                    NguoiPhuTrachID = Convert.ToInt16(txtEditNguoiPhuTrach.Text),
                    isPhongChucNang = chkEditPhongChucNang.Checked,
                    isSuDung = chkEditSuDung.Checked
                };

                KhoPhongBO.Updates(khoPhong);
                PanelEdit.Visible = false;
                LoadData();
            }
            catch (Exception ex)
            {

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var name= txtSearch.Text.Trim();
            var dt = db.KhoPhongs.Where(n => n.TenKhoPhong.Contains(name)).ToList();
            gvKhoPhong.DataSource = dt;
            gvKhoPhong.DataBind();
            txtSearch.Text = string.Empty;
        }
        #endregion
    }
}