using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Collections;
using System.Configuration;

namespace product.products
{
    public partial class ProductOutStore : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
     
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        StringBuilder sqlpin = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                hiddtype.Value = GetQueryString("CAI").Trim();
                hidstockID.Value = GetQueryString("storeID").Trim();
                //hiddDistrict.Value = GetQueryString("name").Trim();//区域
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
                dislist.DataSource = ds.Tables[1];
                dislist.DataBind();
                if (allCount <= pagesize)
                {
                    literalPagination.Visible = false;
                }
                else
                {
                    literalPagination.Visible = true;

                    literalPagination.Text = GenPaginationBar("ProductOutStore.aspx?page=[page]&CAI=" + hiddtype.Value + "&storeID=" + hidstockID.Value + "", pagesize, curpage, allCount);
                }
            }
        }


        private void InitParam()
        {
            #region
            sinfo.PageSize = 20;
            // sinfo.Tablename = "outproduct";
            sinfo.Orderby = "id";
            #region

            if (!string.IsNullOrEmpty(GetQueryString("page")))
            {
                curpage = int.Parse(GetQueryString("page"));
                sinfo.PageIndex = curpage;
            }
            #endregion
           
            //if (hiddDistrict.Value == "")
            //{
            //    sinfo.Sqlstr = "CAI='" + hiddtype.Value + "'";
            //}
            //else
            //{
            //    sinfo.Sqlstr = "CAI='" + hiddtype.Value + "' and WH IN (SELECT basename FROM  dbo.baseinfo WHERE  [type]=4 AND area='" + hiddDistrict.Value + "')";
            //}
			sinfo.Sqlstr = "(SELECT dbo.outproduct.id,dbo.outproduct.rpcode, dbo.products.ShGoogcode,dbo.products.CAD,OD,QTY,WH,WHcode,usercode,spmark,TSNote,basename ,insettime FROM  dbo.outproduct LEFT JOIN  dbo.BaseStore ON WH=dbo.BaseStore.id LEFT JOIN dbo.products ON dbo.outproduct.rpcode =dbo.products.rpcode WHERE  dbo.outproduct.rpcode='" + hiddtype.Value + "' AND WH='" + hidstockID.Value + "')aa";
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