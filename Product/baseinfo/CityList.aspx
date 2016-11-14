<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CityList.aspx.cs" Inherits="product.baseinfo.CityList" %>

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
       城市管理
    </div>
     <div id="right">
        <div class="page ">
            
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
          
        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table width="100%" id="list">
                    <tr>
                        <th width="2%">
                            ID
                        </th>

                          <th width="10%">
                          省份
                        </th>

                        <th width="10%">
                          城市编码 
                        </th>
                         <th width="10%">
                           城市名称  
                        </th>

                        
                       
                         <th width="5%" >
                           标注
                        </th> 
                           <th width="15%">
                            操作
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("id")%>">
                    <td>
                        <%#Eval("id")%>
                    </td>
                    <td  >
                        <%#Eval("pname")%>
                    </td>
                    <td  >
                        <%#Eval("areacode")%>
                    </td>
                    <td  >
                        <%#Eval("cityname")%>
                    </td>
                 
                      <td >
                        <%#Eval("ispoint")%>
                    </td>

                    
                    <td>
                       <a href="../products/mdpoint.aspx?id=<%#Eval("id")%>&tablename=city&type=city">坐标</a>
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