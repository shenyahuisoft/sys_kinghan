using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Drawing.Imaging;

namespace CommonUtil
{

    public class ConvertBitmap
    {

        /// <summary>
        /// 将Stream转换成Byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static MemoryStream ToMemoryStream(Bitmap bitmap, ImageFormat imageFormat)
        {
            System.IO.MemoryStream newStream = new MemoryStream();
            try
            {
                //以指定格式保存图片
                string outputFile = Path.GetTempPath() + Guid.NewGuid().ToString() + "." + imageFormat.ToString();
                bitmap.Save(outputFile, imageFormat);
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
                bitmap.Dispose();
            }
            return newStream;
        }


    }
}
