<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Orderbatch.aspx.cs" Inherits="product.Order.Orderbatch" %>

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
                 $(this).focus(function () { WdatePicker({ dateFmt: 'yyyyMMdd' }); });
                 $(this).click(function () { WdatePicker({ dateFmt: 'yyyyMMdd' }); });

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
         
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>
            <div class="page ">
             
                <strong>批次</strong>
               <input id="batchNo" name="batchNo" type="text" value="<%=batchNo %>"   />
                      <%-- <strong>状态</strong>
             <asp:DropDownList Width="80px" ID="dpstatus" runat="server" name="dpstatus">
             <asp:ListItem  Selected="True" Value="">全部</asp:ListItem>
            
                    <asp:ListItem value="0">待上传查询反馈</asp:ListItem>
                   <asp:ListItem value="1">已查询反馈</asp:ListItem>
                    <asp:ListItem value="2">查询有货</asp:ListItem>
                   <asp:ListItem value="3">查询部分有货</asp:ListItem>
                                  </asp:DropDownList>
                   &nbsp   &nbsp   &nbsp--%>
                <asp:Button runat="server" ID="btnQuerey" Text="查询" OnClick="btnQuerey_Click" />
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
                            批次号
                        </th> 
                        <th width="7%" >
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
                        <%#Eval("batchNo")%>
                    </td>
                    <td>
                        <%#Eval("statusing")%>
                    </td>
                   
                    <td>
                        <%#Eval("inserttime")%>
                    </td>
                   <td>
                        <%#Eval("flow")%>
                    </td>
                    <td>
                        <div style="display: <%# Eval("statusing").ToString()=="待管理员反馈"?" ":"none" %>">
                            <a id="compile" href="../Order/UPQueryfeedback.aspx?id=<%#Eval("id")%>&batchNo=<%#Eval("batchNo")%>">
                                上传查货反馈</a> &nbsp &nbsp 
                                   <a id="A1" href="../Order/DWQueryfeedback.aspx?id=<%#Eval("id")%>&batchNo=<%#Eval("batchNo")%>">
                                上传下单反馈</a> &nbsp &nbsp 
                                  <a href="javascript:del('<%#Eval("batchNo")%>')">
                                删除</a>
                        </div>
                        <div style="display: <%# Eval("statusing").ToString()=="待管理员上传下单反馈"?" ":"none" %>">
                            <a href="../Order/UpOrderAllot.aspx?batch=<%#Eval("batchNo")%>">操作</a> &nbsp
                            <a href="../Handler/downloadOrder.ashx?batch=<%#Eval("batchNo")%>">下载分配汇总</a> &nbsp
                            &nbsp <a href="javascript:cancel('<%#Eval("batchNo")%>')">取消发货</a>
                        </div>
                        <div style="display: <%# Eval("statusing").ToString()=="取消发货"?" ":"none" %>">
                            <a href="../Handler/downloadOrder.ashx?batch=<%#Eval("batchNo")%>">下载分配汇总</a> &nbsp
                            &nbsp
                        </div>
                          <div style="display: <%# Eval("statusing").ToString()=="已完成下单反馈"?" ":"none" %>">
                              <a href="../Order/UpOrderAllot.aspx?batch=<%#Eval("batchNo")%>">操作</a> &nbsp &nbsp &nbsp
                            <a href="../Handler/downloadOrder.ashx?batch=<%#Eval("batchNo")%>">下载分配汇总</a> &nbsp
                            &nbsp
                        </div>
                         <div style="display: <%# Eval("statusing").ToString()=="已分配下单反馈"?" ":"none" %>">
                              <a href="../Order/UpOrderAllot.aspx?batch=<%#Eval("batchNo")%>">操作</a> &nbsp &nbsp &nbsp
                            <a href="../Handler/downloadOrder.ashx?batch=<%#Eval("batchNo")%>">下载分配汇总</a> &nbsp
                            &nbsp
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
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function cancel(batchNo) {
            if (confirm("是否取消发货！")) {
                $.post("../Handler/cancelorder.ashx?&batch=" + batchNo, {}, function (data) {
                    if (data.result == "0") {
                        alert("取消发货成功！");
                        location.replace(location.href)
                    }
                    else
                    { alert("取消发货失败！"); }
                }, "json");
            }
        }
        function del(batchNo) {
            if (confirm("是否删除！")) {
                $.post("../Handler/DelOrderBatch.ashx?&batch=" + batchNo, {}, function (data) {
                    if (data.result == "0") {
                        alert("删除成功！");
                        location.replace(location.href)
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