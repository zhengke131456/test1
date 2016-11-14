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

namespace product.Order
{

	public partial class placeorder : Common.BasePage
	{

		protected string cai = "", clientCode = "", QCDate = "",status = "";
		protected int curpage = 1, pagesize = 20, allCount = 0;
		protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		StringBuilder sqlpin = new StringBuilder();
		StringBuilder sqlnum= new StringBuilder();
		StringBuilder sqlcode = new StringBuilder();
		StringBuilder sqltime = new StringBuilder();
		StringBuilder sqlCad = new StringBuilder();//查看数据是否重复
		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				if (GetQueryString("QCDate") != "") {
					QCDate = GetQueryString("QCDate");
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
				try {
					InitParam("");
					//bindData();
				}
				catch { }
			}
		}

		private void bindData() {

            //DataSet ds = getData();
            //string status = "";
            //if (!ds.Equals(null)) {
            //    allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            //    lblCount.Text = allCount.ToString();

            //    DataTable dt = ds.Tables[1];
            //    dt.Columns.Add("status");
            //    #region 标识c处理
            //    for (int i = 0; i < dt.Rows.Count; i++) {
            //        //Old未下载、已下载、有货、部分有货、无货、已下单、未下单
            //        //修改20150818共分9个状态。未下载、已下载、查询有货、查询部分有货、查询无货、下单有货、下单部分有货、下单无货、取消发货。
            //        if (dt.Rows[i]["ostatus"].ToString() != "") {
            //            switch (dt.Rows[i]["ostatus"].ToString()) {
            //                case "0":
            //                    status = "未下载";
            //                    break;
            //                case "1":
            //                    status = "已下载";
            //                    break;
            //                case "2":
            //                    status = "查询有货";
            //                    break;
            //                case "3":
            //                    status = "查询部分有货";
            //                    break;
            //                case "4":
            //                    status = "查询无货";
            //                    break;
            //                case "5":
            //                    status = "下单有货";
            //                    break;
            //                case "6":
            //                    status = "下单部分有货";
            //                    break;
            //                case "7":
            //                    status = "下单无货";
            //                    break;
            //                case "8":
            //                    status = "取消发货";
            //                    break;
            //            }

            //        }
            //        dt.Rows[i]["status"] = status;
            //    }
            //    #endregion




            //    dislist.DataSource = dt;
            //    dislist.DataBind();
            //    if (allCount <= pagesize) {
            //        literalPagination.Visible = false;
            //    }
            //    else {
            //        literalPagination.Visible = true;
            //        literalPagination.Text = GenPaginationBar("placeorder.aspx?page=[page]&CAD=" + cai + "&clientCode=" + clientCode + "&QCDate=" + QCDate + "&dpstatus=" + status + "", pagesize, curpage, allCount);
            //    }
            //}
		}


