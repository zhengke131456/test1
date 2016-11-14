using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace product.Order
{
	public partial class ClientInfo : Common.BasePage
	{
		protected int curpage = 1, pagesize = 20, allCount = 0;
		protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		StringBuilder sqlpin = new StringBuilder();
		public string type;

		protected void Page_Load(object sender, EventArgs e) {

			try {
				InitParam();
				bindData();
			}
			catch { }
		}
		private void bindData() {

			DataSet ds = getData();

			if (!ds.Equals(null)) {
				allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
				lblCount.Text = allCount.ToString();

				DataTable dt = ds.Tables[1];
			

				dislist.DataSource = ds.Tables[1];
				dislist.DataBind();
				if (allCount <= pagesize) {
					literalPagination.Visible = false;
				}
				else {
					literalPagination.Text = GenPaginationBar("clientInfo.aspx?page=[page]", pagesize, curpage, allCount);
				}

			}
		}


		private void InitParam() {
			#region

			sinfo.PageSize = 20;
			sinfo.Tablename = "ClentInfo";
			sinfo.Orderby = "id";
			if (!string.IsNullOrEmpty(GetQueryString("page"))) {
				curpage = int.Parse(GetQueryString("page"));
				sinfo.PageIndex = curpage;
			}
			sinfo.Sqlstr = "opcode='" + userCode() + "'";
	


			#endregion
		}



		/// <summary>
		/// 信息
		/// </summary>
		/// <returns></returns>
		private DataSet getData() {
			DataSet dt = new DataSet();
			dt = hb.GetprodList(sinfo);

			return dt;
		}

		
	}
}