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
    public static class GetMeterDatabaseByOrganizationId
    {
        public static string GetMeterDatabaseName(string organizationId)
        {
            string connectionstring = ConnectionStringFactory.NXJCConnectionString;
            SqlServerDataFactory _dataFactory = new SqlServerDataFactory(connectionstring);
            string sqlStr = @"SELECT SO.OrganizationID,SD.ManagementDatabase,SD.MeterDatabase
                                FROM system_Organization as SO,system_Database AS SD
                                WHERE SO.DatabaseID=SD.DatabaseID AND
                                SO.OrganizationID=@OrganizationID";
            SqlParameter parameter = new SqlParameter("OrganizationID", organizationId);
            DataTable result = _dataFactory.Query(sqlStr, parameter);
            if (result.Rows.Count == 0)
            {
                throw new Exception("没有找到相关的电表数据库！");
            }
            else
            {
                return result.Rows[0]["MeterDatabase"].ToString().Trim();
            }
        }
    }
}
