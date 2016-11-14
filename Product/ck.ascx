<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ck.ascx.cs" Inherits="product.ck" %>
 
 <script src="/ajax/jquery-1.8.3.min.js" type="text/javascript"></script>
 <script src="/ajax/jquery.cascadeselect.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function () {
            

              $.fn.CascadeSelect({
                url: '/ajax/CascadeSelect.ashx',  //返回Json数据的一般处理文件
                idKey: 'Id',   // 绑定下拉框实际值的字段
                nameKey: 'Name', // 绑定下拉框显示值的字段
                casTopId: 99999,  // 顶级节点ParentId
                casCount: <%=show %>, // 级联个数
                casObjId: ['Select1', 'Select2', 'Select3'], // 级联下拉框id
                //casDefVal: [5, '益阳', 22], // 级联默认值(Id,Name都可以)
            });
        });
 

    </script>

     <strong> 选择仓库</strong>

            <select id="Select1" name="Select1"  class="select"></select>
            <select id="Select2" name="Select2" class="select"></select>
            <select id="Select3" name="Select3" class="select"></select>

