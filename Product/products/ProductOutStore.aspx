<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductOutStore.aspx.cs" Inherits="product.products.ProductOutStore" %>

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
         出库列表
    </div>
    <div id="right">
        <div class="page ">
              <p class="left" id="addp">
       
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <%--<a style="color: Red;" href="../products/proStore_Store.aspx">返回</a> &nbsp;--%>
                  <button style="color:Red;" onclick="window.history.back(); return false;">返回</button>
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
                        <th width="5%">
                            ID
                        </th>
                       <th width="8%">
                                睿配编码
                            </th>
                            <th width="10%">
                             石化商品编码 
                            </th>
                            <th width="8%">
                                 供应商编码 
                            </th>
                         <th width="10%">
                            出库日期
                        </th> 
                       
                         <th width="10%">
                            出库仓库
                        </th> 
                          <th width="5%">
                           数量
                        </th> 
                        <th width="8%">
                            特殊标识
                        </th>
                         <th width="25%">
                            备注
                        </th>
                        <th width="8%">
                            操作人
                        </th>
                           <%--<th width="40px">
                            操作
                        </th>--%>
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
                            <%#Eval("ShGoogcode")%>
                        </td>
                          <td>
                            <%#Eval("CAD")%>
                        </td>
                      <td>
                        <%#Eval("OD", "{0:yyyy-MM-dd}")%>
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
                        <%#Eval("TSNote")%>
                    </td>
                      <td>
                        <%#Eval("usercode")%>
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
          <asp:HiddenField ID="hidstockID" runat="server" />
      
         <asp:HiddenField ID="hiddDistrict" runat="server" />
 
    </form>
</body>
</html>
