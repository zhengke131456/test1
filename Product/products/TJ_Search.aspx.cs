using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace product.products
{
    public partial class TJ_Search : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected string rpcode = "", Edate, Bdate;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        StringBuilder sqlpin = new StringBuilder();
        StringBuilder sqlinID = new StringBuilder();

        protected string desmess = "";

        protected string userCode = ""; //角色

        protected StringBuilder result = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            //是否首次
            //if (!IsPostBack)
            //{
                try
                {
                    getvalue();

                    //无Rpcode 不做动作
                    if (rpcode == "")
                    { return; }

                    InitParam("");
                }
                catch { }
            //}

        }

        private void getvalue()
        {
            userCode = isPartcode();//角色 


            if (!string.IsNullOrEmpty(GetQueryString("rpcode")))
            {
                rpcode = GetQueryString("rpcode");

                //是CAD，还是睿配编码
                //产品属性
                DataTable dt_info = hb.GetDataSet("select *  from products where rpcode ='" + rpcode + "' or cad = '" + rpcode + "'").Tables[0];
                if (dt_info.Rows.Count > 0)
                {
                    desmess ="CAD:"+dt_info.Rows[0]["CAD"].ToString() + " 型号：" + dt_info.Rows[0]["dimension"].ToString() + " 花纹：" + dt_info.Rows[0]["pattern"].ToString() + " " + dt_info.Rows[0]["mark"].ToString();
                    rpcode = dt_info.Rows[0]["rpcode"].ToString();
                }
                else
                {
                    return;
                }

            }
            else rpcode = "";

           
        }

        /// <summary>
        /// 如果是点击查询功能首次要把PageIndex 重置为1
        /// </summary>
        private void InitParam(string Index)
        {

            //仓库
            DataSet temp_stock = hb.GetDataSet("select basename ,basecode from basestore ");

            #region 一级仓库

            string sqltr = "select stockcode,SUM(stockNum)as num1  ,SUM(stockjtNum)as num2 "+
" from stock_storck where stockcode in (select sr_storecode from dbo.SYS_RightStore where SP_code='" + userCode + "')   and rpcode='" + rpcode + "'  " +
 " and  stockcode in (select basecode from BaseStore where nodeLevel=1) group by stockcode order by num1 desc";
            
            //数量
            DataSet temp_mes = hb.GetDataSet(sqltr);
            DataTable dt = temp_mes.Tables[0];

            dt.Columns.Add("basename");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow[] drr = temp_stock.Tables[0].Select(" basecode = '" + dt.Rows[i]["stockcode"].ToString() + "'");
                if (drr.Length > 0)
                {
                    dt.Rows[i]["basename"] = drr[0]["basename"].ToString();
                }
            }

            List1.DataSource = dt;
            List1.DataBind();

            #endregion



            #region 二级仓库

            //全部二级仓库
            string sqlc2 = " select * from dbo.BaseStore where nodeLevel=2 ";
            DataTable temp_stock2 = hb.GetDataSet(sqlc2).Tables[0];

            //全部二级仓库油站数
            string sqlcy2 = " select  COUNT(1) as num,fcode from BaseStore where nodeLevel=3 group by Fcode  ";
            DataTable temp_stocky2 = hb.GetDataSet(sqlcy2).Tables[0];


            //服务中心轮胎数量
            string sqltr2 = "select stockcode,SUM(stockNum)as num1  ,SUM(stockjtNum)as num2 " +
" from stock_storck where stockcode in (select sr_storecode from dbo.SYS_RightStore where SP_code='" + userCode + "')   and rpcode='" + rpcode + "'  " +
" and  stockcode in (select basecode from BaseStore where nodeLevel=2) group by stockcode order by num1 desc";

            DataSet temp_mes2 = hb.GetDataSet(sqltr2);
            DataTable dt2 = temp_mes2.Tables[0];


            //油站轮胎数
            string sqltr3 = " select fcode,SUM(stocknum) as num1 ,SUM(stockjtNum) as num2 from ( " +
 " select a.basecode,a.Fcode,b.* from BaseStore a,stock_storck b where a.Basecode = b.stockcode and a.nodeLevel='3'"+
  " and b.rpcode='" + rpcode + "' and b.stockcode in (select sr_storecode from dbo.SYS_RightStore where SP_code='" + userCode + "') )  " +
  " as b group by fcode order by num1 desc";

            DataSet temp_mes3 = hb.GetDataSet(sqltr3);
            DataTable dt3 = temp_mes3.Tables[0];

            //拼显示数据
            temp_stock2.Columns.Add("StockNum");//服务中心油站数

            temp_stock2.Columns.Add("fNum");//服务中心库存
            temp_stock2.Columns.Add("fJtNum");//服务中心计提库存

            temp_stock2.Columns.Add("fYNum");//服务中心油站总库存
            temp_stock2.Columns.Add("fYJtNum");//服务中心油站总计提库存

            for (int i = 0; i < temp_stock2.Rows.Count; i++)
            {
                temp_stock2.Rows[i]["basename"] = temp_stock2.Rows[i]["basename"].ToString().Replace("睿配服务中心", "");

                //服务中心库存
                DataRow[] dr2 = dt2.Select(" stockcode = '" + temp_stock2.Rows[i]["basecode"].ToString() + "'");
                if (dr2.Length > 0)
                {
                    temp_stock2.Rows[i]["fNum"] = dr2[0]["num1"].ToString();
                    temp_stock2.Rows[i]["fJTNum"] = dr2[0]["num2"].ToString();
                }
                else
                {
                    temp_stock2.Rows[i]["fNum"] = 0;
                    temp_stock2.Rows[i]["fJTNum"] = 0;
                }

                //服务中心油站数
                DataRow[] dry = temp_stocky2.Select(" fcode = '" + temp_stock2.Rows[i]["basecode"].ToString() + "'");
                if (dry.Length > 0)
                {
                    temp_stock2.Rows[i]["StockNum"] = dry[0]["num"].ToString();
                }
                else
                {
                    temp_stock2.Rows[i]["StockNum"] = 0;
                }

                //油站库存统计
                DataRow[] dr3 = dt3.Select(" fcode = '" + temp_stock2.Rows[i]["basecode"].ToString() + "'");
                if (dr3.Length > 0)
                {
                    temp_stock2.Rows[i]["fYNum"] = dr3[0]["num1"].ToString();
                    temp_stock2.Rows[i]["fYJtNum"] = dr3[0]["num2"].ToString();
                }
                else
                {
                    temp_stock2.Rows[i]["fYNum"] = 0;
                    temp_stock2.Rows[i]["fYJtNum"] = 0;
                }

            }


            list2.DataSource = temp_stock2;
            list2.DataBind();

            #endregion

        }

    }
}