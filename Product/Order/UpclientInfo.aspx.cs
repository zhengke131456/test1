using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.Order
{
	public partial class UpclientInfo : Common.BasePage
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		string id;
		protected void Page_Load(object sender, EventArgs e) {
			id = GetQueryString("id");
			if (!IsPostBack) {
			
				DataTable dt = hb.getdate("*", "ClentInfo where ID=" + id);
				if (dt.Rows.Count > 0) {
					txtcode.Value = dt.Rows[0]["clientCode"].ToString();
					addres.Value = dt.Rows[0]["addres"].ToString();
					Note.Value = dt.Rows[0]["Note"].ToString();
				
				}
			}
		}



		protected void Button1_Click(object sender, EventArgs e) {

			string clientCode = GetQueryString("txtcode");
			string addres = GetQueryString("addres");

			string note = GetQueryString("Note");

			string images = "clientCode='" + clientCode + "',addres='" + addres + "',Note='" + note + "'";
			string strwhere = "ID='" + id + "'";

			string table = " ClentInfo  where clientCode='" + clientCode + "' and id<>'" + id + "'";
			

			if (hb.GetScalar(table) == 0) {
				if (hb.updateWhereID("ClentInfo", strwhere, images)) {
					Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../Order/clientInfo.aspx';</script>");
				}
				else {
					Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../Order/clientInfo.aspx';</script>");
				}
			}
			else {
				Response.Write("<script type='text/javascript'>alert('已有该记录，请核实再修该');window.location.href='../Order/clientInfo.aspx';</script>");
			}


		}

	}
}