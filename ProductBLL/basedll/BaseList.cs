using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ProductBLL.Basebll
{
    public class BaseList
    {
      public  string  bi(string ty)
        {
            switch (Convert.ToInt32(ty))
            {
                case 1:
                    return "编码"; 
                case 2:
                    return "花纹";
                
                case 3:
                    return "型号";
               
                case 4:
                    return "仓库";
                   
                case 5:
                    return "销售渠道";
                 
                case 6:
                    return "客户名称";
               
                default:
                    return  "管理";
                   
            }
        }


      /// <summary>
      /// 根据存储过程语句
      /// </summary>
      /// <returns></returns>
      public DataTable getProdatable(string proslq)
      {


          return productcommon.DataMgr.GetProDataTable(proslq);
      }
        /// <summary>
        /// 执行存储过程 返回真假
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
      public bool ProExecinset(string sql)
      {

          if (productcommon.DataMgr.ExcProcedure(sql))
          {
              return true;
          }
          return false;
      }
      /// <summary>
      /// 执行存储过程 返回第一行第一列
      /// 根据定义的返回结果 来解析相应的返回内容
      /// </summary>
      /// <param name="sql"></param>
      /// <returns></returns>
      public int getproScalar(string sql)
      {

          int result=productcommon.DataMgr.GetScalar(sql);
          return result;
      }
        /// <summary>
        /// 新增(basefb)
        /// </summary>
        /// <returns></returns>
        public bool insetpro( string sql)
        {
            //1编码2花纹
            if (productcommon.DataMgr.ExcuteCommand(  sql))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <returns></returns>
        public bool ExeSql(string sql)
        {
            //1编码2花纹
            if (productcommon.DataMgr.ExcuteCommand(sql))
            {
                return true;
            }
            return false;
        }



        /// <summary>
        /// 列表(分数据库）有权限判断
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public DataSet QXGetprodList(SJYEntity.Common.Search sh)
        {
            string sqldt = ProductBLL.Search.Searcher.QXGetbaselistsql(sh);

            //列表信息
            DataSet dtlist = new DataSet();
            dtlist = productcommon.DataMgr.GetDataSet(sqldt);

            return dtlist;
        }

        /// <summary>
        /// 列表(分数据库）
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public DataSet GetprodList(SJYEntity.Common.Search sh)
        {
            string sqldt = ProductBLL.Search.Searcher.Getbaselistsql(sh);

            //列表信息
            DataSet dtlist = new DataSet();
            dtlist = productcommon.DataMgr.GetDataSet(sqldt);

            return dtlist;
        }

        /// <summary>
        /// 查询数据  DataSet
        /// </summary>
        /// <param name="sh"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
           

            //列表信息
            DataSet dtlist = new DataSet();
            dtlist = productcommon.DataMgr.GetDataSet(sql);
            return dtlist;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public bool delbase(string table, string id)
        {
            string sql = "delete from " + table + " where id='" + id + "'";

            if (productcommon.DataMgr.ExcuteCommand(sql))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除where
        /// </summary>
        /// <returns></returns>
        public bool delbasewhere(string table, string where )
        {
            string sql = "delete from " + table + " where " + where + "";

            if (productcommon.DataMgr.ExcuteCommand(sql))
            {
                return true;
            }
            return false;
        }
       

        /// <summary>
        /// 得到数据
        /// </summary>
        /// <returns></returns>
        public DataTable getdate(string cha,string where)
        {
            string sql = "select "+cha+" from " + where;

            return productcommon.DataMgr.GetDataTable(sql);
        }

        /// <summary>
        ///修改
        /// </summary>
        /// <param name="check"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool update(string tablename, string id, string imagess)
        {
            string sql = "UPDATE " + tablename + " SET " + imagess + " where id=" + id + "";
            if (productcommon.DataMgr.ExcuteCommand(sql))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        ///修改
        /// </summary>
        /// <param name="check"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool updateWhereID(string tablename, string id, string imagess)
        {
            string sql = "UPDATE " + tablename + " SET " + imagess + " where "+id;
            if (productcommon.DataMgr.ExcuteCommand(sql))
            {
                return true;
            }
            return false;
        }
        public int GetScalar(string table) 
        {
            string sql = "select count(*) from "+table;
            return productcommon.DataMgr.GetScalar(sql);
        }
        public string GetScalarstring(string cha, string where)
        {
            string sql = "select " + cha + " from " + where;
            return productcommon.DataMgr.GetScalarString(sql);
        }
        public DataTable GetTable(string table)
        {
            string sql = "select basename  from " + table;
            return productcommon.DataMgr.GetDataTable(sql);
        }
    }
}
