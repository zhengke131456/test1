using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using ProductBLL.Basebll;

namespace product.Order
{
	public partial class DWOrderfeedback : System.Web.UI.Page
	{
		protected BaseList hb = new BaseList();
		StringBuilder sqlpin = new StringBuilder();
		StringBuilder sqlexist = new StringBuilder();

		protected void Page_Load(object sender, EventArgs e) {



		}
		protected void BtnUP_Click(object sender, EventArgs e) {

			bool fileIsValid = false;

			#region 判断文件类型是否符合要求
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
			#endregion
			//如果文件类型符合要求，则用SaveAs方法实现上传，并显示信息
			if (fileIsValid == true) {
				try {
					string name = Server.MapPath("~/uploadxls/") + "placeorder" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";
					this.FileUpload1.SaveAs(name);
					if (File.Exists(name)) {
						DataTable dtcsv = ProductBLL.Search.Searcher.OpenCSV(name);
						DataTable dbdt = hb.getdate("*", "dbo.Placeorder WITH(NOLOCK) WHERE 1=1");
						if (dtcsv.Rows.Count > 0) {
							#region CAI够6位数 左边补0


							//for (int i = 0; i < dtcsv.Rows.Count; i++) {

							//    if (dtcsv.Rows[i]["CAI"].ToString().Length < 6) {
							//        //不够6位数 左边补0
							//        dtcsv.Rows[i]["CAI"] = dtcsv.Rows[i]["CAD"].ToString().PadLeft(6, '0');
							//    }

							//}
							#endregion

							#region  和数据库数量比较 来判断状态

							for (int i = 0; i < dtcsv.Rows.Count; i++) {

								string sqlwhere = "placeorderName='" + dtcsv.Rows[i]["下单人"].ToString() + "' AND QueryDate='" + dtcsv.Rows[i]["查货日期"] + "' AND CAD='" + dtcsv.Rows[i]["CAD"] + "' AND ClientCode='" + dtcsv.Rows[i]["客户编码"] + "'";


								string placeorderName = dtcsv.Rows[i]["下单人"].ToString();
								string QueryDate = dtcsv.Rows[i]["查货日期"].ToString();
								string CAD = dtcsv.Rows[i]["CAD"].ToString();
								string ClientCode = dtcsv.Rows[i]["客户编码"].ToString();
								int number = Convert.ToInt32(dtcsv.Rows[i]["数量"].ToString());
								string ostatus = dtcsv.Rows[i]["状态"].ToString();
								Queryfeedback(dbdt.Select(sqlwhere), placeorderName, CAD, QueryDate, ClientCode, number,ostatus, sqlwhere);

							}

							#endregion

							#region 执行上传功能

							if (sqlexist.Length > 0) {
								Response.Write("<script type='text/javascript'>window.parent.alert('" + sqlexist.ToString() + "以上数据系统中不存在请核实！');window.location.href='../Order/UPQueryfeedback.aspx';</script>");
								sqlpin.Clear();
								return;
							}
							
							if (sqlpin.Length > 0) {
								if (hb.insetpro(sqlpin.ToString())) {
									Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../Order/UPQueryfeedback.aspx';</script>");

									return;
								}
								else {
									Response.Write("<script type='text/javascript'>window.parent.alert('上传失败');window.location.href='../Order/UPQueryfeedback.aspx;'</script>");

									return;
								}

							}
							else {
								Response.Write("<script type='text/javascript'>window.parent.alert('系统中没有相关数据');window.location.href='../Order/UPQueryfeedback.aspx;'</script>");
							}
							#endregion

						}

					}

				}
				catch {
					Response.Write("<script type='text/javascript'>window.parent.alert('文件上传失败!')</script>");
					return;
				}
				finally {
					sqlpin.Clear();
					
					sqlexist.Clear();
				}

			}
		}
		/// <summary>
		/// 和数据库比较更新状态
		/// </summary>
		/// <param name="dr"></param>
		/// <param name="csvNumber"></param>
		private void Queryfeedback(DataRow[] dr, string placeorderName, string CAD, string QueryDate, string ClientCode, int csvNumber, string ostatus,string sqlwhere) {
			//0未下载、1已下载、2有货、3部分有货、4无货、5已下单、6未下单
			if (dr.Count() > 0) {

				
				for (int k = 0; k < dr.Count(); k++) {

					if (ostatus == "未下单") {//未下单
						sqlpin.Append("UPDATE dbo.Placeorder SET ostatus='6',number='"+csvNumber+"' WHERE " + sqlwhere + ";");
					}

					else  { //已下单
						sqlpin.Append("UPDATE dbo.Placeorder SET ostatus='5' WHERE " + sqlwhere + ";");
					}
				}
			}
			else {
				//数据库没有该记录
				sqlexist.Append("CAD：" + CAD + "下单人：" + placeorderName + "查货日期：" + QueryDate + "客户编码：" + ClientCode + "\\n");
			}

		}
	}
}