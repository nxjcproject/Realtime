<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="RealtimeBY.Web.Test" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <div id="wrapper" class="easyui-panel" style="width: 100%; height: auto; padding: 2px;">
        <div class="easyui-panel" title="水泥磨配电室" style="width: 100%; height: auto; padding: 10px;">
            <div style="width: 980px;">
                <table>
                    <tr style="height: 23px;">
                        <td style="width: 60px;">电表名称</td>
                        <td>水泥磨1号空压机电源</td>
                        <td>水泥磨2号空压机电源</td>
                        <td>水泥磨3号空压机电源</td>
                        <td>水泥磨4号空压机电源</td>
                        <td>熟料入库皮带</td>
                    </tr>
                    <tr>
                        <td>变比</td>
                        <td>
                            <input type="text" value="CT:100 PT:1" readonly="readonly" /></td>
                        <td>
                            <input type="text" value="CT:100 PT:1" readonly="readonly" /></td>
                        <td>
                            <input type="text" value="CT:100 PT:1" readonly="readonly" /></td>
                        <td>
                            <input type="text" value="CT:100 PT:1" readonly="readonly" /></td>
                        <td>
                            <input type="text" value="CT:60 PT:1" readonly="readonly" /></td>
                    </tr>
                    <tr>
                        <td>电能</td>
                        <td>
                            <input id="A090Energy" type="text" readonly="readonly" /></td>
                        <td>
                            <input id="A091Energy" type="text" readonly="readonly" /></td>
                        <td>
                            <input id="A092Energy" type="text" readonly="readonly" /></td>
                        <td>
                            <input id="A093Energy" type="text" readonly="readonly" /></td>
                        <td>
                            <input id="A094Energy" type="text" readonly="readonly" /></td>
                    </tr>
                    <tr>
                        <td>功率</td>
                        <td>
                            <input id="A090Power" type="text" readonly="readonly" /></td>
                        <td>
                            <input id="A091Power" type="text" readonly="readonly" /></td>
                        <td>
                            <input id="A092Power" type="text" readonly="readonly" /></td>
                        <td>
                            <input id="A093Power" type="text" readonly="readonly" /></td>
                        <td>
                            <input id="A094Power" type="text" readonly="readonly" /></td>
                    </tr>
                </table>
            </div>
            <div style="width: 980px;">
                <table>
                    <tr style="height: 23px;">
                        <td style="width: 60px;">电表名称</td>
                        <td>熟料内返收尘风机</td>
                        <td>1#水泥磨喂料称（新加）</td>
                        <td>2#水泥磨喂料称（新加）</td>
                    </tr>
                    <tr>
                        <td>变比</td>
                        <td>
                            <input type="text" value="CT:40 PT:1" readonly="readonly" /></td>
                        <td>
                            <input type="text" value="CT:40 PT:1" readonly="readonly" /></td>
                        <td>
                            <input type="text" value="CT:30 PT:1" readonly="readonly" /></td>
                    </tr>
                    <tr>
                        <td>电能</td>
                        <td>
                            <input id="A096Energy" type="text" readonly="readonly" /></td>
                        <td>
                            <input id="A097Energy" type="text" readonly="readonly" /></td>
                        <td>
                            <input id="A098Energy" type="text" readonly="readonly" /></td>
                    </tr>
                    <tr>
                        <td>功率</td>
                        <td>
                            <input id="A096Power" type="text" readonly="readonly" /></td>
                        <td>
                            <input id="A097Power" type="text" readonly="readonly" /></td>
                        <td>
                            <input id="A098Power" type="text" readonly="readonly" /></td>
                    </tr>
                </table>
            </div>


        </div>
    </div>

</body>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" ProcessingMode="Remote"></rsweb:ReportViewer>
</html>
