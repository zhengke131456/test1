using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.delete
{
    public partial class delete : Common.BasePage
    {

        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = GetQueryString("id");
            string sTable = GetQueryString("table");
            string type = GetQueryString("type");

            bool bl = false;
            string sResult = "";

            string sql = "DELETE dbo.SYS_RightStore  WHERE  SP_code='" + isPartcode() + "' AND SR_storecode IN (SELECT Basecode FROM  dbo.BaseStore WHERE id='" + id + "')";


            if (sTable == "BaseStore")
            {
                DeleteLog.InsertLog(id, 3);

                //删除该角色仓库权限
                bl = hb.ProExecinset(sql);
                //管理员操作则直接删除仓库
                if (isPartcode() == "0001")
                    bl = hb.delbase(sTable, id);
            }
            else if (sTable == "baseinfo")
            {
                DeleteLog.InsertLog(id, 4);
                bl = hb.delbase(sTable, id);
            }

            else if (sTable == "userinfo")
            {
                DeleteLog.InsertLog(id, 5);
                bl = hb.delbase(sTable, id);
            }

            else
            {
                DeleteLog.InsertLog(id, 2);
                string str = "(select states from transferslip where id='" + id + "')hh";
                DataTable dt = hb.getdate("*", str);
                string s = dt.Rows[0][0].ToString();
                if (s == "待出库")
                    bl = hb.delbase(sTable, id);
            }

            if (bl == true)
            {
                sResult = "{\"result\":\"100\"}"; //删除成功
            }
            else
            {

                sResult = "{\"result\":\"200\"}"; //失败



            }

            Response.Write(sResult);
            Response.End();
        }
    }
}