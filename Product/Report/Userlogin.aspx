<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Userlogin.aspx.cs" Inherits="product.Report.Userlogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录日志</title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            $.each($("input[name=QCDate]"), function () {
                $(this).focus(function () { WdatePicker({ maxDate: '#F{$dp.$D(\'QCEdate\')||\'2050-10-01\'}', dateFmt: 'yyyyMMdd' }); });
                $(this).click(function () { WdatePicker({ maxDate: '#F{$dp.$D(\'QCEdate\')||\'2050-10-01\'}', dateFmt: 'yyyyMMdd' }); });

            });

            $.each($("input[name=QCEdate]"), function () {

                $(this).focus(function () { WdatePicker({ minDate: '#F{$dp.$D(\'QCDate\')}', dateFmt: 'yyyyMMdd', readOnly: true }); });
                $(this).click(function () { WdatePicker({ minDate: '#F{$dp.$D(\'QCDate\')}', dateFmt: 'yyyyMMdd', readOnly: true }); });
            });


        });


    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="subnav">
        人员列表
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
                <strong>开始日期</strong>
                <input id="QCDate" style="width: 85px" name="QCDate" type="text" value="<%=QCDate %>" />
                <strong>结束日期</strong>
                <input id="QCEdate" style="width: 85px" name="QCEdate" type="text" value="<%=QCEDate %>" />
                <strong>用户名</strong>
                <input id="username" name="username" style="width: 95px" type="text" value="<%=username %>" />
                <asp:Button runat="server" ID="btnQuerey" Text="查 询" OnClick="btnQuerey_Click" />
            </p>
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>
        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table width="100%" id="list">
                    <tr>
                        <th width="2%">
                            ID
                        </th>
                        <th width="10%">
                            类别
                        </th>
                        <th width="10%">
                            用户名
                        </th>
                        <th width="10%">
                            登录日期
                        </th>
                        <th width="10%">
                            IP
                        </th>
                        <%-- <th width="15%">
                            操作
                        </th>--%>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("id")%>">
                    <td>
                        <%#Eval("id")%>
                    </td>
                    <td>
                        <%#Eval("logtype")%>
                    </td>
                     <td>
                        <%#Eval("log_opname")%>
                    </td>
                    <td>
                        <%#Eval("log_datetime")%>
                    </td>
                    <td>
                        <%#Eval("AddressIP")%>
                    </td>
                    <%--   
                    <td>
                        <a href="../user/UpdateUserList.aspx?id=<%#Eval("id")%>">编辑</a>|<a href="javascript:del('<%#Eval("id")%>')">删除</a>
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
    <script type="text/javascript">
        function del(id) {
            if (confirm("是否删除！")) {
                $.post("../delete/delete.aspx?table=sys_log&id=" + id, {}, function (data) {
                    if (data.result == "100") {
                        alert("删除成功！");
                        $("#base" + id)[0].style.display = "none";
                    }
                    else
                    { alert("删除失败！"); }
                }, "json");
            }
        }
    </script>
    </form>
</body>
</html>
