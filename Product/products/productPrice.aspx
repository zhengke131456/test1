<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productPrice.aspx.cs" Inherits="product.products.productPrice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
       <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            $.each($("input[name=bdata]"), function () {
                $(this).focus(function () { WdatePicker({ readOnly: true }); });
                $(this).click(function () { WdatePicker({ readOnly: true }); });

            });
          
         
        });
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="subnav">
         产品价格
    </div>
    <div id="right">
        <div class="page ">
            <p class="left">
               <a href="../products/addproPrice.aspx"  ><strong>新增产品价格</strong></a>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="shangchuan" runat="server"  OnClick="Button1_Click" Text="上　传" Width="54px" />
                </p>
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>
             <div class="page ">
                <strong>年份</strong>
                    <asp:DropDownList Width="100" ID="dpyary" runat="server">
                     </asp:DropDownList>
                <strong>编码</strong>
                <input id="QCAI" name="QCAI" type="text" value="<%=cad %>"   />
                <strong>型号</strong>
                <asp:DropDownList Width="180px" ID="dpmodel" runat="server">
                     </asp:DropDownList>
                <asp:Button runat="server" ID="btnQuerey" Text="查询" OnClick="btnQuerey_Click" />
            </div>
        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table width="100%" id="list">
                    <tr>
                        <th width="5%">
                            ID
                        </th>
                      
                         <th width="10%">
                            编码
                        </th> 
                        <th width="10%">
                            型号
                        </th> 
                         <th width="35%">
                            DES
                        </th> 
                         <th width="8%">
                            批发价格
                        </th> 
                         <th width="8%">
                            进货价格
                        </th> 
                         <th width="8%">
                            年份
                        </th> 
                           <th width="8%">
                            批次
                        </th> 
                        <th width="20%">
                            操作
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr id="base<%#Eval("ID")%>">
                    <td>
                        <%#Eval("ID")%>
                    </td>
                    <td>
                        <%#Eval("PRSP_CAD")%>
                    </td>
                     <td>
                        <%#Eval("Model")%>
                    </td>
                     <td>
                        <%#Eval("DES")%>
                    </td>
                      <td>
                        <%#Eval("PRSP_WHSPrice")%>
                    </td>
                      <td>
                        <%#Eval("PRSP_Inprice")%>
                    </td>
                      <td>
                        <%#Eval("PRSP_Year")%>
                    </td>
                    <td>
                        <%#Eval("PRSP_number")%>
                    </td>
                     
                    <td>
                        <a href="../products/updateProPrice.aspx?id=<%#Eval("ID")%>"> 编辑</a>|<a href="javascript:del('<%#Eval("ID")%>')">删除</a>
                           
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
                $.post("../delete/delete.aspx?table=ProductPrice&id=" + id, {}, function (data) {
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
