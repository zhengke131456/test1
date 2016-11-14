<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addpro.aspx.cs" Inherits="product.products.addpro" %>

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
</head>
<body>
    <form id="form1" runat="server">
       <div class="subnav">
        产品添加
    </div>
    <div id="right"> 
        <table width="100%" cellpadding="2" id="shenhe"> 
            <tr>
                <th width="20%">
                    编码：
                </th>
                <td>  
                    <asp:DropDownList ID="dpcode" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>  
              <tr>
                <th width="20%">
                    速度标识：
                </th>
                <td>  
                    <asp:DropDownList ID="dpspeed" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>  
              <tr>
                <th width="20%">
                    载重指数：
                </th>
                <td>  
                    <asp:DropDownList ID="dpload" runat="server">
                    </asp:DropDownList>
                </td>
            </tr> 
              <tr>
                <th width="20%">
                    轮毂直径：
                </th>
                <td>  
                    <asp:DropDownList ID="dpzj" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <th width="20%">
                    花纹：
                </th>
                <td>  
                    <asp:DropDownList ID="dphw" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <th width="20%">
                   适用季节：
                </th>
                <td>  
                    <asp:DropDownList ID="dpseason" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <th width="20%">
                    型号：
                </th>
                <td>  
                    <asp:DropDownList ID="dpxh" runat="server">
                    </asp:DropDownList>
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
