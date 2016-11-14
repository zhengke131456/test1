using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using ProductBLL.download;
namespace product.baseinfo
{
    public partial class basezshstore : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();

        protected string pid = "", cid = "", province = "", city = "", basename = "", styledisplay = "style=\"display: none\"";


        protected string show1 = "";//批量与新增油站权限
        protected string uc = "0005";//亚静角色

        StringBuilder sqlbui = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitParam("");
                getvalue();

                hiddpart.Value = " select * from " + sinfo.Sqlstr;
                bind();
            }

            string userPart = isPartcode();
            //批量与新增油站权限
            if (userPart == uc || userPart == "0001")
            {
                show1 = "1";
            }
        }

        private void InitParam(string Index)
        {
            #region
            string partRights = isPartcode();
            sinfo.PageSize = 20;
            sinfo.Orderby = "id";
            //sinfo.Sqlstr = "(SELECT  dbo.BaseStore.id, dbo.BaseStore.province,dbo.BaseStore.city,dbo.BaseStore.px,dbo.BaseStore.py, Basecode,basename,area,[type],number,notes,cityname,b_status,selltype,OldBasecode ,ChinazcomName,phone,address FROM  dbo.BaseStore LEFT JOIN  dbo.City ON citycode=AreaCode   where [type] = 4 and  Basecode  IN ( SELECT SR_storecode  FROM  dbo.SYS_RightStore where SP_code='" + partRights + "') )hh";
            sinfo.Sqlstr = "(SELECT  dbo.BaseStore.id, dbo.BaseStore.province,dbo.BaseStore.city,dbo.BaseStore.px,dbo.BaseStore.py, Basecode,basename,area,[type],number,notes,cityname,b_status,selltype,OldBasecode ,ChinazcomName,phone,address FROM  dbo.BaseStore LEFT JOIN  dbo.City ON citycode=AreaCode   where [type] = 4 and  Basecode  IN ( SELECT SR_storecode  FROM  dbo.SYS_RightStore where 1=1 ) )hh";

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
        private void getvalue()
        {

            DataTable dtp = hb.GetDataSet("select id ,name from [CityProvince] ").Tables[0];
            DataTable dtc = hb.GetDataSet("select id ,cityname from [City] ").Tables[0];

            sinfo.Sqlstr += " where 1=1 ";

            //仓库
            sinfo.PageSize = 20;
            if (!string.IsNullOrEmpty(GetQueryString("page")))
            {
                curpage = int.Parse(GetQueryString("page"));
                sinfo.PageIndex = curpage;
            }

            if (!string.IsNullOrEmpty(GetQueryString("select1")))
            {
                  pid = GetQueryString("select1");

                 DataRow[] dr = dtp.Select(" id = '" + pid + "'");
                 if (dr.Length > 0)
                 {
                     province = dr[0]["name"].ToString();
                     sinfo.Sqlstr += " and province='" + province + "' ";
                 }
                 
            }

            if (!string.IsNullOrEmpty(GetQueryString("select2")))
            {
                cid = GetQueryString("select2");

                DataRow[] dr = dtc.Select(" id = '" + cid + "'");
                if (dr.Length > 0)
                {
                    city = dr[0]["cityname"].ToString();
                    sinfo.Sqlstr += " and city='" + city + "' ";
                }

                
            }

            if (!string.IsNullOrEmpty(GetQueryString("basename")))
            {
                basename = GetQueryString("basename");
                sinfo.Sqlstr += " and basename like '%" + basename + "%' ";
            }
        }

        private void bind()
        {
            DataSet ds = hb.QXGetprodList(sinfo);

            if (!ds.Equals(null))
            {
                string partRights = isPartcode();
                DataTable dts = hb.getProdatable("SELECT SR_storecode  FROM  dbo.SYS_RightStore where SP_code='" + partRights + "'");


                allCount = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                lblCount.Text = allCount.ToString();

                DataTable dt = ds.Tables[1];

                dt.Columns.Add("ispoint");
                dt.Columns.Add("show");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dt.Rows[i]["px"].ToString()) && string.IsNullOrEmpty(dt.Rows[i]["py"].ToString()))
                    {
                        dt.Rows[i]["ispoint"] = "<span style=\"color:Red;\">否</span>";
                    }
                    else
                    {
                        dt.Rows[i]["ispoint"] = "是";
                    }

                    DataRow[] dr = dts.Select(" SR_storecode = '" + dt.Rows[i]["basecode"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        dt.Rows[i]["show"] = "";
                    }
                    else
                    {
                        dt.Rows[i]["show"] = styledisplay;
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
                    literalPagination.Text = GenPaginationBar("basezshstore.aspx?page=[page]", pagesize, curpage, allCount);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            #region exec格式处理

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

            #endregion
            //如果文件类型符合要求，则用SaveAs方法实现上传，并显示信息
            if (fileIsValid == true)
            {
                string ms = "";
                try
                {
                    string usercode = userCode();
                    string name = Server.MapPath("~/uploadxls/") + "IN" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";
                    this.FileUpload1.SaveAs(name);
                    if (File.Exists(name))
                    {

                        string storeName = "";
                        string crty = "";
                        string storecode = "";
                        string Fstorecode = "";
                        DataTable dt = ProductBLL.Search.Searcher.OpenCSV(name);

                        if (dt.Rows.Count > 0)
                        {

                            int number = Convert.ToInt32(hb.GetScalarstring("ISNULL(MAX(number),0)number", " dbo.BaseStore WHERE [type]=4   "));
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                #region 验证数据
                                storeName = dt.Rows[i]["油站名"].ToString();
                                if (storeName == "")
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.alert('油站名不能为空')</script>");
                                    return;
                                }
                                //storecode = dt.Rows[i][""].ToString();

                                Fstorecode = dt.Rows[i]["上级编码"].ToString();
                                if (Fstorecode == "")
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.alert('上级编码不能为空')</script>");
                                    return;
                                }
                                crty = dt.Rows[i]["城市"].ToString();
                                if (crty == "")
                                {
                                    Response.Write("<script type='text/javascript'>window.parent.alert('城市不能为空')</script>");
                                    return;
                                }

                                if (storecode != "")
                                {
                                    string ss = "BaseStore where type='4' and basecode='" + storecode + "'";

                                    if (hb.GetScalar(ss) != 0)
                                    {

                                        Response.Write("<script type='text/javascript'>window.parent.alert('编码已存在:" + storecode + "')</script>");

                                        return;
                                    }
                                }
                                #endregion


                                number = number + 1;
                                string lsh = Convert.ToString(number).PadLeft(4, '0');//流水号
                                string citycode = hb.GetScalarstring("AreaCode", "dbo.City LEFT JOIN dbo.CityProvince ON CityProvince.Id = City.ProvinceId WHERE cityname like '" + dt.Rows[i]["城市"].ToString().Trim() + "%' AND  Name like '" + dt.Rows[i]["省份"].ToString().Trim() + "%'");

                                string code = "SNP" + citycode + lsh;//编码规则
                                sqlbui.Append("  INSERT INTO dbo.BaseStore( Basecode ,basename ,[type],number , notes , storetype ,citycode , b_status ,selltype,OldBasecode,ChinazcomName,phone,address,fcode,nodeLevel,opcode) values('" + code + "','" + storeName + "','4','" + lsh + "','" + dt.Rows[i]["备注"].ToString() + "','SNP','" + citycode + "','" + dt.Rows[i]["状态"].ToString() + "','" + dt.Rows[i]["销售类型"].ToString() + "','" + code + "','" + dt.Rows[i]["站长"].ToString() + "','" + dt.Rows[i]["联系电话"].ToString() + "','" + dt.Rows[i]["详细地址"].ToString() + "','" + Fstorecode + "','3','" + usercode + "');");

                            }

                            if (hb.insetpro(sqlbui.ToString()))
                            {
                                Response.Write("<script type='text/javascript'>window.parent.alert('添加成功');window.location.href='../baseinfo/basezshstore.aspx'</script>");
                            }
                            else
                            {
                                Response.Write("<script type='text/javascript'>window.parent.alert('添加失败');window.location.href='../baseinfo/basezshstore.aspx'</script>");
                            }



                        }


                    }
                }

                #region catch
                catch (Exception ex)
                {

                    ms = ex.Message.ToString();
                    Response.Write("<script type='text/javascript'>window.parent.alert('文件内容格式不正确，请核实:" + ms + "')</script>");
                    return;
                }
                finally
                {
                    sqlbui.Clear();
                }
                #endregion
            }

        }


        protected void Btnderive_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/downloadxlsx/") + "Eexcl" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "") + ".csv";

            DataTable dtexec = hb.getProdatable(hiddpart.Value);


            dtexec.Columns["Basecode"].ColumnName = "编码";
            dtexec.Columns["basename"].ColumnName = "名字";
            dtexec.Columns["number"].ColumnName = "流水号";
            dtexec.Columns["b_status"].ColumnName = "状态";
            dtexec.Columns["selltype"].ColumnName = "销售类型";
            dtexec.Columns["cityname"].ColumnName = "城市";

            dtexec.Columns["ChinazcomName"].ColumnName = "站长";
            dtexec.Columns["phone"].ColumnName = "联系电话";
            dtexec.Columns["address"].ColumnName = "详细地址";

            if (ExcelHelper.TableToCsv(dtexec, path))
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


            //DataTable dtexec = hb.getProdatable("SELECT Basecode,basename,''''+number AS number ,cityname,b_status ,selltype,ChinazcomName,phone,address FROM  dbo.BaseStore  LEFT JOIN dbo.City ON citycode= AreaCode where [type] = 4");

            //dtexec.Columns["Basecode"].ColumnName = "编码";
            //dtexec.Columns["basename"].ColumnName = "名字";
            //dtexec.Columns["number"].ColumnName = "流水号";
            //dtexec.Columns["b_status"].ColumnName = "状态";
            //dtexec.Columns["selltype"].ColumnName = "销售类型";
            //dtexec.Columns["cityname"].ColumnName = "城市";

            //dtexec.Columns["ChinazcomName"].ColumnName = "站长";
            //dtexec.Columns["phone"].ColumnName = "联系电话";
            //dtexec.Columns["address"].ColumnName = "详细地址";
            //if (ExcelHelper.TableToCsv(dtexec, path))
            //{
            //    downloadfile(path);
            //    Response.Write("<script type='text/javascript'>window.parent.alert('导出EXecl成功!')</script>");

            //    return;
            //}
            //else
            //{
            //    Response.Write("<script type='text/javascript'>window.parent.alert('导出Execl失败!');window.location.href='../products/PriceRebateExecl.aspx';</script>");

            //    return;
            //}

        }
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
    }
}