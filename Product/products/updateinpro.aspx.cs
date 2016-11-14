using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;

namespace product.products
{
	public partial class updateinpro : Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		string id ;
      
        protected void Page_Load(object sender, EventArgs e)
        {
             id = GetQueryString("id");
            if(!IsPostBack)
            {

				DataTable dt = hb.getdate("*", " (SELECT  dbo.inproduct.id,rpcode,OD,QTY,basename,spmark FROM  dbo.inproduct LEFT JOIN dbo.BaseStore   ON   WHCode= Basecode  WHERE   dbo.inproduct.id='" + id + "')hh");
               if (dt.Rows.Count > 0)
               {
				   
				   

				   rpcode.Value = dt.Rows[0]["rpcode"].ToString();
				   TQTY.Value = dt.Rows[0]["QTY"].ToString();
				   dpwh.Value = dt.Rows[0]["basename"].ToString();
				  
               }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
		

		      string num=	 GetQueryString("TQTY");
             if (hb.ProExecinset("exec proUpinproduct '"+id+"' ,'"+num+"','in','"+userCode()+"'"))
            {
                Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../products/inproductList.aspx';</script>");
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../products/inproductList.aspx';</script>");
            }

        }

     
   
    }
}