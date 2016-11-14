<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addcod.aspx.cs" Inherits="product.baseinfo.addcod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>  
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
       <script type="text/javascript" language="javascript">
           function check() {
               if ($("#hdtype").val() == "4") {
                 
                   if ($("#code").val() == "") {
                       alert("编码不能为空");
                       return false;
                   }
               }
               if ($("#hdtype").val() == "5") {

                   if ($("#code").val() == "") {
                       alert("编码不能为空");
                       return false;
                   }
               }
             
                   if ($("#name").val() == "") {
                       alert("名称不能为空");
                       return false;
                   }
               
           }
           function fun() {


               if ($("#hdtype").val() != "4" && $("#hdtype").val() != "5") {

                   document.getElementById("codeid").style.display = "none"
                
               }

               if ($("#hdtype").val() != "4" ) {
                   document.getElementById("trid").style.display = "none"
               }
           
              
           }
       </script>
</head>
<body onload="fun()">
     <form id="form1" name="form1"  method="post" runat="server" onsubmit="return check();">
   <div class="subnav">
        增加<%=biaoti %>
    </div>
    <div id="right">
        <table width="100%" cellpadding="2" id="shenhe">
            <tr id="codeid">
                <th  width="20%">
                    编码：
                </th>
                <td>
                    <input id="code" type="text" name="code" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    名称：
                </th>
                <td>
                    <input id="name" type="text" name="name" />
                </td>
            </tr>
            <tr id="trid">
                <th width="20%">
                    区域：
                </th>
                <td>
                    <asp:DropDownList Width="180px" ID="dparea" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="3" bgcolor="#F3F7F9" align="center">
                    <a style="color: Red;" href="javascript:history.go(-1)">返回</a> &nbsp;
                    <asp:Button ID="Button1" runat="server" Text="提   交" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
     
    </div>
     <asp:HiddenField ID="hdtype" runat="server" />
    </form>
</body>
</html>
