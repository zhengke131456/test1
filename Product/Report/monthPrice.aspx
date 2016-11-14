<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="monthPrice.aspx.cs" Inherits="product.Report.monthPrice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
</head>
<body>
      <form id="form1" runat="server">
      <div class="subnav">
         每月分价格生成
    </div>
      <div id="right">
          <div class="page ">
              <p class="left">
                  <strong Height="25px">月份：</strong>
                  <asp:DropDownList Height="25px" Width="100" ID="dpyary" runat="server">
                  </asp:DropDownList>
                  &nbsp
                   &nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button runat="server" ID="btnReport"  Height="25px" Width="100px" Text="生成报表" OnClick="btnReport_Click" />
              </p>
          </div>
          <div class="clear">
          </div>
      </div>
      </form>
</body>
</html>
