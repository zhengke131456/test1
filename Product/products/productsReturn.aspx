<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productsReturn.aspx.cs" Inherits="product.products.productsReturn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="subnav">
         货物退回
    </div>
    <div id="right">
        <div class="page ">
            <p class="left" id="addp" >
                <strong>退回：</strong>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="shangchuan" runat="server" OnClick="Button1_Click" Text="上　传" Width="54px" />
            </p>
           
        </div>
    </div>
     <asp:HiddenField ID="hiddpart" runat="server" />
    </form>
</body>
</html>
