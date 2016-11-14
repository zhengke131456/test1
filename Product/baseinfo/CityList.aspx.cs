using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.baseinfo
{
    public partial class CityList : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitParam("");
                bind();
            }
        }

        private void InitParam(string Index)
        {
            #region
            string partRights = isPartcode();
            sinfo.PageSize = 20;
            sinfo.Orderby = "id";
            sinfo.Sqlstr = "(select * from city)hh";


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

                sinfo.PageIndex = 1;
            }
            #endregion
        }


        private void bind()
        {
            DataSet ds = hb.QXGetprodList(sinfo);

            if (!ds.Equals(null))
            {
                allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                lblCount.Text = allCount.ToString();

                DataTable dt = ds.Tables[1];

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
                    literalPagination.Text = GenPaginationBar("citylist.aspx?page=[page]", pagesize, curpage, allCount);
                }
            }
        }
       
    }
}