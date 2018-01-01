var g_electricRoom;
var g_organizationId;
var g_levelType;
var g_time;
var g_intervalTime = 300000;
$(document).ready(function () {
    initOrganizationTree();
    initDatagrid();
})

//初始化datagrid
function initDatagrid() {
    $('#AmmeterDatagrid').datagrid({
        frozenColumns: [[
    		//{ field: 'ElectricRoom', title: '电气室', width: 120 },
            { field: 'ElectricRoomName', title: '电气室', width: 120 },
            { field: 'AmmeterName', title: '表名称', width: 190, styler: cellStyler },
            ]],
       columns:[[
    		{ field: 'AmmeterNumber', title: '电表号', width: 45 },         
    		{ field: 'CT', title: 'CT', width: 40 },
            { field: 'PT', title: 'PT', width: 40 },
            { field: 'Current', title: 'I(平均)', width: 50 },
            { field: 'CurrentA', title: 'I(A)', width: 50 },
            { field: 'CurrentB', title: 'I(B)', width: 50 },
            { field: 'CurrentC', title: 'I(C)', width: 50 },
            { field: 'VoltageA', title: 'U(A)', width: 65 },
            { field: 'VoltageB', title: 'U(B)', width: 65 },
            { field: 'VoltageC', title: 'U(C)', width: 65 },
            { field: 'PF', title: 'cosΦ(平均)', width: 70 },
            { field: 'PFA', title: 'cosΦ(A)', width: 50 },
            { field: 'PFB', title: 'cosΦ(B)', width: 50 },
            { field: 'PFC', title: 'cosΦ(C)', width: 50 },
            { field: 'Energy', title: 'W', width: 100 },
            { field: 'Power', title: 'P', width: 60 },
            { field: 'Status', title: '状态', width: 60 },
            {
                field: 'TimeStatusChange', title: '故障开始时间', width: 130, formatter: function (value, row, index) {
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
        fit: true
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
                    g_organizationId = node.OrganizationID;
                    g_electricRoom = node.text;//电气室                   
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
    var win = $.messager.progress({
        title: '请稍后',
        msg: '数据载入中...'
    });
    $.ajax({
        type: "POST",
        url: "RealtimeAmmeterMonitor.aspx/GetRealtimeAmmeterData",
        data: '{organizationId: "' + g_organizationId + '",electricRoomName:"' + g_electricRoom + '",levelType:"' + g_levelType + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //async: false,
        success: function (msg) {
            $.messager.progress('close');
            var myData = jQuery.parseJSON(msg.d);

            //当双击查询某电气室时，隐藏电气室列
            if (g_levelType == "ElectricRoom") {
                $('#AmmeterDatagrid').datagrid('hideColumn', 'ElectricRoomName');
            }
            else {
                $('#AmmeterDatagrid').datagrid('showColumn', 'ElectricRoomName');
            }

            $('#AmmeterDatagrid').datagrid('loadData', myData);
            myMergeCell("AmmeterDatagrid", "ElectricRoomName");
            myTime();
        },
        beforeSend: function (XMLHttpRequest) {
            win;
        },
        error: function () {
            $.messager.progress('close');
            myTime();
        }
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