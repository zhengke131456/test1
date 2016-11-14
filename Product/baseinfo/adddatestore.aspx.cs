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
	public partial class adddatestore : Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		public string basename = "";
        public int level=0;
		public string biaoti ,style= "style=\"display:none\"";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if(!IsPostBack)
            {
				
                dptown.DataSource = hb.getdate(" AreaCode,cityname", "dbo.City WHERE  1=1");
           
                dptown.DataTextField = "cityname";
                dptown.DataValueField = "AreaCode";
                dptown.DataBind();
                dptown.Items.Insert(0, "请选择城市");

            } 
        }



		protected void Button1_Click(object sender, EventArgs e) {

            if (!string.IsNullOrEmpty(GetQueryString("id")))
            {
                hiddID.Value = GetQueryString("id");
                string where = "BaseStore where id=" + hiddID.Value;
                string cha = "basename,Basecode, nodeLevel";
                DataTable dt = hb.getdate(cha, where);


                if (dt.Rows.Count > 0)
                {
                    basecode.Value = dt.Rows[0]["Basecode"].ToString();
                    basename = dt.Rows[0]["basename"].ToString();
                    level = Convert.ToInt32(dt.Rows[0]["nodeLevel"].ToString());

                }
                style = "";
            }

			string table;
			string usercode = userCode();
		
			string code = GetQueryString("code");
			string name = GetQueryString("name");

			string sql = "";
			table = "BaseStore where Basecode='" + code.Trim() + "'  or  basename='" + name.Trim() + "' ";

            level = level + 1;

			if (hb.GetScalar(table) == 0) {

				if (basecode.Value == "") {
                    sql = "insert into BaseStore(Basecode,basename, nodeLevel, Fcode,opcode,citycode) values('" + code + "','" + name + "','" + level + "','0','" + usercode + "','" + dptown.SelectedItem.Value + "')";
				}
				else {
                    sql = "insert into BaseStore(Basecode,basename, nodeLevel, Fcode,opcode,citycode) values('" + code + "','" + name + "','" + level  + "','" + basecode.Value + "','" + usercode + "','" + dptown.SelectedItem.Value + "')";
				}
				if (hb.insetpro(sql)) {
					Response.Write("<script type='text/javascript'>alert('添加成功');window.location.href='../baseinfo/baseinfoStore.aspx';</script>");
				}
				else {
					Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../baseinfo/baseinfoStore.aspx';</script>");
				}
			}
			else {

				Response.Write("<script type='text/javascript'>alert('(" + code + ")已有记录，请核实再上传');window.location.href='../baseinfo/baseinfoStore.aspx';</script>");
			}



		}
    }
}