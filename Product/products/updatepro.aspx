<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updatepro.aspx.cs" Inherits="product.products.updatepro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <style type="text/css">
        select
        {
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="subnav">
        睿配编码
    </div>
    <div id="right">
        <table width="100%" cellpadding="2" id="shenhe">
            <tr>
                <th width="20%">
                    睿配编码：
                </th>
                <td>
                    <input id="rpcode" name="rpcode" runat="server"  />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    石化商品编码：
                </th>
                <td>
                    <input id="ShGoogcode" name="ShGoogcode" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    供应商编码：
                </th>
                <td>
                    <input id="CAD" name="CAD" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    河北海信编码：
                </th>
                <td>
                    <input id="hbcode" name="hbcode" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    浙江海鼎编码：
                </th>
                <td>
                    <input id="zjcode" name="zjcode" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    品类：
                </th>
                <td>
                    <input id="pinglei" name="pinglei" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    品牌：
                </th>
                <td>
                    <input id="pingpai" name="pingpai" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    Dimension：
                </th>
                <td>
                    <input id="Dimension" name="Dimension" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    横截面宽：
                </th>
                <td>
                    <input id="AcrossWidth" name="AcrossWidth" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    高宽比：
                </th>
                <td>
                    <input id="GKB" name="GKB" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    R：
                </th>
                <td>
                    <input id="R" name="R" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    轮辋直径：
                </th>
                <td>
                    <input id="Rimdia" name="Rimdia" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    花纹：
                </th>
                <td>
                    <input id="PATTERN" name="PATTERN" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    特别指示：
                </th>
                <td>
                    <input id="Mark" name="Mark" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    载重指数：
                </th>
                <td>
                    <input id="LOADINGs" name="LOADINGs" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    速度级别：
                </th>
                <td>
                    <input id="SPEEDJb" name="SPEEDJb" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    EXTRA LOAD：
                </th>
                <td>
                    <input id="EXTRALOAD" name="EXTRALOAD" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    OE：
                </th>
                <td>
                    <input id="OE" style="width: 200px" name="OE" runat="server" />
                </td>
            </tr>
            
            <tr>
                <th width="20%">
                    Segment：
                </th>
                <td>
                    <input id="Segment" style="width: 350px" name="Segment" runat="server" />
                </td>
            </tr>
            <tr>
                <th width="20%">
                    DES：
                </th>
                <td>
                    <input id="des" style="width: 350px" name="des" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" bgcolor="#F3F7F9" align="center">
                    <a style="color: Red;" href="javascript:history.go(-1)">返回</a> &nbsp;
                    <asp:Button ID="Button1" runat="server" Text="提   交" OnClick="Button1_Click"   />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
