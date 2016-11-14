using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Collections;
using System.Configuration;

namespace product.products
{
    public partial class outproductList : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected string cai = "", Edate, Bdate;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        StringBuilder sqloutID = new StringBuilder();
        StringBuilder sqlspin = new StringBuilder();
        protected StringBuilder result = new StringBuilder();
        StringBuilder sqlsql = new StringBuilder(); //记录ca1
        protected string ckids = "";//选中仓库编码串

        protected string showp = "1";//是否有操作 批量的权限
        protected void Page_Load(object sender, EventArgs e)
        {
            //Bdate = GetQueryString("bdate");
            //Edate = GetQueryString("edate");
            //cai = GetQueryString("q_cai");

            //是否有批量权限
            //string partRights = isPartcode();//当前角色
            //DataTable dt = hb.GetDataSet(" select  * from basestore WHERE   Basecode  IN ( SELECT SR_storecode  FROM  dbo.SYS_RightStore where SP_code='" + partRights + "' AND [type]= '0' ) ").Tables[0];
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (dt.Rows[i]["Fcode"].ToString() == "0")
            //    {
            //        showp = "1";
            //        break;
            //    }
            //}

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (dt.Rows[i]["Fcode"].ToString() == "0")
            //    {
            //        showp = "1";
            //        break;
            //    }
            //}


            if (!IsPostBack)
            {
                hiddpart.Value = isPartcode();
                try
                {
                    getvalue();
                    InitParam("");
                    bindData();
                }
                catch { }
            }
        }


        private void bindData()
        {
            DataTable rpcodedb = hb.getdate("rpcode,rpcode AS rpname", "dbo.products");
            result.Append("[");
            for (int i = 0; i < rpcodedb.Rows.Count; i++)
            {
                result.Append("{ value: \"" + rpcodedb.Rows[i]["rpcode"].ToString() + "\",label:\" " + rpcodedb.Rows[i]["rpname"].ToString() + "\"},");
            }
            result.Remove(result.Length - 1, 1);//把最后一个，给移除


            result.Append(" ]");

            DataSet ds = getData();

            if (!ds.Equals(null))
            {
                allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                lblCount.Text = allCount.ToString();

                DataTable dt = ds.Tables[1];


                //特殊标志转化（文字）
                dt.Columns.Add("markName");
                IDictionary dictmark = (IDictionary)ConfigurationManager.GetSection("spmark");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 1; j <= dictmark.Count; j++)
                    {
                        string type = "type" + j;
                        if (dt.Rows[i]["spmark"].ToString() == j.ToString())
                        {
                            dt.Rows[i]["markName"] = dictmark[type].ToString();
                        }
                    }

                    if (dt.Rows[i]["saleaddress"].ToString().Length > 8)
                    {
                        dt.Rows[i]["saleaddress"] = dt.Rows[i]["saleaddress"].ToString().Substring(0, 7) + "..";
                    }
                }

