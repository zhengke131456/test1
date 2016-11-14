using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace product.Handler
{
	/// <summary>
	/// cancelorder 的摘要说明
	/// </summary>
	public class cancelorder : IHttpHandler
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

		public void ProcessRequest(HttpContext context) {
			context.Response.ContentType = "text/plain";

	         string 		batch = context.Request["batch"].ToString();
			 if (hb.insetpro("exec proCancelOrder '" + batch + "'"))//取消发货
			 {
				 context.Response.Write("{\"result\":\"0\"}");
			 }
			 else { context.Response.Write("{\"result\":\"1\"}"); }
			
		}

		public bool IsReusable {
			get {
				return false;
			}
		}
	}
}