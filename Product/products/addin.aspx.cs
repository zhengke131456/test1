using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Text;

namespace product.products
{
    public partial class addin :Common.BasePage
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            panduan();

			int n = hb.GetScalar("dbo.products WHERE rpcode ='" + rpcode.Value + "'");


			if (n <= 0) {
				Response.Write("<script type='text/javascript'>alert('睿配编码不存在！');window.location.href='../products/inproductList.aspx';</script>");
				return;
			}
            try
            {
				//string sql = "INSERT dbo.inproduct( rpcode ,ShGoogcode ,CAD ,  OD ,  FHDate , SHDate ,THData ,QTY ,WHCode ,spmark , inprice , usercode , statustype) values ('" + rpcode.Value + "','" + ShGoogcode.Value + "','" + cad.Value + "','" + OD.Value + "','" + FHDate.Value + "','" + SHDate.Value + "','" + THData.Value + "','" + QTY.Value + "','" + dpwh.SelectedValue + "','" + spmark.SelectedValue + "','" + inprice.Value + "','" + userCode() + "','直接入库')";



				#region 批次号
				//LT+R/C日期 +1
				//当天导入 数字累加
				 //LTR20151120-1
				
				string batch = rpcode.Value.Substring(0, 2);
				int num = 1;
			     string day=	DateTime.Today.ToString("yyyy-MM-dd");
				 string Scalar = hb.GetScalarstring(" ISNULL(MAX(batchCount),'') ", " dbo.inproduct   where CONVERT(VARCHAR(20),insettime,23)='" + day + "'");
				
				 if (Scalar != "") {
					 num = Convert.ToInt32(Scalar) + 1;
				 }
				 string batchnum = batch + "R" + day.Replace("-", "") + "-" + num;
				#endregion

				 string sql = "exec pro_instore  '" + rpcode.Value + "','" + OD.Value + "','" + FHDate.Value + "','" + SHDate.Value + "','" + THData.Value + "','" + QTY.Value + "','" + dpwh.SelectedValue + "','" + Convert.ToInt32(spmark.SelectedValue) + "','','" + inprice.Value + "' ,'" + userCode() + "','','','','" + batchnum + "','" + num + "'";


                if (hb.insetpro(sql))
                {

                    Response.Write("<script type='text/javascript'>alert('添加成功');window.location.href='../products/inproductList.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../products/inproductList.aspx';</script>");
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
            DataTable rpcodedb;
			// 根据仓库权限 来显示仓库
            //string sql = " (SELECT Basecode,basename FROM dbo.BaseStore INNER JOIN   dbo.SYS_RightStore  ON  Basecode=SR_storecode WHERE SP_code='" + Hidpart.Value + "')hh";

            //dt = hb.getdate(" * ", sql);
           
            string sql = " select * from (SELECT Basecode,basename FROM dbo.BaseStore INNER JOIN   dbo.SYS_RightStore  ON  Basecode=SR_storecode WHERE SP_code='" + Hidpart.Value + "')hh; SELECT rpcode,rpcode+'<>'+CAD AS rpname FROM  dbo.products ";
            DataSet ds = hb.GetDataSet(sql);

            dt = ds.Tables[0];
            rpcodedb = ds.Tables[1];

			dpwh.DataSource = dt; //出库
			dpwh.DataTextField = "basename";
			dpwh.DataValueField = "basecode";
			dpwh.AutoPostBack = true;
			dpwh.DataBind();
			dpwh.Items.Insert(0, "请选择");
            result=  new StringBuilder();
            result.Append("[");
            for (int i = 0; i < rpcodedb.Rows.Count; i++)
            {
                result.Append("{ value: \"" + rpcodedb.Rows[i]["rpcode"].ToString() + "\",label:\" " + rpcodedb.Rows[i]["rpname"].ToString() + "\"},");
            }
            result.Remove(result.Length - 1, 1);//把最后一个，给移除


            result.Append(" ]");


           
        }
        void panduan() 
        {

        
            if (dpwh.SelectedItem.Text == "请选择")
            {
                dpwh.SelectedItem.Text = "";
            }


	      
        }
    }
}