using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace product.Order
{
	public partial class addclientInfo : Common.BasePage
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				
			}
		}

		protected void Button1_Click(object sender, EventArgs e) {


			string sql = "insert into ClentInfo (  clientCode ,addres , Note , opcode ,inserttime ) values ('" + GetQueryString("txtcode") + "','" + GetQueryString("addres") + "','" + GetQueryString("Note") + "','" + userCode()+ "','" +DateTime.Now+ "')";
			if (hb.insetpro(sql)) {
				Response.Write("<script type='text/javascript'>alert('添加成功');window.location.href='../Order/clientInfo.aspx';</script>");
			}
			else {
				Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../Order/addclientInfo.aspx';</script>");
			}
		}

	
	}
}