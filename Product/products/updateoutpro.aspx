<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updateoutpro.aspx.cs" Inherits="product.products.updateoutpro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Scripts/time/dhtmlgoodies_calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/time/dhtmlgoodies_calendar.js" type="text/javascript"></script>
    <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        select
        {
            width: 100px;
        }
    </style>
    <script type="text/javascript">



        function check() {
            var tixing = "";
            var re = /^[1-9]+[0-9]*]*$/;
            if (!re.test($("#dpnum").val())) {
                tixing += "出货数量不是正整数\n";
            }


            if (tixing != "") {
                alert(tixing);
                return false;
            }
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="subnav">
        出库修改
    </div>
    <div id="right">
        <table width="100%" cellpadding="2" id="shenhe">
            <tr>
                <th width="20%">
                    睿配编码：
                </th>
                <td>
                    <input id="rpcode" name="rpcode" runat="server" readonly="readonly" disabled="disabled" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    出货日期：
                </th>
                <td>
                    <input id="SeriaDate" name="SeriaDate" runat="server" readonly="readonly" disabled="disabled" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    出库仓库：
                </th>
                <td>
                    <input id="wh" type="text" name="wh" runat="server" readonly=readonly disabled="disabled" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    数量：
                </th>
                <td>
                    <input id="dpnum" type="text" name="dpnum" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" bgcolor="#F3F7F9" align="center">
                    <a style="color: Red;" href="javascript:history.go(-1)">返回</a> &nbsp;
                    <asp:Button ID="Button1" runat="server" Text="提   交" OnClick="Button1_Click" OnClientClick="return  check();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
