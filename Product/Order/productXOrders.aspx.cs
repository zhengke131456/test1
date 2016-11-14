using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using productcommon;

namespace product.Order
{

    public partial class productXOrders : System.Web.UI.Page
    {

        protected string cad = "", bdate = "", edate = "", txtcity = "",top="",sqlsql="";
        protected int curpage = 1, pagesize = 50, allCount = 0, allCountSD=0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected Common.BasePage BasePage = new Common.BasePage();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getRequest();

                try
                {

                    sqlsql=  InitParam("");
                    bindData();
                }
                catch { }
            }
        }

        private void getRequest()
        {
            #region 获取参数
            if (Request.Form["QCAD"] != "" && Request.Form["QCAD"] != null)
            {
                cad = Request.Form["QCAD"];
            }
            else if (Request.QueryString["QCAD"] != null)
            {
                cad = Request.QueryString["QCAD"];
            }

            if (Request.Form["bdata"] != "" && Request.Form["bdata"] != null) 
            {
                bdate = Request.Form["bdata"];
            }
            else if (Request.QueryString["bdata"] != null)
            {
                bdate = Request.QueryString["bdata"];
            }

            if (Request.Form["edata"] != "" && Request.Form["edata"] != null)
            {
                edate = Request.Form["edata"];
            }
            else if (Request.QueryString["edata"] != null)
            {
                edate = Request.QueryString["edata"];
            }
            if (Request.Form["TxtCity"] != "" && Request.Form["TxtCity"] != null)
            {
                txtcity = Request.Form["TxtCity"];
            }
            else if (Request.QueryString["TxtCity"] != null)
            {
                txtcity = Request.QueryString["TxtCity"];
            }
           
           
           
            #endregion
        }
        private void bindData()
        {


            DataSet ds = hb.GetDataSet(sqlsql);
          

            if (!ds.Equals(null))
            {
                allCount = ds.Tables[0].Rows.Count;
                lblCount.Text = allCount.ToString();

                allCountSD = ds.Tables[1].Rows.Count;
                lblCountSD.Text = allCountSD.ToString();

                if (allCount > 0)
                {
                    DataTable dd = ds.Tables[0]; 
                    dislist.DataSource = ds.Tables[0];
                    dislist.DataBind();

                }
                if (allCountSD > 0)
                {
                    RepeaterSd.DataSource = ds.Tables[1];
                    RepeaterSd.DataBind();
                }
                
                if (allCount <= pagesize)
                {
                    literalPagination.Visible = false;
                }
                else
                {
                    literalPagination.Visible = true;
                    literalPagination.Text = BasePage.GenPaginationBar("productXOrders.aspx?page=[page]&QCAD=" + cad + "&bdata=" + bdate + "&edata=" + edate + "&TxtCity=" + txtcity + "", pagesize, curpage, allCount);
                } 
            }
            

        }

        
        private  string  InitParam(string Index)
        {

            if (Index == "")
            {
                #region

                if (!string.IsNullOrEmpty(Request.QueryString["page"]))
                {
                    curpage = int.Parse(Request.QueryString["page"]);
                    sinfo.PageIndex = curpage;
                }
                #endregion
            }
            else
            {

                sinfo.PageIndex = 1;
            }

            int tempstar = (curpage - 1) * pagesize;
            int tempend = curpage * pagesize + 1;
               string sqlwhere  = "";
               string sqlstrxd = "";
              
               if (bdate != "")
               {
                   sqlwhere += " and  CONVERT(VARCHAR(7),orderdate,23 )>='" + bdate + "' AND  CONVERT(VARCHAR(7),orderdate,23 )<='" + edate + "'  ";
               }
               if (cad != "")
               {
                   sqlwhere += " and  CAD='" + cad + "'";
               }


               sqlstrxd = " SELECT    * FROM  ( select ROW_NUMBER() OVER ( ORDER BY ID DESC ) AS rn, * from    dbo.product_PlaceOrde  where 1=1 ";
 
               
                sqlstrxd += sqlwhere;
                if (txtcity != "")
                {
                    sqlstrxd += " and KY LIKE'%" + txtcity.Trim() + "%' ";
                }

                sqlstrxd += " )mm where (rn > " + tempstar + " and rn< " + tempend + ")";

                sqlstrxd += " SELECT    * FROM  ( select ROW_NUMBER() OVER ( ORDER BY ID DESC ) AS rn, * from    dbo.product_ActualOrder  where 1=1 ";
                sqlstrxd += sqlwhere;
                if (txtcity != "")
                {
                    sqlstrxd += " and City ='" + txtcity.Trim() + "' ";
                }

                sqlstrxd += " )HH where (rn > " + tempstar + " and rn< " + tempend + ")";


                return sqlstrxd;


        }



        /// <summary>
        /// 信息
        /// </summary>
        /// <returns></returns>
        
        protected void btnQuerey_Click(object sender, EventArgs e)
        {

            getRequest();

            try
            {
                sqlsql= InitParam("query");
                bindData();
            }
            catch { }
        }
       
    }
}