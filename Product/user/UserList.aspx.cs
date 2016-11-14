using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

namespace product.user
{
    public partial class UserList : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        StringBuilder sqlpin = new StringBuilder();
        public string type;
 
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                InitParam();
                bindData();
            }
            catch { }
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
                    literalPagination.Text = GenPaginationBar("UserList.aspx?page=[page]", pagesize, curpage, allCount);
                }
                
            }
        }


        private void InitParam()
        {
            #region
            
            sinfo.PageSize = 20;
            sinfo.Tablename = "userinfo";
            sinfo.Orderby = "id";
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
        }

    }
}