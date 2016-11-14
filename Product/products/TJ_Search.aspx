<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TJ_Search.aspx.cs" Inherits="product.products.TJ_Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />

</head>
<body>

    <div class="subnav">
        库存查询
    </div>
    <div id="right">
        <div class="page ">
          
            <div class="clear">
            </div>

              <form id="form1" runat="server">

            <div class="page ">
                <strong>睿配编码或CAD</strong>
                <input id="rpcode" name="rpcode" type="text" value="" />
                
                <input type="submit" value="查询" /> <strong style="color:Red;"><%=desmess %></strong>
            </div>

            </form>
        </div>
        <div style="width: 35%; overflow: auto;">
            <asp:Repeater runat="server" ID="List1">
                <HeaderTemplate>
                    <table id="list" style="width: 100%">
                        <tr>

                            <th width="6%">
                                仓库编码
                            </th>
                           
                            <th width="8%">
                               仓库名
                            </th>
                            
                            <th width="5%">
                                实际库存
                            </th>
                           
                           
                             <th width="5%">
                                计提库存
                            </th>
                            
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>

                        <td>
                            <%#Eval("stockcode")%>
                        </td>
                       

                        <td>
                            <%#Eval("basename")%>
                        </td>

                        <td>
                            <%#Eval("num1")%>
                        </td>

                         <td>
                            <%#Eval("num2")%>
                        </td>
                       
                      
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>

        <div class="page ">
            <div class="clear">
            </div>

            <strong>二级仓库统计</strong>
            </div>

<div style="width: 50%; overflow: auto;">
            <asp:Repeater runat="server" ID="list2">
                <HeaderTemplate>
                    <table id="list" style="width: 100%">
                        <tr>

                            <th width="7%">
                                销售终端
                            </th>
                           
                            <th width="6%">
                               服务中心实际库存
                            </th>
                            
                            <th width="6%">
                                服务中心计提库存
                            </th>

                             <th width="5%">
                                油站数量
                            </th>

                            <th width="6%">
                                油站实际库存
                            </th>

                             <th width="6%">
                                油站计提库存
                            </th>
                            
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>

                        <td>
                            <%#Eval("basename")%>
                        </td>
                       

                        <td>
                         <%#Eval("fNum")%>
                        </td>

                        <td>
                         <%#Eval("fJtNum")%>
                        </td>

                         <td>
                          <%#Eval("StockNum")%>
                        </td>
                       
                        <td>
                         <%#Eval("fYNum")%>
                        </td>

                         <td>
                          <%#Eval("fYJtNum")%>
                        </td>
                      
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>

    </div>
</body>
</html>


