using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;

namespace product.products
{
    public partial class WX_productList : Common.BasePage
    {
        protected string rpcode = "", code = "", model = "", styledisplay = "style=\"display: none;\"", iscity = "style=\"display: none\"";
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        StringBuilder sqlpin = new StringBuilder();
        StringBuilder sqlCad = new StringBuilder();//查看编码是否重复
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GetQueryString("CAD") != "")
                {
                    code = GetQueryString("CAD");
                }
                if (GetQueryString("rpcode") != "")
                {
                    rpcode = GetQueryString("rpcode");
                }
                
                try
                {
                    InitParam("");
                    bindData();
                }
                catch { }
            }
        }

        private void bindData()
        {
            DataSet ds = getData();

            if (!ds.Equals(null))
            {
                allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                lblCount.Text = allCount.ToString();

                DataTable dt = ds.Tables[1];

                dislist.DataSource = ds.Tables[1];
                dislist.DataBind();
                if (allCount <= pagesize)
                {
                    literalPagination.Visible = false;
                }
                else
                {
                    literalPagination.Visible = true;
                    literalPagination.Text = GenPaginationBar("WX_productList.aspx?page=[page]&rpcode=" + rpcode + "&CAD=" + code + "&model=" + model + "&ischeck=1", pagesize, curpage, allCount);
                }
            }
        }


        private void InitParam(string Index)
        {
            #region
            sinfo.PageSize = 20;

            sinfo.Orderby = "id";
           
            sinfo.Sqlstr = "( SELECT * FROM  dbo.WX_productinfo  ";


            if (code != "")
            {
                sinfo.Sqlstr += " where  CAD  LIKE'%" + code + "%' ";
            }
            else
            {
                sinfo.Sqlstr += " where  1=1 ";
            }
            if (rpcode != "")
            {
                sinfo.Sqlstr += "  and rpcode  LIKE'%" + rpcode + "%' ";
            }
            if (model != "")
            {
                sinfo.Sqlstr += "  and Dimension  LIKE'%" + model + "%')hh ";
            }
            else
            {
                sinfo.Sqlstr += ")hh";
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
            Querey();
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

                if (fileExtension == ".xls")
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
                    string name = Server.MapPath("~/uploadxls/") + "APPproductinfo" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".xls";
                    this.FileUpload1.SaveAs(name);

                    if (File.Exists(name))
                    {
                        //DataTable dtcsv = ProductBLL.Search.Searcher.OpenCSV(name);
                        DataTable dtcsv = ExcelToDS(name).Tables[0];

                        if (dtcsv.Rows.Count > 0)
                        {
                            #region   修订版本号：00.15.04版本循环插入表products
                            //产品
                            DataTable dt_rp = hb.GetDataSet(" select id, rpcode,cad from products ").Tables[0];
                            //省份
                            DataTable dt_p = hb.GetDataSet(" select id, name from cityprovince ").Tables[0];
                            //城市 
                            DataTable dt_c = hb.GetDataSet(" select id, provinceid,pname,replace([cityname],'市','') as cityname from city ").Tables[0];
                            //APP 产品信息
                            DataTable dt_ap = hb.GetDataSet(" select id,rpcode, provincename,cityname from WX_productinfo ").Tables[0];

                            for (int i = 0; i < dtcsv.Rows.Count; i++)
                            {
                                //新加，还是更新信息 (省份，城市，睿配编码）
                                string t_Price = dtcsv.Rows[i]["Price"].ToString();

                                if (t_Price == "") continue;

                                string t_RpCode = dtcsv.Rows[i]["RpCode"].ToString().Trim();
                                string t_provincename = dtcsv.Rows[i]["provincename"].ToString().Trim();
                                string t_cityname = dtcsv.Rows[i]["cityname"].ToString().Trim();
                                string t_SNPXTCode = dtcsv.Rows[i]["SNPXTCode"].ToString();
                                string t_SNPCode = dtcsv.Rows[i]["SNPCode"].ToString().Replace("B","");
                                
                                string t_CAD = dt_rp.Select(" rpcode = '" + t_RpCode + "' ")[0]["cad"].ToString();


                                //省份价格
                                if (t_provincename != "" && t_cityname == "")
                                {
                                    DataRow[] dt_dr = dt_ap.Select(" RpCode = '" + t_RpCode + "' and provincename = '" + t_provincename + "' and cityname = ''  ");
                                    
                                    //省份ID
                                    string tt_pid = dt_p.Select(" name = '" + t_provincename + "' ")[0]["id"].ToString();

                                    //新加
                                    if (dt_dr.Length == 0)
                                    {
                                        sqlpin.Append(" INSERT INTO dbo.WX_productinfo ( rpcode,provinceid ,provincename ,cityid , cityname , SNPCode ,   SNPXTCode , price,cad  ) values ");
                                        sqlpin.Append("('" + t_RpCode + "','" + tt_pid + "','" + t_provincename + "','','','" + t_SNPCode + "','" + t_SNPXTCode + "','" + t_Price + "' ,'" + t_CAD + "')");
                                    }
                                    else //更新
                                    {
                                        sqlpin.Append("update WX_productinfo set SNPCode='" + t_SNPCode + "', SNPXTCode= '" + t_SNPXTCode + "',price = '" + t_Price + "' where provincename = '" + t_provincename + "'  and rpcode='"+t_RpCode+"'  ");
                                    }

                                }
                                //城市 
                                if (t_provincename != "" && t_cityname != "")
                                {
                                    DataRow[] dt_dr = dt_ap.Select(" RpCode = '" + t_RpCode + "' and provincename = '" + t_provincename + "' and cityname = '"+t_cityname+"' ");

                                    //省份ID
                                    string tt_pid = dt_p.Select(" name = '" + t_provincename + "' ")[0]["id"].ToString();
                                    //城市ID
                                    string tt_cityid = dt_c.Select(" cityname = '" + t_cityname + "' ")[0]["id"].ToString();

                                    //新加
                                    if (dt_dr.Length == 0)
                                    {
                                        sqlpin.Append(" INSERT INTO dbo.WX_productinfo ( rpcode,provinceid ,provincename ,cityid , cityname , SNPCode ,   SNPXTCode , price,cad  ) values ");
                                        sqlpin.Append("('" + t_RpCode + "','" + tt_pid + "','" + t_provincename + "','" + tt_cityid + "','" + t_cityname + "','" + t_SNPCode + "','" + t_SNPXTCode + "','" + t_Price + "' ,'" + t_CAD + "')");
                                    }
                                    else //更新
                                    {
                                        sqlpin.Append("update WX_productinfo set SNPCode='" + t_SNPCode + "', SNPXTCode= '" + t_SNPXTCode + "',price = '" + t_Price + "' where provincename = '" + t_provincename + "' and cityname='"+t_cityname+"'  and rpcode='" + t_RpCode + "'  ");
                                    }
                                }
                            }
                            #region 有数据执行插入


                            if (sqlpin.Length > 0)
                            {

                                if (hb.insetpro(sqlpin.ToString()))
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../products/wx_productlist.aspx';</script>");
                                    // dbproduct.Clear();
                                    sqlpin.Clear();
                                    return;
                                }
                                else
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.alert('上传失败');</script>");
                                    sqlpin.Clear();

                                    return;
                                }

                            }

                            #endregion


                            #endregion
                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>window.parent.alert('文件无内容');</script>");
                            return;
                        }
                    }

                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    Response.Write("<script type='text/javascript'>window.parent.alert('错误："+ex.Message.ToString()+"')</script>");
                    return;
                }
                finally
                {

                }
            }
        }
        public void Querey()
        {
            if (!string.IsNullOrEmpty(Request.Form["model"]))
            {
                model = Request.Form["model"].Trim();
            }
            if (!string.IsNullOrEmpty(Request.Form["CAD"]))
            {
                code = Request.Form["CAD"].Trim();
            }
            if (!string.IsNullOrEmpty(Request.Form["rpcode"]))
            {
                rpcode = Request.Form["rpcode"].Trim();
            }

        }

        protected void Buttondow_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/downloadxlsx/Order/");
            string datetime = "产品列表" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "");

            string detailfile = ".csv";//明细

            if (ProductBLL.download.ExcelHelper.TableToCsv(getTable(), path + datetime + detailfile))
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
        protected DataTable getTable()
        {

            Querey();//获取查询参数
            string issx = "0";
            

          

            //str.Append("exec(@sql)");
            string strsql = "exec dowproducts '" + model + "','" + code + "','" + rpcode + "','" + issx + "','" + isPartcode() + "'";
            // strsql += "	, 	pingpai AS '品类' ,Dimension AS '规格' ,  ";
            //strsql += "	PATTERN AS '花纹',LOADINGs AS '载重指数',SPEEDJb AS '速度级别'  ";

            DataTable dtdb = hb.getProdatable(strsql);
            dtdb.Columns.Remove("rpcodeid");
            return dtdb;
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
}