﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace product.user
{
	public partial class PartSelectRight : Common.BasePage
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

		protected DataView dv_dep, dvjson;
		protected StringBuilder result;
		protected string flag;
		
		private void Page_Load(object sender, System.EventArgs e) {

			
			flag = this.Request.QueryString["flag"];//是不是角色赋权
            hidtype.Value = this.Request.QueryString["code"];
			
			//ui = (YJ.Util.UserInfo)Session["userInfo"];
			if (!Page.IsPostBack) {

				
				
				dvjson = hb.getProdatable("SELECT SF_baseClass,SF_rcode,SF_rname FROM  dbo.Sys_Function with(nolock) WHERE SF_del=0 ORDER BY SF_rcode").DefaultView;//所有权限
				
				result = new StringBuilder();
				result.Append("{id:\"0\",");
				CreateTree("0");
				result.Remove(result.Length - 1, 1);
		        ltljson.Text = "<script> var treejson=" + (result.ToString()) + "</script>";

				if (flag == "part") {
					//当前角色的权限
					dvjson = hb.getProdatable("SELECT DISTINCT BF_code FROM dbo.SYS_Right WHERE SP_code='" + hidtype.Value + "'").DefaultView;
				}
				for (int i = 0; i < dvjson.Count; i++) {
					hdcheck.Value = hdcheck.Value + dvjson[i]["BF_code"].ToString() + ",";
				}
				

			}
		

		}
		/// <summary>
		///
		/// </summary>
		/// <param name="fid">节点</param>
		private void CreateTree(string fid) {
			dvjson.RowFilter = "SF_baseClass='" + fid + "'";
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
				treeid[i] = dvjson[i]["SF_rcode"].ToString();
				treename[i] = dvjson[i]["SF_rname"].ToString();
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