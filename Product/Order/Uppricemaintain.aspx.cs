using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace product.Order
{
    public partial class Uppricemaintain : Common.BasePage
    {
        protected string  isshangxian = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region MyRegion
                
        
                if (!string.IsNullOrEmpty(GetQueryString("rpcode")))
                {
                    rpcode.Value = GetQueryString("rpcode").ToString().Trim();
                }
                if (!string.IsNullOrEmpty(GetQueryString("pid")))
                {
                    hiddpid.Value = GetQueryString("pid").ToString().Trim();
                }
                if (!string.IsNullOrEmpty(GetQueryString("cityname")))
                {

                    citycode.Value = GetQueryString("cityname").ToString().Trim();
                }
                if (!string.IsNullOrEmpty(GetQueryString("city")))
                {

                    hiddcity.Value = GetQueryString("city").ToString().Trim();
                }
                
                if (!string.IsNullOrEmpty(GetQueryString("jj")))
                {
                    xianjia.Value = GetQueryString("jj").ToString().Trim();
                }
                if (!string.IsNullOrEmpty(GetQueryString("yj")))
                {
                    yuanjia.Value = GetQueryString("yj").ToString().Trim();
                }
                if (!string.IsNullOrEmpty(GetQueryString("sx")))
                {
                    if (GetQueryString("sx") == "1")
                    {
                        ischeck.Checked = true;
                    }

                }
                else
                {
                    ischeck.Checked = false;
                }
                #endregion
            }

        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {

            if (ischeck.Checked)
            {
                isshangxian = "1";
            }
            string sql = "";

            if (hiddpid.Value != "0")
            {
                sql = "UPDATE dbo.PriceMaintain SET  PriceJinhuo='" + xianjia.Value + "',PriceXiaoshou='" + yuanjia.Value + "',isshangxian='" + isshangxian + "'WHERE CAD='" + rpcode.Value + "'  AND citycode='" + hiddcity.Value + "' ";
               
            }
            else
            {
                sql = "   INSERT INTO dbo.PriceMaintain( citycode  ,cityname,  CAD ,PriceJinhuo ,";
                sql += " PriceXiaoshou , inserttime , opname ,isshangxian  )";
                sql += "VALUES  ( '" + hiddcity.Value + "','" + citycode.Value + "','" + rpcode.Value + "',";
                sql += "'" + xianjia.Value + "','" + yuanjia.Value+ "',GETDATE()";
                sql += ",'"+userCode()+"','"+isshangxian+"'  )";
                
            }
            if (hb.insetpro(sql) )
            {
                Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../Order/priceMaintainNew.aspx?cad=" + rpcode.Value + "';</script>");
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('修改失败');</script>");
            }

        }
    }
}