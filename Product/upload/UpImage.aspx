<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpImage.aspx.cs" Inherits="product.upload.UpImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>上传图片</title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
  
</head>
<body   >
    <form id="form1" runat="server">
    <div class="subnav">
        上传图片
    </div>
    <div id="right">
        <div class="page" style="background-color: #D1EDF2">
            <p class="left">
                <asp:Label ID="Label2" runat="server" Text="上传图片：" ForeColor="#666699"></asp:Label>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="ButtonUp" runat="server" Style="height: 30px; margin-left: 100px;
                    margin-top: 25px" Text="上传" Width="58px" CommandName="ww" 
                    OnClick="ButtonUp_Click" />
           
                     <%-- <a  style="color: Red;" href="../upload/UpPoductImage.aspx?rpcoe=<%=rpcode%>">返回</a>--%>
                   <a  style="color: Red;" href="javascript:history.go(-1)">返回</a> 
                <br />
              
            </p>
            <div class="clear">
            </div>
        </div>
        
    </div>
    </form>
  
</body>
</html>