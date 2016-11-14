using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace product.user
{
    public partial class CustomerOrder : Common.BasePage
    {
        protected int curpage = 1, pagesize = 20, allCount = 0;
        protected string userid = "", Edate, Bdate;
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        StringBuilder sqlpin = new StringBuilder();
        StringBuilder sqlinID = new StringBuilder();
        protected StringBuilder result = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Bdate = GetQueryString("bdate");
            Edate = GetQueryString("edate");
            userid = GetQueryString("userid");
            if (!IsPostBack)
            {
               DataTable dbcity=iscitycode();
               if (dbcity.Rows.Count > 0)
               {
                   for (int i = 0; i < dbcity.Rows.Count; i++)
                   {
                       hiddpart.Value += "'"+dbcity.Rows[i][0].ToString() +"',";//该角色城市编码
                   }
                    hiddpart.Value= hiddpart.Value.Remove(hiddpart.Value.Length - 1, 1); //去掉最后一逗号
               }
               else
               {
                   hiddpart.Value = "0";
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
                    literalPagination.Text = GenPaginationBar("CustomerOrder.aspx?page=[page]&bdate=" + Bdate + "&edate=" + Edate + "&userid=" + userid, pagesize, curpage, allCount);
                }
            }
              
        }

        /// <summary>
        /// 如果是点击查询功能首次要把PageIndex 重置为1
        /// </summary>
        private void InitParam(string Index)
        {

            sinfo.PageSize = 20;


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

                sinfo.PageIndex = 1;
            }


            #region
            sinfo.Sqlstr = " (SELECT OrderPhone.id,(Dimension+ PATTERN) AS canshu,UserID,yuyuenum,OrderPhone.userName,usertel ";
              sinfo.Sqlstr += " ,(OilstationName+ISNULL(serverlocation,'')) AS youzhan,marktime,quantity,price, ";
  sinfo.Sqlstr+="(CASE WHEN orderclass=0 THEN '站内' ELSE '站外' END) AS 'orderclass' ";
  sinfo.Sqlstr+=",(CASE WHEN statustype=1 THEN '已预约'  WHEN statustype=2 THEN '已完成'  WHEN statustype=3 THEN '已取消'  END) AS 'statustype'";
   sinfo.Sqlstr+=", cancelreason,(dafen +':' +pingjiadetails ) AS pingjia  FROM  dbo.OrderPhone ";
  sinfo.Sqlstr+="INNER JOIN dbo.UserInfoForeground ON  UserInfoForeground.userName=UserID ";
  sinfo.Sqlstr += "   LEFT JOIN  dbo.products  ON products.rpcode = OrderPhone.rpcode ";
  sinfo.Sqlstr+=" WHERE Utype=0 AND OrderPhone.citycode in("+ hiddpart.Value +"))hh ";
            if (!string.IsNullOrEmpty(Bdate) && !string.IsNullOrEmpty(Edate))
            {
                sinfo.Sqlstr += " where  CONVERT(VARCHAR(20), marktime, 23) BETWEEN '" + Bdate + "' AND  '" + Edate + "' ";
            }
            else
            {
                sinfo.Sqlstr += " where 1=1 ";
            }

            if (userid != "")
            {
                sinfo.Sqlstr += " and  userid LIKE'%" + userid + "%' ";
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
            if (!string.IsNullOrEmpty(Request.Form["userid"]))
            {
                userid = Request.Form["userid"].Trim();
            }
            if (!string.IsNullOrEmpty(Request["bdata"]))
            {
                Bdate = Request["bdata"].Trim();
            }
            if (!string.IsNullOrEmpty(Request["edata"]))
            {
                Edate = Request["edata"].Trim();
            }
            try
            {
                InitParam("query");
                bindData();
            }
            catch { }
        }
    }
}