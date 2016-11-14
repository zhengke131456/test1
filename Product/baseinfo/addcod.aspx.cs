using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;

namespace product.baseinfo
{
    public partial class addcod :Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        string typep, name = "", code = "";
        public string biaoti;

        protected void Page_Load(object sender, EventArgs e)
        {
           
         
            typep = GetQueryString("typep");
            hdtype.Value = typep;
           
            biaoti = hb.bi(typep);
            bind();
        }

        private void bind()
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
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string table = "";
            panduan();
            if (!string.IsNullOrEmpty(GetQueryString("name")))
            {
                name = GetQueryString("name");

            }
            if (hdtype.Value == "4" || hdtype.Value == "5")
            {
            
                if (!string.IsNullOrEmpty(GetQueryString("code")))
                {
                    code = GetQueryString("code");
                   
                }
                //ISNULL(storetype,'')!='SINOPEC' 剔除中石化仓库 
                table = "baseinfo where type='" + typep + "'  and  ISNULL(storetype,'')!='SINOPEC' and basecode='" + code + "' or basename='" + name + "' ";
            }
            else
            {
                table = "baseinfo where type='" + typep + "' and basename='" + name + "'";
            }
           
         
            if (hb.GetScalar(table) == 0)
            {

                string sql = "insert into baseinfo(basecode,basename, area, type, inserttime ) values('" + code + "','" + name + "','" + dparea.SelectedItem.Text + "','" + typep + "',default)";

                if (hb.insetpro(sql))
                {
                    Response.Write("<script type='text/javascript'>alert('添加成功');window.location.href='../baseinfo/baseinfoList.aspx?type=" + typep + "';</script>");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../baseinfo/baseinfoList.aspx?type=" + typep + "';</script>");
                }
            }
            else
            {
                if (hdtype.Value == "4" || hdtype.Value == "5")
                {
                    Response.Write("<script type='text/javascript'>alert('(" + code + ")已有记录，请核实再上传');window.location.href='../baseinfo/baseinfoList.aspx?type=" + typep + "';</script>");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('(" + name + ")已有记录，请核实再上传');window.location.href='../baseinfo/baseinfoList.aspx?type=" + typep + "';</script>");
                }

            }
        }

        private void panduan()
        {
            if (dparea.SelectedItem.Text == "请选择")
            {
                dparea.SelectedItem.Text = "";
            }
        }
    }
}