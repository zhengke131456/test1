<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestProvinceCity.aspx.cs" Inherits="product.TestProvinceCity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="ajax/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="ajax/jquery.cascadeselect.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function () {

                $.fn.CascadeSelect({
                url: 'ajax/pt.ashx',  //返回Json数据的一般处理文件
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


     <h2> 市区联动</h2>
    <div class="city" >
            省份：<select id="Select1" class="select"></select>
            城市：<select id="Select2" class="select"></select>
    </div> 
    
    
    
    </div>
    
    
    </form>
</body>
</html>

