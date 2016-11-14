<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Navigationconfig.aspx.cs" Inherits="product.user.Navigationconfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
    <div class="subnav">
        框架配置
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
                <a href="../user/addNavigationconfig.aspx"><strong style="color:Red">新增导航</strong></a> 
                &nbsp    &nbsp
                  <strong>编码</strong>
                <input id="code" style="width: 65px" name="code" type="text" value="<%=code %>" />
                <strong>父节点</strong>
                <input id="baseClass" style="width: 65px" name="baseClass" type="text" value="<%=baseClass %>" />
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
                        <th width="5%">
                            编码
                        </th>
                        <th width="8%">
                            功能名称
                        </th>
                        <th width="3%">
                            父节点
                        </th>
                          <th width="5%">
                            节点等级
                        </th>
                          <th width="20%">
                            地址
                        </th>
                         <th width="5%">
                            是否可用
                        </th>
                        <th width="3%">
                           顺序
                        </th>
                        <th width="10%">
                            时间
                        </th>
                         
                        <th width="15%">
                            操作
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("SF_ID")%>">
                    <td>
                        <%#Eval("SF_ID")%>
                    </td>
                    <td>
                        <%#Eval("SF_rcode")%>
                    </td>
                    <td>
                        <%#Eval("SF_rname")%>
                    </td>
                    <td>
                        <%#Eval("SF_baseClass")%>
                    </td>
                     <td>
                        <%#Eval("SF_level")%>
                    </td>
                    <td>
                        <%#Eval("SF_Url")%>
                    </td>
                    <td>
                        <%#Eval("SF_del")%>
                    </td>
                     <td>
                        <%#Eval("SF_order")%>
                    </td>
                     <td>
                        <%#Eval("inserttime")%>
                    </td>
                    <td>
                        <a href="../user/UpNavigationconfig.aspx?id=<%#Eval("SF_ID")%>">编辑</a>|<a href="javascript:del('<%#Eval("SF_ID")%>')">删除</a>
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
    <script type="text/javascript">
        function del(id) {
            if (confirm("是否删除！")) {
                $.post("../delete/delete.aspx?table=Sys_Function&where='SF_ID='&id=" + id, { type: 'isdel', set: 'sf_del=1' }, function (data) {
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
