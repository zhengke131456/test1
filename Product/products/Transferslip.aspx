<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transferslip.aspx.cs" Inherits="product.products.Transferslip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Scripts/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>

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

    <style type="text/css">
        .ui-autocomplete
        {
            max-height: 200px;
            overflow-y: auto; /* 防止水平滚动条 */
            overflow-x: hidden;
            background-color: #CCCCCC;
            width: 200px;
        }
    </style>


</head>
<body>
  
    <div class="subnav">
        调拨单
    </div>

    <div id='pop-div' style="width: 236px;" class="pop-box"> 
            <div class="pop-box-body" > 
                <div style="border-bottom: silver 1px solid; border-left: silver 1px solid; background-color: #f5f5f5;
        width: 220px; float: left; height: 150px; border-top: silver 1px solid; border-right: silver 1px solid"
        id="divtree1">

               <strong>选择时间：</strong>
                <input id="outwhdate" name="outwhdate" type="text" value="" onfocus="WdatePicker({skin:'whyGreen',minDate: '2015-11-18', maxDate: '2020-12-28' })" />
                
                <strong style="display:block;">备注：</strong>
                 <textarea id="message" name="message"   cols="22" rows="5"></textarea>

                <input id="cztype" name="cztype" type="hidden" value="" />

                </div>

                <input id="btnClose" type="button" onclick="hideDiv('pop-div');" value="关闭"/>
                <input id="Button1" type="button"  onclick="ReturnValue();" value="确定"/>
                <input id="Button2" type="button"  onclick="Perror();" value="异常"/>


            </div> 
        </div>


    <div id="right">

        <div class="page ">
                    <form id="form1" runat="server" action="Transferslip.aspx">
            <p class="left">
                 &nbsp;&nbsp;
                 <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="shangchuan" runat="server" OnClick="Button1_Click" Text="上　传" Width="54px" />
                <a href="../link/db.csv"><strong style="color:Red;">&nbsp;&nbsp;下载批量调拨模板</strong>  </a>
            </p>
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>

             
             
            <div class="page ">
             

                <strong>调拨状态</strong>

               <select id="checked" name = "checked" >
                   <option value="" >选择状态</option>
                   <option value="待出库">待出库</option>
                   <option value="待入库">待入库</option>
                   <option value="调拨已完成">调拨已完成</option>
                   <option value="出库异常">出库异常</option>
                   <option value="入库异常">入库异常</option>
                   <option value="调拨失败">调拨失败</option>
               </select>

                <strong>睿配编码</strong>
                <input id="rpcode" name="rpcode" type="text" value="<%=rpcode %>" />
                <strong>出库仓库</strong>
                <input id="outwh" name="outwh" type="text" value="<%=outwh %>" />
                <strong>入库仓库</strong>
                <input id="inwh" name="inwh" type="text" value="<%=inwh %>" />
                <asp:Button runat="server" ID="btnQuerey" Text="查询" OnClick="btnQuerey_Click" />
            </div>

                </form>
        </div>
        <div style="width: 100%; overflow: auto; height: 550px">
            <asp:Repeater runat="server" ID="dislist">
                <HeaderTemplate>
                    <table id="list" style="width: 100%">
                        <tr>
                            <th width="3%">
                                ID
                            </th>
                            <th width="5%">
                                编码
                            </th>
                           
                            <th width="8%">
                                型号花纹
                            </th>
                            
                            <th width="2%">
                                数量
                            </th>
                           
                            <th width="13%">
                                出入库仓库
                            </th>
                            <th width="6%">
                                出入库日期
                            </th>
                            <th width="4%">
                                出入库人员
                            </th>
                           
                           
                            <th width="4%">
                                创建人
                            </th>

                            <th width="5%">
                                创建日期
                            </th>
                           

                             <th width="5%">
                                状态
                            </th>


                            <th width="4%">
                                操作
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr id="base<%#Eval("id")%>">
                        <td>
                            <%#Eval("id")%>
                            <br />
                            <input type="checkbox" name="checkitem" value="<%#Eval("states")%>_<%#Eval("id")%>_<%#Eval("QTY")%>_<%#Eval("outwh")%>_<%#Eval("inwh")%>" id="chkItem" class="chkTopAdItem">
                        </td>
                        <td>
                            <%#Eval("rpcode")%><br />
                             <%#Eval("CAD")%>
                        </td>   
      
                        <td>
                            <%#Eval("Dimension")%><br />
                            <%#Eval("PATTERN")%>
                        </td>

                        <td>
                            <%#Eval("QTY")%>
                        </td>
                       
                        <td style="text-align:left;">
                            出：<%#Eval("opc")%><%#Eval("outwhName")%><br />
                            入：<%#Eval("ipc")%><%#Eval("inWHName")%>
                        </td>
                        <td>
                            出：<%#Eval("outwhtime", "{0:yyyy-MM-dd}")%><br />
                            入：<%#Eval("inwhtime", "{0:yyyy-MM-dd}")%>
                        </td>
                        <td>
                            出：<%#Eval("outOpcode")%><br />
                            入：<%#Eval("inOpcode")%>
                        </td>

                        <td>
                            <%#Eval("opcode")%>
                        </td>

                        <td>
                            <%#Eval("inserttime", "{0:yyyy-MM-dd}")%>
                        </td>

                         <td>
                            <%#Eval("states")%><br />
                             <%#Eval("outerrortime", "{0:yyyy-MM-dd}")%> <%#Eval("outerrormess")%><br />
                              <%#Eval("inerrortime", "{0:yyyy-MM-dd}")%> <%#Eval("inerrormess")%>
                        </td>

                        <td>
                            <div style="display: <%# Eval("states").ToString()=="待出库"?" ":"none" %>">
                                <div style="display: <%# Eval("iswh").ToString()=="0"?" ":"none" %>">
                                     <a href="javascript:del('<%#Eval("id")%>')">删除</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>


             <div class="page ">
             <input type="checkbox" name="checkbox" onclick="CheckAll(this)" /><label for="selectall0">&nbsp;全选本页信息</label> 
              <input name="button" id="PCall" onclick="PCall();" type="button" value="批量出库" /> &nbsp;
            <input name="button" id="PRall" onclick="PRall();" type="button" value="批量入库" /> &nbsp;

             </div>

            <div class="Pages">
            
                <div class="Paginator">
                    <asp:Literal ID="literalPagination" runat="server"></asp:Literal>
                </div>
            </div>

            
          
        </div>
    </div>

