using RealtimeBY.Infrastructure.Configuration;
using SqlServerDataAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RealtimeBY.Service
{
    public static class OrganizationTree
    {
        public static DataTable CreatOrganizationTree(string[] levelCodes)
        {

            string connectionstring = ConnectionStringFactory.NXJCConnectionString;
            SqlServerDataFactory dataFactory = new SqlServerDataFactory(connectionstring);
            //首先获得所有的授权的分厂数据库名
            string meterDbSql = @"select A.LevelCode,A.Name,B.MeterDatabase
                                        from system_Organization A,system_Database B
                                        where A.DataBaseID=B.DataBaseID
                                        and ({0})
                                        and A.LevelType='Factory'";
            StringBuilder levelCodeBuild = new StringBuilder();
            foreach (string code in levelCodes)
            {
                levelCodeBuild.Append("LevelCode like '");
                levelCodeBuild.Append(code);
                levelCodeBuild.Append("%' or ");
            }
            levelCodeBuild.Remove(levelCodeBuild.Length-4,4);
            DataTable meterDb = dataFactory.Query(string.Format(meterDbSql,levelCodeBuild.ToString()));
            string myStr = @"union
                                select (A.LevelCode+'01') as LevelCode,A.OrganizationID,REPLACE(B.ElectricRoom, CHAR(13) + CHAR(10), '<br>') ElectricRoom,('ElectricRoom') as LevelType,'open' as state
                                from system_Organization A
                                left join
                                [{0}].[dbo].AmmeterContrast B
                                on A.OrganizationID=B.OrganizationID
                                where A.LevelType='Factory'
                                and A.LevelCode like '{1}%'  --数据授权使用
                                and B.ElectricRoom is not null
                                and B.ElectricRoom<>''
                                group by LevelCode,A.OrganizationID,B.ElectricRoom,LevelType

                                ";
            StringBuilder childQuery = new StringBuilder();
            StringBuilder criteria = new StringBuilder();
            foreach (DataRow dr in meterDb.Rows)
            {
                childQuery.Append(string.Format(myStr,dr["MeterDatabase"].ToString().Trim(),dr["LevelCode"].ToString().Trim()));
                criteria.Append(string.Format("CHARINDEX(C.LevelCode,'{0}')>0 or CHARINDEX('{0}',C.LevelCode)>0",dr["LevelCode"].ToString().Trim()));
                criteria.Append(" or ");
            }
            criteria.Remove(criteria.Length - 4, 4);
            string mySql = @"select C.LevelCode,C.OrganizationID,C.Name,C.LevelType,(case when C.LevelType='Factory' then 'closed' else 'open' end) as state
                                from system_Organization C
                                where (C.LevelType='Company'
                                or C.LevelType='Factory')
                                and ({0})
                                {1}
                                order by LevelCode";
            DataTable result=dataFactory.Query(string.Format(mySql,criteria.ToString(),childQuery.ToString()));
            int i = 0;
            //处理levelCode
            foreach (DataRow dr in result.Rows)
            {
                if (dr["LevelType"].ToString().Trim() == "Company" || dr["LevelType"].ToString().Trim() == "Factory")
                {
                    i = 0;
                }
                else
                {
                    string t_levelCode=dr["LevelCode"].ToString().Trim();
                    dr["LevelCode"] = t_levelCode.Substring(0, t_levelCode.Length - 2)+i.ToString("00");
                    i++;
                }
            }
            return result;
        }
    }
}
