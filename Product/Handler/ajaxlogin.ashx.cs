using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net;
using product.Common;
using System.Web.SessionState;
namespace product.Handler
{
    /// <summary>
    /// ajaxlogin 的摘要说明
    /// </summary>
    public class ajaxlogin : IHttpHandler, IRequiresSessionState
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string uname = context.Request["um"].ToString();
            string upd = context.Request["pd"].ToString();

			string chb = context.Request["pdq"].ToString();
	
            DataTable dt = hb.getdate("*", "userinfo where username='" + uname+"'");
			string IPaddress = BasePage.GetAddressIP();
            try
            {
                if (upd == dt.Rows[0]["userpass"].ToString())
                {

					if (chb == "true") {

						HttpCookie cookie2 = new HttpCookie(IPaddress,uname);
						cookie2.Expires = DateTime.Now.AddDays(30);
						HttpContext.Current.Response.Cookies.Add(cookie2);

					}
					else {

						HttpCookie sxcookie = new HttpCookie(IPaddress);
						sxcookie.Expires = DateTime.Now.AddDays(-1);
						HttpContext.Current.Response.Cookies.Add(sxcookie);


					}
                    HttpCookie cookie = new HttpCookie("username");
                    cookie.Value = uname;
                    HttpContext.Current.Response.Cookies.Add(cookie);

					hb.insetpro("INSERT INTO dbo.sys_log( log_name , log_opname , log_note ,logtype,AddressIP )VALUES  ( '系统登录日志','" + uname + "','','登录日志','" + IPaddress + "')");
                    // HttpContext.Current.Response.Cookies.Add(new HttpCookie("username", uname));

                    HttpContext.Current.Session["IP"] = IPaddress;
                    HttpContext.Current.Session["UserName"] = uname;
                    context.Response.Write("1");
                }
            }
            catch
            {


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