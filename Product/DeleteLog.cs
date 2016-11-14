using System;
using System.Collections.Generic;
using System.Linq;
using product.Common;
using System.Net;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.Services;


namespace product
{
    public class DeleteLog : Common.BasePage
    {
        /// <summary>
        /// IP地址
        /// </summary>
        private static string IP { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        private static string UserName { get; set; }
        /// <summary>
        /// 操作类型    0：入库  1：出库  2：调拨  3:油站信息  4：基础信息
        /// </summary>
        private static string type { get; set; }
        /// <summary>
        /// 尝试删除的数据（json格式）
        /// </summary>
        private static string Data { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        private static string TableName { get; set; }
        /// <summary>
        /// 添加删除日志
        /// </summary>
        /// <param name="sql"></param>
        public static void InsertLog(string sql, int i)
        {
            IP = Ip;
            UserName = Username;
            GetData(sql, i);
            string insertstr = string.Format("insert into deletelog values('{0}','{1}','{2}','{3}','{4}','{5}')", IP, TableName, UserName, type, Data,TableName);
            productcommon.DataMgr.ExcuteCommand(insertstr);

        }
        private static void GetData(string sql, int i)
        {
            sql.Replace('\'', ' ').Replace('"', ' ').Replace('{', ' ').Replace('}', ' ').Replace('<', ' ').Replace('>', ' ').Trim();
            if (i == 0)
            {
                type = "入库";
                TableName = "inproduct";
                string str = string.Format("select * from dbo.inproduct where id='{0}'", sql);
                Data = GetJsonByDataset(productcommon.DataMgr.GetDataSet(str));
            }
            if (i == 1)
            {
                type = "出库";
                TableName = "outproduct";
                string str = string.Format("select * from dbo.outproduct where id='{0}'", sql);
                Data = GetJsonByDataset(productcommon.DataMgr.GetDataSet(str));
            }
            if (i == 2)
            {
                type = "调拨";
                TableName = "transferslip";
                string str = string.Format("select * from dbo.transferslip where id='{0}'", sql);
                Data = GetJsonByDataset(productcommon.DataMgr.GetDataSet(str));
            }
            if (i == 3)
            {
                type = "油站信息";
                TableName = "BaseStore";
                string str = string.Format("select * from dbo.BaseStore where id='{0}'", sql);
                Data = GetJsonByDataset(productcommon.DataMgr.GetDataSet(str));
            }
            if (i == 4)
            {
                type = "基础信息";
                TableName = "baseinfo";
                string str = string.Format("select * from dbo.baseinfo where id='{0}'", sql);
                Data = GetJsonByDataset(productcommon.DataMgr.GetDataSet(str));
            }
        }
        /// <summary>
        /// 把dataset数据转换成json的格式
        /// </summary>
        /// <param name="ds">dataset数据集</param>
        /// <returns>json格式的字符串</returns>
        public static string GetJsonByDataset(DataSet ds)
        {
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                //如果查询到的数据为空则返回标记ok:false
                return "{\"ok\":false}";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"ok\":true,");
            foreach (DataTable dt in ds.Tables)
            {
                dt.TableName = TableName;
                sb.Append(string.Format("\"{0}\":[", dt.TableName));

                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{");
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\",", dr.Table.Columns[i].ColumnName.Replace("\"", "\\\"").Replace("\'", "\\\'"), ObjToStr(dr[i]).Replace("\"", "\\\"").Replace("\'", "\\\'")).Replace(Convert.ToString((char)13), "\\r\\n").Replace(Convert.ToString((char)10), "\\r\\n");
                    }
                    sb.Remove(sb.ToString().LastIndexOf(','), 1);
                    sb.Append("},");
                }

                sb.Remove(sb.ToString().LastIndexOf(','), 1);
                sb.Append("],");
            }
            sb.Remove(sb.ToString().LastIndexOf(','), 1);
            sb.Append("}");
            return sb.ToString();
        }
        /// <summary>
        /// 将object转换成为string
        /// </summary>
        /// <param name="ob">obj对象</param>
        /// <returns></returns>
        public static string ObjToStr(object ob)
        {
            if (ob == null)
            {
                return string.Empty;
            }
            else
                return ob.ToString();
        }
    }
}