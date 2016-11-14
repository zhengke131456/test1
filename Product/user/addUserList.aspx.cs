using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;

namespace product.user
{
	public partial class addUserList : Common.BasePage
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


			string sql = "insert into userinfo ( username, userpass, part, area,phone,Email,SP_Code ) values ('" + GetQueryString("txtcode") + "','" + GetQueryString("txtpassword") + "','" + dppart.SelectedItem.Text + "','" + dparea.SelectedItem.Text + "','" + GetQueryString("phone") + "','" + GetQueryString("Email") + "','" + dppart.SelectedItem.Value + "')";
            if(hb.insetpro(sql))
            {
                Response.Write("<script type='text/javascript'>alert('添加成功');window.location.href='../user/UserList.aspx';</script>");
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../user/UserList.aspx';</script>");
            }
        }

        void bind() 
        {

            IDictionary dict = (IDictionary)ConfigurationManager.GetSection("District");
            if (dict.Count > 0)
            {
                foreach (string key in dict.Keys)
                {
                    dparea.Items.Add(dict[key].ToString());
                }
                dparea.Items.Insert(0, "请选择");
            }
			//IDictionary dict1 = (IDictionary)ConfigurationManager.GetSection("Part");
			//if (dict1.Count > 0)
			//{
			//    foreach (string key in dict1.Keys)
			//    {
			//        dppart.Items.Add(dict1[key].ToString());
			//    }
			//    dppart.Items.Insert(0, "请选择");
			//}
			ListItem itemAll;
			DataTable partdb = hb.getProdatable("SELECT SP_Code,SP_Name FROM  dbo.SYS_Part WHERE ISNULL(SP_del,0)=0");
			for (int i = 0; i < partdb.Rows.Count; i++) {
				itemAll = new ListItem();
				itemAll.Text = partdb.Rows[i]["SP_Name"].ToString();
				itemAll.Value = partdb.Rows[i]["SP_Code"].ToString();
				dppart.Items.Add(itemAll);
			}
			dppart.Items.Insert(0, "请选择");
        }

        void panduan() 
        {
            if (dparea.SelectedItem.Text == "请选择")
            {
                dparea.SelectedItem.Text = "";
                dparea.SelectedItem.Value = "请选择";
            }
            if (dppart.SelectedItem.Text == "请选择")
            {
                dppart.SelectedItem.Text = "";
                dppart.SelectedItem.Value = "请选择";
            }
        }
    }
}