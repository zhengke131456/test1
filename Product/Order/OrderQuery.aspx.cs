using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.IO;

namespace product.Order
{
	public partial class OrderQuery :  Common.BasePage
	{
        protected string cai = "", clientCode = "", QCDate = "", QCEDate = "", placeorderName = "", status = "", batchno="";
		protected int curpage = 1, pagesize = 20, allCount = 0;
		protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		StringBuilder sqlpin = new StringBuilder();
		StringBuilder sqlCad = new StringBuilder();//查看数据是否重复

		protected void Page_Load(object sender, EventArgs e) {

			if (!IsPostBack) {
				if (!string.IsNullOrEmpty(GetQueryString("page"))) {
					if (GetQueryString("QCDate") != "") {
						QCDate = GetQueryString("QCDate");
					}
					if (GetQueryString("QCEDate") != "") {
						QCEDate = GetQueryString("QCEDate");
					}
					if (GetQueryString("clientCode") != "") {
						clientCode = GetQueryString("clientCode");
					}
					if (GetQueryString("CAD") != "") {
						cai = GetQueryString("CAD");
					}
					if (GetQueryString("dpstatus") != "") {
						status = GetQueryString("orstatus");
						//dpstatus.SelectedValue = status;

					}
                    if (GetQueryString("batchno") != "")
                    {
                        batchno = GetQueryString("batchno");
                    }
					
				}
				try {
					InitParam("");
					bindData();
				}
				catch { }
			}
			
		}


		private void bindData() {
			DataSet ds = getData();
			string orderstatus = "";
			if (!ds.Equals(null)) {
				allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
				lblCount.Text = allCount.ToString();

				DataTable dt = ds.Tables[1];
				dt.Columns.Add("status");
				#region 状态
				for (int i = 0; i < dt.Rows.Count; i++) {
					//未下载、已下载、有货、部分有货、无货、已下单、未下单

					if (dt.Rows[i]["ostatus"].ToString() != "") {
						switch (dt.Rows[i]["ostatus"].ToString()) {
							case "0":
								orderstatus = "未下载";
								break;
							case "1":
								orderstatus = "已下载";
								break;
							case "2":
								orderstatus = "查询有货";
								break;
							case "3":
								orderstatus = "查询部分有货";
								break;
							case "4":
								orderstatus = "查询无货";
								break;
							case "5":
								orderstatus = "下单有货";
								break;
							case "6":
								orderstatus = "下单部分有货";
								break;
							case "7":
								orderstatus = "下单无货";
								break;
							case "8":
								orderstatus = "取消发货";
								break;
						}

					}
					dt.Rows[i]["status"] = orderstatus;
				}
				#endregion


				dislist.DataSource = dt;
				dislist.DataBind();
				if (allCount <= pagesize) {
					literalPagination.Visible = false;

				}
				else {

					literalPagination.Visible = true;
                    literalPagination.Text = GenPaginationBar("orderquery.aspx?page=[page]&CAD=" + cai + "&clientCode=" + clientCode + "&QCDate=" + QCDate + "&QCEDate=" + QCEDate + "&dpstatus=" + status + "&batchno=" + batchno + "", pagesize, curpage, allCount);
				}

			}

		}


		private void InitParam(string Index) {
			#region


			sinfo.PageSize = 20; ;
			//sinfo.Orderby = "id";
			where();

			if (Index == "") {
				#region
				if (!string.IsNullOrEmpty(GetQueryString("page"))) {
					curpage = int.Parse(GetQueryString("page"));
					sinfo.PageIndex = curpage;
				}
				#endregion
			}
			else {

				sinfo.PageIndex = 1;
			}

			#endregion
		}




		/// <summary>
		/// 信息
		/// </summary>
		/// <returns></returns>
		private DataSet getData() {

			DataSet dtt = new DataSet();
			dtt = hb.QXGetprodList(sinfo);

			return dtt;
		}
		protected void btnQuerey_Click(object sender, EventArgs e) {
			Querey();
			try {
				InitParam("query");
				bindData();
			}
			catch { }
		}


