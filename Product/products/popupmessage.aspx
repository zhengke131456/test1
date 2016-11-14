<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popupmessage.aspx.cs" Inherits="product.products.popupmessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            $.each($("input[name=bdata]"), function () {
                $(this).focus(function () { WdatePicker({ readOnly: true }); });
                $(this).click(function () { WdatePicker({ readOnly: true }); });

            });

        });
       
    </script>
</head>
<%--该页面只是用来获取时间  去掉了出入库操作  后台代码全部作废--%>
<body>
    <form id="form1" runat="server">
    <div class="subnav">
        
    </div>
    <div id="right">
        <div class="page ">
            <p class="left" id="addp">
            </p>
            <div class="clear">
            </div>
            <div class="page ">
                <strong>选择日期</strong>
                <input id="bdata" name="bdata" type="text" runat="server" />
                <asp:Button runat="server" ID="Button1" Text="确定" OnClientClick="return check();" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hiddtype" runat="server" />
    <asp:HiddenField ID="Hidden1" runat="server" />
    <asp:HiddenField ID="Hidden1WH" runat="server" />
    </form>
</body>
<script type="text/javascript">
    //获取时间并返回
    $("#Button1").click(function butclick() {
        var data = $("#bdata").val();

//        if (window.opener) {
//            //for chrome
//            window.opener.returnValue = data;
//        }
//        else {
            window.returnValue = data;
//        }

        window.close();
    })

    //    function check() {
    //        var obj = window.dialogArguments
    //        var typeid = obj.id.toString();
    //        var type1 = obj.type.toString();
    //        var wh = obj.wh.toString();

    //        $("#Hidden1").val(typeid);
    //        $("#hiddtype").val(type1);
    //        $("#Hidden1WH").val(wh);
    //    }
</script>
</html>
