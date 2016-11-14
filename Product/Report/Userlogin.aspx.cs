using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.Report
{
	public partial class Userlogin : Common.BasePage
	{
		protected string username = "", QCDate = "", QCEDate = "";

		protected int curpage = 1, pagesize = 50, allCount = 0;
		protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

		protected void Page_Load(object sender, EventArgs e) {

			if (!IsPostBack) {
				if (!string.IsNullOrEmpty(GetQueryString("page"))) {
					Querey();
				}
				try {
					InitParam("");
					bindData();
				}
				catch { }
			}


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
					literalPagination.Text = GenPaginationBar("Userlogin.aspx?page=[page]&username=" + username + "&QCDate=" + QCDate + "&QCEDate=" + QCEDate + "", pagesize, curpage, allCount);
				}

			}

		}
		private void InitParam(string Index) {
			#region


			sinfo.PageSize = 0; ;
			//sinfo.Orderby = "id";
			where();

			if (Index == "") {
				#region
				if (!string.IsNullOrEmpty(GetQueryString("page"))) {
					curpage = int.Parse(GetQueryString("page"));
					sinfo.PageIndex = curpage;
				}
				#endregion
			}
			else {

				sinfo.PageIndex = 1;
			}

			#endregion
		}




		/// <summary>
		/// 信息
		/// </summary>
		/// <returns></returns>
		private DataSet getData() {

			DataSet dtt = new DataSet();
			dtt = hb.QXGetprodList(sinfo);

			return dtt;
		}
		protected void btnQuerey_Click(object sender, EventArgs e) {
			Querey();
			try {
				InitParam("query");
				bindData();
			}
			catch { }
		}



		public void Querey() {

			if (!string.IsNullOrEmpty(Request.Form["QCDate"])) {
				QCDate = Request.Form["QCDate"].Trim();
			}
			if (!string.IsNullOrEmpty(Request.Form["QCEDate"])) {
				QCEDate = Request.Form["QCEdate"].Trim();
			}
			if (!string.IsNullOrEmpty(Request.Form["username"])) {
				username = Request.Form["username"].Trim();
			}
			
		}

		public void where() {

				sinfo.Sqlstr = "sys_log where 1=1 ";
			if (QCDate != "") {
				sinfo.Sqlstr += " and  log_datetime  >='" + QCDate + "' and  log_datetime  <='" + QCEDate + "'  ";
			}
			if (username != "") {
				sinfo.Sqlstr += "  and  log_opname  LIKE '%" + username + "%' ";
			}
			
			
			sinfo.Orderby += "  id DESC ";

		}

	}
}