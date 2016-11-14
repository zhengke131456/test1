<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addclientInfo.aspx.cs" Inherits="product.Order.addclientInfo" %>

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
      
              if ($("#txtcode").val() == "" ) {
                  tixing += "客户编码不能为空\\n";
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
        新增客户
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
                <th width="40%">
                    地址：
                </th>
                <td>
                    <input id="addres" type="text" name="addres" />
                </td>
            </tr>
            
            <tr>
                <th width="40%">
                    备注：
                </th>
                <td>
                    <input id="Note" type="text" name="Note" runat="server" />
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
