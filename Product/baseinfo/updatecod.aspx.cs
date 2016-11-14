using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;

namespace product.baseinfo
{
    public partial class updatecod : Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        public string basename = "", basecode="";
        public string biaoti,  style = "style=\"display:none\"";
         
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if(!IsPostBack)
            {
                if (!string.IsNullOrEmpty(GetQueryString("id")))
                {
                    hiddID.Value = GetQueryString("id");
                    string where = "baseinfo where id=" + hiddID.Value;
                    string cha = "*";
                    DataTable dt = hb.getdate(cha, where);
                    bind();

                    if (dt.Rows.Count > 0)
                    {
                        basename = dt.Rows[0]["basename"].ToString();
                        hdtypep.Value = dt.Rows[0]["type"].ToString();
                        if (hdtypep.Value == "4" || hdtypep.Value == "5")
                        {
                            if (dt.Rows[0]["area"].ToString() != "")
                            {
                               
                                dparea.SelectedValue = dt.Rows[0]["area"].ToString().Trim();
                             
                            }
                            basecode = dt.Rows[0]["basecode"].ToString();

                        }
                    }
                    biaoti = hb.bi(hdtypep.Value);
                }
            } 
        }

        private void bind()
        {
            IDictionary dict = (IDictionary)ConfigurationManager.GetSection("District");
            if (dict.Count > 0)
            {
                foreach (string key in dict.Keys)
                {
                    dparea.Items.Add(dict[key].ToString().Trim());
                }
                dparea.Items.Insert(0, "请选择");
            }
        }
        private void panduan()
        {
            if (dparea.SelectedItem.Text == "请选择")
            {
                dparea.SelectedItem.Text = "";
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            panduan();
            
            string table;
            string baname = GetQueryString("basename");
            basecode = GetQueryString("basecode");
            if (hdtypep.Value == "4" || hdtypep.Value == "5"  )
            {

                table = "baseinfo where (basecode='" + basecode.Trim() + "'  or  basename='" + baname.Trim() + "') and  id<> '" + hiddID.Value + "'";
            }
            else
            {
                table = "baseinfo where basename='" + baname.Trim() + "' and id<> '" + hiddID.Value + "'";
            }
            string images = "basename='" + baname + "',basecode='" + basecode + "',area='" + dparea.SelectedItem.Text + "' ";
         
            if (hb.GetScalar(table) == 0)
            {
                if (hb.update("baseinfo", hiddID.Value, images))
                {
                    Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../baseinfo/baseinfoList.aspx?type=" + hdtypep.Value + "';</script>");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../baseinfo/baseinfoList.aspx?type=" + hdtypep.Value + "';</script>");
                }
            }
            else 
            {
                if (hdtypep.Value == "4"|| hdtypep.Value == "5")
                {
                    Response.Write("<script type='text/javascript'>alert('(" + basecode + ")已有记录，请核实再上传');window.location.href='../baseinfo/baseinfoList.aspx?type=" + hdtypep.Value + "';</script>");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('(" + baname + ")已有记录，请核实再上传');window.location.href='../baseinfo/baseinfoList.aspx?type=" + hdtypep.Value + "';</script>");
                }
            }
        }
    }
}