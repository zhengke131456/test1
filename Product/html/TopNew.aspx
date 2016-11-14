<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TopNew.aspx.cs" Inherits="product.html.TopNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html >
 <head>
 
  <title>Document</title>
       <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/admin.css" rel="stylesheet" type="text/css" />
 </head>


 <body>
 <form id="Form1"   runat="server" >
 <table cellspacing="0" cellpadding="0" width="100%" background="../images/header_bg.jpg"
        border="0">
        <tr height="56">
            <td width="260">
             
                <img height="56" src="../images/header_left.jpg" width="260">
            </td>
            <td style="font-weight: bold; color: #fff; padding-top: 20px" align="middle">
                当前用户： <%=userName%>&nbsp;&nbsp; <a style="color: #fff" onclick="delCookie('userName')" >退出系统</a>
              

            </td>
            <td align="right" width="268">
                <img height="56" src="../images/header_right.jpg" width="268">
            </td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr bgcolor="#1c5db6" height="4">
            <td>
          
            </td>
        </tr>
    </table>

     <script type="text/javascript">
         function delCookie(id) {
             if (confirm("是否要退出系统！")) {
                 $.post("../Handler/clearCookie.ashx?id=" + id, {}, function (data) {
                     if (data == "1") {
                         window.parent.location = '../login.aspx';

                     }

                 }, "Text");
             }
         }
    </script>
     </form>
 </body>
</html>