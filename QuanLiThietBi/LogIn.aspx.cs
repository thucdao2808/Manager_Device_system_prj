using DataAccess.QLThietBi.BO;
using DataAccess.QLThietBi.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLiThietBi
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private QuanLyThietBiEntities db = new QuanLyThietBiEntities();
        private DangNhapBO dangnhap = new DangNhapBO();
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "" || txtPassword.Text == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vui lòng nhập đầy đủ email và mật khẩu!');", true);
            }
            else
            {
                var user = db.DangNhaps.FirstOrDefault(u => u.UserName == txtEmail.Text);
                if (user != null && user.Password == txtPassword.Text)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Đăng Nhập Thành Công');", true);
                    Response.Redirect("/FormThietBi/ThietBi.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Đăng Nhập Thất Bại');", true);
                }
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("LogOut.aspx");
        }
    }
    }

