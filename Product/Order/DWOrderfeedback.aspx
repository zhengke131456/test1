﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DWOrderfeedback.aspx.cs" Inherits="product.Order.DWOrderfeedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传下单反馈</title>
       <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
      <div class="subnav">
        <strong>上传下单反馈</strong>
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="btnUP" runat="server" OnClick="BtnUP_Click" Text="上 传" Width="54px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
            </p>
        </div>
    </div>
    </form>
</body>
</html>