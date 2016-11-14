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

    public partial class productSDOrders : System.Web.UI.Page
    {

        protected string cad = "", bdate = "", edate = "", txtcity = "",top="";
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected Common.BasePage BasePage = new Common.BasePage();
        StringBuilder sqlstr = new StringBuilder();
        StringBuilder sqlsql = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getRequest();

                try
                {

                    InitParam("");
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

            DataSet ds = getData();

            if (!ds.Equals(null))
            {
                allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                lblCount.Text = allCount.ToString();

                DataTable dt = ds.Tables[1];
                dt.Columns.Add("show");


                dislist.DataSource = ds.Tables[1];
                dislist.DataBind();
                if (allCount <= pagesize)
                {
                    literalPagination.Visible = false;
                }
                else
                {
                    literalPagination.Visible = true;
                    literalPagination.Text = BasePage.GenPaginationBar("productSDOrders.aspx?page=[page]&QCAD=" + cad + "&bdata=" + bdate + "&edata=" + edate + "&TxtCity=" + txtcity + "", pagesize, curpage, allCount);
                } 
            }
        }

        
        private void InitParam(string Index)
        {
          
                sinfo.PageSize = 20;
                sinfo.Sqlstr = "(SELECT * FROM dbo.product_ActualOrder  where 1=1 ";
              
                if (bdate != "")
                {
                    
                    sinfo.Sqlstr += " and  CONVERT(VARCHAR(7),orderdate,23 )>='" + bdate + "' AND  CONVERT(VARCHAR(7),orderdate,23 )<='" + edate + "'  ";

                }
                if (cad != "")
                {
                    sinfo.Sqlstr += " and  CAD='" + cad + "'";
                }
                if (txtcity != "")
                {
                    sinfo.Sqlstr += " and City LIKE'%" + txtcity.Trim() + "%' ";
                }
               
                sinfo.Sqlstr += ")MM ";
          
        
            
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

            getRequest();

            try
            {
                InitParam("query");
                bindData();
            }
            catch { }
        }
       
    }
}