<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Orderallocation.aspx.cs"
    Inherits="product.Order.Orderallocation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查询反馈分配</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=IE8,chrome=1" />

    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
         <link href="../Styles/dict.css" rel="stylesheet" type="text/css" />
    <link href="../JS/Grid/codebase/dhtmlxgrid.css" rel="stylesheet" type="text/css" />
    <%--  <script src="../JS/Grid/codebase/dhtmlxcommon.js"></script>--%>
    <script src="../JS/Grid/codebase/dhtmlxgrid.js" type="text/javascript"></script>
    <script>

	  
     var data =<%=resultstr%>;
	    var myGrid; myDataProcessor;
	    function doOnLoad() {
	        myGrid = new dhtmlXGridObject('gridbox');
	        myGrid.setImagePath("../JS/Grid/codebase/imgs/");
	        myGrid.setColumnIds(<%=setHeaderIds%>);
	            myGrid.setHeader(<%=setHeader%>);
		    myGrid.attachHeader(<%=attachHeader%>);
	         myGrid.setInitWidths("10,10,10,85,98,50,50,*");
            // myGrid.setInitWidths("*");
	        myGrid.setColAlign("center,center,center,center,center,left,left,left,left,left,left");

	        myGrid.getCombo(5).put(2, 2);
	        myGrid.setColTypes(<%=setColTypes%>);
	        myGrid.enableEditEvents(false, true, true);
	        myGrid.attachEvent("onEditCell", doOnCellEdit);
	         myGrid.setColSorting(<%=setColSorting%>);
	        myGrid.setColumnMinWidth(50, 0);
             myGrid.setStyle("background:#15A0F5; font-weight:bold;", "", "color:red;", "");

	        myGrid.init();
            myGrid.setColumnHidden(0,true);//颜色标记
             myGrid.setColumnHidden(1,true);//用来记录操作的那个客户
              myGrid.setColumnHidden(2,true);// 用来记录下单人
	        //myGrid.enableMultiselect(true);
	        myGrid.parse(data, function () {
          
	          for (var i = 1; i <= myGrid.getRowsNum(); i++) {
	                var colour = myGrid.cells(i, 0).getValue();
                   
	                if (colour =="red") {
	                    myGrid.setRowColor(i, "#FFD2D2");
	                }
                    else
                    {
                    myGrid.setRowColor(i, "#E0EDF5");
                    }
	            }
	          
	        },"json");
	        // -------------------------------  



	        myDataProcessor = new dataProcessor("../Handler/UpdataRow.ashx"); // lock feed url
	        myDataProcessor.enableDataNames(true);
             myDataProcessor.setTransactionMode("POST", false);
            // myDataProcessor.setUpdateMode("off"); 
	        myDataProcessor.init(myGrid); // link dataprocessor to the grid

	        //回掉函数
	          myDataProcessor.defineAction("error", function (tag) {
	              alert(tag.firstChild.nodeValue);
                  var rowid=tag.getAttribute("sid");
                  var val=document.getElementById("hiddenid").value;
                  var hiddencol=document.getElementById("hiddencol").value;
                  myGrid.cells(rowid, hiddencol).setValue(val)
	             return true;
	           
	         });
	         myDataProcessor.defineAction("update", function (tag) {
              //alert(tag.firstChild.nodeValue);
                var rowid=tag.getAttribute("sid");
                var colour=tag.firstChild.nodeValue;
                
                myGrid.setRowColor(rowid, colour);
                alert("修改成功！");
//	            var  colour=tag.firstChild.nodeValue;
//                var rowid=tag.getAttribute("ids");
//                var colour = myGrid.cells(rowid, 0).getValue();
//                   alert(colour);
//	                if (colour =="red") {
//	                    myGrid.setRowColor(rowid, "red");
//	                }
//               
	             return true;
	         });
	      


	    }

	  
	    function doOnCellEdit(stage, row, cell, newValue, oldValue) {
	         
             
	        if (stage == 1) {
	            var old = myGrid.cells(row, cell).getValue();
	            var ss = myGrid.getColumnLabel(cell - 1) + ',';
	           var name=ss+old;
	           myGrid.cells(row, 1).setValue(name)
             document.getElementById("hiddencol").value=cell;
	        }
	        if (stage == 2) {
	            if (newValue != oldValue) {
//                  myDataProcessor.setUpdated(row,true);
//            document.getElementById("some_name").onclick();  
             document.getElementById("hiddenid").value=oldValue;
     
             
	             myDataProcessor.sendData();
    
	            }
                else
                {
                grid1.cells(row,cell).setValue(oldValue);
                }
	         }
             return true;
	        
	    }

    </script>
</head>
<body onload="doOnLoad()">
    <form id="form1" runat="server">
    <div class="subnav">
        分配管理
    </div>
    <div id="right">
     <div class="page ">
            <p class="left">

            <p class="right">
                  <a style="color: Red;" href="../Order/batchmanagement.aspx">返回</a> &nbsp;</p>
            <div class="clear">
            </div>
            <div class="page ">
            
            </div>
        </div>
       
        <div id="gridbox" style="width: 100%; height: 500px; background-color: white;">
        </div>
         <div class="Pages">
            <div class="center">
            
            <asp:Button runat="server" ID="btnQuerey" Text="全部提交" OnClick="btnQuerey_Click" />
              &nbsp;&nbsp; &nbsp;&nbsp;  &nbsp;&nbsp; &nbsp;&nbsp;
            </div>
        </div>
            
    </div>
     <input type="hidden" id="hiddenid" value="" />
     <input type="hidden" id="hiddencol" value="" />
    </form>
   
   
  
</body>
</html>
