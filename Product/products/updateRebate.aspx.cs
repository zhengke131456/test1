using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.products
{
    public partial class updateRebate : Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = GetQueryString("id");
            if (!IsPostBack)
            {
                bind();
                DataTable dt = hb.getdate("*", "Rebate where ID=" + id);
                if(dt.Rows.Count>0)
                {
                    dpcode.SelectedItem.Text= dt.Rows[0]["RE_CAD"].ToString();
                    dpcode.SelectedItem.Value = dt.Rows[0]["PR_ID"].ToString();
                    Texfl1.Text = dt.Rows[0]["RE_1"].ToString();
                    Texfl2.Text = dt.Rows[0]["RE_2"].ToString();
                    Texfl3.Text = dt.Rows[0]["RE_3"].ToString();
                    Texfl4.Text = dt.Rows[0]["RE_4"].ToString();
                    Texfl5.Text = dt.Rows[0]["RE_5"].ToString();
                    dpYear.SelectedValue = dt.Rows[0]["RE_Date"].ToString();
                
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



            //日期
            dpYear.DataSource = hb.getdate("Pry_Yary", "productYear where Pry_type=0");
            dpYear.DataTextField = "Pry_Yary";
         
            dpYear.DataBind();
            dpYear.Items.Insert(0, "请选择");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            panduan();
            string images = "RE_CAD='" + dpcode.SelectedItem.Text+ "',RE_1='" + Texfl1.Text + "',RE_2='" + Texfl2.Text + "',RE_3='" + Texfl3.Text + "',RE_4='" + Texfl4.Text + "',RE_5='" + Texfl5.Text + "',RE_Date='" + dpYear.SelectedItem.Value + "',PR_ID='" + dpcode.SelectedItem.Value + "'";
            string strwhere= "ID='"+id+"'";
            if (hb.updateWhereID("Rebate", strwhere, images))
            {
                Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../products/productRebate.aspx';</script>");
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../products/productRebate.aspx';</script>");
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