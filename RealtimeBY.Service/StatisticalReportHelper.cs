using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace RealtimeBY.Service
{
    public class StatisticalReportHelper
    {
        private static readonly FileIO.XmlSerializerIO mXmlSerializerIO = new FileIO.XmlSerializerIO();
        public static string ReadReportHeaderFile(string myFilePath, DataTable myDataTable)
        {
            string m_ColumnsJsonData = "";
            if (File.Exists(myFilePath))
            {
                FormTableConvert.FormTemplate m_FormTableTemplate = (FormTableConvert.FormTemplate)mXmlSerializerIO.XmlSerializerFromFile(myFilePath, typeof(FormTableConvert.FormTemplate));
                //DataTable m_DataTable = GetDataTable();
                string m_ColumnsJson = FormTableConvert.TemplateToEasyUIGridJson.ToEasyUIGridJson(m_FormTableTemplate);
                //string m_DataTableJson = DataTypeConvert.DataTableConvertJson.DataTableToJson(myDataTable, "rows", myDataTable.Rows.Count, true);
                string m_DataTableJson = EasyUIJsonParser.DataGridJsonParser.GetDataRowJson(myDataTable);   //DataTypeConvert.DataTableConvertJson.DataTableToJson(myDataTable, "rows", myDataTable.Rows.Count, true);
                m_ColumnsJsonData = "{" + m_DataTableJson + "," + m_ColumnsJson + "}";
            }
            return m_ColumnsJsonData;
        }
        public static string CreatePrintHtmlTable(string myFilePath, DataTable myDataTable, string[] m_TagData)
        {
            FormTableConvert.FormTemplate m_FormTemplate = (FormTableConvert.FormTemplate)mXmlSerializerIO.XmlSerializerFromFile(myFilePath, typeof(FormTableConvert.FormTemplate));
            //string[] m_TagData = new string[] { "10月份", "报表类型:日报表", "汇总人:某某某", "审批人:某某某" };
            //DataTable m_DataTable = GetDataTable();
            if (m_FormTemplate != null && myDataTable != null)
            {
                string m_HtmlTable = FormTableConvert.TemplateToHtmlTable.ToHtmlDataHtmlTable(m_FormTemplate, m_TagData, myDataTable, FormTableConvert.ConvertType.Print);
                //string m_HtmlTable = FormTableConvert.TemplateToHtmlTable.ToHtmlSourceHtmlTable(m_FormTemplate);
                return m_HtmlTable;
            }
            else
            {
                return "";
            }
        }
        public static string CreateExportHtmlTable(string myFilePath, DataTable myDataTable, string[] m_TagData)
        {
            //FormTableConvert.FormTemplate m_FormTemplate = (FormTableConvert.FormTemplate)mXmlSerializerIO.XmlSerializerFromFile(myFilePath, typeof(FormTableConvert.FormTemplate));
            ////string[] m_TagData = new string[] { "10月份", "报表类型:日报表", "汇总人:某某某", "审批人:某某某" };
            ////DataTable m_DataTable = GetDataTable();
            //if (m_FormTemplate != null && myDataTable != null)
            //{
            //    string m_HtmlTable = FormTableConvert.TemplateToHtmlTable.ToHtmlDataHtmlTable(m_FormTemplate, m_TagData, myDataTable, FormTableConvert.ConvertType.Export);
            //    //string m_HtmlTable = FormTableConvert.TemplateToHtmlTable.ToHtmlSourceHtmlTable(m_FormTemplate);
            //    return m_HtmlTable;
            //}
            //else
            //{
            //    return "";
            //}
            int count=myDataTable.Rows.Count;
            if (count != 0)
            {
                StringBuilder strB = new StringBuilder();
                strB.Append("<table style = \"border-collapse:collapse;border:0px;margin:0px;padding:0px;table-layout:fixed;\">");               
                strB.Append("<tr><td style = \" height:20.24pt;  font-size:16pt; text-align:center; v-align:middle;\" colspan=\"14\"  >电表历史数据报表</td></tr><tr>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:99pt; height:14.24pt;  font-size:12pt; text-align:left; v-align:middle;\">时间</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:107.25pt; height:14.24pt; font-size:12pt; text-align:left; v-align:middle;\">电表名</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:43.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">电表号</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:33.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">CT</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:33.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">PT</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">I（平均）</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">I(A)</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">I(B)</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">I(C)</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">U(A)</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">U(B)</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">U(C)</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">cosΦ(平均)</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">cosΦ(A)</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">cosΦ(B)</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">cosΦ(C)</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">W</td>");
                strB.Append("<td style = \" border-top: solid 0.1pt black; border-left: solid 0.1pt black; border-right: solid 0.1pt black; border-bottom: solid 0.1pt black; backGround-color:#cccccc; width:63.75pt; height:14.24pt;  font-size:12pt; text-align:center; v-align:middle;\">P</td></tr>");
                for (int i = 0; i < count;i++ )
                {
                    strB.Append("<tr>");
                    for (int j = 0; j < 18; j++)
                    {
                        string strCell = myDataTable.Rows[i][j].ToString().Trim();
                        strB.Append("<td>" + strCell + "</td>");
                    }
                    strB.Append("</tr>");
                }
                strB.Append("</table>");
                string tableHtml = strB.ToString();

                return tableHtml;
            }
            else
            {
                return "";
            }
        }
        public static void ExportExcelFile(string myFileType, string myFileName, string myData)
        {
            if (myFileType == "xls")
            {
                UpDownLoadFiles.DownloadFile.ExportExcelFile(myFileName, myData);
            }
        }
    }
}
