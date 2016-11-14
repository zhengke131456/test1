<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpOrderAllot.aspx.cs" Inherits="product.Order.UpOrderAllot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单操作</title>
      <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">

         $(function () {

             $.each($("input[name=QCDate]"), function () {
                 $(this).focus(function () { WdatePicker({ dateFmt: 'yyyyMMdd' }); });
                 $(this).click(function () { WdatePicker({ dateFmt: 'yyyyMMdd' }); });

             });

         });
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="subnav">
         下单管理
    </div>
    <div id="right">
        <div class="page ">
        
            <p class="right">
               
              
                符合条件的记录有<strong style="color: Red"><asp:Label ID="lblCount"
                    runat="server"></asp:Label></strong>条</p> &nbsp; &nbsp; &nbsp;
                        <a style="color: Red;" href="../Order/Orderbatch.aspx">返回</a> 
            <div class="clear">
            </div>
            <div class="page ">
               <strong>查货日期</strong>
                <input id="QCDate" name="QCDate" type="text" value="<%=QCDate %>"   />
                <strong>CAD</strong>
                <input id="CAD" name="CAD" type="text" value="<%=cai %>"   />
                <strong>客户编码</strong>
               <input id="clientCode" name="clientCode" type="text" value="<%=clientCode %>"   />
                       <strong>状态</strong>
             <asp:DropDownList Width="80px" ID="dpstatus" runat="server" name="dpstatus">
             <asp:ListItem  Selected="True" Value="">全部</asp:ListItem>
            
                    <asp:ListItem value="0">未下载</asp:ListItem>
                   <asp:ListItem value="1">已下载</asp:ListItem>
                    <asp:ListItem value="2">查询有货</asp:ListItem>
                   <asp:ListItem value="3">查询部分有货</asp:ListItem>
                     <asp:ListItem value="4">查询无货</asp:ListItem>
                    <asp:ListItem value="5">下单有货</asp:ListItem>
                    <asp:ListItem value="6">下单部分有货</asp:ListItem>
                      <asp:ListItem value="7">下单无货</asp:ListItem>
                    <asp:ListItem value="8">取消发货</asp:ListItem>
                  </asp:DropDownList>
                   &nbsp   &nbsp   &nbsp
                <asp:Button runat="server" ID="btnQuerey" Text="查询" OnClick="btnQuerey_Click" />
            </div>
        </div>
        <asp:Repeater runat="server" ID="dislist">
            <HeaderTemplate>
                <table width="100%" id="list">
                    <tr>
                        <th width="3%">
                            ID
                        </th>
                        <th width="3%">
                            选择
                        </th>
                         <th width="7%" >
                            查货日期
                        </th> 
                         <th width="5%">
                            CAD
                        </th> 
                         <th width="5%">
                            状态
                        </th> 
                         <th width="20%">
                            DESC
                        </th> 
                         <th width="5%">
                          查询数量
                        </th> 
                           <th width="5%">
                           分配数量
                        </th> 
                        <th width="5%">
                           客户编码
                        </th> 
                        <th width="5%">
                            下单人
                        </th> 
                          </th> 
                           <th width="4%">
                            中转仓
                        </th> 
                           <th width="10%">
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
                     <input type="checkbox" id="checkboxID" runat="server"  class="chkselect" value= <%#Eval("id")%>/>
                    </td>
                    <td>
                        <%#Eval("QueryDate")%>
                    </td>
                    <td>
                        <%#Eval("CAD")%>
                    </td>
                    <td>
                        <%#Eval("status")%>
                    </td>
                    <td>
                        <%#Eval("Odesc")%>
                    </td>
                    <td>
                        <%#Eval("number")%>
                    </td>
                     <td>
                        <%#Eval("allotNum")%>
                    </td>
                    <td>
                        <%#Eval("ClientCode")%>
                    </td>
                    <td>
                       <%#Eval("placeorderName")%>
                    </td>
                    <td>
                        <%#Eval("storagename")%>
                    </td>

                    <td>
                
                  
                        <a id="compile"  href="../Order/UpdateOrderAllot.aspx?id=<%#Eval("id")%>">修改</a>
                        <%--|<a id="del" " href="javascript:del('<%#Eval("id")%>')">删除</a>--%>
                    
                    </td>
                   
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
        <div class="Pages">
            <div class="left">
                <input type="button" id="Button2" value="设置选中为无货"  style=" width: 100px; "  onclick="isOnButton()" />
            </div>
            <div class="center">
                <asp:Button runat="server" ID="Button1" Text="全部提交" OnClick="Button1_Click" />
            </div>
        </div>
             <input type="hidden" id="hidbatch" runat="server" />
    </div>
    <script type="text/javascript">
        function del(id) {
            if (confirm("是否删除！")) {
                $.post("../delete/delete.aspx?table=Placeorder&id=" + id, {}, function (data) {
                    if (data.result == "100") {
                        alert("删除成功！");

                        $("#base" + id)[0].style.display = "none";
                    }
                    else
                    { alert("删除失败！"); }
                }, "json");
            }
        }


        function isOnButton() {
    
          

            if (confirm("确认无货！")) {

                var str = "";
                $('.chkselect').each(function () {
                    if ($(this).attr("checked")) {
                        // alert($(this).attr('id'));
                        var id = $(this).attr('id');
                        var valvalue = $("#" + id + "").val();
                 
//                       <a href="../Order/UpOrderAllot.aspx?batch="+batch+"">
                             
                        str += valvalue + ",";

                    }
                });
        
//                str = str.substring(0, str.length - 1);
                //获取选中的 iD
              
                $.post("../Handler/updateoeder.ashx?table=Placeorder&id=" + str, {}, function (data) {
                    if (data.result == "0") {

                        alert("成功！");
                        $('.chkselect').each(function () {
                            $(this).attr("checked","")
                        });
                        window.location.reload();
                    }
                    else
                    { alert("失败！"); }
                }, "json");
            }
    
        
        }
    </script>
    </form>
</body>
</html>

