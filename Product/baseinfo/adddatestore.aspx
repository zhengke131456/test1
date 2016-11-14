<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adddatestore.aspx.cs" Inherits="product.baseinfo.adddatestore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
      <script type="text/javascript" language="javascript">
          function check() {

              if ($("#code").val() == "") {
                      alert("编码不能为空");
                      return false;
                  }
        
              if ($("#basename").val() == "") {
                  alert("名称不能为空");
                  return false;
              }
          }
       
      </script>
</head>
<body >
      <form id="form1" name="form1" method="post"  runat="server"  onsubmit="return check();">
   <div class="subnav">
         添加仓库
    </div>
 <div id="right">
        <table width="100%" cellpadding="2" id="shenhe">
           <tr  <%=style %> >
                <th  width="20%"  >
                    上级仓库：
                </th>
                <td>
                    <input id="Text1" type="text" name="Text1"  value= "<%=basename %>" readonly="readonly"  style=" background-color: Gray"   />
                </td>
            </tr>
            <tr id="codeid">
                <th  width="20%">
                    仓库编码：
                </th>
                <td>
                    <input id="code" type="text" name="code"  />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    仓库名称：
                </th>
                <td>
                    <input id="name" type="text" name="name"  />
                </td>
            </tr>
           <tr>
                <th width="20%">
                    城市：
                </th>
                <td>
                    <asp:DropDownList Width="180px" ID="dptown" runat="server">
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
    <input type="hidden" id="hiddID" runat="server" />
    <input type="hidden" id="basecode" runat="server" />
    </form>
</body>
  
</html> 