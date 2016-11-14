<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transferslip_Error.aspx.cs" Inherits="product.products.Transferslip_Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Scripts/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />

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
    <div id="right">
        <div class="page ">
          
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>

              <form id="form1" runat="server">

            <div class="page ">
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
                            <th width="2%">
                                ID
                            </th>
                            <th width="4%">
                                编码
                            </th>
                           
                            <th width="5%">
                                型号花纹
                            </th>
                            
                            <th width="3%">
                                数量
                            </th>
                           
                            <th width="10%">
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
                             <th width="12%">
                                状态
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr id="base<%#Eval("id")%>">
                        <td>
                            <%#Eval("id")%><br />
                            <input type="checkbox" name="checkitem" value="<%#Eval("id")%>" id="chkItem" class="chkTopAdItem">
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
                            出：<%#Eval("outwhName")%><br />
                            入：<%#Eval("inWHName")%>
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
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
            <div class="Pages">
                <div class="Paginator">
                    <asp:Literal ID="literalPagination" runat="server"></asp:Literal>
                </div>
            </div>

             <div class="page">
            <input type="checkbox" name="checkbox" onclick="CheckAll(this)" /><label for="selectall0">&nbsp;全选本页信息</label> 
            <input name="button" id="Pcontinue" onclick="Pcontinue();" type="button" value="继续执行调拨" /> &nbsp;
            <input name="button" id="Pstop" onclick="Pstop();" type="button" value="调拨终止" /> &nbsp;

         </div>
        


        </div>
    </div>
</body>
</html>
<script type="text/javascript">


    function Pcontinue() {
        //有没有选中的行
        var b = false;
        $(".chkTopAdItem").each(function () { if (this.checked) b = true; });

        if (!b) {
            alert("请选择,调拨单！");
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

                $.post("../Handler/HandlerAll.ashx?tablename=transferslip&dotype=setpcont&ids=" + encodeURI(ids), function (data) {
                    if (data.result == "0") {
                        alert("批量更新成功！");
                        window.location.reload();
                    }
                }, "json");

            }
        }
    }

    function Pstop() {
        //有没有选中的行
        var b = false;
        $(".chkTopAdItem").each(function () { if (this.checked) b = true; });

        if (!b) {
            alert("请选择,调拨单！");
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

                $.post("../Handler/HandlerAll.ashx?tablename=transferslip&dotype=setpstop&ids=" + encodeURI(ids), function (data) {
                    if (data.result == "0") {
                        alert("批量终止调拨成功！");
                        window.location.reload();
                    }
                }, "json");

            }
        }
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


