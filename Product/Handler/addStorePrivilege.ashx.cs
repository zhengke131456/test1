using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace product.Handler
{
	/// <summary>
	/// addStorePrivilege 的摘要说明
	/// </summary>
	public class addStorePrivilege : IHttpHandler
	{

		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		public void ProcessRequest(HttpContext context) {
			context.Response.ContentType = "text/plain";
			string id = context.Request["id"].ToString();
			string note = context.Request["note"].ToString();
			if (hb.ProExecinset("exec pro_AddStorenodes '" + id + "','" + note + "' ")) {
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