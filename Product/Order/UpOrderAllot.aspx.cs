using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.Order
{
	public partial class UpOrderAllot : Common.BasePage
	{
		protected string batchNo = "";
		protected string cai = "", clientCode = "", QCDate = "", statu = "";
		protected int curpage = 1, pagesize = 20, allCount = 0;
		protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		protected void Page_Load(object sender, EventArgs e) {

			if (!IsPostBack) {
				if (GetQueryString("batch") != "") {
					hidbatch.Value = batchNo = GetQueryString("batch");
				}
				try {
					query();
					bindData();
				}
				catch { }

			}
		}



		private void bindData() {


			string sqlwhere = " dbo.Placeorder where 1=1  and  batchmark='" + batchNo + "' ";
			if (QCDate != "") {
				sqlwhere += " and  QueryDate  ='" + QCDate + "' ";
			}
			if (cai != "") {
				sqlwhere += "  and  CAD  LIKE'%" + cai + "%' ";
			}
			if (clientCode != "") {
				sqlwhere += " and ClientCode LIKE'%" + clientCode + "%'";
			}
			if (statu != "") {
				sqlwhere += " and ostatus ='" + statu + "'";
			}


			DataTable dt = hb.getdate("*", sqlwhere);
			string status = "";
			if (dt.Rows.Count>0) {
				allCount = dt.Rows.Count;
				
				
				dt.Columns.Add("status");
				#region 标识c处理
				for (int i = 0; i < dt.Rows.Count; i++) {
					//Old未下载、已下载、有货、部分有货、无货、已下单、未下单
					//修改20150818共分9个状态。未下载、已下载、查询有货、查询部分有货、查询无货、下单有货、下单部分有货、下单无货、取消发货。
					if (dt.Rows[i]["ostatus"].ToString() != "") {
						switch (dt.Rows[i]["ostatus"].ToString()) {
							case "0":
								status = "未下载";
								break;
							case "1":
								status = "已下载";
								break;
							case "2":
								status = "查询有货";
								break;
							case "3":
								status = "查询部分有货";
								break;
							case "4":
								status = "查询无货";
								break;
							case "5":
								status = "下单有货";
								break;
							case "6":
								status = "下单部分有货";
								break;
							case "7":
								status = "下单无货";
								break;
							case "8":
								status = "取消发货";
								break;
						}

					}
					dt.Rows[i]["status"] = status;
				}
				#endregion

				dislist.DataSource = dt;
				dislist.DataBind();
	
			}
		}



		protected void query() {

			if (!string.IsNullOrEmpty(Request.Form["QCDate"])) {
				QCDate = Request.Form["QCDate"].Trim();
			}
			if (!string.IsNullOrEmpty(Request.Form["clientCode"])) {
				clientCode = Request.Form["clientCode"].Trim();
			}
			if (!string.IsNullOrEmpty(Request.Form["CAD"])) {
				cai = Request.Form["CAD"].Trim();
			}
			if (!string.IsNullOrEmpty(Request.Form["dpstatus"])) {
				statu = Request.Form["dpstatus"].ToString();
			}
			
		
		}

		protected void btnQuerey_Click(object sender, EventArgs e) {



			if (GetQueryString("batch") != "") {
				batchNo = GetQueryString("batch");
			}

			query();
			bindData();
		}

		protected void Button1_Click(object sender, EventArgs e) {

			//更新状态
			if (GetQueryString("batch") != "") {
				batchNo = GetQueryString("batch");
			}

			if (hb.ProExecinset("exec [Pro_isOrderfeedback] '" + batchNo + "'")) {
				Response.Write("<script type='text/javascript'>window.parent.alert('全部更新成功');window.location.href='../Order/UpOrderAllot.aspx?batch="+batchNo+"';</script>");
			

			}
			else {

				Response.Write("<script type='text/javascript'>window.parent.alert('全部更新失败！');window.location.href='../Order/UpOrderAllot.aspx?batch=" + batchNo + "';</script>");
			
			}
		
		}
	}




}