</body>
</html>


<script type="text/javascript">

    //默认选中调拨状态
    $("#checked").val('<%=states %>');

    function popupDiv(div_id) {
        var div_obj = $("#" + div_id);

        div_obj.css({ "position": "absolute" })
               .animate({ left: 230,
                   top: 200, opacity: "show"
               }, "slow");
           }

    function hideDiv(div_id) {
        $("#" + div_id).animate({ left: 0, top: 0, opacity: "hide" }, "slow");
    }

    //批量异常
    function Perror() {
        var outdate = $("#outwhdate").val();
        if ($('#outwhdate').val() == "") {
            alert("请选择日期");
            return;
        }

        var message = $("#message").val();
        if ($('#message').val() == "") {
            alert("如异常必须填写异常信息！");
            return;
        }

        var errorstatus = "出库异常";
        if ($("#cztype").val() == "1") {
            errorstatus = "出库异常";
        }
        if ($("#cztype").val() == "2") {
            errorstatus = "入库异常";
        }

        var ids = "-1";
        $(".chkTopAdItem").each(function () {

            if (this.checked) {
                b = true;
                ids = ids + "," + this.value;
            }
        });

        // i 表示 第一个，g 表示全部
        re = new RegExp("-1,", "g");
        ids = ids.replace(re, "");

        $.post("../Handler/HandlerAll.ashx?tablename=transferslip&dotype=setpEr&states=" + encodeURI(errorstatus) + "&mess=" + encodeURI(message) + "&outdate=" + encodeURI(outdate) + "&ids=" + encodeURI(ids), function (data) {
            if (data.result == "0") {
                alert("异常处理成功！");
                window.location.reload();
            }
            else {
                alert(data.mes);
            }
        }, "json");
    }

    //批量出入库,选择日期后开闭
    function ReturnValue() {
        var outdate = $("#outwhdate").val();
        if ($('#outwhdate').val() == "") {
            alert("请选择日期");
            return;
        }

        var ids = "-1";
        $(".chkTopAdItem").each(function () {

            if (this.checked) {
                b = true;
                ids = ids + "," + this.value;
            }
        });

        // i 表示 第一个，g 表示全部
        re = new RegExp("-1,", "g");
        ids = ids.replace(re, "");



        //批量出
        if ($("#cztype").val() == "1") {

            $.post("../Handler/HandlerAll.ashx?tablename=transferslip&dotype=setpcdb&states=" + encodeURI("待入库") + "&outdate=" + encodeURI(outdate) + "&ids=" + encodeURI(ids), function (data) {
                if (data.result == "0") {
                    alert("批量出库更新成功！");
                    window.location.reload();
                }
                else {
                    alert(data.mes);
                }
            }, "json");
        }

        //批量入
        if ($("#cztype").val() == "2") {

            $.post("../Handler/HandlerAll.ashx?tablename=transferslip&dotype=setprdb&states=" + encodeURI("调拨已完成") + "&outdate=" + encodeURI(outdate) + "&ids=" + encodeURI(ids), function (data) {
                if (data.result == "0") {
                    alert("批量入库更新成功！");
                    window.location.reload();
                }
                else {
                    alert(data.mes);
                }
            }, "json");
        }

        //关闭弹窗
        hideDiv('pop-div');

    }

    //批量出
    function PCall() {
        //有没有选中待出库
        var b = true;
       // var bb = false;
        $(".chkTopAdItem").each(function () {
            //选中
            if (this.checked) {

                var cedv = this.value;
                var arr = cedv.split('_')[0];

                if (arr != "待出库") {
                    b = false;
                }
            }
        }
        );

        if (!b) {
            alert("请选择待出库单！并且不能包括其它状态的记录！");
        }
        else {
            var ids = "-1";
            $(".chkTopAdItem").each(function () {

                if (this.checked) {
                    b = true;
                    ids = ids + "," + this.value;
                }
            });

            if (ids != "-1") {

                //批量出库标识
                $("#cztype").val("1");

                //弹出入库日期选择：
                popupDiv('pop-div');

            }
        }
    }

    //批量入
    function PRall() {
        //有没有选中待出库
        var b = true;
        // var bb = false;
        $(".chkTopAdItem").each(function () {
            //选中
            if (this.checked) {

                var cedv = this.value;
                var arr = cedv.split('_')[0];

                if (arr != "待入库") {
                    b = false;
                }
            }
        }
        );

        if (!b) {
            alert("请选择待入库单！并且不能包括其它状态的记录！");
        }
        else {
            var ids = "-1";
            $(".chkTopAdItem").each(function () {

                if (this.checked) {
                    b = true;
                    ids = ids + "," + this.value;
                }
            });

            if (ids != "-1") {

                //批量入库标识
                $("#cztype").val("2");

                //弹出入库日期选择：
                popupDiv('pop-div');

            }
        }
    }

    //批量异常


    </script>
   