		public void Querey() {

			if (!string.IsNullOrEmpty(Request.Form["QCDate"])) {
				QCDate = Request.Form["QCDate"].Trim();
			}
			if (!string.IsNullOrEmpty(Request.Form["QCEDate"])) {
				QCEDate = Request.Form["QCEdate"].Trim();
			}
			if (!string.IsNullOrEmpty(Request.Form["clientCode"])) {
				clientCode = Request.Form["clientCode"].Trim();
			}
			if (!string.IsNullOrEmpty(Request.Form["CAD"])) {
				cai = Request.Form["CAD"].Trim();
			}
			if (!string.IsNullOrEmpty(Request.Form["dpstatus"])) {
				status = Request.Form["dpstatus"].ToString();
			}
            if (!string.IsNullOrEmpty(Request.Form["batchno"]))
            {
                batchno = Request.Form["batchno"].ToString();
            }
		}

		public void where() {
			string userName = userCode();
			if (userName == "admin") {
				sinfo.Sqlstr = "Placeorder where 1=1";
			}
			else {

				sinfo.Sqlstr = "Placeorder where placeorderName='" + userCode() + "' ";
			}

			if (QCDate != "") {
				sinfo.Sqlstr += " and  QueryDate  >='" + QCDate + "' and  QueryDate  <='" + QCEDate + "'  ";
			}
			if (cai != "") {
				sinfo.Sqlstr += "  and  CAD  LIKE '%" + cai + "%' ";
			}
			if (clientCode != "") {
				sinfo.Sqlstr += " and ClientCode  LIKE '%" + clientCode + "%'";
			}
			if (status != "") {
				sinfo.Sqlstr += " and ostatus ='" + status + "'";
			}

            if (batchno != "")
            {
                sinfo.Sqlstr += " and batchmark like '%" + batchno + "%'";
            }

		}




		protected void Button1_Click(object sender, EventArgs e) {

		
			string path = Server.MapPath("~/downloadxlsx/Order/");
			string datetime = "订单明细" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "");

			string detailfile = ".csv";//明细



			if (ProductBLL.download.ExcelHelper.TableToCsv(getTable(), path + datetime + detailfile)) {
				downloadfile(path + datetime + detailfile);
				Response.Write("<script type='text/javascript'>window.parent.alert('导出EXecl成功!')</script>");

				return;
			}
			else {
				Response.Write("<script type='text/javascript'>window.parent.alert('导出Execl失败!');window.location.href='../products/PriceRebateExecl.aspx';</script>");

				return;
			}


	
		

			}
		


	

		/// <summary>
		/// 明细数据
		/// </summary>
		/// <param name="path"></param>
		protected  DataTable  getTable() {

			Querey();//获取查询参数
			InitParam("query");
			string strsql = "batchmark as '批次',CAD,QueryDate AS '查货日期',Odesc AS 'desc',number AS '查询数量',allotNum as '分配数',ClientCode AS '客户编码',placeorderName AS '下单人',storagename as '中转仓' ";
			strsql += "	,(CASE WHEN ostatus='0' THEN'未下载' WHEN ostatus='1' THEN '已下载' ";
			strsql += "	 WHEN ostatus='2' THEN'查询有货' WHEN ostatus='3' THEN '查询部分有货'  ";
			strsql += "	 WHEN ostatus='4' THEN'查询无货' WHEN ostatus='5' THEN '下单有货' ";
			strsql += "	WHEN ostatus='6' THEN'下单部分有货' WHEN ostatus='7' THEN'下单无货' WHEN ostatus='8' THEN'取消发货'  END)AS'状态 '";
			DataTable dtdb = hb.getdate(strsql, sinfo.Sqlstr);

		   return  dtdb;
		}

		void downloadfile(string s_path) {
			System.IO.FileInfo file = new System.IO.FileInfo(s_path);
			Response.Clear();
			Response.ClearContent();
			Response.ClearHeaders();

			Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);
			Response.AddHeader("Content-Length", file.Length.ToString());
			Response.AddHeader("Content-Transfer-Encoding", "binary");
			Response.ContentType = "application/octet-stream";
			Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
			Response.WriteFile(file.FullName);
			Response.Flush();
			HttpContext.Current.Response.Clear();
			//下载完成后删除服务器下生成的文件
			if (File.Exists(s_path)) {
				File.Delete(s_path);

			}
			HttpContext.Current.Response.End();
		}
	

	}
}