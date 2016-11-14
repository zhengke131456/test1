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
	public partial class Navigationconfig : Common.BasePage
	{

		protected string code = "", baseClass = "";
			protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected void Page_Load(object sender, EventArgs e)
        {

			if (!IsPostBack) {

				query();
			}
        }
        private void bindData()
        {

            DataSet ds = getData();

            if (!ds.Equals(null))
            {
                allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                lblCount.Text = allCount.ToString();

                DataTable dt = ds.Tables[1];
            
                dislist.DataSource = ds.Tables[1];
                dislist.DataBind();
                if (allCount <= pagesize)
                {
                    literalPagination.Visible = false;
                }
                else
                {
					literalPagination.Visible = true;
                    literalPagination.Text = GenPaginationBar("Navigationconfig.aspx?page=[page]", pagesize, curpage, allCount);
                }
                
            }
        }
		private void query() {
		
				if (GetQueryString("code") != "") {
					code = GetQueryString("code");
				}
				if (GetQueryString("baseClass") != "") {
					baseClass = GetQueryString("baseClass");
				}

	
			try {
				InitParam();
				bindData();
			}
			catch { }
		}

        private void InitParam()
        {
            #region

		       
            sinfo.PageSize = 20;
            sinfo.Orderby = "SF_rcode";
			sinfo.Sqlstr = "(SELECT SF_ID,SF_rcode ,SF_rname , SF_baseClass ,SF_level ,SF_Url , (CASE WHEN SF_del=0  THEN '是' ELSE '否' END)AS SF_del,SF_note ,SF_order,inserttime  FROM  dbo.Sys_Function where 1=1  ";
			if(code!="")
			{
				sinfo.Sqlstr +=" and SF_rcode like '"+code+"%' ";
			}
			if (baseClass!="") {
				sinfo.Sqlstr += " and SF_baseClass ='" + baseClass + "' ";
			}
			sinfo.Sqlstr +=" )sf ";
            if (!string.IsNullOrEmpty(GetQueryString("page")))
            {
                curpage = int.Parse(GetQueryString("page"));
                sinfo.PageIndex = curpage;
            }


            #endregion
        }

		protected void btnQuerey_Click(object sender, EventArgs e) {

			query();
		}

        /// <summary>
        /// 信息
        /// </summary>
        /// <returns></returns>
        private DataSet getData()
        {
            DataSet dt = new DataSet();
            dt = hb.QXGetprodList(sinfo);

            return dt;
        }

    

		
	}
}