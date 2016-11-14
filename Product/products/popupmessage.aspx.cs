using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace product.products
{
    public partial class popupmessage : Common.BasePage
    {
        protected string Bdate;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            
            //string type = hiddtype.Value;
            //string id = Hidden1.Value;
            //string wh = Hidden1WH.Value;
            //string date = bdata.Value;
            //string username = Common.BasePage.userCode();
            //if (date == "")
            //{
            //    //Response.Write("{\"msg\":\"<script type='text/javascript'> window.returnValue='时间不能为空';window.close();</script>\"}");
            //    Response.Write("时间不能为空");
            //    Response.Write("<script type='text/javascript'> window.parent.returnValue='时间不能为空';window.parent.close();</script>");
            //}
            //else
            //{
            //    int cout = hb.GetScalar("  dbo.SYS_RightStore INNER JOIN dbo.userinfo ON dbo.SYS_RightStore.SP_code = dbo.userinfo.SP_Code WHERE username='" + username + "' AND SR_storecode='" + wh + "'"); //表示有权限操作该仓库
            //    if (cout > 0)
            //    {
            //        if (type == "1")
            //        {
            //            //出库
            //            if (hb.ProExecinset("exec pro_TransferslipStore '" + id + "','" + date + "','" + username + "','1'"))
            //            {
            //                Response.Write("<script type='text/javascript'> window.returnValue='出库成功';window.close();</script>");
            //                //成功
            //            }
            //            else
            //            {
            //                Response.Write("<script type='text/javascript'> window.returnValue='出库失败';window.close();</script>");
            //                //失败
            //            }

            //        }
            //        else
            //        {
            //            if (hb.ProExecinset("exec pro_TransferslipStore '" + id + "','" + date + "','" + username + "','0'"))
            //            {
            //                Response.Write("<script type='text/javascript'> window.returnValue='入库成功'; window.close();</script>");
            //            }
            //            else
            //            {
            //                Response.Write("<script type='text/javascript'> window.returnValue='入库失败'; window.close();</script>");
            //            }
            //        }

            //    }
            //    else
            //    {
            //        Response.Write("<script type='text/javascript'>window.returnValue='无权限操作该仓库';window.close();</script>");
            //        //无权限
            //    }
            //}
        }

    }
}