$(function () {
    InitDate();
    loadDataGrid("first");
});
//初始化日期框
function InitDate() {
    var nowDate = new Date();
    var beforeDate = new Date();
    beforeDate.setDate(nowDate.getDate());
    var nowString = nowDate.getFullYear() + '-' + (nowDate.getMonth() + 1) + '-' + nowDate.getDate() + " " + nowDate.getHours() + ":" + nowDate.getMinutes() + ":" + nowDate.getSeconds();
    var beforeString = beforeDate.getFullYear() + '-' + (beforeDate.getMonth() + 1) + '-' + beforeDate.getDate() + " 00:00:00";
    $('#startDate').datetimebox('setValue', beforeString);
    $('#endDate').datetimebox('setValue', nowString);
}
var organizationID = "";
function onOrganisationTreeClick(node) {
    organizationID = node.OrganizationId;
    var level = organizationID.split("_").length;
    if (level <= 3) {
        $.messager.alert("提示", "请选择分厂级别！");
    } else {
        $('#TextBox_OrganizationText').textbox('setText', node.text);

        $('#TextBox_OrganizationId').val(organizationID);

        $('#comb_EDRoom').combobox('setValue', "");
        $('#comb_EDRoom').combobox('setText', "");
        $('#comb_Emeter').combobox('setValue', "");
        $('#comb_Emeter').combobox('setText', "");

        LoadEDRoomList(organizationID);

    }
}
function LoadEDRoomList(OrganizationId) {
    $.ajax({
        type: "POST",
        url: "HistoryAmmeterQuery.aspx/GetEDRoomListJson",
        data: '{organizationId: "' + OrganizationId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
           var m_MsgData = jQuery.parseJSON(msg.d);
            LoadEDRoomComboboxData(m_MsgData);
           
        }
    });
}
function LoadEDRoomComboboxData(myData) {
    $('#comb_EDRoom').combobox({
        data: myData.rows,
        valueField: 'ElectricRoom',
        textField: 'ElectricRoom',
        panelHeight:400,
        onSelect: function (param) {
            var ElectricRoom = param.ElectricRoom;
            $('#comb_EDRoom').combobox('setText', ElectricRoom);

           $('#comb_Emeter').combobox('setValue', "");
           $('#comb_Emeter').combobox('setText', "");
           LoadEmeterList(ElectricRoom);
        }
    });
}
function LoadEmeterList(ERoomName) {
    $.ajax({
        type: "POST",
        url: "HistoryAmmeterQuery.aspx/GetEmeterListJson",
        data: '{organizationId: "' + organizationID + '",eRoomName:"' + ERoomName + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
           var m_MsgData = jQuery.parseJSON(msg.d);
           LoadEmeterComboboxData(m_MsgData);
        }
    });
}
var ammeterName = "";
var ammeterNumber = "";
function LoadEmeterComboboxData(myData) {
    $('#comb_Emeter').combobox({
        data: myData.rows,
        valueField: 'AmmeterNumber',
        textField: 'AmmeterName',
        panelHeight:400,
        onSelect: function (param) {
            ammeterName = param.AmmeterName;
            ammeterNumber = param.AmmeterNumber;
        }
    });
}
function QueryHistoryAmmeterData() {
   var startTime=  $('#startDate').datetimebox('getValue');
   var endTime = $('#endDate').datetimebox('getValue');
   var win = $.messager.progress({
       title: '请稍后',
       msg: '数据载入中...'
   });
    $.ajax({
        type: "POST",
        url: "HistoryAmmeterQuery.aspx/GetAmeterDataJson",
        data: '{organizationId: "' + organizationID + '",meterNumber:"' + ammeterNumber + '",startTime:"' + startTime + '",endTime:"' + endTime + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $.messager.progress('close');
            var m_MsgData = jQuery.parseJSON(msg.d);
            loadDataGrid("last",m_MsgData);
        },
        beforeSend: function (XMLHttpRequest) {
            win;
        }
    });

}
function loadDataGrid(type, myData) {
    if (type == "first") {
        $('#table_HistoryAmmeterData').datagrid({
            frozenColumns: [[
                    { field: 'vDate', title: '时间', width: 130 },
                    { field: 'AmmeterName', title: '电表名', width: 140 }
                     ]],
            columns: [[                  
                    
                    { field: 'AmmeterNumber', title: '电表号', width: 45 },
                    { field: 'CT', title: 'CT', width: 40 },
                    { field: 'PT', title: 'PT', width: 40 },
                    { field: 'Current', title: 'I(平均)', width: 60 },
                    { field: 'CurrentA', title: 'I(A)', width: 60 },
                    { field: 'CurrentB', title: 'I(B)', width: 60 },
                    { field: 'CurrentC', title: 'I(C)', width: 60 },
                    { field: 'VoltageA', title: 'U(A)', width: 65 },
                    { field: 'VoltageB', title: 'U(B)', width: 65 },
                    { field: 'VoltageC', title: 'U(C)', width: 65 },
                    { field: 'PF', title: 'cosΦ(平均)', width: 80 },
                    { field: 'PFA', title: 'cosΦ(A)', width: 60 },
                    { field: 'PFB', title: 'cosΦ(B)', width: 60 },
                    { field: 'PFC', title: 'cosΦ(C)', width: 60 },
                    { field: 'Energy', title: 'W', width: 100 },
                    { field: 'Power', title: 'P', width: 60 }
            ]],
            fit: true,
            //toolbar: "#toolbar_ReportTemplate",
            rownumbers: true,
            singleSelect: true,
            striped: true,
            data: []
            //,
            //pagination: true
        });
    }
    else {
        $('#table_HistoryAmmeterData').datagrid('loadData', myData);
    }
}
function RefreshFun() {
    QueryHistoryAmmeterData();
}


function ExportFileFun() {
    var m_FunctionName = "ExcelStream";
    var m_Parameter1 = "Parameter1";
    var m_Parameter2 = "Parameter2";

    var form = $("<form id = 'ExportFile'>");   //定义一个form表单
    form.attr('style', 'display:none');   //在form表单中添加查询参数
    form.attr('target', '');
    form.attr('method', 'post');
    form.attr('action', "HistoryAmmeterQuery.aspx");

    var input_Method = $('<input>');
    input_Method.attr('type', 'hidden');
    input_Method.attr('name', 'myFunctionName');
    input_Method.attr('value', m_FunctionName);
    var input_Data1 = $('<input>');
    input_Data1.attr('type', 'hidden');
    input_Data1.attr('name', 'myParameter1');
    input_Data1.attr('value', m_Parameter1);
    var input_Data2 = $('<input>');
    input_Data2.attr('type', 'hidden');
    input_Data2.attr('name', 'myParameter2');
    input_Data2.attr('value', m_Parameter2);

    $('body').append(form);  //将表单放置在web中 
    form.append(input_Method);   //将查询参数控件提交到表单上
    form.append(input_Data1);   //将查询参数控件提交到表单上
    form.append(input_Data2);   //将查询参数控件提交到表单上
    form.submit();
    //释放生成的资源
    form.remove();

    /*
    var m_Parmaters = { "myFunctionName": m_FunctionName, "myParameter1": m_Parameter1, "myParameter2": m_Parameter2 };
    $.ajax({
        type: "POST",
        url: "Report_Example.aspx",
        data: m_Parmater,                       //'myFunctionName=' + m_FunctionName + '&myParameter1=' + m_Parameter1 + '&myParameter2=' + m_Parameter2,
        //contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "1") {
                alert("导出成功!");
            }
            else{
                alert(msg.d);
            }
        }
    });
    */
}