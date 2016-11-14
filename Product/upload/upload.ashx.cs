using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Text;

namespace product.upload
{
    /// <summary>
    /// upload 的摘要说明
    /// </summary>
    public class upload : IHttpHandler
    {
        StringBuilder sqlpin = new StringBuilder();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        public void ProcessRequest(HttpContext context)
        {
           // string saveurl = @"D:\\小程序\product\product\uploadxls\";
            context.Response.ContentType = "text/html";
            context.Response.Buffer = false;
            string type = context.Request.QueryString["type"].ToString();
            try
            {
                HttpPostedFile file = context.Request.Files["upfile"];
                if (file.FileName != "")
                {
                    if (file.FileName.IndexOf(".csv") > 0)
                    {
                        //file.FileName.IndexOf(".xls") > 0 || 
                        //if (!file.FileName.Contains("订单"))
                        //{
                        //    context.Response.Write("<script type='text/javascript'> window.parent.alert('你上传的不是订单表');window.parent.document.getElementById('Ld').style.display='none' </script>");
                        //    context.Response.End();
                        //}
                        HttpServerUtility Server = context.Server;
                        string fname = type+"-" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";
                        string tmpPath = Server.MapPath("~/uploadxls/") + fname;
                        file.SaveAs(tmpPath); 

                        if (File.Exists(tmpPath))
                        {
                            DataTable dt = ProductBLL.Search.Searcher.OpenCSV(tmpPath);
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    string table = "baseinfo where type='" + type + "' and  basename='" + dt.Rows[i][0].ToString() + "'";
                                    if (sqlpin.ToString().Contains(dt.Rows[i][0].ToString()))
                                    {
                                        sqlpin.Clear();
                                        context.Response.Write("<script type='text/javascript'>window.parent.alert('(" + dt.Rows[i][0].ToString() + ")上传文件重复，请核实再上传');</script>");
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
                                            context.Response.Write("<script type='text/javascript'>window.parent.alert('(" + dt.Rows[i][0].ToString() + ") 数据库已有记录，请核实再上传');</script>");
                                            return;
                                        }
                                    }
                                }
                                if (sqlpin.Length > 0)
                                {
                                   
                                        if (hb.insetpro(sqlpin.ToString()))
                                        {
                                            context.Response.Write("<script type='text/javascript'>window.parent.alert('上传成功');window.location.href='../baseinfo/baseinfoList.aspx?type=" + type + "';</script>");
                                            sqlpin.Clear();
                                        }
                                        else
                                        {
                                            context.Response.Write("<script type='text/javascript'>window.parent.alert('上传失败');</script>");
                                            sqlpin.Clear();
                                        }
                                   
                                }
                            }
                        } 
                    }
                    else {
                        context.Response.Write("<script type='text/javascript'>window.parent.alert('文件格式不正确!请上传正确格式的文件')</script>"); 
                    }
                }
                else 
                {
                    context.Response.Write("<script type='text/javascript'>window.parent.alert('您还没选择文件！请先选择上传文件！！')</script>");
                }
            }
            catch 
            {
                context.Response.Write("<script type='text/javascript'>window.parent.alert('文件工作区名字不正确')</script>");
            }
           
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}