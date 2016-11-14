<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="basezshstore.aspx.cs" Inherits="product.baseinfo.basezshstore" %>
<%@ Register TagPrefix="uc1" TagName="pcity" Src="~/Pcity.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
 <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
 <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />

</head>
<body>

      <div class="subnav">
       中石化仓库管理
    </div>


     <div id="right">

      <div class="page ">
      <form id="form1" style="display:inline-block; float:left;" runat="server">

        <div class="page ">

            <p class="left">

               <%if (show1 == "1")
                 { %><a href="../baseinfo/addzshstore.aspx"><strong>新增数据</strong></a> 
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="shangchuan" runat="server" OnClick="Button1_Click" Text="上　传" Width="54px" /><%} %>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <a href="../link/store.csv"><strong style="color:Red;">&nbsp;&nbsp;下载油站入库模板</strong>  </a> &nbsp;&nbsp;&nbsp; 
                 <asp:HiddenField ID="hiddpart" runat="server" />
                 <asp:Button ID="Btnderive" runat="server" OnClick="Btnderive_Click" Text="导出Execl" Width="80px" />

               </p>
               

            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red">
                <asp:Label ID="lblCount" runat="server"></asp:Label></strong>条</p>
           </div>
        
        </form>

          <form id="form3"  method="post" style="width:800px; float:left;" action="basezshstore.aspx">

                 <uc1:pcity ID="showck" name="showck" runat="server"/> 
               
                <strong>油站名称</strong> <input id="basename" name="basename" type="text" value="<%=basename %>" />

               <input type="submit" value="查询" /><%=province %> &nbsp;&nbsp;&nbsp;&nbsp;<%=city %>

            </form>
 </div>

        
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table width="100%" id="list">
                    <tr>
                        <th width="2%">
                            ID
                        </th>
                        <th width="10%">
                          编码 
                        </th>
                         <th width="10%">
                           仓库名称  
                        </th>
                         <th width="5%"  >
                            城市
                         </th> 
                          <th width="5%" >
                           销售类型
                        </th> 
                         <th width="5%" >
                           状态
                        </th> 
                      <%--    <th width="10%" >
                           区域
                        </th> 
                          <th width="10%" >
                           流水号
                        </th> --%>
                         <th width="6%" >
                           站长名
                        </th> 
                         <th width="8%" >
                           电话
                        </th> 
                         <th width="25%" >
                           油站地址
                        </th> 
                        <th width="8%" >
                           备注
                        </th> 
                         <th width="3%" >
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
                        <%#Eval("Basecode")%>
                    </td>
                    <td  >
                        <%#Eval("basename")%>
                    </td>
                    <td  >
                        <%#Eval("cityname")%>
                    </td>
                    <td>
                        <%#Eval("selltype")%>
                    </td>
                     <td>
                        <%#Eval("b_status")%>
                    </td>



                     <td  >
                        <%#Eval("ChinazcomName")%>
                    </td>
                    <td>
                        <%#Eval("phone")%>
                    </td>
                     <td>
                        <%#Eval("address")%>
                    </td>
                       <td  >
                        <%#Eval("notes")%>
                    </td>

                      <td  >
                        <%#Eval("ispoint")%>
                    </td>

                    
                    <td>
                        <div <%#Eval("show")%>  >
                        <a href="../baseinfo/updatezshstore.aspx?id=<%#Eval("id")%>"> 编辑</a>|<a href="javascript:del('<%#Eval("id")%>')">删除</a>|<a href="../products/mdpoint.aspx?id=<%#Eval("id")%>&tablename=BaseStore&type=store">坐标</a>
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
</body>
</html>
