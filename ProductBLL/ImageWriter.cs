using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace ImageWriter
{
    /// <summary>
    /// 图片处理类
    /// </summary>
    public class ImageManager
    {
        #region 变量声明开始
        /// <summary>
        /// 枚举: 水印位置
        /// </summary>
        public enum WatermarkPosition
        {
            /// <summary>
            /// 左上
            /// </summary>
            LeftTop,
            /// <summary>
            /// 左中
            /// </summary>
            Left,
            /// <summary>
            /// 左下
            /// </summary>
            LeftBottom,
            /// <summary>
            /// 正上
            /// </summary>
            Top,
            /// <summary>
            /// 正中
            /// </summary>
            Center,
            /// <summary>
            /// 正下
            /// </summary>
            Bottom,
            /// <summary>
            /// 右上
            /// </summary>
            RightTop,
            /// <summary>
            /// 右中
            /// </summary>
            RightCenter,
            /// <summary>
            /// 右下
            /// </summary>
            RigthBottom
        }
        #endregion 变量声明结束
        #region 构造函数开始
        /// <summary>
        /// 构造函数: 默认
        /// </summary>
        public ImageManager()
        { }
        #endregion 构造函数结束
        #region 私有函数开始
        /// <summary>
        /// 获取: 图片去扩展名(包含完整路径及其文件名)小写字符串
        /// </summary>
        /// <param name="path">图片路径(包含完整路径,文件名及其扩展名): string</param>
        /// <returns>返回: 图片去扩展名(包含完整路径及其文件名)小写字符串: string</returns>
        private string GetFileName(string path)
        {
            return path.Remove(path.LastIndexOf('.')).ToLower();
        }
        /// <summary>
        /// 获取: 图片以'.'开头的小写字符串扩展名
        /// </summary>
        /// <param name="path">图片路径(包含完整路径,文件名及其扩展名): string</param>
        /// <returns>返回: 图片以'.'开头的小写字符串扩展名: string</returns>
        private string GetExtension(string path)
        {
            return path.Remove(0, path.LastIndexOf('.')).ToLower();
        }
        /// <summary>
        /// 获取: 图片以 '.' 开头的小写字符串扩展名对应的 System.Drawing.Imaging.ImageFormat 对象
        /// </summary>
        /// <param name="format">以 '. '开头的小写字符串扩展名: string</param>
        /// <returns>返回: 图片以 '.' 开头的小写字符串扩展名对应的 System.Drawing.Imaging.ImageFormat 对象: System.Drawing.Imaging.ImageFormat</returns>
        private ImageFormat GetImageFormat(string format)
        {
            switch (format)
            {
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".emf":
                    return ImageFormat.Emf;
                case ".exif":
                    return ImageFormat.Exif;
                case ".gif":
                    return ImageFormat.Gif;
                case ".ico":
                    return ImageFormat.Icon;
                case ".png":
                    return ImageFormat.Png;
                case ".tif":
                    return ImageFormat.Tiff;
                case ".tiff":
                    return ImageFormat.Tiff;
                case ".wmf":
                    return ImageFormat.Wmf;
                default:
                    return ImageFormat.Jpeg;
            }
        }
        /// <summary>
        /// 获取: 枚举 Uinatlex.ToolBox.ImageManager.WatermarkPosition 对应的 System.Drawing.Rectangle 对象
        /// </summary>
        /// <param name="positon">枚举 Uinatlex.ToolBox.ImageManager.WatermarkPosition: Uinatlex.ToolBox.ImageManager.WatermarkPosition</param>
        /// <param name="X">原图宽度: int</param>
        /// <param name="Y">原图高度: int</param>
        /// <param name="x">水印宽度: int</param>
        /// <param name="y">水印高度: int</param>
        /// <param name="i">边距: int</param>
        /// <returns>返回: 枚举 Uinatlex.ToolBox.ImageManager.WatermarkPosition 对应的 System.Drawing.Rectangle 对象: System.Drawing.Rectangle</returns>
        private Rectangle GetWatermarkRectangle(WatermarkPosition positon, int X, int Y, int x, int y, int i)
        {
            switch (positon)
            {
                case WatermarkPosition.LeftTop:
                    return new Rectangle(i, i, x, y);
                case WatermarkPosition.Left:
                    return new Rectangle(i, (Y - y) / 2, x, y);
                case WatermarkPosition.LeftBottom:
                    return new Rectangle(i, Y - y - i, x, y);
                case WatermarkPosition.Top:
                    return new Rectangle((X - x) / 2, i, x, y);
                case WatermarkPosition.Center:
                    return new Rectangle((X - x) / 2, (Y - y) / 2, x, y);
                case WatermarkPosition.Bottom:
                    return new Rectangle((X - x) / 2, Y - y - i, x, y);
                case WatermarkPosition.RightTop:
                    return new Rectangle(X - x - i, i, x, y);
                case WatermarkPosition.RightCenter:
                    return new Rectangle(X - x - i, (Y - y) / 2, x, y);
                default:
                    return new Rectangle(X - x - i, Y - y - i, x, y);
            }
        }
        #endregion 私有函数结束
        #region 文字生成开始
        #endregion 文字生成结束
        #region 设置透明度开始
        /// <summary>
        /// 设置: 图片 System.Drawing.Bitmap 对象透明度
        /// </summary>
        /// <param name="sBitmap">图片 System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="transparence">水印透明度(值越高透明度越低,范围在0.0f~1.0f之间): float</param>
        /// <returns>图片 System.Drawing.Bitmap: System.Drawing.Bitmap</returns>
        public Bitmap SetTransparence(Bitmap bm, float transparence)
        {
            if (transparence == 0.0f || transparence == 1.0f)
                throw new ArgumentException("透明度值只能在0.0f~1.0f之间");
            float[][] floatArray = 
            {
                new float[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f }, 
                new float[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f }, 
                new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f }, 
                new float[] { 0.0f, 0.0f, 0.0f, transparence, 0.0f },
                new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f } 
            };
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(new ColorMatrix(floatArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Bitmap bitmap = new Bitmap(bm.Width, bm.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(bm, new Rectangle(0, 0, bm.Width, bm.Height), 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel, imageAttributes);
            graphics.Dispose();
            imageAttributes.Dispose();
            bm.Dispose();
            return bitmap;
        }
        /// <summary>
        ///  设置: 图片 System.Drawing.Bitmap 对象透明度
        /// </summary>
        /// <param name="readpath">图片路径(包含完整路径,文件名及其扩展名): string</param>
        /// <param name="transparence">水印透明度(值越高透明度越低,范围在0.0f~1.0f之间): float</param>
        /// <returns>图片 System.Drawing.Bitmap: System.Drawing.Bitmap</returns>
        public Bitmap SetTransparence(string readpath, float transparence)
        {
            return SetTransparence(new Bitmap(readpath), transparence);
        }
        #endregion 设置透明度结束
        #region 添加水印开始
        /// <summary>
        /// 生成: 原图绘制水印的 System.Drawing.Bitmap 对象
        /// </summary>
        /// <param name="sBitmap">原图 System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="wBitmap">水印 System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="position">枚举 Uinatlex.ToolBox.ImageManager.WatermarkPosition : Uinatlex.ToolBox.ImageManager.WatermarkPosition</param>
        /// <param name="margin">水印边距: int</param>
        /// <returns>返回: 原图绘制水印的 System.Drawing.Bitmap 对象 System.Drawing.Bitmap</returns>
        public Bitmap CreateWatermark(Bitmap sBitmap, Bitmap wBitmap, WatermarkPosition position, int margin)
        {
            Graphics graphics = Graphics.FromImage(sBitmap);
 graphics.DrawImage(wBitmap, GetWatermarkRectangle(position, sBitmap.Width, sBitmap.Height, wBitmap.Width, wBitmap.Height, margin));
 //graphics.DrawImage(wBitmap, GetWatermarkRectangle(position, sBitmap.Width, sBitmap.Height, 40,20, margin));
         
            graphics.Dispose();
            wBitmap.Dispose();
            return sBitmap;
        }
        #endregion 添加水印结束
        #region 图片切割开始
        #endregion 图片切割结束
        #region 图片缩放开始
        #endregion 图片缩放结束
        #region 保存图片到文件开始
        #region 普通保存开始
        /// <summary>
        /// 保存: System.Drawing.Bitmap 对象到图片文件
        /// </summary>
        /// <param name="bitmap">System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="writepath">保存路径(包含完整路径,文件名及其扩展名): string</param>
        public void Save(Bitmap bitmap, string writepath)
        {
            try
            {
                bitmap.Save(writepath, GetImageFormat(GetExtension(writepath)));
                bitmap.Dispose();
            }
            catch
            {
                throw new ArgumentException("图片保存错误");
            }
        }
        /// <summary>
        /// 保存: 对象到图片文件
        /// </summary>
        /// <param name="readpath">原图路径(包含完整路径,文件名及其扩展名): string</param>
        /// <param name="writepath">保存路径(包含完整路径,文件名及其扩展名): string</param>
        public void Save(string readpath, string writepath)
        {
            if (string.Compare(readpath, writepath) == 0)
                throw new ArgumentException("源图片与目标图片地址相同");
            try
            {
                Save(new Bitmap(readpath), writepath);
            }
            catch
            {
                throw new ArgumentException("图片读取错误");
            }
        }
        #endregion 普通保存结束
        
        #region 透明度调整保存开始
        /// <summary>
        /// 保存: 设置透明度的对象到图片文件
        /// </summary>
        /// <param name="sBitmap">图片 System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="transparence">水印透明度(值越高透明度越低,范围在0.0f~1.0f之间): float</param>
        /// <param name="writepath">保存路径(包含完整路径,文件名及其扩展名): string</param>
        public void SaveTransparence(Bitmap bitmap, float transparence, string writepath)
        {
            Save(SetTransparence(bitmap, transparence), writepath);
        }
        /// <summary>
        /// 保存: 设置透明度的象到图片文件
        /// </summary>
        /// <param name="readpath">原图路径(包含完整路径,文件名及其扩展名): string</param>
        /// <param name="transparence">水印透明度(值越高透明度越低,范围在0.0f~1.0f之间): float</param>
        /// <param name="writepath">保存路径(包含完整路径,文件名及其扩展名): string</param>
        public void SaveTransparence(string readpath, float transparence, string writepath)
        {
            Save(SetTransparence(readpath, transparence), writepath);
        }
        #endregion 透明度调整保存结束
        #region 水印图片保存开始
        /// <summary>
        /// 保存: 绘制水印的对象到图片文件
        /// </summary>
        /// <param name="sBitmap">原图 System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="wBitmap">水印 System.Drawing.Bitmap 对象: System.Drawing.Bitmap</param>
        /// <param name="position">枚举 Uinatlex.ToolBox.ImageManager.WatermarkPosition : Uinatlex.ToolBox.ImageManager.WatermarkPosition</param>
        /// <param name="margin">水印边距: int</param>
        /// <param name="writepath">保存路径(包含完整路径,文件名及其扩展名): string</param>
        public void SaveWatermark(Bitmap sBitmap, Bitmap wBitmap, WatermarkPosition position, int margin, string writepath)
        {
            Save(CreateWatermark(sBitmap, wBitmap, position, margin), writepath);
        }
        /// <summary>
        /// 保存: 绘制水印的对象到图片文件
        /// </summary>
        /// <param name="readpath">图片路径(包含完整路径,文件名及其扩展名): string</param>
        /// <param name="watermarkpath">水印图片路径(包含完整路径,文件名及其扩展名): string</param>
        /// <param name="transparence">水印透明度(值越高透明度越低,范围在0.0f~1.0f之间): float</param>
        /// <param name="position">枚举 Uinatlex.ToolBox.ImageManager.WatermarkPosition : Uinatlex.ToolBox.ImageManager.WatermarkPosition</param>
        /// <param name="margin">水印边距: int</param>
        /// <param name="writepath">保存路径(包含完整路径,文件名及其扩展名): string</param>
        public void SaveWatermark(string readpath, string watermarkpath, float transparence, WatermarkPosition position, int margin, string writepath)
        {
            if (string.Compare(readpath, writepath) == 0)
                throw new ArgumentException("源图片与目标图片地址相同");
            if (transparence == 0.0f)
                Save(readpath, writepath);
            else if (transparence == 1.0f)
                SaveWatermark(new Bitmap(readpath), new Bitmap(watermarkpath), position, margin, writepath);
            else
                SaveWatermark(new Bitmap(readpath), SetTransparence(watermarkpath, transparence), position, margin, writepath);
        }
        #endregion 水印图片保存结束
        #region 图片切割保存开始
        #endregion 图片切割保存结束
        #region 图片缩放保存开始
        #endregion 图片缩放保存开始
        #endregion 保存图片到文件结束
    }
}