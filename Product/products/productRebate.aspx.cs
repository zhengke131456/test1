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

    public partial class Rebate : Common.BasePage
    {

        protected string cad = "";
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected int n = 0;
        StringBuilder sqlCad = new StringBuilder();//查看编码+月份是否重复
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
                if (GetQueryString("model") != "")
                {
                    dpmodel.SelectedValue = GetQueryString("model");
                }
                if (GetQueryString("dpyeary") != "")
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


            dpyary.DataSource = hb.getdate(" DISTINCT Pry_Yary", "dbo.productYear WHERE Pry_type=0");
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
                    literalPagination.Text = GenPaginationBar("productRebate.aspx?page=[page]&CAD="+cad+"&model=" + dpmodel.SelectedItem.Text + "&dpyeary=" + dpyary.SelectedItem.Text + "", pagesize, curpage, allCount);
                } 
            }
        }


        private void InitParam( string Index)
        {
            #region 
            sinfo.PageSize = 20;
            //sinfo.Tablename = "Rebate"; 
            sinfo.Orderby = "id";
            sinfo.Sqlstr = " (SELECT dbo.Rebate.ID, PR_ID ,RE_CAD , RE_1 ,RE_2 ,RE_3 , RE_4 , RE_5 ,RE_Date , RE_inserttime , RE_type ,model,[des]  FROM dbo.Rebate Left JOIN  dbo.products ON CAD=RE_CAD ";
            if (dpyary.SelectedItem.Text != "")
            {
                sinfo.Sqlstr += " where  RE_Date = '" + dpyary.SelectedItem.Text + "'";
            }
            else
            {
                sinfo.Sqlstr += " where 1=1 ";
            }

            if (cad != "")
            {
                sinfo.Sqlstr += " and  RE_CAD LIKE'%" + cad + "%' ";
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
            DataTable Rebate = DataMgr.GetTableRebate();
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
                         
                            var CADPrice = from row in dt.Rows.Cast<DataRow>()

                                           group row by new { CP_CAD = row["CAD"].ToString(), CP_Yeat = row["Date"].ToString(), } into result
                                           select new { Peo = result.Key, Count = result.Count() };

                            foreach (var group in CADPrice)
                            {
                                if (Convert.ToInt32(group.Count) > 1)
                                {
                                    sqlCad.Append(group.Peo.CP_CAD + "：" + group.Peo.CP_Yeat + ",");
                                }
                            }





                            if (sqlCad.Length == 0)//没有重复项
                            {
                                string str = "RE_CAD,RE_Date";
                                string table = "Rebate";
                              
                                DataTable dbRebate = hb.getdate(str, table);//数据库表

                                string tableinfo = "baseinfo where type=1";
                                DataTable dbbaseinfo = hb.getdate("ID,basename", tableinfo);//返回编码

                                #region 和数据库表对比
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {

                                    string ss = "RE_CAD='" + dt.Rows[i]["CAD"].ToString() + "' and RE_Date='" + dt.Rows[i]["Date"].ToString() + "'";
                                    DataRow[] sRow = dbRebate.Select(ss);


                                    if (sRow.Length != 0)//和Rebate 数据对比
                                    {
                                        sqlCad.Append(dt.Rows[i]["CAD"].ToString() + ":" + dt.Rows[i]["Date"].ToString() + ",");
                                    }
                                    else
                                    {
                                        DataRow[] RowID = dbbaseinfo.Select("basename='" + dt.Rows[i]["CAD"].ToString() + "'");

                                        if (RowID.Count() == 0)
                                        {
                                            sqlCad.Append(dt.Rows[i]["CAD"].ToString() + ",");
                                            n = 1;
                                        }
                                        else
                                        {
                                            drproduct = Rebate.NewRow();
                                            drproduct[0] = dt.Rows[i]["CAD"].ToString();
                                            drproduct[1] = dt.Rows[i]["back1"].ToString() == "" ? 0 : Convert.ToDecimal(dt.Rows[i]["back1"].ToString());
                                            drproduct[2] = dt.Rows[i]["back2"].ToString() == "" ? 0 : Convert.ToDecimal(dt.Rows[i]["back2"].ToString());
                                            drproduct[3] = dt.Rows[i]["back3"].ToString() == "" ? 0 : Convert.ToDecimal(dt.Rows[i]["back3"].ToString());
                                            drproduct[4] = dt.Rows[i]["back4"].ToString() == "" ? 0 : Convert.ToDecimal(dt.Rows[i]["back4"].ToString());
                                            drproduct[5] = dt.Rows[i]["back5"].ToString() == "" ? 0 : Convert.ToDecimal(dt.Rows[i]["back5"].ToString());
                                            drproduct[6] = dt.Rows[i]["Date"].ToString();
                                            drproduct[7] = RowID[0]["ID"].ToString();
                                            Rebate.Rows.Add(drproduct);
                                        }
                                    }
                                }
                                #endregion

                                #region  执行插入

                                if (sqlCad.Length == 0)//如果没有重复项
                                {

                                    //if (hb.insetpro(sqlpin.ToString()))
                                    if (DataMgr.BulkToDBRebate(Rebate))
                                    {
                                        Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../products/productRebate.aspx';</script>");
                                        Rebate.Clear();
                                        return;
                                    }
                                    else
                                    {
                                        Response.Write("<script type='text/javascript'>window.parent.alert('上传失败');</script>");
                                        return;
                                    }

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
                                    Rebate.Clear();
                                    return;
                                }

                                #endregion
                            }
                            else
                            {

                                Response.Write("<script type='text/javascript'>window.parent.alert('上传失败！返利表存在重复：" + sqlCad.ToString() + "');</script>");

                                sqlCad.Clear();
                                Rebate.Clear();
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
                catch 
                {
                     
                    //Response.Write("<script type='text/javascript'>window.parent.alert('文件内容格式不正确：" + Label1.Text.ToString() + "');</script>");
                    Response.Write("<script type='text/javascript'>window.parent.alert('文件内容格式不正确')</script>");
                    return;
                }
                finally
                {
                    Rebate.Dispose();
                }
            }
        }
    }
}