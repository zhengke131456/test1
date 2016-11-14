<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="proStore_Store.aspx.cs"
    Inherits="product.products.proStore_Store" %>

    <%@ Register TagPrefix="uc1" TagName="ck1" Src="~/cktree.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Scripts/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ui-autocomplete
        {
            max-height: 200px;
            overflow-y: auto; /* 防止水平滚动条 */
            overflow-x: hidden;
            background-color: #CCCCCC;
            width: 200px;
        }
    </style>
    <script type="text/javascript">

        function ckclear() {
            document.getElementById("ckids").value = "";
            $("#showckname").html("");
        }
    </script>
</head>
<body>
   
    
    <div class="subnav">
        库存信息
    </div>
    <div id="right">
        <div class="page ">

            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>

            <div class="clear"></div>


            <div class="page ">
                
                <form id="form2" method="post" style="display:inline-block; float:left;" action="proStore_Store.aspx">
                
                <uc1:ck1 ID="showck" name="showck" runat="server"/> 

                <input id="Button2" type="button" onclick="ckclear();"  value="全部仓库" />


                <strong>睿配编码</strong>
                <input id="QCAI" name="QCAI" type="text" value="<%=cai %>" />
                

              <strong>型号</strong>
              <select id="dimension" name="dimension" >
              <%=Opstr %>
              </select>
                



                <input id="ckids" name="ckids" type="hidden" value="<%=ckids %>" />
                <input type="submit" value="查询" /> 

                &nbsp;&nbsp;&nbsp;<span id="showckname"></span> 

               </form>

                 <form id="form3" runat="server" style="width:100px; float:left;">
                <asp:Button ID="Button1" runat="server" OnClick="Buttondow_Click" Text="下载" Width="54px" />
                <asp:HiddenField ID="hiddpart" runat="server" />
                </form>
            </div>


        </div>
        <div style="overflow: auto; width: 100%; height: 550px;">
            <asp:Repeater runat="server" ID="dislist">
                <HeaderTemplate>
                    <table width="100%" id="list">
                        <tr>
                            <th width="5%">
                                ID
                            </th>
                             <th width="10%">
                                仓库编码
                            </th>
                            <th width="10%">
                                仓库
                            </th>
                            <th width="10%">
                                睿配编码
                            </th>
                            <th width="10%">
                                供应商编码
                            </th>
                            <th width="10%">
                                型号
                            </th>
                            <th width="10%">
                                花纹
                            </th>
                            <th width="6%">
                                实际
                            </th>
                             <th width="6%">
                                计提
                            </th>
                           
                            <th width="50%">
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
                            <%#Eval("Basecode")%>
                        </td>
                        <td>
                            <%#Eval("stockName")%>
                        </td>
                        <td>
                            <%#Eval("rpcode")%>
                        </td>
                        <td>
                            <%#Eval("cad")%>
                        </td>
                        <td>
                            <%#Eval("Dimension")%>
                        </td>
                        <td>
                            <%#Eval("PATTERN")%>
                        </td>
                        <td>
                            <%#Eval("stockNum")%>
                        </td>
                        <td>
                            <%#Eval("stockjtNum")%>
                        </td>
                        
                        <td>
                            <a href="../products/ProductInStore.aspx?storeID=<%#Eval("stockID")%>&CAI=<%#Eval("rpcode")%>">
                                入库明细</a>| <a href="../products/ProductOutStore.aspx?storeID=<%#Eval("stockID")%>&CAI=<%#Eval("rpcode")%>">
                                    出库明细</a>
                        </td>
                    </tr>
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
</body>
</html>

<script type="text/javascript">

    //默认选中调拨状态
    $("#dimension").val('<%=dimension %>');

</script>

