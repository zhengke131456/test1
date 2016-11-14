<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="product.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title> 
      <link href="images/Default.css" type="text/css" rel="stylesheet">
    <link href="images/xtree.css" type="text/css" rel="stylesheet">
    <link href="images/User_Login.css" type="text/css" rel="stylesheet">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta  http-equiv="X-UA-Compatible" content="IE=9" > </meta >
    <meta content="MSHTML 6.00.6000.16674" name="GENERATOR">
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="Scripts/jslogin.js" type="text/javascript"></script> 
</head>



<script type="text/javascript">

    document.onkeyup = function (e) {
        if (e == null) {
            _key = event.keyCode;
        } else {
            _key = e.which;
        }

        if (_key == 13) {
            $("#LG").click();
        }
    }

   
</script>
<body id="userlogin_body">
    <div>
    </div>
    <div id="user_login">
        <dl>
            <dd id="user_top">
                <ul>
                    <li class="user_top_l"></li>
                    <li class="user_top_c"></li>
                    <li class="user_top_r"></li>
                </ul>
                <dd id="user_main">
                    <ul>
                        <li class="user_main_l"></li>
                        <li class="user_main_c">
                            <div class="user_main_box">
                                <ul>
                                    <li class="user_main_text">用户名： </li>
                                    <li class="user_main_input">
                                     <input  runat="server" class="TxtUserNameCssClass" id="username" maxlength="20" name="username">  
                                    
                                    </li>
                                   
                                </ul>
                                <ul>
                                    <li class="user_main_text">密 码： </li>
                                    <li class="user_main_input">
                                        <input class="TxtPasswordCssClass" id="password" type="password" name="password">
                                    </li>
                                </ul>

                                 <ul>
                                    <li ><input type="checkbox" id="chb"  runat="server" />记住用户名 </li>
                                 
                                </ul>
                            </div>
                              <div class="mima1"  >
                            
                              </div>
                            <div class="mima">
                                <span class="login"><a href="#">
                                    <img alt="" id="LG" src="../images/psd612736_r2_c2.jpg" onclick="javascript:check(); " /></a></span>
                                <span class="chongzhi"><a href="#">
                                    <img alt="" src="../images/psd612736_r2_c4.jpg" onclick="javascript:cz();" /></a></span>
                            </div>
                        </li>
                       <li class="user_main_r">
                          <span id="nmerror" style="color: Red; font-size: 12px ;  position: relative; left: -40px; top:-55px;"></span>
                          <span id="pderror" style="color: Red; font-size: 12px; position: relative; left: -40px; top:-20px;"></span>
                        </li>
                    </ul>
                    <dd id="user_bottom">
                        <ul>
                            <li class="user_bottom_l"></li>
                            <li class="user_bottom_c"></li>
                            <li class="user_bottom_r"></li>
                        </ul>
                    </dd>
        </dl>
    </div>
    <span id="ValrUserName" style="display: none; color: red"></span><span id="ValrPassword"
        style="display: none; color: red"></span><span id="ValrValidateCode" style="display: none;
            color: red"></span>
    <div id="ValidationSummary1" style="display: none; color: red">
    </div>
    <div>
    </div>
  </body>
</html>
