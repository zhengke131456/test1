using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace product.Handler
{
    /// <summary>
    /// UpPoint 的摘要说明
    /// </summary>
    public class UpPoint : IHttpHandler
    {
        protected string id = "", state = "0", table = "" ,px="",py="",type= "";
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                table = context.Request["table"];
                id = context.Request["id"];
                px = context.Request["px"];
                py = context.Request["py"];

                string sql = "update " + table + " set px='" + px + "',py='" + py + "' where id='" + id + "'";

                if (hb.ExeSql(sql))
                {
                    context.Response.Write("{\"result\":\"1\"}");
                }
                else
                {
                    context.Response.Write("{\"result\":\"0\"}");
                }
            }
            catch { }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}