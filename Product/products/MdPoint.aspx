<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MdPoint.aspx.cs" Inherits="product.products.MdPoint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
     <title>修改坐标</title>

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.3" ></script>
        <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />


    
    
</head>
<body onload="loadMapArea()">
    <div class="subnav">
        地图标注 > <a >标点</a> > 修改</div>
    <div id="right">
        <div id="mapmask">
            <div class="mright">
                <h2>
                    添加修改标点</h2>
                <ul>
                    <li>ID：<%=id %>
                    </li>
                    <li>名 称 ：<%=name %></li>
                
                    <li class="orange">移动地图上标点位置，点击“保存”即可。</li>
                    <li>经 度 ：<input id="px" readonly="readonly" style="background-color:#F3F7F9;" name="px" value="<%=px %>" /></li>
                    <li>纬 度 ：<input id="py" readonly="readonly" style="background-color:#F3F7F9;" name="py" value="<%=py %>" /></li>
                </ul>
                <div class="clear">
                </div>
            </div>
            <div class="map" id="mapDiv" style="height: 380px; border: solid 1px #ccc;">
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="page" style="text-align: center;">
            <input type="button" value="保存标点" id="btnSave" />
            <input type="button" value="返回上一页" id="btnBack" />
        </div>
    </div>
</body>

<script type="text/javascript">
    var projmap = { table: "<%=table %>", id: "<%=id %>", px: "<%=px %>", py: "<%=py %>", ispoint: "<%=isPoint %>", address: "<%=name %>" }
</script>

<script type="text/javascript" src="../JS/Map/BaiduMapEdit.js"></script>

</html>

