<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="NhomThietBi.aspx.cs" Inherits="QuanLiThietBi.FormThietBi.NhomThietBi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Quản Lí Nhóm Thiết Bị</title>
        <link rel="stylesheet" href="../Resource/Style/NhomThietBi.css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <div class="container">
                    <div class="header">
                        <image class="icon" src="../Resource/Images/iiconKho.jpg" width="20" height="20">
                            <div class="title">Quản Lý Nhóm Thiết Bị</div>
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
                    <asp:GridView ID="grvNhomThietBi" runat="server" CssClass="table" HeaderStyle-BackColor="White" AlternatingRowStyle-BackColor="LightGray" RowStyle-Height="40px" AutoGenerateColumns="false" HorizontalAlign="Center" DataKeyNames="ID" HeaderStyle-ForeColor="white">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDelNhomThietBi" runat="server" CssClass="checkbox" HeaderStyle-HorizontalAlign="Center" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="STT" ReadOnly="true" />
                            <asp:TemplateField HeaderText="Sửa">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEditNhomThietBi" runat="server" Width="20" Style="border-radius: 50%;" Height="20" ImageUrl="~/Resource/Images/pngtree-service-icon-repair-symbol-vector-png-image_8279875.png" CommandArgument='<%# Eval("ID").ToString() %>' OnClick="btnEditNhomThietBi_Click"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TenNhomThietBi" HeaderText="Tên Nhóm Thiết Bị" />
                            <asp:BoundField DataField="GhiChu" HeaderText="Ghi Chú" />
                            <asp:BoundField DataField="ThuTu" HeaderText="Thứ Tự" />
                            <asp:TemplateField HeaderText="Sử Dụng" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="imgSuDung" runat="server" ImageUrl="~/Resource/Images/yes.jpg" CssClass="Icon-yes" Width="20"
                                        Height="20" Style="border-radius: 50%;"
                                        Visible='<%# Convert.ToBoolean(Eval("isSuDung")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hệ Thống" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="imgHeThong" runat="server" ImageUrl="~/Resource/Images/yes.jpg" Width="20"
                                        Height="20" Style="border-radius: 50%;" CssClass="Icon-yes" Visible='<%# Convert.ToBoolean(Eval("isHeThong")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Panel ID="pnlMainEdit" runat="server" CssClass="device-group-panel" Visible="false">
                    <div class="device-group-box">
                        <h2 class="device-group-title">Thêm nhóm thiết bị</h2>
                        <asp:HiddenField ID="hdfEdit" Value='<% #Eval("ID").ToString() %>' runat="server" />
                        <div class="device-group-item">
                            <asp:Label ID="lblTenNhomEdit" runat="server" CssClass="device-group-label" Text="Tên nhóm thiết bị *"></asp:Label>
                            <asp:TextBox ID="txtTenNhomEdit" runat="server" CssClass="device-group-input"></asp:TextBox>
                        </div>

                        <div class="device-group-item">
                            <asp:Label ID="lblGhiChuEdit" runat="server" CssClass="device-group-label" Text="Ghi chú"></asp:Label>
                            <asp:TextBox ID="txtGhiChuEdit" runat="server" CssClass="device-group-input"></asp:TextBox>
                        </div>

                        <div class="device-group-item">
                            <asp:Label ID="lblThuTuEdit" runat="server" CssClass="device-group-label" Text="Thứ tự"></asp:Label>
                            <asp:TextBox ID="txtThuTuEdit" runat="server" CssClass="device-group-input"></asp:TextBox>
                        </div>

                        <div class="device-group-item">
                            <asp:Label ID="lblSuDungEdit" runat="server" CssClass="device-group-label" Text="Sử Dụng"></asp:Label>
                            <asp:CheckBox ID="chkSuDungEdit" runat="server" />
                        </div>

                        <div class="device-group-item">
                            <asp:Label ID="lblHeThongEdit" runat="server" CssClass="device-group-label" Text="Hệ Thống"></asp:Label>
                            <asp:CheckBox ID="chkHeThongEdit" runat="server" />
                        </div>


                        <div class="device-group-item">
                            <asp:Label ID="lblNhomHeThongEdit" runat="server" CssClass="device-group-label" Text="Nhóm hệ thống"></asp:Label>
                            <asp:DropDownList ID="ddlNhomHeThongEdit" runat="server" CssClass="device-group-input"></asp:DropDownList>
                        </div>

                        <div class="device-group-buttons">
                            <asp:Button ID="btnGhiVaThemEdit" runat="server" Text="Ghi và thêm" CssClass="device-group-btn" OnClick="btnGhiVaThemEdit_Click" />
                            <asp:Button ID="btnDongEdit" runat="server" Text="Đóng" CssClass="device-group-btn" OnClick="btnDongEdit_Click" />
                        </div>
                    </div>
                </asp:Panel> 
                <asp:Panel ID="pnlMain" runat="server" CssClass="device-group-panel" Visible="false">
                    <div class="device-group-box">
                        <h2 class="device-group-title">Sửa nhóm thiết bị</h2>

                        <div class="device-group-item">
                            <asp:Label ID="lblTenNhom" runat="server" CssClass="device-group-label" Text="Tên nhóm thiết bị "></asp:Label>
                            <asp:TextBox ID="txtTenNhom" runat="server" CssClass="device-group-input"></asp:TextBox>
                        </div>

                        <div class="device-group-item">
                            <asp:Label ID="lblGhiChu" runat="server" CssClass="device-group-label" Text="Ghi chú"></asp:Label>
                            <asp:TextBox ID="txtGhiChu" runat="server" CssClass="device-group-input"></asp:TextBox>
                        </div>

                        <div class="device-group-item">
                            <asp:Label ID="lblThuTu" runat="server" CssClass="device-group-label" Text="Thứ tự"></asp:Label>
                            <asp:TextBox ID="txtThuTu" runat="server" CssClass="device-group-input"></asp:TextBox>
                        </div>

                        <div class="device-group-item">
                            <asp:Label ID="lblSuDung" runat="server" CssClass="device-group-label" Text="Sử Dụng"></asp:Label>
                            <asp:CheckBox ID="chkSuDung" runat="server" />
                        </div>

                        <div class="device-group-item">
                            <asp:Label ID="lblHeThong" runat="server" CssClass="device-group-label" Text="Hệ Thống"></asp:Label>
                            <asp:CheckBox ID="chkHeThong" runat="server" />
                        </div>

                        <div class="device-group-item">
                            <asp:Label ID="lblNhomCha" runat="server" CssClass="device-group-label" Text="Nhóm thiết bị cha"></asp:Label>
                            <asp:DropDownList ID="ddlNhomCha" runat="server" CssClass="device-group-input"></asp:DropDownList>
                        </div>

                        <div class="device-group-item">
                            <asp:Label ID="lblNhomHeThong" runat="server" CssClass="device-group-label" Text="Nhóm hệ thống"></asp:Label>
                            <asp:DropDownList ID="ddlNhomHeThong" runat="server" CssClass="device-group-input"></asp:DropDownList>
                        </div>

                        <div class="device-group-buttons">
                            <asp:Button ID="btnGhiVaThem" runat="server" Text="Ghi và thêm" CssClass="device-group-btn" OnClick="btnGhiVaThem_Click" />
                            <asp:Button ID="btnDong" runat="server" Text="Đóng" CssClass="device-group-btn" OnClick="btnDong_Click" />
                        </div>
                    </div>
                </asp:Panel>

            </div>
        </form>
    </body>
    </html>
</asp:Content>
