using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using productcommon;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.Collections;
using System.Configuration;

namespace product.Order
{

	public partial class Exportplaceorder : Common.BasePage
	{

        protected string cai = "", clientCode = "", QCDate = "", QCEDate = "", placeorderName = "", status = "", batchno = "";
		protected int curpage = 1, pagesize = 20, allCount = 0;
		protected string sqlwhere = "";
		protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		StringBuilder sqlpin = new StringBuilder();
		StringBuilder sqlCad = new StringBuilder();//查看数据是否重复
	
		protected void Page_Load(object sender, EventArgs e) {
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
				if (GetQueryString("dpODName") != "") {
					placeorderName = GetQueryString("dpODName");
				}
				if (GetQueryString("dpstatus") != "") {
					status = GetQueryString("dpstatus");
					dpstatus.SelectedValue = status;
			
				}

                if (GetQueryString("batchno") != "")
                {
                    batchno = GetQueryString("batchno");
                }
				try {
					InitParam("");
					bindData();
				}
				catch { }
			}
			else {
				//BtnDWCollect.Visible = false;
				//BtnDWdetail.Visible = false;
				Button1.Visible = false;
			}
		}


		private void bindData() {
			DataSet ds = getData();
			string orderstatus = "";
			if (!ds.Equals(null)) {
				allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
				lblCount.Text = allCount.ToString();

				DataTable dt = ds.Tables[1];
				 if (dt.Rows.Count > 0) {
					 //BtnDWCollect.Visible = true;
					 //BtnDWdetail.Visible = true;
					 Button1.Visible = true;
				 }
				 else {
					 //BtnDWCollect.Visible = false;
					 //BtnDWdetail.Visible = false;
					 Button1.Visible = false;
				 }
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
						 literalPagination.Text = GenPaginationBar("Exportplaceorder.aspx?page=[page]&CAD=" + cai + "&clientCode=" + clientCode + "&QCDate=" + QCDate + "&QCEDate=" + QCEDate + "&dpODName=" + placeorderName + "&dpstatus=" + status + "", pagesize, curpage, allCount);
					 }
				
			}
			
		}


		private void InitParam(string Index) {
			  #region

		          
				sinfo.PageSize = 20; ;
				sinfo.Orderby = "id";
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
			if (!string.IsNullOrEmpty(Request.Form["dpODName"])) {
				placeorderName = Request.Form["dpODName"].Trim();
			}
			if (!string.IsNullOrEmpty(Request.Form["dpstatus"])) {
				status = Request.Form["dpstatus"].ToString();
				//dpstatus.SelectedValue= status;
				
			}
            if (!string.IsNullOrEmpty(Request.Form["batchno"]))
            {
                batchno = Request.Form["batchno"].ToString();
            }
		}

		public void where() {

			    sinfo.Sqlstr = "";
				sinfo.Sqlstr = "Placeorder where 1=1 ";
		
			if (QCDate != "") {
				sinfo.Sqlstr += " and  QueryDate  >='" + QCDate + "' and  QueryDate  <='" + QCEDate + "'  ";
			}
			if (cai != "") {
				sinfo.Sqlstr += "  and  CAD  LIKE'%" + cai + "%' ";
			}
			if (clientCode != "") {
				sinfo.Sqlstr += " and ClientCode  LIKE '%" + clientCode + "'%";
			}
			
			if (status != "") {
				sinfo.Sqlstr += " and ostatus ='" + status + "'";
			}

            if (batchno != "")
            {
                sinfo.Sqlstr += " and batchmark like '%" + batchno.Trim() + "%'";
            }
			sqlwhere = sinfo.Sqlstr;
			if (placeorderName != "") {
				sinfo.Sqlstr += " and placeorderName = '" + placeorderName + "'";
			}

		}


		
		
		






		protected void Button1_Click(object sender, EventArgs e) {

			int mingxi,huizong,xiadanren;
			string path = Server.MapPath("~/downloadxlsx/Order/");
			string datetime = DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "");
			string collectfile = "汇总.csv";//汇总
			string ordersPeople = "下单人汇总.csv";//汇总
			string detailfile = "明细.csv";//明细
			string compressedfiles = "订单";//压缩后的文件

			Querey();//获取查询参数
			if (QCDate == "" && QCEDate == "") {
				Response.Write("<script type='text/javascript'>window.parent.alert('请选择查询日期!');</script>");
				return;
			}
			else {
				where();
		    mingxi=	 getordetail(path + detailfile);//生成明细文件
			huizong = getordercollect(path + collectfile);//生成汇总文件
			xiadanren = getXdcollect(path + ordersPeople);//下单人汇总
			}

			if (mingxi == 0 && huizong == 0 && xiadanren==0) {
				#region 如果是 未下载 要更新批次



				if (status == "0") {

					//查询条件是未下载 要保存该批次记录并且回写批次记录

					//INSERT INTO dbo.Orderbatch( batch ,ordersUser ,states )
					string username = userCode();
					//string batch = "P" + DateTime.Now.ToString("yyyyMMddHHmmss");//批次号
					string batch = "P" + DateTime.Now.ToString("yyyyMMddHHmm");//批次号
					string sqlsql = " SELECT id from  " + sinfo.Sqlstr;

					#region 流程节点1（待管理员上传反馈）

					string sql = "";
					IDictionary dictmark = (IDictionary)ConfigurationManager.GetSection("flowone");
						
					string flow1 = "";//流程节点1（待管理员上传反馈）
					flow1 = dictmark["flow1"].ToString();//流程节点1（待管理员上传反馈）



					#endregion
					//批次号
				
					if (hb.GetScalar("OrderBatchNumber WHERE  batchNo ='" + batch + "'") <= 0) {
						//总批次号不存在


					
						 sql = " INSERT INTO dbo.OrderBatchNumber( batchNo, num,  statusing,opcode)VALUES ('" + batch + "',1,'" + flow1 + "','" + username + "'); ";
					}


					// 分配 下单人批次管理
					sql += " INSERT INTO dbo.Orderbatch( batch ,ordersUser ,states,opcode )  select  '" + batch + "',placeorderName,'" + flow1 + "','" + username + "' from " + sqlwhere + " GROUP BY placeorderName  ";

					//sql += " AND placeorderName NOT IN ( SELECT  ordersUser FROM    dbo.Orderbatch WHERE   states = '" + flow1 + "' AND batch = 'P20150822' ) GROUP BY placeorderName ";
						
						sql += " UPDATE Placeorder SET  batchmark='" + batch + "' ,ostatus=1 where id  in( ";
						sql += sqlsql + ");";

						if (!hb.ProExecinset(sql)) {
							Response.Write("<script type='text/javascript'>window.parent.alert('更新批次记录失败!');window.location.href='../products/PriceRebateExecl.aspx';</script>");
							return;
						}

					

					
					//生成下单人pci
					

					//string orderClient = "SELECT ClientCode FROM " + sinfo.Sqlstr + " and placeorderName ='" + username + "'  GROUP BY  ClientCode";
					
					
				}
				#endregion



				List<string> listFJ = new List<string>();//保存附件路径
				List<string> listFJName = new List<string>();//保存附件名字
				listFJ.Add(path + detailfile);
				listFJ.Add(path + collectfile);
				listFJ.Add(path + ordersPeople);
				listFJName.Add(detailfile);
				listFJName.Add(collectfile);
				listFJName.Add(ordersPeople);

				ZipFileMain(listFJ.ToArray(), listFJName.ToArray(), (path + compressedfiles + datetime + ".zip"), 9);//压缩文件

				if (File.Exists(path + detailfile)) {
					File.Delete(path + detailfile);

				}
				if (File.Exists(path + collectfile)) {
					File.Delete(path + collectfile);
				}
				if (File.Exists(path + ordersPeople)) {
					File.Delete(path + ordersPeople);
				}
				DownloadFile((compressedfiles + datetime + ".zip"), (path + compressedfiles + datetime + ".zip"));//下载文件
			}
			else {
				if (mingxi == 1 || huizong == 0 ) { Response.Write("<script type='text/javascript'>window.parent.alert('导出Execl失败!');window.location.href='../products/PriceRebateExecl.aspx';</script>"); }
			}

		}

		protected int  getordercollect(string path) {

			string sqlstr =  sinfo.Sqlstr + "  GROUP BY CAD,Odesc ";

			DataTable dtdb = hb.getdate("CAD,Odesc,SUM(number)AS number", sqlstr);
			if (!ProductBLL.download.ExcelHelper.TableToCsv(dtdb, path)) {

				//Response.Write("<script type='text/javascript'>window.parent.alert('导出Execl失败!');window.location.href='../products/PriceRebateExecl.aspx';</script>");
				return 1;
			}
			else {
				return 0;
			}
		}



		/// <summary>
		/// 下单人汇总
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		protected int getXdcollect(string path) {

			string sqlstr = sinfo.Sqlstr + " GROUP BY placeorderName,CAD       ORDER BY placeorderName ";

			DataTable dtdb = hb.getdate(" placeorderName as '下单人',CAD,SUM(number)number", sqlstr);
			if (!ProductBLL.download.ExcelHelper.TableToCsv(dtdb, path)) {

				

				return 1;
			}
			else {
				return 0;
			}
		}
		/// <summary>
		/// 明细数据
		/// </summary>
		/// <param name="path"></param>
		protected int getordetail(string path) {

			string strsql = "CAD,QueryDate AS '查货日期',Odesc AS 'desc',number AS '数量',ClientCode AS '客户编码',placeorderName AS '下单人',storagename as '中转仓'";
			strsql += "	,(CASE WHEN ostatus='0' THEN'未下载' WHEN ostatus='1' THEN '已下载' ";
			strsql += "	 WHEN ostatus='2' THEN'查询有货' WHEN ostatus='3' THEN '查询部分有货'  ";
			strsql += "	 WHEN ostatus='4' THEN'查询无货' WHEN ostatus='5' THEN '下单有货' ";
			strsql += "	WHEN ostatus='6' THEN'下单部分有货' WHEN ostatus='7' THEN'下单无货' WHEN ostatus='8' THEN'取消发货'  END)AS'状态 '";
			DataTable dtdb = hb.getdate(strsql, sinfo.Sqlstr);

			if (ProductBLL.download.ExcelHelper.TableToCsv(dtdb, path)) {
				return 0;
			}
			else {
				
				return 1;
			}
		}

		/// <summary>
		/// 压缩文件
		/// </summary>
		/// <param name="filenames">要压缩的所有文件（完全路径)</param>
		/// <param name="fileName">文件名称</param>
		/// <param name="name">压缩后文件路径</param>
		/// <param name="Level">压缩级别</param>
		public void ZipFileMain(string[] filenames, string[] fileName, string name, int Level) {
			ZipOutputStream s = new ZipOutputStream(File.Create(name));
			Crc32 crc = new Crc32();
			//压缩级别
			s.SetLevel(Level); // 0 - store only to 9 - means best compression
			try {
				int m = 0;
				foreach (string file in filenames) {
					//打开压缩文件
					FileStream fs = File.OpenRead(file);//文件地址
					byte[] buffer = new byte[fs.Length];
					fs.Read(buffer, 0, buffer.Length);
					//建立压缩实体
					ZipEntry entry = new ZipEntry(fileName[m].ToString());//原文件名
					//时间
					entry.DateTime = DateTime.Now;
					//空间大小
					entry.Size = fs.Length;
					fs.Close();
					crc.Reset();
					crc.Update(buffer);
					entry.Crc = crc.Value;
					s.PutNextEntry(entry);
					s.Write(buffer, 0, buffer.Length);
					m++;
				}
			}
			catch {
				throw;
			}
			finally {
				s.Finish();
				s.Close();
			}
		}
		private void DownloadFile(string fileName, string filePath) {
			FileInfo fileInfo = new FileInfo(filePath);
			Response.Clear();
			Response.ClearContent();
			Response.ClearHeaders();
			Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
			Response.AddHeader("Content-Length", fileInfo.Length.ToString());
			Response.AddHeader("Content-Transfer-Encoding", "binary");
			Response.ContentType = "application/octet-stream";
			Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
			Response.WriteFile(fileInfo.FullName);
			Response.Flush();
		     Response.Clear();
			//下载完成后删除服务器下生成的文件
			 if (File.Exists(filePath)) {
				 File.Delete(filePath);

			}
			Response.End();
		}
	
		
	}
}