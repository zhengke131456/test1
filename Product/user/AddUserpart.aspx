﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUserpart.aspx.cs" Inherits="product.user.AddUserpart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>  
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
       <script type="text/javascript" language="javascript">
           function check() {
               if ($("#code").val() == "" ) {
                   alert("角色名称不能为空");
                   return false;
               }
           }
      </script>
</head>
<body>
     <form id="form1" name="form1"  method="post" runat="server" onsubmit="return check();">
   <div class="subnav">
        增加角色
    </div>
    <div id="right"> 
        <table width="100%" cellpadding="2" id="shenhe"> 
            <tr>
                <th width="6%">
                    角色编码：
                </th>
                <td> 
                 <input id="code" type="text" name="code" value="<%=sp_code %>" readonly="readonly" style="background:#CCCCCC"     /> 
                </td>
            </tr>  
              <tr>
                <th width="10%">
                    角色名称：
                </th>
                <td> 
                 <input id="name" type="text" name="name"   /> 
                </td>
            </tr>  
          
            <tr>
                <td colspan="3" bgcolor="#F3F7F9" align="center">
                    <a style="color:Red;" href="javascript:history.go(-1)">返回</a> &nbsp; <asp:Button ID="Button1" runat="server" Text="提   交" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>

