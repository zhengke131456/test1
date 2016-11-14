using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.products
{
    public partial class updateoutpro :Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        string id;
         protected void Page_Load(object sender, EventArgs e)
        {
            id = GetQueryString("id");
            if (!IsPostBack)
            {

				DataTable dt = hb.getdate("*", "(SELECT  dbo.outproduct.id,rpcode,OD,QTY,basename FROM  dbo.outproduct LEFT JOIN dbo.BaseStore   ON   WHCode= Basecode  WHERE   dbo.outproduct.id='" + id + "')hh");
                if (dt.Rows.Count > 0)
                {
                    SeriaDate.Value = Convert.ToDateTime(dt.Rows[0]["OD"]).ToShortDateString();
					rpcode.Value = dt.Rows[0]["rpcode"].ToString();

					dpnum.Value = dt.Rows[0]["QTY"].ToString();
					wh.Value = dt.Rows[0]["basename"].ToString();
                   
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

			string num = GetQueryString("dpnum");
			 if (hb.ProExecinset("exec proUpinproduct '" + id + "' ,'" + num + "','out','"+userCode()+"'"))
            {
                Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../products/outproductList.aspx';</script>");
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../products/outproductList.aspx';</script>");
            }
        }
   
    }
}