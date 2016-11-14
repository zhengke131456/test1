<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpPoductImage.aspx.cs"
    Inherits="product.upload.UpPoductImage"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传图片</title>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
 
</head>
<body   style="background-color: #A0E8F4">
    <form id="form1" runat="server">
    <div class="subnav">
        图片管理
    </div>
    <div id="right">
        <div class="page" style="background-color: #D1EDF2">
            <p class="left">
        
            </p>
            <div class="clear">
            </div>
        </div>
        <div>
            <asp:DataList ID="DataList1" BackColor="#A0E8F4" runat="server" RepeatColumns="4"
                Width="100%">
                <ItemTemplate>
                    <div id="base<%#Eval("columnName")%>" style="height: 140px; margin-bottom: 20px;
                        margin-top: 55px;">
                        <div style="width: 160px; margin: auto; height: 120px; border: 1px solid gray;">
                            <img alt="图片" width="160px" height="120px" style="margin: auto; border: none;"
                                src="<%#Eval("ImagePath")%>" />
                        </div>
                        <div id="asd" runat="server" style="color: Gray; text-align: center; height: 44px;
                            font-size: xx-small;">
                            <p>
                                <span style="font-size: 20px">
                                    <%#Eval("ImageName")%></span></p>
                            <div style="margin-top: 3px">
                                <input id="delImage" type="button" onclick="del('<%#Eval("rpcode")%>','<%#Eval("columnName")%>','<%#Eval("absolutepath")%>')"
                                    value="删除" />
                                <a href="../upload/UpImage.aspx?rpcode='<%#Eval("rpcode")%>'&columnName='<%#Eval("columnName")%>'" >
                                <img src="../images/sc.png" />
                                 </a>
                                   
                            </div>
                            &nbsp;
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    </form>
    <script type="text/javascript">
        function del(rpcode, columnName, path) {
            if (confirm("是否删除改图片！")) {
                var ss = path;
                $.post("../Handler/DelImage.ashx?rpcode=" + rpcode + "&columnName=" + columnName + "", { path: path }, function (data) {
                    if (data == "100") {
                        alert("删除成功！");
                        window.location.reload();

                    }
                    else if (data == "200") {
                        alert("文件不存在！");
                    }
                    else {
                        alert("删除失败！");
                    }
                }, "json");
            }
        }
  
    </script>
</body>
</html>
