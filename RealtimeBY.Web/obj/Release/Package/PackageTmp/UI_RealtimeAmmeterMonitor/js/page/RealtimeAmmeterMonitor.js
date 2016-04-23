
var g_electricRoom;
var g_organizationId;
var g_levelType;
var g_time;
var g_intervalTime = 300000;
var g_num = 0;
$(document).ready(function () {
    initOrganizationTree();
    //initTooltip();
    initDatagrid();
})

//初始化datagrid
function initDatagrid() {
    $('#AmmeterDatagrid').datagrid({
        columns: [[
    		{
    		    field: 'ElectricRoom', title: '电气室', width: 150, styler: function (value, row, index) {
    		        if (index < g_num - 1 &&　index != 0) {
    		            return 'background-color:#CCCCCC;border-top:1px solid #1F1F1F;';
    		        }
    		        else {
    		            return 'background-color:#CCCCCC;';
    		        }
    		    }
    		},
            { field: 'AmmeterName', title: '表名称', width: 150, styler: cellStyler },
    		{ field: 'AmmeterNumber', title: '电表号', width: 100 },         
    		{ field: 'CT', title: 'CT', width: 100 },
            { field: 'PT', title: 'PT', width: 100 },
            { field: 'Current', title: '平均电流', width: 100 },
            { field: 'CurrentA', title: 'A项电流', width: 100 },
            { field: 'CurrentB', title: 'B项电流', width: 100 },
            { field: 'CurrentC', title: 'C项电流', width: 100 },
            { field: 'VoltageA', title: 'A项电压', width: 100 },
            { field: 'VoltageB', title: 'B项电压', width: 100 },
            { field: 'VoltageC', title: 'C项电压', width: 100 },
            { field: 'PF', title: '平均功率因数', width: 100 },
            { field: 'PFA', title: 'A项功率因数', width: 100 },
            { field: 'PFB', title: 'B项功率因数', width: 100 },
            { field: 'PFC', title: 'C项功率因数', width: 100 },
            { field: 'Energy', title: '电能', width: 100 },
            { field: 'Power', title: '功率', width: 100 },
            { field: 'Status', title: '状态', width: 100 },
            {
                field: 'TimeStatusChange', title: '故障开始时间', width: 100, formatter: function (value, row, index) {
                    if (row["Status"] == "不能读取") {
                        return value;
                    }
                    else {
                        return '--';
                    }
                }
            }
        ]],
        striped: true,
        singleSelect: true,
        fit:true
    });
}

//初始化目录树
function initOrganizationTree() {
    $.ajax({
        type: "POST",
        url: "RealtimeAmmeterMonitor.aspx/GetOrganizationTree",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            var mData = JSON.parse(msg.d);
            $('#OrganisationTree').tree({
                data: mData,
                id: 'id',
                animate: true,
                lines: true,
                onDblClick: function (node) {
                    clearTimeout(g_time);
                    g_electricRoom = node.text;//电气室
                    g_organizationId = node.OrganizationID;
                    g_levelType = node.LevelType;
                    queryAmmeter();
                }
            });
        }
    });
}

function queryAmmeter() {
    if (g_levelType == "Company") {
        $.messager.alert("提示", "请选择分厂及分厂以下级别");
        return;
    }
    $.ajax({
        type: "POST",
        url: "RealtimeAmmeterMonitor.aspx/GetRealtimeAmmeterData",
        data: '{organizationId: "' + g_organizationId + '",electricRoomName:"' + g_electricRoom + '",levelType:"' + g_levelType + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (msg) {
            var myData = jQuery.parseJSON(msg.d);
            g_num = myData.total;

            $('#AmmeterDatagrid').datagrid('loadData', myData);
            myMergeCell("AmmeterDatagrid", "ElectricRoom");
            myTime();
        },
        error:myTime
    });
}

function myTime() {
    g_time=setTimeout(
                function () {
                    queryAmmeter()
                }, g_intervalTime);
}

//单元格样式控制
function cellStyler(value, row, index) {
    if (row["Status"]=="不能读取") {
        return 'background-color:#ffee00;color:red;';
    }
}