using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Text;

namespace product.products
{
    public partial class historyoperation : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        StringBuilder sbinfo = new StringBuilder();//操作记录
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                Hiddencode.Value = GetQueryString("id").ToString();

                try
                {
                    InitParam();
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
                dt.Columns.Add("markName");
                IDictionary dictmark = (IDictionary)ConfigurationManager.GetSection("spmark");
                #region 标识状态
                
               
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    for (int j = 1; j <= dictmark.Count; j++)
                    {
                        string type = "type" + j;
                        if (dt.Rows[i]["hstatus"].ToString() == j.ToString())
                        {
                            if (j.ToString() == "4")
                            {
                                dt.Rows[i]["markName"] = "置换入库";
                            }

                            else
                            {
                                dt.Rows[i]["markName"] = dictmark[type].ToString();
                            }
                        }
                        else if (dt.Rows[i]["hstatus"].ToString() == "45")
                        {
                            dt.Rows[i]["markName"] = "置换出库";
                        }
                    }

                }
                #endregion

                #region 文字描述操作记录



                string Cai =  dt.Rows[0]["CAI"].ToString();;
                string ontnumber = Hiddencode.Value;//原始编码
                string rkstore = ""; //入库仓库
                string ckstroe = "";//出库仓库
                string inserttime = ""; //操作日期
                string markName = ""; //标识
                string code = "";//原始编码
                sbinfo.Append("<p>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ontnumber = dt.Rows[i]["onenumber"].ToString();
                    markName = dt.Rows[i]["markName"].ToString();
                    inserttime = dt.Rows[i]["inserttime"].ToString();
                    code = dt.Rows[i]["code"].ToString();
                    if (dt.Rows[i]["htype"].ToString() == "0")//入库
                    {
                        rkstore = dt.Rows[i]["rkbasename"].ToString();
                        sbinfo.Append("入库记录(操作日期)：" + inserttime + "&nbsp;入库仓库：" + rkstore + "&nbsp;流水号：" + code + "&nbsp;标识：" + markName + "</br> ");
                    }
                    else
                    {
                        ckstroe=dt.Rows[i]["ckbasename"].ToString();
                        sbinfo.Append("出库记录(操作日期)：" + inserttime + "&nbsp;出库仓库：" + ckstroe + "&nbsp;流水号：" + code + "&nbsp;标识：" + markName + "</br> ");
                    }
                  
                }
             
                sbinfo.Append("</p>");
                lbInfo.Text = sbinfo.ToString();
                #endregion
                dislist.DataSource = dt;
                dislist.DataBind();
                if (allCount <= pagesize)
                {
                    literalPagination.Visible = false;
                }
                else
                {
                    literalPagination.Visible = true;
                    literalPagination.Text = GenPaginationBar("inproductDetails.aspx?page=[page]&id=" + Hiddencode.Value + "", pagesize, curpage, allCount);
                }

            }
        }
        private void InitParam()
        {
            #region
            sinfo.PageSize = 20;
            sinfo.Orderby = "id";
            if (!string.IsNullOrEmpty(GetQueryString("page")))
            {
                curpage = int.Parse(GetQueryString("page"));
                sinfo.PageIndex = curpage;
            }

            sinfo.Sqlstr = "(SELECT  dbo.sys_historyoperation.ID, CAI,inID,outid, onenumber,code,hstatus,rk.basename AS rkbasename,ck.basename AS ckbasename  ,htype,dbo.sys_historyoperation.inserttime FROM  dbo.sys_historyoperation LEFT JOIN dbo.baseinfo rk ON  RKWHCode=rk.Basecode  LEFT JOIN dbo.baseinfo ck ON  CKWHCode=ck.Basecode LEFT JOIN  dbo.inproductDetails ON  onenumber=ipd_rknumber WHERE ipd_number='" + Hiddencode.Value + "'  ) aa  ";

            #endregion
        }
        private DataSet getData()
        {
            DataSet dt = new DataSet();
            dt = hb.QXGetprodList(sinfo);
            return dt;
        }
    }
}