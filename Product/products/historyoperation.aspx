<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="historyoperation.aspx.cs" Inherits="product.products.historyoperation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>历史操作</title>
     <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div class="subnav">
        进库列表
    </div>
    <div id="right">
        <div class="page ">
            <p class="left" id="addp">
                <a style="color: Red;"href="javascript:history.go(-1)"">返回</a> &nbsp;
            </p>
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>
          
            
        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table id="list">
                    <tr>
                        <th width="5%">
                            ID
                        </th>
                        <th width="8%">
                            CAI
                        </th>
                        <th width="8%">
                            原始流水号
                        </th>
                        <th width="8%">
                            流水号
                        </th>
                        <th width="7%">
                            状态
                        </th>
                        <th width="10%">
                            入库仓库
                        </th>
                        <th width="10%">
                            出库仓库
                        </th>
                        <th width="10%">
                            操作日期
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("id")%>">
                    <td>
                        <%#Eval("id")%>
                    </td>
                    <td>
                        <%#Eval("CAI")%>
                    </td>
                    <td>
                        <%#Eval("onenumber")%>
                    </td>
                    <td>
                        <%#Eval("code")%>
                    </td>
                    <td>
                        <%#Eval("markName")%>
                    </td>
                    <td>
                        <%#Eval("rkbasename")%>
                    </td>
                    <td>
                        <%#Eval("ckbasename")%>
                      
                    </td>
                    <td>
                        <%#Eval("inserttime")%>
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
          <div class="page ">
                <asp:Literal ID="lbInfo" runat="server"></asp:Literal>
              
            </div>
    </div>
  <asp:HiddenField ID="Hiddencode" runat="server" />

    </form>
</body>
</html>
