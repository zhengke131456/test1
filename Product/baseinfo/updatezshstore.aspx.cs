using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;

namespace product.baseinfo
{
    public partial class updatezshstore : Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 bind(); 
                if (!string.IsNullOrEmpty(GetQueryString("id")))
                {
                   
                     hiddID.Value = GetQueryString("id");
                     string where = "   dbo.BaseStore LEFT JOIN dbo.City  ON citycode=AreaCode WHERE [type]=4 AND dbo.BaseStore.id='" + hiddID.Value + "' ";

                     DataTable dt = hb.getdate("dbo.BaseStore.id, Basecode,basename,area,cityname,AreaCode,BaseStore.Fcode as storecode,dbo.City.ProvinceId ,selltype,b_status,ChinazcomName,phone,[address]", where);

                    if (dt.Rows.Count > 0)
                    {
						storecode.Value = dt.Rows[0]["storecode"].ToString();
                        dpprovince.SelectedValue = dt.Rows[0]["ProvinceId"].ToString();
                        dptown.SelectedValue = dt.Rows[0]["AreaCode"].ToString();
                        dparea.SelectedValue = dt.Rows[0]["area"].ToString();
                        code.Value = dt.Rows[0]["Basecode"].ToString();
                        name.Value = dt.Rows[0]["basename"].ToString();
                        dpselltype.SelectedValue = dt.Rows[0]["selltype"].ToString();
						ChinazcomName.Value = dt.Rows[0]["ChinazcomName"].ToString();
						phone.Value = dt.Rows[0]["phone"].ToString();
						address.Value = dt.Rows[0]["address"].ToString();
                        dpstatus.SelectedValue = dt.Rows[0]["b_status"].ToString();
                    }
                   

                }
            }
        }


        protected void dpprovince_SelectedIndexChanged(object sender, EventArgs e)
        {
            dptownliset();
        }
        private void dptownliset()
        {

            if (dpprovince.SelectedItem.ToString() != "请选择省份")
            {
                dptown.Items.Clear();
                string storeCode = dpprovince.SelectedItem.Value;
                dptown.DataSource = hb.getdate(" AreaCode,cityname", "dbo.City WHERE  ISNULL(ProvinceId,'')='" + storeCode + "' ");
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
        private void bind()
        {

            dpprovince.DataSource = hb.getdate("  name,Id", "dbo.CityProvince WHERE  1=1 ");
            dpprovince.DataTextField = "name";
            dpprovince.DataValueField = "id";
            dpprovince.DataBind();
            
            dpprovince.Items.Insert(0, "请选择省份");

            string storeCode = dpprovince.SelectedItem.Value;
            if (storeCode == "请选择省份")
            { dptown.DataSource = hb.getdate(" AreaCode,cityname", "dbo.City WHERE  1=1"); }
            else
            {
                dptown.DataSource = hb.getdate(" AreaCode,cityname", "dbo.City WHERE  ProvinceId='" + storeCode + "'");
            }


    
            dptown.DataTextField = "cityname";
            dptown.DataValueField = "AreaCode";
            dptown.DataBind();
            dptown.Items.Insert(0, "请选择城市");

            dpselltype.Items.Add("销售");
            dpselltype.Items.Add("展示");
			dpselltype.Items.Add("代销");
            dpstatus.Items.Add("运营");
            dpstatus.Items.Add("关闭");

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (dparea.SelectedItem.Text == "请选择")
            {
                dparea.SelectedItem.Text = "";
            }
			  

			string ss = "BaseStore where  basecode='" + code.Value + "'and id!='" + hiddID.Value + " '";

             if (hb.GetScalar(ss) == 0)
             {
				 string set = "basecode='" + code.Value + "',citycode='" + dptown.SelectedItem.Value + "', notes='" + note.Value + "',area='" + dparea.SelectedItem.Text + "',basename='" + name.Value + "',selltype='" + dpselltype.SelectedItem.Value + "',b_status='" + dpstatus.SelectedItem.Value + "',fcode='" + storecode.Value + "',ChinazcomName='" + ChinazcomName.Value+ "',phone='" + phone.Value + "', address='" + address.Value + "'";
				 if (hb.update("BaseStore", hiddID.Value, set))
                 {
                     Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../baseinfo/basezshstore.aspx';</script>");
                 }
                 else
                 {
                     Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../baseinfo/basezshstore.aspx';</script>");
                 }
             }
             else
             {
                 Response.Write("<script type='text/javascript'>alert('编码重复');</script>");
             }
        }
    }
}