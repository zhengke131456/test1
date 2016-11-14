<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productSDOrders.aspx.cs" Inherits="product.Order.productSDOrders" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
       <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>


        
</head>
<body>
    <form id="form1" runat="server">
      <div class="subnav">
         实到明细
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
           
                </p>
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>
          
        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table width="100%" id="list">
                    <tr>
                        <th width="3%">
                            ID
                        </th>
                      
                         <th width="6%">
                            CAD
                        </th> 
                          <th width="8%">
                             下单日期
                        </th>
                        <th width="8%">
                            到货日期
                        </th> 
                        
                         <th width="8%">
                            结算日期
                        </th> 
                         <th width="5%">
                            城市
                        </th> 
                        <th width="15%">
                            详细型号
                        </th> 
                        <th width="5%">
                            进货价
                        </th> 
                       <%-- <th width="5%">
                            出货价
                        </th> --%>
                        <th width="5%">
                            数量
                        </th> 
                        
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("ID")%>">
                    <td>
                        <%#Eval("rn")%>
                    </td>
                    <td>
                        <%#Eval("CAD")%>
                    </td>
                    <td>
                        <%#Eval("Orderdate")%>
                    </td>
                    <td>
                        <%#Eval("DHdate")%>
                    </td>
                    <td>
                        <%#Eval("balancedate")%>
                    </td>
                    <td>
                        <%#Eval("City")%>
                    </td>
                    <td>
                        <%#Eval("model")%>
                    </td>
                    <td>
                        <%#Eval("Purchaseprice")%>
                    </td>
                   <%-- <td>
                        <%#Eval("shipmentprice")%>
                    </td>--%>
                    <td>
                        <%#Eval("number")%>
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

    </form>
</body>
</html>
