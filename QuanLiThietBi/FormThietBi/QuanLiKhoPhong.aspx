<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuanLiKhoPhong.aspx.cs" MasterPageFile="~/Site.Master"Inherits="QuanLiThietBi.QuanLiKhoPhong" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản Lý Kho Phòng</title>
    <link rel="stylesheet" href="../Resource/Style/KhoPhongStyle.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <image class="icon" src="../Resource/Images/iiconKho.jpg" width="20" height="20">
                    <div class="title">Quản Lý Kho Phòng</div>
            </div>
            <div class="actions">
                <div>
                    <asp:Button ID="Add" CssClass="btn-action btn-add" Text="Thêm" runat="server" OnClick="Add_Click" />
                </div>
                <div>
                    <asp:Button ID="Export" CssClass="btn-action btn-export" Text="Xuất" runat="server" OnClick="Export_Click" />
                </div>
                <div>
                    <asp:Button ID="btnDel" CssClass="btn-action btn-delete" Text="Xóa" runat="server" OnClick="btnDel_Click" />
                </div>
            </div>
        </div>

        <div class="search-section">
            <div class="box-nav">
                <div class="filter">
                    <h4 class="filter-title">Xếp Loại</h4>
                    <asp:DropDownList ID="DropDownList1" CssClass="dropdown-filter" runat="server"></asp:DropDownList>
                </div>
                <div class="search">
                    <h4 class="search-title">Tên/Kho Phòng</h4>
                    <asp:TextBox ID="txtSearch" CssClass="textbox-search" runat="server" PlaceHolder="Mời Bạn Nhập Kho Phòng để Tìm Kiếm"></asp:TextBox>
                    <asp:Button ID="btnSearch" CssClass="btn-search" runat="server" Text="Tìm Kiếm" OnClick="btnSearch_Click" />
                </div>
            </div>
            <div></div>
        </div>
        <div class="table-container">
            <asp:GridView ID="gvKhoPhong" runat="server" CssClass="table" HeaderStyle-BackColor="White" AlternatingRowStyle-BackColor="LightGray" RowStyle-Height="40px" AutoGenerateColumns="false" HorizontalAlign="Center" DataKeyNames="ID" HeaderStyle-ForeColor="white">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkDel" runat="server" CssClass="checkbox" HeaderStyle-HorizontalAlign="Center" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="STT" ReadOnly="true" />
                    <asp:TemplateField HeaderText="Sửa">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" Width="20" Style="border-radius: 50%;" Height="20" ImageUrl="~/Resource/Images/pngtree-service-icon-repair-symbol-vector-png-image_8279875.png" OnClick="btnEdit_Click" CommandArgument='<%# Eval("ID").ToString() %>'></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TenKhoPhong" HeaderText="Tên kho/phòng" />
                    <asp:BoundField DataField="XepLoaiKhoPhongID" HeaderText="Xếp loại kho/phòng" />
                    <asp:BoundField DataField="NguoiPhuTrachID" HeaderText="Người phụ trách" />
                    <asp:TemplateField HeaderText="Là Phòng Chức Năng" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Image ID="imgChucNang" runat="server" ImageUrl="~/Resource/Images/yes.jpg" CssClass="Icon-yes" Width="20"
                                Height="20" Style="border-radius: 50%;"
                                Visible='<%# Convert.ToBoolean(Eval("isPhongChucNang")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sử dụng" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Image ID="immgChucNang" runat="server" ImageUrl="~/Resource/Images/yes.jpg" Width="20"
                                Height="20" Style="border-radius: 50%;" CssClass="Icon-yes" Visible='<%# Convert.ToBoolean(Eval("isSuDung")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <asp:Panel ID="Panel1" runat="server" CssClass="panel-container" Visible="false">

            <div class="header">
                <img src="../Resource/Images/Khohang.jpg" width="20" height="20" class="icon" />
                <span class="title">Chi tiết kho/phòng</span>
            </div>

            <div class="button-group">
                <asp:Button ID="btn_add" runat="server" CssClass="btn btn-secondary" Text="Ghi và thêm" OnClick="btn_add_Click" />
                <asp:Button ID="btn_cancel" runat="server" CssClass="btn btn-danger" Text="Đóng" OnClick="btn_cancel_Click" />
            </div>
            <div class="form-group">
                <asp:Label ID="Lbl_name" runat="server" CssClass="label" Text="Tên kho/phòng *"></asp:Label>
                <asp:TextBox ID="txtTenKhoPhong" runat="server" CssClass="textbox"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="Lbl_ThuTu" runat="server" CssClass="label" Text="Xếp loại"></asp:Label>
                <asp:TextBox ID="txtXepLoai" runat="server" CssClass="textbox"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="Lbl_nguoiPhutrach" runat="server" CssClass="label" Text="Người phụ trách"></asp:Label>
                <asp:TextBox ID="txtNguoiPhuTrach" CssClass="textbox" runat="server"></asp:TextBox>
            </div>

            <div class="form-group checkbox-group">
                <asp:CheckBox ID="chkPhongChucNang" runat="server" CssClass="checkbox" />
                <asp:Label ID="Lbl_PhongChucNang" runat="server" CssClass="label" Text="Là phòng chức năng"></asp:Label>
            </div>

            <div class="form-group checkbox-group">
                <asp:CheckBox ID="chkSuDung" runat="server" CssClass="checkbox" />
                <asp:Label ID="Lbl_SuDung" runat="server" CssClass="label" Text="Sử dụng"></asp:Label>
            </div>

        </asp:Panel>
        <asp:Panel ID="PanelEdit" runat="server" CssClass="panel-container" Visible="false">
            <div class="header">
                <img src="../Resource/Images/Khohang.jpg" width="20" height="20" class="icon" />
                <span class="title">Sửa kho/phòng</span>
            </div>
            <asp:HiddenField ID="hiddenFieldID" runat="server" />
            <div class="button-group">
                <asp:Button ID="btn_update" runat="server" CssClass="btn btn-secondary" Text="Cập nhật" OnClick="btn_update_Click" />
                <asp:Button ID="btn_cancel_edit" runat="server" CssClass="btn btn-danger" Text="Đóng" OnClick="btn_cancel_edit_Click" />
            </div>
            <div class="form-group">
                <asp:Label ID="Lbl_edit_name" runat="server" CssClass="label" Text="Tên kho/phòng *"></asp:Label>
                <asp:TextBox ID="txtEditTenKhoPhong" runat="server" CssClass="textbox"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="Lbl_edit_ThuTu" runat="server" CssClass="label" Text="Xếp loại"></asp:Label>
                <asp:TextBox ID="txtEditXepLoai" runat="server" CssClass="textbox"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="Lbl_edit_nguoiPhutrach" runat="server" CssClass="label" Text="Người phụ trách"></asp:Label>
                <asp:TextBox ID="txtEditNguoiPhuTrach" runat="server"></asp:TextBox>
            </div>

            <div class="form-group checkbox-group">
                <asp:CheckBox ID="chkEditPhongChucNang" runat="server" CssClass="checkbox" />
                <asp:Label ID="Lbl_edit_PhongChucNang" runat="server" CssClass="label" Text="Là phòng chức năng"></asp:Label>
            </div>

            <div class="form-group checkbox-group">
                <asp:CheckBox ID="chkEditSuDung" runat="server" CssClass="checkbox" />
                <asp:Label ID="Lbl_edit_SuDung" runat="server" CssClass="label" Text="Sử dụng"></asp:Label>
            </div>
        </asp:Panel>
    </form>
</body>
</html>
    </asp:Content>
