using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace product.products
{
    public partial class addout : Common.BasePage
    {
        protected StringBuilder result;
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				Hidpart.Value = isPartcode();
                bind();
            }
        }
        protected void dpwh_SelectedIndexChanged(object sender, EventArgs e)
        {
            productlist();
        }
        private void productlist()
        {

            if (dpwh.SelectedItem.ToString() != "请选择")
            {

                string storeCode = dpwh.SelectedItem.Value;


                string str = "h.rpcode + '数量:' +  CONVERT(VARCHAR(20), qty) AS rpcode  ,  CONVERT(VARCHAR(20), h.rpcode) +'<>'+h.CAD+'<>数量:' + CONVERT(VARCHAR(20), qty) AS rpname";
                DataTable rpcodedb = hb.getdate(str, " (SELECT products.rpcode ,products.CAD,stockcode as WHCode ,SUM(ISNULL(stockNum,0)) AS qty FROM dbo.stock_storck   LEFT JOIN dbo.products ON products.rpcode = stock_storck.rpcode where stockcode='" + storeCode + "'  GROUP BY products.rpcode ,stockcode,products.CAD )h ");
                result = new StringBuilder();
                result.Append("[");
                for (int i = 0; i < rpcodedb.Rows.Count; i++)
                {
                    result.Append("{ value: \"" + rpcodedb.Rows[i]["rpcode"].ToString() + "\",label:\" " + rpcodedb.Rows[i]["rpname"].ToString() + "\"},");
                }
                result.Remove(result.Length - 1, 1);//把最后一个，给移除

                result.Append(" ]");
             


            }
           
        }
   
        protected void Button1_Click(object sender, EventArgs e)
        {
            
		
            try
            {
				//string sql = "INSERT INTO  dbo.outproduct( rpcode ,ShGoogcode,CAD ,  OD , QTY ,WHcode , spmark ,TSNote , usercode ,statustype ) values ('" + dpproduct.SelectedValue + "','" + ShGoogcode.Value + "','" + cad.Value + "','" + OD.Value + "','" + QTY.Value + "','" + dpwh.SelectedItem.Value + "','" + spmark.SelectedValue + "','','" + userCode() + "','直接出库' )";
				#region 批次号
				//LT+R/C日期 +1
				//当天导入 数字累加
				//LTR20151120-1

				string batch = "LT";
				int num = 1;
				string day = DateTime.Today.ToString("yyyy-MM-dd");
				string Scalar = hb.GetScalarstring(" ISNULL(MAX(batchCount),'') ", " dbo.outproduct   where CONVERT(VARCHAR(20),insettime,23)='" + day + "'");

				if (Scalar != "") {
					num = Convert.ToInt32(Scalar) + 1;
				}
				string batchnum = batch + "C" + day.Replace("-", "") + "-" + num;
				#endregion
                
                string sql = "exec pro_outstore  '" + rpcode.Value.Substring(0, 10)+ "', '" + OD.Value + "', '" + dpwh.SelectedItem.Value + "'," + QTY.Value + "," + spmark.SelectedValue + ",'','" + userCode() + "','" + batchnum + "','" + num + "'";


                if (hb.insetpro(sql))
                {
                    Response.Write("<script type='text/javascript'>alert('添加成功');window.location.href='../products/outproductList.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../products/outproductList.aspx';</script>");
                }
            }
            catch 
            {
                Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../products/inproductList.aspx';</script>");
            }
        }
        void bind()
        {
			DataTable dt;
			// 根据仓库权限 来显示仓库
			string sql = " (SELECT Basecode,basename FROM dbo.BaseStore INNER JOIN   dbo.SYS_RightStore  ON  Basecode=SR_storecode WHERE SP_code='" + Hidpart.Value + "')hh";

			dt = hb.getdate(" * ", sql);

			dpwh.DataSource = dt; //出库
			dpwh.DataTextField = "basename";
			dpwh.DataValueField = "basecode";
			dpwh.AutoPostBack = true;
			dpwh.DataBind();
			dpwh.Items.Insert(0, "请选择");

          
        }
       
    }
}