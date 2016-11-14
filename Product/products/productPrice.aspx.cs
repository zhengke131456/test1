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

namespace product.products
{

    public partial class productPrice : Common.BasePage
    {

        protected string cad = "",  Bdate;
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
    
        StringBuilder sqlCad = new StringBuilder();//查看编码+年份是否重复
        StringBuilder sqlstock = new StringBuilder();//库存盈亏
        protected int n = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                      bind();
                    if (GetQueryString("model") != "")
                    {
                        dpmodel.SelectedValue = GetQueryString("model");
                    }
                    if ( GetQueryString("dpyeary") != "")
                    {
                        dpyary.SelectedValue = GetQueryString("dpyeary");
                    }
                    if (GetQueryString("CAD") != "")
                    {
                        cad = GetQueryString("CAD");
                    }
                try
                {
                    
                    InitParam("");
                    bindData();
                }
                catch { }
            }
        }

        private void bind()
        {

            dpmodel.DataSource = hb.getdate(" DISTINCT basename", "dbo.baseinfo WHERE  type=3");
            dpmodel.DataTextField = "basename";
            dpmodel.DataBind();
            dpmodel.Items.Insert(0, "");


            dpyary.DataSource = hb.getdate(" DISTINCT Pry_Yary", "dbo.productYear WHERE Pry_type=1");
            dpyary.DataTextField = "Pry_Yary";
            dpyary.DataBind();
            dpyary.Items.Insert(0, "");

        }
        private void bindData()
        {




            DataSet ds = getData();

            if (!ds.Equals(null))
            {
                allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                lblCount.Text = allCount.ToString();

                DataTable dt = ds.Tables[1];
                dt.Columns.Add("show");


                dislist.DataSource = ds.Tables[1];
                dislist.DataBind();
                if (allCount <= pagesize)
                {
                    literalPagination.Visible = false;
                }
                else
                {
                    literalPagination.Visible = true;
                    literalPagination.Text = GenPaginationBar("productPrice.aspx?page=[page]&CAD=" + cad + "&model=" + dpmodel.SelectedItem.Text + "&dpyeary=" + dpyary.SelectedItem.Text + "", pagesize, curpage, allCount);
                } 
            }
        }

        private void panduan()
        {
            if (dpyary.SelectedItem.Text == "请选择")
            {
                dpyary.SelectedItem.Text = "";
            }
            if (dpmodel.SelectedItem.Text == "请选择")
            {
                dpmodel.SelectedItem.Text = "";
            }
        }
        private void InitParam(string Index)
        {
            #region 
            sinfo.PageSize = 20;
            //sinfo.Tablename = "ProductPrice";
            sinfo.Orderby = "ID";
            sinfo.Sqlstr = " (SELECT dbo.ProductPrice.ID, PR_ID ,PRSP_CAD ,model,[des] ,PRSP_Inprice ,PRSP_WHSPrice ,PRSP_Year ,PRSP_Inserttime ,PYType,PRSP_number FROM dbo.ProductPrice Left JOIN  dbo.products ON CAD=PRSP_CAD ";
            if (dpyary.SelectedItem.Text != "")
            {
                sinfo.Sqlstr += " where  PRSP_Year = '" + dpyary.SelectedItem.Text + "'";
            }
            else
            {
                sinfo.Sqlstr += " where 1=1 ";
            }

            if (cad != "")
            {
                sinfo.Sqlstr += " and  PRSP_CAD LIKE'%" + cad + "%' ";
            }

            if (dpmodel.SelectedItem.Text != "")
            {
                sinfo.Sqlstr += " and  model ='" + dpmodel.SelectedItem.Text + "'  )HH  ";
            }
            else
            {
                sinfo.Sqlstr += "  )HH ";
            }

            if (Index == "")
            {
                #region

                if (!string.IsNullOrEmpty(GetQueryString("page")))
                {
                    curpage = int.Parse(GetQueryString("page"));
                    sinfo.PageIndex = curpage;
                }
                #endregion
            }
            else
            {

                sinfo.PageIndex = 1;
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
            dt = hb.QXGetprodList(sinfo);

            return dt;
        }

        protected void btnQuerey_Click(object sender, EventArgs e)
        {
           // panduan();
            if (!string.IsNullOrEmpty(Request["QCAI"]))
            {
                cad = Request.Form["QCAI"].Trim();
            }
            try
            {
                InitParam("query");
                bindData();
            }
            catch { }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable ProductPrice = DataMgr.GetTableProPrice();
            DataRow drproduct;
            bool fileIsValid = false;
            //如果确认了文件上传，则判断文件类型是否符合要求
            if (this.FileUpload1.HasFile)
            {
                //获取上传文件的后缀名
                String fileExtension = System.IO.Path.GetExtension(this.FileUpload1.FileName).ToLower();//ToLower是将Unicode字符的值转换成它的小写等效项

                //判断文件类型是否符合要求

                if (fileExtension == ".csv")
                {
                    fileIsValid = true;
                }
                else
                {
                    Response.Write("<script type='text/javascript'>window.parent.alert('文件格式不正确!请上传正确格式的文件')</script>");
                    return;
                }

            }
            //如果文件类型符合要求，则用SaveAs方法实现上传，并显示信息
            if (fileIsValid == true)
            {
               
                try
                {
                    string name = Server.MapPath("~/uploadxls/") + "ProPrice" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";
                    this.FileUpload1.SaveAs(name);
                    if (File.Exists(name))
                    {

                       
                        DataTable dt = ProductBLL.Search.Searcher.OpenCSV(name);
                        if (dt.Rows.Count > 0)
                        {   
                            #region    先检测execl 是否存在数据重复，然后在和表对比
                           // 检测CAD(编码)编码列+年份 是否有重复 如果有重复就终止操作并提示重复记录
                            var CADPrice = from row in dt.Rows.Cast<DataRow>()

                                           group row by new { CP_CAD = row["CAD"].ToString(), CP_Yeat = row["year"].ToString(), } into result
                                 select new { Peo = result.Key, Count = result.Count() };

                            foreach (var group in CADPrice)
                            {
                                if (Convert.ToInt32(group.Count) > 1)
                                {
                                    sqlCad.Append( group.Peo.CP_CAD + "年份:" + group.Peo.CP_Yeat + ",");
                                }
                            }





                            if (sqlCad.Length == 0)//导入的Exec编码没有重复项
                            {
                                string Year=dt.Rows[0]["year"].ToString();//导入的年份
                                string username = userCode();
                                int number = Convert.ToInt32( dt.Rows[0]["batch"].ToString());//次数
                                double NewInprice =0;//当前进货价
                                double OldInprice = 0;//上一次进货价
                                double profit = 0;//盈亏额

                                //string str = "PRSP_CAD,PRSP_Year,PRSP_Inprice,PRSP_WHSPrice ";
                                //string table = " (SELECT * FROM   ProductPrice WHERE  ID in (SELECT MAX(id) AS id FROM dbo.ProductPrice where PRSP_Year='" + Year + "' GROUP BY PRSP_CAD  ,PRSP_Year))ProductPrice"; //最后一次导入的价格表

                              DataTable dbProductPrice =   hb.getProdatable(
 "SELECT dbo.ProductPrice.PRSP_CAD,dbo.ProductPrice.PRSP_Year,PRSP_Inprice,PRSP_WHSPrice,PRSP_number FROM  dbo.ProductPrice  INNER JOIN (SELECT PRSP_CAD ,PRSP_Year, MAX(PRSP_number) AS number FROM  dbo.ProductPrice WHERE  PRSP_Year='" + Year + "' GROUP BY  PRSP_CAD,PRSP_Year) h ON dbo.ProductPrice.PRSP_CAD = h.PRSP_CAD WHERE h.PRSP_Year=dbo.ProductPrice.PRSP_Year AND number=dbo.ProductPrice.PRSP_number");//当前年份最后一次导入数据


                                //string table = "ProductPrice where PRSP_Year='" + Year + "'and  PRSP_number='" + number + " ";
                               // DataTable dbProductPrice = hb.getdate(str, table);//价格表数据库表


                                string tableinfo = "baseinfo where type=1";
                                DataTable dbbaseinfo = hb.getdate("ID,basename", tableinfo);//返回编码 
                                hb.ProExecinset("DELETE dbo.stockprofit_detailed WHERE spdyear='" + Year + "';DELETE dbo.Stock_totalprofit WHERE stpYear='" + Year + "'");//先删除当前年份库存盈亏明细和汇总表
                                #region 生成盈亏明细


                               
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {  
                                    DataRow[] RowID = dbbaseinfo.Select("basename='" + dt.Rows[i]["CAD"].ToString() + "'");

                                    if (RowID.Count() == 0)//编码不存在
                                    {
                                        sqlCad.Append(dt.Rows[i]["CAD"].ToString() + ",");
                                        n = 1;
                                        break;

                                    }
                                    else
                                    {
                                        #region 判断是否需要导入  //批发价和进货价 一样 就不计算库存盈亏

                                        if (number != 1)
                                        {
                                            string sss = "PRSP_CAD='" + dt.Rows[i]["CAD"].ToString() + "' and PRSP_WHSPrice='" + dt.Rows[i]["WHSprice"].ToString() + "' and PRSP_Inprice='" + dt.Rows[i]["Inprice"].ToString() + "'";
                                            DataRow[] sssRow = dbProductPrice.Select(sss);
                                            if (sssRow.Count() == 0) //价格不同 
                                            {
                                                string ss = "PRSP_CAD='" + dt.Rows[i]["CAD"].ToString() + "' and PRSP_Year='" + dt.Rows[i]["year"].ToString() + "'";
                                                DataRow[] sRow = dbProductPrice.Select(ss);

                                                if (sRow.Count() != 0)//和ProductPrice 数据对比 如果价格表已经存在该数据
                                                {

                                                    //sqlCad.Append(dt.Rows[i]["CAD"].ToString() + "年份:" + dt.Rows[i]["year"].ToString() + ",");

                                                    for (int k = 0; k < sRow.Count(); k++)
                                                    {
                                                        NewInprice = Convert.ToDouble(dt.Rows[i]["Inprice"]);
                                                        OldInprice = Convert.ToDouble(sRow[k]["PRSP_Inprice"]);
                                                        profit = NewInprice - OldInprice;//进货价差额

                                                    }
                                                     hb.ProExecinset("exec pro_stockprofit '" + dt.Rows[i]["CAD"].ToString() + "'," + OldInprice + "," + NewInprice + "," + profit + ",'" + Year + "','" + username + "'");
                                                    


                                                }

                                                drproduct = ProductPrice.NewRow();
                                                drproduct[0] = dt.Rows[i]["CAD"].ToString();
                                                drproduct[1] = dt.Rows[i]["WHSprice"].ToString();
                                                drproduct[2] = Convert.ToDecimal(dt.Rows[i]["Inprice"].ToString());
                                                drproduct[3] = dt.Rows[i]["year"].ToString();
                                                drproduct[4] = RowID[0]["ID"].ToString();//编码ID
                                                drproduct[5] = dt.Rows[i]["batch"].ToString();//导入次数
                                                ProductPrice.Rows.Add(drproduct);
                                              

                                            }
                                           

                                        }
                                        else
                                        {
                                            drproduct = ProductPrice.NewRow();
                                            drproduct[0] = dt.Rows[i]["CAD"].ToString();
                                            drproduct[1] = dt.Rows[i]["WHSprice"].ToString();
                                            drproduct[2] = Convert.ToDecimal(dt.Rows[i]["Inprice"].ToString());
                                            drproduct[3] = dt.Rows[i]["year"].ToString();
                                            drproduct[4] = RowID[0]["ID"].ToString();//编码ID
                                            drproduct[5] = dt.Rows[i]["batch"].ToString();//导入次数
                                            ProductPrice.Rows.Add(drproduct);
                                           
                                        }
                                        #endregion






                                    }
                                }
                                #endregion

                                #region  执行插入

                                if (sqlCad.Length == 0)//如果没有重复项
                                {
                                    
                                      
                                        if (DataMgr.BulkToDBProPrice(ProductPrice) && hb.insetpro("INSERT INTO dbo.Stock_totalprofit( stockID, totalprofit,stpYear ) SELECT *,'" + Year + "' FROM (SELECT storckId,SUM(ISNULL(profitprice,0))AS profitprice FROM  dbo.stockprofit_detailed WHERE  spdyear='" + Year + "' GROUP BY storckId)a"))
                                        {
                                            Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../products/productPrice.aspx';</script>");
                                        }
                                        else
                                        { Response.Write("<script type='text/javascript'>window.parent.alert('上传失败');</script>"); }
                                
                                  

                                }
                                else
                                {
                                    if (n == 0)
                                    {
                                        Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！上传数据和数据库存在重复：" + sqlCad.ToString() + "');</script>");
                                    }
                                    else
                                    {
                                        Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！请在编码列表处维护编码,新编码：" + sqlCad.ToString() + "');</script>");
                                    }
                                   

                                    sqlCad.Clear();
                                    ProductPrice.Clear();
                                    return;
                                }

                                #endregion
                            }
                            else
                            {
                               
                              Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！产品价格存在重复：" + sqlCad.ToString() + "');</script>");
                              
                                sqlCad.Clear();
                                ProductPrice.Clear();
                                return;
 
                            }

                           

                          
                            #endregion
                            
                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>window.parent.alert('文件无内容');</script>");
                            return;
                        }
                    }

                }
                catch ( Exception ex)
                {
                    ex.Message.ToString();
                    Response.Write("<script type='text/javascript'>window.parent.alert('文件内容格式不正确，请核实')</script>");
                    return;
                }
                finally
                {
                    ProductPrice.Dispose();
                }
            }
        }
    }
}