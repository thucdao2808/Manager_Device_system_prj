using DataAccess;
using DataAccess.QLThietBi.BO;
using DataAccess.QLThietBi.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuanLiThietBi
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            DangNhapBO dangnhap = new DangNhapBO();
            dangnhap.InsertUser(txtEmail.Text, txtPassword.Text);


        }
    }
}