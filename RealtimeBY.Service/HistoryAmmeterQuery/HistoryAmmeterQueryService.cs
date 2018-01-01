using RealtimeBY.Infrastructure.Configuration;
using SqlServerDataAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RealtimeBY.Service.HistoryAmmeterQuery
{
    public class HistoryAmmeterQueryService
    {
        public static DataTable GetEDRoomList(string organizationId)
        {

            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            SqlServerDataFactory _dataFactory = new SqlServerDataFactory(connectionString);
            string meterDbName = GetMeterDatabaseByOrganizationId.GetMeterDatabaseName(organizationId);
            string sql = @"select A.ElectricRoom AS ElectricRoom, B.ElectricRoomName AS ElectricRoomName
                             from (SELECT DISTINCT ElectricRoom FROM [{0}].[dbo].[AmmeterContrast] {1}) A
                        left join [{0}].[dbo].[ElectricRoomContrast] B on A.ElectricRoom=B.ElectricRoom 
		                          order by B.DisplayIndex";
            DataTable EDRoomListTable = new DataTable();
            EDRoomListTable = _dataFactory.Query(string.Format(sql, meterDbName, "where EnabledFlag=1 "));
            return EDRoomListTable;
         }
        public static DataTable GetEmeterList(string organizationId, string eRoomName) 
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            SqlServerDataFactory _dataFactory = new SqlServerDataFactory(connectionString);
            string meterDbName = GetMeterDatabaseByOrganizationId.GetMeterDatabaseName(organizationId);
            string sql = @" select AmmeterName,AmmeterNumber from [{0}].[dbo].[AmmeterContrast] {1}                           
                            order by AmmeterNumber";
            DataTable EmeterListTable = new DataTable();
            EmeterListTable = _dataFactory.Query(string.Format(sql, meterDbName, "where EnabledFlag=1 and ElectricRoom='" + eRoomName + "'"));
            return EmeterListTable;
        }
        public static DataTable GetAmeterDataTable(string organizationId, string meterNumber, string startTime, string endTime) 
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            SqlServerDataFactory _dataFactory = new SqlServerDataFactory(connectionString);
            string meterDbName = GetMeterDatabaseByOrganizationId.GetMeterDatabaseName(organizationId);
            string sql = @" USE [{0}]                       
                        select  B.vDate,A.AmmeterName,A.AmmeterNumber,A.CT,A.PT, C.{1} as [Current],CA.{1} as [CurrentA],
                        CB.{1} as [CurrentB],CC.{1} as [CurrentC],
                        VA.{1} as [VoltageA], VB.{1} as [VoltageB], VC.{1} as [VoltageC],
                        PF.{1} as [PF],PFA.{1} as [PFA],PFB.{1} as [PFB],PFC.{1} as [PFC],
                        B.{1}Energy as Energy,B.{1}Power as Power                      
                        from [dbo].[AmmeterContrast] A,[dbo].[HistoryAmmeter] B, 
                        [dbo].[History_Current] C,[dbo].[History_CurrentA] CA,[dbo].[History_CurrentB] CB,[dbo].[History_CurrentC] CC,
                        [dbo].[History_PF]  PF,[dbo].[History_PF]  PFA,[dbo].[History_PF] PFB,[dbo].[History_PF] PFC,
                        [dbo].[History_VoltageA] VA,[dbo].[History_VoltageB] VB,[dbo].[History_VoltageC] VC
                        where A.EnabledFlag=1 and A.AmmeterNumber='{1}' 
                        and B.vDate>=@startTime and B.vDate<=@endTime
                        and B.vDate=C.vDate and B.vDate=CA.vDate and B.vDate=CB.vDate and B.vDate=CC.vDate
                        and B.vDate=PF.vDate and B.vDate=PFA.vDate and B.vDate=PFB.vDate and B.vDate=PFC.vDate
                        and B.vDate=VA.vDate and B.vDate=VB.vDate and B.vDate=VC.vDate
                        order by vDate ";
            DataTable EmeterListTable = new DataTable();
            string mySql = string.Format(sql, meterDbName, meterNumber);
            SqlParameter[] parameter = { new SqlParameter("@startTime", startTime), new SqlParameter("@endTime", endTime) };
            EmeterListTable = _dataFactory.Query(mySql, parameter);
            return EmeterListTable;
        }       
    }
}
