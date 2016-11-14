using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace product.Handler
{
	/// <summary>
	/// clearCookie 的摘要说明
	/// </summary>
	public class clearCookie : IHttpHandler
	{


		public void ProcessRequest(HttpContext context) {
			context.Response.ContentType = "text/plain";

			

				HttpCookie cookie = HttpContext.Current.Request.Cookies["username"];


				if (cookie != null) {
					cookie = new HttpCookie("username");
					cookie.Expires = DateTime.Now.AddDays(-1d);
					HttpContext.Current.Response.Cookies.Add(cookie);
					context.Response.Write("1");
					
				}


			
		}

		public bool IsReusable {
			get {
				return false;
			}
		}
	}
}