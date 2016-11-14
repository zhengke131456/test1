<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updatecod.aspx.cs" Inherits="product.baseinfo.updatecod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
      <script type="text/javascript" language="javascript">
          function check() {

              if ($("#hdtypep").val() == "4" || $("#hdtypep").val() == "5") {
                  if ($("#basecode").val() == "") {
                      alert("编码不能为空");
                      return false;
                  }
              }
            
              if ($("#basename").val() == "") {
                  alert("名称不能为空");
                  return false;
              }
          }
          function fun() {

              if ($("#hdtypep").val() != "4" && $("#hdtypep").val() != "5") {

                 
                  document.getElementById("codeid").style.display = "none"
              }


              if ($("#hdtypep").val() != "4" ) {

                  document.getElementById("trart").style.display = "none"
                 
              }
          }
      </script>
</head>
<body onload="fun()">
      <form id="form1" name="form1" method="post"  runat="server"  onsubmit="return check();">
   <div class="subnav">
        修改<%=biaoti %>
    </div>
    <div id="right"> 
        <table width="100%" cellpadding="2" id="shenhe">
            <tr id="codeid">
                <th width="20%">
                    编码：
                </th>
                <td>
                    <input id="basecode" type="text" name="basecode" value="<%=basecode %>" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    名称：
                </th>
                <td>
                    <input id="basename" type="text" name="basename" value="<%=basename %>" />
                </td>
            </tr>
            <tr id="trart">
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
                    <a style="color: Red;" href="javascript:history.go(-1)">返回</a> &nbsp;&nbsp;<asp:Button
                        ID="Button1" runat="server" Text="提   交" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
        
    </div>
      <asp:HiddenField ID="hiddID" runat="server" />
       <asp:HiddenField ID="hdtypep" runat="server" />
    </form>
</body>
  
</html> 