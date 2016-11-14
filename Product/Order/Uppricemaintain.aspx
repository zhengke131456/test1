<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Uppricemaintain.aspx.cs"
    Inherits="product.Order.Uppricemaintain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
      <script type="text/javascript" language="javascript">
       function check() {

              if ($("#xianjia").val() == "" ) {
                 
                      alert("现价不能为空");
                      return false;
                
              }

                  if ($("#yuanjia").val() == "") {
                  alert("原价不能为空");
                  return false;
              }
          }
            </script>
</head>
<body >
    <form id="form1" name="form1" method="post" runat="server"  onsubmit="return check();" >
    <div class="subnav">
        价格修改
    </div>
    <div id="right">
        <table width="100%" cellpadding="2" id="shenhe">
            <tr id="codeid">
                <th width="20%">
                    睿配编码：
                </th>
                <td>
                    <input id="rpcode" type="text" runat="server"  readonly="readonly" disabled="disabled" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    城市：
                </th>
                <td>
                    <input id="citycode" type="text"  runat="server" readonly="readonly" disabled="disabled" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    现价：
                </th>
                <td>
                    <input id="xianjia" type="text"  runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    原价：
                </th>
                <td>
                    <input id="yuanjia" type="text" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    是否上线：
                </th>
                <td>
                    <input id="ischeck"  type="checkbox"  runat="server"  name="isshangxian"  />
                </td>
            </tr>
            <tr>
                <td colspan="3" bgcolor="#F3F7F9" align="center">
                    <a style="color: Red;" href="javascript:history.go(-1)">返回</a> &nbsp;&nbsp;<asp:Button
                        ID="Button1" runat="server" Text="提   交" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hiddpid" runat="server" />
   <asp:HiddenField ID="hiddcity" runat="server" />
  
    </form>
</body>
</html>
