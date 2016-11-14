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
    public partial class ProductInStore : Common.BasePage
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
               
               // hiddpart.Value = ispartRights();
                Hiddentype.Value = GetQueryString("CAI").Trim();
                hidstockID.Value = GetQueryString("storeID").Trim();
                //hiddDistrict.Value = isDistrictRights();
                //part = ispartRights();
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
             
                dt.Columns.Add("markName");
                IDictionary dictmark = (IDictionary)ConfigurationManager.GetSection("spmark");
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    for (int j = 1; j <= dictmark.Count; j++)
                    {
                        string type = "type" + j;
                        if (dt.Rows[i]["spmark"].ToString() == j.ToString())
                        {
                            dt.Rows[i]["markName"] = dictmark[type].ToString();
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
                    //literalPagination.Text = GenPaginationBar("ProductInStore.aspx?page=[page]&CAI=" + Hiddentype.Value + "&bdate=" + Bdate + "&edate=" + Edate + "&name=" + hiddDistrict.Value + "", pagesize, curpage, allCount);
                    literalPagination.Text = GenPaginationBar("ProductInStore.aspx?page=[page]", pagesize, curpage, allCount);
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

				sinfo.Sqlstr = "(SELECT dbo.inproduct.id,dbo.inproduct.rpcode,OD,FHDate,SHDate,usercode,THData,QTY,WHCode,spmark,inprice,basename AS 'WHName' ,dbo.products.ShGoogcode,dbo.products.CAD,note FROM  dbo.inproduct LEFT JOIN  dbo.BaseStore ON WH=dbo.BaseStore.id LEFT JOIN dbo.products ON dbo.inproduct.rpcode =dbo.products.rpcode WHERE  dbo.inproduct.rpcode='" + Hiddentype.Value + "' AND WH='" + hidstockID.Value + "')aa";



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