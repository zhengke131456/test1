using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.products
{
    public partial class addpro : System.Web.UI.Page
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
            string sql = "insert into products values (default,'" + dpcode.SelectedItem.Text + "','" + dpspeed.SelectedItem.Text + "','" +Convert.ToInt32( dpload.SelectedItem.Text) + "','" +Convert.ToInt32(dpzj.SelectedItem.Text)+ "','" + dphw.SelectedItem.Text + "','" + dpseason.SelectedItem.Text + "','" + dpxh.SelectedItem.Text + "',default)";
            if(hb.insetpro(sql))
            {
                Response.Write("<script type='text/javascript'>alert('添加成功');window.location.href='../products/productList.aspx';</script>");
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../products/productList.aspx';</script>");
            }
        }

        void bind() 
        {
            dpcode.DataSource = hb.getdate("basename", "baseinfo where type=1 ORDER  BY basename");
            dpcode.DataTextField = "basename"; 
            dpcode.DataBind();
            dpcode.Items.Insert(0, "请选择");



            dpspeed.DataSource = ProductBLL.Search.Searcher.speedtable();
            dpspeed.DataTextField = "name";
            dpspeed.DataBind();
            dpspeed.Items.Insert(0, "请选择");


            dpload.DataSource = ProductBLL.Search.Searcher.LOADINGtable();
            dpload.DataTextField = "name";
            dpload.DataBind();
            dpload.Items.Insert(0, "请选择");

            dpzj.DataSource = ProductBLL.Search.Searcher.Rtable();
            dpzj.DataTextField = "name";
            dpzj.DataBind();
            dpzj.Items.Insert(0, "请选择");

       

             
            dphw.DataSource = hb.getdate("basename", "baseinfo where type=2");
            dphw.DataTextField = "basename";
            dphw.DataBind();
            dphw.Items.Insert(0, "请选择");


            dpseason.DataSource = ProductBLL.Search.Searcher.SEASONtable();
            dpseason.DataTextField = "name";
            dpseason.DataBind();
            dpseason.Items.Insert(0, "请选择");
          
            dpxh.DataSource = hb.getdate("basename", "baseinfo where type=3");
            dpxh.DataTextField = "basename";
            dpxh.DataBind();
            dpxh.Items.Insert(0, "请选择");


            
        }

        void panduan() 
        {
            if (dpcode.SelectedItem.Text == "请选择")
            {
                dpcode.SelectedItem.Text = "";
            }
            if (dpspeed.SelectedItem.Text == "请选择")
            {
                dpspeed.SelectedItem.Text = "";
            }
            if (dpload.SelectedItem.Text == "请选择")
            {
                dpload.SelectedItem.Text = "0";
            }
            if (dpzj.SelectedItem.Text == "请选择")
            {
                dpzj.SelectedItem.Text = "0";
            }
            if (dphw.SelectedItem.Text == "请选择")
            {
                dphw.SelectedItem.Text = "";
            }
            if (dpseason.SelectedItem.Text == "请选择")
            {
                dpseason.SelectedItem.Text = "";
            }
            if (dpxh.SelectedItem.Text == "请选择")
            {
                dpxh.SelectedItem.Text = "";
            }
        }
    }
}