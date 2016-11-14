using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.user
{
	public partial class UserPassword : Common.BasePage
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		public string basename = "";

		public string email;
		protected void Page_Load(object sender, EventArgs e) {


			if(!IsPostBack)
			{
				string where = "dbo.userinfo WHERE  username='" + userCode() + "'";
				string cha = " Email ";
				DataTable dt = hb.getdate(cha, where);
				if (dt.Rows.Count > 0) {
					email = dt.Rows[0]["Email"].ToString();
				
				}
				//biaoti = hb.bi(typep);
			}
		}
		

		protected void Button1_Click(object sender, EventArgs e) {
			string email = GetQueryString("email");
			string pwd = GetQueryString("pwdnew");
			string images = "userpass='" + pwd + "',Email='" + email + "'";
			if (hb.updateWhereID("dbo.userinfo", "username='"+userCode()+"'", images)) {
				Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../user/UserPassword.aspx';</script>");
				}
				else {
					Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../user/UserPassword.aspx';</script>");
				}
			

		}
	}
}