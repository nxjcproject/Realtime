<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmmeterMonitor.aspx.cs" Inherits="RealtimeBY.Web.UI_RealtimeBYC_BYF.AmmeterMonitor" %>

<%@ Register Src="../UI_WebUserControls/OrganizationSelector/OrganisationTree.ascx" TagName="OrganisationTree" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>电表监控</title>
    <link rel="stylesheet" type="text/css" href="/lib/ealib/themes/gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/lib/ealib/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtIcon.css" />
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtCss.css" />

    <script type="text/javascript" src="/lib/ealib/jquery.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/jquery.easyui.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/easyui-lang-zh_CN.js" charset="utf-8"></script>
    <script type="text/javascript" src="/UI_RealtimeBYC_BYF/js/common/AmmeterMonitor.js" charset="utf-8"></script>
    <script type="text/javascript" src="/UI_RealtimeBYC_BYF/js/common/ammeterPresenter.js" charset="utf-8"></script>
</head>
<body id="containerId">
    <div id="cc" class="easyui-layout" data-options="fit:'true'">
        <div data-options="region:'west',split:true" style="width: 215px;">
            <%--<uc1:OrganisationTree ID="OrganisationTree1" runat="server" />--%>
            <ul id="OrganisationTree"></ul>
        </div>
        <div data-options="region:'center'">
            <%--<div class="easyui-layout" data-options="fit:'true'">
                <div data-options="region:'north'" style="background-color: rgb(250,250,250); height: 50px">
                    <table style="padding-left: 10px; padding-top: 10px">
                        <tr>
                            <td>生产线：</td>
                            <td>
                                <input id="productLineName" class="easyui-textbox" style="width: 180px;" readonly="true" /><input id="organizationId" readonly="true" style="display: none;" /></td>
                            <td>选择电气室：</td>
                            <td>
                                <input id="electricRoomId" class="easyui-combobox" data-options="panelHeight:'auto'" />
                            </td>
                            <td><a href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:'icon-search',plain:true"
                                onclick="QueryReportFun();">查询</a>
                            </td>
                        </tr>
                    </table>
                    </div>--%>
            <%--<div data-options="region:'center'" style="padding: 5px;">--%>
            <div id="wrapper" class="easyui-panel" data-options="fit:true" style="height: auto; padding: 2px; display: none">
            </div>
            <%--</div>--%>
            <%--</div>--%>
        </div>
        <a id="tooltip" href="javascript:void(0)"></a>
    </div>
</body>
</html>
