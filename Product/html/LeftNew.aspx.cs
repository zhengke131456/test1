using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;

namespace product.html
{
    public partial class LeftNew : Common.BasePage
    {

		protected DataView dvChild;
		protected DataTable dvjson;
		protected StringBuilder result;
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected void Page_Load(object sender, EventArgs e)
        {

            
			//当前人员
			hdtype.Value = userCode();
			dvjson = hb.getProdatable("select SF_rcode,SF_rname,SF_Url,SF_order, SF_baseClass from  SYS_Right   inner join Sys_Function on SF_rcode=BF_code    INNER  JOIN dbo.userinfo ON SYS_Right.SP_code=dbo.userinfo.SP_Code  where  username='" + hdtype.Value + "' AND   SF_del=0 order by SF_rcode");//当前人员所有功能权限
			result = new StringBuilder();


			result.Append("<ul id=\"nav\"> ");
			if (dvjson.Rows.Count > 0) {
				DataRow[] dvParent = dvjson.Select("SF_baseClass=0 ", "SF_order asc");

				for (int i = 0; i < dvParent.Count(); i++) {
					string id = "" + dvParent[i]["SF_rcode"].ToString() + "";
					string Menu = id + "00";
					result.Append("<li class=\"listyl\" id=\"" + id + "\"><a href=\"#Menu=" + Menu + "\" onclick=\"DoMenu('" + Menu + "')\">" + dvParent[i]["SF_rname"].ToString() + "</a>");//父节点


				
					dvChild = dvjson.DefaultView;
					dvChild.RowFilter = " SF_baseClass='" + dvParent[i]["SF_rcode"].ToString() + "' ";
					dvChild.Sort = "SF_order asc";
					result.Append(" <ul id=\"" + Menu + "\" class=\"collapsed\"> ");
					for (int k = 0; k < dvChild.Count; k++) {//遍历子节点
						result.Append(" <li id=\"" + dvChild[k]["SF_rcode"].ToString() + "\"><a href=\"" + dvChild[k]["SF_Url"].ToString() + "\" target=\"rightFrame\">" + dvChild[k]["SF_rname"].ToString() + "</a></li>");

					}
					result.Append(" </ul> </li>");
				}

				// <li class="listyl" id="10"><a href="#Menu=1000" onclick="DoMenu('1000')">基础信息管理</a>
				//  <ul id="1000" class="expanded">
				//   <li id="1001"><a href="../baseinfo/baseinfoList.aspx?type=1" target="rightFrame">编码列表</a></li>
				//   <li id="1002"><a href="../baseinfo/baseinfoList.aspx?type=7" target="rightFrame">CAI列表</a></li>
				//   <li id="1003"><a href="../baseinfo/baseinfoList.aspx?type=2" target="rightFrame">花纹列表</a></li>

				//  </ul>
				//</li>
			}
			result.Append(" </ul>");
			want.Text = result.ToString();
           
        }

        
    }
}