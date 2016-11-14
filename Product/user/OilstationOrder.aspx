<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OilstationOrder.aspx.cs"
    Inherits="product.user.OilstationOrder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Scripts/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            $.each($("input[name=bdata]"), function () {
                $(this).focus(function () { WdatePicker({ maxDate: '#F{$dp.$D(\'edata\')||\'2020-10-01\'}', readOnly: true }); });
                $(this).click(function () { WdatePicker({ maxDate: '#F{$dp.$D(\'edata\')||\'2020-10-01\'}', readOnly: true }); });

            });
            $.each($("input[name=edata]"), function () {

                $(this).focus(function () { WdatePicker({ minDate: '#F{$dp.$D(\'bdata\')}', maxDate: '2020-10-01', readOnly: true }); });
                $(this).click(function () { WdatePicker({ minDate: '#F{$dp.$D(\'bdata\')}', maxDate: '2020-10-01', readOnly: true }); });
            });

            if ($("#hiddpart").val() == "信息查看员") {
                document.getElementById("addp").style.display = "none";

            }
        });
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="subnav">
        客户订单
    </div>
    <div id="right">
        <div class="page ">
            <p class="left" id="addp">
            </p>
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>
            <div class="page ">
                <strong>开始日期</strong>
                <input id="bdata" name="bdata" type="text" value="<%=Bdate %>" />
                <strong>结束日期</strong>
                <input id="edata" name="edata" type="text" value="<%=Edate %>" />
                <strong>客户ID</strong>
                <input id="userid" name="userid" type="text" value="<%=userid %>" />
                <asp:Button runat="server" ID="btnQuerey" Text="查询" OnClick="btnQuerey_Click" />
            </div>
        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table id="list">
                    <tr>
                        <th width="6%">
                            客户ID
                        </th>
                        <th width="5%">
                            预约号
                        </th>
                        <th width="8%">
                            客户名称
                        </th>
                        <th width="5%">
                            联系电话
                        </th>
                        <th width="15%">
                            站点
                        </th>
                        <th width="10%">
                            预约日期
                        </th>
                          <th width="15%">
                            参数
                        </th>
                        <th width="3%">
                            数量
                        </th>
                        <th width="3%">
                            价格
                        </th>
                        <th width="5%">
                            订单状态
                        </th>
                        <th width="5%">
                            站内/站外
                        </th>
                        <th width="15%">
                            评价
                        </th>
                        <th width="10%">
                            取消原因
                        </th>
                       <%-- <th width="8%">
                            操作
                        </th>--%>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("id")%>">
                    <td>
                        <%#Eval("UserID")%>
                    </td>
                    <td>
                        <%#Eval("yuyuenum")%>
                    </td>
                    <td>
                        <%#Eval("userName")%>
                    </td>
                    <td>
                        <%#Eval("usertel")%>
                    </td>
                    <td>
                     <%#Eval("youzhan")%>
                  
                    </td>
                    <td>
                        <%#Eval("marktime", "{0:yyyy-MM-dd hh:mm}")%>
                    </td>
                     <td>
                    <%#Eval("canshu")%>
                    </td>
                    <td>
                    <%#Eval("quantity")%>
                    </td>
                    <td>
               <%#Eval("price")%>
                    </td>
                    <td>
                        <%#Eval("statustype")%>
                    </td>
                    <td>
                        <%#Eval("orderclass")%>
                    </td>
                    <td>
                        <%#Eval("pingjia")%>
                    </td>
                    <td>
                        <%#Eval("cancelreason")%>
                    </td>
                 
                 <%--   <td>
                        <a href="../products/updateinpro.aspx?id=<%#Eval("id")%>">编辑</a>|
                       <a href="javascript:del('<%#Eval("id")%>')">
                            已完成</a>
                    </td>--%>
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
    <asp:HiddenField ID="hiddtype" runat="server" />|
    <asp:HiddenField ID="hiddpart" runat="server" />
    <script type="text/javascript">
        function del(id) {
            if (confirm("是否修改状态为已完成！")) {
                $.post("../Handler/Upphonestate.ashx?id=" + id, {}, function (data) {
                    if (data == "100") {
                        alert("修改成功！");
                        location.reload();
                    }
                    else
                    { alert("修改失败！"); }
                }, "json");
            }
        }
    </script>
    </form>
</body>
</html>
