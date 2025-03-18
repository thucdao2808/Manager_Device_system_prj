<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ThietBi.aspx.cs" Inherits="QuanLiThietBi.FormThietBi.ThietBi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link rel="stylesheet" href="../Resource/Style/ThietBi.css" />
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
                    <asp:GridView ID="grvThietBi" runat="server" CssClass="table" HeaderStyle-BackColor="White"  RowStyle-Height="40px" AutoGenerateColumns="false" HorizontalAlign="Center" DataKeyNames="ID" HeaderStyle-ForeColor="white">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDelThietBi" runat="server" CssClass="checkbox" HeaderStyle-HorizontalAlign="Center" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="STT" ReadOnly="true" />
                            <asp:TemplateField HeaderText="Sửa">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Width="20" Style="border-radius: 50%;" Height="20" ImageUrl="~/Resource/Images/pngtree-service-icon-repair-symbol-vector-png-image_8279875.png" OnCommand="Edit_Click" CommandArgument='<%# Eval("ID").ToString() %>'></asp:ImageButton>
                                  <%--  <asp:ImageButton ID="btnEditThietBi" runat="server" Width="20" Style="border-radius: 50%;" Height="20" ImageUrl="~/Resource/Images/pngtree-service-icon-repair-symbol-vector-png-image_8279875.png" CommandArgument='<%# Eval("ID").ToString() %>'></asp:ImageButton>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TenThietBi" HeaderText="Tên Thiết Bị" />
                            <asp:BoundField DataField="MaThietBi" HeaderText="Mã Thiết Bị" />
                            <asp:BoundField DataField="NhomThietBiID" HeaderText="Nhóm thiết bị" />
                            <asp:TemplateField HeaderText=" Phòng Ban Sử Dụng" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="imgPhongBanSuDung" runat="server" ImageUrl="~/Resource/Images/yes.jpg" CssClass="Icon-yes" Width="20"
                                        Height="20" Style="border-radius: 50%;"
                                        Visible='<%# Convert.ToBoolean(Eval("isPhongBanSuDung")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nhân viên Sử Dụng" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="imgNhanVienSuDung" runat="server" ImageUrl="~/Resource/Images/yes.jpg" CssClass="Icon-yes" Width="20"
                                        Height="20" Style="border-radius: 50%;"
                                        Visible='<%# Convert.ToBoolean(Eval("isNhanVienSuDung")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QL Số Lượng" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="imgQuanLyTheoSoLuong" runat="server" ImageUrl="~/Resource/Images/yes.jpg" CssClass="Icon-yes" Width="20"
                                        Height="20" Style="border-radius: 50%;"
                                        Visible='<%# Convert.ToBoolean(Eval("isQuanLyTheoSoLuong")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QL Số Hiệu" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="imgQuanLyTheoSoHieug" runat="server" ImageUrl="~/Resource/Images/yes.jpg" CssClass="Icon-yes" Width="20"
                                        Height="20" Style="border-radius: 50%;"
                                        Visible='<%# Convert.ToBoolean(Eval("isQuanLyTheoSoHieu")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MucDichSuDung" HeaderText="Mục Đích " />
                            <asp:BoundField DataField="MoTa" HeaderText="Mô Tả " />
                            <asp:TemplateField HeaderText="Nhà cung cấp" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblNhaCungCap" Text='<%# GetNhaCungCap(Eval("NhaCungCapID")) %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Thương Hiệu" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalRate" Text='<%# GetThuongHieu(Eval("ThuongHieuID")) %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Đơn vị tính" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblDonViTinh" Text='<%# GetDonViTinh(Eval("DonViTinhID")) %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sử Dụng" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="imgSuDung" runat="server" ImageUrl="~/Resource/Images/yes.jpg" CssClass="Icon-yes" Width="20"
                                        Height="20" Style="border-radius: 50%;"
                                        Visible='<%# Convert.ToBoolean(Eval("isSuDung")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tiêu Hao" HeaderStyle-CssClass="active" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Image ID="imgHeThong" runat="server" ImageUrl="~/Resource/Images/yes.jpg" Width="20"
                                        Height="20" Style="border-radius: 50%;" CssClass="Icon-yes" Visible='<%# Convert.ToBoolean(Eval("isThietBiTieuhao")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div>
                    <asp:Panel ID="PanelMain" runat="server" GroupingText="Thêm/Sửa" CssClass="maincontainer" Visible="false">
                        <asp:HiddenField ID="FieldID"
                            runat="server" />
                        <div class="Contain_btn">
                            <div class="box_btn">
                                <asp:Button ID="btnSave" CssClass="btnSave" runat="server" Text="Ghi" OnClick="btnSave_Click" />
                                <asp:Button ID="btnThoat" CssClass="btnCancel" runat="server" Text="Hủy Bỏ" />
                            </div>
                            <div>
                                <asp:ImageButton ID="btnCancelForm" runat="server" ImageUrl="~/Resource/Images/cancel.jpg" CssClass="btnCancelForm" OnClick="btnCancelForm_Click" />
                            </div>
                        </div>
                        <asp:Panel ID="PanelDeviceInfo" runat="server" GroupingText="Thông Tin Thiết Bị" CssClass="PanelDeviceInfo">
                            <div class="panelDetail1">
                                <div class="itemFlex">
                                    <div class="inputgroupdevice">
                                        <asp:Label ID="LabelDeviceName" runat="server" Text="Tên Thiết Bị" CssClass="labelstyle"></asp:Label>
                                        <asp:TextBox ID="TextBoxDeviceName" runat="server" CssClass="inputstyle"></asp:TextBox>
                                    </div>
                                    <div class="inputgroupdevice">
                                        <asp:Label ID="LblNhaCungCap" runat="server" Text="Nhà Cung Cấp" CssClass="labelstyle"></asp:Label>
                                        <asp:DropDownList ID="ddlNhaCungCap" runat="server" CssClass="dropdownstyle"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="itemFlex">
                                    <div class="inputgroupdevice">
                                        <asp:Label ID="LabelDeviceCode" runat="server" Text="Mã Thiết Bị" CssClass="labelstyle"></asp:Label>
                                        <asp:TextBox ID="TextBoxDeviceCode" runat="server" CssClass="inputstyle"></asp:TextBox>
                                    </div>
                                    <div class="inputgroupdevice">
                                        <asp:Label ID="LblThuongHieu" runat="server" Text="Thương Hiệu" CssClass="labelstyle"></asp:Label>
                                        <asp:DropDownList ID="ddlThuongHieu" runat="server" CssClass="dropdownstyle"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="itemFlex">
                                    <div class="inputgroupdevice">
                                        <asp:Label ID="LabelDeviceGroup" runat="server" Text="Nhóm Thiết Bị" CssClass="labelstyle"></asp:Label>
                                        <asp:DropDownList ID="DropDownListDeviceGroup" runat="server" CssClass="dropdownstyle">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="inputgroupdevice">
                                        <asp:Label ID="LblDonViTinh" runat="server" Text="Đơn Vị Tính" CssClass="labelstyle"></asp:Label>
                                        <asp:DropDownList ID="ddlDonViTinh" runat="server" CssClass="dropdownstyle"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="itemFlex">
                                    <div class="radiocontainer">
                                        <asp:RadioButton ID="RadioDepartment" runat="server" CssClass="radiostyle" Text="Phòng ban" />
                                        <asp:RadioButton ID="RadioEmployee" runat="server" CssClass="radiostyle" Text="Nhân viên" />
                                    </div>
                                    <div class="inputgroupdevice">
                                        <asp:Label ID="LblThietBiTieuHao" runat="server" Text="Thiết Bị Tiêu Hao" CssClass="labelstyle">
                                        </asp:Label>
                                        <asp:CheckBox ID="chkThietBiTieuHao" runat="server" CssClass="checkboxstyle" />
                                    </div>
                                </div>
                                <div class="itemFlex">
                                    <div class="radiocontainer">
                                        <asp:RadioButton ID="RadioQuantity" runat="server" CssClass="radiostyle" Text="Số Lượng" />
                                        <asp:RadioButton ID="RadioSerialNumber" runat="server" CssClass="radiostyle" Text="Thiết Bị" />
                                    </div>
                                    <div class="inputgroupdevice">
                                        <asp:Label ID="LblSuDung" runat="server" Text="Sử Dụng" CssClass="labelstyle"></asp:Label>
                                        <asp:CheckBox ID="chkSuDung" runat="server" CssClass="checkboxstyle" />
                                    </div>
                                </div>

                                <div class="inputgroupdevice">
                                    <asp:Label ID="LabelUsagePurpose" runat="server" Text="Mục Đích Sử Dụng" CssClass="labelstyle"></asp:Label>
                                    <asp:TextBox ID="TextBoxUsagePurpose" runat="server" CssClass="inputstyleTarget"></asp:TextBox>
                                </div>
                                <div class="inputgroupdevice">
                                    <asp:Label ID="lblMota" runat="server" Text="Mô tả" CssClass="labelstyle"></asp:Label>
                                    <asp:TextBox ID="TxtMota" runat="server" CssClass="inputstyleDescription"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PanelMaintenanceInfo" runat="server" GroupingText="Thông Tin Bảo Trì" CssClass="sectioncontainer">
                            <div class="detailMaintain">
                                <div class="inputgroups">
                                    <asp:Label ID="LabelMaintenanceTeam" runat="server" Text="Đội Bảo Trì" CssClass="labelstyle"></asp:Label>
                                    <asp:TextBox ID="TextBoxMaintenanceTeam" runat="server" CssClass="inputstyle"></asp:TextBox>
                                    <%--<asp:DropDownList ID="DropDownListMaintenanceTeam" runat="server" CssClass="dropdownstyle"></asp:DropDownList>--%>
                                </div>

                                <div class="inputgroups">
                                    <asp:Label ID="LabelMaintenanceFrequency" runat="server" Text="Tần Suất Bảo trì" CssClass="labelstyle"></asp:Label>
                                    <asp:TextBox ID="TextBoxMaintenanceFrequency" runat="server" CssClass="inputstyle">Ngày</asp:TextBox>
                                </div>
                            </div>
                            <div class="detailMaintain">
                                <div class="inputgroups">
                                    <asp:Label ID="LabelMaintenanceStaff" runat="server" Text="Nhân Sự Bảo Trì" CssClass="labelstyle"></asp:Label>
                                    <asp:TextBox ID="TextBoxMaintenanceStaff" runat="server" CssClass="inputstyle"></asp:TextBox>
                                    <%--<asp:DropDownList ID="DropDownListMaintenanceStaff" runat="server" CssClass="dropdownstyle"></asp:DropDownList>--%>
                                </div>
                                <div class="inputgroups">
                                    <asp:Label ID="LabelMaintenanceDuration" runat="server" Text="Thời Lượng Bảo Trì" CssClass="labelstyle"></asp:Label>
                                    <asp:TextBox ID="TextBoxMaintenanceDuration" runat="server" CssClass="inputstyle"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </form>
    </body>
    </html>
</asp:Content>
