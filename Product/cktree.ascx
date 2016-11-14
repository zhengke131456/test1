<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cktree.ascx.cs" Inherits="product.cktree" %>


     <script src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="/JS/Tree/codebase/dhtmlxtree.css" rel="stylesheet" type="text/css" />
    <script src="/JS/Tree/codebase/dhtmlxtree.js" type="text/javascript"></script>

 <script language="javascript" type="text/javascript">
     function popupDiv(div_id) {
         var div_obj = $("#" + div_id);

         div_obj.css({ "position": "absolute" })
               .animate({ left: 30,
                   top: 10, opacity: "show"
               }, "slow");

     }

     function hideDiv(div_id) {
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
                <input id="Button1" type="button" onclick="ReturnValue();" value="确定"/>
            </div> 
        </div>
    <input type="button" id="btnTest"  value='选择仓库' onclick="popupDiv('pop-div');"/>

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

        function ReturnValue() {

            var treenote = tree.getAllCheckedBranches();

            //仓库编码串 
            document.getElementById("ckids").value = treenote;
            //$("#ckids").val = treenote;
            //alert($("#ckids").val);



            var ids = treenote.split(",");

            var shows = "";
            for (var i = 0; i < ids.length; i++) {

                shows += tree.getItemText(ids[i]) + ";";

            }

            $("#showckname").html(shows);


            hideDiv('pop-div');
            //document.getElementById("showckname").
        }
    </script>