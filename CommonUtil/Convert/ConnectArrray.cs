using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;

namespace CommonUtil
{

    /// <summary>
    /// 数组连接
    /// </summary>
    public class ConnectArrray
    {

        /// <summary>
        /// 连接String数组
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string[] ByString(params string[][] array)
        {
            int length = 0;
            foreach (string[] arr in array)
            {
                length += arr.Length;
            }

            string[] charConnected = new string[length];

            for (int i = 0; i < array.Length; i++)
            {
                string[] arr = array[i];
                int index = (i == 0) ? 0 : array[i - 1].Length;
                arr.CopyTo(charConnected, index);
            }
            return charConnected;
        }

    }
}
