<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="batchmanagement.aspx.cs" Inherits="product.Order.batchmanagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        批次管理
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
           
            <%--    <strong>开始日期</strong>
                <input id="QCDate" style="width: 65px" name="QCDate" type="text" value="<%=QCDate %>" />
                <strong>结束日期</strong>
                <input id="QCEdate" style="width: 65px" name="QCEdate" type="text" value="<%=QCEDate %>" />
                <strong>CAD</strong>
                <input id="CAD" name="CAD" style="width: 65px" type="text" value="<%=cai %>" />
                <strong>客户编码</strong>
                <input id="clientCode" style="width: 65px" name="clientCode" type="text" value="<%=clientCode %>" />
                 

                <asp:Button runat="server" ID="btnQuerey" Text="查 询" OnClick="btnQuerey_Click" />--%>
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
                         <th width="7%" >
                           批次
                        </th> 
                         <th width="5%">
                            下单人
                        </th> 
                         <th width="10%">
                            状态
                        </th> 
                         <th width="10%">
                            日期
                        </th> 
                         <th width="10%">
                            流程
                        </th> 
                        <th width="10%">
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
                        <%#Eval("batch")%>
                    </td>
                    <td>
                        <%#Eval("ordersUser")%>
                    </td>
                    <td>
                        <%#Eval("states")%>
                    </td>
                    <td>
                        <%#Eval("inserttime")%>
                    </td>
                      <td>
                        <%#Eval("flow")%>
                    </td>
                    <td>
                     
                        
                 <div style="display: <%# Eval("states").ToString()=="待分配查货反馈"?" ":"none" %>">
                                  
                            <a href="../Order/Orderallocation.aspx?id=<%#Eval("id")%>&batch=<%#Eval("batch")%>&ordersUser=<%#Eval("ordersUser")%>&flow=<%#Eval("states")%>">
                                分配</a>
                        </div>
                  
                    <div style="display: <%# Eval("states").ToString()=="待分配下单反馈"?" ":"none" %>">

                            <a href="../Order/DWOrderallocation.aspx?id=<%#Eval("id")%>&batch=<%#Eval("batch")%>&ordersUser=<%#Eval("ordersUser")%>&flow=<%#Eval("states")%>">
                                分配</a>
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
               
            </div>
               
        </div>
    </div>
    <input type="hidden" id="hiddbatch" value=""  runat="server"/>
    </form>
</body>
  <script type="text/javascript">
      function cancel(id) {
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
</html>
