using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace product.user
{
	public partial class UserPartRight : Common.BasePage
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
				dt.Columns.Add("show");


				dislist.DataSource = ds.Tables[1];
				dislist.DataBind();
				if (allCount <= pagesize) {
					literalPagination.Visible = false;
				}
				else {
					literalPagination.Visible = true;
					literalPagination.Text = GenPaginationBar("UserPartRight.aspx?page=[page]", pagesize, curpage, allCount);
				}

			}
		}


		private void InitParam() {
			#region

			sinfo.PageSize = 20;
			//sinfo.Tablename = "SYS_Part";
			sinfo.Orderby = "SP_ID";
			sinfo.Sqlstr = "SYS_Part where  ISNULL(SP_del,0)=0";
			if (!string.IsNullOrEmpty(GetQueryString("page"))) {
				curpage = int.Parse(GetQueryString("page"));
				sinfo.PageIndex = curpage;
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

	

	}
}