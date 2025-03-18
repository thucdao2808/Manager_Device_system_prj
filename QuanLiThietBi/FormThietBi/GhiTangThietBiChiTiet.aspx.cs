using DataAccess;
using DataAccess.QLThietBi.BO;
using DataAccess.QLThietBi.Model;
using QuanLiThietBi.FormThietBi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLiThietBi
{
    public partial class GhiTangThietBiChiTiet : System.Web.UI.Page
    {

        private GhiTangThietBiBO tbgt = new GhiTangThietBiBO();
        private List<DataAccess.QLThietBi.Model.GhiTangThietBi> ghiTangThietBi = new GhiTangThietBiBO().GetGhiTang();
        private NhomThietBiBO ntb;
        private KhoPhongBO kp;
        private NhaCungCapBO ncc;
        private ThietBiBO tb;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Loadgrv();
                LoadGrvListGhiTang();
                BindDropdownList();
            }
        }

        public void Loadgrv()
        {
            GhiTangThietBiChiTietBO gtct = new GhiTangThietBiChiTietBO();
            var data = ghiTangThietBi.ToList();
            grvGhiTangct.DataSource = data;
            grvGhiTangct.DataBind();
        }
        public string GetMoney(int id)
        {
            GhiTangThietBiChiTietBO gt = new GhiTangThietBiChiTietBO();
            return gt.GetMoney(id);

        }
        public void LoadGrvListGhiTang()
        {
            GhiTangThietBiChiTietBO gtang = new GhiTangThietBiChiTietBO();
            var dtb = gtang.GetGhiTang();
            grvThietBi.DataSource = dtb;
            grvThietBi.DataBind();

        }
        protected void imgEdit_Click(object sender, ImageClickEventArgs e)
        {

        }
        #region GetInfo
        protected string GetKhoPhong(object strKhoPhong)
        {
            if (strKhoPhong == null)
            {
                return "không có gì";
            }

            string tenKhoPhong = string.Empty;
            using (var db = new QuanLyThietBiEntities())
            {

                var khoPhong = (from kp in db.KhoPhongs
                                join gtct in db.GhiTangThietBiChiTiets on kp.ID equals gtct.KhoPhongID
                                where kp.ID == (int)strKhoPhong
                                select kp).FirstOrDefault();

                if (khoPhong == null)
                {
                    return "Chưa có kho Phòng nào ";
                }

                tenKhoPhong = khoPhong.TenKhoPhong;
            }
            return tenKhoPhong;
        }
        protected string GetThietBi(object strThietbi)
        {
            if (strThietbi == null)
            {
                return "";
            }
            string tenThietbi = string.Empty;
            using (var db = new QuanLyThietBiEntities())
            {
                var tbi = (from tb in db.ThietBis
                           join gtct in db.GhiTangThietBiChiTiets on tb.ID equals gtct.ThietBiID
                           where tb.ID == (int)strThietbi
                           select tb).FirstOrDefault();
                if (tbi == null)
                {
                    return "chưa có thiết bị nào";
                }
                tenThietbi = tbi.TenThietBi;
            }


            return tenThietbi;

        }
        protected string GetDonViTinh(int thietbiId)
        {
            using (var db = new QuanLyThietBiEntities())
            {
                var donvitinh = (from gtct in db.GhiTangThietBiChiTiets
                                 join tb in db.ThietBis on gtct.ThietBiID equals tb.ID
                                 join dmdv in db.DmRootThietBiDonViTinhs on tb.DonViTinhID equals dmdv.ID
                                 where gtct.ThietBiID == thietbiId
                                 select dmdv.TenDonViTinh
                                ).FirstOrDefault();
                return donvitinh ?? "chưa có đơn vị nào";
            }
        }

        protected string GetNhomThietBi(int thietBiID)
        {
            using (var db = new QuanLyThietBiEntities())
            {
                var nhomThietBi = (from gtct in db.GhiTangThietBiChiTiets
                                   join tb in db.ThietBis on gtct.ThietBiID equals tb.ID
                                   join ntb in db.NhomThietBis on tb.NhomThietBiID equals ntb.ID
                                   where gtct.ThietBiID == thietBiID
                                   select ntb.TenNhomThietBi).FirstOrDefault();

                return nhomThietBi ?? "Không có nhóm thiết bị";
            }
        }

        #endregion

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            LoadDateTime();

        }

        protected void LoadDateTime()
        {
            using (var db = new QuanLyThietBiEntities())
            {
                var query = db.GhiTangThietBis.AsQueryable();// tạo 1 truy vấn có thể  thay đổi
                if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
                {
                    DateTime fromDate;
                    DateTime toDate;

                    bool isFromDateValid = DateTime.TryParseExact(txtFromDate.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fromDate);
                    bool isToDateValid = DateTime.TryParseExact(txtToDate.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out toDate);

                    if (isFromDateValid && isToDateValid)
                    {
                        query = query.Where(x => x.NgayLapPhieu >= fromDate && x.NgayLapPhieu <= toDate);
                    }
                    else
                    {

                        return;
                    }
                }
                grvGhiTangct.DataSource = query.ToList();
                grvGhiTangct.DataBind();
            }


        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlDetailChung.Visible = true;
            string currentSoPhieu = GetCurrentSoPhieuFromGridView();


            string nextSoPhieu = GenerateNextSoPhieu(currentSoPhieu);


            txtSoPhieu.Text = nextSoPhieu;



        }
        private string GetCurrentSoPhieuFromGridView()
        {
            if (grvGhiTangct.Rows.Count > 0)
            {

                var lastRow = grvGhiTangct.Rows[grvGhiTangct.Rows.Count - 1];
                var soPhieuCell = lastRow.Cells[3].Text;

                return soPhieuCell;
            }
            return null;
        }

        protected void btnHuy_Click(object sender, ImageClickEventArgs e)
        {
            pnlDetailChung.Visible = false;
        }

        protected void btnThemPhieu_Click(object sender, EventArgs e)
        {
            string currentSoPhieu = GetCurrentSoPhieuFromGridView();

            // Tạo số phiếu mới
            string nextSoPhieu = GenerateNextSoPhieu(currentSoPhieu);

            // Gán số phiếu mới cho TextBox
            txtSoPhieu.Text = nextSoPhieu;
            DateTime ngayLapPhieu;
            if (DateTime.TryParseExact(txtNgayLap.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngayLapPhieu))
            {
                string ghiChu = txtMoTa.Text; // Hoặc
                GhiTangThietBiChiTietBO ghiTangBO = new GhiTangThietBiChiTietBO();
                ghiTangBO.InsertPhieu(nextSoPhieu, ngayLapPhieu, ghiChu);
                LoadGrvListGhiTang();
                PanelGrvDetail.Visible = true;
            }
            pnlDetailChung.Visible = false;
        }

        private string GenerateNextSoPhieu(string currentSoPhieu)
        {
            if (string.IsNullOrEmpty(currentSoPhieu))
            {
                return "PH001";
            }

            string numberPart = currentSoPhieu.Substring(2);

            if (int.TryParse(numberPart, out int number))
            {
                number++;
                return "PH" + number.ToString("D3");
            }
            else
            {
                return "PH001";
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            PanelGrvDetail.Visible = false;

            if (
            PanelGrvDetail.Visible == false)
            {
                PanelContain.Visible = true;
            }
        }

        public void BindDropdownList()
        {
            DataAccess.QLThietBi.BO.NhomThietBiBO ntb = new NhomThietBiBO();
            var data = ntb.GetNhomThietBi();
            ddlNhomThietBi.DataSource = data;
            ddlNhomThietBi.DataTextField = "TenNhomThietBi";
            ddlNhomThietBi.DataValueField = "ID";
            ddlNhomThietBi.DataBind();

            DataAccess.QLThietBi.BO.NhaCungCapBO ncc = new NhaCungCapBO();
            var dataNcc = ncc.GetNhaCungCap();
            ddlNhaCungCap.DataSource = dataNcc;
            ddlNhaCungCap.DataTextField = "TenNhaCungCap";
            ddlNhaCungCap.DataValueField = "ID";
            ddlNhaCungCap.DataBind();

            DataAccess.QLThietBi.BO.ThietBiBO tb = new ThietBiBO();
            var dataTb = tb.GetThietBi();
            ddlThietBi.DataSource = dataTb;
            ddlThietBi.DataTextField = "TenThietBi";
            ddlThietBi.DataValueField = "ID";
            ddlThietBi.DataBind();

            DataAccess.QLThietBi.BO.KhoPhongBO kp = new KhoPhongBO();
            var dataKp = kp.GetKhoPhong();
            ddlKhoPhongs.DataSource = dataKp;
            ddlKhoPhongs.DataTextField = "TenKhoPhong";
            ddlKhoPhongs.DataValueField = "ID";
            ddlKhoPhongs.DataBind();

            using(var db = new QuanLyThietBiEntities())
            {
               
                var dbDvt = db.DmRootThietBiDonViTinhs.Select(dvt => new
                {
                    dvt.TenDonViTinh,
                    dvt.ID
                }).ToList();
                ddlDonVi.DataSource = dbDvt;
                ddlDonVi.DataTextField = "TenDonViTinh";
                ddlDonVi.DataValueField = "ID";
                ddlDonVi.DataBind();
            }
            using (var db = new QuanLyThietBiEntities())
            {
                var dbTh = db.DMRootThietBiThuongHieux.Select(th => new
                {
                    th.TenThuongHieu,
                    th.ID
                }).ToList();
                ddlThuongHieu2.DataSource = dbTh;
                ddlThuongHieu2.DataTextField = "TenThuongHieu";
                ddlThuongHieu2.DataValueField = "ID";
                ddlThuongHieu2.DataBind();

                ddlThuongHieuText.DataSource = dbTh;
                ddlThuongHieuText.DataTextField = "TenThuongHieu";
                ddlThuongHieuText.DataValueField = "ID";
                ddlThuongHieuText.DataBind();
            }
            
        }

        protected void txtSoHieuDen_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void txtSoHieuTu_TextChanged(object sender, EventArgs e)
        {
if (!string.IsNullOrEmpty(txtSoHieuTu.Text) && !string.IsNullOrEmpty(txtSoLuong.Text))
            {
                int soHieuTu = int.Parse(txtSoHieuTu.Text);
                int soLuong = int.Parse(txtSoLuong.Text);
                int soHieuDen = soHieuTu + soLuong;
                txtSoHieuDen.Text = soHieuDen.ToString();
            }
        }

        protected void txtThanhTien_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            int DonGia = int.Parse(txtDonGia.Text);
            int SoLuong = int.Parse(txtSoLuong.Text);
            int ThanhTien = DonGia * SoLuong;
            txtThanhTien.Text = ThanhTien.ToString();
        }

        protected void btnGhi_Click(object sender, EventArgs e)
        {
            DataAccess.QLThietBi.Model.GhiTangThietBiChiTiet gtDetail = new DataAccess.QLThietBi.Model.GhiTangThietBiChiTiet()
            {
                ThietBiID = Convert.ToInt32(ddlThietBi.SelectedValue),
                NhaCungCapID = Convert.ToInt32(ddlNhaCungCap.SelectedValue),
                KhoPhongID =Convert.ToInt32(ddlKhoPhongs.SelectedValue),
                SoLuong = Convert.ToInt32(txtSoLuong.Text),
                SoHieuTu = Convert.ToInt32(txtSoHieuTu.Text),
                SoHieuDen = Convert.ToInt32(txtSoHieuDen.Text),
                DonGia = Convert.ToDecimal(txtDonGia.Text),
                ThanhTien = Convert.ToDecimal(txtThanhTien.Text),
                ThuongHieuID = Convert.ToInt32(ddlThuongHieu2.SelectedValue),
                


            };
            GhiTangThietBiChiTietBO.InsertDetailForm(gtDetail);
            LoadGrvListGhiTang();

        }
        
    }


}