<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="outproductDetails.aspx.cs" Inherits="product.products.outproductDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
       <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>
    
</head>
<body>
    
  <form id="Form1"   runat="server" >
       <div class="subnav">
       入库明细
    </div>
    <div id="right">
        <div class="page ">
          <p class="left" id="addp" >
             
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <a style="color: Red;" href="../products/outproductList.aspx">返回</a> &nbsp;
                <%--  <a style="color: Red;" href="javascript:history.go(<% =returnCount %>)">返回</a> &nbsp;--%>
            </p>
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
        </div>
        <asp:Repeater runat="server" ID="dislist">
             <HeaderTemplate>
                <table width="100%" id="list">
                    <tr>
                        <th width="3%">
                            ID
                        </th>
                        <th width="8%">
                            产品编码
                        </th> 
                        <th width="8%">
                            入库仓库
                        </th> 
                        
                         <th width="8%">
                            价格
                        </th> 
                         <th width="5%">
                           流水号
                        </th> 
                        <th width="10%">
                            特殊标识
                        </th>
                         <th width="10%">
                            其他
                        </th>
                         <th width="30%">
                            历史操作
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("id")%>">
                    <td>
                        <%#Eval("id")%>
                    </td>
                    <td>
                        <%#Eval("ipd_CAI")%>
                    </td>
                    <td>
                        <%#Eval("basename")%>
                    </td>
                    <td>
                        <%#Eval("ipd_price")%>
                    </td>
                    <td>
                        <%#Eval("ipd_number")%>
                    </td>
                    <td>
                        <%#Eval("markName")%>
                    </td>
                    <td>
                        <%#Eval("ipd_note")%>
                    </td>
                       <td>
                            <a href="../products/historyoperation.aspx?id='<%#Eval("ipd_number")%>'">查看历史操作</a>
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
  <asp:HiddenField ID="Hiddentype" runat="server" />
  <asp:HiddenField ID="hiddDistrict" runat="server" />
    </form>
</body>
</html>
