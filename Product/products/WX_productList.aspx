<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WX_productList.aspx.cs" Inherits="product.products.WX_productList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="subnav">
        石化APP接入产品信息
    </div>
    <div id="right">
        <div class="page "> 
            <p class="left"   >
         

                <strong>石化APP接入产品信息</strong> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="shangchuan" runat="server" OnClick="Button1_Click" Text="上　传" Width="54px" />
                      
            </p>
           
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>
            <div class="page ">
                <strong>睿配编码</strong>
                <input id="rpcode" name="rpcode" type="text" value="<%=rpcode %>" />
                <strong>供应商编码</strong>
                <input id="CAD" name="CAD" type="text" value="<%=code %>" />
                <strong>型号</strong>
                <input id="model" name="model" type="text" value="<%=model %>" />
                
                <asp:Button runat="server" ID="btnQuerey" Text="查询" OnClick="btnQuerey_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="Button1" runat="server" OnClick="Buttondow_Click" Text="下载" Width="54px"  />
            </div>
        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table width="100%" id="list">
                    <tr>
                        <th width="3%">
                            ID
                        </th>
                        <th width="7%">
                            省份ID
                        </th>
                        <th width="5%">
                            省份名
                        </th>
                        <th width="5%">
                            城市ID
                        </th>
                        <th width="5%">
                            城市名
                        </th>
                        <th width="10%">
                            石化系统编码
                        </th>
                    
                        <th width="8%">
                            石化编码
                        </th>
                  
                        <th width="3%">
                            睿配编码
                        </th>
                        <th width="3%">
                            CAD
                        </th>
                         <th width="3%">
                            价格
                        </th>
                         <th width="5%">
                            入库时间
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("id")%>">
                    <td>
                        <%#Eval("id")%>
                    </td>
                    <td>
                        <%#Eval("provinceid")%>
                    </td>
                    <td>
                        <%#Eval("provincename")%>
                    </td>
                    <td>
                        <%#Eval("cityid")%>
                    </td>
                    <td>
                        <%#Eval("cityname")%>
                    </td>
                    <td>
                        <%#Eval("SNPXTcode")%>
                    </td>
                   
                    <td>
                        <%#Eval("SNPcode")%>
                    </td>

                    <td>
                        <%#Eval("rpcode")%>
                    </td>
                   
                    <td>
                        <%#Eval("cad")%>
                    </td>

                    <td>
                        <%#Eval("price")%>
                    </td>
                    <td>
                       <%#Eval("inserttime", "{0:yyyy-MM-dd}")%>
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
