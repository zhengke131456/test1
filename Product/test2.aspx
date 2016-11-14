<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test2.aspx.cs" Inherits="product.test2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

     <script src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="/JS/Tree/codebase/dhtmlxtree.css" rel="stylesheet" type="text/css" />
    <script src="/JS/Tree/codebase/dhtmlxtree.js" type="text/javascript"></script>

 <script language="javascript" type="text/javascript">
     function popupDiv(div_id) {
         var div_obj = $("#" + div_id);
//         var windowWidth = document.body.clientWidth;
//         var windowHeight = document.body.clientHeight;
//         var popupHeight = div_obj.height();
//         var popupWidth = div_obj.width();

         div_obj.css({ "position": "absolute" })
               .animate({ left: 30,
                   top: 10, opacity: "show"
               }, "slow");

     }

     function hideDiv(div_id) {
         //$("#mask").remove();
         $("#" + div_id).animate({ left: 0, top: 0, opacity: "hide" }, "slow");
     } 
   </script>


       <style type="text/css">
        .pop-box {  
            z-index: 9999; /*这个数值要足够大，才能够显示在最上层*/ 
            margin-bottom: 3px;  
            display: none;  
            position: absolute;  
            background: #FFF;  
            border:solid 1px #6e8bde;  
        }  
         
        .pop-box h4 {  
            color: #FFF;  
            cursor:default;  
            height: 18px;  
            font-size: 14px;  
            font-weight:bold;  
            text-align: left;  
            padding-left: 8px;  
            padding-top: 4px;  
            padding-bottom: 2px;  
        }  
        .pop-box-body {  
            clear: both;  
            margin: 4px;  
            padding: 2px;  
        }
        .mask {  
            color:#C7EDCC;
            background-color:#C7EDCC;
            position:absolute;
            top:0px;
            left:0px;
            filter: Alpha(Opacity=60);
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">


     <asp:Literal ID="ltljson" runat="server"></asp:Literal>
    <asp:Literal ID="ltlcheck" runat="server"></asp:Literal>
    <input id="hdcheck" type="hidden" runat="server" name="hdcheck"/>
    <input id="hidtype" type="hidden" runat="server" name="hidtype"/>

       


     <div id='pop-div' style="width: 236px;" class="pop-box"> 
            <div class="pop-box-body" > 
                <div style="border-bottom: silver 1px solid; border-left: silver 1px solid; background-color: #f5f5f5;
        width: 220px; float: left; height: 500px; border-top: silver 1px solid; border-right: silver 1px solid"
        id="divtree1">
                </div>

                <input id="btnClose" type="button" onclick="hideDiv('pop-div');" value="关闭"/>
            </div> 
        </div>
    <input type="button" id="btnTest"  value='test' onclick="popupDiv('pop-div');"/>

 <input type="button" id="Button1"  value='test' onclick="ReturnValue();"/>



    </form>


    <script language="javascript" type="text/javascript">

        tree = new dhtmlXTreeObject("divtree1", "100%", "100%", 0);
        tree.setSkin('dhx_skyblue');
        tree.setImagePath("/js/tree/codebase/imgs/dhxtree_skyblue/");
        tree.enableCheckBoxes(1);

        tree.loadJSONObject(treejson);
     </script>

    <script language="javascript" type="text/javascript">
        var hck = document.getElementById("hdcheck").value.split(',');
        for (i = 0; i < hck.length; i++) {
            if (hck[i] != "") {
                tree.setCheck(hck[i], 1);
                tree.openItem(hck[i]);
            }
        }
        //tree.enableThreeStateCheckboxes(true);

        function ReturnValue() {

            var treenote = tree.getAllCheckedBranches();
           // var treenotename = tree.getSelectedItemText();

            var tdd = tree.getItemText('cd0001');


            //alert(tdd);

//            if (confirm("确认修改权限！")) {

//                $.post("../Handler/addStorePrivilege.ashx", { id: $('#hidtype').val(), note: treenote }, function (data) {
//                    if (data == "1") {

//                        alert("修改成功!");
//                        //window.parent.location = '../login.aspx'
//                    }
//                    else {
//                        alert("修改失败！");
//                    }
//                }, "Text");

//            }

        }
    </script>



</body>
</html>
