<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpNavigationconfig.aspx.cs"
    Inherits="product.user.UpNavigationconfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function check() {
            if ($("#code").val() == "" || $("#name").val() == "") {
                alert("编码和名称不能为空");
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" name="form1" method="post" runat="server">
    <div class="subnav">
        修改
    </div>
    <div id="right">
        <table width="100%" cellpadding="2" id="shenhe">
            <tr>
                <th width="20%">
                    编码：
                </th>
                <td>
                    <input id="code" type="text" name="code" value="<%=code %>" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    功能模块：
                </th>
                <td>
                    <input id="name" type="text" name="name" value="<%=name %>" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    父节点：
                </th>
                <td>
                    <input id="Fnode" type="text" name="Fnode" value="<%=fnode %>" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    等级：
                </th>
                <td>
                    <input id="level" type="text" name="level" value="<%=level %>" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    顺序：
                </th>
                <td>
                    <input id="SF_order" type="text" name="SF_order" value="<%=SF_order %>" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    URL：
                </th>
                <td>
                    <input id="url" type="text" name="url" style="width: 30%;"  value="<%=url %>"/>
                </td>
            </tr>
             <tr>
                <th width="20%">
                    是否可用：
                </th>
                <td>
                 <input id="isdel" type="checkbox" runat="server"  value="<%=isdelName %> "  name="isdel"  />
                 
                </td>
            </tr>
            <tr>
              
                <th width="5%">
                  
                </th>
                <td colspan="3" bgcolor="#F3F7F9" align="center">
                    <a style="color: Red;" href="javascript:history.go(-1)">返回</a> &nbsp;
                    <asp:Button ID="Button1" runat="server" Text="提   交" OnClick="Button1_Click" OnClientClick="return check();" />
                </td>
                   
            </tr>
        </table>
        <input  id="hddID" runat="server" type="hidden" />    </div>
    </form>
</body>
</html>
