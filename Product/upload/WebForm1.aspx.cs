using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ImageWriter;

namespace product.upload
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string newfileName = Server.MapPath("images/Img/") + DateTime.Now.Millisecond + "" + DateTime.Now.Second + ".jpg";
            string newfileName1 = Server.MapPath("images/Img/") + DateTime.Now.Millisecond + "1" + DateTime.Now.Second + ".jpg";
            string shuiyin2 = @"C:\Users\work\Documents\项目文档\产品图片\111\shuiyin2.png";
            ImageManager im = new ImageManager();
            //调用很简单 im.SaveWatermark(原图地址, 水印地址, 透明度, 水印位置, 边距,保存到的位置); 
            //im.SaveWatermark(Server.MapPath("images/Img/LTM.jpg"), Server.MapPath("images/Img/shuiyin.png"), 1f, ImageManager.WatermarkPosition.LeftTop, 1, newfileName);
            im.SaveWatermark(Server.MapPath("images/Img/LTM.jpg"), Server.MapPath("images/Img/shuiyin1.jpg"), 1f, ImageManager.WatermarkPosition.LeftTop, 1, shuiyin2);
        }
    }
}