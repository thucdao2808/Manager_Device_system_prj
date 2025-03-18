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
    public partial class ThietBi : System.Web.UI.Page
    {
        private GridView grv;
        private QuanLyThietBiEntities db = new QuanLyThietBiEntities();
        private List<DataAccess.QLThietBi.Model.NhaCungCap> nhaCungCaps = new NhaCungCapBO().GetNhaCungCap();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadThietBi();
            }    
        }
        public void LoadThietBi()
        {
            ThietBiBO thietbi = new ThietBiBO();
            var data = thietbi.GetThietBi();
            grvThietBi.DataSource = data;
            grvThietBi.DataBind();
        }

        private void LoadDropDownList()
        {
            ddlNhaCungCap.DataSource = nhaCungCaps;
            ddlNhaCungCap.DataTextField = "TenNhaCungCap";
            ddlNhaCungCap.DataValueField = "ID";
            ddlNhaCungCap.DataBind();
            using (var db = new QuanLyThietBiEntities())
            {
                var thuongHieus = db.DMRootThietBiThuongHieux.ToList();
                ddlThuongHieu.DataSource = thuongHieus;
                ddlThuongHieu.DataTextField = "TenThuongHieu";
                ddlThuongHieu.DataValueField = "ID";
                ddlThuongHieu.DataBind();

                var nhomThietBis = db.DMRootThietBiNhomThietBis.ToList();
                DropDownListDeviceGroup.DataSource = nhomThietBis;
                DropDownListDeviceGroup.DataTextField = "TenNhomThietBi";
                DropDownListDeviceGroup.DataValueField = "ID";
                DropDownListDeviceGroup.DataBind();

                var donViTinhs = db.DmRootThietBiDonViTinhs.ToList();
                ddlDonViTinh.DataSource = donViTinhs;
                ddlDonViTinh.DataTextField = "TenDonViTinh";
                ddlDonViTinh.DataValueField = "ID";
                ddlDonViTinh.DataBind();
            }
        }
        private void ClearForm()
        {
            FieldID.Value = string.Empty;
            TextBoxDeviceName.Text = string.Empty;
            TextBoxDeviceCode.Text = string.Empty;
            TextBoxMaintenanceTeam.Text = string.Empty;
            TextBoxMaintenanceStaff.Text = string.Empty;
            RadioEmployee.Checked = false;
            RadioDepartment.Checked = false;
            RadioQuantity.Checked = false;
            RadioSerialNumber.Checked = false;
            chkThietBiTieuHao.Checked = false;
            chkSuDung.Checked = false;
            TextBoxUsagePurpose.Text = string.Empty;
            TxtMota.Text = string.Empty;
            ddlNhaCungCap.SelectedIndex = 0;
            ddlThuongHieu.SelectedIndex = 0;
            DropDownListDeviceGroup.SelectedIndex = 0;
            ddlDonViTinh.SelectedIndex = 0;

        }

        protected void Add_Click(object sender, EventArgs e)
        {
            PanelMain.Visible = true;
            LoadDropDownList();
            ClearForm();
        }
        protected void Edit_Click(object sender, CommandEventArgs e)
        {
            LoadDropDownList();
            string id = e.CommandArgument.ToString().Split(',').FirstOrDefault();
            DataAccess.QLThietBi.Model.ThietBi thietBi = new ThietBiBO().GetThietBi().Find(f => f.ID.ToString() == id);
            if (thietBi != null)
            {
                FieldID.Value = thietBi.ID.ToString();
                TextBoxDeviceName.Text = thietBi.TenThietBi;
                TextBoxDeviceCode.Text = thietBi.MaThietBi;
                RadioEmployee.Checked = thietBi.isNhanVienSuDung;
                RadioEmployee.Checked = thietBi.isPhongBanSuDung;
                RadioQuantity.Checked = thietBi.isQuanLyTheoSoLuong;
                RadioSerialNumber.Checked = thietBi.isQuanLyTheoSoHieu;
                chkThietBiTieuHao.Checked = thietBi.isThietBiTieuHao;
                chkSuDung.Checked = thietBi.isSuDung;
                TextBoxUsagePurpose.Text = thietBi.MucDichSuDung;
                TxtMota.Text = thietBi.MoTa;
                TextBoxMaintenanceTeam.Text = thietBi.DoiBaoTriID.ToString();
                TextBoxMaintenanceStaff.Text = thietBi.NhanSuBaoTri.ToString();
               


                ddlNhaCungCap.SelectedIndex = nhaCungCaps.FindIndex(f => f.ID == thietBi.NhaCungCapID);
                using (var db = new QuanLyThietBiEntities())
                {
                    var thuongHieus = db.DMRootThietBiThuongHieux.ToList();
                    ddlThuongHieu.SelectedIndex = thuongHieus.FindIndex(f => f.ID == thietBi.ThuongHieuID);

                    var nhomThietBis = db.DMRootThietBiNhomThietBis.ToList();
                    DropDownListDeviceGroup.SelectedIndex = nhomThietBis.FindIndex(f => f.ID == thietBi.NhomThietBiID);

                    var donViTinhs = db.DmRootThietBiDonViTinhs.ToList();
                    ddlDonViTinh.SelectedIndex = nhomThietBis.FindIndex(f => f.ID == thietBi.DonViTinhID);
                }
            }


            PanelMain.Visible = true;
        }

        protected void btnCancelForm_Click(object sender, ImageClickEventArgs e)
        {
            PanelMain.Visible = false;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
        protected void Export_Click(object sender, EventArgs e)
        {
            ThietBiBO thietbi = new ThietBiBO();
            var data = grvThietBi;
            thietbi.export("ThietBi", data);
            
        }

        protected string GetNhaCungCap(object nhaCungCapID)
        {
            if (nhaCungCapID == null)
                return "";
            var ncc = nhaCungCaps.Find(f => f.ID == Convert.ToInt32(nhaCungCapID));
            if (ncc == null)
                return "";
            return ncc.TenNhaCungCap;
        }
        protected string GetThuongHieu(object strThuongHieuID)
        {
            if (strThuongHieuID == null)
                return "";
            string tenThuongHieu = string.Empty;
            using (var db = new QuanLyThietBiEntities())
            {
                int thuongHieuID = Convert.ToInt32(strThuongHieuID);
                var thuongHieus = db.DMRootThietBiThuongHieux.Where(f => f.ID == thuongHieuID).FirstOrDefault();
                if (thuongHieus == null)
                    return "";
                tenThuongHieu = thuongHieus.TenThuongHieu;
            }
            return tenThuongHieu;
        }
        protected string GetDonViTinh(object strDonViTinh)
        {
            if (strDonViTinh == null)
                return "";
            string tenDonViTinh = string.Empty;
            using (var db = new QuanLyThietBiEntities())
            {
                int donViTinhID = Convert.ToInt32(strDonViTinh);
                var donViTinh = db.DmRootThietBiDonViTinhs.Where(f => f.ID == donViTinhID).FirstOrDefault();
                if (donViTinh == null)
                    return "";
                tenDonViTinh = donViTinh.TenDonViTinh;
            }
            return tenDonViTinh;
        }
        protected void btnDel_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grvThietBi.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                if (chk != null && chk.Checked)
                {
                    int id = Convert.ToInt32(grvThietBi.DataKeys[row.RowIndex].Value);
                    ThietBiBO thietbi = new ThietBiBO();
                    thietbi.Deleted(id);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string id = FieldID.Value.ToString();
            if (string.IsNullOrEmpty(id))
            {
                Inserting();
            }
            else if (!string.IsNullOrEmpty(id))
            {
                Updating();
            }
        }
        protected void Inserting()
        {
            DataAccess.QLThietBi.Model.ThietBi tb = new DataAccess.QLThietBi.Model.ThietBi()
            {
                TenThietBi = TextBoxDeviceName.Text,
                MaThietBi = TextBoxDeviceCode.Text,
                NhomThietBiID = Convert.ToInt16(DropDownListDeviceGroup.SelectedValue),
                NhaCungCapID = Convert.ToInt16(ddlNhaCungCap.SelectedValue),
                ThuongHieuID = Convert.ToInt16(ddlThuongHieu.SelectedValue),
                DonViTinhID = Convert.ToInt16(ddlDonViTinh.SelectedValue),
                isNhanVienSuDung = RadioEmployee.Checked,
                isPhongBanSuDung = RadioEmployee.Checked,
                isQuanLyTheoSoLuong = RadioQuantity.Checked,
                isQuanLyTheoSoHieu = RadioSerialNumber.Checked,
                isThietBiTieuHao = chkThietBiTieuHao.Checked,
                isSuDung = chkSuDung.Checked,
                MucDichSuDung = TextBoxUsagePurpose.Text,
                MoTa = TxtMota.Text,
            };

            if (!string.IsNullOrEmpty(TextBoxMaintenanceTeam.Text))
                tb.DoiBaoTriID = Convert.ToInt16(TextBoxMaintenanceTeam.Text);
            if (!string.IsNullOrEmpty(TextBoxMaintenanceStaff.Text))
                tb.NhanSuBaoTri = Convert.ToInt16(TextBoxMaintenanceStaff.Text);
            ThietBiBO.Insert(tb);
            Response.Redirect(Request.RawUrl);

        }
        protected void Updating()
        {
            DataAccess.QLThietBi.Model.ThietBi tb = new DataAccess.QLThietBi.Model.ThietBi()
            {
                ID = int.Parse(FieldID.Value),
                TenThietBi = TextBoxDeviceName.Text,
                MaThietBi = TextBoxDeviceCode.Text,
                NhomThietBiID = Convert.ToInt16(DropDownListDeviceGroup.SelectedValue),
                NhaCungCapID = Convert.ToInt16(ddlNhaCungCap.SelectedValue),
                ThuongHieuID = Convert.ToInt16(ddlThuongHieu.SelectedValue),
                DonViTinhID = Convert.ToInt16(ddlDonViTinh.SelectedValue),
                isNhanVienSuDung = RadioEmployee.Checked,
                isPhongBanSuDung = RadioEmployee.Checked,
                isQuanLyTheoSoLuong = RadioQuantity.Checked,
                isQuanLyTheoSoHieu = RadioSerialNumber.Checked,
                isThietBiTieuHao = chkThietBiTieuHao.Checked,
                isSuDung = chkSuDung.Checked,
                MucDichSuDung = TextBoxUsagePurpose.Text,
                MoTa = TxtMota.Text,
              
            };

            if (!string.IsNullOrEmpty(TextBoxMaintenanceTeam.Text))
                tb.DoiBaoTriID = Convert.ToInt16(TextBoxMaintenanceTeam.Text);
            if (!string.IsNullOrEmpty(TextBoxMaintenanceStaff.Text))
                tb.NhanSuBaoTri = Convert.ToInt16(TextBoxMaintenanceStaff.Text);

            ThietBiBO.Update(tb);
            Response.Redirect(Request.RawUrl);

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var name = txtSearch.Text.Trim();
            var dt = db.KhoPhongs.Where(n => n.TenKhoPhong.Contains(name)).ToList();
             grvThietBi.DataSource = dt;
            grvThietBi.DataBind();
            txtSearch.Text = string.Empty;
        }
       
    }
}