<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="product._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理系统</title>
</head>
 

        <frameset border=0 framespacing=0 rows="60, *" frameborder=0>
        <frame name=head src="../html/TopNew.aspx" frameborder=0 noresize scrolling=no>
            <frameset cols="170, *">
                <frame id="leftFrame" name=leftFrame src="../html/LeftNew.aspx" frameborder=0 noresize />
                <frame id="rightFrame" name=rightFrame src="../rightts.aspx" frameborder=0 noresize scrolling=yes />
            </frameset>
    </frameset>

	<%--<frameset cols="200,*" frameborder="no" border="0" framespacing="0">

   <frame src="../html/LeftNew.aspx" name="leftFrame"  id="leftFrame" />

    <frame src="" name="rightFrame" scrolling="auto" id="rightFrame" />
    </frameset>     
 --%>
</html>