		private void InitParam(string Index) {
			#region
			sinfo.PageSize = 20;; 
			sinfo.Orderby = "id";
			 string username=userCode();
			 if (username == "admin") {
				 sinfo.Sqlstr = "Placeorder where 1=1 ";
			 }
			 else {
				 sinfo.Sqlstr = "Placeorder where placeorderName='" + username + "' ";
			 }


			if (QCDate != "") {
				sinfo.Sqlstr += " and  QueryDate  ='" + QCDate + "' ";
			}
			if (cai != "") {
				sinfo.Sqlstr += "  and  CAD  LIKE'%" + cai + "%' ";
			}
			if (clientCode != "") {
				sinfo.Sqlstr += " and ClientCode LIKE'%" + clientCode + "%'";
			}
			if (status != "") {
				sinfo.Sqlstr += " and ostatus ='" + status + "'";
			}
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
			DataSet dt = new DataSet();
			dt = hb.QXGetprodList(sinfo);

			return dt;
		}
		protected void btnQuerey_Click(object sender, EventArgs e) {

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
				status = Request.Form["dpstatus"].ToString();
			}
			try {
				InitParam("query");
				bindData();
			}
			catch { }
		}
		protected void BtnUP_Click(object sender, EventArgs e) {
			
			bool fileIsValid = false;
			string UserName = "";
			//如果确认了文件上传，则判断文件类型是否符合要求
			if (this.FileUpload1.HasFile) {
				//获取上传文件的后缀名
				String fileExtension = System.IO.Path.GetExtension(this.FileUpload1.FileName).ToLower();//ToLower是将Unicode字符的值转换成它的小写等效项

				//判断文件类型是否符合要求
				if (fileExtension == ".csv") {
					fileIsValid = true;
				}
				else {
					Response.Write("<script type='text/javascript'>window.parent.alert('文件格式不正确!请上传正确格式的文件')</script>");
					return;
				}

			}
			//如果文件类型符合要求，则用SaveAs方法实现上传，并显示信息
			if (fileIsValid == true) {
				try {

					UserName = userCode();
					string name = Server.MapPath("~/uploadxls/UploadOrder/") + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + UserName + ".csv";
					this.FileUpload1.SaveAs(name);
					if (File.Exists(name)) {
						DataTable dtcsv = ProductBLL.Search.Searcher.OpenCSV(name);
						if (dtcsv.Rows.Count > 0) {
							

							//客户编码
							DataTable ClentInfo = hb.getdate("clientCode", " dbo.ClentInfo where opcode='" + UserName + "'");
							
							#region CAI够6位数 左边补0
							
							
							//for (int i = 0; i < dtcsv.Rows.Count; i++) {

							//    if (dtcsv.Rows[i]["CAI"].ToString().Length < 6) {
							//        //不够6位数 左边补0
							//        dtcsv.Rows[i]["CAI"] = dtcsv.Rows[i]["CAI"].ToString().PadLeft(6, '0');
							//    }

							//}
							#endregion

							#region 验证格式
							 int a=0;
							 if (ClentInfo.Rows.Count > 0) {
								 for (int i = 0; i < dtcsv.Rows.Count; i++) {

									 if (!GetRegexCAD(dtcsv.Rows[i]["CAD"].ToString())) {
										 sqlCad.Append(dtcsv.Rows[i]["CAD"].ToString() + ",");
									 }
									 if (!GetRegexNum(dtcsv.Rows[i]["查货日期"].ToString())) {
										 //
										 sqltime.Append(dtcsv.Rows[i]["CAD"].ToString() + ",");
									 }
									 if (int.TryParse(dtcsv.Rows[i]["数量"].ToString(), out  a) == false) {
										 //
										 sqlnum.Append(dtcsv.Rows[i]["CAD"].ToString() + ",");
									 }
									 DataRow[] dr = ClentInfo.Select("clientCode='" + dtcsv.Rows[i]["客户编码"].ToString() + "'");
									 if (dr.Length <= 0) {

										 sqlcode.Append(dtcsv.Rows[i]["客户编码"].ToString() + ",");
									 }

								 }
								 if (sqlCad.Length > 0) {

									 Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！CAD不符合格式：" + sqlCad.ToString() + "');</script>");

									 sqlCad.Clear();
									 return;
								 }
								 if (sqltime.Length > 0) {

									 Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！当前编码的查询日期格式不正确：" + sqlCad.ToString() + "');</script>");

									 sqltime.Clear();
									 return;
								 }
								 if (sqlnum.Length > 0) {

									 Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！当前编码数量不是有效数字：" + sqlCad.ToString() + "');</script>");

									 sqlnum.Clear();
									 return;
								 }
								 if (sqlcode.Length > 0) {

									 Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！客户编码不存在：" + sqlcode.ToString() + "');</script>");

									 sqlnum.Clear();
									 return;
								 }
							 }
							 else {
								 Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！客户编码不存在：" + sqlCad.ToString() + "');</script>");

								 sqlnum.Clear();
								 return;
							 }
							#endregion
							
						

							#region 当前csv中 同一客户，同一CAD，在同一天不能重复。

							var ageGroups = from emp in dtcsv.Rows.Cast<DataRow>()
										group emp by new { t1 = emp["CAD"].ToString(), t2 = emp["查货日期"].ToString(),t3 = emp["客户编码"].ToString() } into g
										select new { CAD = g.Key.t1, qudate = g.Key.t2, code = g.Key.t3,orcout=g.Count() };

							
			
							foreach (var group in ageGroups) {
								if (Convert.ToInt32(group.orcout) > 1) {
									sqlCad.Append(group.CAD + ",");
								}
							}


							#endregion

					
							#region 数据是否重复


							if (sqlCad.Length <= 0)//没有重复项
                            {

								#region 和当前下单人已经上传过的数据库比较 同一客户，同一CAD，在同一天不能重复。

								//DataTable dtdb = hb.getdate("*", " Placeorder where placeorderName='" + UserName + "'");//查询出当前下单人上传的数据

								//if (dtdb.Rows.Count > 0) {


									#region 2015-10-26 不用和数据库比较


									//if (isrepeat(dtcsv, dtdb)) {
									//    Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！数据重复：" + sqlCad.ToString() + "');</script>");
									//    return;

									//}
									//else {
									//    for (int i = 0; i < dtcsv.Rows.Count; i++) {
									//        sqlpin.Append("INSERT INTO dbo.Placeorder( CAD , QueryDate ,ostatus ,Odesc , number ,ClientCode ,placeorderName,allotNum,upfeedback ) VALUES ('" + dtcsv.Rows[i]["CAD"].ToString() + "','" + dtcsv.Rows[i]["查货日期"].ToString() + "',0,'" + dtcsv.Rows[i]["DESC"].ToString() + "','" + dtcsv.Rows[i]["数量"].ToString() + "' ,'" + dtcsv.Rows[i]["客户编码"].ToString() + "','" + UserName + "',0,'未分配'); ");
									//    }

									//}
									#endregion
								//}
								#endregion
								//else {
								//    for (int i = 0; i < dtcsv.Rows.Count; i++) {
								//        sqlpin.Append("INSERT INTO dbo.Placeorder( CAD , QueryDate ,ostatus ,Odesc , number ,ClientCode ,placeorderName,allotNum,upfeedback ) VALUES ('" + dtcsv.Rows[i]["CAD"].ToString().Trim() + "','" + dtcsv.Rows[i]["查货日期"].ToString().Trim() + "',0,'" + dtcsv.Rows[i]["DESC"].ToString().Trim() + "','" + dtcsv.Rows[i]["数量"].ToString().Trim() + "' ,'" + dtcsv.Rows[i]["客户编码"].ToString().Trim() + "','" + UserName + "',0,'未分配'); ");
								//    }

								//}

								for (int i = 0; i < dtcsv.Rows.Count; i++) {
										sqlpin.Append("INSERT INTO dbo.Placeorder( CAD , QueryDate ,ostatus ,Odesc , number ,ClientCode ,placeorderName,allotNum,upfeedback ) VALUES ('" + dtcsv.Rows[i]["CAD"].ToString().Trim() + "','" + dtcsv.Rows[i]["查货日期"].ToString().Trim() + "',0,'" + dtcsv.Rows[i]["DESC"].ToString().Trim() + "','" + dtcsv.Rows[i]["数量"].ToString().Trim() + "' ,'" + dtcsv.Rows[i]["客户编码"].ToString().Trim() + "','" + UserName + "',0,'未分配'); ");
									}
									
							}
						
							else {
							
								Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！数据重复：" + sqlCad.ToString() + "');</script>");

								sqlCad.Clear();
								return;
							}
							#endregion


							#region 执行上传功能


							if (sqlpin.Length > 0) {
								if (hb.insetpro(sqlpin.ToString())) {
									Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../Order/placeorder.aspx';</script>");

									return;
								}
								else {
									Response.Write("<script type='text/javascript'>window.parent.alert('上传失败');window.location.href='../Order/placeorder.aspx;</script>");

									return;
								}

							}
							#endregion

						}
						else {
							Response.Write("<script type='text/javascript'>window.parent.alert('文件无内容');</script>");
							return;
						}
					}

				}
				catch  (Exception ex) {
					
					Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！');</script>");
					
					return;
				}
				finally {

					sqlCad.Clear();
					sqlpin.Clear();
				}
			}
		}



		/// <summary>
		/// 比较上传的csv和数据库数据是否重复
		/// </summary>
		/// <param name="dcsvt"></param>
		/// <param name="dbdadt"></param>
		/// <returns></returns>
		private  bool isrepeat(DataTable dcsvt, DataTable dbdadt) {

		

			bool isresult=false;
			for (int i = 0; i < dcsvt.Rows.Count; i++) {

				//DateTime.Parse(dcsvt.Rows[i]["查货日期"].ToString());
			

				string ss = "CAD='" + dcsvt.Rows[i]["CAD"].ToString() + "' and QueryDate='" +dcsvt.Rows[i]["查货日期"].ToString()+"' and ClientCode='" + dcsvt.Rows[i]["客户编码"].ToString() + "'";
				DataRow[] sRow = dbdadt.Select(ss);
				if (sRow.Length > 0) {
					sqlCad.Append(dcsvt.Rows[i]["CAD"].ToString() +"日期"+dcsvt.Rows[i]["查货日期"].ToString() + dcsvt.Rows[i]["客户编码"].ToString() + "|");
				}
			}
			if(sqlCad.Length>0)
			{
				isresult=true;
			}
			return isresult;


		}  
	}
}