<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DWQueryfeedback.aspx.cs" Inherits="product.Order.DWQueryfeedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传查询反馈</title>
       <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <div class="subnav">
        <strong>上传查询反馈 批次：<%=batchNo %></strong>
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="btnUP" runat="server" OnClick="BtnUP_Click" Text="上 传" Width="54px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
              <a style="color: Red;" href="../Order/Orderbatch.aspx">返回</a> &nbsp;
            </p>
        </div>
    </div>
    <input type="hidden" value="" id="batchNoID" runat="server" />
        <input type="hidden" value="" id="batchName" runat="server" />
    </form>
</body>
</html>
