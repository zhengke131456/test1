<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RightTS.aspx.cs" Inherits="product.RightTS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
html,body,div,span,applet,object,iframe,h1,h2,h3,h4,h5,h6,p,blockquote,pre,a,abbr,acronym,address,big,cite,code,del,dfn,em,font,img,ins,kbd,q,s,samp,small,strike,strong,sub,sup,tt,var,dl,dt,dd,ol,ul,li,fieldset,form,label,legend,table,caption,tbody,tfoot,thead,tr,th,td,section, article, aside, header, footer, nav, dialog, figure,menu,hgroup,iframe{ margin:0; padding:0; border:0;  font-family:"Microsoft YaHei";}
.content{width:600px; heigh:400px; border:2px solid #d2dee5;background:#e7edf1;-webkit-border-radius:8px;-ms-border-radius:8px; border-radius:8px; margin:0 auto; overflow:hidden; margin-top:100px; padding-bottom:20px;}
.content h1{font_size:36px; font-weight:400; text-align:center; margin:20px auto; color:#003399;}
.content ul{display:block; margin:10px 20px; list-style:none; overflow:hidden; }
.content ul li{display:block; clear:both;}
.content ul li h3{font-size:14px; font-weight:normal; color:#516877; line-height:32px; float:left;}
.content ul li h3 span{ margin-right:20px;}
.content ul li a{display:block;float:right; margin-left:20px; color:#003399; font-size:14px;  text-decoration:none;}
.content ul li a:hover{color:#ff6600;}


</style>


</head>
<body>
    <form id="form1" runat="server">
<div class="content">
 	<h1>未操作提示</h1>
    <ul>
    	<li>
        	<%if (show1 == "1")
           { %><h3>您有 <strong><span style="color:Red;"><%=shows1 %>条</span></strong>未审核调拨单记录</h3><a href="products/Transferslip_T.aspx"><strong>请点击查看</strong></a><%} %>
        </li>
        <li>
        <%if (show2 == "1")
          { %>
        	<h3>您有<strong><span style="color:Red;"><%=shows2 %>条</span></strong>待出库调拨单记录</h3><a href="products/Transferslip.aspx?checked=待出库"><strong>请点击查看</strong></a><%} %>
        </li>
        <li>
         <%if (show3 == "1")
           { %>
        	<h3>您有<strong><span style="color:Red;"><%=shows3 %>条</span></strong>待入库调拨单记录</h3><a href="products/Transferslip.aspx?checked=待入库"><strong>请点击查看</strong></a><%} %>
        </li>

         <li>
         <%if (show4 == "1")
           { %>
        	<h3>您有<strong><span style="color:Red;"><%=shows4 %>条</span></strong>调拨异常记录</h3><a href="products/Transferslip_error.aspx"><strong>请点击查看</strong></a><%} %>
        </li>
    </ul>
 
 </div>

    </form>
</body>
</html>
