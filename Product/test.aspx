<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="product.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="ajax/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="ajax/jquery.cascadeselect.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function () {
            
//            $.fn.CascadeSelect({
//                url: 'ajax/CascadeSelect.ashx',  //返回Json数据的一般处理文件
//                idKey: 'Id',     // 绑定下拉框实际值的字段
//                nameKey: 'Name', // 绑定下拉框显示值的字段
//                casTopId: 0,  // 顶级节点ParentId
//                casCount: 4,  // 级联个数
//                casObjId: ['SelProvince', 'SelCity', 'SelArea', 'SelXian'], // 级联下拉框id
//                casDefVal: ['全国', 5, '益阳', 22], // 级联默认值(Id,Name都可以)
//            });

//              $.fn.CascadeSelect({
//                url: 'ajax/CascadeSelect.ashx',  //返回Json数据的一般处理文件
//                idKey: 'Id',   // 绑定下拉框实际值的字段
//                nameKey: 'Name', // 绑定下拉框显示值的字段
//                casTopId: 99999,  // 顶级节点ParentId
//                casCount: <%=show %>, // 级联个数
//                casObjId: ['Select1', 'Select2', 'Select3'], // 级联下拉框id
//                //casDefVal: [5, '益阳', 22], // 级联默认值(Id,Name都可以)
            //            });

                $.fn.CascadeSelect({
                url: 'ajax/CascadeSelect.ashx',  //返回Json数据的一般处理文件
                idKey: 'Id',   // 绑定下拉框实际值的字段
                nameKey: 'Name', // 绑定下拉框显示值的字段
                casTopId: 99999,  // 顶级节点ParentId
                casCount: 2, // 级联个数
                casObjId: ['Select1', 'Select2'], // 级联下拉框id
                //casDefVal: [5, '益阳', 22], // 级联默认值(Id,Name都可以)
            });

        });
 

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   
   
   <h2> 省级联动</h2>
<%--    <div class="city" >
            <select id="SelProvince" class="select"></select>
            <select id="SelCity" class="select"></select>
            <select id="SelArea" class="select"></select>
            <select id="SelXian" class="select"></select>
    </div> --%>

     <h2> 市区联动</h2>
    <div class="city" >
            <select id="Select1" class="select"></select>
            <select id="Select2" class="select"></select>
    </div> 
    
    
    
    </div>
    
    
    </form>
</body>
</html>
