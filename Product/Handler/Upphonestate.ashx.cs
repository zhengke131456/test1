using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace product.Handler
{
    /// <summary>
    /// Upphonestate 的摘要说明
    /// </summary>
    public class Upphonestate : IHttpHandler
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["id"].ToString();
            if (hb.insetpro("UPDATE dbo.OrderPhone SET statustype=2 WHERE id='" + id + "'"))//数据库清除
            {
                context.Response.Write("100");
            }
            else
            {
                context.Response.Write("200");
            }
         
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