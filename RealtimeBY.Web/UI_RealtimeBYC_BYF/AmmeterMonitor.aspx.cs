using EasyUIJsonParser.Infrastructure;
using RealtimeBY.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealtimeBY.Web.UI_RealtimeBYC_BYF
{
    public partial class AmmeterMonitor : WebStyleBaseForEnergy.webStyleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.InitComponts();
            if (!IsPostBack)
            {
#if DEBUG
                ////////////////////调试用,自定义的数据授权
                List<string> m_DataValidIdItems = new List<string>() { "zc_nxjc_qtx_tys", "zc_nxjc_qtx_efc" };
                AddDataValidIdGroup("ProductionOrganization", m_DataValidIdItems);
#elif RELEASE
#endif
                //this.OrganisationTree1.Organizations = GetDataValidIdGroup("ProductionOrganization");                         //向web用户控件传递数据授权参数
                //this.OrganisationTree1.PageName = "AmmeterMonitor.aspx";                                     //向web用户控件传递当前调用的页面名称
                //this.OrganisationTree1.LeveDepth = 5;
                //this.OrganisationTree_ProductionLine.OrganizationTypeItems.Add("水泥磨");
                //this.OrganisationTree_ProductionLine.OrganizationTypeItems.Add("熟料");
                
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
            string[] otherColumns = { "OrganizationID", "LevelType","state" };
            string json = EasyUIJsonParser.TreeJsonParser.DataTableToJsonByLevelCode(table, "LevelCode", "Name", otherColumns);
            return json;
        }
        /// <summary>
        /// 获取电气室
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string GetElecRoomName(string organizationId)
        {
            DataTable roomTable = AmmetersService.GetElectricRoom(organizationId);
            string json = EasyUIJsonParser.ComboboxJsonParser.DataTableToJson(roomTable);
            return json;
        }
        [WebMethod]
        public static string CreatHtml(string organizationId,string electricRoomName,string levelType)
        {
            string htmlStr= AutoCreatHtmlStrSrevice.GetHtml(organizationId, electricRoomName,levelType);
            return htmlStr;
        }
        [WebMethod]
        public static string GetValues(string organizationId)
        {
            DataTable dt = AmmetersService.GetCurrentValue(organizationId);
            return JsonHelper.DataTableFirstRowToJson(dt);
        }
    }
}