<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="baseinfoStore.aspx.cs" Inherits="product.baseinfo.baseinfoStore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
      
</head>
<body>
    
  <form id="Form1"   runat="server" >
       <div class="subnav">
       仓库管理
    </div>
    <div id="right">
        <div class="page ">
            <p class="left"  <%=styledisplay%> >
               <a href="../baseinfo/adddatestore.aspx"  ><strong>新增仓库</strong></a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
                </p>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 仓库名称：<input id="BM" name="BM" type="text" value= "<%=Code %>" />
                 <asp:Button runat="server" ID="btnQuerey" Text="查询" 
                onclick="btnQuerey_Click" />
            <p class="right">每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount" runat="server"></asp:Label></strong>条</p>
          

        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table width="100%" id="list">
                    <tr>
                        <th width="2%">
                            ID
                        </th>
                        <th width="20%"  >
                            编码
                        </th> 
                         <th width="20%"  >
                            名称
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
                    <td>
                        <%#Eval("Basecode")%>
                    </td>
                    <td>
                        <%#Eval("basename")%>
                    </td>
                     <td  >
                        <%#Eval("ispoint")%>
                    </td>
    
                    <td>
                     <div <%=styledisplay%>  >
                        <a href="../baseinfo/adddatestore.aspx?id=<%#Eval("id")%>">添加下级仓库</a> | <a href="../baseinfo/updatestore.aspx?id=<%#Eval("id")%>">
                            编辑</a>| <a href="javascript:del('<%#Eval("id")%>')">删除</a> |<a href="../products/mdpoint.aspx?id=<%#Eval("id")%>&tablename=BaseStore&type=store">坐标</a>
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
  <asp:HiddenField ID="Hiddentype" runat="server" />
    <script type="text/javascript">
        function del(id) {
            if (confirm("是否删除！")) {
                $.post("../delete/delete.aspx?table=BaseStore&id=" + id, {}, function (data) {
                    if (data.result == "100") {
                        alert("删除成功！");
                        $("#base" + id)[0].style.display = "none";
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
