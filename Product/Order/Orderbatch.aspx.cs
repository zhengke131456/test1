using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

namespace product.Order
{
	public partial class Orderbatch : Common.BasePage
	{
		protected string batchNo = "";
		protected int curpage = 1, pagesize = 20, allCount = 0;
		protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		StringBuilder sqlpin = new StringBuilder();
		StringBuilder sqlnum = new StringBuilder();
		StringBuilder sqlCad = new StringBuilder();
		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {

				if (!IsPostBack) {
					if (GetQueryString("batchNo") != "") {
						batchNo = GetQueryString("batchNo");
					}
					try {
						InitParam("");
						bindData();
					}
					catch { }

				}
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
					literalPagination.Text = GenPaginationBar("placeorder.aspx?page=[page]&batchNo=" + batchNo + "", pagesize, curpage, allCount);
				}
			}
		}


		private void InitParam(string Index) {
			#region
			sinfo.PageSize = 20; ;
			sinfo.Orderby = "batchNo";
			string username = userCode();
			if (username == "admin") {
				sinfo.Sqlstr = "OrderBatchNumber where 1=1  ";
			}
			else {
				sinfo.Sqlstr = "OrderBatchNumber where opcode='" + username + "' ";
			}



			if (batchNo != "") {
				sinfo.Sqlstr += "  and  batchNo  LIKE'%" + batchNo + "%' ";
			}
			
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
			DataSet dt = new DataSet();
			dt = hb.QXGetprodList(sinfo);

			return dt;
		}
		protected void btnQuerey_Click(object sender, EventArgs e) {
			batchNo = GetQueryString("batchNo");
						if (batchNo != "") {

				sinfo.Sqlstr += "  and  batchNo  LIKE'%" + batchNo + "%' ";
			}
			
			try {
				InitParam("query");
				bindData();
			}
			catch { }
		}
		
	}
}