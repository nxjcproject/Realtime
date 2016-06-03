var dataProviderUrl = "AmmeterMonitor.aspx/GetValues";
var pollingIntervals = 300000;
var pollforupdates = true;
var pollingTimer;
var mOrganizationId = '';
//$(document).ready(function () {
//    // 文档加载完毕后开始获取最新数据
//    getLatestData();
//});

function getLatestData(organizationId) {
    // 发送 asynchronous POST 请求获取最新数据
    mOrganizationId = organizationId;
    dto = '';
    varType = "POST";
    varContentType = "application/json; charset=utf-8";
    varDataType = "json";
    varData = JSON.stringify(dto);

    $.ajaxSetup({ cache: false });

    $.ajax({
        type: varType,                  // GET or POST or PUT or DELETE verb
        url: dataProviderUrl,           // location of the service
        data: '{organizationId:"' + organizationId + '"}',
        //data: varData,                  // data sent to server
        contentType: varContentType,    // content type sent to server
        dataType: varDataType,          // expected data format from server
        success: serviceSuccessful,     // on successfull service call 
        // the serviceSuccessful method
        // error: serviceFailed            // on unsuccessfull service call 
        // the serviceFailed method
        error: setupTimerToPollLatestData
    });
}

function serviceSuccessful(resultObject) {
    if (pollforupdates == true) {
        displayValue($.parseJSON(resultObject.d));
        setupTimerToPollLatestData();
    }
}

function setupTimerToPollLatestData() {
    // 设置获取最新数据定时器
    pollingTimer = setTimeout(
        function () {
            getLatestData(mOrganizationId);
        }, pollingIntervals);
}

// 显示数据项
function displayValue(json) {
    for (var pair in json) {
        //电能保留整数
        if (pair.indexOf("Energy") > 0) {
            $("#" + pair).val(parseInt(json[pair]));
        }
        //功率保留两位小数
        if (pair.indexOf("Power") > 0) {
            $("#" + pair).val(parseFloat(json[pair]).toFixed(2));
        }
    }
}