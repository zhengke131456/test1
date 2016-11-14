<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addproPrice.aspx.cs" Inherits="product.products.addproPrice" %>

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
            if (!re.test($("#DTPFPrice").val())) {
                tixing += "批发价格不是正整数\n";
            }
            if (!re.test($("#DTJHPrice").val())) {
                tixing += "进货价格不是正整数\n";
            }
            if ($("#dpYear").val() == "请选择") {
                tixing += "年份必填\n";
            }
            if (tixing != "") {
                alert(tixing);
                return false;
            }
            return  true;
         }

        </script>
</head>
<body>
    <form id="form1" runat="server">
       <div class="subnav">
        产品价格添加
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
                    批发价格：
                </th>
                <td>
                    <input id="DTPFPrice" type="text" name="DTPFPrice" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    进货价格：
                </th>
                <td>
                    <input id="DTJHPrice" type="text" name="DTJHPrice" />
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
