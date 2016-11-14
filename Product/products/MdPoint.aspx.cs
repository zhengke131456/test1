using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.products
{
    public partial class MdPoint: Common.BasePage
    {
        protected string id = "", table = "", name = "", address = "",type="";
        protected string px1 = "", py1 = "", isPoint = "0";

        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

        protected string px = "", py = "";//默认坐标

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                id = GetQueryString("id");
                table = GetQueryString("tablename");

                if (!string.IsNullOrEmpty(id))
                {
                    DataTable dt = hb.GetDataSet(" select * from " + table + " where id = "+id+"").Tables[0];
                    
                    if (dt.Rows.Count > 0)
                    {
                        id = dt.Rows[0]["id"].ToString();

                        string citycode = "";
                        if (table.ToLower() == "basestore")
                        {
                            //城市 编码
                            citycode = dt.Rows[0]["citycode"].ToString();
                            name = dt.Rows[0]["basename"].ToString();
                        }
                        if (table.ToLower() == "city")
                        {
                            name = dt.Rows[0]["cityname"].ToString();
                        }
                        
                        px1 = dt.Rows[0]["px"].ToString();
                        py1 = dt.Rows[0]["py"].ToString();

                        //已有坐标
                        if (px1 != "" && px1 != "null" && py1 != "" && py1 != "null" && px1 != "0" && py1 != "0")
                        {
                            isPoint = "1";
                            px = px1;
                            py = py1;
                        }
                            //默认 坐标
                        else
                        {
                            // 油站取城市
                            if (table.ToLower() == "basestore")
                            {
                                DataTable dcity = hb.GetDataSet(" select * from city  where AreaCode = '" + citycode + "'").Tables[0];

                                px = dcity.Rows[0]["px"].ToString();
                                py = dcity.Rows[0]["py"].ToString();
                            }
                            //  城市取北京
                            if (table.ToLower() == "city")
                            {
                                name = dt.Rows[0]["cityname"].ToString();
                                px = "116.406151";
                                py = "39.91401";
                            }

                        }
                    }
                }
            }
            catch { }
        }
    }
}
