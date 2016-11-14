using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml;
using System.Text;

namespace product.Handler
{
	/// <summary>
	/// UpdataRow 的摘要说明
	/// </summary>
	public class UpdataRow : IHttpHandler
	{
		protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
		   StringBuilder sqlstr = new StringBuilder();
		   
		public void ProcessRequest(HttpContext context) {
			 context.Response.ContentType = "text/xml";

			 string ids = context.Request["gr_id"].ToString();
			// string mark = "mark"; //保存的 修改的客户名+Old 分配数 的字段名
			 string markdate = context.Request["mark"].ToString();//获取保存的 修改的客户名+Old 分配数
			 string[] strarr = markdate.Split(',');
			
			 string upClien = strarr[0].ToString(); //修改的客户  item.ToString();
			 string oldallotNum = strarr[1].ToString();//旧的分配数
			 string placeorder = context.Request["placeorder"].ToString(); //下单人
		   string cad = context.Request["CAD"].ToString();

		   string num = context.Request[upClien + "num"].ToString();//查询数
		   string allotNum = context.Request[upClien + "allotNum"].ToString();//分配数
			 string feedbackNum = context.Request["反馈数"].ToString();//反馈数
			 string batchmark = context.Request["批次"].ToString();//批次
			 string stockQty = context.Request["库存数"].ToString();//库存数



			

			 //  当前cad 编码 下的所有客户 数分配总和是否小于反馈书+库存数
			 if (hb.getproScalar("exec Pro_iscad '" + cad + "','" + placeorder + "','" + batchmark + "'," + feedbackNum + "," + stockQty + "," + allotNum + ",'" + upClien + "' ") == 0) {




				
				 #region 如果查询数为0 则要新增数据


				 if (num == "0") {
				
						 //表示是新增数据要执行插入

						 #region 检测是执行插入还是更新
						 string insql = "";

						 if (hb.GetScalar(" dbo.Placeorder  WHERE   batchmark = '" + batchmark + "' AND placeorderName = '" + placeorder + "'  AND  CAD ='" + cad + "' and ClientCode='" + upClien + "'") > 0) {
							 //更新
							 insql = "update dbo.Placeorder set allotNum='" + allotNum + "',upfeedback='已分配' where  batchmark = '" + batchmark + "' AND placeorderName = '" + placeorder + "'  AND  CAD ='" + cad + "' and ClientCode='" + upClien + "' ";


						 }
						 else {
							// 插入
							 insql = " INSERT INTO dbo.Placeorder ( CAD ,QueryDate ,ostatus ,Odesc ,number ,ClientCode ,placeorderName , allotNum ,batchmark , upfeedback,flow) ";


							 insql += " SELECT TOP 1 '" + cad + "',QueryDate,ostatus,describe,0 ,'" + upClien + "' ,placeorderName , '" + allotNum + "' ,'" + batchmark + "' , '已分配',flow FROM  dbo.Placeorder ";

							 insql += " LEFT JOIN dbo.product_CAD ON dbo.Placeorder.CAD = dbo.product_CAD.Cad ";
							 insql += " WHERE   batchmark='" + batchmark + "' AND  placeorderName='" + placeorder + "' ";
						 }
						 #endregion


						 if (!hb.insetpro(insql)) {

							 sqlstr.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><data><action type='error' sid='" + ids + "' tid='" + ids + "'>新增数据失败</action></data>");
							 context.Response.Write(sqlstr.ToString());
							 return;
						 }
						 else {
							 #region 废除 验证修改的当前记录是否满足 分配数大于等于反馈数


							 /*
							 if (
						 hb.getproScalar("exec Pro_upallocationcolour '" + placeorder + "','" + batchmark + "','" + cad + "','" + feedbackNum + "','" + stockQty + "','" + ids + "'") == 0)//表示满足分配数大于等于反馈数
							 {
								 sqlstr.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><data><action type='update' sid='" + ids + "' tid='" + ids + "'>white</action></data>");
							 }
							 else {
								 sqlstr.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><data><action type='update' sid='" + ids + "' tid='" + ids + "'>red</action></data>");

							 }
							 */
							 #endregion


							 #region 更新行的颜色
							 //如果反馈数不等于分配数就改变颜色 

							 if (hb.getproScalar("exec Pro_upcolour '" + cad + "','" + placeorder + "','" + batchmark + "'") == 0) {
								 //如果返回0 就表示是白色（分配数大于等于反馈数） 否则就是红色
								 sqlstr.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><data><action type='update' sid='" + ids + "' tid='" + ids + "'>#E0EDF5</action></data>");
							 }
							 else {
								 sqlstr.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><data><action type='update' sid='" + ids + "' tid='" + ids + "'>#FFD2D2</action></data>");
							 
							 }
							 #endregion

							 
							 context.Response.Write(sqlstr.ToString());
							 return;
						 }

				 }
				 #endregion

				 #region 如果查询数不为0 则要新增数据

				 else {

					 if (oldallotNum != allotNum) {
						 //如果新的分配数不等于原来的值 则修改这条记录
						 string upsql = " UPDATE dbo.Placeorder SET allotNum='" + allotNum + "',upfeedback='已分配' ";
						 upsql += " WHERE  CAD='" + cad + "' AND batchmark='" + batchmark + "' AND  placeorderName='" + placeorder + "' and  ClientCode='" + upClien + "'";

						 if (!hb.insetpro(upsql)) {
							 sqlstr.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><data><action type='error' sid='" + ids + "'  tid='" + ids + "'>修改数据失败</action></data>");
							 context.Response.Write(sqlstr.ToString());
							 return;
						 }
						 else {
							
							 #region 更新行的颜色
							 //如果反馈数不等于分配数就改变颜色 

							 if (hb.getproScalar("exec Pro_upcolour '" + cad + "','" + placeorder + "','" + batchmark + "'") == 0) {
								 //如果返回0 就表示是白色（分配数大于等于反馈数） 否则就是红色
								 sqlstr.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><data><action type='update' sid='" + ids + "' tid='" + ids + "'>#E0EDF5</action></data>");
							 }
							 else {
								 sqlstr.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><data><action type='update' sid='" + ids + "' tid='" + ids + "'>#FFD2D2</action></data>");

							 }
							 #endregion


							 //sqlstr.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><data><action type='update' sid='" + ids + "' tid='" + ids + "'>修改成功</action></data>");
							 context.Response.Write(sqlstr.ToString());
							 return;
						 }

					 }
				 }
				 #endregion


			




				 


			 }
			 else {

				 sqlstr.Append("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?><data><action type='error'  sid='" + ids + "' tid='" + ids + "'>客户总的分配数大于反馈书和库存数之和</action></data>");
				 context.Response.Write(sqlstr.ToString());

			 }
		
		}

		public bool IsReusable {
			get {
				return false;
			}
		}
	}
}