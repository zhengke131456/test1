<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPartRight.aspx.cs" Inherits="product.user.UserPartRight" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
      
</head>
<body>
    
  <form id="Form1"   runat="server" >
       <div class="subnav">
       角色管理
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
               <a href="../user/AddUserpart.aspx"><strong>新增角色</strong></a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                        <th width="2%">
                            ID
                        </th>
                         <th width="10%">
                            角色编码
                        </th>  
                        <th width="10%">
                            角色名称
                        </th>  
                        <th width="5%">
                            创建人
                        </th>  
                         <th width="5%">
                            创建时间
                        </th>  
                         <th width="5%">
                            修改时间
                        </th>  
                    
                           <th width="15%">
                            操作
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("SP_ID")%>">
                    <td>
                        <%#Eval("SP_ID")%>
                    </td>
                     <td>
                        <%#Eval("SP_Code")%>
                    </td>
                    <td>
                        <%#Eval("SP_Name")%>
                    </td>
                     <td>
                        <%#Eval("opcode")%>
                    </td>
                     <td>
                        <%#Eval("inserttime")%>
                    </td>
                     <td>
                        <%#Eval("Updatetime")%>
                    </td>
                    <td>
                        <a  href="../user/PartSelectCityArea.aspx?flag=part&id=<%#Eval("SP_ID")%>"><span style="color:Red; font-weight:bold ">  城市权限</span></a>|
                    
                        <a  href="../user/PartSelectstore.aspx?flag=part&id=<%#Eval("SP_code")%>"><span style="color:Red; font-weight:bold ">  分配仓库权限</span></a>|
                        <a href="../user/UpdateUserPart.aspx?id=<%#Eval("SP_ID")%>">编辑</a>  &nbsp;&nbsp;|<a href="javascript:del('<%#Eval("SP_ID")%>')">删除</a>
                        &nbsp;&nbsp;|

                       <a  href="../user/PartSelectRight.aspx?flag=part&id=<%#Eval("SP_ID")%>&code=<%#Eval("SP_code")%>"><span style="color:Red; font-weight:bold ">  模块权限</span></a>
                 
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
                $.post("../delete/delete.aspx?table=SYS_Part&where='SP_ID='&id=" + id, {type:'isdel',set:'sp_del=1' }, function (data) {
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


