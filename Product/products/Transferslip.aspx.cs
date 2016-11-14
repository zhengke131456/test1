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

	public partial class Transferslip : Common.BasePage
    {
        protected StringBuilder result;
        protected string outwh = "", rpcode = "" ,inwh="";
        protected int curpage = 1, pagesize = 10, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        //StringBuilder sqlpin = new StringBuilder();
        StringBuilder sqlCad = new StringBuilder();//查看编码是否重复

        //当前用户角色
        protected string spcode = "";

        //选中状态
        protected string states = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
               
                try
                {
                    getvalue();
                    InitParam("");
                    bindData();
                }
                catch { }
            //}
        }

        private void bindData()
        {
             DataTable rpcodedb = hb.getdate("rpcode,rpcode+'<>'+CAD AS rpname","dbo.products");

            result = new StringBuilder();
            result.Append("[");
            for (int i = 0; i < rpcodedb.Rows.Count; i++)
            {
                result.Append("{ value: \"" + rpcodedb.Rows[i]["rpcode"].ToString() + "\",label:\" " + rpcodedb.Rows[i]["rpname"].ToString() + "\"},");
            }
            result.Remove(result.Length - 1, 1);//把最后一个，给移除


            result.Append(" ]");

			string ispat = isPartcode();
            DataSet ds = getData();

            if (!ds.Equals(null))
            {
                allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                lblCount.Text = allCount.ToString();

                DataTable dt = ds.Tables[1];
                dt.Columns.Add("iswh");//0,1,2
                dt.Columns.Add("ipc");
                dt.Columns.Add("opc");

				DataTable dbstore = hb.getdate("SR_storecode", "dbo.SYS_RightStore WHERE SP_code='" + ispat + "'");
				for (int i = 0; i < dt.Rows.Count; i++) {


					if (dt.Rows[i]["states"].ToString() == "待出库") {
						DataRow[] dr = dbstore.Select("SR_storecode='" + dt.Rows[i]["outwh"].ToString() + "'");
					
					if (dr.Length >0) {

						dt.Rows[i]["iswh"]="0";//
					}
					}

					if (dt.Rows[i]["states"].ToString() == "待入库") {
						DataRow[] drr = dbstore.Select("SR_storecode='" + dt.Rows[i]["inwh"].ToString() + "'");

						if (drr.Length > 0) {

							dt.Rows[i]["iswh"] = "0";//
						}

					}

                    if (dt.Rows[i]["inl"].ToString() == "3")
                    {
                        dt.Rows[i]["ipc"] = dt.Rows[i]["ip"].ToString() + "," + dt.Rows[i]["icity"].ToString() + ",";
                    }
                    if (dt.Rows[i]["onl"].ToString() == "3")
                    {
                        dt.Rows[i]["opc"] = dt.Rows[i]["op"].ToString() + "," + dt.Rows[i]["ocity"].ToString() + ",";
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
                    literalPagination.Text = GenPaginationBar("Transferslip.aspx?page=[page]&outwh=" + outwh + "&rpcode=" + rpcode + "&checked=" + states + "&inwh=" + inwh + "", pagesize, curpage, allCount);
                } 
            }
        }


        private void InitParam(string Index)
        {

			string partcode = isPartcode();
            #region 
            sinfo.PageSize = 10;
            //sinfo.Tablename = "products"; 
           // sinfo.Orderby = "id";
            sinfo.Sqlstr = "(SELECT dbo.transferslip.[InErrorMess] ,dbo.transferslip.[InErrorTime] ,dbo.transferslip.[OutErrorMess],dbo.transferslip.[OutErrorTime],  dbo.products.ShGoogcode,dbo.products.CAD,dbo.transferslip.id,dbo.transferslip.rpcode,QTY,states,rk.basename AS inWHName,rk.nodelevel as inl,rk.province as ip,rk.city as icity ,ck.basename AS outwhName,ck.province as op,ck.city as ocity ,ck.nodelevel as onl,outwhtime,inwhtime,dbo.transferslip.opcode, outOpcode,inOpcode,dbo.transferslip.inserttime ,dbo.transferslip.inWH,dbo.transferslip.outwh,Dimension,PATTERN  FROM  transferslip  LEFT JOIN dbo.BaseStore rk ON  rk.Basecode=inWH   LEFT JOIN dbo.BaseStore ck ON  ck.Basecode=outwh  LEFT JOIN dbo.products ON dbo.products.rpcode=dbo.transferslip.rpcode )hh where ";

			sinfo.Sqlstr += "( EXISTS ( SELECT SR_storecode FROM  dbo.SYS_RightStore rk  WHERE    inWH = rk.SR_storecode AND SP_code = '" + partcode + "' ) OR EXISTS ( SELECT    SR_storecode FROM      dbo.SYS_RightStore ck   WHERE     outwh = ck.SR_storecode AND SP_code = '" + partcode + "' ) )";

            if (rpcode != "")
            {
                sinfo.Sqlstr += "  and  rpcode  LIKE'%" + rpcode + "%' ";
            }
            else
            {
                sinfo.Sqlstr += " and 1=1 ";
            }
            if (outwh != "")
            {
				sinfo.Sqlstr += "  and outwhName ='" + outwh + "' ";
            }
            if (inwh != "")
            {
				sinfo.Sqlstr += "  and inWHName  = '" + inwh + "' ";
            }

            if (states != "")
            {
                sinfo.Sqlstr += "  and states  = '" + states + "' ";
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

        //查询
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

        //初始值
        protected void getvalue()
        {
            if (!string.IsNullOrEmpty(GetQueryString("inwh")))
            {
                inwh = GetQueryString("inwh").Trim();
            }
            if (!string.IsNullOrEmpty(GetQueryString("rpcode")))
            {
                rpcode = Request.Form["rpcode"].Trim();
            }
            if (!string.IsNullOrEmpty(GetQueryString("outwh")))
            {
                outwh = GetQueryString("outwh").Trim();
            }

            if (!string.IsNullOrEmpty(GetQueryString("checked")))
            {
                states = GetQueryString("checked").Trim();
            }
        }


        //批量调拨单
        protected void Button1_Click(object sender, EventArgs e)
        {
            bool fileIsValid = false;
            //如果确认了文件上传，则判断文件类型是否符合要求

            #region 确定是CSV 文件
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
                    Response.Write("<script type='text/javascript'>window.parent.alert('文件格式不正确!请上传正确格式的CSV文件')</script>");
                    return;
                }

            }
            #endregion

            //如果文件类型符合要求，则用SaveAs方法实现上传，并显示信息
            if (fileIsValid == true)
            {
                try
                {
                    string name = Server.MapPath("~/uploadxls/") + "批量调拨单_" + Username + "_" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";
                    this.FileUpload1.SaveAs(name);
                    if (File.Exists(name))
                    {
                        DataTable dt = ProductBLL.Search.Searcher.OpenCSV(name);
                        int qty = 0; int numsl = 0;

                        string usercode = userCode();
                        string cstorecode = "";//出库编码
                        string rstorecode = "";//入库编码
                        string tsbs = "";
                        string cad ="";
                        //string s = "";

                        #region 判断 数据是否合法

                        DataTable isrpcode = hb.getdate("rpcode,cad ", "dbo.products");  //

                        string mesDis = "";//错误信息
                        string inum = "";//操作数量
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            cstorecode = dt.Rows[i]["出库编码"].ToString();
                            rstorecode = dt.Rows[i]["入库编码"].ToString();
                            rpcode = dt.Rows[i]["睿配编码"].ToString();
                            cad = dt.Rows[i]["供应商编码"].ToString();


                            #region 睿配编码,CAD (存在一者便可，如同时存在按睿配编码为准）

                            //两者都存在已睿配编码为准。
                            if (rpcode == "" && cad == "")
                            {
                                mesDis = "第" + (i + 1).ToString() + "条记录，睿配编码与供应商编码不能同时为空。";
                                Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                return;
                            }

                            if (rpcode != "")
                            {
                                DataRow[] drrp = isrpcode.Select("rpcode='" + rpcode + "'");

                                if (drrp.Length <= 0)
                                {
                                    mesDis = "第" + (i + 1).ToString() + "条记录，睿配编码: " + rpcode + " 不存在。";
                                    Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                    return;
                                }
                            }
                            else
                            {
                                DataRow[] drrd = isrpcode.Select("CAD='" + cad + "'");

                                if (drrd.Length <= 0)
                                {
                                    mesDis = "第" + (i + 1).ToString() + "条记录，供应商编码: " + rpcode + " 不存在。";
                                    Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                    return;
                                }
                            }

                            #endregion

                            #region 根据角色判断是否有出仓库权限.入库权限不限 4

                            spcode = isPartcode();

                            DataTable dtstore = hb.getdate("SR_storecode", "dbo.SYS_RightStore WHERE  SP_code='" + spcode + "'");  //可操作的仓库编码

                            //出库
                            DataRow[] dr = dtstore.Select("SR_storecode='" + cstorecode + "'");
                            if (dr.Length <= 0)
                            {
                                mesDis = "第" + (i + 1).ToString() + "条记录，当前用户没有出库仓库: " + cstorecode + " 的操作权限或仓库名输入不正确。";
                                Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                return;
                            }

                            #endregion

                            #region 库存数只能为正数,调拨日期必写，时间格式校正。
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
                                mesDis = "第" + (i + 1).ToString() + "条记录，调拨数量:  " + inum + " 输入不正确，只能为正整数。";
                                Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                return;
                            }
                            string ddate = dt.Rows[i]["调拨日期"].ToString();
                            if (ddate == "")
                            {
                                mesDis = "第" + (i + 1).ToString() + "条记录，”调拨日期“ 为必写项。";
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
                                    mesDis = "第" + (i + 1).ToString() + "条记录，”调拨日期“  " + ddate + ", 格式不正确。";
                                    Response.Write("<script type='text/javascript'>window.parent.alert('" + mesDis + "');</script>");
                                    return;
                                }
                            }
                            #endregion

                            #region 调拨出库库存是否充足

                            if (rpcode == "")
                            {
                                DataRow[] drrd = isrpcode.Select("CAD='" + cad + "'");
                                rpcode = drrd[0]["rpcode"].ToString();
                            }
                            if (cad == "")
                            {
                                DataRow[] drrd = isrpcode.Select("rpcode='" + rpcode + "'");
                                cad = drrd[0]["cad"].ToString();
                            }

                            qty = hb.getproScalar("SELECT stockNum FROM  dbo.stock_storck   where  stockcode='" + cstorecode + "' AND rpcode='" + rpcode + "'");

                            //数量
                            int tempnn = Convert.ToInt32(dt.Rows[i]["数量"].ToString());
                            if (tempnn > qty)
                            {
                                Response.Write("<script type='text/javascript'>window.parent.alert('出库仓库： " + cstorecode + " ，编码:" + rpcode + " 数量： " + tempnn + " 大于目前仓库库存数请核实！');</script>");
                                return;
                            }
                            #endregion
                        }
                        #endregion

                        #region  exec 有数据
                        StringBuilder sb = new StringBuilder();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string outwh = dt.Rows[i]["出库编码"].ToString();
                            string inwh = dt.Rows[i]["入库编码"].ToString();
                            rpcode = dt.Rows[i]["睿配编码"].ToString();
                            cad = dt.Rows[i]["供应商编码"].ToString();

                            //睿配编码 不为空
                            if (rpcode != "")
                            {
                                DataRow[] drrp = isrpcode.Select("rpcode='" + rpcode + "'");
                                cad = drrp[0]["cad"].ToString();
                            }
                            else
                            {
                                DataRow[] drrd = isrpcode.Select("CAD='" + cad + "'");
                                rpcode = drrd[0]["rpcode"].ToString();
                            }

                            //初始状态  待审核、待出库
                            //待出库：夸市级的。
                            bool bl = true;
                            string states = "待出库";

                            bl = CheckStroeIsLevel(outwh, inwh);
                            if (bl == false) states = "待审核";

                            string sql = "INSERT INTO dbo.transferslip  ( rpcode , cad , outwh , inwh,qty , dbtime ,opcode,states ) VALUES  ( '" + rpcode + "','" + cad + "','" + outwh + "','" + inwh + "','" + dt.Rows[i]["数量"].ToString() + "','" + dt.Rows[i]["调拨日期"].ToString() + "','" + Username + "','" + states + "' ) ;";
                            sb.Append(sql);

                        }
                        if (hb.ProExecinset(sb.ToString()))
                        {
                            Response.Write("<script type='text/javascript'>window.parent.alert('批量生成调拨单成功！');</script>");
                            return;
                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>window.parent.alert('批量生成调拨单失败，请稍后再试或联系管理员！');</script>");
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
                catch
                {
                    Response.Write("<script type='text/javascript'>window.parent.alert('文件内容格式不正确，请核实')</script>");
                    return;
                }
                finally
                {

                }
            }
        }
       
    }
}