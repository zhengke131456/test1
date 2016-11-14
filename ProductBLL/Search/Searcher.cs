using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
namespace ProductBLL.Search
{

    public class Searcher
    {
        //protected static readonly string FoldTimeS = ConfigurationSettings.AppSettings["FoldTime"].ToString();
        //protected static readonly string FoldTimeZ = ConfigurationSettings.AppSettings["FoldTimeZ"].ToString();
        #region 根权限来显示数据20150424
        /// <summary>
      /// 根据搜索实体类，拼接分页语句，包含条件语句
        /// </summary>
        /// <param name="sh"></param>
        /// <returns></returns>
        public static string QXGetbaselistsql(SJYEntity.Common.Search sh) {

			string orderby = " order by id desc ";

			if (sh.Orderby != "") {
				orderby = " order by " + sh.Orderby;
			}

			string sqldt;

			int tempstar = (sh.PageIndex - 1) * sh.PageSize;
			int tempend = sh.PageIndex * sh.PageSize + 1;

			sqldt = "select count(*) from " + sh.Sqlstr;
			if (sh.PageSize == 0) {
                sqldt += "select * from " + sh.Sqlstr +  " "+ orderby;
			}
			{
				sqldt += "; select top " + sh.PageSize + "  * from (select  * ,row_number() over(" + orderby + ") as rn from " + sh.Sqlstr + ")as aa where (rn > " + tempstar + " and rn< " + tempend + ")";
			}
			return sqldt;
		}

        /// <summary>
        /// </summary>
        /// <param name="searchinfo"></param>
        /// <returns></returns>
       
        #endregion 



        #region 基础数据
        /// <summary>
        /// 根据搜索实体类，拼接分页语句，包含条件语句
        /// </summary>
        /// <param name="curpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="sinfo"></param>
        /// <returns></returns>
        public static string Getbaselistsql(SJYEntity.Common.Search sh)
        {
            string whereStr = GetbaselistWhere(sh);
            string orderby = "order by id desc ";

            if (sh.Orderby != "")
            {
                orderby = "order by " + sh.Orderby;
            }

            string sqldt;
            int tempstar = (sh.PageIndex - 1) * sh.PageSize;
            int tempend = sh.PageIndex * sh.PageSize + 1;
            sqldt = "select count(*) from " + sh.Tablename + " where" + whereStr;
            sqldt += "; select top " + sh.PageSize + "  * from (select  * ,row_number() over(" + orderby + ") as rn from " + sh.Tablename + " where " + whereStr + " )as aa where (rn > " + tempstar + " and rn< " + tempend + ")";

            return sqldt;
        }

        /// <summary>
        /// </summary>
        /// <param name="searchinfo"></param>
        /// <returns></returns>
        public static string GetbaselistWhere(SJYEntity.Common.Search sh)
        {

            string wherestr = "";
            wherestr += " 1=1  and ";

            if (sh.Protype != "")
            {
                wherestr += "type='" + sh.Protype + "' and ISNULL(storetype,'')!='SINOPEC'   and ";

            }
            if (sh.Basename != "")
                wherestr += "Basename like '%" + sh.Basename + "%'  and ";
            if (sh.Basecode != "")
                wherestr += "Basecode like '%" + sh.Basecode + "%'  and ";
            if (sh.Sqlstr != "")
                wherestr += " " + sh.Sqlstr + "  and ";
        
            if (wherestr != "")
                wherestr = wherestr.Substring(0, wherestr.Length - 4);

            return wherestr;

        }
        #endregion 

        /// <summary>
        /// xls连接
        /// </summary>
        /// <param name="tmpPath"></param>
        /// <returns></returns>
        public static DataTable readxls(string tmpPath)
        {
            string strconn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + tmpPath + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            OleDbConnection conn = new OleDbConnection(strconn);
            conn.Open();
            string sql = "SELECT * FROM [sheet1$] ";
            DataSet objDS = new DataSet();
            OleDbDataAdapter objadp = new OleDbDataAdapter(sql, conn);
            objadp.Fill(objDS);
            conn.Close();
            return objDS.Tables[0]; ;
        }



        #region 读取导入的文件，将CSV文件的数据读取到DataTable中
        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name=""></param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static DataTable OpenCSV(string fileName)
        {
            

          

            DataTable dt = new DataTable();
            FileStream fs = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            try
            {
                //记录每次读取的一行记录
                string strLine = "";
                //记录每行记录中的各字段内容
                string[] aryLine;
                //标示列数
                int columnCount = 0;
                //标示是否是读取的第一行
                bool IsFirst = true;

                //逐行读取CSV中的数据
                while ((strLine = sr.ReadLine()) != null)
                {
                    aryLine = strLine.Split(','); //把读取到的内容分割
                    if (IsFirst == true)
                    {
                        IsFirst = false;
                        columnCount = aryLine.Length;
                        //创建列
                        for (int i = 0; i < columnCount; i++)
                        {
                            DataColumn dc = new DataColumn(aryLine[i]);
                            dt.Columns.Add(dc);
                        }
                    }
                    else
                    {
                        DataRow dr = dt.NewRow(); //创建行
                        for (int j = 0; j < columnCount; j++)
                        {
                            dr[j] = aryLine[j];
                        }
                        dt.Rows.Add(dr);
                    }
                }

                sr.Close();
                fs.Close();

            }
            catch (Exception ex)
            {
              //  Mes += "文件导入失败，请检查数据格式！" + ex.ToString() + "/r/n";
            }

            finally
            {
                sr.Close();
                fs.Close(); 
            }

            return dt;
        }
        #endregion


        // 新建表速度标识
        public static DataTable speedtable() 
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name", Type.GetType("System.String"));
            string[] speed = { "S" ,"T","H","L","M","N","P","Q","R","U","V","W","Y","Z" };
            for (int i = 0; i < speed.Length;i++ )
            {
                DataRow dr = dt.NewRow();
                dr["name"] = speed[i];
                dt.Rows.Add(dr);
            }
            return dt;
        }


        // 新建表载重指数
        public static DataTable LOADINGtable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name", Type.GetType("System.Int32")); 
            for (int i = 50; i <131; i++)
            {
                DataRow dr = dt.NewRow();
                dr["name"] = i;
                dt.Rows.Add(dr);
            }
            return dt;
        }


        // 新建表轮毂直径
        public static DataTable Rtable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name", Type.GetType("System.Int32"));
            for (int i = 12; i < 23; i++)
            {
                DataRow dr = dt.NewRow();
                dr["name"] = i;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        // 新建表适用季节
        public static DataTable SEASONtable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name", Type.GetType("System.String"));
            string[] SEASON = { "ALL", "SUMMER", "WINTER" };
            for (int i = 0; i < SEASON.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["name"] = SEASON[i];
                dt.Rows.Add(dr);
            }
            return dt;
        }

    }
}
