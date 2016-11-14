<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addproRebate.aspx.cs" Inherits="product.products.addproRebate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <style type="text/css">
    select
    {
        width:100px;
        }
    </style>
    <script type="text/javascript">

        function check() {
            var tixing = "";
            var re = /^\d+(\.\d+)?$/;

            if ($("#dpcode").val() == "请选择") {
                tixing += "CAI必填\n";
            }
            if (!re.test($("#dpFl1").val())) {
                tixing += "返利1不是有效数字\n";
            }
            if (!re.test($("#dpFl2").val())) {
                tixing += "返利2不是有效数字\n";
            }
            if (!re.test($("#dpFl3").val())) {
                tixing += "返利3不是有效数字\n";
            }
            if (!re.test($("#dpFl4").val())) {
                tixing += "返利4不是有效数字\n";
            } 
            if (!re.test($("#dpFl5").val())) {
                tixing += "返利5不是有效数字\n";
            }
            if ($("#dpYear").val() == "请选择") {
                tixing += "年份必填\n";
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
        返利信息添加
    </div>
    <div id="right">
        <table width="100%" cellpadding="2" id="shenhe">
            <tr>
                <th width="20%">
                    编码：
                </th>
                <td>
                    <asp:DropDownList ID="dpcode" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th width="20%">
                    返利1：
                </th>
                <td>
                    <input id="dpFl1" type="text" name="dpFl1" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    返利2：
                </th>
                <td>
                    <input id="dpFl2" type="text" name="dpFl2" />
                </td>
            </tr>
             <tr>
                <th width="20%">
                    返利3：
                </th>
                <td>
                    <input id="dpFl3" type="text" name="dpFl3" />
                </td>
            </tr>
             <tr>
                <th width="20%">
                    返利4：
                </th>
                <td>
                    <input id="dpFl4" type="text" name="dpFl4" />
                </td>
            </tr>
             <tr>
                <th width="20%">
                    返利5：
                </th>
                <td>
                    <input id="dpFl5" type="text" name="dpFl5" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    年份：
                </th>
                <td>
                    <asp:DropDownList ID="dpYear" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3" bgcolor="#F3F7F9" align="center">
                    <a style="color: Red;" href="javascript:history.go(-1)">返回</a> &nbsp;
                     <asp:Button ID="Button1" runat="server" Text="提   交" OnClick="Button1_Click" OnClientClick="return check();" />
                   
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
