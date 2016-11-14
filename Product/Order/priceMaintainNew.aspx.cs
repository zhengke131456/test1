using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.Order
{
    public partial class priceMaintainNew : Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected int pagesize = 20;
       
      
        protected string resultstr, cad;
        protected void Page_Load(object sender, EventArgs e)
        {




            cad = GetQueryString("cad");//cad

          

            #region MyRegion






            string strsql = "    SELECT  ROW_NUMBER()  OVER(order by price.CAD) AS cid,SrCity.rpcode,";
            strsql += "   SrCity.cityname,SrCity.AreaCode,SrCity.spcode,ISNULL(price.PID,0)AS pid, ";
            strsql += "   price.PriceJinhuo,price.PriceXiaoshou,price.isshangxian ,";
            strsql += "    (CASE WHEN price.isshangxian='1' THEN'是' ELSE '否'END) AS shagnxian ";
            strsql += "FROM (SELECT    '" + cad + "' AS rpcode ,  cityname ,AreaCode ,spcode ";
            strsql += " FROM   dbo.City INNER JOIN dbo.SYS_RightNew ON AreaCode = Srcode ";
            strsql += "       WHERE     spcode = '"+isPartcode()+"') SrCity  LEFT JOIN  ";
            strsql += " ( SELECT  CAD,id AS PID, PriceJinhuo , PriceXiaoshou ,isshangxian, citycode  ";
            strsql += "   FROM    dbo.PriceMaintain WHERE CAD='" + cad + "' ) price  ";
            strsql += "  ON price.CAD=SrCity.rpcode  AND  price.citycode=SrCity.AreaCode ";


            DataTable dvjson = hb.getProdatable(strsql);//下单明细

            dislist.DataSource = dvjson;
            dislist.DataBind();




         


            #endregion

        }
        
    }
}