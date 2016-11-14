using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace product.user
{
	public partial class AddUserpart : Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		public string sp_code;
        public string biaoti;
        protected void Page_Load(object sender, EventArgs e)
        {

			sp_code = hb.GetScalarstring(" isnull(MAX(SP_Code),0)AS  SP_Code", "dbo.SYS_Part");
			int code =  Convert.ToInt32( sp_code) + 1;
			if (code.ToString().Length < 4) {
				sp_code = code.ToString().PadLeft(4, '0');
			}
			else { sp_code = code.ToString(); }
		
		
     
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
			string code = GetQueryString("code");
			string name = GetQueryString("name");
			if (code != "" && name != "")
            {
                

				string table = " dbo.SYS_Part WHERE  SP_Name='" + name + "'";
               

                if (hb.GetScalar(table) == 0 )
                {
					string sql = "insert into  dbo.SYS_Part(SP_Name , SP_Code ,opcode) values('" + name + "','" + code + "','" + userCode() + "')";

                    if (hb.insetpro(sql))
                    {
						Response.Write("<script type='text/javascript'>alert('添加成功');window.location.href='../user/UserPartRight.aspx';</script>");
                    }
                    else
                    {
						Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../user/AddUserpart.aspx';</script>");
                    }
                }
                else
                {
					Response.Write("<script type='text/javascript'>alert('(" + name + ")角色已存在！');window.location.href='../user/AddUserpart.aspx';</script>");

                }
            }
        }
    }
}