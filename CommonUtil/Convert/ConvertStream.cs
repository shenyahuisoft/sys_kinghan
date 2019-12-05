using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;

namespace CommonUtil
{

    public class ConvertStream
    {

        /// <summary>
        /// 将Stream转换成Byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToBuffer(Stream stream)
        {
            int streamLength = (int)stream.Length;
            byte[] buffer = new byte[streamLength];
            stream.Read(buffer, 0, streamLength);
            stream.Close();
            return buffer;
        }


    }
}
