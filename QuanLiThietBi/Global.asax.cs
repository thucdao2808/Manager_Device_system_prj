using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace QuanLiThietBi
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-3.6.0.min.js",
                DebugPath = "~/Scripts/jquery-3.6.0.js",
                CdnPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js",
                CdnDebugPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.js"
            });
            Application["SoLuongTruyCap"] = 0;
            Application["SoLuongOnline"] = 0;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
           Application["SoLuongTruyCap"] = Convert.ToInt32(Application["SoLuongTruyCap"]) + 1;
            Application["SoLuongOnline"] = Convert.ToInt32(Application["SoLuongOnline"]) + 1;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Response.Redirect("ThongBaoLoi.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application["SoLuongTruyCap"] = Convert.ToInt32(Application["SoLuongTruyCap"]) -1;
            Application["SoLuongOnline"] = Convert.ToInt32(Application["SoLuongOnline"]) - 1;
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}