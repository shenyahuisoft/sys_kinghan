using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;
using System.Drawing;

namespace CommonUtil
{

    public class ConvertBase64
    {

        /// <summary>
        /// 将Base64编码的文本转换成普通文本
        /// </summary>
        /// <param name="base64">Base64编码的文本</param>
        /// <returns></returns>
        public static string ToString(string base64)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(base64));
        }

        /// <summary>
        /// 将Base64编码文本转换成Byte[]
        /// </summary>
        /// <param name="base64">Base64编码文本</param>
        /// <returns></returns>
        public static Byte[] ToBytes(string base64)
        {
            char[] charBuffer = base64.ToCharArray();
            byte[] bytes = Convert.FromBase64CharArray(charBuffer, 0, charBuffer.Length);
            return bytes;
        }

        /// <summary>
        /// 将Base64编码文本转换成Stream
        /// </summary>
        /// <param name="base64">Base64编码文本</param>
        /// <returns></returns>
        public static Stream ToStream(string base64)
        {
            char[] charBuffer = base64.ToCharArray();
            byte[] bytes = Convert.FromBase64CharArray(charBuffer, 0, charBuffer.Length);
            return new MemoryStream(bytes);

        }

        //解析base64编码获取图片
        public static Bitmap Base64ToImg(string base64Code)
        {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64Code));
            return new Bitmap(stream);
        }


    }
}
