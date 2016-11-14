using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;

namespace product.products
{
    public partial class Transferslip_T : Common.BasePage
    {
        protected StringBuilder result;
        protected string outwh = "", rpcode = "", inwh = "";
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        //StringBuilder sqlpin = new StringBuilder();
        StringBuilder sqlCad = new StringBuilder();//查看编码是否重复

        //当前用户角色
        protected string spcode = "";

        //
        protected void getvalue()
        {
            if (GetQueryString("inwh") != "")
            {
                inwh = GetQueryString("inwh");
            }
            if (GetQueryString("rpcode") != "")
            {
                rpcode = GetQueryString("rpcode");
            }
            if (GetQueryString("outwh") != "")
            {
                outwh = GetQueryString("outwh");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                try
                {
                    getvalue();
                    InitParam("");
                    bindData();
                }
                catch { }
            }
        }

        private void bindData()
        {
            DataTable rpcodedb = hb.getdate("rpcode,rpcode+'<>'+CAD AS rpname", "dbo.products");

            result = new StringBuilder();
            result.Append("[");
            for (int i = 0; i < rpcodedb.Rows.Count; i++)
            {
                result.Append("{ value: \"" + rpcodedb.Rows[i]["rpcode"].ToString() + "\",label:\" " + rpcodedb.Rows[i]["rpname"].ToString() + "\"},");
            }
            result.Remove(result.Length - 1, 1);//把最后一个，给移除


            result.Append(" ]");



            string ispat = isPartcode();
            DataSet ds = getData();

            if (!ds.Equals(null))
            {
                allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                lblCount.Text = allCount.ToString();

                DataTable dt = ds.Tables[1];
                dt.Columns.Add("iswh");//0,1,2



                DataTable dbstore = hb.getdate("SR_storecode", "dbo.SYS_RightStore WHERE SP_code='" + ispat + "'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["states"].ToString() == "待审核")
                    {
                        dt.Rows[i]["iswh"] = "0";//
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
                    literalPagination.Text = GenPaginationBar("Transferslip_T.aspx?page=[page]&outwh=" + outwh + "&rpcode=" + rpcode + "&inwh=" + inwh + "", pagesize, curpage, allCount);
                }
            }
        }


        private void InitParam(string Index)
        {

            string partcode = isPartcode();
            #region
            sinfo.PageSize = 20;
            //sinfo.Tablename = "products"; 
            // sinfo.Orderby = "id";
            sinfo.Sqlstr = "(SELECT dbo.products.ShGoogcode,dbo.products.CAD,dbo.transferslip.id,dbo.transferslip.rpcode,QTY,states,rk.basename AS inWHName ,ck.basename AS outwhName,outwhtime,inwhtime,dbo.transferslip.opcode, outOpcode,inOpcode,dbo.transferslip.inserttime ,dbo.transferslip.inWH,dbo.transferslip.outwh,Dimension,PATTERN  FROM  transferslip  LEFT JOIN dbo.BaseStore rk ON  rk.Basecode=inWH   LEFT JOIN dbo.BaseStore ck ON  ck.Basecode=outwh  LEFT JOIN dbo.products ON dbo.products.rpcode=dbo.transferslip.rpcode )hh where ";


            sinfo.Sqlstr += "( EXISTS ( SELECT   SR_storecode FROM     dbo.SYS_RightStore rk  WHERE    inWH = rk.SR_storecode AND SP_code = '" + partcode + "' ) OR EXISTS ( SELECT    SR_storecode FROM      dbo.SYS_RightStore ck   WHERE     outwh = ck.SR_storecode AND SP_code = '" + partcode + "' ) )";

            if (rpcode != "")
            {
                sinfo.Sqlstr += "  and  rpcode  LIKE'%" + rpcode + "%' ";
            }
            else
            {
                sinfo.Sqlstr += " and 1=1 ";
            }
            if (outwh != "")
            {
                sinfo.Sqlstr += "  and outwhName  ='" + outwh + "' ";
            }
            if (inwh != "")
            {
                sinfo.Sqlstr += "  and inWHName  ='" + inwh + "' ";
            }

            //待审核
            sinfo.Sqlstr += "  and states  ='待审核' ";

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

        //查询
        protected void btnQuerey_Click(object sender, EventArgs e)
        {
            // panduan();
            getvalue();
            try
            {
                InitParam("query");
                bindData();
            }
            catch { }
        }

    }
}