<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addNavigationconfig.aspx.cs" Inherits="product.user.addNavigationconfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
              if ($("#txtcode").val() == "" || $("#tetName").val() == "") {
                  tixing += "编码和名称不能为空\\n";
              }

              if ($("#Fnode").val() == "") {
               
                  tixing += "父节点不能为空\\n";
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
        新增功能
    </div>
    <div id="right">
        <table width="100%" cellpadding="2" id="shenhe">
            <tr>
                <th width="20%">
                    编码：
                </th>
                <td>
                    <input id="txtcode" type="text" name="txtcode" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    模块名称：
                </th>
                <td>
                    <input id="tetName" type="text" name="tetName" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    父节点：
                </th>
                <td>
                    <input id="Fnode" type="text" name="Fnode" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    等级：
                </th>
                <td>
                        <input id="level" type="text" name="level" />
                </td>
            </tr>
              <tr>
                <th width="20%">
                    顺序：
                </th>
                <td>
                    <input id="SF_order" type="text" name="SF_order"  />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    URL：
                </th>
                <td>
                    <input id="url" type="text" name="url"  style="width:30%" />
                </td>
            </tr>
            <tr>
                <td colspan="3" bgcolor="#F3F7F9" align="center">
                    <a style="color: Red;" href="javascript:history.go(-1)">返回</a> &nbsp;
                    <asp:Button ID="Button2" runat="server" Text="提   交" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
