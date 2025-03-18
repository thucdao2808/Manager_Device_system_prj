<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="NhaCungCap.aspx.cs" Inherits="QuanLiThietBi.FormThietBi.NhaCungCap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link rel="stylesheet" href="../Resource/Style/NhaCungCapStyle.css" />
    </head>
    <body>

        <form id="form1" runat="server">
            <div>
                <div>
                    <div class="container">
                        <div class="header">
                            <image class="icon" src="../Resource/Images/iiconKho.jpg" width="20" height="20">
                                <div class="title">Quản Lý Kho Phòng</div>
                        </div>
                        <div class="actions">
                            <div>
                                <asp:Button ID="btnAdd" CssClass="btn-action btn-add" Text="Thêm" runat="server" OnClick="btnAdd_Click" />
                            </div>
                            <div>
                                <asp:Button ID="Export" CssClass="btn-action btn-export" Text="Xuất" runat="server" OnClick="Export_Click" />
                            </div>
                            <div>
                                <asp:Button ID="btnDel" CssClass="btn-action btn-delete" Text="Xóa" runat="server" OnClick="btnDel_Click" />
                            </div>
                        </div>
                        <div class="search-section">
                            <div class="box-nav">
                                <div class="search">
                                    <h4 class="search-title">Tên Nguồn cung cấp </h4>
                                    <asp:TextBox ID="txtSearch" CssClass="textbox-search" runat="server" PlaceHolder="Mời Bạn Nhập tên Nguồn  để Tìm Kiếm"></asp:TextBox>
                                    <asp:Button ID="btnSearch" CssClass="btn-search" runat="server" Text="Tìm Kiếm" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="grvNhaCungCap" runat="server" CssClass="table" HeaderStyle-BackColor="White" RowStyle-Height="40px" AutoGenerateColumns="false" HorizontalAlign="Center" DataKeyNames="ID" HeaderStyle-ForeColor="white" OnRowDataBound="grvNhaCungCap_RowDataBound">
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
                                <asp:BoundField DataField="TenNhaCungCap" HeaderText="Tên nhà cung cấp" />
                                <asp:TemplateField HeaderText="Ghi Chú">
                                    <ItemTemplate>
                                        <%--<asp:TextBox ID="txtNote" runat="server"></asp:TextBox>--%>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Eval("GhiChu") %>'></asp:TextBox>
                                        <asp:HiddenField ID="NhaCungCapID" runat="server" Value='<%# Eval("ID").ToString() %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sử dụng" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Image ID="imgChucNang" runat="server" ImageUrl="~/Resource/Images/yes.jpg" Width="20"
                                            Height="20" Style="border-radius: 50%;" CssClass="Icon-yes" Visible='<%# Convert.ToBoolean(Eval("isSuDung")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Panel ID="pnlFormAdd" runat="server" CssClass="formAdd" Visible="false">
                        <h1>Chi tiết nguồn cung cấp</h1>
                        <div class="ItemAdd">
                            <asp:Button ID="btnThem" runat="server" Text="Thêm" CssClass="btnPlus" OnClick="btnThem_Click" />
                            <asp:Button ID="btnDong" runat="server" Text="Đóng" CssClass="btncancel" OnClick="btnDong_Click" />
                        </div>
                        <div class="ItemDeltails">
                            <label for="txtTenNhaCungCap">Tên nguồn cung cấp *</label>
                            <asp:TextBox ID="txtTenNhaCungCap" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="ItemDeltails">
                            <label for="txtGhiChu">Ghi chú</label>
                            <asp:TextBox ID="txtGhiChu" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                        <div class="ItemDeltails">
                            <label for="chkSuDung">Sử dụng</label>
                            <asp:CheckBox ID="chkSuDung" runat="server" CssClass="form-checkbox" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlFormEdit" runat="server" CssClass="formEdit" Visible="false">
                        <h1>Chỉnh sửa nguồn cung cấp</h1>

                        <asp:HiddenField ID="hdfID" Value='<%# Eval("ID").ToString() %>' runat="server" />

                        <div class="formWork">
                            <asp:Button ID="btnCapNhat" runat="server" Text="Cập nhật" CssClass="btn btn-primary" OnClick="btnCapNhat_Click" />
                            <asp:Button ID="Button2" runat="server" Text="Đóng" CssClass="btn btn-secondary" />
                        </div>

                        <div class="ItemEdit">
                            <label for="lblTenNhaCungCap">Tên nguồn cung cấp *</label>
                            <asp:TextBox ID="txtNameSupply" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="ItemEdit">
                            <label for="lblGhiChu">Ghi chú</label>
                            <asp:TextBox ID="txtTakeNote" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>

                        <div class="ItemEdit">
                            <label for="lblSuDung">Sử dụng</label>
                            <asp:CheckBox ID="chkEditUse" runat="server" CssClass="form-checkbox" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </form>
    </body>
    </html>
</asp:Content>
