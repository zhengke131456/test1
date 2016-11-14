using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.IO;

namespace product.products
{
    public partial class TJ_InStock : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected string rpcode = "", Edate, Bdate;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        StringBuilder sqlpin = new StringBuilder();
        StringBuilder sqlinID = new StringBuilder();

        protected string userCode = ""; //角色

        protected StringBuilder result = new StringBuilder();
        protected string ckids = "";//选中仓库编码串

        
        protected void Page_Load(object sender, EventArgs e)
        {
            //是否首次
            if (!IsPostBack)
            {
                try
                {
                    getvalue();
                    InitParam("");
                }
                catch { }
            }

        }

        private void getvalue()
        {
            userCode = isPartcode();//角色 

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

            if (!string.IsNullOrEmpty(GetQueryString("rpcode")))
            {
                rpcode = GetQueryString("rpcode");
            }
            else rpcode = "";
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

        /// <summary>
        /// 如果是点击查询功能首次要把PageIndex 重置为1
        /// </summary>
        private void InitParam(string Index)
        {
            #region

            string temp_where = "  and 1=1 ";
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
                temp_where += basecodes;
            }

            if (Bdate != "" && Edate != "")
            {
                temp_where += " and  CONVERT(VARCHAR(20), SHDate, 23) BETWEEN '" + Bdate + "' AND  '" + Edate + "' ";
            }

            if (rpcode != "")
            {
                temp_where += " and  rpcode LIKE '%" + rpcode + "%' ";
            }

            //总数
            string sqlnum = " select count(*) from  ( select whcode,rpcode,SUM(QTY)as qyt from (select  * from inproduct where WHCode in (select sr_storecode from dbo.SYS_RightStore where SP_code='"+userCode+"')  "+temp_where+" ) a group by WHCode,rpcode )b";


            //分页
            int tempstar = (sinfo.PageIndex - 1) * sinfo.PageSize;
            int tempend = sinfo.PageIndex * sinfo.PageSize + 1;

            string sqltr = "select top " + sinfo.PageSize + "  *  from (select  * ,row_number() over(order by whcode,rpcode desc ) as rn from " +
 "( select whcode,rpcode,SUM(QTY)as QTY  from " +
 "(select  * from inproduct where WHCode in (select sr_storecode from dbo.SYS_RightStore where SP_code='" + userCode + "') " + temp_where + " ) a " +
 " group by WHCode,rpcode )as b )as aa where (rn > " + tempstar + " and rn< " + tempend + ")";

            //下载sql
            hiddpart.Value = " select whcode,rpcode,SUM(QTY)as QTY  from " +
 "(select  * from inproduct where WHCode in (select sr_storecode from dbo.SYS_RightStore where SP_code='" + userCode + "') " + temp_where + " ) a " +
 " group by WHCode,rpcode ";


            //数量
            DataTable temp_sum = hb.GetDataSet(sqlnum).Tables[0];
            allCount = int.Parse(temp_sum.Rows[0][0].ToString());
            lblCount.Text = allCount.ToString();

            DataSet temp_mes = hb.GetDataSet(sqltr);
            DataTable dt = temp_mes.Tables[0];

            //产品
            DataSet temp_p = hb.GetDataSet("select rpcode ,cad,acrosswidth,gkb,r,rimdia,pattern from products ");

            //仓库
            DataSet temp_stock = hb.GetDataSet("select basename ,basecode from basestore ");

            dt.Columns.Add("cad");
            dt.Columns.Add("basename");
            dt.Columns.Add("xh");
            dt.Columns.Add("pattern");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DataRow[] dr = temp_p.Tables[0].Select(" rpcode = '" + dt.Rows[i]["rpcode"].ToString()+ "'");
                if (dr.Length > 0) 
                {
                    dt.Rows[i]["cad"] = dr[0]["cad"].ToString();
                    dt.Rows[i]["xh"] = dr[0]["acrosswidth"].ToString() + "/" + dr[0]["gkb"].ToString() + dr[0]["r"].ToString() + dr[0]["rimdia"].ToString();
                    dt.Rows[i]["pattern"] = dr[0]["pattern"].ToString();
                }

                DataRow[] drr = temp_stock.Tables[0].Select(" basecode = '" + dt.Rows[i]["whcode"].ToString() + "'");
                if (drr.Length > 0)
                {
                    dt.Rows[i]["basename"] = drr[0]["basename"].ToString();
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
                literalPagination.Text = GenPaginationBar("tj_instock.aspx?page=[page]&bdate=" + Bdate + "&edate=" + Edate + "&rpcode=" + rpcode + "&ckids=" + ckids + "", pagesize, curpage, allCount);
            }

            #endregion

        }


        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Buttondow_Click(object sender, EventArgs e)
        {
            string path = Server.MapPath("~/downloadxlsx/TJ/");
            string datetime = "入库统计" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").ToString().Replace("-", "");

            string detailfile = ".csv";//明细

            if (hiddpart.Value == "") return;

            DataTable dt = hb.GetDataSet(hiddpart.Value).Tables[0];


            DataSet temp_p = hb.GetDataSet("select rpcode ,cad,acrosswidth,gkb,r,rimdia,pattern from products ");

            //仓库
            DataSet temp_stock = hb.GetDataSet("select basename ,basecode from basestore ");

            dt.Columns.Add("cad");
            dt.Columns.Add("basename");
            dt.Columns.Add("xh");
            dt.Columns.Add("pattern");

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                DataRow[] dr = temp_p.Tables[0].Select(" rpcode = '" + dt.Rows[i]["rpcode"].ToString() + "'");
                if (dr.Length > 0)
                {
                    dt.Rows[i]["cad"] = dr[0]["cad"].ToString();
                    dt.Rows[i]["xh"] = dr[0]["acrosswidth"].ToString() + "/" + dr[0]["gkb"].ToString() + dr[0]["r"].ToString() + dr[0]["rimdia"].ToString();
                    dt.Rows[i]["pattern"] = dr[0]["pattern"].ToString();
                }

                DataRow[] drr = temp_stock.Tables[0].Select(" basecode = '" + dt.Rows[i]["whcode"].ToString() + "'");
                if (drr.Length > 0)
                {
                    dt.Rows[i]["basename"] = drr[0]["basename"].ToString();
                }

            }


            dt.Columns["WHcode"].SetOrdinal(0);
            dt.Columns["basename"].SetOrdinal(1);
            dt.Columns["rpcode"].SetOrdinal(2);
            dt.Columns["CAD"].SetOrdinal(3);
            dt.Columns["xh"].SetOrdinal(4);
            dt.Columns["pattern"].SetOrdinal(5);
            dt.Columns["QTY"].SetOrdinal(6);
            


            dt.Columns["WHcode"].ColumnName = "入库仓库编码";
            dt.Columns["basename"].ColumnName = "入库仓库名称";
            dt.Columns["rpcode"].ColumnName = "睿配编码";
            dt.Columns["CAD"].ColumnName = "CAD编码";
            dt.Columns["xh"].ColumnName = "型号";
            dt.Columns["pattern"].ColumnName = "花纹";
            dt.Columns["QTY"].ColumnName = "数量";


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
}