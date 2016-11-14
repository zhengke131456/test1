using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using ImageWriter;

namespace product.upload
{
    public partial class UpImage : Common.BasePage
    {

        protected string rpcode = "",columnname="";
        protected string xiaotuFile = "", lunboFile = "", zhanshiFile = "", projectpath = "", absolutepath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            rpcode = GetQueryString("rpcode");
            columnname = GetQueryString("columnName");
            #region 获取路径


            try
            {
                projectpath = ConfigurationManager.AppSettings["projectpath"].ToString();
                absolutepath = ConfigurationManager.AppSettings["absolutepath"].ToString();

                xiaotuFile = ConfigurationManager.AppSettings["suolue"].ToString();
                lunboFile = ConfigurationManager.AppSettings["lunbo"].ToString();
                zhanshiFile = ConfigurationManager.AppSettings["zhanshi"].ToString();
            }
            catch
            {
                projectpath = "http://127.0.0.1:8020/webHbulder/img/";
                absolutepath = @"C:\Users\work\Desktop\webHbulder\img\";
                xiaotuFile = "liebiao";
                lunboFile = "lunbo";
                zhanshiFile = "zhanshi";

            }
            #endregion
        }
        protected void ButtonUp_Click(object sender, EventArgs e)
        {
            if (!FileUpload1.HasFile)
            {
                Response.Write("<script>alert('请选择你要上传的图片！')</script>");

            }
            else
            {
                string fileContentType = FileUpload1.PostedFile.ContentType;
                if (fileContentType == "image/bmp" || fileContentType == "image/gif" || fileContentType == "image/jpeg" || fileContentType == "image/png")
                {
                    string name = FileUpload1.PostedFile.FileName;                  // 客户端文件名


                    string xiaotuPath = "";//缩略图绝对路径
                    string lunboPath = "";//轮播图绝对路径
                    string zhanshiPath = "";//展示图绝对路径

                    string newImagName = "";




                    //根据CommandName 列名 更新相应的列
                    switch (columnname)
                    {

                        #region 保存图片


                        case "IconUrl"://xiao图
                            newImagName = rpcode + "X1.jpg";
                            xiaotuPath = absolutepath+ "\\" + xiaotuFile + "\\" + newImagName;
                            string yuantu = absolutepath + "\\yuantu" + "\\" + newImagName;
                            SaveXImag(xiaotuPath, yuantu, newImagName);
                            break;
                        case "ImageUrl1"://轮播图
                            newImagName = rpcode + "L1.jpg";
                            lunboPath = absolutepath + "\\" + lunboFile + "\\" + newImagName;
                            SaveImag(lunboPath, newImagName, "ImageUrl1");
                            break;
                        case "ImageUrl2"://轮播图图
                            newImagName = rpcode + "L2.jpg";
                            lunboPath = absolutepath + "\\" + lunboFile + "\\" + newImagName;
                            SaveImag(lunboPath, newImagName, "ImageUrl2");
                            break;
                        case "ImageUrl3"://轮播图
                            newImagName = rpcode + "L3.jpg";
                            lunboPath = absolutepath + "\\" + lunboFile + "\\" + newImagName;
                            SaveImag(lunboPath, newImagName, "ImageUrl3");
                            break;
                        case "Imgshow1"://展示图
                            newImagName = rpcode + "Z1.jpg";
                            zhanshiPath = absolutepath + "\\" + zhanshiPath + "\\" + newImagName;
                            SaveImag(zhanshiPath, newImagName, "Imgshow1");
                            break;
                        case "Imgshow2"://展示图
                            newImagName = rpcode + "Z2.jpg";
                            zhanshiPath = absolutepath + "\\" + zhanshiPath + "\\" + newImagName;
                            SaveImag(zhanshiPath, newImagName, "Imgshow2");
                            break;
                        case "Imgshow3"://展示图

                            newImagName = rpcode + "Z3.jpg";
                            zhanshiPath = absolutepath + "\\" + zhanshiPath + "\\" + newImagName;
                            SaveImag(zhanshiPath, newImagName, "Imgshow3");
                            break;
                        case "Imgshow4"://展示图
                            newImagName = rpcode + "Z4.jpg";
                            zhanshiPath = absolutepath + "\\" + zhanshiPath + "\\" + newImagName;
                            SaveImag(zhanshiPath, newImagName, "Imgshow4");
                            break;
                        #endregion
                    }


                   



                }
                else
                {
                    Response.Write("<script>alert('文件类型不符！')</script>");
              
                }

            }
        }


        /// <summary>
        /// 图片路径
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <param name="imgName">名字</param>
        /// <param name="columnname">更新的列名</param>
        public void SaveImag(string imagePath, string imgName, string columnname)
        {
            if (!File.Exists(imagePath))//如果不存在就保存
            {

                //先保存名字到数据库
                if (hb.insetpro("UPDATE dbo.ProductImage SET " + columnname + "='" + imgName + "' WHERE ImgrRpcode='" + rpcode + "'"))//数据库保存成功
                {
                    try
                    {
                        FileUpload1.SaveAs(imagePath);                           // 使用 SaveAs 方法保存文件
                        Response.Write("<script>alert('图片成功上传！')</script>");

                      
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('文件上传失败！')</script>");
                      
                    }
                }
                else
                {
                    Response.Write("<script>alert('文件上传服务器失败，请重新上传！')</script>");
                 
                }

            }
            else
            {
                Response.Write("<script>alert('文件已经存在，请先删除后再上传！')</script>");
                
            }
        }

        /// <summary>
        /// 保存缩略图
        /// </summary>
        /// <param name="imagePath">小图</param>
        /// <param name="YimagePath">原图保存路径</param>
        /// <param name="filename"></param>
        public void SaveXImag(string imagePath, string yimagePath, string imgName)
        {
            if (!File.Exists(imagePath))
            {

                //先保存名字到数据库
                if (hb.insetpro("UPDATE dbo.ProductImage SET IconUrl='" + imgName + "' WHERE ImgrRpcode='" + rpcode + "'"))//数据库保存成功
                {



                    try
                    {
                        FileUpload1.SaveAs(yimagePath);                           // 使用 SaveAs 方法保存文件
                        ///imagePath 小图地址
                        ///xiaotu生成缩略图图片地址
                        string xiaotu = @"C:\Users\work\Documents\项目文档\产品图片\111\" + imgName;
                        MakeThumbnail(yimagePath, xiaotu, 100, 100, "Cut"); // 生成缩略图方法
                     
                       
                        ImageManager im = new ImageManager();
                        //调用很简单 im.SaveWatermark(原图地址, 水印地址, 透明度, 水印位置, 边距,保存到的位置); 
                        //im.SaveWatermark(Server.MapPath("images/Img/LTM.jpg"), Server.MapPath("images/Img/shuiyin.png"), 1f, ImageManager.WatermarkPosition.LeftTop, 1, newfileName);
                        im.SaveWatermark(xiaotu, Server.MapPath("../images/Img/shuiyin1.jpg"), 1f, ImageManager.WatermarkPosition.LeftTop, 1, imagePath);
                        File.Delete(xiaotu);
                        Response.Write("<script>alert('图片成功上传！')</script>");
                     

                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('文件上传失败！')</script>");
                        
                    }
                }
                else
                {
                    Response.Write("<script>alert('文件上传服务器失败，请重新上传！')</script>");
                
                }
            }
            else
            {
                Response.Write("<script>alert('文件已经存在，请重命名后上传！')</script>");
              
            }
        }

        //生成缩略图/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalImagePath">原始</param>
        /// <param name="thumbnailPath">缩略</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode"></param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

       
      
    }
}