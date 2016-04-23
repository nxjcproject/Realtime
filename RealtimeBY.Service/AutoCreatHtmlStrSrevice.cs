using RealtimeBY.Infrastructure.Configuration;
using SqlServerDataAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RealtimeBY.Service
{
    public static class AutoCreatHtmlStrSrevice
    {
        //TODO:效率问题有待优化
        static public string GetHtml(string organizationId,string electricRoomName,string levelType)
        {
            if (levelType != "ElectricRoom")
            {
                DataTable electricRoomTable = AmmetersService.GetElectricRoom(organizationId);
                StringBuilder build = new StringBuilder();
                foreach (DataRow dr in electricRoomTable.Rows)
                {
                    string t_electricRoomName = dr["ElectricRoom"].ToString().Trim();
                    DataTable t_sourceTable = GetTableByElectricRoom(organizationId, t_electricRoomName);
                    build.Append(PanelHtmlStr(t_sourceTable, t_electricRoomName));
                }
                return build.ToString();
            }
            else
            {
                DataTable sourceTable = GetTableByElectricRoom(organizationId, electricRoomName);
                //return ToHtmlStrByTable(sourceTable,electricRoomName);
                return PanelHtmlStr(sourceTable, electricRoomName);
            }
        }
        /// <summary>
        /// 获取电表对照表的内容
        /// </summary>
        /// <param name="electricRoomName">电气室名</param>
        /// <returns></returns>
        static private DataTable GetTableByElectricRoom(string organizationId,string electricRoomName)
        {
            string managementDatabaseName = GetMeterDatabaseByOrganizationId.GetMeterDatabaseName(organizationId);
            string connectionstring = ConnectionStringFactory.NXJCConnectionString;
            SqlServerDataFactory _dataFactory = new SqlServerDataFactory(connectionstring);
            string sqlStr = @"SELECT A.AmmeterName,A.AmmeterNumber,A.CT,A.PT,A.AmmeterAddress,A.Status,A.TimeStatusChange
                                FROM [{0}].[dbo].AmmeterContrast AS A
                                WHERE A.ElectricRoom=@ElectricRoom
                                and A.EnabledFlag = 1"
                            ;
            
            SqlParameter paramater = new SqlParameter("ElectricRoom", electricRoomName);
            return _dataFactory.Query(string.Format(sqlStr, managementDatabaseName), paramater);
        }
        /// <summary>
        /// 增加easyui-panel的Html
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="electricRoom"></param>
        /// <returns></returns>
        static private string PanelHtmlStr(DataTable sourceTable, string electricRoom)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string panel = string.Format("<div title=\"{0}\" class=\"easyui-panel\"  style=\"height: auto; padding: 10px;\">",electricRoom);
            stringBuilder.Append(panel);
            stringBuilder.Append(ToHtmlStrByTable(sourceTable, electricRoom));
            stringBuilder.Append("</div>");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 转换为HTML（根据电气室的table）
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <returns></returns>
        static private string ToHtmlStrByTable(DataTable sourceTable, string electricRoom)
        {
            //整体
            StringBuilder mBaseBuilder = new StringBuilder();
            //string mbase="<div id=\"wrapper\" class=\"easyui-panel\" style=\"width:100%;height:auto;padding:2px;\">"+
            //                "<div class=\"easyui-panel\" title=\""+electricRoom+"\" style=\"height:auto;padding:10px;\">" ;
            //mBaseBuilder.Append(mbase);
            //电表名称
            StringBuilder ammeterName = new StringBuilder();
            //变比
            StringBuilder ratio = new StringBuilder();
            //电能
            StringBuilder energy = new StringBuilder();
            //功率
            StringBuilder power = new StringBuilder();
            
            int count = sourceTable.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                //为每行第一个表时添加起始标签
                DataRow  nowRow=sourceTable.Rows[i];
                if (i % 5 == 0)
                {
                    mBaseBuilder.Append("<div ><table>");
                    ammeterName.Append("<tr style=\"height:23px;\"><td style=\"width:60px;\">电表名称</td>");
                    ratio.Append("<tr><td>变比</td>");
                    energy.Append("<tr><td>电能</td>");
                    power.Append("<tr><td>功率</td>");

                }
                string ammeterStyle = "";
                if (nowRow["Status"].ToString().Trim() != "正常读取")
                {
                    ammeterStyle = " style=\"color:red\"";
                }
                ammeterName.Append("<td" + ammeterStyle + " class=\"ammeterName\" data-ammeterStatus=\"" + nowRow["Status"].ToString().Trim() + "\" data-ammeterNum=\"" +
                    nowRow["AmmeterNumber"].ToString().Trim() + "\" data-ammeterAddr=\"" + nowRow["AmmeterAddress"].ToString().Trim() + "\" data-timeStatusChange=\"" +
                    nowRow["TimeStatusChange"].ToString().Trim() + "\">" + nowRow["AmmeterName"].ToString().Trim() + "</td>");
                ratio.Append("<td><input type=\"text\" value=\"CT:"+nowRow["CT"].ToString().Trim()+" PT:"+nowRow["PT"].ToString().Trim()+"\" readonly=\"readonly\" /></td>");
                energy.Append("<td><input id=\""+nowRow["AmmeterNumber"].ToString().Trim()+"Energy\" type=\"text\" readonly=\"readonly\" /></td>");
                power.Append("<td><input id=\"" + nowRow["AmmeterNumber"].ToString().Trim() + "Power\" type=\"text\" readonly=\"readonly\" /></td>");
                //为每行的最后一个电表时添加结束标签
                if (i % 5 == 4||i==count-1)
                {                    
                    ammeterName.Append("</tr>");
                    ratio.Append("</tr>");
                    energy.Append("</tr>");
                    power.Append("</tr>");
                    mBaseBuilder.Append(ammeterName.ToString())
                                .Append(ratio.ToString())
                                .Append(energy.ToString())
                                .Append(power.ToString())
                                .Append("</table></div>");
                    ammeterName.Clear();
                    ratio.Clear();
                    energy.Clear();
                    power.Clear();
                }
            }
            //mBaseBuilder.Append("</div></div>");
            return mBaseBuilder.ToString();
        }
    }
}
