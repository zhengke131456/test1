using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using System.Collections;
using System.Configuration;

namespace product.products
{

    public partial class PriceRebateExecl : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected DataTable dtCAD;
     
        protected DataTable dbData;
        protected DataSet ds;
        
        protected void Page_Load(object sender, EventArgs e)
        {
			//if (!IsPostBack)
			//{
               


			//     if (string.IsNullOrEmpty(GetQueryString("page")))//第一次进来刷新报表数据，如果是翻页就不刷新数据 
			//     {
			//        //第一次进来重新生成报表
			//        //hb.ProExecinset("exec Pro_subtotal_New");
			//     }
			//     try
			//     {
                   
			//        InitParam();
			//        bindData();
			//     }
			//    catch { }
            
			//}
            
        }

		/// <summary>
		///  生成报表
		/// </summary>
		/// <returns></returns>
       private   DataTable dt ()
       {
                       #region 生成报表



                        #region 初始化表
        

                    DataTable DThead = new DataTable();
                    DataTable DTPFS = new DataTable();//价格表
                    DataTable DTFL = new DataTable();//返利表

                    DataTable DTYary = new DataTable();//价格表年份
                    DTYary.Columns.Add("PRSP_Year");
                    DataTable DTdate = new DataTable();//返利表月份
                    DTdate.Columns.Add("RE_Date");
                    DataTable dtdes = new DataTable();//

           #endregion
                    #region 获取列进件和返利列
                    
                 

                    IDictionary dictYear = (IDictionary)ConfigurationManager.GetSection("Year");
                    
                        DataRow drd = DTYary.NewRow();
                        if (dictYear.Count > 0)
                        {
                            foreach (string key in dictYear.Keys)
                            {  int n=0;
                            drd[n] = dictYear[key].ToString() + "单价(含税)".Trim();
                               DTYary.Rows.Add(drd);
                               drd = DTYary.NewRow();
                               n++;
                            }

                        }
                        

                        IDictionary dictmonth = (IDictionary)ConfigurationManager.GetSection("month");

                        drd = DTdate.NewRow();
                        if (dictmonth.Count > 0)
                        {
                            foreach (string key in dictmonth.Keys)
                            {
                                int n = 0;
                                drd[n] = dictmonth[key].ToString() + "返利".Trim();
                                DTdate.Rows.Add(drd);
                                drd = DTdate.NewRow();
                                n++;
                            }

                        }
                        DataView dvv = DTdate.DefaultView;
                        dvv.Sort = "RE_Date";
                        DTdate = dvv.ToTable();
                    #endregion

                    dtdes = hb.getdate(" CAD,[des]", "dbo.products");
                    dtCAD = hb.getdate("DISTINCT  basename", "dbo.baseinfo WHERE [type]=1 ORDER BY basename");

					string sqlstr = " ProductPrice LEFT JOIN ( SELECT MAX(ISNULL(PRSP_number,0)) as maxnumber, PRSP_CAD  AS CAD , ";
		  sqlstr += "  PRSP_Year AS newYear FROM  dbo.ProductPrice GROUP BY  PRSP_CAD,PRSP_Year   ";
		    sqlstr += " )newProductPrice ON dbo.ProductPrice.PRSP_CAD = newProductPrice.CAD  ";
		       sqlstr += "  WHERE maxnumber=dbo.ProductPrice.PRSP_number AND newYear=dbo.ProductPrice.PRSP_Year ";



			   DTPFS = hb.getdate("DISTINCT PRSP_CAD,PRSP_Year,PRSP_Inprice", sqlstr);//每年最新进货价

                    DTFL = hb.getdate("RE_CAD ,SUM(ISNULL(RE_1,0)+ISNULL(RE_2,0)) AS RE,RE_Date", "dbo.Rebate GROUP BY RE_CAD,RE_Date");//每月的返利

                    

                    #region  生成标题列

                   

                    DThead.Columns.Add("编号");//
                    DThead.Columns.Add("CAD");//
                    DThead.Columns.Add("DES");//
                    for (int i = 0; i < DTYary.Rows.Count; i++)
                    {
                        DThead.Columns.Add(DTYary.Rows[i]["PRSP_Year"].ToString().Trim());
                    }
                    for (int i = 0; i < DTdate.Rows.Count; i++)
                    {
                        DThead.Columns.Add(DTdate.Rows[i]["RE_Date"].ToString().Trim());
                    }
                    #endregion

                    DataRow dr = DThead.NewRow();

                    for (int i = 0; i < dtdes.Rows.Count; i++)
                    {
                        dr["编号"] = i + 1;
                        dr["CAD"] = dtdes.Rows[i]["CAD"].ToString().Trim();

                        #region 进价
                        
                        
                        //获取当前CAD进价
                        DataRow[] drINprice = DTPFS.Select("PRSP_CAD='" + dtdes.Rows[i]["CAD"].ToString().Trim()+"'");
                        //获取des列
                        DataRow[] drdes = dtdes.Select("CAD='" + dtdes.Rows[i]["CAD"].ToString().Trim() + "'");

                        if (drINprice.Count() > 0)
                        {
                            for (int n = 0; n < drINprice.Count(); n++)
                            {
                                string yary = drINprice[n]["PRSP_Year"].ToString();
                                yary += "单价(含税)";//列名

                                DataRow[] drisyary = DTYary.Select("PRSP_Year='" + yary + "'");
                                if (drisyary.Count() > 0)//标示有这个列需要添加数据
                                {
                                    dr[yary] = drINprice[n]["PRSP_Inprice"].ToString();
                                }

                            }
                        }
                        if (drdes.Count() > 0)
                        {
                            dr["DES"] = drdes[0]["des"].ToString();
                        }
                        #endregion

                        #region 返利
                        //获取当前CAD返利
                        DataRow[] drfl = DTFL.Select("RE_CAD='" + dtdes.Rows[i]["CAD"].ToString().Trim() + "'");

                        if (drfl.Count() > 0)
                        {
                            for (int n = 0; n < drfl.Count(); n++)
                            {
                                string fl = drfl[n]["RE_Date"].ToString();
                                fl += "返利";//列名
                                DataRow[] dtisdate = DTdate.Select("RE_Date='" + fl + "'");
                                if (dtisdate.Count() > 0)//有这个列需要添加数据
                                {
                                    if (drfl[n]["RE"].ToString().Trim() != "0.00")
                                    {
                                        dr[fl] = drfl[n]["RE"].ToString();
                                    }
                                   

                                   
                                }
                               
                            }
                        }
                        
                        #endregion

                        DThead.Rows.Add(dr);
                        dr = DThead.NewRow();


                    }

                    #endregion
                    //DataView dv = DThead.DefaultView;
                    //dv.Sort = "编号";
                    //DataTable tb3 = dv.ToTable();
                    return DThead;
       }


