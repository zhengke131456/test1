using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using product.Common;
using System.Data;

namespace product.Handler
{
    /// <summary>
    /// warehouse 的摘要说明
    /// </summary>
    public class warehouse : IHttpHandler
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request["id"].ToString();
            string date = context.Request["date"].ToString();
            string type = context.Request["type"].ToString();
            string wh = context.Request["wh"].ToString();
            string username = Common.BasePage.userCode();
            int i = 2;

            int cout = hb.GetScalar("  dbo.SYS_RightStore INNER JOIN dbo.userinfo ON dbo.SYS_RightStore.SP_code = dbo.userinfo.SP_Code WHERE username='" + username + "' AND SR_storecode='" + wh + "'"); //表示有权限操作该仓库
            if (cout > 0)
            {
                string str = "(select states from transferslip where id='" + id + "')hh";
                DataTable dt = hb.getdate("*", str);
                string s = "";
                if (dt.Rows.Count > 0)
                {
                    s = dt.Rows[0][0].ToString();
                    if (type == "1")
                    {

                        if (s == "待出库")
                        {
                            //出库
                            if (hb.ProExecinset("exec pro_TransferslipStore '" + id + "','" + date + "','" + username + "','1'"))
                                i = 0;
                            else i = 2;
                        }
                        else i = 4;
                    }
                    else
                    {
                        if (s == "待入库")
                        {
                            if (hb.ProExecinset("exec pro_TransferslipStore '" + id + "','" + date + "','" + username + "','0'"))
                                i = 0;
                            else i = 2;
                        }
                        else i = 4;
                    }
                }
                else
                {
                    i = 4;
                }


            }
            else i = 3;
            context.Response.Write("{\"result\":\"" + i + "\"}");
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