                dislist.DataSource = dt;
                dislist.DataBind();
                if (allCount <= pagesize)
                {
                    literalPagination.Visible = false;
                }
                else
                {
                    literalPagination.Visible = true;
                    literalPagination.Text = GenPaginationBar("outproductList.aspx?page=[page]&bdate=" + Bdate + "&edate=" + Edate + "&QCAI=" + cai + "&ckids=" + ckids + "", pagesize, curpage, allCount);
                }
            }
        }

        //初始化参数
        private void InitParam(string Index)
        {
            #region

            sinfo.Sqlstr = "  ( SELECT INbatch,dbo.outproduct.usercode ,dbo.outproduct.saleaddress ,dbo.outproduct.insettime ,dbo.outproduct.id ,dbo.outproduct.rpcode ,dbo.products.ShGoogcode,dbo.products.CAD , OD , QTY ,WHcode ,spmark , basename AS  basename ";
            sinfo.Sqlstr += " FROM dbo.outproduct INNER JOIN  dbo.SYS_RightStore ON  WHcode= SR_storecode     LEFT JOIN dbo.BaseStore  ON WHcode=Basecode  LEFT JOIN dbo.products ON dbo.outproduct.rpcode =dbo.products.rpcode ";
            sinfo.Sqlstr += "	WHERE SP_code='" + hiddpart.Value + "'  ";

            //if (ispartRights() != "系统管理员")
            //{
            //    sinfo.Sqlstr += " AND usercode='" + userCode() + "' ";
            //}

            //选中仓库
            string basecodes = "";
            if (ckids != "")
            {
                string[] ck = ckids.Split(new char[] { ',' });

                for (int i = 0; i < ck.Length; i++)
                {
                    if (i == 0)
                    {
                        basecodes += " and  (";
                    }
                    if (i == ck.Length - 1)
                    {
                        basecodes += " whcode = '" + ck[i] + "' ) ";
                    }
                    else
                    {
                        basecodes += " whcode = '" + ck[i] + "' or  ";
                    }
                }
            }

            if (basecodes != "")
            {
                sinfo.Sqlstr += basecodes;
            }


            if (!string.IsNullOrEmpty(Bdate) && !string.IsNullOrEmpty(Edate))
            {
                sinfo.Sqlstr += " and   CONVERT(VARCHAR(20), OD, 23) BETWEEN '" + Bdate + "' AND  '" + Edate + "' ";
            }
            if (cai != "")
            {
                sinfo.Sqlstr += " and  outproduct.rpcode LIKE'%" + cai + "%' )hh ";
            }
            else
            {
                sinfo.Sqlstr += " )hh ";
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

            //下载
            HiddenField1.Value = "select spmark,rpcode,CAD,OD,QTY,WHcode,basename,usercode,insettime,saleaddress  from  " + sinfo.Sqlstr + " order by id desc";
            return dt;
        }
        protected void btnQuerey_Click(object sender, EventArgs e)
        {
            getvalue();
            try
            {
                InitParam("query");
                bindData();
            }
            catch { }
        }

        private void getvalue()
        {
            //仓库
            sinfo.PageSize = 20;
            if (!string.IsNullOrEmpty(GetQueryString("page")))
            {
                curpage = int.Parse(GetQueryString("page"));
                sinfo.PageIndex = curpage;
            }

            if (!string.IsNullOrEmpty(GetQueryString("ckids")))
            {
                ckids = GetQueryString("ckids");
            }

            if (!string.IsNullOrEmpty(GetQueryString("QCAI")))
            {
                cai = GetQueryString("QCAI");
            }
            else cai = "";
            if (!string.IsNullOrEmpty(GetQueryString("bdata")))
            {
                Bdate = GetQueryString("bdata").Trim();
            }
            else Bdate = "";
            if (!string.IsNullOrEmpty(Request["edata"]))
            {
                Edate = GetQueryString("edata").Trim();
            }
            else Edate = "";
           
        }


        //批量出库
        protected void Button1_Click(object sender, EventArgs e)
        {
            bool fileIsValid = false;
            //如果确认了文件上传，则判断文件类型是否符合要求

            #region MyRegion


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
            #endregion

            //如果文件类型符合要求，则用SaveAs方法实现上传，并显示信息
            if (fileIsValid == true)
            {
                try
                {
                    string name = Server.MapPath("~/uploadxls/") + "OUT_批量入库_"+ Username +"_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";
                    this.FileUpload1.SaveAs(name);
                    if (File.Exists(name))
                    {
                        DataTable dt = ProductBLL.Search.Searcher.OpenCSV(name);
                        int qty = 0; int numsl = 0;

                        string usercode = userCode();
                        string storecode = "";
                        string tsbs = "";
                        //string s = "";

                        #region 判断 数据是否合法

                        DataTable isrpcode = hb.getdate("rpcode,cad ", "dbo.products");  //

                        string mesDis = "";//错误信息
                        string inum = "";//操作数量
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            storecode = dt.Rows[i]["仓库编码"].ToString();

                            #region 睿配编码,CAD (存在一者便可，如同时存在按睿配编码为准）

                            //两者都存在已睿配编码为准。
                            string srpcode = dt.Rows[i]["睿配编码"].ToString();
                            string scad = dt.Rows[i]["供应商编码"].ToString();

                            if (srpcode == "" && scad == "")
                            {
                                mesDis = "第" + (i + 1).ToString() + "条记录，睿配编码与供应商编码不能同时为空。";
                                Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                return;
                            }

                            if (srpcode != "")
                            {
                                DataRow[] drrp = isrpcode.Select("rpcode='" + srpcode + "'");


                                if (drrp.Length <= 0)
                                {
                                    mesDis = "第" + (i + 1).ToString() + "条记录，睿配编码: " + srpcode + " 不存在。";
                                    Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                    return;
                                }
                            }
                            else
                            {
                                DataRow[] drrd = isrpcode.Select("CAD='" + scad + "'");

                                if (drrd.Length <= 0)
                                {
                                    mesDis = "第" + (i + 1).ToString() + "条记录，供应商编码: " + srpcode + " 不存在。";
                                    Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                    return;
                                }
                            }

                            #endregion

                            #region 根据角色判断是否有该仓库权限 4

                            DataTable dtstore = hb.getdate("SR_storecode", "dbo.SYS_RightStore WHERE  SP_code='" + hiddpart.Value + "'");  //可操作的仓库编码

                            DataRow[] dr = dtstore.Select("SR_storecode='" + storecode + "'");
                            if (dr.Length <= 0)
                            {

                                //s = 4;
                                //sqlinID.Append(storecode + ",");
                                //break;

                                mesDis = "第" + (i + 1).ToString() + "条记录，当前用户没仓库: " + storecode + "的操作权限或仓库名输入不正确。";
                                Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                return;
                            }

                            #endregion

                            #region 库存数只能为正数,到库日期必写，时间格式校正。
                            try
                            {
                                inum = dt.Rows[i]["数量"].ToString();
                                int tempn = int.Parse(inum);

                                if (tempn <= 0)
                                {
                                    //跳错
                                    tempn = int.Parse("a");
                                }
                            }
                            catch
                            {
                                mesDis = "第" + (i + 1).ToString() + "条记录，进仓数量:  " + inum + " 输入不正确，只能为正整数。";
                                Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                return;
                            }


                            string ddate = dt.Rows[i]["出库日期"].ToString();


                            if (ddate == "")
                            {
                                mesDis = "第" + (i + 1).ToString() + "条记录，”出库日期“ 为必写项。";
                                Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                return;
                            }
                            //到库
                            if (ddate != "")
                            {
                                try
                                {
                                    DateTime dt1 = DateTime.Parse(ddate);
                                }
                                catch
                                {
                                    mesDis = "第" + (i + 1).ToString() + "条记录，”出库日期“  " + ddate + ", 格式不正确。";
                                    Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                    return;
                                }
                            }
                            #endregion

                            #region 标识c处理
                            tsbs = dt.Rows[i]["特殊标识"].ToString().Trim();

                            
                            switch (tsbs)
                            {
                                case "正常销售":
                                    tsbs = "2";
                                    break;

                                case "货品内销":
                                    tsbs = "5";
                                    break;

                                case "特殊出库":
                                    tsbs = "7";
                                    break;

                                case "大客户出库":
                                    tsbs = "9";
                                    break;

                                default:
                                    tsbs = "未知";
                                    break;
                            }
                            if (tsbs == "未知")
                            {
                                //s = 5;//标识不存在需要在webconfig配置
                                mesDis = "第" + (i + 1).ToString() + "条记录，特殊标识:  " + dt.Rows[i]["特殊标识"].ToString().Trim() + " 输入不正确，只能“正常销售“；“货品内销“；“特殊出库“；“大客户出库“；。";
                                Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                return;
                                //break;

                            }
                            if (tsbs == "7")
                            {
                                if (dt.Rows[i]["特殊描述"].ToString() == "")
                                {
                                    //s = 6;
                                    mesDis = "第" + (i + 1).ToString() + "条记录，特殊标识:  " + dt.Rows[i]["特殊标识"].ToString().Trim() + " 时，“特殊描述“不能为空。";
                                    Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                    return;
                                }
                            }

                            #endregion
                        }

                        #endregion


                        
                        #region  exec 有数据

                        int s = 0;
                        {
                             //usercode = userCode();

                            #region 循环判断仓库权限及数量   2016-05-30改
                           // List<products> list = new List<products>();
                            
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string storeName = dt.Rows[i]["仓库编码"].ToString();
                                numsl = Convert.ToInt32(dt.Rows[i]["数量"].ToString());
                                string rpcode = dt.Rows[i]["睿配编码"].ToString();
                                string tcad = dt.Rows[i]["供应商编码"].ToString();

                                

                                //库存是否足

                                if (rpcode == "")
                                {
                                    DataRow[] drrd = isrpcode.Select("CAD='" + tcad + "'");

                                    rpcode = drrd[0]["rpcode"].ToString();
                                }
                                qty = hb.getproScalar("SELECT stockNum FROM  dbo.stock_storck   where  stockcode='" + storeName + "' AND rpcode='" + rpcode + "'");

                                if (numsl > qty)
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.alert(' " + tcad + " 出库数量大于库存数请核实" + sqlspin.ToString() + "');</script>");
                                    return;
                                }
                            }

                            #endregion


                        #endregion

                        #region 批次号
                            //LT+R/C日期 +1
                            //当天导入 数字累加
                            //LTR20151120-1


                            int numbatch = 1;
                            string day = DateTime.Today.ToString("yyyy-MM-dd");
                            string Scalar = hb.GetScalarstring("  ISNULL(MAX(batchCount),'') ", " dbo.outproduct   where CONVERT(VARCHAR(20),insettime,23)='" + day + "'");

                            if (Scalar != "")
                            {
                                numbatch = Convert.ToInt32(Scalar) + 1;
                            }

                            #endregion

                        #region 执行 出库命令


                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string storeName = dt.Rows[i]["仓库编码"].ToString();
                                numsl = Convert.ToInt32(dt.Rows[i]["数量"].ToString());
                                string rpcode = dt.Rows[i]["睿配编码"].ToString();
                                string saleaddress = dt.Rows[i]["销售地"].ToString();

                                #region 标识c处理
                                tsbs = dt.Rows[i]["特殊标识"].ToString().Trim();
                                if (tsbs != "")
                                {
                                    switch (tsbs)
                                    {
                                        case "正常销售":
                                            tsbs = "2";
                                            break;

                                        case "货品内销":
                                            tsbs = "5";
                                            break;

                                        case "特殊出库":
                                            tsbs = "7";
                                            break;

                                        case "大客户出库":
                                            tsbs = "9";
                                            break;

                                        default:
                                            tsbs = "1";
                                            break;
                                    }
                                }
                      
                                #endregion

                                string note = dt.Rows[i]["睿配编码"].ToString() + storecode;
                                string cad = dt.Rows[i]["供应商编码"].ToString();

                                string batch = "";
                                string batchnum = "";

                                //睿配编码 不为空
                                if (rpcode != "")
                                {
                                    DataRow[] drrp = isrpcode.Select("rpcode='" + rpcode + "'");

                                    batch = dt.Rows[i]["睿配编码"].ToString().Substring(0, 2);
                                    batchnum = batch + "C" + day.Replace("-", "") + "-" + numbatch;

                                    cad = drrp[0]["cad"].ToString();
                                }
                                else
                                {
                                    DataRow[] drrd = isrpcode.Select("CAD='" + cad + "'");

                                    rpcode = drrd[0]["rpcode"].ToString();

                                    batch = rpcode.Substring(0, 2);
                                    batchnum = batch + "C" + day.Replace("-", "") + "-" + numbatch;

                                }
                                #region   执行出库 根据返回数字解析结果

                                string sqlstr = "exec pro_outstore  '" + rpcode + "', '" + cad + "','" + saleaddress + "','" + dt.Rows[i]["出库日期"].ToString() + "', '" + storeName + "'," + numsl + "," + tsbs + ",'" + dt.Rows[i]["特殊描述"].ToString() + "','" + usercode + "','" + batchnum + "','" + numbatch + "' ";



                                if (!hb.ProExecinset(sqlstr))
                                {
                                    s = 1;
                                    break;
                                }
                                #endregion


                            }
                            #endregion

                            if (s == 0)
                            {

                                Response.Write("<script type='text/javascript'>window.parent.alert('出库成功');</script>");
                                return;
                            }
                            else
                            {
                                Response.Write("<script type='text/javascript'>window.parent.alert('出库失败请删除记录');</script>");
                                return;

                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript'>window.parent.alert('文件无内容');</script>");

                        return;
                    }
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>window.parent.alert('文件内容格式不正确，请核实')</script>");
                    return;
                }
                finally
                {
                    sqloutID.Clear();
                    sqlspin.Clear();
                    sqlsql.Clear();

                }
            }
        }


        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Buttondow_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/downloadxlsx/TJ/");
            string datetime = "出库明细" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "");

            string detailfile = ".csv";//明细

            if (HiddenField1.Value == "") return;

            DataTable dt = hb.GetDataSet(HiddenField1.Value).Tables[0];

            dt.Columns.Add("markName");
            IDictionary dictmark = (IDictionary)ConfigurationManager.GetSection("spmark");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 1; j <= dictmark.Count; j++)
                {
                    string type = "type" + j;
                    if (dt.Rows[i]["spmark"].ToString() == j.ToString())
                    {
                        dt.Rows[i]["markName"] = dictmark[type].ToString();
                    }
                }

                if (dt.Rows[i]["saleaddress"].ToString().Length > 8)
                {
                    dt.Rows[i]["saleaddress"] = dt.Rows[i]["saleaddress"].ToString().Substring(0, 7) + "..";
                }
            }


            dt.Columns["rpcode"].SetOrdinal(0);
            dt.Columns["CAD"].SetOrdinal(1);
            dt.Columns["OD"].SetOrdinal(2);
            dt.Columns["QTY"].SetOrdinal(3);
            dt.Columns["WHcode"].SetOrdinal(4);
            dt.Columns["basename"].SetOrdinal(5);
            dt.Columns["markName"].SetOrdinal(6);
            dt.Columns["usercode"].SetOrdinal(7);
            dt.Columns["insettime"].SetOrdinal(8);
            dt.Columns["saleaddress"].SetOrdinal(8);


            dt.Columns["rpcode"].ColumnName = "睿配编码";
            dt.Columns["CAD"].ColumnName = "供应商编码";
            dt.Columns["OD"].ColumnName = "出货日期";
            dt.Columns["QTY"].ColumnName = "数量";
            dt.Columns["WHcode"].ColumnName = "仓库编码";
            dt.Columns["basename"].ColumnName = "仓库名";
            dt.Columns["markName"].ColumnName = "特殊标示";
            dt.Columns["usercode"].ColumnName = "操作人";
            dt.Columns["insettime"].ColumnName = "操作时间";
            dt.Columns["saleaddress"].ColumnName = "销售地";


            if (ProductBLL.download.ExcelHelper.TableToCsv(dt, path + datetime + detailfile))
            {
                downloadfile(path + datetime + detailfile);
                Response.Write("<script type='text/javascript'>window.parent.alert('导出EXecl成功!')</script>");

                return;
            }
            else
            {
                Response.Write("<script type='text/javascript'>window.parent.alert('导出Execl失败!');window.location.href='../products/PriceRebateExecl.aspx';</script>");

                return;
            }

        }

        void downloadfile(string s_path)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(s_path);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();

            Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.WriteFile(file.FullName);
            Response.Flush();
            HttpContext.Current.Response.Clear();
            //下载完成后删除服务器下生成的文件
            if (File.Exists(s_path))
            {
                File.Delete(s_path);

            }
            HttpContext.Current.Response.End();
        }


    }
    ///// <summary>
    ///// 出库信息
    ///// </summary>
    //public class products
    //{
    //    /// <summary>
    //    /// 仓库名字
    //    /// </summary>
    //    public string StoreName { get; set; }
    //    /// <summary>
    //    /// 睿配编码
    //    /// </summary>
    //    public string RpCode { get; set; }
    //    /// <summary>
    //    /// 供应商编码
    //    /// </summary>
    //    public string CAD { get; set; }
    //    /// <summary>
    //    /// 数量
    //    /// </summary>
    //    public int Number { get; set; }
    //}
}