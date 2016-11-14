<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PCity.ascx.cs" Inherits="product.PCity" %>

 <script src="/ajax/jquery-1.8.3.min.js" type="text/javascript"></script>
 
 <script src="/ajax/jquery.cascadeselect.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function () {

                $.fn.CascadeSelect({
                url: '/ajax/pt.ashx',  //返回Json数据的一般处理文件
                idKey: 'Id',   // 绑定下拉框实际值的字段
                nameKey: 'Name', // 绑定下拉框显示值的字段
                casTopId: 99999,  // 顶级节点ParentId
                casCount: 2, // 级联个数
                casObjId: ['Select1', 'Select2'], // 级联下拉框id
            });
        });

    </script>

     省份：<select id="Select1" name="Select1"  class="select"></select>
     城市：<select id="Select2" name="Select2" class="select"></select>
     