<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addin.aspx.cs" Inherits="product.products.addin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Scripts/time/dhtmlgoodies_calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/time/dhtmlgoodies_calendar.js" type="text/javascript"></script>
    <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        select
        {
            width: 100px;
        }
        .ui-autocomplete
        {
            max-height: 200px;
            overflow-y: auto; /* 防止水平滚动条 */
            overflow-x: hidden;
            background-color: #CCCCCC;
            width: 200px;
        }
    </style>
    <script type="text/javascript">

        $(function () {
            $.each($("input[name=OD]"), function () {

                $(this).focus(function () { WdatePicker({ readOnly: true }); });
                $(this).click(function () { WdatePicker({ readOnly: true }); });
            });

            $.each($("input[name=FHDate]"), function () {
                $(this).focus(function () { WdatePicker({ readOnly: true }); });
                $(this).click(function () { WdatePicker({ readOnly: true }); });
            });
            $.each($("input[name=SHDate]"), function () {
                $(this).focus(function () { WdatePicker({ readOnly: true }); });
                $(this).click(function () { WdatePicker({ readOnly: true }); });
            });
            $.each($("input[name=THData]"), function () {
                $(this).focus(function () { WdatePicker({ readOnly: true }); });
                $(this).click(function () { WdatePicker({ readOnly: true }); });
            });
        });

        function check() {
            var tixing = "";
            var re = /^[1-9]+[0-9]*]*$/;

            if ($("#rpcode").val() == "") {
                tixing += "睿配编码\n";
            }
            if (!re.test($("#QTY").val())) {
                tixing += "数量不是正整数\n";
            }


            if ($("#dpwh").val() == "请选择") {
                tixing += "仓库名必填\n";
            }
            if (tixing != "") {
                alert(tixing);
                return false;
            }
            return true;
        }





    </script>

    <script>

        $(function () {
           var availableTags =<%=result%>;
            $("#rpcode").autocomplete({
                source: availableTags
            });

            $("#rpcode").data("autocomplete")._renderItem = function(ul, item) {
					return $("<li></li>")
						.data("item.autocomplete", item)
						.append("<a>" + item.label + "</a>")
						.appendTo(ul);
				};
				

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="subnav">
        进库添加
    </div>
    <div id="right">
        <table width="100%" cellpadding="2" id="shenhe">
            <tr>
                <th width="10%">
                    睿配编码 ：
                </th>
                <td>
                    <input id="rpcode" name="rpcode" runat="server" />
                </td>
            </tr>
           
            <tr>
                <th width="8%">
                    下单日期：
                </th>
                <td>
                    <input id="OD" type="text" name="OD" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="8%">
                    发货日期：
                </th>
                <td>
                    <input id="FHDate" type="text" name="FHDate" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="8%">
                    到货日期：<span style="color:Red">*</span>
                </th>
                <td>
                    <input id="SHDate" type="text" name="SHDate" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="8%">
                    退货日期：
                </th>
                <td>
                    <input id="THData" type="text" name="THData" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="8%">
                    数量：
                </th>
                <td>
                    <input id="QTY" type="text" name="QTY" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="10%">
                    仓库名：
                </th>
                <td>
                    <asp:DropDownList ID="dpwh" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th width="10%">
                    特殊标示 ：
                </th>
                <td>
                    <asp:DropDownList ID="spmark" runat="server">
                        <asp:ListItem Text="正常入库" Value="1"></asp:ListItem>
                        <asp:ListItem Text="退货入库" Value="3"></asp:ListItem>
                        <%--<asp:ListItem Text="仓库调拨" Value="4"></asp:ListItem>--%>
                        <asp:ListItem Text="货品内销" Value="5"></asp:ListItem>
                        <asp:ListItem Text="特殊入库" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th width="8%">
                    价格：
                </th>
                <td>
                    <input id="inprice" type="text" name="inprice" runat="server" />
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
    <asp:HiddenField ID="Hidpart" runat="server" />
    </form>
</body>
</html>
