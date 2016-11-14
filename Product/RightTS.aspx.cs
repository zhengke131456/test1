using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product
{
    public partial class RightTS : Common.BasePage
    {

        protected string show1 = "";//待审核
        protected string show2 = "";//待出库
        protected string show3 = "";//待入库
        protected string show4 = "";//异常

        protected string shows1 = "";//待审核
        protected string shows2 = "";//待出库
        protected string shows3 = "";//待入库
        protected string shows4 = "";//异常

        protected string uc = "0005";//亚静角色

        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected void Page_Load(object sender, EventArgs e)
        {
            string userPart = isPartcode();
            //待审核

            string sql1 = "SELECT count(1) FROM [transferslip]  where states='待审核' ";

            DataSet ds1 = hb.GetDataSet(sql1);
            int si = int.Parse(ds1.Tables[0].Rows[0][0].ToString());

            if ((userPart == uc || userPart =="0001") && si > 0)
            {
                show1 = "1";
                shows1 = si.ToString();
            }

            //待出库
            string sql2 = "SELECT count(1) FROM [transferslip]  where states='待出库' and outWH in (select SR_storecode from SYS_RightStore where Sp_code='" + userPart + "') ;";
            DataSet ds22 = hb.GetDataSet(sql2);

            int si2 = int.Parse(ds22.Tables[0].Rows[0][0].ToString());

            if (si2 > 0)
            {
                show2 = "1";
                shows2 = si2.ToString();
            }

            //待入库
            string sql3 = "SELECT count(1) FROM [transferslip]  where states='待入库' and inWH in (select SR_storecode from SYS_RightStore where Sp_code='" + userPart + "') ;";
            DataSet ds33 = hb.GetDataSet(sql3);

            int si3 = int.Parse(ds33.Tables[0].Rows[0][0].ToString());

            if (si3 > 0)
            {
                show3 = "1";
                shows3 = si3.ToString();
            }

            //异常调拨
            string sql4 = "SELECT count(1) FROM [transferslip]  where (states='出库异常' or states='入库异常')  ;";
            DataSet ds44= hb.GetDataSet(sql4);

            int si4= int.Parse(ds44.Tables[0].Rows[0][0].ToString());

            if ((userPart == uc || userPart == "0001") && si4 > 0)
            {
                show4= "1";
                shows4 = si4.ToString();
            }
        }
    }
}