using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.Collections;
using System.Configuration;

namespace product.Order
{
	public partial class batchmanagement : Common.BasePage
	{

		protected int curpage = 1, pagesize = 20, allCount = 0;
		protected string flow3 = "";//流程节点1（待管理员上传反馈）

		protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		protected void Page_Load(object sender, EventArgs e) {
			try {
				#region 流程节点1（待管理员上传反馈）


				IDictionary dictmark = (IDictionary)ConfigurationManager.GetSection("flowone");

				flow3 = dictmark["flow1"].ToString();//流程节点1（待管理员上传反馈）


				#endregion
                //SendMail163("cdq", "sssss", "VVVVVVVVVVV");

				InitParam("");
				bindData();
			}
			catch { }
			hiddbatch.Value = this.Request.QueryString["batch"];
		}
		private void bindData() {

			DataSet ds = getData();
	
			if (!ds.Equals(null)) {
				allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
				lblCount.Text = allCount.ToString();

				DataTable dt = ds.Tables[1];
			


				dislist.DataSource = dt;
				dislist.DataBind();
				if (allCount <= pagesize) {
					literalPagination.Visible = false;
				}
				else {
					literalPagination.Visible = true;
					literalPagination.Text = GenPaginationBar("batchmanagement.aspx?page=[page]", pagesize, curpage, allCount);
				}
			}
		}
		private DataSet getData() {
			DataSet dt = new DataSet();
			dt = hb.GetprodList(sinfo);

			return dt;
		}

		private void InitParam(string Index) {
		
			sinfo.PageSize = 20; ;
			sinfo.Orderby = "id";
			sinfo.Tablename = "Orderbatch";
			string username = userCode();
			string partRights= ispartRights() ;
			if (partRights == "运营主管") {
				sinfo.Sqlstr = "  opcode='" + username + "'";
			}
			else if (partRights == "系统管理员") {
				sinfo.Sqlstr = " 1=1 ";
			}
			else {
				sinfo.Sqlstr = " ordersUser='" + username + "'";
			}
			if (!string.IsNullOrEmpty(GetQueryString("page"))) {
				curpage = int.Parse(GetQueryString("page"));
				sinfo.PageIndex = curpage;
			}
			else { sinfo.PageIndex = 1; }
			
		}

	
	}
}