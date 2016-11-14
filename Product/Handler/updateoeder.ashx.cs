using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace product.Handler
{
	/// <summary>
	/// updateoeder 的摘要说明
	/// </summary>
	public class updateoeder : IHttpHandler
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		public void ProcessRequest(HttpContext context) {
			context.Response.ContentType = "text/plain";

			string id = context.Request["id"].ToString();

			if (hb.ProExecinset("exec proUpPlaceorder '" + id + "'")) {

				context.Response.Write("{\"result\":\"0\"}");
			}
			else {
				context.Response.Write("{\"result\":\"1\"}");
			}
			
		}

		public bool IsReusable {
			get {
				return false;
			}
		}
	}
}