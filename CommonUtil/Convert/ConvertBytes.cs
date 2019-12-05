using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;

namespace CommonUtil
{

    public class ConvertBytes
    {

        /// <summary>
        /// 将Byte[]转换成Base64编码文本
        /// </summary>
        /// <param name="binBuffer">Byte[]</param>
        /// <returns></returns>
        public static string ToBase64(byte[] binBuffer)
        {
            int base64ArraySize = (int)Math.Ceiling(binBuffer.Length / 3d) * 4;
            char[] charBuffer = new char[base64ArraySize];
            Convert.ToBase64CharArray(binBuffer, 0, binBuffer.Length, charBuffer, 0);
            string s = new string(charBuffer);
            return s;
        }

        /// <summary>
        /// 将Byte[]转换成MemoryStream
        /// </summary>
        /// <param name="buffer">Byte[]</param>
        /// <returns></returns>
        public static Stream ToStream(byte[] buffer)
        {
            return new MemoryStream(buffer);
        }

    }
}
