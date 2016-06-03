<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HistoryAmmeterQuery.aspx.cs" Inherits="RealtimeBY.Web.UI_HistoryAmmeterQuery.HistoryAmmeterQuery" %>
<%@ Register Src="../UI_WebUserControls/OrganizationSelector/OrganisationTree.ascx" TagName="OrganisationTree" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="/lib/ealib/themes/gray/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/lib/ealib/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtIcon.css" />
    <link rel="stylesheet" type="text/css" href="/lib/extlib/themes/syExtCss.css" />

    <script type="text/javascript" src="/lib/ealib/jquery.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/jquery.easyui.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/easyui-lang-zh_CN.js" charset="utf-8"></script>

    <script type="text/javascript" src="/lib/ealib/extend/jquery.PrintArea.js" charset="utf-8"></script>
    <script type="text/javascript" src="/lib/ealib/extend/jquery.jqprint.js" charset="utf-8"></script>
    <script type="text/javascript" src="/js/common/PrintFile.js" charset="utf-8"></script>

    <script type="text/javascript" src="js/page/HistoryAmmeterQuery.js" charset="utf-8"></script>
</head>
<body>
    <div class="easyui-layout" data-options="fit:true,border:false" style="padding: 5px;">
        <div data-options="region:'west',border:false " style="width: 230px;">
            <uc1:OrganisationTree ID="OrganisationTree_ProductionLine" runat="server" />
        </div>
         <div data-options="region:'center',border:false">
            <div class="easyui-layout" data-options="fit:true,border:false">
            <div id="toolbar_EnergyConsumptionPlanInfo" data-options="region:'north'" style="height: 112px">
            <table>
                <tr>
                    <td>
                        
                        <table>
                            <tr>
                                <td>组织机构</td>
                                <td style="width: 80px;">
                                    <input id="TextBox_OrganizationText" class="easyui-textbox" data-options="editable:false, readonly:true" style="width: 80px;" />
                                </td>
                                 <td><input id="TextBox_OrganizationId" style="width: 2px; visibility: hidden;" /></td>
                                  <td>电力室</td>
                                <td style="width: 140px;">
                                    <input id="comb_EDRoom" class="easyui-combobox" style="width: 140px;"data-options="panelHeight:'auto'" />
                                </td>                           
                                <td>电表</td>
                                <td style="width: 150px;">
                                    <input id="comb_Emeter" class="easyui-combobox" style="width: 150px;"data-options="panelHeight:'auto'" />
                                </td>  
                            </tr>
                        </table>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                         <table>
                            <tr>
                                <td  class="queryDate" style="width:60px">开始时间：</td>
                                <td class="queryDate">
                                    <input id="startDate" type="text" class="easyui-datetimebox" required="required" style="width: 150px;" />
                                </td>     
                                <td class="queryDate">                            
                                    结束时间：<input id="endDate" type="text" class="easyui-datetimebox" required="required" style="width: 150px;" />
                                </td> 
                                <td style="width:20px"></td>  
                                <td class="queryDate"><a href="javascript:void(0);" class="easyui-linkbutton" data-options="iconCls:'icon-search',plain:true"
                                    onclick="QueryHistoryAmmeterData();">查询</a>
                                </td>  
                                </tr>
                           </table>
                        <td>
                 </tr>
                 <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                <a href="#" class="easyui-linkbutton" data-options="iconCls:'icon-reload',plain:true" onclick="RefreshFun();">刷新</a>
                                </td>  
                                <td><a href="#" class="easyui-linkbutton" data-options="iconCls:'ext-icon-page_white_excel',plain:true" onclick="ExportFileFun();">导出</a>
                            </td>
                            <td><a href="#" class="easyui-linkbutton" data-options="iconCls:'ext-icon-printer',plain:true,disabled:true" onclick="PrintFileFun();">打印</a>
                            </td>
                            <td>
                                <div class="datagrid-btn-separator"></div>
                            </td>                                  
                        </tr>
                    </table>           
                    </td>
                </tr>
            </table>
        </div>   
             <div data-options="region:'center',border:false">
                    <table id="table_HistoryAmmeterData" ></table>
            </div>
         </div>
    </div>
    </div>
   <%-- <form id="formMain" runat="server" target="_blank" method="post">
        <div>
            <asp:HiddenField ID="HiddenField_UserName" runat="server" />
        </div>
    </form>--%>
</body>
</html>
