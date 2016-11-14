using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace productcommon
{
    /// <summary>
    /// 数据库管理类
    /// </summary>
    public sealed class DataMgr
    {

        #region 连接字符串
        protected static readonly string conStr = ConfigurationManager.ConnectionStrings["Con"].ToString();
        static SqlConnection connection;
        public static SqlConnection Connection
        {
            get
            {
                //string conStr = " Data Source=.;Initial Catalog=readhtml;Integrated Security=True";
                //string conStr = ConfigurationManager.ConnectionStrings["Con"].ToString();
                if (connection == null)
                {
                    connection = new SqlConnection(conStr);
                    connection.Open();
                }
                else if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                else if (connection.State == ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }
        #endregion
       

        #region 初始化一个products表
        public static DataTable GetTableSchema()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{  
            new DataColumn("CAI",typeof(string)),  
            new DataColumn("CAD",typeof(string)),  
            new DataColumn("Dimension",typeof(string)),
            new DataColumn("model",typeof(string)),
            new DataColumn("P",typeof(string)),  
            new DataColumn("AcrossWidth",typeof(int)),  
            new DataColumn("GKB",typeof(int)),
             new DataColumn("R",typeof(string)),  
            new DataColumn("Rimdia",typeof(string)),  
            new DataColumn("PATTERN",typeof(string)),
            new DataColumn("Mark",typeof(string)),
            new DataColumn("LOADINGs",typeof(string)),  
            new DataColumn("SPEEDJb",typeof(string)),  
            new DataColumn("EXTRALOAD",typeof(string)),

             new DataColumn("OE",typeof(string)),  
            new DataColumn("des",typeof(string))
            });

            return dt;
        }
        #endregion

        #region 执行SqlBulkCopy 拷贝表到products
        public static bool BulkToDBProducts(DataTable dt)
        {
            bool result = false;
            SqlConnection sqlConn = new SqlConnection(conStr);

            SqlBulkCopy bulkCopy = new SqlBulkCopy(conStr, SqlBulkCopyOptions.CheckConstraints);
            bulkCopy.DestinationTableName = "products";
            bulkCopy.ColumnMappings.Add("CAD", "CAD");
            bulkCopy.ColumnMappings.Add("CAI", "CAI");
            bulkCopy.ColumnMappings.Add("Dimension", "Dimension");
            bulkCopy.ColumnMappings.Add("model", "model");
            bulkCopy.ColumnMappings.Add("P", "P");
            bulkCopy.ColumnMappings.Add("AcrossWidth", "AcrossWidth");
            bulkCopy.ColumnMappings.Add("GKB", "GKB");
            bulkCopy.ColumnMappings.Add("R", "R");
            bulkCopy.ColumnMappings.Add("Rimdia", "Rimdia");
            bulkCopy.ColumnMappings.Add("PATTERN", "PATTERN");
            bulkCopy.ColumnMappings.Add("Mark", "Mark");
            bulkCopy.ColumnMappings.Add("LOADINGs", "LOADINGs");
            bulkCopy.ColumnMappings.Add("SPEEDJb", "SPEEDJb");
            bulkCopy.ColumnMappings.Add("EXTRALOAD", "EXTRALOAD");
            bulkCopy.ColumnMappings.Add("OE", "OE");
            bulkCopy.ColumnMappings.Add("des", "des");
            bulkCopy.BatchSize = dt.Rows.Count;

            try
            {
                sqlConn.Open();
                if (dt != null && dt.Rows.Count != 0)
                    bulkCopy.WriteToServer(dt);
                result = true;

            }
            catch
            {

                result = false;
            }
            finally
            {
                sqlConn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();

            }
            return result;
        }
        #endregion

        #region 初始化一个产品价格表
        public static DataTable GetTableProPrice()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{  
            new DataColumn("PRSP_CAD",typeof(string)),  
            new DataColumn("PRSP_WHSPrice",typeof(decimal)),  
            new DataColumn("PRSP_Inprice",typeof(decimal)),
            new DataColumn("PRSP_Year",typeof(string)),
             new DataColumn("PR_ID",typeof(int)),
              new DataColumn("PRSP_number",typeof(string)),
           
            });

            return dt;
        }
        #endregion
        #region 初始化一个返利表
        public static DataTable GetTableRebate()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{  
            new DataColumn("RE_CAD",typeof(string)),  
            new DataColumn("RE_1",typeof(decimal)),  
            new DataColumn("RE_2",typeof(decimal)),
            new DataColumn("RE_3",typeof(decimal)),
             new DataColumn("RE_4",typeof(decimal)),
            new DataColumn("RE_5",typeof(decimal)),  
            new DataColumn("RE_Date",typeof(string)),
            new DataColumn("PR_ID",typeof(int))
            
            });

            return dt;
        }
        #endregion
       

        #region 执行SqlBulkCopy 拷贝表到 baseinfo
        public static bool BulkToDBbaseinfo(DataTable dt)
        {
            bool result = false;
            SqlConnection sqlConn = new SqlConnection(conStr);

            SqlBulkCopy bulkCopy = new SqlBulkCopy(conStr, SqlBulkCopyOptions.CheckConstraints);
            bulkCopy.DestinationTableName = "baseinfo";
            bulkCopy.ColumnMappings.Add("basename", "basename");//
            bulkCopy.ColumnMappings.Add("infotype", "type");


            bulkCopy.BatchSize = dt.Rows.Count;

            try
            {
                sqlConn.Open();
                if (dt != null && dt.Rows.Count != 0)
                    bulkCopy.WriteToServer(dt);
                result = true;

            }
            catch
            {

                result = false;
            }
            finally
            {
                sqlConn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();

            }
            return result;
        }
        #endregion

        #region 执行SqlBulkCopy 拷贝表到 Rebate
        public static bool BulkToDBRebate(DataTable dt)
        {
            bool result = false;
            SqlConnection sqlConn = new SqlConnection(conStr);

            SqlBulkCopy bulkCopy = new SqlBulkCopy(conStr, SqlBulkCopyOptions.CheckConstraints);
            bulkCopy.DestinationTableName = "Rebate";
            bulkCopy.ColumnMappings.Add("RE_CAD", "RE_CAD");//
            bulkCopy.ColumnMappings.Add("RE_1", "RE_1");
            bulkCopy.ColumnMappings.Add("RE_2", "RE_2");
            bulkCopy.ColumnMappings.Add("RE_3", "RE_3");
            bulkCopy.ColumnMappings.Add("RE_4", "RE_4");
            bulkCopy.ColumnMappings.Add("RE_5", "RE_5");
            bulkCopy.ColumnMappings.Add("RE_Date", "RE_Date");
            bulkCopy.ColumnMappings.Add("PR_ID", "PR_ID");
            bulkCopy.BatchSize = dt.Rows.Count;

            try
            {
                sqlConn.Open();
                if (dt != null && dt.Rows.Count != 0)
                    bulkCopy.WriteToServer(dt);
                result = true;

            }
            catch
            {

                result = false;
            }
            finally
            {
                sqlConn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();

            }
            return result;
        }
        #endregion

        #region 执行SqlBulkCopy 拷贝表到 ProductPrice
        public static bool BulkToDBProPrice(DataTable dt)
        {
            bool result = false;
            SqlConnection sqlConn = new SqlConnection(conStr);

            SqlBulkCopy bulkCopy = new SqlBulkCopy(conStr, SqlBulkCopyOptions.CheckConstraints);
            bulkCopy.DestinationTableName = "ProductPrice";
            bulkCopy.ColumnMappings.Add("PRSP_CAD", "PRSP_CAD");//
            bulkCopy.ColumnMappings.Add("PRSP_WHSPrice", "PRSP_WHSPrice");
            bulkCopy.ColumnMappings.Add("PRSP_Inprice", "PRSP_Inprice");
            bulkCopy.ColumnMappings.Add("PRSP_Year", "PRSP_Year");
            bulkCopy.ColumnMappings.Add("PR_ID", "PR_ID");
            bulkCopy.ColumnMappings.Add("PRSP_number", "PRSP_number");
            bulkCopy.BatchSize = dt.Rows.Count;

            try
            {
                sqlConn.Open();
                if (dt != null && dt.Rows.Count != 0)
                    bulkCopy.WriteToServer(dt);
                result = true;

            }
            catch
            {

                result = false;
            }
            finally
            {
                sqlConn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();

            }
            return result;
        }
        #endregion


        #region 调用存储过程执行插入命令

        
        public static bool ExcProcedure(string safeSql)
        {
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            //cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandTimeout=0;
            bool result = false;
            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
        #endregion


        #region 通过安全的sql语句，
        /// <summary>
        /// 通过安全的sql语句执行数据库操作
        /// </summary>
        /// <param name="safeSql"></param>
        /// <returns></returns>
        public static bool ExcuteCommand(string safeSql)
        {
            SqlCommand cmd = new SqlCommand(safeSql, Connection);

            cmd.Transaction = connection.BeginTransaction();

            bool result = false;
            try
            {
                if (cmd.ExecuteNonQuery() >= 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                cmd.Transaction.Commit();
            }
            catch
            {
                result = false;
                cmd.Transaction.Rollback();
            }

            return result;
        }
        #endregion


        #region 通过安全的sql语句，获取DataSet
        public static DataSet GetDataSet(string safeSql)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds ;
        }
        #endregion

        #region 通过安全的sql语句，获取DataTable
        public static DataTable GetDataTable(string safeSql)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
        #endregion

        #region 通过安全的sql语句获取首行首列
        public static int GetScalar(string safeSql)
        {
             int result ;
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            object result2 = cmd.ExecuteScalar();
            if (result2==null)
            {
                result = 0;
            }
            else
            {
                if (result2.ToString() == "")
                { result = 0; }
                else
                {
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return result;
        }
        #endregion

        #region 通过安全的sql语句获取首行首列 返回string 类型
        public static string  GetScalarString(string safeSql)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand(safeSql, Connection);
            if (cmd.ExecuteScalar() != null)
            {
               
                result = Convert.ToString(cmd.ExecuteScalar());
            }
           
            return result;
        }
        #endregion
        /// <summary>
        /// 执行存储过程
        /// </summary>
       
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public static DataTable GetProDataTable(string sql)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, Connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
}
