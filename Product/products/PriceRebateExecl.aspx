<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PriceRebateExecl.aspx.cs" Inherits="product.products.PriceRebateExecl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
   <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
   
</head>
<body>
    <form id="form1" runat="server">
      <div class="subnav">
        价格返利汇总
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
            <%--   <strong style=" color:Red;">注意：如果返利表或者价格表有新增数据要点击刷新数据按钮从新获取报表数据</strong>
         --%>  
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <%--  <asp:Button ID="Button2" runat="server" Text="刷新数据" onclick="Button2_Click" /> --%>
                
          <asp:Button ID="Button1" runat="server"  Height="25px" Text="导出Execl" OnClick="Button1_Click" />
              </p>
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
           
        </div>
  
        <div style="overflow: auto; width: 100%; height:550px;" >
           <asp:Repeater runat="server" ID="dislist" 
            onitemdatabound="dislist_ItemDataBound">
            <HeaderTemplate>
             <table width="29380px" id="list">
                <asp:Literal ID="lit_head" runat="server"></asp:Literal>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Literal ID="lit_item" runat="server"></asp:Literal>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
        </div>
        <div class="Pages">
            <div class="Paginator">
                <asp:Literal ID="literalPagination" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
   
    </form>
   
</body>
</html>
