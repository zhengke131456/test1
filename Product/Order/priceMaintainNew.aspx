<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="priceMaintainNew.aspx.cs" Inherits="product.Order.priceMaintainNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Scripts/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>
   
</head>
<body>
    <form id="form1" runat="server">
    <div class="subnav">
        价格维护
    </div>
    <div id="right">
        <div class="page ">
            <p class="left" id="addp">
               <a style="color: Red;" href="javascript:history.go(-1)">返回</a> &nbsp;
            </p>
           
            <div class="clear">
            </div>
          
        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table id="list">
                    <tr>
                        <th width="10%">
                            睿配编码
                        </th>
                        <th width="10%">
                            城市
                        </th>
                        <th width="10%">
                            现价
                        </th>
                        <th width="10%">
                            原价
                        </th>
                        <th width="10%">
                            是否上线
                        </th>
                        
                        <th width="10%">
                            操作
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("pid")%>">
                    <td>
                        <%#Eval("rpcode")%>
                    </td>
                    <td>
                        <%#Eval("cityname")%>
                    </td>
                    <td>
                        <%#Eval("PriceJinhuo")%>
                    </td>
                    <td>
                        <%#Eval("PriceXiaoshou")%>
                    </td>
                    <td>
                     <%#Eval("shagnxian")%>
                  
                    </td>
                 
                    <td>
                        <a href="../Order/Uppricemaintain.aspx?rpcode=<%#Eval("rpcode")%>&pid=<%#Eval("pid")%>&cityname=<%#Eval("cityname")%>&city=<%#Eval("AreaCode")%>&jj=<%#Eval("PriceJinhuo")%>&yj=<%#Eval("PriceXiaoshou")%>&sx=<%#Eval("isshangxian")%>  ">编辑</a>
                      
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
        
    </div>
    <asp:HiddenField ID="hiddtype" runat="server" />|
    <asp:HiddenField ID="hiddpart" runat="server" />
    
    </script>
    </form>
</body>
</html>
