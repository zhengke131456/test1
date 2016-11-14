<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderQuery.aspx.cs" Inherits="product.Order.OrderQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
        <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>
     <script type="text/javascript">

         $(function () {
             $.each($("input[name=QCDate]"), function () {
                 $(this).focus(function () { WdatePicker({ maxDate: '#F{$dp.$D(\'QCEdate\')||\'2050-10-01\'}', dateFmt: 'yyyyMMdd' }); });
                 $(this).click(function () { WdatePicker({ maxDate: '#F{$dp.$D(\'QCEdate\')||\'2050-10-01\'}', dateFmt: 'yyyyMMdd' }); });

             });

             $.each($("input[name=QCEdate]"), function () {

                 $(this).focus(function () { WdatePicker({ minDate: '#F{$dp.$D(\'QCDate\')}', dateFmt: 'yyyyMMdd', readOnly: true }); });
                 $(this).click(function () { WdatePicker({ minDate: '#F{$dp.$D(\'QCDate\')}', dateFmt: 'yyyyMMdd', readOnly: true }); });
             });


         });


    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="subnav">
        订单查询
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
           
                <strong>开始日期</strong>
                <input id="QCDate" style="width: 85px" name="QCDate" type="text" value="<%=QCDate %>" />
                <strong>结束日期</strong>
                <input id="QCEdate" style="width: 85px" name="QCEdate" type="text" value="<%=QCEDate %>" />
                <strong>CAD</strong>
                <input id="CAD" name="CAD" style="width: 95px" type="text" value="<%=cai %>" />
                <strong>客户编码</strong>
                <input id="clientCode" style="width: 65px" name="clientCode" type="text" value="<%=clientCode %>" />

                <strong>批次号</strong>
                <input id="batchno" style="width: 120px" name="batchno" type="text" value="<%=batchno %>" />
                 <strong>状态</strong>
             <asp:DropDownList Width="80px" ID="dpstatus" runat="server" name="dpstatus">
             <asp:ListItem  Selected="True" Value="">全部</asp:ListItem>
                      <asp:ListItem value="0">未下载</asp:ListItem>
                   <asp:ListItem value="1">已下载</asp:ListItem>
                    <asp:ListItem value="2">查询有货</asp:ListItem>
                   <asp:ListItem value="3">查询部分有货</asp:ListItem>
                     <asp:ListItem value="4">查询无货</asp:ListItem>
                    <asp:ListItem value="5">下单有货</asp:ListItem>
                    <asp:ListItem value="6">下单部分有货</asp:ListItem>
                      <asp:ListItem value="7">下单无货</asp:ListItem>
                    <asp:ListItem value="8">下单无货</asp:ListItem>
                  </asp:DropDownList>
                   &nbsp   &nbsp   &nbsp

                <asp:Button runat="server" ID="btnQuerey" Text="查 询" OnClick="btnQuerey_Click" />
            </p>
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>
            <div class="page ">
             
            </div>
        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table width="100%" id="list">
                    <tr>
                        <th width="3%">
                            ID
                        </th>
                         <th width="4%" >
                            查货日期
                        </th> 
                         <th width="5%">
                            CAD
                        </th> 
                         <th width="6%">
                            状态
                        </th> 
                         <th width="25%">
                            DESC
                        </th> 
                         <th width="4%">
                         查询数量
                        </th> 
                            <th width="4%">
                           分配数量
                        </th> 
                        <th width="6%">
                           批次号
                        </th> 
                        <th width="5%">
                           客户编码
                        </th> 
                        <th width="4%">
                            下单人
                        </th> 
                         <th width="4%">
                            中转仓
                        </th> 
                           <th width="8%">
                            流程
                        </th> 
                           <th width="8%">
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
                        <%#Eval("QueryDate")%>
                    </td>
                    <td>
                        <%#Eval("CAD")%>
                    </td>
                    <td>
                        <%#Eval("status")%>
                    </td>
                    <td>
                        <%#Eval("Odesc")%>
                    </td>
                    <td>
                        <%#Eval("number")%>
                    </td>
                    <td>
                        <%#Eval("allotNum")%>
                    </td>
                    <td>
                        <%#Eval("batchmark")%>
                    </td>

                    <td>
                        <%#Eval("ClientCode")%>
                    </td>
                    <td>
                        <%#Eval("placeorderName")%>
                    </td>
                        <td>
                        <%#Eval("storagename")%>
                    </td>
                    <td>
                        <%#Eval("flow")%>
                    </td>
                     <td>
                
                    <div  style=" display:<%# Eval("status").ToString()=="未下载"?" ":"none" %> ">
                        <a id="compile"  href="../Order/Upplaceorder.aspx?id=<%#Eval("id")%>">编辑</a>
                        |<a id="del" " href="javascript:del('<%#Eval("id")%>')">删除</a>
                         </div>
                    </td>
                   
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
        <div class="Pages">
            <div class="Paginator">
                <asp:Literal ID="literalPagination" runat="server"></asp:Literal>
                        &nbsp;&nbsp; &nbsp;&nbsp;
                   <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="下载" Width="54px"  />
                   &nbsp;&nbsp; &nbsp;
            </div>
               
        </div>
    </div>
        <script type="text/javascript">
            function del(id) {
                if (confirm("是否删除！")) {
                    $.post("../delete/delete.aspx?table=Placeorder&id=" + id, {}, function (data) {
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