using RealtimeBY.Service;
using RealtimeBY.Service.HistoryAmmeterQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealtimeBY.Web.UI_HistoryAmmeterQuery
{
    public partial class HistoryAmmeterQuery : WebStyleBaseForEnergy.webStyleBase
    {
        private const string REPORT_TEMPLATE_PATH = "\\ReportHeaderTemplate\\HistoryAmmeterReport.xml";
        private static DataTable myDataTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.InitComponts();
            ////////////////////调试用,自定义的数据授权
#if DEBUG
            List<string> m_DataValidIdItems = new List<string>() { "zc_nxjc_byc_byf", "zc_nxjc_qtx", "zc_nxjc_tsc_tsf", "zc_nxjc_ychc" };
            AddDataValidIdGroup("ProductionOrganization", m_DataValidIdItems);
            mPageOpPermission = "1111";
#elif RELEASE
#endif
            this.OrganisationTree_ProductionLine.Organizations = GetDataValidIdGroup("ProductionOrganization");                 //向web用户控件传递数据授权参数
            this.OrganisationTree_ProductionLine.PageName = "HistoryAmmeterQuery.aspx";                                     //向web用户控件传递当前调用的页面名称
            this.OrganisationTree_ProductionLine.LeveDepth = 5;
            if (!IsPostBack)
            {
              
            }
            //以下是接收js脚本中post过来的参数
            string m_FunctionName = Request.Form["myFunctionName"] == null ? "" : Request.Form["myFunctionName"].ToString();             //方法名称,调用后台不同的方法
            string m_Parameter1 = Request.Form["myParameter1"] == null ? "" : Request.Form["myParameter1"].ToString();                   //方法的参数名称1
            string m_Parameter2 = Request.Form["myParameter2"] == null ? "" : Request.Form["myParameter2"].ToString();                     //方法的参数名称2
            if (m_FunctionName == "ExcelStream")
            {
                //ExportFile("xls", "导出报表1.xls");
                //string[] m_TagData = new string[] { "10月份", "报表类型:历史表", "汇总人:某某某", "审批人:某某某" };
                string[] m_TagData = new string[] { };
                string m_HtmlData = StatisticalReportHelper.CreateExportHtmlTable(mFileRootPath +
                    REPORT_TEMPLATE_PATH, myDataTable, m_TagData);
                StatisticalReportHelper.ExportExcelFile("xls", "电表历史数据.xls", m_HtmlData);
            }
        }

        [WebMethod]
        public static string GetEDRoomListJson(string organizationId) 
        {
            DataTable table = HistoryAmmeterQueryService.GetEDRoomList(organizationId);
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
            return json;
        }
        [WebMethod]
        public static string GetEmeterListJson(string organizationId, string eRoomName)
        {
            DataTable table = HistoryAmmeterQueryService.GetEmeterList(organizationId, eRoomName);
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
            return json;
       //     return null;
        }
        [WebMethod]
        public static string GetAmeterDataJson(string organizationId, string meterNumber, string startTime,string endTime) 
        {
            myDataTable = HistoryAmmeterQueryService.GetAmeterDataTable(organizationId, meterNumber, startTime, endTime);
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(myDataTable);
            return json;
        }
    }
}