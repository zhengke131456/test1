using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.products
{
    public partial class addproRebate : Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack){
            bind();}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            panduan();
            string sql = "insert into Rebate (PR_ID,RE_CAD ,RE_1 ,RE_2 , RE_3 , RE_4 , RE_5 , RE_Date) values ('" + dpcode.SelectedItem.Value + "','" + dpcode.SelectedItem.Text + "','" + Convert.ToDouble(GetQueryString("dpFl1") == "" ? "0" : GetQueryString("dpFl1")) + "','" + Convert.ToDouble(GetQueryString("dpFl2") == "" ? "0" : GetQueryString("dpFl2")) + "','" + Convert.ToDouble(GetQueryString("dpFl3") == "" ? "0" : GetQueryString("dpFl3")) + "','" + Convert.ToDouble(GetQueryString("dpFl4") == "" ? "0" : GetQueryString("dpFl4")) + "','" + Convert.ToDouble(GetQueryString("dpFl5") == "" ? "0" : GetQueryString("dpFl5")) + "','" + dpYear.SelectedItem.Text + "')";
            if(hb.insetpro(sql))
            {
                Response.Write("<script type='text/javascript'>alert('添加成功');window.location.href='../products/productRebate.aspx';</script>");
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../products/productRebate.aspx';</script>");
            }
        }

        void bind() 
        {
            dpcode.DataSource = hb.getdate("basename,ID", "baseinfo where type=1");
            dpcode.DataTextField = "basename";
            dpcode.DataValueField = "ID";
            dpcode.DataBind();
            dpcode.Items.Insert(0, "请选择");



            dpYear.DataSource = hb.getdate("Pry_Yary", "productYear where Pry_type=1");
            dpYear.DataTextField = "Pry_Yary";
            dpYear.DataBind();
            dpYear.Items.Insert(0, "请选择");


            
        }

        void panduan() 
        {
            if (dpcode.SelectedItem.Text == "请选择")
            {
                dpcode.SelectedItem.Text = "";
                dpcode.SelectedItem.Value = "请选择";
            }
            if (dpYear.SelectedItem.Text == "请选择")
            {
                dpYear.SelectedItem.Text = "";
                dpYear.SelectedItem.Value = "请选择";
            }
         
        }
    }
}