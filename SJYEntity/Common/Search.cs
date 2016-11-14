using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJYEntity.Common
{
    public class Search
    {
        private int pageIndex = 1;

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        private int pageSize = 20;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        private string tablename = "";
        /// <summary>
        /// 表名
        /// </summary>
        public string Tablename
        {
            get { return tablename; }
            set { tablename = value; }
        }

        private string protype = "";

        public string Protype
        {
            get { return protype; }
            set { protype = value; }
        }
        private string orderby = "";

        public string Orderby
        {
            get { return orderby; }
            set { orderby = value; }
        }
        //like查询
        private string basename = "";

        public string Basename
        {
            get { return basename; }
            set { basename = value; }
        }
        private string basecode = "";
        public string Basecode
        {
            get { return basecode; }
            set { basecode = value; }
        }


        //sql命令 from 后面拼接语句（数据源）
        private string sqlstr = "";

        public string Sqlstr
        {
            get { return sqlstr; }
            set { sqlstr = value; }
        }
        

    }
}
