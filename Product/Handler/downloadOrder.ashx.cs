using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web;
using System.Data;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
namespace product.Handler
{
	/// <summary>
	/// downloadOrder 的摘要说明
	/// </summary>
	public class downloadOrder : IHttpHandler
	{
			protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
			protected string batch;
		  public void ProcessRequest(HttpContext context) {
			context.Response.ContentType = "text/plain";
			int mingxi, huizong, xiadanren;
			 batch = context.Request["batch"].ToString();

			string path = context.Server.MapPath("~/downloadxlsx/Order/");
			string datetime = DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "");
			string collectfile = "下单汇总.csv";//汇总
			string detailfile = "下单明细.csv";//明细
			string ordersPeople = "下单人汇总.csv";//汇总

			string compressedfiles = "订单实际下单";//压缩后的文件
			

			mingxi = getordetail(path + detailfile);//生成明细文件
			huizong = getordercollect(path + collectfile);//生成汇总文件
			xiadanren = getXdcollect(path + ordersPeople);//下单人汇总


			if (mingxi == 0 && huizong == 0) {


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
				DownloadFile((compressedfiles + datetime + ".zip"), (path + compressedfiles + datetime + ".zip"), context);//下载文件
				

			}
			else {
				if (mingxi == 1 || huizong == 0) {
					context.Response.Write("<script type='text/javascript'>window.parent.alert('导出Execl失败!');window.location.href='../Order/batchmanagement.aspx';</script>");
				}
			}
		}

		public bool IsReusable {
			get {
				return false;
			}
		}
		
		/// <summary>
		/// 汇总
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		protected int getordercollect(string path) {

			string sqlstr = " dbo.Placeorder WHERE batchmark='" + batch + "' GROUP BY CAD,batchmark";

			DataTable dtdb = hb.getdate("CAD,batchmark,SUM(allotNum)AS allotNum", sqlstr);
			if (!ProductBLL.download.ExcelHelper.TableToCsv(dtdb, path)) {

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

			string sqlstr = " dbo.Placeorder WHERE batchmark='" + batch + "'   GROUP BY placeorderName,CAD       ORDER BY placeorderName ";

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

			string strsql = "CAD,QueryDate AS '查货日期',Odesc AS 'desc',number AS '数量',allotNum as '分配数',ClientCode AS '客户编码',placeorderName AS '下单人' ,storagename as '中转仓'";
			strsql += "	,(CASE WHEN ostatus='0' THEN'未下载' WHEN ostatus='1' THEN '已下载' ";
			strsql += "	 WHEN ostatus='2' THEN'查询有货' WHEN ostatus='3' THEN '查询部分有货'  ";
			strsql += "	 WHEN ostatus='4' THEN'查询无货' WHEN ostatus='5' THEN '下单有货' ";
			strsql += "	WHEN ostatus='6' THEN'下单部分有货' WHEN ostatus='7' THEN'下单无货' WHEN ostatus='8' THEN'取消发货'  END)AS'状态 '";
			DataTable dtdb = hb.getdate(strsql, "dbo.Placeorder WHERE batchmark='" + batch + "'");

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
		private void DownloadFile(string fileName, string filePath, HttpContext context) {
			FileInfo fileInfo = new FileInfo(filePath);
			context.Response.Clear();
			context.Response.ClearContent();
			context.Response.ClearHeaders();
			context.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
			context.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
			context.Response.AddHeader("Content-Transfer-Encoding", "binary");
			context.Response.ContentType = "application/octet-stream";
			context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
			context.Response.WriteFile(fileInfo.FullName);
			context.Response.Flush();
			context.Response.Clear();
			//下载完成后删除服务器下生成的文件
			if (File.Exists(filePath)) {
				File.Delete(filePath);

			}
			context.Response.End();
		}
	}
}