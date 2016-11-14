

$(function ($) {
    cas_settings = {};
    $.fn.CascadeSelect = function (options) {
        
        var cas_setId = options['casObjId'][0];
        cas_settings[cas_setId] = 
        {
            url: 'xxoo.ashx',  //返回Json数据的一般处理文件
            idKey: 'id',
            nameKey: 'name',
            casTopId: 99999 ,    // 顶级节点ParentId
            //casCount: "<%=show %>",   // 级联个数
            casCount:'<%=show %>', 
            casObjId: [],  // 级联下拉框id
            casDefVal: [], // 级联默认值 
        };
        $.extend(cas_settings[cas_setId], options); 
        // ----------------->   
    }

    // 加载级联数据
    function LoadCasData(curLevel, cas_setId, ParentID) {
        if (curLevel > cas_settings[cas_setId].casCount) return; // curCount 清零？

        var cid = cas_settings[cas_setId].casObjId[curLevel - 1]; // 下拉框id
        var cdefval = cas_settings[cas_setId].casDefVal[curLevel - 1]; //默认值
        var idKey = cas_settings[cas_setId].idKey;
        var nameKey = cas_settings[cas_setId].nameKey;
        var CPID = 0;  //市级所需要的父级ID

        var curObj = $('#' + cid); // 当前下拉框
        curObj.empty(); //清空下拉框 
        if (ParentID == undefined) {
            LoadCasData(curLevel + 1, cas_setId);
            return;
        } 

        $.ajax({
            url: cas_settings[cas_setId].url, type: 'post', data: { Level: curLevel, ParentID: ParentID }, dataType: 'json', async: false
                , success: function (data) {
                    if (data.length == 0) {
                        //LoadCasData(curLevel + 1, cas_setId); // 加载子项数据
                        return;
                    }

                    CPID = data[0][idKey];   //如果没有传入参数，系统默认父级第一个ID
                    for (var i = 0; i < data.length; i++) {
                        if (data[i][idKey] == cdefval || data[i][nameKey] == cdefval ) {
                            CPID = data[i][idKey];  // 选中默认值
                            curObj.append(" <option value='" + data[i][idKey] + "' selected='selected'>" + data[i][nameKey] + "</option>");
                        } else {
                            curObj.append(" <option value='" + data[i][idKey] + "'>" + data[i][nameKey] + "</option>");
                        }
                    }

                     
                       
                    //LoadCasData(curLevel + 1, cas_setId, CPID); // 加载子项数据
                    return;
                }
            , error: function () {
                console.log('加载数据出错！');
            }
            });

    }

    // 设置下拉框级别
    function SetCasLevel(cas_setId) {
        for (var i = 0; i < cas_settings[cas_setId].casCount; i++) {
            var cid = cas_settings[cas_setId].casObjId[i]; // 下拉框id 
            $('#' + cid).data('level', i + 1); // 当前下拉框级别
            $('#' + cid).data('setid', cas_setId); // 配置信息Id

            //下拉框改变
            $('#' + cid).change(function () {
                var CPID = $(this).val();
                if(CPID!="")
                {
                LoadCasData($(this).data('level') + 1, $(this).data('setid'),CPID);
                }
            });
        }
    }

    // 页面加载
    $(function () {
        for (var cas_setId in cas_settings) {
            console.log(cas_setId);
            SetCasLevel(cas_setId);
            LoadCasData(1, cas_setId, cas_settings[cas_setId].casTopId);
        }
    });

});