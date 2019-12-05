using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

namespace CommonUtil
{

    /// <summary>
    /// 图片保存格式
    /// </summary>
    public enum PictureFormat
    {
        /// <summary>
        /// BMP
        /// </summary>
        BMP,

        /// <summary>
        /// JPEG
        /// </summary>
        JPG,

        /// <summary>
        /// PNG
        /// </summary>
        PNG,

        /// <summary>
        /// GIF
        /// </summary>
        GIF
    }


    /// <summary>
    /// PictureTools 的摘要说明
    /// </summary>
    public class PictureTools
    {

        /// <summary>
        /// 获取ImageFormat
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private static System.Drawing.Imaging.ImageFormat GetImageFormat(PictureFormat format)
        {
            if (format == PictureFormat.BMP) return System.Drawing.Imaging.ImageFormat.Bmp;
            if (format == PictureFormat.GIF) return System.Drawing.Imaging.ImageFormat.Gif;
            if (format == PictureFormat.JPG) return System.Drawing.Imaging.ImageFormat.Jpeg;
            if (format == PictureFormat.PNG) return System.Drawing.Imaging.ImageFormat.Png;
            return System.Drawing.Imaging.ImageFormat.Bmp;
        }


        /*

        /// <summary>
        /// 格式化图片
        /// </summary>
        /// <param name="originalImagePath">源图物理路径</param>
        /// <param name="formatImagePath">新图物理路径</param>
        /// <param name="formatWidth">格式化宽度</param>
        /// <param name="formatHeight">格式化高度</param>
        /// <param name="cut">是否裁剪，true:裁剪、false:不裁剪</param>
        /// <param name="picFormat">图片输出格式</param>
        public static void FormatPicture(string originalPath, string formatPath, int formatWidth, int formatHeight, bool cut, PictureFormat picFormat)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalPath);

            int towidth = formatWidth;
            int toheight = formatHeight;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            if (cut)
            {
                //指定高宽裁减（不变形） 
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
                    oh = originalImage.Width * formatHeight / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
            }
            else
            {
                //默认指定高度宽度，失真缩放

                //指定宽度缩放
                if (formatHeight <= 0)
                {
                    formatWidth = originalImage.Width * formatHeight / originalImage.Height;
                }

                //指定高度缩放
                else if (formatWidth <= 0)
                {
                    formatHeight = originalImage.Height * formatWidth / originalImage.Width;
                }
            }

            //新建一个bmp图片 
            System.Drawing.Image image = new System.Drawing.Bitmap(formatWidth, formatHeight);
            //新建一个画板 
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image);
            //设置高质量插值法 
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度 
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充 
            graphics.Clear(System.Drawing.Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分 
            graphics.DrawImage(
                originalImage,
                new System.Drawing.Rectangle(0, 0, formatWidth, formatHeight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel
            );
            originalImage.Dispose();

            System.Drawing.Imaging.ImageFormat imageFormat = GetImageFormat(picFormat);

            //以指定格式保存图片
            try
            {
                image.Save(formatPath, imageFormat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //originalImage.Dispose();
                image.Dispose();
                graphics.Dispose();
            }
        }
        
        */



        /// <summary>
        /// 格式化图片
        /// </summary>
        /// <param name="originalImagePath">源图物理路径</param>
        /// <param name="formatImagePath">新图物理路径</param>
        /// <param name="formatWidth">格式化宽度</param>
        /// <param name="formatHeight">格式化高度</param>
        /// <param name="cut">是否裁剪，true:裁剪、false:不裁剪</param>
        /// <param name="picFormat">图片输出格式</param>
        public static void FormatPicture(string originalPath, string formatPath, int formatWidth, int formatHeight, bool cut, PictureFormat picFormat)
        {
            Byte[] fileBinary = File.ReadAllBytes(originalPath);

            Byte[] formatBinary = FormatPicture(fileBinary, formatWidth, formatHeight, cut, picFormat);
            if (formatBinary != null)
            {
                FileConvert.SaveFile(formatPath, formatBinary);
            }
        }


