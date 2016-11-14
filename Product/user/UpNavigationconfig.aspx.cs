using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.user
{
	public partial class UpNavigationconfig : Common.BasePage
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();



		public string name, code, fnode, level, url, isdelName, isdelclass,SF_order;
		protected void Page_Load(object sender, EventArgs e) {

			if (!IsPostBack) {
				if (!string.IsNullOrEmpty(GetQueryString("id"))) {
					hddID.Value = GetQueryString("id");

					string where = "dbo.Sys_Function WHERE SF_ID=" + hddID.Value;
					string cha = " * ";
					DataTable dt = hb.getdate(cha, where);
					if (dt.Rows.Count > 0) {
						code = dt.Rows[0]["SF_rcode"].ToString();
						name = dt.Rows[0]["SF_rname"].ToString();
						fnode = dt.Rows[0]["SF_baseClass"].ToString();
						level = dt.Rows[0]["SF_level"].ToString();
						url = dt.Rows[0]["SF_Url"].ToString();
						SF_order = dt.Rows[0]["SF_order"].ToString();
						if (dt.Rows[0]["SF_del"].ToString() == "0") {
							isdel.Checked = true;
						}
						else {
							isdel.Checked = false;
							
						}
						isdel.Value = dt.Rows[0]["SF_del"].ToString();
					}
				}
			}
			//biaoti = hb.bi(typep);
		}



		protected void Button1_Click(object sender, EventArgs e) {

			string del = "0";

			if (!isdel.Checked) { del = "1"; }
			
			
			string code = GetQueryString("code");
			string images = "SF_rcode='" + code + "' ,SF_rname ='" + GetQueryString("name") + "', SF_baseClass='" + GetQueryString("Fnode") + " ', SF_level='" + GetQueryString("level") + "' ,SF_order='" + GetQueryString("SF_order") + "', SF_Url='" + GetQueryString("url") + "', SF_del='" + del + "'";


			if (hb.GetScalar(" dbo.Sys_Function WHERE  SF_rcode='" + code + "' AND  SF_ID!='" + hddID.Value + "'") != 0) {
				Response.Write("<script type='text/javascript'>alert('当前编码已经存在，请核实后在修改');window.location.href='../user/UpNavigationconfig.aspx?id=" + hddID.Value + "';</script>");
				return;
			}
			else {


				if (hb.updateWhereID("dbo.Sys_Function", "sf_id='" + hddID.Value + "'", images)) {
					Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../user/Navigationconfig.aspx';</script>");
				}
				else {
					Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../user/UpNavigationconfig.aspx?id=" + hddID.Value + "';</script>");
				}
			}

		}
	}
}