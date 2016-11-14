<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="placeorder.aspx.cs" Inherits="product.Order.placeorder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
        <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>
     <script type="text/javascript">

         $(function () {

             $.each($("input[name=QCDate]"), function () {
                 $(this).focus(function () { WdatePicker({ dateFmt: 'yyyyMMdd' }); });
                 $(this).click(function () { WdatePicker({ dateFmt: 'yyyyMMdd' }); });

             });

         });
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="subnav">
         下单上传
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="FileUpload1" runat="server" />
                   <asp:Button ID="btnUP" runat="server"  OnClick="BtnUP_Click" Text="单据上传" Width="54px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <%--  <asp:Button ID="btnDW" runat="server"   OnClick="BtnDW_Click" Text="单据下载" Width="54px" />--%>

             <a href="../link/order.csv" style="color:Red"><strong>标准上传文件格式下载</strong></a>
                </p>
         
            <div class="clear">
            </div>
            
        </div>
        
        <div class="Pages">
            <div class="Paginator">
                <asp:Literal ID="literalPagination" runat="server"></asp:Literal>
            </div>
        </div>
    </div>

    </form>
</body>
</html>
