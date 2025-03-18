<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOut.aspx.cs" Inherits="QuanLiThietBi.LogOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Resource/Style/LogOut.css" />
</head>
<body>
 <form id="form1" runat="server">
        <asp:Image ID="Image1" runat="server" CssClass="background-image" ImageUrl="~/Resource/Images/Pasted.png" />
        <div class="form-container">
            <p class="form-title">Sign Up </p>
            <div class="input-container">
                <asp:TextBox ID="txtEmail" runat="server"  CssClass="form-control" Placeholder="Enter email"></asp:TextBox>
            </div>
            <div class="input-container">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Enter password"></asp:TextBox>
            </div>
            <asp:Button ID="btnSignIn" runat="server" CssClass="submit" Text="Sign in" OnClick="btnSignIn_Click" />
            <p class="signup-link">
                No account?
                <asp:HyperLink ID="lnkSignUp" runat="server" NavigateUrl="~/LogIn.aspx">Sign In</asp:HyperLink>
            </p>
        </div>
    </form>
</body>
</html>
