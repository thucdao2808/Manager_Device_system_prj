<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="QuanLiThietBi.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Resource/Style/LoginStyle.css" />
</head>
<body>
    <form id="form1" runat="server">
            <asp:Image ID="Image" runat="server" CssClass="imgFull" ImageUrl="~/Resource/Images/Pasted.png"></asp:Image>
        <div class="card">
            <h4 class="title">Log In!</h4>
            <asp:Panel ID="pnlLogin" runat="server">
                <div class="field">
                    <asp:Image ID="imgEmail" runat="server" ImageUrl="~/Resource/Images/email.png" CssClass="input-icon" />
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="input-field" Placeholder="Email" ></asp:TextBox>
                </div>
                <div class="field">
                    <asp:Image ID="imgPassword" runat="server" ImageUrl="~/Resource/Images/password.jpg" CssClass="input-icon" />
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="input-field" Placeholder="Password" TextMode="Password"></asp:TextBox>
                </div>
                <div>
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn" Text="Login" OnClick="btnLogin_Click" />
                    <asp:Button ID="btnLogOut" runat="server" CssClass="btn" Text="LogOut" OnClick="btnLogOut_Click" />
                </div>
                <asp:HyperLink ID="lnkForgotPassword" runat="server" CssClass="btn-link" NavigateUrl="#">Forgot your password?</asp:HyperLink>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
