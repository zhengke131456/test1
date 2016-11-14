<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updateRebate.aspx.cs" Inherits="product.products.updateRebate" %>

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
        返利修改
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
                <th width="7%">
                    返利1：
                </th>
                <td>  
                   <asp:TextBox ID="Texfl1" runat="server"></asp:TextBox>
                </td>
            </tr>  
              <tr>
                <th width="7%">
                    返利2：
                </th>
                <td>  
                <asp:TextBox ID="Texfl2" runat="server"></asp:TextBox>
                </td>
            </tr> 
             <tr>
                <th width="7%">
                    返利3：
                </th>
                <td>  
                <asp:TextBox ID="Texfl3" runat="server"></asp:TextBox>
                </td>
            </tr>  <tr>
                <th width="7%">
                    返利4：
                </th>
                <td>  
                <asp:TextBox ID="Texfl4" runat="server"></asp:TextBox>
                </td>
            </tr>  <tr>
                <th width="7%">
                    返利5：
                </th>
                <td>  
                <asp:TextBox ID="Texfl5" runat="server"></asp:TextBox>
                </td>
            </tr> 
              <tr>
                <th width="20%">
                   年份：
                </th>
                <td>  
                    <asp:DropDownList ID="dpYear" runat="server">
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
