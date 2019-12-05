using O2S.Components.PDFRender4NET;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CommonUtil
{

    public class PdfTools
    {

        /// <summary>
        /// 将PDF文档转换为图片的方法【官方方法】
        /// </summary>
        /// <param name="pdfInputPath">PDF文件路径</param>
        /// <param name="imageOutputPath">图片输出路径</param>
        /// <param name="imageName">生成图片的名字</param>
        /// <param name="startPageNum">从PDF文档的第几页开始转换</param>
        /// <param name="endPageNum">从PDF文档的第几页开始停止转换</param>
        /// <param name="imageFormat">设置所需图片格式</param>
        /// <param name="resolution">清晰度，分为10级（数字1-10）</param>
        public static void ConvertPdfToImage(string pdfInputPath, string imageOutputPath,
            string imageName, int startPageNum, int endPageNum, ImageFormat imageFormat, int resolution)
        {
            PDFFile pdfFile = PDFFile.Open(pdfInputPath);
            if (!Directory.Exists(imageOutputPath))
            {
                Directory.CreateDirectory(imageOutputPath);
            }
            // validate pageNum
            if (startPageNum <= 0)
            {
                startPageNum = 1;
            }
            if (endPageNum > pdfFile.PageCount)
            {
                endPageNum = pdfFile.PageCount;
            }
            if (startPageNum > endPageNum)
            {
                int tempPageNum = startPageNum;
                startPageNum = endPageNum;
                endPageNum = startPageNum;
            }
            // start to convert each page
            for (int i = startPageNum; i <= endPageNum; i++)
            {
                Bitmap pageImage = pdfFile.GetPageImage(i - 1, 56 * (int)resolution);
                pageImage.Save(imageOutputPath + imageName + i.ToString() + "." + imageFormat.ToString(), imageFormat);
                pageImage.Dispose();
            }
            pdfFile.Dispose();

            /*
             *  官方调用方式
             *  ConvertPdfToImage("F:\\Events.pdf", "F:\\", "A", 1, 5, ImageFormat.Jpeg, 1);
             */
        }

        /// <summary>
        /// 转换PDF为指定格式的图片
        /// </summary>
        /// <param name="pdfInputStream"></param>
        /// <param name="imageFormat"></param>
        /// <param name="resolution">清晰度，分为10级（数字1-10）</param>
        /// <returns></returns>
        public static IList<Stream> ConvertPdfToImage(Stream inputStream, ImageFormat imageFormat, int resolution)
        {
            PDFFile pdfFile = PDFFile.Open(inputStream);

            IList<Stream> result = new List<Stream>();
            for (int i = 0; i < pdfFile.PageCount; i++)
            {
                Bitmap pageImage = pdfFile.GetPageImage(i, 56 * (int)resolution);
                result.Add(ConvertBitmap.ToMemoryStream(pageImage, imageFormat));
            }
            pdfFile.Dispose();

            return result;
        }

        /// <summary>
        /// 转换PDF为JPG的图片
        /// </summary>
        /// <param name="pdfInputStream"></param>
        /// <param name="resolution">清晰度，分为10级（数字1-10）</param>
        /// <returns></returns>
        public static IList<Stream> ConvertPdfToJpg(Stream inputStream, int resolution)
        {
            return ConvertPdfToImage(inputStream, ImageFormat.Jpeg, resolution);
        }

        /// <summary>
        /// 转换PDF为PNG的图片
        /// </summary>
        /// <param name="pdfInputStream"></param>
        /// <param name="resolution">清晰度，分为10级（数字1-10）</param>
        /// <returns></returns>
        public static IList<Stream> ConvertPdfToPng(Stream inputStream, int resolution)
        {
            return ConvertPdfToImage(inputStream, ImageFormat.Png, resolution);
        }

    }
}