        private void bindData()
        {

            ds = getData();

            if (!ds.Equals(null))
            {
                allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                lblCount.Text = allCount.ToString();

                dbData = ds.Tables[1];
            
                dislist.DataSource = dbData;
                dislist.DataBind();
                if (allCount <= pagesize)
                {
                    literalPagination.Visible = false;
                }
                else
                {
                    literalPagination.Visible = true;
                    literalPagination.Text = GenPaginationBar("PriceRebateExecl.aspx?page=[page]", pagesize, curpage, allCount);
                }
            }
        }
       
        private void InitParam()
        {
            #region
            sinfo.PageSize = 20;
            sinfo.Tablename = "CacheTable";
            sinfo.Orderby = "C_type,内部唯一代码";
           

            if (!string.IsNullOrEmpty(GetQueryString("page")))
            {
                curpage = int.Parse(GetQueryString("page"));
                sinfo.PageIndex = curpage;
            }



            #endregion
        }



        /// <summary>
        /// 信息
        /// </summary>
        /// <returns></returns>
        private DataSet getData()
        {
            DataSet dt = new DataSet();
            dt = hb.GetprodList(sinfo);

            return dt;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
         
            
            string path = Server.MapPath("~/downloadxlsx/") + "Eexcl" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";



            //ProductBLL.download.ExcelHelper.TableToExcelForXLSX(DT, pathcsv);
            //ProductBLL.download.ExcelHelper.TableToExcelForXLSX(DT, xls);
            //string ss = " ( SELECT    ROW_NUMBER() OVER ( ORDER BY C_type, 内部唯一代码 ) AS 序号,* FROM  CacheTable WHERE     1 = 1) AS aa";


			if (ProductBLL.download.ExcelHelper.TableToCsv(dt(), path))
            {
                downloadfile(path);
                Response.Write("<script type='text/javascript'>window.parent.alert('导出EXecl成功!')</script>");

                return;
            }
            else
            {
                Response.Write("<script type='text/javascript'>window.parent.alert('导出Execl失败!');window.location.href='../products/PriceRebateExecl.aspx';</script>");
                
                return;
            }

        }
     
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (hb.ProExecinset(" exec  Pro_subtotal_New"))
            {
                InitParam();
                bindData();
              
                Response.Write("<script type='text/javascript'>window.parent.alert('刷新成功!')</script>");
                return;
            }
            else
            {
                Response.Write("<script type='text/javascript'>window.parent.alert('刷新失败!')</script>");
                return;
            }
        }
        /// <summary>
        /// 浏览器下载
        /// </summary>
        /// <param name="s_path"></param>
        void downloadfile(string s_path)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(s_path);
            HttpContext.Current.Response.ContentType = "application/ms-download";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            HttpContext.Current.Response.WriteFile(file.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            //下载完成后删除服务器下生成的文件
            if (File.Exists(s_path))  
            {
                File.Delete(s_path);
 
            }
            HttpContext.Current.Response.End();
        }

        protected void dislist_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (e.Item.ItemType == ListItemType.Header)
            {
                sb.Append("<tr width=\"100%\">");

                DataColumn col = dbData.Columns[dbData.Columns.Count-1];//取从新排序后的序号作为ID
                string name ="ID";
                sb.AppendFormat("<th width=\"20px\" >{0}</th>", name);

                col = dbData.Columns[2];
                name = col.ColumnName;
                sb.AppendFormat("<th width=\"250px\">{0}</th>", name);
                //for (int i = 2; i < 20; i++)
                for (int i = 3; i < dbData.Columns.Count-1; i++)
                {
                    col = dbData.Columns[i];
                    name = col.ColumnName;
                    sb.AppendFormat("<th width=\"80px\">{0}</th>", name);
                }
               
              
                sb.Append("</tr>");
                Literal literal = (Literal)e.Item.FindControl("lit_head");
                literal.Text = sb.ToString();
                sb.Clear();
            }
            else if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int index = e.Item.ItemIndex;
                sb.Append("<tr>");
             
                sb.AppendFormat("<td>{0}</td>", dbData.Rows[index][dbData.Columns.Count-1]);
                sb.AppendFormat("<td>{0}</td>", dbData.Rows[index][2]);
               //for (int k = 2; k <20; k++)
               for (int k = 3; k < dbData.Columns.Count-1; k++)
                {
                    sb.AppendFormat("<td>{0}</td>", dbData.Rows[index][k]);
                }

               
               
                sb.Append("</tr>");

                Literal liter = (Literal)e.Item.FindControl("lit_item");
                liter.Text = sb.ToString();
            }
        }
    }
}