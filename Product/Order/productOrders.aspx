<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productOrders.aspx.cs" Inherits="product.Order.productOrders" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
       <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
           
            $.each($("input[name=bdata]"), function () {
                $(this).focus(function () { WdatePicker({ maxDate: '#F{$dp.$D(\'edata\')||\'2020-10-01\'}', skin: 'whyGreen', dateFmt: 'yyyyMM', readOnly: true }); });
                $(this).click(function () { WdatePicker({ maxDate: '#F{$dp.$D(\'edata\')||\'2020-10-01\'}', skin: 'whyGreen', dateFmt: 'yyyyMM', readOnly: true }); });

            });
            $.each($("input[name=edata]"), function () {

                $(this).focus(function () { WdatePicker({ minDate: '#F{$dp.$D(\'bdata\')}', maxDate: '2020-10-01', skin: 'whyGreen', dateFmt: 'yyyyMM', readOnly: true }); });
                $(this).click(function () { WdatePicker({ minDate: '#F{$dp.$D(\'bdata\')}', maxDate: '2020-10-01', skin: 'whyGreen', dateFmt: 'yyyyMM', readOnly: true }); });
            });
         
        });
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="subnav">
        明细
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="shangchuan" runat="server"  OnClick="Button1_Click" Text="上 传" Width="54px" />

                </p>
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>
            <div class="page ">
                <strong>CAD编码</strong>
                <input id="QCAD" name="QCAD" type="text" value="<%=cad %>" />
                <strong>下单开始日期</strong>
                <input id="bdata" name="bdata" type="text" value="<%=bdate %>" />
                <strong>下单结束日期</strong>
                <input id="edata" name="edata" type="text" value="<%=edate %>" />
                <strong>城市</strong>
                <input id="TxtCity" name="TxtCity" type="text" value="<%=txtcity %>" />
                <strong>下单数量Top</strong>
                <input id="Txttop" name="Txttop" type="text" />
                <asp:Button runat="server" ID="btnQuerey" Text="查询" OnClick="btnQuerey_Click" />
            </div>
        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table width="100%" id="list">
                    <tr>
                        <th width="5%">
                            ID
                        </th>
                      
                         <th width="8%">
                            CAD
                        </th> 
                          <th width="25%">
                             DESC
                        </th>
                        <th width="5%">
                            下单数量
                        </th> 
                         <th width="5%">
                            实到
                        </th> 
                         <th width="15%">
                            下单日期
                        </th> 
                         <th width="15%">
                            进货价
                        </th> 
                        <th width="20%">
                            操作
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("ID")%>">
                    <td>
                        <%#Eval("rn")%>
                    </td>
                    <td>
                        <%#Eval("CAD")%>
                    </td>
                     <td>
                        <%#Eval("modedes")%>
                    </td>
                     <td>
                        <%#Eval("num")%>
                    </td>
                      <td>
                        <%#Eval("number")%>
                    </td>
                       <td>
                        <%=bdate %>至 <%=edate %>
                    </td>
                    <td>
                        <%#Eval("JHprice")%>
                    </td>
                      <%--   <td>
                        <%#Eval("shipmentprice")%>
                    </td>--%>
                   
                    <td>
                        <a href="../Order/productXOrders.aspx?QCAD=<%#Eval("CAD")%>&bata=<%=bdate %>&eata=<%=bdate %>&TxtCity=<%=txtcity %>"> 详细</a>
<%--                        <a href="../Order/productSDOrders.aspx?QCAD=<%#Eval("CAD")%>&bata=<%=bdate %>&eata=<%=bdate %>&TxtCity=<%=txtcity %>">实到明细 </a>--%>
                           

                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
        <div class="Pages">
            <div class="Paginator">
                <asp:Literal ID="literalPagination" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    <%--<script type="text/javascript">
//        function del(id) {
//            if (confirm("是否删除！")) {
//                $.post("../delete/delete.aspx?table=ProductPrice&id=" + id, {}, function (data) {
//                    if (data.result == "100") {
//                        alert("删除成功！");
//                        $("#base" + id)[0].style.display = "none";
//                    }
//                    else
//                    { alert("删除失败！"); }
//                }, "json");
//            }
//        }
    </script>--%>
    </form>
</body>
</html>
