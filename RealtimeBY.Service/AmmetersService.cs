using RealtimeBY.Infrastructure.Configuration;
using SqlServerDataAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeBY.Service
{
    public class AmmetersService
    {
        /// <summary>
        /// 获取当前的电表电能与功率值
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCurrentValue(string organizationId)
        {
            DataTable result = new DataTable();

            string connectionString = ConnectionStringFactory.NXJCConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string managementDatabaseName = GetMeterDatabaseByOrganizationId.GetMeterDatabaseName(organizationId);
                SqlCommand command = connection.CreateCommand();
                string sqlStr = string.Format("SELECT * FROM [{0}].[dbo].[RealtimeAmmeter]", managementDatabaseName);
                //command.CommandText = "SELECT * FROM [zc_nxjc_byc_byf].[dbo].[RealtimeAmmeter]";
                command.CommandText =sqlStr;
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(result);
                }
            }

            return result;
        }
        /// <summary>
        /// 获得该分厂有哪些电气室
        /// </summary>
        /// <param name="organizationId">组织机构ID</param>
        /// <returns></returns>
        public static DataTable GetElectricRoom(string organizationId)
        {
            //电表数据库名
            string managementDatabaseName = GetMeterDatabaseByOrganizationId.GetMeterDatabaseName(organizationId);
            string connectionstring = ConnectionStringFactory.NXJCConnectionString;
            SqlServerDataFactory _dataFactory = new SqlServerDataFactory(connectionstring);
            string sqlStr= @"SELECT A.ElectricRoom
                                FROM [{0}].[dbo].AmmeterContrast AS A
                                WHERE A.EnabledFlag = 1
                                GROUP BY A.ElectricRoom
                                ORDER by A.ElectricRoom";
            return _dataFactory.Query(string.Format(sqlStr,managementDatabaseName));
        }
    }
}
