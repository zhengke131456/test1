using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using productcommon;

namespace product.baseinfo
{
	public partial class baseinfoStore : Common.BasePage
	
    {
		protected string Code = "" ,styledisplay = "style=\"display: none\"";
			
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		StringBuilder sqlpin = new StringBuilder();
        StringBuilder sqlRepeat = new StringBuilder();//查看导入的某列是否重复
        public  string type;
        public  string biaoti;
        protected void Page_Load(object sender, EventArgs e)
        {
			if (userCode() == "admin") { styledisplay = ""; }
            if (!IsPostBack)
            {
				try {
					InitParam("");
					bindData();
				}
				catch { }
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
                dt.Columns.Add("show");
                dt.Columns.Add("ispoint");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dt.Rows[i]["px"].ToString()) && string.IsNullOrEmpty(dt.Rows[i]["py"].ToString()))
                    {
                        dt.Rows[i]["ispoint"] = "<span style=\"color:Red;\">否</span>";
                    }
                    else
                    {
                        dt.Rows[i]["ispoint"] = "是";
                    }
                }

                dislist.DataSource = dt;
                dislist.DataBind();
                if (allCount <= pagesize)
                {
                    literalPagination.Visible = false;
                }
                else
                {
                    literalPagination.Visible = true;
					literalPagination.Text = GenPaginationBar("baseinfoStore.aspx?page=[page]&BaseName=" + Code, pagesize, curpage, allCount);
                }
                biaoti=hb.bi(type);
            }
        }


        private void InitParam(string Index )
        {

			sinfo.PageSize = 20; ;
			sinfo.Orderby = "type";
			string partRights = isPartcode();
			//sinfo.Sqlstr = " dbo.BaseStore WHERE   Basecode  IN ( SELECT SR_storecode  FROM  dbo.SYS_RightStore where SP_code='" + partRights + "' AND [type]= '0' ) ";

            sinfo.Sqlstr = " dbo.BaseStore WHERE   Basecode  IN ( SELECT SR_storecode  FROM  dbo.SYS_RightStore where [type]= '0' ) ";

			if (Code != "") {

				sinfo.Sqlstr += " and  basename like  '%" + Code + "%'";
			}

            if (Index == "")
            {
                #region
               
                    if (!string.IsNullOrEmpty(GetQueryString("page")))
                    {
                        curpage = int.Parse(GetQueryString("page"));
                        sinfo.PageIndex = curpage;
                    }
                #endregion
            }
            else
            {
              
                sinfo.PageIndex =1;
            }
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

  

        protected void btnQuerey_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["BM"]))
            {
                Code = Request["BM"].Trim();
            }
            try
            {
                InitParam("1");
                bindData();
            }
            catch { }
        }
       
    }
}