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
                DataTable dt = hb.getdate("*", "userinfo where ID=" + id);
                if(dt.Rows.Count>0)
                {
                    txtcode.Value = dt.Rows[0]["username"].ToString();
                    txtpassword.Value= dt.Rows[0]["userpass"].ToString();
					phone.Value = dt.Rows[0]["phone"].ToString();
					Email.Value = dt.Rows[0]["Email"].ToString();
                    if (dt.Rows[0]["part"].ToString()!="")
                    {
						dppart.SelectedValue = dt.Rows[0]["SP_Code"].ToString();
                    }
                    if (dt.Rows[0]["area"].ToString() != "")
                    {
                        dparea.SelectedValue = dt.Rows[0]["area"].ToString();
                    }
                }
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            panduan();
            string username = GetQueryString("txtcode");
            string userpass = GetQueryString("txtpassword");
            string part = dppart.SelectedItem.Text;
            string area = dparea.SelectedItem.Text;
			string phone = GetQueryString("phone");
			string email = GetQueryString("Email");

			string partcode = dppart.SelectedItem.Value;
			string images = "username='" + username + "',userpass='" + userpass + "',part='" + part + "',SP_Code='" + partcode + "',area='" + area + "',phone='" + phone + "',Email='" + email + "'";
            string strwhere= "ID='"+id+"'";

			string table = " userinfo  where username='" + username + "' and userpass='" + userpass + "'and SP_Code='" + partcode + "' and area='" + area + "' and id<>'" + id + "'";


            string strid = "userinfo  where username='" + username + "' and id<>'" + id + "' ";
            if (hb.GetScalar(strid) == 0)
            {
                if (hb.GetScalar(table) == 0)
                {
                    if (hb.updateWhereID("userinfo", strwhere, images))
                    {
                        Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../user/UserList.aspx';</script>");
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../user/UserList.aspx';</script>");
                    }
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('已有该记录，请核实再修该');window.location.href='../user/UserList.aspx';</script>");
                }
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('(" + username + ")已有记录，请核实再修该');window.location.href='../user/UpdateUserList.aspx?id="+id+"';</script>");
            }
        }

        void panduan()
        {
            if (dppart.SelectedItem.Text == "请选择")
            {
                dppart.SelectedItem.Text = "";
            }
            if (dparea.SelectedItem.Text == "请选择")
            {
                dparea.SelectedItem.Text = "";
            }
        }
    }
}