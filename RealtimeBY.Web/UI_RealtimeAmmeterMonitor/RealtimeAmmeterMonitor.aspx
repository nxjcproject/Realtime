<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RealtimeAmmeterMonitor.aspx.cs" Inherits="RealtimeBY.Web.UI_RealtimeAmmeterMonitor.RealtimeAmmeterMonitor" %>
<%@ Register Src="../UI_WebUserControls/OrganizationSelector/OrganisationTree.ascx" TagName="OrganisationTree" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>电表实时监控</title>
    <link rel="stylesheet" type="text/css" href="/lib/ealib/themes/gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/lib/ealib/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtIcon.css" />
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtCss.css" />

    <script type="text/javascript" src="/lib/ealib/jquery.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/jquery.easyui.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/easyui-lang-zh_CN.js" charset="utf-8"></script>
    <script type="text/javascript" src="/UI_RealtimeAmmeterMonitor/js/page/RealtimeAmmeterMonitor.js" charset="utf-8"></script>
    <script type="text/javascript" src="/UI_RealtimeAmmeterMonitor/js/common/MergeCell.js" charset="utf-8"></script>
</head>
<body>
    <div id="cc" class="easyui-layout" data-options="fit:'true'">
        <div data-options="region:'west',split:true" style="width: 215px;">
            <div id="OrganisationTree"></div>
        </div>
        <div data-options="region:'center'">      
            <table id="AmmeterDatagrid"></table>      
        </div>
    </div>
</body>
</html>
