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
	public partial class updatestore : Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
      
        public string biaoti ;
         
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if(!IsPostBack)
            {
                bind(); 
                if (!string.IsNullOrEmpty(GetQueryString("id")))
                {
                    hiddID.Value = GetQueryString("id");
					string where = "BaseStore where id=" + hiddID.Value;
                    string cha = "*";
                    DataTable dt = hb.getdate(cha, where);
                  

                    if (dt.Rows.Count > 0)
                    {
                        basename.Value = dt.Rows[0]["basename"].ToString();
                        basecode.Value = dt.Rows[0]["Basecode"].ToString();
                        
                        //dpprovince.SelectedValue = dt.Rows[0]["ProvinceId"].ToString();
                        dptown.SelectedValue = dt.Rows[0]["citycode"].ToString();
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

          

           

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
           
            
            string table;

            table = "BaseStore where (Basecode='" + basecode.Value.Trim() + "'  or  basename='" + basename.Value.Trim() + "') and id!='" + hiddID.Value + "' ";



            string images = "basename='" + basename.Value + "',Basecode='" + basecode.Value + "',citycode='" + dptown.SelectedItem.Value + "' ";
         
            if (hb.GetScalar(table) == 0)
            {
				if (hb.update("BaseStore", hiddID.Value, images))
                {
					Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../baseinfo/baseinfoStore.aspx';</script>");
                }
                else
                {
					Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../baseinfo/baseinfoStore.aspx';</script>");
                }
            }
            else 
            {

				Response.Write("<script type='text/javascript'>alert('(" + basecode + ")已有记录，请核实再上传');window.location.href='../baseinfo/baseinfoStore.aspx';</script>");
              
            }
        }
    }
}