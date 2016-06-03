var organizationId = '';

$(document).ready(function () {
    initOrganizationTree();
    //initTooltip();
    
})

function initOrganizationTree() {
    $.ajax({
        type: "POST",
        url: "AmmeterMonitor.aspx/GetOrganizationTree",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async:false,
        success: function (msg) {
            var mData = JSON.parse(msg.d);
            $('#OrganisationTree').tree({
                data: mData,
                id: 'id',
                animate: true,
                lines: true,
                onDblClick: function (node) {
                    queryAmmeter(node);
                }
                //valueField: 'ElectricRoom',
                //textField: 'ElectricRoom'
            });
        }
    });
}

function queryAmmeter(node) {
    var electricRoom = node.text;//电气室
    var organizationId = node.OrganizationID;
    var levelType = node.LevelType;
    if (levelType == "Company") {
        $.messager.alert("提示","请选择分厂及分厂以下级别");
        return;
    }
    $.ajax({
        type: "POST",
        url: "AmmeterMonitor.aspx/CreatHtml",
        data: '{organizationId: "' + organizationId + '",electricRoomName:"' + electricRoom + '",levelType:"'+levelType+'"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async:false,
        success: function (msg) {
            var mHtml = msg.d;
            //$('#myContainerId').empty();
            //$('#myContainerId').append(mHtml);
            if (mHtml != '' && mHtml != undefined) {
                $('#wrapper').css('display', 'block');
                //$('#myContainerId').html(mHtml);
                //$('#myContainerId').panel('setTitle', electricRoom);
                $('#wrapper').html(mHtml);
                $.parser.parse('#wrapper');//EasyUI Parser 解析器
                getLatestData(organizationId);
            }
        }
    });
    bindEvent();
}

function onOrganisationTreeClick(node) {
    $('#productLineName').textbox('setText', node.text);
    $('#organizationId').val(node.OrganizationId);
    organizationId = $('#organizationId').val();
    initCombobox(organizationId);
    $('#electricRoomId').combobox('setValue', '');
    QueryReportFun();
}

function initCombobox(organizationId) {
    $.ajax({
        type: "POST",
        url: "AmmeterMonitor.aspx/GetElecRoomName",
        data: '{organizationId: "' + organizationId + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var mData = JSON.parse(msg.d);
            $('#electricRoomId').combobox({
                data: mData,
                valueField: 'ElectricRoom',
                textField: 'ElectricRoom'
            });
        }
    });
}

function QueryReportFun() {
    var electricRoom = $('#electricRoomId').combobox('getValue');
    //var electricRoom=$('#electricRoomId').val();
    $.ajax({
        type: "POST",
        url: "AmmeterMonitor.aspx/CreatHtml",
        data: '{organizationId: "' + organizationId + '",electricRoomName:"' + electricRoom + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var mHtml = msg.d;
            //$('#myContainerId').empty();
            //$('#myContainerId').append(mHtml);
            if (mHtml != ''&&mHtml!=undefined) {
                $('#wrapper').css('display', 'block');
                //$('#myContainerId').html(mHtml);
                //$('#myContainerId').panel('setTitle', electricRoom);
                $('#wrapper').html(mHtml);
                $.parser.parse('#wrapper');//EasyUI Parser 解析器
                getLatestData(organizationId);
            }
        }
    });
    setTimeout('bindEvent()', 2000);

}
//事件绑定
function bindEvent() {
    
    $('.ammeterName').mouseenter(function (e) {       
        var ammeterNum = $(this).attr('data-ammeterNum');
        var ammeterAddr = $(this).attr('data-ammeterAddr');
        var ammeterStatus = $(this).attr('data-ammeterStatus');
        var timeStatusChange = $(this).attr('data-timeStatusChange');
        //alert("ammeterNum:" + ammeterNum + ",ammeterAddr:" + ammeterAddr);
        var myContent = "编号:" + ammeterNum + ",表地址:" + ammeterAddr + ",状态:" + ammeterStatus;
        if (ammeterStatus != '正常读取') {
            myContent = myContent + "(故障开始时间:" + timeStatusChange+")";
        }
        $(this).tooltip({
            //deltaX: e.pageX,
            //deltaY: e.pageY,
            position:'top',
            onShow: function () {
                $(this).tooltip('tip').css({
                    backgroundColor: '#C4C4C4',
                    borderColor: '#C4C4C4'
                })},
            content:myContent
        });
        $(this).tooltip('show');
    })
    $('.ammeterName').mouseleave(function (e) {
        $(this).tooltip('hide');
    })
}

//function initTooltip() {
//    $('#tooltip').tooltip({
//        position: 'right',
//        onShow: function () {
//            $(this).tooltip('tip').css({
//                backgroundColor: '#C4C4C4',
//                borderColor: '#C4C4C4'
//            });
//        }
//    });
//}