        /// <summary>
        /// 格式化图片
        /// </summary>
        /// <param name="fileBinary">源图片二进制</param>
        /// <param name="formatWidth">格式化宽度</param>
        /// <param name="formatHeight">格式化高度</param>
        /// <param name="cut">是否裁剪，true:裁剪、false:不裁剪</param>
        /// <param name="picFormat">图片输出格式</param>
        /// <returns>新图片二进制</returns>
        public static Byte[] FormatPicture(Byte[] fileBinary, int formatWidth, int formatHeight, bool cut, PictureFormat picFormat)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(new MemoryStream(fileBinary));

            if (formatWidth > 0 && formatHeight == 0)
            {
                formatHeight = ConvertObject.ToInt32(Math.Round(formatWidth * (float)originalImage.Height / originalImage.Width));
            }
            if (formatHeight > 0 && formatWidth == 0)
            {
                formatWidth = ConvertObject.ToInt32(Math.Round(formatHeight * (float)originalImage.Width / originalImage.Height));
            }

            int towidth = formatWidth;
            int toheight = formatHeight;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;



            if (cut)
            {
                //指定高宽裁减（不变形） 
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
                    oh = originalImage.Width * formatHeight / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
            }
            else
            {
                //默认指定高度宽度，失真缩放

                //指定宽度缩放
                if (formatHeight <= 0)
                {
                    formatWidth = originalImage.Width * formatHeight / originalImage.Height;
                }

                //指定高度缩放
                else if (formatWidth <= 0)
                {
                    formatHeight = originalImage.Height * formatWidth / originalImage.Width;
                }
            }

