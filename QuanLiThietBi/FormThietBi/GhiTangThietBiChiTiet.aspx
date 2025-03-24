<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="GhiTangThietBiChiTiet.aspx.cs" Inherits="QuanLiThietBi.GhiTangThietBiChiTiet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link rel="stylesheet" href="../Resource/Style/GhiTangThietBiChiTietStyle.css" />
        <style>
            .container-wrapper {
                width: 100%;
                height: 100vh;
                background-color: #f9f9f9;
            }

            /* Nhóm nút bấm */
            .group-buttons {
                display: flex;
                gap: 10px;
                margin-bottom: 15px;
            }

            .btn-style {
                padding: 8px 15px;
                border: none;
                border-radius: 4px;
                font-size: 14px;
                cursor: pointer;
                transition: 0.3s;
            }

                .btn-style:hover {
                    opacity: 0.8;
                }

            /* Bộ lọc tìm kiếm */
            .group-filters {
                margin-bottom: 20px;
            }

            .date-selection {
                display: flex;
                flex-wrap: wrap;
                gap: 15px;
            }

            .filter-box, .search-bar {
                display: flex;
                flex-direction: column;
                gap: 5px;
            }

            .label-style {
                font-weight: bold;
            }

            .textbox-style, .input-textbox {
                padding: 8px;
                border: 1px solid #ccc;
                border-radius: 4px;
                font-size: 14px;
                width: 200px;
            }

            /* GridView */
            .gridview {
                width: 100%;
                border-collapse: collapse;
                margin-top: 15px;
            }

                .gridview th, .gridview td {
                    padding: 10px;
                    text-align: center;
                    border: 1px solid #ccc;
                }

            .grid-checkbox {
                transform: scale(1.2);
                cursor: pointer;
            }

            .grid-icon {
                cursor: pointer;
            }

            .grid-price {
                font-weight: bold;
            }

           
            @media (max-width: 768px) {
                .date-selection {
                    flex-direction: column;
                }

                .textbox-style, .input-textbox {
                }

                .gridview th, .gridview td {
                    font-size: 12px;
                    padding: 6px;
                }
            }

            .pnlDetailChung {
                width: 900px;
                height: auto;
                position: fixed;
                top: 200px;
                left: 50%;
                transform: translate(-50%, -50%);
                z-index: 900;
                background-color: #f9f9f9;
                border: 3px solid #000000;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                padding: 20px;
            }

            #PanelContain {
            }

            .panel-container {
                width: 900px;
                position: fixed;
                top: 400px;
                left: 50%;
                transform: translate(-50%, -50%);
                z-index: 999;
                background-color: #f9f9f9;
                border: 3px solid #000000;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                padding: 20px;
            }

            .pnlChung {
                padding: 20px 20px;
            }

            .pnlDetailChung > .panel-container {
                margin-top: 15px;
            }

            .Box-ContainItem {
                display: flex;
                flex-direction: row;
                justify-content: space-between;
            }

            .ContainItem {
                display: flex;
                gap: 20px;
            }

            .form-groupss {
                display: flex;
                align-items: center;
                margin-bottom: 10px;
            }

                .form-groupss label {
                    width: 80px;
                    font-weight: bold;
                }

            .form-controla {
                flex: 1;
                padding: 5px;
                border: 1px solid #ccc;
                border-radius: 4px;
            }

            .lblMota {
                align-self: flex-start;
                margin-top: 5px;
            }

            .form-controls {
                width: 100%;
                height: 80px;
                padding: 5px;
                border: 1px solid #ccc;
                border-radius: 4px;
            }

            /* GridView */
            .table {
                font-size: 9px;
                border-collapse: collapse;
                margin-top: 15px;
            }

                .table th, .table td {
                    text-align: center;
                    border: 1px solid #ccc;
                    padding: 8px;
                }

            /* Nút sửa */
            .edit-button {
                cursor: pointer;
                width: 20px;
                height: 20px;
            }

            /* Nhãn hiển thị */
            .label-field {
                font-weight: bold;
            }

            /* Header số hiệu */
            th[colspan="2"] {
                text-align: center;
            }

            /* Responsive */
            @media (max-width: 768px) {
                .Box-ContainItem {
                    flex-direction: column;
                }

                .ContainItem {
                }

                .table th, .table td {
                    font-size: 12px;
                    padding: 6px;
                }
            }

            /* Căn giữa khối chứa thông tin */
            .Box-ContainItem {
                display: flex;
                justify-content: space-between;
                align-items: center;
                margin: auto;
                gap: 20px;
            }

            /* Định dạng các item bên trong */
            .ContainItem {
                flex: 1;
                min-width: 250px;
            }

            /* Ô mô tả có độ rộng lớn hơn để nằm giữa */
            .description-box {
                flex: 2;
                text-align: center;
            }

            /* Định dạng chung cho ô nhập */
            .form-controla {
                padding: 8px;
                border: 1px solid #ccc;
                border-radius: 4px;
                font-size: 14px;
            }

            /* Responsive */
            @media (max-width: 768px) {
                .Box-ContainItem {
                    flex-direction: column;
                    align-items: stretch;
                }

                .description-box {
                    text-align: left;
                }
            }


            .ghiTang-container {
            }

            .ghiTang-title {
                font-weight: bold;
                margin-top: 10px;
                border-bottom: 1px solid #ccc;
                padding-bottom: 5px;
            }

            .ghiTang-group {
                display: flex;
                align-items: center;
                margin-bottom: 10px;
            }

                .ghiTang-group label {
                    width: 120px;
                    padding-right: 10px;
                    text-align: right;
                }

                .ghiTang-group .ghiTang-input {
                    flex: 1;
                }

            .ghiTang-buttons {
                text-align: right;
                margin-top: 10px;
            }

            .butCancel {
                width: 20px;
                height: 20px;
                object-fit: cover;
                border-radius: 50%;
            }

            .bannerPnl1 {
                display: flex;
                justify-content: space-between;
                padding: 10px;
            }

            .boxGhiTang {
                position: fixed;
                top: 300px;
                left: 50%;
                transform: translate(-50%, -50%);
                z-index: 1200;
                background-color: #f9f9f9;
                border: 3px solid #000000;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="container-wrapper">
                <div class="group-buttons">
                    <asp:Button ID="btnAdd" runat="server" Text="Thêm" CssClass="btn-style btn-insert" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnExport" runat="server" Text="Xuất" CssClass="btn-style btn-download" OnClick="btnExport_Click" />
                    <asp:Button ID="btnDel" runat="server" Text="Xóa" CssClass="btn-style btn-remove" />
                </div>

                <div class="group-filters">
                    <div class="date-selection">
                        <div class="filter-box">
                            <asp:Label ID="Label1" runat="server" Text="Từ Ngày" CssClass="label-style"></asp:Label>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox-style"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate" Format="dd/MM/yyyy" />
                        </div>
                        <div class="filter-box">
                            <asp:Label ID="Label2" runat="server" Text="Đến Ngày" CssClass="label-style"></asp:Label>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox-style"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" />
                            <div class="filter-box">
                                <asp:Button ID="btnFilter" runat="server" Text="Lọc" OnClick="btnFilter_Click" CssClass="btn-style btn-searching" />
                            </div>
                        </div>

                        <div class="search-bar">
                            <asp:Label ID="Label3" runat="server" Text="Số Phiếu"></asp:Label>
                            <asp:TextBox ID="TxtSoPhieuSearch" runat="server" PlaceHolder="Nhập số phiếu" CssClass="input-textbox"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Tìm Kiếm" CssClass="btn-style btn-searching" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>

                <asp:GridView ID="grvGhiTangct" runat="server" AutoGenerateColumns="false" CssClass="gridview">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" ItemStyle-CssClass="grid-cell" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkTb" runat="server" CssClass="grid-checkbox" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" runat="server" Width="20" Height="20" CssClass="grid-icon" CommandArgument='<%#Eval("ID") %>' ImageUrl="~/Resource/Images/pen.png" OnClick="imgEdit_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SoPhieu" HeaderText="Số Phiếu" ItemStyle-CssClass="grid-cell" />
                        <asp:BoundField DataField="NgayLapPhieu" HeaderText="Ngày Nhập" ItemStyle-CssClass="grid-cell" />
                        <asp:TemplateField HeaderText="Thành tiền">
                            <ItemTemplate>
                                <asp:Label ID="GiaTien" runat="server" CssClass="grid-price" Text='<%#GetMoney(Convert.ToInt64(Eval("DonViID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="GhiChu" HeaderText="Mô tả" ItemStyle-CssClass="grid-cell" />
                    </Columns>
                </asp:GridView>
                <asp:Panel ID="pnlDetailChung" runat="server" CssClass="pnlDetailChung" Visible="false">
                    <div class="bannerPnl1">
                        <div>
                            <asp:Button ID="btnThemPhieu" runat="server" Text="Ghi" OnClick="btnThemPhieu_Click" />
                        </div>
                        <div>
                            <asp:Button ID="btnThemSua" runat="server" OnClick="btnThemSua_Click"  Text="Button" />
                        </div>
                        <div>
                            <asp:ImageButton ID="btnHuy" CssClass="butCancel" ImageUrl="~/Resource/Images/cancel.jpg" runat="server" OnClick="btnHuy_Click" />
                        </div>
                    </div>
                    <asp:Panel ID="pnlChung" runat="server" GroupingText="Thông Tin Chung">
                         <asp:HiddenField ID="PhieuId" Value='<%#Eval("ID").ToString() %>'  runat="server" />
                        <div class="Box-ContainItem">
                            <div class="ContainItem">
                                <div class="form-groupss">
                                    <label for="lblSoPhieu">Số phiếu:</label>
                                    <asp:TextBox ID="txtSoPhieu" runat="server" CssClass="form-controla" Enabled="true" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="form-groupss">
                                    <label for="txtNgayLap">Ngày lập:</label>
                                    <asp:TextBox ID="txtNgayLap" runat="server" CssClass="form-controla"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="Calendarpnl" runat="server" TargetControlID="txtNgayLap" Format="dd/MM/yyyy" />
                                </div>
                            </div>
                        </div>
                        <div class="form-groupss">
                            <label for="txtMoTa" class="lblMota">Mô tả:</label>
                            <asp:TextBox ID="txtMoTa" runat="server" CssClass="form-controls" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </asp:Panel>

                </asp:Panel>
              

                        <asp:Panel ID="PanelGrvDetail" runat="server" CssClass="panel-container" Visible="false" GroupingText="Danh sách Thiết Bị Ghi Tăng">
                            <div>
                                <asp:Button ID="Button1" runat="server" Text="Thêm vào danh sách" OnClick="Button1_Click" />
                            </div>
                            <asp:GridView ID="grvThietBi" runat="server" AutoGenerateColumns="False" CssClass="table">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="STT" />
                                    <asp:TemplateField HeaderText="Sửa">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CssClass="edit-button" ImageUrl="~/Resource/Images/pencil.png" CommandName="Edit" CommandArgument='<%#Eval("ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Kho Phòng">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" CssClass="label-field" Text='<%#GetKhoPhong(Eval("KhoPhongID")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Thiết Bị">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" CssClass="label-field" Text='<%#GetThietBi(Eval("ThietBiID")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Đơn Vị Tính">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" CssClass="label-field" Text='<%#GetDonViTinh((int)Eval("ThietBiID")) %>'> </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nhóm Thiết Bị">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" CssClass="label-field" Text='<%#GetNhomThietBi((int)Eval("ThietBiID")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SoLuong" HeaderText="SL" />
                                    <asp:TemplateField HeaderText="Số Hiệu" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <table style="width: 100%; border-collapse: collapse; font-size: smaller;">
                                                <tr>
                                                    <th colspan="2" style="background-color: #0066A2; color: white; text-align: center; border: 1px solid #ddd;">Số hiệu
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <th style="background-color: #0066A2; color: white; text-align: center; border: 1px solid #ddd;">Từ</th>
                                                    <th style="background-color: #0066A2; color: white; text-align: center; border: 1px solid #ddd;">Đến</th>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%; border-collapse: collapse; font-size: smaller;">
                                                <tr>
                                                    <td style="width: 50%; text-align: center; border: 1px solid #ddd;">
                                                        <asp:Label ID="lblSoHieuTu" runat="server" Text='<%# Eval("SoHieuTu") %>' Style="font-size: smaller;"></asp:Label>
                                                    </td>
                                                    <td style="width: 50%; text-align: center; border: 1px solid #ddd;">
                                                        <asp:Label ID="lblSoHieuDen" runat="server" Text='<%# Eval("SoHieuDen") %>' Style="font-size: smaller;"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DonGia" HeaderText="Đơn giá" DataFormatString="{0:N0}" />
                                    <asp:BoundField DataField="ThanhTien" HeaderText="Thành tiền" DataFormatString="{0:N0}" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                 


                <asp:Panel ID="PanelContain" runat="server" CssClass="boxGhiTang" Visible="false">
                    <asp:Panel ID="pnlThietBi" runat="server" GroupingText="Thiết Bị Ghi Tăng">
                        <div class="ghiTang-group">
                            <label>Nhóm thiết bị:</label>
                            <asp:DropDownList ID="ddlNhomThietBi" runat="server" CssClass="ghiTang-input">
                            </asp:DropDownList>

                            <label>Đơn vị tính:</label>
                            <asp:DropDownList ID="ddlDonVi" runat="server" CssClass="ghiTang-input"></asp:DropDownList>
                        </div>

                        <div class="ghiTang-group">
                            <label>Thương hiệu:</label>
                            <asp:DropDownList ID="ddlThuongHieuText" runat="server" CssClass="ghiTang-input"></asp:DropDownList>

                            <label>Được dùng bởi:</label>
                            <asp:DropDownList ID="ddlDuocDungBoi" runat="server" CssClass="ghiTang-input"></asp:DropDownList>
                        </div>

                        <div class="ghiTang-group">
                            <label>Thiết bị <span style="color: red">*</span>:</label>
                            <asp:DropDownList ID="ddlThietBi" runat="server" CssClass="ghiTang-input"></asp:DropDownList>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlSoLieu" runat="server" GroupingText="Số liệu ghi tăng">

                        <div class="ghiTang-group">
                            <label>Kho phòng <span style="color: red">*</span>:</label>
                            <asp:DropDownList ID="ddlKhoPhongs" runat="server" CssClass="ghiTang-input"></asp:DropDownList>

                            <label>Số lượng <span style="color: red">*</span>:</label>
                            <asp:TextBox ID="txtSoLuong" runat="server" CssClass="ghiTang-input"></asp:TextBox>
                        </div>

                        <div class="ghiTang-group">
                            <label>Số hiệu từ:</label>
                            <asp:TextBox ID="txtSoHieuTu" runat="server" OnTextChanged="txtSoHieuTu_TextChanged" CssClass="ghiTang-input"></asp:TextBox>

                            <label>Số hiệu đến:</label>
                            <asp:TextBox ID="txtSoHieuDen" OnTextChanged="txtSoHieuDen_TextChanged" runat="server" CssClass="ghiTang-input"></asp:TextBox>
                        </div>

                        <div class="ghiTang-group">
                            <label>Đơn giá <span style="color: red">*</span>:</label>
                            <asp:TextBox ID="txtDonGia" runat="server" OnTextChanged="txtDonGia_TextChanged" CssClass="ghiTang-input"></asp:TextBox>

                            <label>Thành tiền <span style="color: red">*</span>:</label>
                            <asp:TextBox ID="txtThanhTien" OnTextChanged="txtThanhTien_TextChanged" runat="server" CssClass="ghiTang-input"></asp:TextBox>
                        </div>

                        <div class="ghiTang-group">
                            <label>Nhà cung cấp <span style="color: red">*</span>:</label>
                            <asp:DropDownList ID="ddlNhaCungCap" runat="server" CssClass="ghiTang-input"></asp:DropDownList>

                            <label>Thương hiệu:</label>
                            <asp:DropDownList ID="ddlThuongHieu2" runat="server" CssClass="ghiTang-input"></asp:DropDownList>
                        </div>

                        <div class="ghiTang-group">
                            <label>Ngày mua <span style="color: red">*</span>:</label>
                            <asp:TextBox ID="txtNgayMua" runat="server" CssClass="ghiTang-input"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="CldNgayMua" runat="server" TargetControlID="txtNgayMua" Format="dd/MM/yyyy" />

                            <label>Ngày hết hạn:</label>
                            <asp:TextBox ID="txtNgayHetHan" runat="server" CssClass="ghiTang-input"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="CldNgayHetHan" runat="server" TargetControlID="txtNgayHetHan" Format="dd/MM/yyyy" />
                        </div>

                        <div class="ghiTang-group">
                            <label>Ghi chú:</label>
                            <asp:TextBox ID="txtGhiChu" runat="server" TextMode="MultiLine" CssClass="ghiTang-input"></asp:TextBox>
                        </div>
                    </asp:Panel>
                    <div>
                        <div class="ghiTang-buttons">
                            <asp:Button ID="btnGhi" runat="server" Text="Ghi" CssClass="ghiTang-btn" OnClick="btnGhi_Click" />
                            <asp:Button ID="btnDong" runat="server" Text="Đóng" CssClass="ghiTang-btn-danger" OnClick="btnDong_Click" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </form>
    </body>
    </html>
</asp:Content>
