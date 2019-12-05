using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;

namespace CommonUtil
{
    public class ConvertString
    {

        /// <summary>
        /// 将类型转换为Boolean
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>转换失败默认返回当前时间</returns>
        public static Boolean ToBoolean(string obj)
        {
            return ToBoolean(obj, false);
        }

        /// <summary>
        /// 将类型转换为Boolean
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败后的默认值</param>
        /// <returns></returns>
        public static Boolean ToBoolean(string obj, Boolean defaultValue)
        {
            Boolean data = false;
            if (Boolean.TryParse(obj, out data))
            {
                return data;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将普通文本转换成Base64编码的文本
        /// </summary>
        /// <param name="value">普通文本</param>
        /// <returns></returns>
        public static string ToBase64(string obj)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(obj));
        }

        /// <summary>
        /// 将普通文本转换成MemoryStream
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static MemoryStream ToStream(string obj)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(obj));
        }

        /// <summary>
        /// 将IP地址转换为长整型数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToIpAddrValue(string obj)
        {
            long result = -1;

            string[] arrnum = obj.Split('.');

            if (arrnum.Length != 4)
            {
                return result;
            }

            int[] ints = { 0, 0, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                ints[i] = ConvertObject.ToInt32(arrnum[i], -1);
                if (ints[i] < 0 && ints[i] > 255)
                {
                    return result;
                }
            }

            result = (long)ints[0] * 256 * 256 * 256 + (long)ints[1] * 256 * 256 + (long)ints[2] * 256 + (long)ints[3];

            return result;
        }

        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="val"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToTruncate(string str, int length)
        {
            int nLength = 0;
            bool isCut = false;
            StringBuilder sb = new StringBuilder();

            int valLength = Regex.Replace(str, "[^\0-\x00ff]", "aa").Length;

            if (valLength > length)
            {
                Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
                char[] stringChar = str.ToCharArray();
                for (int i = 0; i < stringChar.Length; i++)
                {
                    if (regex.IsMatch((stringChar[i]).ToString()))
                    {
                        sb.Append(stringChar[i]);
                        nLength += 2;
                    }
                    else
                    {
                        sb.Append(stringChar[i]);
                        nLength = nLength + 1;
                    }

                    if (nLength > length)
                    {
                        isCut = true;
                        break;
                    }
                }
            }
            if (isCut)
            {
                return sb.ToString() + "...";
            }
            else
            {
                return str;
            }
        }

    }
}
