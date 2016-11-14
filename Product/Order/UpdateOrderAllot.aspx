<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateOrderAllot.aspx.cs" Inherits="product.Order.UpdateOrderAllot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
   
</head>
<body >
    <form id="form1" name="form1" method="post" runat="server" onsubmit="return check();">
    <div class="subnav">
        修改单据
    </div>
    <div id="right">
        <table width="100%" cellpadding="2" id="shenhe">
            <tr id="codeid">
                <th width="20%">
                    查货日期：
                </th>
                <td>
                    <input id="QueryDate" type="text" name="QueryDate" value="<%=QueryDate %>" disabled=disabled readonly=readonly style="background:#CCCCCC"  />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    CAD：
                </th>
                <td>
                    <input id="CAD" type="text" name="CAD" value="<%=CAD %>" disabled=disabled  style="background:#CCCCCC"  />
                </td>
            </tr>
            <tr id="trart">
                <th width="20%">
                    状态：
                </th>
                <td>
                    <input id="status" type="text" name="status" value="<%=status %>" disabled="disabled" readonly="true"  style="background:#CCCCCC" />
                </td>
            </tr>
            <tr id="tr1">
                <th width="20%">
                    查询数量：
                </th>
                <td>
                    <input id="number" type="text" name="number" value="<%=number %>"  isabled="disabled" readonly="true"  style="background:#CCCCCC"/>
                </td>
            </tr>
             <tr id="tr4">
                <th width="20%">
                    分配数：
                </th>
                <td>
                    <input id="allotNum" type="text" name="allotNum" value="<%=allotNum %>"  />
                </td>
            </tr>
            <tr id="tr2">
                <th width="20%">
                    客户编码：
                </th>
                <td>
                    <input id="ClientCode" type="text" name="ClientCode" value="<%=ClientCode %>" readonly="true"  disabled=disabled style="background:#CCCCCC"  />
                </td>
            </tr>
            <tr id="tr3">
                <th width="20%">
                    下单人：
                </th>
                <td>
                    <input id="placeorderName" type="text" name="placeorderName" style="background:#CCCCCC"  disabled=disabled value="<%=placeorderName %>" readonly="true"  />
                </td>
            </tr>
            <tr>
                <td colspan="3" bgcolor="#F3F7F9" align="center">
                    <a style="color: Red;"  href="../Order/UpOrderAllot.aspx?batch=<%=batchmark%>">返回</a> &nbsp;&nbsp;<asp:Button
                        ID="Button1" runat="server" Text="提   交" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hiddID" runat="server" />
    </form>
</body>
</html>
