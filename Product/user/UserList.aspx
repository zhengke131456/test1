<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="product.user.UserList" %>

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
        人员列表
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
                <a href="../user/addUserList.aspx"><strong>新增人员</strong></a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="shangchuan" runat="server" OnClick="Button1_Click" Text="上　传" Width="54px" />
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
                            账号
                        </th>
                        <th width="10%">
                            角色
                        </th>
                        <th width="10%">
                            区域
                        </th>
                        <th width="10%">
                            手机号
                        </th>
                        <th width="10%">
                            邮箱
                        </th>
                        <th width="15%">
                            操作
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("id")%>">
                    <td>
                        <%#Eval("id")%>
                    </td>
                    <td>
                        <%#Eval("username")%>
                    </td>
                    <td>
                        <%#Eval("part")%>
                    </td>
                    <td>
                        <%#Eval("area")%>
                    </td>
                    <td>
                        <%#Eval("phone")%>
                    </td>
                    <td>
                        <%#Eval("Email")%>
                    </td>
                    <td>
                        <a href="../user/UpdateUserList.aspx?id=<%#Eval("id")%>">编辑</a>|<a href="javascript:del('<%#Eval("id")%>')">删除</a>
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
                $.post("../delete/delete.aspx?table=userinfo&id=" + id, {}, function (data) {
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
