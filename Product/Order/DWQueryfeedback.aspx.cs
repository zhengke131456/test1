using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text;
using ProductBLL.Basebll;
using System.Collections;
using System.Configuration;
namespace product.Order
{
	public partial class DWQueryfeedback : Common.BasePage
	{
		protected BaseList hb = new BaseList();
		StringBuilder sqlpin = new StringBuilder();
		StringBuilder sqlnum = new StringBuilder();
		StringBuilder sqlCad = new StringBuilder();
		StringBuilder issqlrepeat = new StringBuilder();
		protected string batchNo = "";


		private string sUser;//下单人信息
		protected void Page_Load(object sender, EventArgs e) {

			if (!IsPostBack) {
				if (GetQueryString("batchNo") != "") {
					batchNo = batchName.Value = GetQueryString("batchNo");
					batchNoID.Value = GetQueryString("id");

				}
			}

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
						//DataTable dbdt = hb.getdate("*", "dbo.Placeorder WITH(NOLOCK) WHERE 1=1");
						if (dtcsv.Rows.Count > 0) {
							#region CAI够6位数 左边补0


							//for (int i = 0; i < dtcsv.Rows.Count; i++) {

							//    if (dtcsv.Rows[i]["CAI"].ToString().Length < 6) {
							//        //不够6位数 左边补0
							//        dtcsv.Rows[i]["CAI"] = dtcsv.Rows[i]["CAI"].ToString().PadLeft(6, '0');
							//    }

							//}
							#endregion



							#region 验证格式
							int a = 0;
							for (int i = 0; i < dtcsv.Rows.Count; i++) {

								if (!GetRegexCAD(dtcsv.Rows[i]["CAD"].ToString().Trim())) {
									sqlCad.Append(dtcsv.Rows[i]["CAD"].ToString().Trim() + ",");
								}

								if (int.TryParse(dtcsv.Rows[i]["数量"].ToString(), out  a) == false) {
									//
									sqlnum.Append(dtcsv.Rows[i]["CAD"].ToString() + ",");
								}


							}
							if (sqlCad.Length > 0) {

								Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！CAD不符合格式：" + sqlCad.ToString() + "');</script>");

								sqlCad.Clear();
								return;
							}

							if (sqlnum.Length > 0) {

								Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！不是有效数字：" + sqlnum.ToString() + "');</script>");

								sqlnum.Clear();
								return;
							}

							string batchstring = "";
							var batchcout = from row in dtcsv.Rows.Cast<DataRow>()

											group row by new { batch = row["批次"].ToString() } into result
											select new { Peo = result.Key, Count = result.Count() };
							foreach (var group in batchcout) {
								if (batchName.Value != group.Peo.batch.Trim()) {
									batchstring += group.Peo.batch + ",";
								}

							}
							if (batchstring != "") {


								Response.Write("<script type='text/javascript'>window.parent.alert('该文件还有别的批次号：" + batchstring.ToString() + "');</script>");


								return;
							}

							//下单人、批次号，是否有CAD上传记录
							DataTable dsall = hb.GetDataSet("select * from [Placeorder] where batchmark='" + batchName.Value + "'").Tables[0];

							for (int i = 0; i < dtcsv.Rows.Count; i++) {
								bool blf = false;
								for (int j = 0; j < dsall.Rows.Count; j++) {
									if (dtcsv.Rows[i]["下单人"].ToString().Trim() == dsall.Rows[j]["placeordername"].ToString().Trim() &&
										dtcsv.Rows[i]["批次"].ToString().Trim() == dsall.Rows[j]["batchmark"].ToString().Trim() &&
										dtcsv.Rows[i]["CAD"].ToString().Trim() == dsall.Rows[j]["CAD"].ToString().Trim()) {
										blf = true;
									}
									else {
										if (dtcsv.Rows[i]["CAD"].ToString().Trim() == "0" && dtcsv.Rows[i]["米其林库存"].ToString().Trim() != "0") {
											blf = true;
										}
										else {

										}
									}
								}

								if (blf == false) {
									Response.Write("<script type='text/javascript'>window.parent.alert('该文件中下单人为：" + dtcsv.Rows[i]["下单人"].ToString() + "、批次为：" + dtcsv.Rows[i]["批次"].ToString() + ";上传查货没有CAD号为：" + dtcsv.Rows[i]["CAD"].ToString() + "的查货信息；如果此条信息为米其林仓库库存数较大的型号，请在上传CSV文件的把数量标为0，米其林库存标为库存数');</script>");

									return;
								}
							}

							#endregion


							#region 同一个下单人 cad是否重复


							var cadrepeat = from row in dtcsv.Rows.Cast<DataRow>()

											group row by new { cad = row["CAD"].ToString().Trim(), usercode = row["下单人"].ToString().Trim(), } into result
											select new { Peo = result.Key, Count = result.Count() };

							foreach (var group in cadrepeat) {


								if (group.Count > 1) {
									issqlrepeat.Append("" + group.Peo.cad + "-" + group.Peo.usercode + "\\n");

								}
							}
							if (issqlrepeat.Length > 0) {

								Response.Write("<script type='text/javascript'>window.parent.alert('当前cad 重复："+issqlrepeat.ToString()+"');window.location.href='../Order/UPQueryfeedback.aspx';</script>");
								return;

							}
							#endregion


							#region  不用  和数据库数量比较 来判断状态

							for (int i = 0; i < dtcsv.Rows.Count; i++) {

								sqlpin.Append("INSERT INTO dbo.UpQueryfeedback( CAD , BatchNo , ordername ,feedbackNum ,StockQty,storage ) VALUES ( '" + dtcsv.Rows[i]["CAD"].ToString().Trim() + "', '" + dtcsv.Rows[i]["批次"].ToString().Trim() + "', '" + dtcsv.Rows[i]["下单人"].ToString().Trim() + "', '" + dtcsv.Rows[i]["数量"].ToString().Trim() + "', '" + dtcsv.Rows[i]["米其林库存"].ToString().Trim() + "', '" + dtcsv.Rows[i]["中转仓"].ToString().Trim() + "' );");

							}


							#endregion
						
							#region 流程节点1（待管理员上传反馈）


							IDictionary dictmark = (IDictionary)ConfigurationManager.GetSection("flowtwo");

							string flow2 = "";//流程节点1（待管理员上传反馈）

							string flow3 = "";//流程节点1（待管理员上传反馈）
							flow3 = dictmark["flow3"].ToString();//流程节点1（待管理员上传反馈）
							flow2 = dictmark["flow2"].ToString();//流程节点1（待管理员上传反馈）


							#endregion


							#region 更新下单单批次状态


							var orderbatch = from row in dtcsv.Rows.Cast<DataRow>()

											 group row by new { batch = row["批次"].ToString().Trim(), usercode = row["下单人"].ToString().Trim(), } into result
											 select new { Peo = result.Key, Count = result.Count() };

							foreach (var group in orderbatch) {



								sUser += group.Peo.usercode + ",";


								sqlCad.Append("UPDATE  dbo.Orderbatch SET states='" + flow3 + "',flow='下单反馈' WHERE batch='" + group.Peo.batch + "' AND  ordersUser='" + group.Peo.usercode + "';");


							}
							#endregion


							sqlpin.Append("UPDATE dbo.OrderBatchNumber SET statusing= '" + flow2 + "' ,updatetime='" + DateTime.Now.ToString() + "' ,flow='下单反馈' WHERE id='" + batchNoID.Value + "';");


							string upsql = "UPDATE Placeorder SET ostatus='4'  WHERE  NOT EXISTS ( SELECT TOP 1 1   ";
							upsql += "  FROM dbo.UpQueryfeedback WHERE Placeorder.CAD=dbo.UpQueryfeedback.CAD AND BatchNo=batchmark);";

							sqlCad.Append(upsql);


							//sqlCad.Append(" UPDATE  dbo.Placeorder SET allotNum=number WHERE id IN (  SELECT id FROM ( ");
							//sqlCad.Append(" SELECT  dbo.Placeorder.ID, dbo.Placeorder.CAD,number,feedbackNum FROM  dbo.Placeorder   ");
							//sqlCad.Append(" LEFT JOIN dbo.UpQueryfeedback ON  dbo.Placeorder.CAD=dbo.UpQueryfeedback.CAD ");
							//sqlCad.Append(" WHERE  batchmark=BatchNo ) a WHERE a.feedbackNum=a.number )");

							#region 如果查询数总和等于反馈书 则修改成 分配数 =查询数
							
						
							sqlCad.Append("   UPDATE  dbo.Placeorder SET     allotNum = dbo.Placeorder.number ");
							sqlCad.Append(" FROM    ( SELECT    porder.CAD ,BatchNo ,feedbackNum ,number ,ordername FROM      dbo.UpQueryfeedback ");
							sqlCad.Append(" LEFT JOIN ( SELECT  CAD ,");
							sqlCad.Append(" batchmark , placeorderName , SUM(number) number  FROM    dbo.Placeorder GROUP BY CAD ,");
							sqlCad.Append("batchmark ,   placeorderName ) porder ON porder.CAD = dbo.UpQueryfeedback.CAD");
							sqlCad.Append("      WHERE     porder.batchmark = BatchNo AND porder.placeorderName = ordername AND feedbackNum=porder.number");

							sqlCad.Append(") feedback WHERE   feedback.cad = dbo.Placeorder.CAD AND feedback.BatchNo = dbo.Placeorder.batchmark");
							sqlCad.Append("         AND dbo.Placeorder.placeorderName = feedback.ordername ");


							#endregion


							sqlCad.Append(" UPDATE  dbo.Placeorder SET storagename= storage ,flow='下单反馈' ");
							sqlCad.Append(" FROM dbo.UpQueryfeedback  WHERE dbo.Placeorder.CAD=dbo.UpQueryfeedback.CAD ");
							sqlCad.Append(" AND placeorderName=ordername AND batchmark=BatchNo  ");

							#region 执行上传功能


							if (sqlpin.Length > 0) {
								if (hb.insetpro(sqlpin.ToString())) { //上传查询反馈


									if (hb.insetpro(sqlCad.ToString())) { //更新下单人状态

										if (hb.insetpro("exec Pro_upcolour '','','" + batchName.Value + "'"))//如果反馈数不等于分配数就改变颜色 
										{
											//给全部下单人发送邮件，通知分配查询反馈

											string[] st = sUser.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

											for (int i = 0; i < st.Length; i++) {
												SendMail163(st[i], "下单反馈", "米其林轮胎批次号为:" + batchName.Value + " 的下单反馈信息管理员已上传至服务，请立刻登录系统，完成查货分配！");
											}

											Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../Order/Orderbatch.aspx';</script>");
										}
										else {
											Response.Write("<script type='text/javascript'>window.parent.alert('标记颜色失败');window.location.href='../Order/DWQueryfeedback.aspx';</script>");
										}

										return;
									}
									else {
										Response.Write("<script type='text/javascript'>window.parent.alert('更新下单人状态失败');window.location.href='../Order/DWQueryfeedback.aspx'</script>");

										return;
									}


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

				}

			}
		}













	}
}