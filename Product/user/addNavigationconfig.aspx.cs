using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace product.user
{
	public partial class addNavigationconfig : Common.BasePage
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

		protected void Page_Load(object sender, EventArgs e) {
			//if (!IsPostBack) {
		
			//}
		}

		protected void Button1_Click(object sender, EventArgs e) {


			if (hb.GetScalar("Sys_Function where  SF_rcode='" + GetQueryString("txtcode") + "'") == 0) {
				string sql = "INSERT INTO dbo.Sys_Function ( SF_rcode ,SF_rname , SF_baseClass , SF_level , SF_Url ,SF_order ) values ('" + GetQueryString("txtcode") + "','" + GetQueryString("tetName") + "','" + GetQueryString("Fnode") + "','" + GetQueryString("level") + "','" + GetQueryString("url") + "','" + GetQueryString("SF_order") + "')";
				if (hb.insetpro(sql)) {
					Response.Write("<script type='text/javascript'>alert('添加成功');window.location.href='../user/Navigationconfig.aspx';</script>");
				}
				else {
					Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../user/addNavigationconfig.aspx';</script>");
				}
			}
			else {
				Response.Write("<script type='text/javascript'>alert('编码已存在！');window.location.href='../user/addNavigationconfig.aspx';</script>");
 
			}
		}

		
	}
}