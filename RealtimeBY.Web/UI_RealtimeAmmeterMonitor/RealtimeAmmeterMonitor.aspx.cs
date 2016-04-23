using RealtimeBY.Service;
using RealtimeBY.Service.RealtimeAmmeterMonitor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealtimeBY.Web.UI_RealtimeAmmeterMonitor
{
    public partial class RealtimeAmmeterMonitor : WebStyleBaseForEnergy.webStyleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.InitComponts();
            if (!IsPostBack)
            {
#if DEBUG
                ////////////////////调试用,自定义的数据授权
                List<string> m_DataValidIdItems = new List<string>() { "zc_nxjc_qtx_tys","zc_nxjc_qtx_efc" };
                AddDataValidIdGroup("ProductionOrganization", m_DataValidIdItems);
#elif RELEASE
#endif
            }
        }

        /// <summary>
        /// 获取电气室列表
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetOrganizationTree()
        {
            //获取数据授权
            List<string> oganizationIds = WebStyleBaseForEnergy.webStyleBase.GetDataValidIdGroup("ProductionOrganization");
            IList<string> levelCodes = WebUserControls.Service.OrganizationSelector.OrganisationTree.GetOrganisationLevelCodeById(oganizationIds);
            DataTable table = OrganizationTree.CreatOrganizationTree(levelCodes.ToArray());
            string[] otherColumns = { "OrganizationID", "LevelType", "state" };
            string json = EasyUIJsonParser.TreeJsonParser.DataTableToJsonByLevelCode(table, "LevelCode", "Name", otherColumns);
            return json;
        }

        [WebMethod]
        public static string GetRealtimeAmmeterData(string organizationId, string electricRoomName, string levelType)
        {
            DataTable table= RealtimeAmmeterMonitorService.GetRealtimeData(organizationId, electricRoomName, levelType);
            string json = EasyUIJsonParser.DataGridJsonParser.DataTableToJson(table);
            return json;
        }
    }
}