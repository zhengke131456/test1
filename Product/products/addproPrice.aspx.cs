using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.products
{
    public partial class addproPrice : Common.BasePage
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


            string sql = "insert into ProductPrice (PR_ID,PRSP_CAD ,PRSP_WHSPrice ,PRSP_Inprice ,PRSP_Year ) values ('" + dpcode.SelectedItem.Value + "','" + dpcode.SelectedItem.Text + "','" + Convert.ToDouble(GetQueryString("DTPFPrice") == "" ? "0" : GetQueryString("DTPFPrice")) + "','" + Convert.ToDouble(GetQueryString("DTJHPrice") == "" ? "0" : GetQueryString("DTJHPrice")) + "','" + dpYear.SelectedItem.Text + "')";
            if(hb.insetpro(sql))
            {
                Response.Write("<script type='text/javascript'>alert('添加成功');window.location.href='../products/productPrice.aspx';</script>");
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../products/productPrice.aspx';</script>");
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