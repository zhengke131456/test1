<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TJ_OutStock.aspx.cs" Inherits="product.products.TJ_OutStock" %>

<%@ Register TagPrefix="uc1" TagName="ck1" Src="~/cktree.ascx" %>
   

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Scripts/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript" >
        function ckclear() {
            document.getElementById("ckids").value = "";
            $("#showckname").html("");
        }
    </script>
   
</head>
<body>
   
    <div class="subnav">
        出库信息汇总
    </div>
    <div id="right">
        <div class="page ">
            <p class="right">
                每页<%=pagesize%>条 &nbsp; 符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p>
            <div class="clear">
            </div>

             <form id="form1" style="display:inline-block; float:left;" method="post" action="TJ_outStock.aspx">

            <div class="page ">

               <uc1:ck1 ID="showck" name="showck" runat="server"/> 

                <input id="Button1" type="button" onclick="ckclear();"  value="全部仓库" />
                
                <strong>出货日期起</strong>
                <input id="bdata" name="bdata" type="text" value="<%=Bdate %>" onfocus="WdatePicker({skin:'whyGreen',minDate: '2015-11-18', maxDate: '2020-12-28' })" />
                <strong>出货日期结</strong>
                <input id="edata" name="edata" type="text" value="<%=Edate %>" onfocus="WdatePicker({skin:'whyGreen',minDate: '2015-11-18', maxDate: '2020-12-28' })" />
               
                <strong>睿配编码</strong>
                <input id="Rpcode" name="rpcode" type="text" value="<%=rpcode %>" />

                 <input id="ckids" name="ckids" type="hidden" value="<%=ckids %>" />

                 <input type="submit" value="查询" /> <span id="showckname"></span>
            </div>


                </form>

                 <form id="form3" runat="server" style="width:100px; float:left;">
                <asp:Button ID="Button2" runat="server" OnClick="Buttondow_Click" Text="下载" Width="54px" />
                <asp:HiddenField ID="hiddpart" runat="server" />
                </form>

        </div>


        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table id="list">
                    <tr>
                       
                         <th width="6%">
                            出库仓库编码
                        </th>

                        <th width="6%">
                            出库仓库名称
                        </th>

                        <th width="8%">
                            睿配编码
                        </th>

                        <th width="8%">
                            CAD
                        </th>

                         <th width="6%">
                            型号
                        </th>

                        <th width="6%">
                            花纹
                        </th>

                        <th width="3%">
                            实际数量
                        </th>

                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                     <td>
                        <%#Eval("WHcode")%>
                    </td>

                    <td>
                        <%#Eval("basename")%>
                    </td>
                   

                    <td>
                        <%#Eval("rpcode")%>
                    </td>

                     <td>
                        <%#Eval("cad")%>
                    </td>

                    <td>
                        <%#Eval("xh")%>
                    </td>

                    <td>
                        <%#Eval("pattern")%>
                    </td>
                   
                    <td>
                        <%#Eval("QTY")%>
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


</body>
</html>
