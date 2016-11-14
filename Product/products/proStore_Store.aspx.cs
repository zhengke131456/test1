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

    public partial class proStore_Store : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected string District = "";
        protected string cai = "", strockName = "", xinghao = "";
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected StringBuilder result = new StringBuilder();
        protected StringBuilder sqlCad = new StringBuilder();//查看编码+年份是否重复
        protected int n = 0;
        protected string ckids = "";//选中仓库编码串

        protected string userPart = ""; //角色

        protected string Opstr = "";//型号串

        protected string dimension = "";//选中型号

        protected void Page_Load(object sender, EventArgs e)
        {
            
            cai = Request.QueryString["QCAI"];
            strockName = Request.QueryString["strockName"];
            xinghao = Request.QueryString["xh"];

            userPart = isPartcode();

            if (!IsPostBack)
            {
                try
                {
                    Querey();
                    InitParam("");
                    bindData();
                    bind();
                }
                catch { }

            }
        }
        /// <summary>
        /// 绑定油站名称数据
        /// </summary>
        private void bind()
        {
            //DataTable dt;

            //// 根据仓库权限 来显示仓库
            ////string sql = " (SELECT Basecode,basename FROM dbo.BaseStore INNER JOIN   dbo.SYS_RightStore  ON  Basecode=SR_storecode WHERE SP_code='" + Hidpart.Value + "')hh";
            //string str = "(select * from BaseStore where Basecode in (select SR_storecode from SYS_RightStore where SP_code='" + userPart + "'))hh";
            //dt = hb.getdate(" * ", str);

            //dpstoreNew.DataSource = dt;

            //dpstoreNew.DataTextField = "basename";
            //dpstoreNew.DataValueField = "basename";
            //dpstoreNew.DataBind();
            //dpstoreNew.Items.Insert(0, "请选择");

            //绑定全部型号列表
            string str = "select * from baseinfo where type=3 ";
            DataTable dt = hb.GetDataSet(str).Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    Opstr += "<option value=\"\" >请选择</option><option value=\"" + dt.Rows[i]["basename"].ToString() + "\" >" + dt.Rows[i]["basename"].ToString() + "</option>";
                }
                else
                {
                    Opstr += "<option value=\"" + dt.Rows[i]["basename"].ToString() + "\" >" + dt.Rows[i]["basename"].ToString() + "</option>";
                }
            }
        }


        private void bindData()
        {

            DataTable rpcodedb = hb.getdate("rpcode,rpcode+'<>'+CAD AS rpname", "dbo.products");

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


                dislist.DataSource = ds.Tables[1];
                dislist.DataBind();
                if (allCount <= pagesize)
                {
                    literalPagination.Visible = false;
                }
                else
                {
                    literalPagination.Visible = true;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("proStore_Store.aspx?page=[page]");
                    if (strockName != "")
                        sb.Append("&strockName=" + strockName);
                    if (xinghao != "")
                        sb.Append("&xh=" + xinghao);
                    if (cai != "")
                        sb.Append("&QCAI=" + cai);
                    if (ckids != "")
                        sb.Append("&ckids=" + ckids);
                    literalPagination.Text = GenPaginationBar(sb.ToString(), pagesize, curpage, allCount);
                }
            }
        }


        private void InitParam(string Index)
        {

            #region
            sinfo.PageSize = 20;

            sinfo.Orderby = "ID";

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


            if (Index == "query")
            {
                sinfo.Sqlstr = "(SELECT dbo.stock_storck.ID,dbo.stock_storck.rpcode,stockNum,stockjtNum,basename as stockName,Basecode,desNote ,stockID ,stockcode,    ''''+ CONVERT(VARCHAR(30),ShGoogcode) as ShGoogcode,dbo.products.CAD,Dimension,PATTERN FROM dbo.stock_storck  LEFT JOIN dbo.BaseStore ON  dbo.BaseStore.id=stockID INNER JOIN dbo.SYS_RightStore ON stockcode=SR_storecode LEFT JOIN dbo.products ON dbo.products.rpcode=dbo.stock_storck.rpcode where sp_code='" + userPart + "')hh ";
            }
            else
            {
                sinfo.Sqlstr = "(SELECT dbo.stock_storck.ID,dbo.stock_storck.rpcode,stockNum,stockjtNum,basename as stockName,Basecode,desNote ,stockID ,   ShGoogcode  ,dbo.products.CAD,Dimension,PATTERN,stockcode FROM dbo.stock_storck  LEFT JOIN dbo.BaseStore ON  dbo.BaseStore.id=stockID INNER JOIN dbo.SYS_RightStore ON stockcode=SR_storecode LEFT JOIN dbo.products ON dbo.products.rpcode=dbo.stock_storck.rpcode where sp_code='" + userPart + "')hh ";
            }

            sinfo.Sqlstr += " where ( stockNum>0  or stockjtNum>0)";

            if (!string.IsNullOrEmpty(cai))
            {
                sinfo.Sqlstr += " and  rpcode= '" + cai + "'";
            }
            if (!string.IsNullOrEmpty(dimension))
            {
                sinfo.Sqlstr += " and  dimension= '" + dimension + "'";
            }
           
               

            //if (!string.IsNullOrEmpty(strockName))
            //{
            //    sinfo.Sqlstr += " and  stockName LIKE'%" + strockName + "%' ";
            //    dpstoreNew.SelectedValue = strockName;
            //}
            //if (!string.IsNullOrEmpty(xinghao))
            //{
            //    sinfo.Sqlstr += " and  Dimension ='" + xinghao + "' ";
            //    dimension.SelectedValue = xinghao;
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
                        basecodes += " stockcode = '" + ck[i] + "' ) ";
                    }
                    else
                    {
                        basecodes += " stockcode = '" + ck[i] + "' or  ";
                    }
                }
            }

            if (basecodes != "")
            {
                sinfo.Sqlstr += basecodes;
            }

            hiddpart.Value = sinfo.Sqlstr;

            sinfo.Orderby = " stockNum desc ";
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

        public void Querey()
        {
            if (!string.IsNullOrEmpty(Request.Form["QCAI"]))
            {
                cai = Request.Form["QCAI"].Trim();
            }
            else cai = "";
            //if (dpstoreNew.SelectedItem.ToString() != "请选择")
            //{
            //    strockName = dpstoreNew.SelectedItem.ToString();
            //}
            //else strockName = "";


            if (!string.IsNullOrEmpty(GetQueryString("page")))
            {
                curpage = int.Parse(GetQueryString("page"));
                sinfo.PageIndex = curpage;
            }


            if (!string.IsNullOrEmpty(GetQueryString("dimension")))
            {
                dimension = GetQueryString("dimension");
            }

            if (!string.IsNullOrEmpty(GetQueryString("ckids")))
            {
                ckids = GetQueryString("ckids");
            }
        }


        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Buttondow_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/downloadxlsx/Order/");
            string datetime = "库存" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "");

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

            //Querey();//获取查询参数
            //InitParam("query");
            string strsql = "rpcode as '睿配编码',ShGoogcode  as '石化商品编码',cad as '供应商编码',Dimension as '型号',PATTERN as '花纹'";
            strsql += ",stockName  as '仓库名',	Basecode  as '仓库编码',stockNum as '数量'  ";


            DataTable dtdb = hb.getdate(strsql, hiddpart.Value);

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