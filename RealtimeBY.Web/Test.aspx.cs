using RealtimeBY.Service;
using SqlServerDataAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RealtimeBY.Web
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            OrganizationTree.CreatOrganizationTree(new string[]{"O03","O02"});

            //string test = AutoCreatHtmlStrSrevice.GetHtml("","");
            //string endTime = DateTime.Parse("2014-01-23").AddDays(1).ToString();
            
            //ISqlServerDataFactory _dataFactory = new SqlServerDataFactory("Data Source=QH-20140815HAUR;Initial Catalog=Db_02_01;Integrated Security=True");
            //string sql = @"SELECT * FROM Db_02_01.[dbo].[HistoyFormulaValue] WHERE vDate>='2015/1/13 0:00:00' AND vDate<'2015/1/14 0:00:00'";
            ////SqlParameter parameters = new SqlParameter("factoryOrganizationID", factoryOrganizationID);
            //DataTable table= _dataFactory.Query(sql);

            //DataRow[] rows = table.Select("vDate>#2015-01-13 15:00:00# AND vDate<#2015-01-13 16:00:00#");
            ////DataRow[] rows1 = table.Select("vDate>#2015-01-13 15:00# AND vDate<#2015-01-13 16:00");
            //object temp = table.Compute("Sum(FormulaValue)", "vDate>#2015-01-13 15:00:00# AND vDate<#2015-01-13 16:00:00#");
        }
    }
}