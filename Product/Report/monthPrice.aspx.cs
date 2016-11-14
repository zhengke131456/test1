using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using ProductBLL.download;

namespace product.Report
{
    public partial class monthPrice : System.Web.UI.Page
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
            }
        }
        protected void btnReport_Click(object sender, EventArgs e)
        {

            string month = dpyary.SelectedValue.ToString().Trim();
            string year = month.Substring(0, 4);
            string sqlstr = "(SELECT RE_CAD ,RE_Date, CONVERT(int, SUM(ISNULL(RE_1,0)))AS RE_1,CONVERT(int,SUM(ISNULL(RE_2,0)))AS RE_2 FROM dbo.Rebate ";
            sqlstr += " WHERE RE_Date='" + month + "' GROUP BY RE_CAD,RE_Date ) Pricerebate ";
                     //sqlstr+= " LEFT JOIN ( SELECT PRSP_CAD,PRSP_Year,SUM(ISNULL(PRSP_WHSPrice,0)) AS PRSP_WHSPrice ";
                     //sqlstr += "  FROM  dbo.ProductPrice WHERE PRSP_Year='" + year + "' GROUP BY PRSP_CAD,PRSP_Year ";

                     sqlstr += " LEFT JOIN ( SELECT PRSP_CAD,PRSP_Year,PRSP_WHSPrice FROM  dbo.ProductPrice WHERE PRSP_Year='2015'AND  id IN (SELECT   MAX(id)AS id  FROM  dbo.ProductPrice WHERE PRSP_Year='" + year + "'  GROUP BY PRSP_CAD,PRSP_Year)";

                     sqlstr+= "  ) pricewh ON  Pricerebate.RE_CAD=pricewh.PRSP_CAD LEFT JOIN dbo.products ON  Pricerebate.RE_CAD=CAD ";
			

					 sqlstr += "	 LEFT JOIN ( SELECT    ROW_NUMBER() OVER ( ORDER BY num ) id , ";
					 sqlstr += "	  XD.CAD , num ,number ,isNull(convert(FLOAT,number),0)/num*100 AS'succeed', ";
					 sqlstr += "	  JHprice , [des] AS modedes FROM   ( SELECT    CAD ,SUM(num) num  FROM dbo.product_PlaceOrde  ";
					 sqlstr += "	GROUP BY  CAD  ) XD LEFT JOIN ( SELECT  CAD ,SUM(number) number FROM    dbo.product_ActualOrder ";
					 sqlstr += "	   GROUP BY CAD) SD ON SD.CAD = XD.CAD  LEFT JOIN dbo.products ON XD.CAD = dbo.products.CAD  ";
					 sqlstr += "        LEFT JOIN ( SELECT  CAD , MAX(DHdate) PDHdate ,MAX(Purchaseprice) JHprice FROM    dbo.product_ActualOrder  ";
					 sqlstr += "    GROUP BY CAD) price ON XD.CAD = price.CAD ) MM ON MM.CAD = pricerebate.RE_CAD  ";
					 dt = hb.getdate(" RE_CAD as CAD,[des],CONVERT(int,PRSP_WHSPrice )as 批发价格,RE_1 as 返利1,RE_2  as 返利2, '' as  price, num as 下单 , number as 实到 ,CONVERT(VARCHAR(10),CONVERT(DECIMAL(18,2),succeed))+'%' 成功率, CONVERT(DECIMAL(18,2),succeed) as succeed ", sqlstr);

            //dt.Columns.Add("price");

            if (dt.Rows.Count > 0)
            {
                decimal wprice = 0, back1 = 0, back2 = 0;
                decimal price = 0;
                #region 计算价格


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    wprice = Convert.ToInt32(dt.Rows[i]["批发价格"].ToString() == "" ? "0" : dt.Rows[i]["批发价格"].ToString());
                    back1 = Convert.ToInt32(dt.Rows[i]["返利1"].ToString());
                    back2 = Convert.ToInt32(dt.Rows[i]["返利2"].ToString());

                    if (back2 != 0)
                    {
                        price = wprice - back1 - back2-10;

                    }
                    else
                    {
                        if (wprice < 600)
                        {
                            price = wprice - back1 - 20;
                        }
                        if (wprice >= 600 && wprice < 900)
                        {
                            price = wprice - back1 - 40;
                        }
                        if (wprice >= 900 && wprice < 1500)
                        {
                            price = wprice - back1 - 60;
                        }
                        if (wprice >= 1500)
                        {
                            price = wprice - back1 - 100;
                        }
                    }
                    dt.Rows[i]["price"] = price;
                }
                #endregion

            }
            else
            {
                Response.Write("<script type='text/javascript'>window.parent.alert('没有该月份数据请核实！')</script>");
                return;
            }
			//string path = Server.MapPath("~/downloadxlsx/") + "Price" + DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".xls";

			string path ="Price" + DateTime.Now.ToString("yyyy/MM/dd").Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".xls";

			if (downloadfile(dt, path, "xls"))
            {
              
                Response.Write("<script type='text/javascript'>window.parent.alert('导Execl出成功!')</script>");

                return;
            }
            else
            {
                Response.Write("<script type='text/javascript'>window.parent.alert('导出Execl失败!');window.location.href='../products/PriceRebateExecl.aspx';</script>");

                return;
            }
        }
        /// <summary>
        /// 浏览器下载
        /// </summary>
        /// <param name="s_path"></param>
        protected  bool  downloadfile(DataTable dt,string s_path,string pathtype)
        {


			bool result = true;
            HttpContext.Current.Response.ContentType = "application/ms-download";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Current.Response.Charset = "utf-8";
			HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(s_path, System.Text.Encoding.UTF8));
           
			HttpContext.Current.Response.BinaryWrite(ExcelHelper.TableToExcel(dt, pathtype).GetBuffer());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            //下载完成后删除服务器下生成的文件
           
            HttpContext.Current.Response.End();
			return result;
        }
        private void bind()
        {

            dpyary.DataSource = hb.getdate(" DISTINCT Pry_Yary", "dbo.productYear WHERE Pry_type=0");
            dpyary.DataTextField = "Pry_Yary";
            dpyary.DataBind();
            dpyary.Items.Insert(0, "请选择月份");

        }
    }
}