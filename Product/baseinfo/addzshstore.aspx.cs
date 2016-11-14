using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProductBLL.Search;
using ProductBLL.Basebll;
using System.Collections;
using System.Configuration;
namespace product.baseinfo
{
    public partial class addzshstore : Common.BasePage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if(!IsPostBack)
            {
              bind();
            }
        }

        private void bind()
        {

            dpselltype.Items.Add("销售");
            dpselltype.Items.Add("展示");
			dpselltype.Items.Add("代销");
            dpstatus.Items.Add("运营");
            dpstatus.Items.Add("关闭");
            dpprovince.DataSource = hb.getdate("  id,Name", "dbo.CityProvince WHERE  1=1 ");
            dpprovince.DataTextField = "Name";
            dpprovince.DataValueField = "id";
            dpprovince.DataBind();
            dpprovince.Items.Insert(0, "请选择省份");

            IDictionary dict = (IDictionary)ConfigurationManager.GetSection("District");
            if (dict.Count > 0)
            {
                foreach (string key in dict.Keys)
                {
                    dparea.Items.Add(dict[key].ToString());
                }
                dparea.Items.Insert(0, "请选择");
            }
           

        }
        protected void dpprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dpprovince.SelectedItem.ToString() != "请选择")
            {

                string storeCode = dpprovince.SelectedItem.Value;
                dptown.DataSource = hb.getdate(" AreaCode,cityname", "dbo.City WHERE  ProvinceId='" + storeCode + "' ");
                dptown.DataTextField = "cityname";
                dptown.DataValueField = "AreaCode";
                dptown.DataBind();
                dptown.Items.Insert(0, "请选择城市");



            }
            else
            {
                dptown.Items.Clear();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

			//if (!string.IsNullOrEmpty(code.Value))
			//{
			//    code.Value = code.Value;

			//}
            if (!string.IsNullOrEmpty(name.Value))
            {
                name.Value = name.Value;

            }
			string ss = "dbo.BaseStore where type='0'  and basecode='" + fcode.Value + "'";

            if (hb.GetScalar(ss)!= 0)
            {
                int number = Convert.ToInt32(hb.GetScalarstring("ISNULL(MAX(number),0)", "dbo.BaseStore where [type]=4   "));
                //仓库流水号
                number = number + 1;
                string num = Convert.ToString(number).PadLeft(4, '0');
                string lsh = "SNP" + dptown.SelectedValue.ToString() + num;

				string table = " INSERT INTO dbo.BaseStore  ( Basecode , basename ,   area , [type] , citycode , number , notes ,storetype,b_status,selltype ,OldBasecode,Fcode,nodeLevel,opcode) VALUES  ( '" + lsh + "','" + name.Value + "','" + dparea.SelectedItem.Value + "','4','" + dptown.SelectedItem.Value + "','" + num + "','" + note.Value + "','SNP','" + dpstatus.SelectedItem.Value + "','" + dpselltype.SelectedItem.Value + "','" + lsh + "','" + fcode.Value + "','3','" + userCode() + "' ) ";
                if (hb.insetpro(table))
                {
                    Response.Write("<script type='text/javascript'>alert('添加成功');window.location.href='../baseinfo/basezshstore.aspx';</script>");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('添加失败');window.location.href='../baseinfo/basezshstore.aspx';</script>");
                }
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('上级仓库不存在！');window.location.href='../baseinfo/basezshstore.aspx';</script>");
            }
           
        }
    }
}