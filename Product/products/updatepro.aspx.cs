using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace product.products
{
    public partial class updatepro : Common.BasePage
    {
        protected ProductBLL.Basebll.BaseList hb = new ProductBLL.Basebll.BaseList();
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {

            id = GetQueryString("id");

            if (GetQueryString("type") == "ck")
            {
                Button1.Visible = false;
            }
            if (!IsPostBack)
            {

                DataTable dt = hb.getdate("*", "products where id=" + id);
                if (dt.Rows.Count > 0)
                {


                    rpcode.Value = dt.Rows[0]["rpcode"].ToString();             //睿配编码
                    ShGoogcode.Value = dt.Rows[0]["ShGoogcode"].ToString();     //石化商品编码
                    CAD.Value = dt.Rows[0]["CAD"].ToString();                   //供应商编码
                    pinglei.Value = dt.Rows[0]["pinglei"].ToString();           //品类
                    pingpai.Value = dt.Rows[0]["pingpai"].ToString();           //品牌
                    Dimension.Value = dt.Rows[0]["Dimension"].ToString();       //Dimension
                    AcrossWidth.Value = dt.Rows[0]["AcrossWidth"].ToString();   //横截面宽
                    GKB.Value = dt.Rows[0]["GKB"].ToString();                   //高宽比
                    R.Value = dt.Rows[0]["R"].ToString();                       //R
                    PATTERN.Value = dt.Rows[0]["PATTERN"].ToString();           //花纹
                    Mark.Value = dt.Rows[0]["Mark"].ToString();                 //特别提示
                    LOADINGs.Value = dt.Rows[0]["LOADINGs"].ToString();         //载重指数
                    Segment.Value = dt.Rows[0]["Segment"].ToString();           //Segment
                    EXTRALOAD.Value = dt.Rows[0]["EXTRALOAD"].ToString();       //EXTRA LOAD
                    OE.Value = dt.Rows[0]["OE"].ToString();                     //OE
                    des.Value = dt.Rows[0]["des"].ToString();                   //DES
                    hbcode.Value = dt.Rows[0]["hbcode"].ToString();             //河北海信编码
                    zjcode.Value = dt.Rows[0]["zjcode"].ToString();             //浙江海鼎编码
                    SPEEDJb.Value = dt.Rows[0]["SPEEDJb"].ToString();           //速度级别
                    Rimdia.Value = dt.Rows[0]["Rimdia"].ToString();             //轮毂直径

                }
            }
        }

        void bind()
        {
            //dpcode.DataSource = hb.getdate("basename", "baseinfo where type=1");
            //dpcode.DataTextField = "basename";
            //dpcode.DataBind();
            //dpcode.Items.Insert(0, "请选择");

            //dpCAI.DataSource = hb.getdate("basename", "baseinfo where type=7");
            //dpCAI.DataTextField = "basename";
            //dpCAI.DataBind();
            //dpCAI.Items.Insert(0, "请选择");

            //DPHW.DataSource = hb.getdate("basename", "baseinfo where type=2");
            //DPHW.DataTextField = "basename";
            //DPHW.DataBind();
            //DPHW.Items.Insert(0, "请选择");

            //dpmodel.DataSource = hb.getdate("basename", "baseinfo where type=3");
            //dpmodel.DataTextField = "basename";
            //dpmodel.DataBind();
            //dpmodel.Items.Insert(0, "请选择");



            //dpseason.DataSource = ProductBLL.Search.Searcher.SEASONtable();
            //dpseason.DataTextField = "name";
            //dpseason.DataBind();
            //dpseason.Items.Insert(0, "请选择");





        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            //string images1 = " CAI='" + dpCAI.SelectedItem.Text + "' ,CAD='" + dpcode.SelectedItem.Text + "' ,model='" + dpmodel.SelectedItem.Text + "' ,P='" + dpP.Value + "' ,AcrossWidth='" + dphjmk.Value + "',GKB='" + dpdkb.Value + "' , R='" + dpR.Value + "' , Rimdia='" + dpLGZJ.Value + "' ,PATTERN='" + DPHW.SelectedValue + "',Mark='" + dptbzs.Value + "',LOADINGs='" + dpzzzs.Value + "' ,SPEEDJb='" + dpsdjb.Value + "',EXTRALOAD='" + LOAD.Value + "' ,OE='" + OE.Value + "',[des]='" + dpDES.Value + "',productionplace='" + txplace.Value + "'";
            string images = " rpcode='" + rpcode.Value + "' ,ShGoogcode='" + ShGoogcode.Value + "' ,CAD='" + CAD.Value + "' ,pinglei ='" + pinglei.Value + "', pingpai='" + pingpai.Value + "' , Dimension='" + Dimension.Value + "' ,   AcrossWidth ='" + AcrossWidth.Value + "', GKB='" + GKB.Value + "' ,   R ='" + R.Value + "',   Rimdia ='" + Rimdia.Value + "',  PATTERN ='" + PATTERN.Value + "',  Mark='" + Mark.Value + "' ,  LOADINGs='" + LOADINGs.Value + "' ,   SPEEDJb='" + SPEEDJb.Value + "' ,  EXTRALOAD='" + EXTRALOAD.Value + "' ,Segment='" + Segment.Value + "' ,   OE ='" + OE.Value + "', [des]='" + des.Value + "',[hbcode]='" + hbcode.Value + "',[zjcode]='" + zjcode.Value + "'";

            if (hb.update("products", id, images))
            {
                Response.Write("<script type='text/javascript'>alert('修改成功');window.location.href='../products/productList.aspx';</script>");
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('修改失败');window.location.href='../products/productList.aspx';</script>");
            }
        }

        void panduan()
        {
            //if (dpcode.SelectedItem.Text == "请选择")
            //{
            //    dpcode.SelectedItem.Text = "";
            //}

        }
    }
}