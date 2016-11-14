using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.user
{
	public partial class UpdateUserPart : Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        public string basename = "";
        public string biaoti;
        string id;
		public string name, code, opcode;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(GetQueryString("id")))
            {
                id = GetQueryString("id");
				string where = "SYS_Part where SP_ID=" + id;
                string cha = " * ";
                DataTable dt = hb.getdate(cha, where);
                if (dt.Rows.Count > 0)
                {
					name = dt.Rows[0]["SP_Name"].ToString();
					code = dt.Rows[0]["SP_Code"].ToString();
					opcode = dt.Rows[0]["opcode"].ToString();
                }
                //biaoti = hb.bi(typep);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string bname = GetQueryString("name");
            string bcode = GetQueryString("code");
			


			string images = "SP_Name='" + bname + "',Updatetime='" + DateTime.Now.ToString() + "'";
            

			if (hb.GetScalar(" dbo.SYS_Part WHERE  SP_Name='" + bname + "' AND  SP_ID!='" + id + "'") !=0) {
				Response.Write("<script type='text/javascript'>alert('当前角色已存在，请核实后在修改');window.location.href='../user/UpdateUserPart.aspx';</script>");
				return;
			}
			else {


				if (hb.updateWhereID("dbo.SYS_Part", "sp_id='" + id + "'", images))
                {
					Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../user/UserPartRight.aspx';</script>");
                }
                else
                {
					Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../user/UpdateUserPart.aspx';</script>");
                }
            }
           
        }
    }
}