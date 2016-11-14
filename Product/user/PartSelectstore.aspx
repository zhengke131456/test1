<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartSelectstore.aspx.cs"
    Inherits="product.user.PartSelectstore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择仓库</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../JS/Tree/codebase/dhtmlxtree.css" rel="stylesheet" type="text/css" />
    <script src="../JS/Tree/codebase/dhtmlxtree.js" type="text/javascript"></script>
</head>
<body >
    <form id="SelectReceiver" method="post" runat="server">
    <div style="border-bottom: silver 1px solid; border-left: silver 1px solid; background-color: #f5f5f5;
        width: 450px; float: left; height: 650px; border-top: silver 1px solid; border-right: silver 1px solid"
        id="divtree1">
    </div>
    <asp:Literal ID="ltljson" runat="server"></asp:Literal>
    <asp:Literal ID="ltlcheck" runat="server"></asp:Literal>
    <input id="hdcheck" type="hidden" runat="server" name="hdcheck"/>
    <div style="clear: both">
        <input type="button" id="btnok" class="input" value="确定" onclick="ReturnValue();"/><input
            class="input" onclick=" Returnlocation() " type="button" value="返回"/>
        <input id="hidtype" type="hidden" runat="server" name="hidtype"/></div>
    </form>
    <script>
      
        tree = new dhtmlXTreeObject("divtree1", "100%", "100%", 0);
        tree.setSkin('dhx_skyblue');
        tree.setImagePath("../js/tree/codebase/imgs/dhxtree_skyblue/");
        tree.enableCheckBoxes(1);
        //tree.setOnCheckHandler(toncheck);
        tree.loadJSONObject(treejson);

        function ReturnValue() {

            var treenote = tree.getAllCheckedBranches();
            if (confirm("确认修改权限！")) {

                $.post("../Handler/addStorePrivilege.ashx", { id: $('#hidtype').val(),note:treenote }, function (data) {
                    if (data == "1") {

                        alert("修改成功!");
                        //window.parent.location = '../login.aspx'
                    }
                    else {
                        alert("修改失败！");
                     }
                }, "Text");

            }

        }
        function Returnlocation() {

            window.parent.rightFrame.location.href = '../user/UserPartRight.aspx';
            // window.parent.location = '../user/UserPartRight.aspx';


        }
           
    </script>
    <script>
        var hck = document.getElementById("hdcheck").value.split(',');
        for (i = 0; i < hck.length; i++) {
            if (hck[i] != "") {
                tree.setCheck(hck[i], 1);
                tree.openItem(hck[i]);
                //doLog(tree.getItemText(hck[i]));
            }
        }
        //tree.enableThreeStateCheckboxes(true);
    </script>
</body>
</html>
