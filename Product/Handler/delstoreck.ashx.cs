using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace product.Handler
{
	/// <summary>
	/// delstoreck 的摘要说明
	/// </summary>
	public class delstoreck : IHttpHandler
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		public void ProcessRequest(HttpContext context) {
			context.Response.ContentType = "text/plain";
			string id = context.Request["id"].ToString();

			string type = context.Request["type"].ToString(); //0 是入 1是出

			if (type == "0") {
                DeleteLog.InsertLog(id,0);
				if (hb.ProExecinset("exec pro_Delstock '" + id + "','" + type + "' ")) {
					context.Response.Write("{\"result\":\"100\"}");
				}
				else {
					context.Response.Write("{\"result\":\"200\"}");
				}
			}
			if (type == "1") {
                DeleteLog.InsertLog(id, 1);
				if (hb.ProExecinset("exec pro_Delstock '" + id + "','" + type + "' ")) {
					context.Response.Write("{\"result\":\"100\"}");
				}
				else {
					context.Response.Write("{\"result\":\"200\"}");
				}
			}

		}

		public bool IsReusable {
			get {
				return false;
			}
		}
	}
}