using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace product.Handler
{
	/// <summary>
	/// addFunctionalnodes 的摘要说明
	/// </summary>
	public class addFunctionalnodes : IHttpHandler
	{

		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		public void ProcessRequest(HttpContext context) {
			context.Response.ContentType = "text/plain";
			string id = context.Request["id"].ToString();
			string note = context.Request["note"].ToString();
			 if(hb.ProExecinset("exec pro_AddFunctionalnodes '" + id + "','" + note + "' "))
			 {
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