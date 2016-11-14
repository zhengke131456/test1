<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addUserList.aspx.cs" Inherits="product.user.addUserList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <style type="text/css">
    select
    {
        width:100px;
        }
    </style>
      <script type="text/javascript">

          function check() {
  
              var tixing = "";
//              if ($("#txtcode").val() == "admin") {
//                  if ($("#dparea").val() == "") {
//                      tixing += "管理员必须选择区域\\n";
//                  }
//              }
              if ($("#txtcode").val() == "" || $("#txtpassword").val() == "") {
                  tixing += "账号密码不能为为空\\n";
              }

              if ($("#dppart").val() == "请选择") {
                  tixing += "角色不能为空\\n";
              }
              if (tixing != "") {
                  
                  alert(tixing);
                  return false;
              }
              else {

                  return true;
              }
          }

      </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return check();"  >
       <div class="subnav">
        新增人员
    </div>
    <div id="right">
        <table width="100%" cellpadding="2" id="shenhe">
            <tr>
                <th width="20%">
                    账号：
                </th>
                <td>
                       <input id="txtcode" type="text" name="txtcode" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    密码：
                </th>
                <td>
                    <input id="txtpassword" type="text" name="txtpassword" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    角色：
                </th>
                <td>
                    <asp:DropDownList ID="dppart" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th width="20%">
                    区域：
                </th>
                <td>
                    <asp:DropDownList ID="dparea" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th width="20%">
                    手机号：
                </th>
                <td>
                    <input id="phone" type="text" name="phone" runat="server" />
                </td>
            </tr>
              <tr>
                <th width="20%">
                    邮箱：
                </th>
                <td>
                    <input id="Email" type="text" name="Email" runat="server" />
                </td>
            </tr>


            <tr>
                <td colspan="3" bgcolor="#F3F7F9" align="center">
                    <a style="color: Red;" href="javascript:history.go(-1)">返回</a> &nbsp;
                     <asp:Button ID="Button1" runat="server" Text="提   交" OnClick="Button1_Click"  />
                   
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
