<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Store_permutation.aspx.cs" Inherits="product.products.Store_permutation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title></title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/time/Calendar/WdatePicker.js" type="text/javascript"></script>

</head>
<body onload="fun()">
    <form id="form1" runat="server">
      <div class="subnav">
         货物置换
    </div>
    <div id="right">
        <div class="page ">
            <p class="left" id="addp" >
                <strong>置换：</strong>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="shangchuan" runat="server" OnClick="Button1_Click" Text="上　传" Width="54px" />
            </p>
           
        </div>
    </div>
     <asp:HiddenField ID="hiddtype" runat="server" />
       <asp:HiddenField ID="hiddpart" runat="server" />
    <script type="text/javascript">
        function del(id) {
            if (confirm("是否删除！")) {
                $.post("../delete/delete.aspx?table=inproduct&type=SINOPEC&id=" + id, {}, function (data) {
                    if (data.result == "100") {
                        alert("删除成功！");
                        $("#base" + id)[0].style.display = "none";
                    }
                    else
                    { alert("删除失败！"); }
                }, "json");
            }
        }
    </script>
    </form>
</body>
</html>
