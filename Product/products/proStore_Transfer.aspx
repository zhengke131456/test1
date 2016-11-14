<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="proStore_Transfer.aspx.cs"
    Inherits="product.products.proStore_Transfer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.mockjax.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        var outArray = new Array();
        var inArray = new Array();
        //验证
        function check() {
            var outname = $("#outproduct").val();
            var inname = $("#inproduct").val();
            if ($.inArray(outname, outArray) == -1) {
                alert("出库仓库不存在或权限不足");
                return false;
            }
            if ($.inArray(inname, inArray) == -1) {
                alert("入库仓库不存在或权限不足");
                return false;
            }
            if (outname == inname) {
                alert("出库仓库与入库仓库不能相同");
                return false;
            }
            if ($("#dpproduct").val() == "请选择" || $("#dpproduct").val() == null) {

                alert("产品信息不能为空");
                return false;
            }

            if (!isNaN($("#Zhnum").val())) {


                var ddl = document.getElementById("dpproduct")

                var index = ddl.selectedIndex;
                var a = ddl.options[index].text;

                var s = a.indexOf("数量:");

                var newnmu = a.substr(s + 3)


                if (parseInt(newnmu) < parseInt($("#Zhnum").val())) {
                    alert("调拨数不能大于当前库存数量");
                    return false;

                }
            }
            else {
                alert("调拨数不是有效数字");
                return false;
            }


        };


        //自动填充




        function outnameload() {
            //获取油站列表
            var outname = $("#outname").val();
            if (outname != "")
                $("#outproduct").attr('value', outname);

            //添加数组（出库）
            var str = $("#outnamelist").val();
            str = str + "";
            outArray = str.split(',');

            //添加数组（入库）
            var str = $("#innamelist").val();
            str = str + "";
            inArray = str.split(',');

            //设置input权限
            //$("#outproduct").attr("autocomplete", "on");
            //$("#inproduct").attr("autocomplete", "on");


            //入库自动填充
            $(function () {
                $("#inproduct").autocomplete(inArray, {
                    minChars: 1,
                    maxHeight: 300,
                    lookup: inArray,
                    params: { orgNameCom: $('#inproduct').val() },
                    showNoSuggestionNotice: true,
                    noSuggestionNotice: 'Sorry, no matching results'
                }).result(function () {
                    var inname = $("#inproduct").val();
                    $("#inname").val(inname);
                });
            })

            //出库自动填充
            $(function () {
                $("#outproduct").autocomplete(outArray, {
                    minChars: 1,
                    maxHeight: 300,
                    lookup: outArray,
                    params: { orgNameCom: $('#outproduct').val() },
                    showNoSuggestionNotice: true,
                    noSuggestionNotice: 'Sorry, no matching results'
                }).result(function () {
                    var outname = $("#outproduct").val();
                    $("#outname").val(outname);
                    window.location = "proStore_Transfer.aspx?outname=" + outname;
                });
            })
        }
    </script>
</head>
<body onload="outnameload()">
    <form id="form1" runat="server">
    <div class="subnav">
        新建调拨单
    </div>
    <%--    <div id="right2">
        <div class="page ">
            <p class="left">
                <strong>调拨出库</strong>
                <asp:DropDownList Width="200px" ID="dpstore" runat="server" OnSelectedIndexChanged="dpstore_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp; <strong>睿配编码</strong>
                <asp:DropDownList Width="600px" ID="dpproduct" runat="server">
                </asp:DropDownList>
            </p>
            <div class="clear">
            </div>
            <div class="page ">
                <strong>调拨入库</strong>
                <asp:DropDownList Width="200" ID="dpstoreNew" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp; <strong>调拨数量</strong>
                <input id="Zhnum" name="Zhnum" type="text" runat="server" />
                <asp:Button runat="server" ID="btnOK" Width="100px" Text="确定" OnClick="BtnOK_Click"
                    OnClientClick="return check()" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a style="color: Red; width: 80px; height: 3px;
                    font-size: 20px;" href="../products/Transferslip.aspx">返回</a>
            </div>
        </div>
    </div>--%>
    <div id="right">
        <div class="page ">
            <p class="left">
                <strong>&nbsp;调拨出库</strong>
                <input id="outproduct" name="outproduct" type="text" class="edit_input" value=""
                    size="35" />
                &nbsp;&nbsp; <strong>睿配编码</strong>
                <asp:DropDownList Width="600px" ID="dpproduct" runat="server" AutoPostBack="true">
                </asp:DropDownList>
            </p>
            <div class="clear">
            </div>
            <div class="page ">
                <strong>调拨入库</strong>
                <input id="inproduct" name="inproduct" type="text" class="edit_input" value="" size="33" />
                &nbsp;&nbsp; <strong>调拨数量</strong>
                <input id="Zhnum" name="Zhnum" type="text" runat="server" value="" />
                <asp:Button runat="server" ID="Button1" Width="100px" Text="确定" OnClick="BtnOK_Click"
                    OnClientClick="return check()" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a style="color: Red; width: 80px; height: 30px;
                    font-size: 20px;" href="../products/Transferslip.aspx">返回</a>
            </div>
        </div>
        <br />
        <hr />
        <h4>
            新建调拨已修改：</h4>
        <p>
            调拨出库：加入跨区调拨，可输入所有的仓库，有自动检索。<br />
            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;例：从“北京仓库”调拨出库，输入“北”或者“北京”，然后鼠标选择北京仓库即可<br />
            调拨入库：只允许输入有权限测仓库，有自动检索。<br/>
            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;例：调拨到“上海仓库”，输入“上”或者“上海”，输入“仓库”则无效，然后鼠标选择上海仓库

        </p>
    </div>
    <asp:HiddenField ID="Hidddistrict" runat="server" />
    <asp:HiddenField ID="Hidpart" runat="server" />
    <asp:HiddenField ID="outname" runat="server" />
    <asp:HiddenField ID="inname" runat="server" />
    <asp:HiddenField ID="outnamelist" runat="server" />
    <asp:HiddenField ID="innamelist" runat="server" />
    </form>
</body>
</html>
