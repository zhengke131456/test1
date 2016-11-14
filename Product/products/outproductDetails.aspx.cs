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
using System.Collections;
using System.Configuration;

namespace product.products
{
    public partial class outproductDetails : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected string cai = "", Edate, Bdate;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList(); 
       // StringBuilder sqlpin = new StringBuilder();
        StringBuilder sqlRepeat = new StringBuilder();//查看导入的某列是否重复
        public  string part;
        public  string biaoti;
        protected void Page_Load(object sender, EventArgs e)
        {
            //returnCount--;
            if (!IsPostBack)
            {
               
 
                Hiddentype.Value = GetQueryString("id").Trim();
               
                try
                {
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
                dt.Columns.Add("markName");
                IDictionary dictmark = (IDictionary)ConfigurationManager.GetSection("spmark");
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    for (int j = 1; j <= dictmark.Count; j++)
                    {
                        string type = "type" + j;

                        if (dt.Rows[i]["ipd_Status"].ToString() == "45")
                        {
                                dt.Rows[i]["markName"] = "置换出库"; 
                           
 
                        }
                        else
                        {
                            if (dt.Rows[i]["ipd_Status"].ToString() == j.ToString())
                            {
                                dt.Rows[i]["markName"] = dictmark[type].ToString();
                            }
                        }

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
                    literalPagination.Text = GenPaginationBar("outproductDetails.aspx?page=[page]&id=" + Hiddentype.Value + "", pagesize, curpage, allCount);
                }
               
            }
        }


        private void InitParam(string Index)
        {
            #region
            sinfo.PageSize = 20;
            sinfo.Orderby = "id";
                if (!string.IsNullOrEmpty(GetQueryString("page")))
                {
                    curpage = int.Parse(GetQueryString("page"));
                    sinfo.PageIndex = curpage;
                }

                sinfo.Sqlstr = "(SELECT inproductDetails.id,ipd_ID ,out_id, ipd_CAI ,ipd_WHCode ,ipd_price ,  ipd_note ,ipd_Status , outdatetime , ipd_number ,ipd_Statusold ,ipd_rknumber ,basename,log_note  FROM  dbo.inproductDetails LEFT JOIN dbo.baseinfo ON  ipd_WHCode=Basecode LEFT JOIN dbo.sys_storagelog ON  log_code=ipd_number  WHERE [type]=4 AND  out_id='" + Hiddentype.Value + "' AND ipd_Status IN(SELECT (CASE WHEN  spmark='4' THEN '45' ELSE spmark END )spmark FROM  dbo.outproduct WHERE id='" + Hiddentype.Value + "')   ) aa  ";


             

            #endregion
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