using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using productcommon;

namespace product.baseinfo
{
    public partial class baseinfolist : Common.BasePage
    {
        protected string Code = "", style = "style=\"display:none\"", style1 = "style=\"display:none\"";
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        // StringBuilder sqlpin = new StringBuilder();
        StringBuilder sqlRepeat = new StringBuilder();//查看导入的某列是否重复
        public string type;
        public string biaoti;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(GetQueryString("BaseName")))
                {
                    Code = GetQueryString("BaseName").Trim();
                }
                type = GetQueryString("type");
                Hiddentype.Value = type;
                if (type == "4")
                {
                    //如果是仓库维护 显示区域和编码 
                    style = "";
                    style1 = "";
                }
                if (type == "5")
                { style = ""; }
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
                    literalPagination.Text = GenPaginationBar("baseinfolist.aspx?page=[page]&type=" + Hiddentype.Value + "&BaseName=" + Code, pagesize, curpage, allCount);
                }
                biaoti = hb.bi(type);
            }
        }


        private void InitParam(string Index)
        {
            if (!string.IsNullOrEmpty(GetQueryString("typ")))
            {
                type = GetQueryString("typ");

            }
            if (type == "4" || type == "5")
            {
                sinfo.Basecode = Code;
            }
            else
            {
                sinfo.Basename = Code;
            }

            sinfo.PageSize = 20;
            sinfo.Tablename = "baseinfo";
            sinfo.Protype = type;
            sinfo.Orderby = "id";

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
                sinfo.Protype = Hiddentype.Value;
                sinfo.PageIndex = 1;
            }
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

            /*****************************修订***************************************************
              ///功能：type=1时判断导入编码是否存在重复
              ///目的：
              ///完成时间：2015-04-13
              ///作者：cx
              ///遗留问题：无
              ///说明：无 
              ///版本：15.04.14
              ///修订：修订方式
              ************************************************** */
            #region 修订版本：15.04.14


            //如果文件类型符合要求，则用SaveAs方法实现上传，并显示信息
            if (fileIsValid == true)
            {
                try
                {
                    string name = Server.MapPath("~/uploadxls/") + "IN" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";
                    this.FileUpload1.SaveAs(name);
                    if (File.Exists(name))
                    {
                        DataTable dt = ProductBLL.Search.Searcher.OpenCSV(name);
                        dt.Columns.Add("infotype", typeof(int));

                        if (dt.Rows.Count > 0)
                        {

                            // 检测列是否有重复 如果重复就终止操作并提示重复数据
                            var IsRepeat = from row in dt.Rows.Cast<DataRow>() group row by Convert.ToString(row[0]) into resultCollection select resultCollection;

                            foreach (var group in IsRepeat)
                            {
                                if (Convert.ToInt32(group.Count().ToString()) > 1)
                                {
                                    sqlRepeat.Append(group.Key.ToString() + ",");//记录重复数据
                                }
                            }

                            #region 导入表不存在重复


                            if (sqlRepeat.Length == 0)
                            {

                                #region 先对比两表数据是否重复，如果不重复执行SqlBulkCopy 直接拷贝表


                                string table = "baseinfo where type='" + Hiddentype.Value + "'";
                                DataTable dbbaseinfo = hb.GetTable(table);//数据库表
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    dt.Rows[j]["infotype"] = Convert.ToInt32(Hiddentype.Value);
                                    DataRow[] sRow = dbbaseinfo.Select("basename='" + dt.Rows[j][0].ToString() + "'");

                                    if (sRow.Length != 0)
                                    {
                                        sqlRepeat.Append(dt.Rows[j][0].ToString() + ",");
                                    }
                                }
                                if (sqlRepeat.Length == 0)
                                {
                                    if (DataMgr.BulkToDBbaseinfo(dt))
                                    {
                                        Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../baseinfo/baseinfoList.aspx?type=" + Hiddentype.Value + "';</script>");

                                        return;
                                    }
                                    else
                                    {
                                        Response.Write("<script type='text/javascript'>window.parent.alert('文件无内容');</script>");
                                        //sqlpin.Clear();
                                        return;
                                    }

                                }
                                else
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.alert('(" + sqlRepeat.ToString() + ") 数据库已有记录，请核实再上传');</script>");
                                }

                                #endregion

                                #region 历史版本15.04.13// 循环一次 判断一次然后记录插入语句
                                //for (int i = 0; i < dt.Rows.Count; i++)
                                //{






                                // string table = "baseinfo where type='" + type + "' and  basename='" + dt.Rows[i][0].ToString() + "'";
                                //if (hb.GetScalar(table) == 0)//查询该字段
                                //{
                                //    sqlpin.Append("insert  into baseinfo values ('" + dt.Rows[i][0].ToString() + "','" + type + "',default);");
                                //}
                                //else
                                //{
                                //    sqlpin.Clear();
                                //    Response.Write("<script type='text/javascript'>window.parent.alert('(" + dt.Rows[i][0].ToString() + ") 数据库已有记录，请核实再上传');</script>");
                                //    return;
                                //}

                                //}
                                #endregion

                            }
                            else
                            {
                                Response.Write("<script type='text/javascript'>window.parent.alert('(" + sqlRepeat.ToString() + ")上传文件重复，请核实再上传');</script>");

                                return;
                            }
                            #endregion

                            /*
                            if (sqlpin.Length > 0)
                            {

                                if (hb.insetpro(sqlpin.ToString()))
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../baseinfo/baseinfoList.aspx?type=" + type + "';</script>");
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
                            */


                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>window.parent.alert('文件无内容');</script>");
                            //sqlpin.Clear();
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
                    sqlRepeat.Clear();
                }




            }

            #endregion


            #region 历史版本版本：15.04.14

            /*
            //如果文件类型符合要求，则用SaveAs方法实现上传，并显示信息
            if (fileIsValid == true)
            {
                try
                {
                    string name = Server.MapPath("~/uploadxls/") + "IN" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";
                    this.FileUpload1.SaveAs(name);
                    if (File.Exists(name))
                    {
                        DataTable dt = ProductBLL.Search.Searcher.OpenCSV(name);
                        if (dt.Rows.Count > 0)
                        {

                           

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string table = "baseinfo where type='" + type + "' and  basename='" + dt.Rows[i][0].ToString() + "'";
                                if (sqlpin.ToString().Contains(dt.Rows[i][0].ToString()))
                                {
                                    sqlpin.Clear();
                                   Response.Write("<script type='text/javascript'>window.parent.alert('(" + dt.Rows[i][0].ToString() + ")上传文件重复，请核实再上传');</script>");
                                    return;
                                }
                                else
                                {
                                    if (hb.GetScalar(table) == 0)
                                    {
                                        sqlpin.Append("insert  into baseinfo values ('" + dt.Rows[i][0].ToString() + "','" + type + "',default);");
                                    }
                                    else
                                    {
                                        sqlpin.Clear();
                                        Response.Write("<script type='text/javascript'>window.parent.alert('(" + dt.Rows[i][0].ToString() + ") 数据库已有记录，请核实再上传');</script>");
                                        return;
                                    }
                                }

                            }
                            if (sqlpin.Length > 0)
                            {

                                if (hb.insetpro(sqlpin.ToString()))
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../baseinfo/baseinfoList.aspx?type=" + type + "';</script>");
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
                        }
                        else
                        {
                            Response.Write("<script type='text/javascript'>window.parent.alert('文件无内容');</script>");
                            sqlpin.Clear();
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
                }

            }
            */
            #endregion

        }

        protected void btnQuerey_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["BM"]))
            {
                Code = Request["BM"].Trim();
            }
            try
            {
                InitParam("1");
                bindData();
            }
            catch { }
        }

    }
}