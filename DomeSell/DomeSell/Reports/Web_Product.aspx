<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Web_Product.aspx.cs" Inherits="DomeSell.Reports.Web_Product" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายงานเพิ่มสินค้าเข้าสต๊อก</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div class="row container">
            <div class="col-md-4">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Button ID="Button1" runat="server" Text="ค้นหา" OnClick="Button1_OnClick" CssClass="btn btn-success" />
                <asp:Button ID="Button2" runat="server" Text="ย้อนกลับ" OnClick="Button2_OnClick" CssClass="btn btn-info" />
            </div>
        </div>
        <br />
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" SizeToReportContent="True" Width="100%" ZoomMode="FullPage" fullScreenMode="true">
        </rsweb:ReportViewer>
    </form>
</body>
</html>
