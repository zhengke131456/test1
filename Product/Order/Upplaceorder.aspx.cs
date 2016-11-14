using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.Order
{
	public partial class Upplaceorder : Common.BasePage
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		public string QueryDate = "", CAD = "", status = "", number = "", ClientCode = "", placeorderName = "";
		protected void Page_Load(object sender, EventArgs e) {

			if (!IsPostBack) {
				if (!string.IsNullOrEmpty(GetQueryString("id"))) {
					hiddID.Value = GetQueryString("id");
					string where = "  Placeorder where id =" + hiddID.Value;
					string cha = "*";
					DataTable dt = hb.getdate(cha, where);

					if (dt.Rows.Count > 0) {
						for (int i = 0; i < dt.Rows.Count; i++) {
							QueryDate = dt.Rows[0]["QueryDate"].ToString();
							CAD = dt.Rows[0]["CAD"].ToString();
							status = dt.Rows[0]["ostatus"].ToString();
							number = dt.Rows[0]["number"].ToString();
							ClientCode = dt.Rows[0]["ClientCode"].ToString();
							placeorderName = dt.Rows[0]["placeorderName"].ToString();
							switch (dt.Rows[i]["ostatus"].ToString()) {
								case "0":
									status = "未下载";
									break;
								case "1":
									status = "已下载";
									break;
								case "2":
									status = "有货";
									break;
								case "3":
									status = "部分有货";
									break;
								case "4":
									status = "无货";
									break;
								case "5":
									status = "已下单";
									break;
								case "6":
									status = "未下单";
									break;
							}
						}
					}



				}
			}
		}
		protected void Button1_Click(object sender, EventArgs e) {


			string where = "number='" + GetQueryString("number") + "' ";
			if (hb.update("Placeorder", hiddID.Value, where)) {
				Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../Order/Upplaceorder.aspx?id=" + hiddID.Value + "';</script>");
			}
			else {
				Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../Order/Upplaceorder.aspx?id=" + hiddID.Value + "';</script>");
			}


		}
    
	}
}