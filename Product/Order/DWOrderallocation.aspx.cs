using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace product.Order
{
	public partial class DWOrderallocation : Common.BasePage
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

		protected DataTable dv_client, dvjson;
		protected StringBuilder result;
		protected StringBuilder cadpin;
		protected StringBuilder cadsum;
		protected string ordersUser, batch, resultstr, flow;//flow 状态 是下单还是查询反馈
		protected string setHeader = "\"颜色,bieti,placeorder,CAD,批次,查 &nbsp;&nbsp;货反馈数,米其林库存数,"; //标题列
		protected string setHeaderIds = "\"颜色,mark,placeorder,CAD,批次,反馈数,库存数,";//每行的ID
		protected string setColTypes = "\"ed,ed,ro,ro,ro,ro,ro,";//列的属性
		protected string attachHeader = "[\"#rspan\",\"#rspan\",\"#rspan\", \"#rspan\", \"#rspan\", \"#rspan\", \"#rspan\",";
		protected string setColSorting = "\"str,str,str,str,str,str,str,";
		protected void Page_Load(object sender, EventArgs e) {


           // SendMail163("ZK", "sssss", "VVVVVVVVVVV");
            
            ordersUser = this.Request.QueryString["ordersUser"];//下单人
			batch = this.Request.QueryString["batch"];//批次号
	      	flow=this.Request.QueryString["flow"];
	

			#region MyRegion
			
		

			DataSet ds = hb.GetDataSet("exec [Pro_Placeorderallot] '" + ordersUser + "','" + batch + "'");//下单明细
			if (ds.Tables.Count > 0) {
				dv_client = ds.Tables[0];
				dvjson = ds.Tables[1];
			}
			if (dv_client.Rows.Count > 0) {
				for (int i = 0; i < dv_client.Rows.Count; i++) {
					//bieti,CAD,批次,反馈数,库存数,zkwh1,#cspan,zkwh2,#cspan,zkwh3,#cspan
					setHeader += dv_client.Rows[i]["ClientCode"].ToString().Trim() + ",#cspan,";
					string number = dv_client.Rows[i]["ClientCode"].ToString().Trim() + "num,";//
					string allotNum = dv_client.Rows[i]["ClientCode"].ToString().Trim() + "allotNum,";
					setHeaderIds += number + allotNum;
					setColTypes += "ro,ed,";
					attachHeader += "\"查询数\", \"分配数\",";
					setColSorting += "str,str";
				}


			}

			//["#rspan", "#rspan", "#rspan", "#rspan", "#rspan", "查询数", "分配数", "查询数", "分配数", "查询数", "分配数"]

			setHeader = setHeader.Remove(setHeader.Length - 1, 1);//移除最后一个逗号
			setHeaderIds = setHeaderIds.Remove(setHeaderIds.Length - 1, 1);//移除最后一个逗号
			setColTypes = setColTypes.Remove(setColTypes.Length - 1, 1);//移除最后一个逗号
			attachHeader = attachHeader.Remove(attachHeader.Length - 1, 1);
			setColSorting = setColSorting.Remove(setColSorting.Length - 1, 1);
			attachHeader += "]";
			setHeader += "\"";
			setHeaderIds += "\"";
			setColTypes += "\"";

			setColSorting += "\"";
			result = new StringBuilder();
			result.Append("{rows:[");
			CreateGrid();
			result.Remove(result.Length - 1, 1);

			result.Append(" ]};");
			resultstr = result.ToString();
			//ltljson.Text = "<script> var data=" + (result.ToString()) + "</script>";



			#endregion

		}
		/// <summary>
		///
		/// </summary>
		/// <param name="fid">节点</param>
		private void CreateGrid() {


			for (int i = 0; i < dvjson.Rows.Count; i++) {
				result.Append("{ id: " + dvjson.Rows[i]["ID"].ToString() + ",");

				result.Append("data: [");
				result.Append("\"" + dvjson.Rows[i]["colour"].ToString() + "\",");
				result.Append("\"客户\",");
				result.Append("\"" + ordersUser + "\",");
				result.Append("\"" + dvjson.Rows[i]["CAD"].ToString() + "\",");
				#region MyRegion


				if (dvjson.Rows[i]["batchmark"].ToString() == "")
					dvjson.Rows[i]["batchmark"] = "0";
				if (dvjson.Rows[i]["feedbackNum"].ToString() == "")
					dvjson.Rows[i]["feedbackNum"] = "0";
				if (dvjson.Rows[i]["StockQty"].ToString() == "")
					dvjson.Rows[i]["StockQty"] = "0";
				#endregion
				result.Append("\"" + dvjson.Rows[i]["batchmark"].ToString() + "\",");
				result.Append("\"" + dvjson.Rows[i]["feedbackNum"].ToString() + "\",");
				result.Append("\"" + dvjson.Rows[i]["StockQty"].ToString() + "\",");
				//id: 1, data: ["列", "350696_103", "P20150820", "10", "1", "10", "1", "3", "0", "3", "0"]
				for (int k = 0; k < dv_client.Rows.Count; k++) {
					string number = dv_client.Rows[k]["ClientCode"].ToString().Trim() + "num";//
					string allotNum = dv_client.Rows[k]["ClientCode"].ToString().Trim() + "allotNum";
					if (dvjson.Rows[i][number].ToString() == "")
						dvjson.Rows[i][number] = "0";
					if (dvjson.Rows[i][allotNum].ToString() == "")
						dvjson.Rows[i][allotNum] = "0";
					result.Append(" \"" + dvjson.Rows[i][number].ToString() + "\",");
					result.Append(" \"" + dvjson.Rows[i][allotNum].ToString() + "\",");

				}

				result.Append("]},");
			}




		}
		protected void btnQuerey_Click(object sender, EventArgs e) {

			cadpin = new StringBuilder(); cadsum = new StringBuilder();
			DataSet ds = hb.GetDataSet("exec Pro_isUpbatch '" + ordersUser + "','" + batch + "','query',''");

			if (!ds.Equals(null)) {
				DataTable ordre = ds.Tables[0];//订单的分配数
				DataTable feedback = ds.Tables[1];//查货反馈表 数据
				DataRow []dr =null;
				if (ordre.Rows.Count > 0) {


					#region 检查每条cad分配数是否符合大于等于反馈数 and 小于等于总数

					for (int i = 0; i < ordre.Rows.Count; i++) {

						string cad = ordre.Rows[i]["CAD"].ToString();
						int allotNum = Convert.ToInt32(ordre.Rows[i][1].ToString());//分配数
						dr = feedback.Select("cad='" + cad + "'");
						if (dr.Length > 0) {
							int feedbackNum = Convert.ToInt32(dr[0][1].ToString());//反馈数
							int stockQty = Convert.ToInt32(dr[0][2].ToString());//库存数
							int sumnum = Convert.ToInt32(dr[0][3].ToString());//总数
							if (allotNum < feedbackNum) {
								//如果分配数小于反馈数就提示
								cadpin.Append("" + cad + "\\n");

							}
							if (allotNum > sumnum) {
								//大于了分配数+库存数之和
								cadsum.Append("" + cad + "\\n");

							}

						}

					}
					#endregion
					if (cadpin.Length > 0 && cadsum.Length > 0)
					{

						Response.Write("<script type='text/javascript'>window.parent.alert('当前CAD" + cadpin.ToString() + "分配数小于反馈数\\n当前CAD" + cadsum.ToString() + "分配总数大于反馈数和米其林库存数总和')</script>");
						return;

					}
					else if (cadpin.Length > 0) {

						Response.Write("<script type='text/javascript'>window.parent.alert('当前CAD" + cadpin.ToString() + "全部客户分配总数小于查货反馈数!请将查货反馈分配完！谢谢！')</script>");
						return;

					}
					else if (cadsum.Length > 0) {

						Response.Write("<script type='text/javascript'>window.parent.alert('当前CAD" + cadsum.ToString() + "分配数大于反馈数和库存数总和')</script>");
						return;

					}
					else {




						if (hb.ProExecinset("exec Pro_isUpbatch '" + ordersUser + "','" + batch + "','','" + flow + "'")) {

                            SendMail163("zk", ordersUser + "完成下单分配", "下单员为：" + ordersUser + " 已完成批次号为：" + batch + "的查货分配!");//放着，
                            SendMail163("cdq", ordersUser + "完成下单分配", "下单员为：" + ordersUser + " 已完成批次号为：" + batch + "的查货分配!");

							Response.Write("<script type='text/javascript'>window.parent.alert('当前下单人全部分配完成')</script>");

						}
					
					}

				}

			}


			#region  废除 验证当前批次下当前下单员 是否已经分配完成 和 当前批次下所有下单员是否已经完成
			/*
				
			//	DataTable ddt = hb.getProdatable("exec Pro_isUpbatch '" + ordersUser + "','" + batch + "'");

			if (ddt.Rows.Count > 0) {
					 string isnum = ddt.Rows[0][0].ToString();
					 if (isnum == "0") {
						 Response.Write("<script type='text/javascript'>window.parent.alert('当前批次下该下单人已经分配的总和小于反馈数总和')</script>");
						 return;
						 
					 }

					 else if (isnum == "1") {
						 Response.Write("<script type='text/javascript'>window.parent.alert('全部分配完成')</script>");
						 return;
					 }
					  
					 //else if (isnum == "2") { //这个就不提示啦
					 //    Response.Write("<script type='text/javascript'>window.parent.alert('还有下单员没有分配完成')</script>");
					    
					 //    return;
					 //}

				 }
			*/

			#endregion
		}
	}
}