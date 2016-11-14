using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace product.user
{

    public partial class PartSelectCityArea : Common.BasePage
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

		protected DataView dv_dep, dvjson;
		protected StringBuilder result;
		protected string flag;
		// 添加仓库权限
		private void Page_Load(object sender, System.EventArgs e) {

			
			flag = this.Request.QueryString["flag"];//是不是角色赋权
			hidtype.Value = this.Request.QueryString["id"];
			
		
			if (!Page.IsPostBack) {

				
			     dvjson = hb.getProdatable("SELECT * FROM  dbo.City WHERE  isArea=1").DefaultView;//城市
				
				result = new StringBuilder();
				result.Append("{id:\"0\",");
				CreateTree("1");
				result.Remove(result.Length - 1, 1);
		        ltljson.Text = "<script> var treejson=" + (result.ToString()) + "</script>";

				if (flag == "part") {
					//已经分配的当前角色的仓库权限
                    dv_dep = hb.getProdatable("SELECT Srcode FROM dbo.SYS_RightNew WHERE spcode='" + hidtype.Value + "' and type=1").DefaultView;
				}
                for (int i = 0; i < dv_dep.Count; i++)
                {
                    hdcheck.Value = hdcheck.Value + dv_dep[i]["Srcode"].ToString() + ",";
				}
				

			}
		

		}
		/// <summary>
		///
		/// </summary>
		/// <param name="fid">节点</param>
		private void CreateTree(string fid) {
            dvjson.RowFilter = "isArea='" + fid + "'";
			int i = 0;
			string[] treeid = new string[dvjson.Count];
			string[] treename = new string[dvjson.Count];
			bool flag = false;

			if (treeid.Length > 0) {
				result.Append("item:[");
				flag = true;
			}
			else {
				result.Remove(result.Length - 1, 1);
				result.Append("},");
			}

			
			for (; i < dvjson.Count; i++) {
                treeid[i] = dvjson[i]["AreaCode"].ToString();
                treename[i] = dvjson[i]["cityname"].ToString();
			}
			for (i = 0; i < treeid.Length; i++) {
				result.Append("{id:\"" + treeid[i] + "\",text:\"" + treename[i] + "\",");
				CreateTree(treeid[i]);
			}
			if (flag) {
				result.Remove(result.Length - 1, 1);
				result.Append("]},");
			}
		}

	}
}