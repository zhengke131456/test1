using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.products
{
    public partial class updateProPrice : Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = GetQueryString("id");
            if (!IsPostBack)
            {
                bind();
                DataTable dt = hb.getdate("*", "ProductPrice where ID=" + id);
                if(dt.Rows.Count>0)
                {
                    dpcode.SelectedItem.Text = dt.Rows[0]["PRSP_CAD"].ToString();
                  dpcode.SelectedItem.Value = dt.Rows[0]["PR_ID"].ToString();
                  TexPF.Text = dt.Rows[0]["PRSP_WHSPrice"].ToString();
                   Texjh.Text= dt.Rows[0]["PRSP_Inprice"].ToString();
                 dpYear.SelectedValue = dt.Rows[0]["PRSP_Year"].ToString();
                
                }
            }
        }

        void bind()
        {
            //编码
            dpcode.DataSource = hb.getdate("basename,ID", "baseinfo where type=1");
            dpcode.DataTextField = "basename";
            dpcode.DataValueField = "ID";
            dpcode.DataBind();
            dpcode.Items.Insert(0, "请选择");



            //年份
            dpYear.DataSource = hb.getdate("Pry_Yary", "productYear where Pry_type=1");
            dpYear.DataTextField = "Pry_Yary";
            dpYear.DataBind();
            dpYear.Items.Insert(0, "请选择");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            panduan();
            string images = "PRSP_CAD='" + dpcode.SelectedItem.Text + "',PRSP_WHSPrice='" + TexPF.Text + "',PRSP_Inprice='" + Texjh.Text + "',PRSP_Year='" + dpYear.SelectedItem.Text + "',PR_ID='" + dpcode.SelectedItem.Value + "'";
            string strwhere= "ID='"+id+"'";
            if (hb.updateWhereID("ProductPrice", strwhere, images))
            {
                Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../products/productPrice.aspx';</script>");
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../products/productPrice.aspx';</script>");
            }
        }

        void panduan()
        {
            if (dpcode.SelectedItem.Text == "请选择")
            {
                dpcode.SelectedItem.Text = "";
            }
            if (dpYear.SelectedItem.Text == "请选择")
            {
                dpYear.SelectedItem.Text = "";
            }
        }
    }
}