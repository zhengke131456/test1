﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftNew.aspx.cs" Inherits="product.html.LeftNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>  
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Navigation.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
      
</head>
<body >
     <form id="form1" name="form1"  runat="server">
     <div id="Menu">
        <asp:Literal ID="want" runat="server"></asp:Literal>
     </div>
   
     <asp:HiddenField ID="hdtype" runat="server" />
    </form>
</body>
<script type="text/javascript">
    var LastLeftID = "";
    function MenuFix() {
        var obj = document.getElementById("nav").getElementsByTagName("li");

        for (var i = 0; i < obj.length; i++) {
            obj[i].onmouseover = function () {
                this.className += (this.className.length > 0 ? " " : "") + "sfhover";
            }
            obj[i].onMouseDown = function () {
                this.className += (this.className.length > 0 ? " " : "") + "sfhover";
            }
            obj[i].onMouseUp = function () {
                this.className += (this.className.length > 0 ? " " : "") + "sfhover";
            }
            obj[i].onmouseout = function () {
                this.className = this.className.replace(new RegExp("( ?|^)sfhover\\b"), "");
            }
        }
    }
    function DoMenu(emid) {
        var obj = document.getElementById(emid);
        obj.className = (obj.className.toLowerCase() == "expanded" ? "collapsed" : "expanded");
        if ((LastLeftID != "") && (emid != LastLeftID)) //关闭上一个Menu
        {
            document.getElementById(LastLeftID).className = "collapsed";
        }
        LastLeftID = emid;
    }
    function GetMenuID() {
        var MenuID = "";
        var _paramStr = new String(window.location.href);
        var _sharpPos = _paramStr.indexOf("#");

        if (_sharpPos >= 0 && _sharpPos < _paramStr.length - 1) {
            _paramStr = _paramStr.substring(_sharpPos + 1, _paramStr.length);
        }
        else {
            _paramStr = "";
        }

        if (_paramStr.length > 0) {
            var _paramArr = _paramStr.split("&");
            if (_paramArr.length > 0) {
                var _paramKeyVal = _paramArr[0].split("=");
                if (_paramKeyVal.length > 0) {
                    MenuID = _paramKeyVal[1];
                }
            }
            /*
            if (_paramArr.length>0)
            {
            var _arr = new Array(_paramArr.length);
            }
  
            //取所有#后面的，菜单只需用到Menu
            //for (var i = 0; i < _paramArr.length; i++)
            {
            var _paramKeyVal = _paramArr[i].split('=');
   
            if (_paramKeyVal.length>0)
            {
            _arr[_paramKeyVal[0]] = _paramKeyVal[1];
            }  
            }
            */
        }

        if (MenuID != "") {
            DoMenu(MenuID)
        }
    }
    GetMenuID(); //*这两个function的顺序要注意一下，不然在Firefox里GetMenuID()不起效果
    MenuFix();
</script>
</html>