            //新建一个bmp图片 
            System.Drawing.Image image = new System.Drawing.Bitmap(formatWidth, formatHeight);
            //新建一个画板 
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image);
            //设置高质量插值法 
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度 
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充 
            graphics.Clear(System.Drawing.Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分 
            graphics.DrawImage(
                originalImage,
                new System.Drawing.Rectangle(0, 0, formatWidth, formatHeight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel
            );
            originalImage.Dispose();

            System.Drawing.Imaging.ImageFormat imageFormat = GetImageFormat(picFormat);

            //以指定格式保存图片
            try
            {
                MemoryStream stream = new MemoryStream();
                image.Save(stream, imageFormat);
                return stream.GetBuffer();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //originalImage.Dispose();
                image.Dispose();
                graphics.Dispose();
            }
        }


        /// <summary>
        /// 转换图片格式
        /// </summary>
        /// <param name="originalStream">源图片流</param>
        /// <param name="newFormat">图片输出格式</param>
        /// <returns>新图片流</returns>
        public static Stream FormatPicture(Stream originalStream, PictureFormat newFormat)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(originalStream);
            System.Drawing.Imaging.ImageFormat imageFormat = GetImageFormat(newFormat);
            System.IO.MemoryStream newStream = new MemoryStream();

            try
            {
                //以指定格式保存图片
                string outputFile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
                originalImage.Save(outputFile, imageFormat);
                FileStream fs = File.Open(outputFile, FileMode.Open);
                newStream = new MemoryStream(ConvertStream.ToBuffer(fs));
                fs.Close();
                File.Delete(outputFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                originalImage.Dispose();
            }

            return newStream;
        }


        /// <summary>
        /// 转换图片格式
        /// </summary>
        /// <param name="originalBase64">源图片Base64</param>
        /// <param name="newFormat">图片输出格式</param>
        /// <returns>新图片流</returns>
        public static Stream FormatPicture(string originalBase64, PictureFormat newFormat)
        {
            char[] charBuffer = originalBase64.ToCharArray();
            byte[] bytes = Convert.FromBase64CharArray(charBuffer, 0, charBuffer.Length);
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(new MemoryStream(bytes));
            System.Drawing.Imaging.ImageFormat imageFormat = GetImageFormat(newFormat);
            System.IO.MemoryStream newStream = new MemoryStream();

            try
            {
                //以指定格式保存图片
                string outputFile = Path.GetTempPath() + Guid.NewGuid().ToString() + "." + newFormat.ToString();
                originalImage.Save(outputFile, imageFormat);
                FileStream fs = File.Open(outputFile, FileMode.Open);
                newStream = new MemoryStream(ConvertStream.ToBuffer(fs));
                fs.Close();
                File.Delete(outputFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                originalImage.Dispose();
            }

            return newStream;
        }

        /// <summary>
        /// 转换图片格式
        /// </summary>
        /// <param name="originalStream">源图片流</param>
        /// <param name="formatWidth">格式化宽度</param>
        /// <param name="formatHeight">格式化高度</param>
        /// <param name="cut">是否裁剪，true:裁剪、false:不裁剪</param>
        /// <param name="newFormat">图片输出格式</param>
        /// <returns>新图片流</returns>
        public static Stream FormatPicture(Stream originalStream, int formatWidth, int formatHeight, bool cut, PictureFormat newFormat)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(originalStream);

            if (formatWidth > 0 && formatHeight == 0)
            {
                formatHeight = ConvertObject.ToInt32(Math.Round(formatWidth * (float)originalImage.Height / originalImage.Width));
            }
            if (formatHeight > 0 && formatWidth == 0)
            {
                formatWidth = ConvertObject.ToInt32(Math.Round(formatHeight * (float)originalImage.Width / originalImage.Height));
            }

            int towidth = formatWidth;
            int toheight = formatHeight;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            if (cut)
            {
                //指定高宽裁减（不变形） 
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
                    oh = originalImage.Width * formatHeight / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
            }
            else
            {
                //默认指定高度宽度，失真缩放

                //指定宽度缩放
                if (formatHeight <= 0)
                {
                    formatWidth = originalImage.Width * formatHeight / originalImage.Height;
                }

                //指定高度缩放
                else if (formatWidth <= 0)
                {
                    formatHeight = originalImage.Height * formatWidth / originalImage.Width;
                }
            }

            //新建一个bmp图片 
            System.Drawing.Image image = new System.Drawing.Bitmap(formatWidth, formatHeight);
            //新建一个画板 
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image);
            //设置高质量插值法 
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度 
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充 
            graphics.Clear(System.Drawing.Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分 
            graphics.DrawImage(
                originalImage,
                new System.Drawing.Rectangle(0, 0, formatWidth, formatHeight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel
            );
            originalImage.Dispose();

            System.Drawing.Imaging.ImageFormat imageFormat = GetImageFormat(newFormat);

            MemoryStream newStream = new MemoryStream();

            try
            {
                //以指定格式保存图片
                string outputFile = Path.GetTempPath() + Guid.NewGuid().ToString();// +".jpg";
                image.Save(outputFile, imageFormat);
                FileStream fs = File.Open(outputFile, FileMode.Open);
                newStream = new MemoryStream(ConvertStream.ToBuffer(fs));
                fs.Close();
                File.Delete(outputFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                originalImage.Dispose();
            }

            return newStream;
        }

        /// <summary>
        /// 将图片转化为base64
        /// </summary>
        /// <param name="imgagePath"></param>
        /// <returns></returns>
        public static string GetBase64String(string imgagePath)
        {
            if (string.IsNullOrEmpty(imgagePath))
            {
                return "";
            }
            Byte[] image;
            using (FileStream fs = File.OpenRead(imgagePath))
            {
                int filelength = 0;
                filelength = (int)fs.Length; //获得文件长度 
                image = new Byte[filelength]; //建立一个字节数组 
                fs.Read(image, 0, filelength); //按字节流读取 
            }
            return Convert.ToBase64String(image);
        }
    }

}