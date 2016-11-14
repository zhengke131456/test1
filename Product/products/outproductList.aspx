<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="outproductList.aspx.cs"
    Inherits="product.products.outproductList" %>

    <%@ Register TagPrefix="uc1" TagName="ck1" Src="~/cktree.ascx" %>

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

          function ckclear()
        {
        document.getElementById("ckids").value = "";
         $("#showckname").html("");
        }
    </script>
    <script type="text/javascript">

        $(function () {
            $.each($("input[name=bdata]"), function () {
                $(this).focus(function () { WdatePicker({ maxDate: '#F{$dp.$D(\'edata\')||\'2020-10-01\'}', readOnly: true }); });
                $(this).click(function () { WdatePicker({ maxDate: '#F{$dp.$D(\'edata\')||\'2020-10-01\'}', readOnly: true }); });

            });
            $.each($("input[name=edata]"), function () {

                $(this).focus(function () { WdatePicker({ minDate: '#F{$dp.$D(\'bdata\')}', maxDate: '2020-10-01', readOnly: true }); });
                $(this).click(function () { WdatePicker({ minDate: '#F{$dp.$D(\'bdata\')}', maxDate: '2020-10-01', readOnly: true }); });
            });
            if ($("#hiddtype").val() == "信息查看员") {
                $("#addp").style.display = "none";
            }
            if ($("#hiddpart").val() == "信息查看员") {
                document.getElementById("addp").style.display = "none";

            }

        });
              
                
             

    </script>
</head>
<body>
    <form id="form1" runat="server" action="outproductList.aspx">
    <div class="subnav">
        出库列表
    </div>
    <div id="right">
        <div class="page ">
            <p class="left" id="addp">
                <%if(showp=="1") {%>
                &nbsp;&nbsp;&nbsp;
                <a><strong>批量出库</strong></a>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="shangchuan" runat="server" OnClick="Button1_Click" Text="上　传" Width="54px" />
                 <a href="../link/out.csv"><strong style="color:Red;">&nbsp;&nbsp;下载批量出库模板</strong>  </a><%} %>
            </p>
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>
            <div class="page ">
               
               <uc1:ck1 ID="showck" name="showck" runat="server"/> 

                <input id="Button1" type="button" onclick="ckclear();"  value="全部仓库" />

                <strong>出库日期起</strong>
                <input id="bdata" name="bdata" type="text" value="<%=Bdate %>" onfocus="WdatePicker({skin:'whyGreen',minDate: '2015-11-18', maxDate: '2020-12-28' })"/>
                <strong>出库日期结</strong>
                <input id="edata" name="edata" type="text" value="<%=Edate %>" onfocus="WdatePicker({skin:'whyGreen',minDate: '2015-11-18', maxDate: '2020-12-28' })"/>
                <strong>睿配编码</strong>
                <input id="QCAI" name="QCAI" type="text" value="<%=cai %>" />

                <input id="ckids" name="ckids" type="hidden" value="<%=ckids %>" />
                <asp:Button runat="server" ID="btnQuerey" Text="查询" OnClick="btnQuerey_Click" /><span id="showckname"></span>

                 <asp:Button ID="Button2" runat="server" OnClick="Buttondow_Click" Text="下载" Width="54px" />
                <asp:HiddenField ID="HiddenField1" runat="server" />

            </div>
        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table width="100%" id="list">
                    <tr>
                        <th width="4%">
                            ID
                        </th>
                        <th width="7%">
                            睿配编码
                        </th>

                        <th width="8%">
                            供应商编码
                        </th>
                        <th width="6%">
                            出库日期
                        </th>
                         <th width="6%">
                            仓库编码
                        </th>

                        <th width="13%">
                            出库仓库
                        </th>
                        <th width="3%">
                            数量
                        </th>
                        <th width="6%">
                            特殊标识
                        </th>
                        <th width="9%">
                            批次号
                        </th>
                        <th width="6%">
                            操作人
                        </th>
                         <th width="6%">
                            操作时间
                        </th>
                         <th width="7%">
                            销售地
                        </th>
                        <th width="7%">
                            操作
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("id")%>">
                    <td>
                        <%#Eval("id")%>
                    </td>
                    <td>
                        <%#Eval("rpcode")%>
                    </td>

                    <td>
                        <%#Eval("CAD")%>
                    </td>
                    <td>
                        <%#Eval("OD", "{0:yyyy-MM-dd}")%>
                    </td>
                     <td>
                        <%#Eval("WHcode")%>
                    </td>

                    <td>
                        <%#Eval("basename")%>
                    </td>
                    <td>
                        <%#Eval("QTY")%>
                    </td>
                    <td>
                        <%#Eval("markName")%>
                    </td>
                    <td>
                        <%#Eval("INbatch")%>
                    </td>
                    <td>
                        <%#Eval("usercode")%>
                    </td>
                    <td>
                        <%#Eval("insettime", "{0:yyyy-MM-dd}")%>
                    </td>
                    <td>
                        <%#Eval("saleaddress")%>
                    </td>
                    <td>
                        <a href="../products/updateoutpro.aspx?id=<%#Eval("id")%>">编辑</a>| <a href="javascript:del('<%#Eval("id")%>')">
                            删除</a>
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
    </div>
    <asp:HiddenField ID="hiddtype" runat="server" />
    <asp:HiddenField ID="hiddpart" runat="server" />
    <script type="text/javascript">
        function del(id) {
            if (confirm("是否删除！")) {
                $.post("../Handler/delstoreck.ashx?table=BaseStore&type=1&id=" + id, {}, function (data) {
                    if (data.result == "100") {
                        alert("删除成功！");
                        $("#base" + id)[0].style.display = "none";
                    }
                    else
                    { alert("删除失败！"); }
                }, "json");
            }
        }
    </script>
    </form>
</body>
</html>
