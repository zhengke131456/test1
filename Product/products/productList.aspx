<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productList.aspx.cs" Inherits="product.products.productList" %>

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
        产品列表
    </div>
    <div id="right">
        <div class="page "> 
            <p class="left" <%=styledisplay%>  >
         

                <a href="../products/addpro.aspx"><strong>新增产品</strong></a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                  <strong>是否上线</strong>
                <input id="chck" name="model" type="checkbox" runat="server"  />
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
                            睿配编码
                        </th>
                        <th width="5%">
                            石化商品编码
                        </th>
                        <th width="5%">
                            供应商编码
                        </th>
                        <th width="5%">
                            品类
                        </th>
                        <th width="10%">
                            Dimension
                        </th>
                       <%-- <th width="3%">
                            横截面宽
                        </th>
                        <th width="3%">
                            高宽比
                        </th>
                        <th width="3%">
                            R
                        </th>
                        <th width="10%">
                            轮辋直径
                        </th>--%>
                        <th width="8%">
                            花纹
                        </th>
                     <%--   <th width="3%">
                            特别标示
                        </th>--%>
                        <th width="3%">
                            载重指数
                        </th>
                        <th width="3%">
                            速度级别
                        </th>
                      <%--  <th width="5%">
                            EXTRA LOAD
                        </th>
                        <th width="10%">
                            Segment
                        </th>
                        <th width="15%">
                            OE
                        </th>--%>

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
                        <%#Eval("rpcode")%>
                    </td>
                    <td>
                        <%#Eval("ShGoogcode")%>
                    </td>
                    <td>
                        <%#Eval("CAD")%>
                    </td>
                    <%--<td>
                        <%#Eval("pinglei")%>
                    </td>--%>
                    <td>
                        <%#Eval("pingpai")%>
                    </td>
                    <td>
                        <%#Eval("Dimension")%>
                    </td>
                   <%-- <td>
                        <%#Eval("AcrossWidth")%>
                    </td>
                    <td>
                        <%#Eval("GKB")%>
                    </td>
                    <td>
                        <%#Eval("R")%>
                    </td>
                    <td>
                        <%#Eval("Rimdia")%>
                    </td>--%>
                    <td>
                        <%#Eval("PATTERN")%>
                    </td>
                    <%--<td>
                        <%#Eval("Mark")%>
                    </td>--%>
                    <td>
                        <%#Eval("LOADINGs")%>
                    </td>
                    <td>
                        <%#Eval("SPEEDJb")%>
                    </td>
                   <%-- <td>
                        <%#Eval("EXTRALOAD")%>
                    </td>
                    <td>
                        <%#Eval("Segment")%>
                    </td>
                    
                    <td>
                        <%#Eval("OE")%>
                    </td>--%>
                       
                    <td>
                       <div <%=iscity%>  >
                        <a href="../Order/priceMaintainNew.aspx?cad=<%#Eval("rpcode")%>">添加价格</a>
                      </div> 
                        <div <%=styledisplay%>  >
                        <a href="../products/updatepro.aspx?id=<%#Eval("id")%>">编辑</a>
                   <%--     |<a href="javascript:del('<%#Eval("id")%>')">删除</a>--%>
                      </div> 
                      <a href="../products/updatepro.aspx?type=ck&id=<%#Eval("id")%>">查看</a>
                  <%--    <a href="../upload/UpPoductImage.aspx?rpcode=<%#Eval("rpcode")%>">图片管理</a>--%>
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
                $.post("../delete/delete.aspx?table=products&id=" + id, {}, function (data) {
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
