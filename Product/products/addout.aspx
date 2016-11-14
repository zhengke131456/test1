<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addout.aspx.cs" Inherits="product.products.addout" %>

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
            width: 300px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $.each($("input[name=OD]"), function () {
                $(this).focus(function () { WdatePicker({ readOnly: true }); });
                $(this).click(function () { WdatePicker({ readOnly: true }); });
            });
        });


        function check() {
           
            var tixing = "";
            var re = /^[1-9]+[0-9]*]*$/;
            if (!re.test($("#QTY").val())) {
                tixing += "数量不是正整数\n";
            }
            if ($("#rpcode").val() == "") {
                tixing += "睿配编码不能为空\n";
            }
            if ($("#dpwh").val() == "" || $("#dpwh").val()=="请选择") {
                tixing += "出库仓库不能为空\n";
            }
            if ($("#OD").val() == "") {
                tixing += "出库日期不能为空\n";
            }

         
            if (!isNaN($("#QTY").val())) {
         
                var ddl = $("#rpcode").val();


          
                var s = ddl.indexOf("数量:");
              
                var newnmu = ddl.substr(s + 3)
              
             
                if (parseInt(newnmu) < parseInt($("#QTY").val())) {

                    tixing += "出数不能大于当前库存数量\n";

                }
                
            }
            if (tixing != "") {
                alert(tixing);
                return false;
            } else {

                return true;
            }
        };

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
        出库添加
    </div>
    <div id="right">
        <div id="Div1">
            <table width="100%" cellpadding="2" id="shenhe">
                <tr>
                    <th width="20%">
                        出库仓库：
                    </th>
                    <td>
                        <asp:DropDownList ID="dpwh" OnSelectedIndexChanged="dpwh_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th width="10%">
                        睿配编码
                    </th>
                    <td>
                     <%--   <asp:DropDownList Width="200px" ID="dpproduct" runat="server">
                        </asp:DropDownList>--%>
                          <input id="rpcode" name="rpcode" runat="server" />
                    </td>
                </tr>
               <%-- <tr>
                    <th width="30%">
                        石化商品编码 ：
                    </th>
                    <td>
                        <input id="ShGoogcode" name="ShGoogcode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th width="5%">
                        供应商编码 ：
                    </th>
                    <td>
                        <input id="cad" type="text" name="cad" runat="server" />
                    </td>
                </tr>--%>
                <tr>
                    <th width="20%">
                        出库日期：
                    </th>
                    <td>
                        <input id="OD" name="OD" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th width="20%">
                        数量：
                    </th>
                    <td>
                        <input id="QTY" type="text" name="QTY" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th width="10%">
                        特殊标示 ：
                    </th>
                    <td>
                        <asp:DropDownList ID="spmark" runat="server">
                            <asp:ListItem Text="正常销售出库" Value="2"></asp:ListItem>
                            <%--<asp:ListItem Text="调拨出库" Value="4"></asp:ListItem>--%>
                            <asp:ListItem Text="特殊削减" Value="6"></asp:ListItem>
                            <asp:ListItem Text="特殊出库" Value="7"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" bgcolor="#F3F7F9" align="center">
                        <a style="color: Red;" href="../products/outproductList.aspx">返回</a> &nbsp;
                        <asp:Button ID="Button1" runat="server" Text="提   交" OnClick="Button1_Click" OnClientClick="return  check();" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="Hidpart" runat="server" />
    </form>
</body>
</html>
