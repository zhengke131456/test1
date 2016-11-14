<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPassword.aspx.cs" Inherits="product.user.UserPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>密码修改</title>
      <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
      <script type="text/javascript" language="javascript">
          function check() {

              if ($("#pwd").val() == "") {
                  alert("密码不能为空");
                  return false;
              }
              if ($("#pwd").val() != $("#pwdnew").val()) {
                  var pwd = document.getElementById("pwd");
                  pwd.focus();
                  alert("两次密码不一致");
                  return false
              }
          }
          function myFunction() {
              if ($("#pwd").val() != $("#pwdnew").val()) {
                  var pwd = document.getElementById("pwd");
                  pwd.focus();
                  alert("两次密码不一致");
              }
          }
      </script>
</head>
<body>
      <form id="form1" name="form1" method="post"  runat="server"  onsubmit="return check();">
   <div class="subnav">
        密码修改
    </div>
    <div id="right"> 
        <table width="100%" cellpadding="2" id="shenhe"> 
          <tr>
                <th width="20%">
                    新密码：
                </th>
                <td> 
                 <input id="pwd" type="password" name="pwd" style="width :35%"  /> 
                </td>
            </tr>  
              <tr>
                <th width="20%">
                    确认密码：
                </th>
                <td> 
                 <input id="pwdnew" type="password" name="pwdnew"  onblur="myFunction();" style="width :35%" /> 
                </td>
            </tr>  
              <tr>
                <th width="35%">
                    邮箱：
                </th>
                <td> 
                 <input id="email" type="text" name="email" value="<%=email %>" style="width :35%"    /> 
                </td>
            </tr> 

             <tr>
           <th width="20%">
                   
                </th>
                 <td colspan="3" bgcolor="#F3F7F9" align="center">
                    <a style="color:Red;" href="javascript:history.go(-1)">返回</a> &nbsp; <asp:Button ID="Button1" runat="server" Text="提   交" OnClick="Button1_Click" OnClientClick="return check();" />
                </td>
      
            </tr> 
        </table>
        
    </div>
    </form>
</body>
</html> 
