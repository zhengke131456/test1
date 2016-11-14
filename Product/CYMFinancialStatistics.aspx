<%@ Page Title="" Language="C#" MasterPageFile="~/CheYiMaHome_MB.Master" AutoEventWireup="true" CodeBehind="CYMFinancialStatistics.aspx.cs" Inherits="CheYiMaAdmin.CYMFinancialStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.easyui1.3.6.min.js"></script>
    <script src="js/easyui-lang-zh_CN.js"></script>
    <script src="js/datagrid-defaultview.js" type="text/javascript"></script>
    <script src="JavaScript/datagrid-detailview.js" type="text/javascript"></script>
    <script src="JavaScript/iTsai-webtools-min.js"></script>
    <script src="CheYiMaAdminJavaScript/CYMFinancialStatisticsJavaScript.js"></script>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div style="display: none">
        <button type="button" onclick="DCExecl()">导出execl</button>
    </div>
    
    <div>
        普通搜索：<input id="ss" class="easyui-searchbox"
            searcher="SearchFinancialStatisticsOrderListInfo"
            prompt="请输入要搜索的内容" menu="#Searchmm" style="width: 300px"></input>
        <div id="Searchmm" style="width: 120px">
            <div name="CYMUsersPhoneNumber" <%--iconcls="icon-ok"--%>>手机号</div>
            <div name="CYMCarNumber" <%--iconcls="icon-ok"--%>>车牌号</div>
            <div name="NewCYMUsersFabricOrderNumber">订单号</div>
        </div>
        <div>
            选择店铺：<select name="CYMMerchantId" id="CYMMerchantId">
                <option value="0">------全部------</option>
                <option value="24914">车姨妈普通洗车</option>
                <option value="24913">线上自主购买会员卡</option>
                <option value="24839">车姨妈沿海赛洛城店</option>
                <option value="24863">坤锽（北京）移动科技有限公司</option>
            </select>

            
            <div id="FabricOrderNames" runat="server"></div>


            支付时间：<input class="easyui-datebox" id='StarPayTime' style="width: 200px" />到<input class="easyui-datebox" id='EndPayTime' style="width: 200px" />
            有无交易额：<select name="IsHaveMoney" id="IsHaveMoney">
                <option value="0">全部</option>
                <option value="1">有</option>
                <option value="2">无</option>
            </select>
            有无库存消耗：<select name="IsInventoryConsumption" id="IsInventoryConsumption">
                <option value="0">全部</option>
                <option value="1">有</option>
                <option value="2">无</option>
            </select>


            <button type="button" onclick="GetFinancialStatisticsByPayTime()" id="GetFinancialStatisticsByPayTime">查询</button>
        </div>
    </div>
    <div>
        <table id="FinancialStatistics"></table>
    </div>
    <div id="mm" class="easyui-menu" style="width: 120px;">
        <div style="font-size: 14px" data-options="iconCls:'icon-tip'">改变订单状态</div>
        <div class="menu-sep"></div>
        <div onclick="EditFinanceStatusTo1()" data-options="iconCls:'icon-ok'">已核单</div>
        <div onclick="EditFinanceStatusTo2()" data-options="iconCls:'icon-cancel'">错误单</div>
        <div onclick="EditFinanceStatusTo3()" data-options="iconCls:'icon-cut'">无效单</div>
        <div onclick="EditFinanceStatusTo0()" data-options="iconCls:'icon-remove'">未核单</div>
        <div class="menu-sep"></div>
        <div style="font-size: 14px" data-options="iconCls:'icon-tip'">更新信息</div>
        <div class="menu-sep"></div>
        <div onclick="EditFinanceRemark()" data-options="iconCls:'icon-ok'">更新财务备注</div>
        <div onclick="EditServerRemark()" data-options="iconCls:'icon-ok'">更新服务备注</div>
        <div onclick="EditCarMileage()" data-options="iconCls:'icon-ok'">更新车辆里程</div>
    </div>

    <div id="FinanceRemarkDiv">
        <textarea id="FinanceRemark" style="width: 400px; height: 200px"></textarea>
    </div>
    <div id="ServerRemarkDiv">
        <textarea id="ServerRemark" style="width: 400px; height: 200px"></textarea>
    </div>
    <div id="CarMileageDiv">
        <input type="number" id="CarMileage" value="0" min="0" />
    </div>




</asp:Content>
