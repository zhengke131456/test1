//地图纠错
var map;
var oldMarker, oldPoint;
var newPx = "", newPy = "";
var newPoint, newMarker, polyline;
var Marker, Point;
var imgSrc = "/sjy/images/admini/";
//function $(id) {
//    return document.getElementById(id);
//}
function loadMapArea() {

    Point = new BMap.Point(projmap.px, projmap.py);

    map = new BMap.Map("mapDiv");                        // 创建Map实例
    map.centerAndZoom(Point, 11);                        // 初始化地图,设置中心点坐标和地图级别
    map.addControl(new BMap.NavigationControl());               // 添加平移缩放控件
    map.addControl(new BMap.ScaleControl());                    // 添加比例尺控件
    map.addControl(new BMap.OverviewMapControl());              //添加缩略地图控件
    map.enableScrollWheelZoom();
    map.setMinZoom(7);
    map.setMaxZoom(19);

    initMap();
}
function initMap() {

    Marker = new BMap.Marker(Point);
    map.addOverlay(Marker);
    Marker.enableDragging(true); // 设置标注可拖拽
        
    mapClick();
}
function mapClick() {


    Marker.addEventListener("dragend", function (e) {
        $("#px").value = newPx= e.point.lng;
        $("#py").value = newPy = e.point.lat;
    });


}

function createNewMarker(point, drag, icon) {
    var opts = { clickable: true, draggable: drag, icon: icon };
    var marker = new GMarker(point, opts);
    if (drag) {
        GEvent.addListener(marker, "dragend", function (point) {
            if (point) {
                newPx = point.lng();
                newPy = point.lat();
            }
            if (polyline) {
                map.removeOverlay(polyline);
            }
            if (projmap.ispoint == "1") {
                newMarker.setImage(imgSrc + "marker_red.png");
                polyline = new GPolyline([point, oldPoint], "#ff0000", 6);
                map.addOverlay(polyline);
            }
        });
    }
    return marker;
}
function createNewIcon() {
    var icon = new GIcon();
    if (map.iconType == "old") {
        icon.image = imgSrc + "marker_blue.png";
    }
    else {
        icon.image = imgSrc + "marker_red.png";
    }
    icon.shadow = imgSrc + "shadow_gray.png";
    icon.iconSize = new GSize(21, 35);
    icon.shadowSize = new GSize(37, 35);
    icon.iconAnchor = new GPoint(10, 35);
    icon.infoWindowAnchor = new GPoint(0, 0);
    return icon;
}
//$(document).ready(function(){
//  $("button").click(function(){
//    $("p").slideToggle();
//  });

if ($("#btnSave")) {
    
    $("#btnSave").click(function () {
        if (newPx == "" || newPy == "") { return; }

        $.post("../handler/uppoint.ashx?px=" + newPx + "&py=" + newPy + "&table=" + projmap.table + "&id=" + projmap.id, {}, function (data) {
            if (data.result == "1") {
                projmap.ispoint = "1";
                oldPoint = new BMap.Point(newPx, newPy);
                map.setCenter(oldPoint, 13);
                alert("标点修改成功！！");
            }
            else
            { alert("标点修改失败，请稍候重试！"); }
        }, "json");
        
        });
}

$("#btnBack").click(function () { history.back(); });

//$("#btnBack").onclick = function () {
//    back();
//}
