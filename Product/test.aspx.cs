using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product
{
    public partial class test : Common.BasePage
    {
        protected SJYEntity.Common.Search sinfo = new SJYEntity.Common.Search();
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected string show = "2";//三级还是二级联动

        protected void Page_Load(object sender, EventArgs e)
        {
            string prentId = HttpContext.Current.Request.Form["ParentID"];



            //总仓
            string partRights = isPartcode();//当前角色
            sinfo.Sqlstr = " dbo.BaseStore WHERE   Basecode  IN ( SELECT SR_storecode  FROM  dbo.SYS_RightStore where SP_code='" + partRights + "' AND [type]= '0' ) ";
            sinfo.PageSize = 10000;
            sinfo.Orderby = "type";

            DataSet ds = new DataSet();
            ds = hb.QXGetprodList(sinfo);
            DataTable dt = ds.Tables[1];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Fcode"].ToString() == "0")
                {
                    show = "3";
                    break;
                }
            }

        }
    }
}