<script type="text/javascript">
    function CalcShowModalDialogLocation(dialogWidth, dialogHeight) {
        var iWidth = dialogWidth;
        var iHeight = dialogHeight;
        var iTop = (window.screen.availHeight - 20 - iHeight) / 2;
        var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
        return 'dialogWidth:300px;dialogHeight:200px;dialogTop: ' + iTop + 'px; dialogLeft: ' + iLeft + 'px;center:yes;scroll:no;status:no;resizable:0;location:no';
    }
    function del(id) {
        if (confirm("是否删除！")) {
            $.post("../delete/delete.aspx?table=transferslip&id=" + id, {}, function (data) {
                if (data.result == "100") {
                    alert("删除成功！");
                    $("#base" + id)[0].style.display = "none";
                }
                else
                { alert("删除失败！\r\n可能该调拨已出库，请刷新后重试。"); }
            }, "json");
        }
    }


    function instore(id, inwh) {
        var DialogLocation = CalcShowModalDialogLocation(500, 260);
        var Trans = new Object();
        Trans.id = id.toString();
        Trans.wh = inwh;
        Trans.type = "0";
        // var getv = window.showModalDialog("../products/popupmessage.aspx", Trans, "dialogWidth=300px;dialogHeight=200px; center:yes ;scroll: no;");
        //弹出页面获取时间
        var value = window.showModalDialog("../products/popupmessage.aspx", window, DialogLocation);

        if (value == '') {
            alert('入库日期不能为空，请重新输入！');

        } else {

            $.post("../Handler/warehouse.ashx?type=0&id=" + id, { date: value, wh: inwh }, function (data) {
                if (data.result == "0") {
                    alert("入库成功！");
                    window.location.reload();
                }
                if (data.result == "3") {
                    alert("无权限操作该仓库！");
                    window.location.reload();
                }

                if (data.result == "2") {
                    alert("入库失败！");
                    window.location.reload();
                }
                if (data.result == "4") {
                    alert("调拨单已入库或被删除\r\n请先刷新");
                    window.location.reload();
                }
            }, "json");
        }
        //        }        
    }

    function outstore(id, outwh) {
        var DialogLocation = CalcShowModalDialogLocation(500, 260);
        var Trans = new Object();
        Trans.id = id.toString();
        Trans.wh = outwh;
        Trans.type = "1";
        //弹出页面获取时间
        var value = window.showModalDialog("../products/popupmessage.aspx", window, DialogLocation);
//        if (!returnValue) {
//            returnValue = window.returnValue;
//        }
        //        if (typeof (getv) != "undefined") {
        //            alert(getv);
        //            window.location.reload();

        //        }
        //        else {

        //            var value = prompt("请输入出库日期");

        if (value == '') {
            alert('出库日期不能为空，请重新输入！');

        } else {

            $.post("../Handler/warehouse.ashx?type=1&id=" + id, { date: value, wh: outwh }, function (data) {
                if (data.result == "0") {
                    alert("出库成功！");
                    window.location.reload();
                }
                if (data.result == "3") {
                    alert("无权限操作该仓库！");
                    window.location.reload();
                }

                if (data.result == "2") {
                    alert("出库失败！");
                    window.location.reload();
                }
                if (data.result == "4") {
                    alert("调拨单已出库或被删除\r\n请先刷新");
                    window.location.reload();
                }
            }, "json");
        }
        //        }
    }


    //全选
    function CheckAll(obj) {
        if (obj.checked) {
            $(".chkTopAdItem").each(function ()
            { this.checked = true; });
        }
        else {
            $(".chkTopAdItem").each(function ()
            { this.checked = false; });
        }
    }
</script>
