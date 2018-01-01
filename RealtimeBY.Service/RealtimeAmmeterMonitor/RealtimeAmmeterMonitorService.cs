using RealtimeBY.Infrastructure.Configuration;
using SqlServerDataAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RealtimeBY.Service.RealtimeAmmeterMonitor
{
    public class RealtimeAmmeterMonitorService
    {
        public static DataTable GetRealtimeData(string organizationId, string electricRoomName, string levelType)
        {
            string connectionString = ConnectionStringFactory.NXJCConnectionString;
            SqlServerDataFactory _dataFactory = new SqlServerDataFactory(connectionString);
            string meterDbName = GetMeterDatabaseByOrganizationId.GetMeterDatabaseName(organizationId);
            string sql = @"select A.AmmeterNumber, A.AmmeterName,A.ElectricRoom, A.CT, A.PT,A.Status,A.TimeStatusChange, A.AmmeterCode,B.ElectricRoomName,B.DisplayIndex
                            from [{0}].[dbo].AmmeterContrast A 
				       left join [{0}].[dbo].ElectricRoomContrast B on A.ElectricRoom=B.ElectricRoom {1}
                            order by B.DisplayIndex,A.AmmeterNumber";
            DataTable mainTable = new DataTable();
            if ("ElectricRoom" == levelType)
            {
                mainTable = _dataFactory.Query(string.Format(sql, meterDbName, "where A.EnabledFlag=1 and A.ElectricRoom=(select C.ElectricRoom from [" + meterDbName + "].[dbo].ElectricRoomContrast C where C.ElectricRoomName= '" + electricRoomName + "')"));
            }
            else
            {
                mainTable = _dataFactory.Query(string.Format(sql, meterDbName, "where A.EnabledFlag=1"));
            }

            string[] array = new string[] { "Current", "CurrentA", "CurrentB", "CurrentC", "PF", "PFA", "PFB", "PFC" ,
                                            "VoltageA","VoltageB","VoltageC"};
            List<DataTable> list = new List<DataTable>();
            Dictionary<string, DataTable> dictionary = new Dictionary<string, DataTable>();//存储电流电压实时值
            //增加电流电压等信息
            foreach (string item in array)
            {
                mainTable.Columns.Add(item, typeof(decimal));
                try
                {
                    dictionary.Add(item, _dataFactory.Query(string.Format("select * from [{0}].[dbo].Realtime_{1}", meterDbName,item)));
                }
                catch { };
            }
            //电能，功率特殊处理
            mainTable.Columns.Add("Energy", typeof(decimal));
            mainTable.Columns.Add("Power", typeof(decimal));
            dictionary.Add("EnergyPower", _dataFactory.Query(string.Format("select * from [{0}].[dbo].RealtimeAmmeter",meterDbName)));

            //将电流电压等信息添加到mainTable表中
            foreach (DataRow dr in mainTable.Rows)
            {
                string ammeterNum = dr["AmmeterNumber"].ToString().Trim();
                foreach (string key in dictionary.Keys.ToArray())
                {
                    if (dictionary[key].Columns.Contains(ammeterNum))
                    {
                        dr[key] = dictionary[key].Rows[0][ammeterNum];
                    }
                }
                //电能功率特殊处理
                if (dictionary["EnergyPower"].Columns.Contains(ammeterNum + "Energy"))//电能
                {
                    dr["Energy"] = dictionary["EnergyPower"].Rows[0][ammeterNum + "Energy"];
                }
                if (dictionary["EnergyPower"].Columns.Contains(ammeterNum + "Power"))//电压
                {
                    dr["Power"] = dictionary["EnergyPower"].Rows[0][ammeterNum + "Power"];
                }
            }
            

            return mainTable;
        }

       
    }
}
