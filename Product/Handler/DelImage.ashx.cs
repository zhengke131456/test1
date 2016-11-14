using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace product.Handler
{
    /// <summary>
    /// DelImage 的摘要说明
    /// </summary>
    public class DelImage : IHttpHandler
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
       
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";



            string columnName = context.Request["columnName"].ToString();
            string imgpath = context.Request["path"].ToString();
            string rpcode = context.Request["rpcode"].ToString();



            if (File.Exists(imgpath))
            {
                try
                {
                    File.Delete(imgpath);
                    if (hb.insetpro("UPDATE dbo.ProductImage SET " + columnName + " ='' WHERE ImgrRpcode='" + rpcode + "'"))//数据库清除
                    {
                        context.Response.Write("100");
                    }
                    else
                    {
                        context.Response.Write("300");
                    }
                 
                }
                catch
                {
                    context.Response.Write("300");
                }
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