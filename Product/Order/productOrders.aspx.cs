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

    public partial class productOrders : System.Web.UI.Page
    {

        protected string cad = "", bdate = "", edate = "", txtcity = "",top="";
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected Common.BasePage BasePage = new Common.BasePage();
        StringBuilder sqlstr = new StringBuilder();
        StringBuilder sqlsql = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getRequest();

                try
                {

                    InitParam("");
                    bindData();
                }
                catch { }
            }
        }

        private void getRequest()
        {
            #region 获取参数
            if (Request.Form["QCAD"] != "" && Request.Form["QCAD"] != null)
            {
                cad = Request.Form["QCAD"];
            }
            else if (Request.QueryString["QCAD"] != null)
            {
                cad = Request.QueryString["QCAD"];
            }

            if (Request.Form["bdata"] != "" && Request.Form["bdata"] != null) 
            {
                bdate = Request.Form["bdata"];
            }
            else if (Request.QueryString["bdata"] != null)
            {
                bdate = Request.QueryString["bdata"];
            }

            if (Request.Form["edata"] != "" && Request.Form["edata"] != null)
            {
                edate = Request.Form["edata"];
            }
            else if (Request.QueryString["edata"] != null)
            {
                edate = Request.QueryString["edata"];
            }
            if (Request.Form["TxtCity"] != "" && Request.Form["TxtCity"] != null)
            {
                txtcity = Request.Form["TxtCity"];
            }
            else if (Request.QueryString["TxtCity"] != null)
            {
                txtcity = Request.QueryString["TxtCity"];
            }
            if (Request.Form["Txttop"] != "" && Request.Form["Txttop"] != null)
            {
                top = Request.Form["Txttop"];
            }
            else if (Request.QueryString["Txttop"] != null)
            {
                top = Request.QueryString["Txttop"];
            }
           
           
            #endregion
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

                if (allCount <= pagesize || top != "")
                {
                    literalPagination.Visible = false;
                }
                else
                {
                    literalPagination.Visible = true;
                    literalPagination.Text = BasePage.GenPaginationBar("productOrders.aspx?page=[page]&QCAD=" + cad + "&bdata=" + bdate + "&edata=" + edate + "&TxtCity=" + txtcity + "", pagesize, curpage, allCount);
                } 
            }
        }

        
        private void InitParam(string Index)
        {
            #region 
            if (top == "" || top == "0")
            {
                sinfo.PageSize = 16;
             
            }
            else
            {
                sinfo.PageSize = Convert.ToInt32( top);
                sinfo.Orderby = "ID DESC ";//降序
            }
           

        
            
            
           
                string sqlstring = "";
                sinfo.Sqlstr = " (SELECT  ROW_NUMBER() OVER( ORDER BY num  ) id, XD.CAD, num,number,JHprice ";
                sinfo.Sqlstr += " ,[des] AS modedes FROM (SELECT CAD,SUM(num)num FROM  dbo.product_PlaceOrde";
                sinfo.Sqlstr += " where 1=1 ";
                if (bdate != "")
                {
                    sqlstring = " and  CONVERT(VARCHAR(7),orderdate,23 )>='" + bdate + "' AND  CONVERT(VARCHAR(7),orderdate,23 )<='" + edate + "' ";
                  
                }
                if (cad != "")
                {
                    sqlstring += " and  CAD='" + cad + "' ";
                }
                if (txtcity != "")
                {
                    sinfo.Sqlstr += "  and  KY LIKE '%" + txtcity.Trim() + "%'";
                }

                sinfo.Sqlstr += sqlstring.ToString();
                sinfo.Sqlstr += " GROUP BY CAD )XD LEFT JOIN(  ";


                //---实到----
                sinfo.Sqlstr += " SELECT CAD ,SUM (number) number  ";
                sinfo.Sqlstr += " FROM dbo.product_ActualOrder ";

                sinfo.Sqlstr += " WHERE 1=1 ";
                if (txtcity != "")
                {
                    sinfo.Sqlstr += "  and  City ='"+ txtcity.Trim() + "'";
                }
                sinfo.Sqlstr += sqlstring.ToString();

                sinfo.Sqlstr += " GROUP BY  CAD )SD ON SD.CAD=XD.CAD ";
                sinfo.Sqlstr += "  LEFT JOIN dbo.products  ON XD.CAD = dbo.products.CAD "; 
           sinfo.Sqlstr += " LEFT JOIN  ( SELECT  CAD, MAX(DHdate)PDHdate ,MAX(Purchaseprice)JHprice FROM  ";
           sinfo.Sqlstr += " dbo.product_ActualOrder GROUP BY CAD )price ON   XD.CAD = price.CAD  ";

                sinfo.Sqlstr += ")MM ";
   

          
            if (Index == "")
            {
                #region

                if (!string.IsNullOrEmpty(Request.QueryString["page"]))
                {
                    curpage = int.Parse(Request.QueryString["page"]);
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

            getRequest();

            try
            {
                InitParam("query");
                bindData();
            }
            catch { }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
           
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
                string name = "";
                string ordertype="";
                try
                {
                    if (this.FileUpload1.FileName.Contains("下单"))
                    {
                        ordertype="下单";
                        name = Server.MapPath("~/uploadxls/") + "XDOrder" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";
                    }
                    else
                    {
                        name = Server.MapPath("~/uploadxls/") + "ActualOrder" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";
                    }
                    this.FileUpload1.SaveAs(name);
                    if (File.Exists(name))
                    {

                       
                        DataTable dt = ProductBLL.Search.Searcher.OpenCSV(name);

                        if (dt.Rows.Count > 0)
                        {

                            #region 根据文件判断导入的是下单还是实到数据
                            
                        
                            if (ordertype == "下单")
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        sqlstr.Append("INSERT INTO dbo.product_PlaceOrde( CAD, orderdesc, num, orderdate, KY )VALUES ('" + dt.Rows[i]["CAD"].ToString() + "','" + dt.Rows[i]["desc"].ToString() + "','" + dt.Rows[i]["num"].ToString() + "','" + dt.Rows[i]["date"].ToString() + "','" + dt.Rows[i]["ky"].ToString() + "'); ");
                                    }
                                }
                            }
                            else//实到
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        sqlstr.Append("INSERT INTO dbo.product_ActualOrder ( Orderdate ,DHdate , balancedate ,City , CAD , model , Purchaseprice ,shipmentprice ,number  )VALUES ('" + dt.Rows[i]["下单日期"].ToString() + "','" + dt.Rows[i]["到货日期"].ToString() + "','" + dt.Rows[i]["结算日期"].ToString() + "','" +dt.Rows[i]["城市"].ToString() + "','" + dt.Rows[i]["CAD"].ToString() + "','" + dt.Rows[i]["详细型号"].ToString() + "','" + dt.Rows[i]["进货价"].ToString() + "','" + dt.Rows[i]["出货价"].ToString() + "','" + dt.Rows[i]["数量"].ToString() + "'); ");
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>window.parent.alert('文件无内容');</script>");
                            return;
                        }
                    }
                    if (sqlstr.Length>0)
                    {
                        if (hb.insetpro( sqlstr.ToString()))
                        {
                            Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../Order/productOrders.aspx';</script>");
                            
                            return;
                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>window.parent.alert('上传失败');window.location.href='../Order/productOrders.aspx;</script>");
                            
                            return;
                        }
                    }


                }
                catch 
                {
                    Response.Write("<script type='text/javascript'>window.parent.alert('文件内容格式不正确，请核实')</script>");
                    return;
                }
                finally
                {

                    sqlstr.Clear();
                }
            }
        }
    }
}