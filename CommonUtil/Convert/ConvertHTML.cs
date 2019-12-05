using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;

namespace CommonUtil
{
    public class ConvertHTML
    {

        /// <summary>
        /// 将类型转换为String
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>如果转换失败默认返回 空字符串</returns>
        public static string ToString(object obj)
        {
            return ToString(obj, "", 0);
        }

        /// <summary>
        /// 将类型转换为String
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="maxLength">字符串最大长度,0则不做限制</param>
        /// <returns></returns>
        public static string ToString(object obj, int maxLength)
        {
            return ToString(obj, "", maxLength);
        }

        /// <summary>
        /// 将类型转换为String
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败后的默认值</param>
        /// <returns></returns>
        public static string ToString(object obj, string defaultValue)
        {
            return ToString(obj, defaultValue, 0);
        }

        /// <summary>
        /// 将类型转换为String
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败后的默认值</param>
        /// <param name="maxLength">字符串最大长度,0则不做限制</param>
        /// <returns></returns>
        public static string ToString(object obj, string defaultValue, int maxLength)
        {
            try
            {
                if (obj == null)
                {
                    return defaultValue;
                }
                string returnStr = Convert.ToString(obj).Replace("\r\n", " ").Replace("<br>", "\n").Replace("&nbsp;", " ").Trim();
                if (maxLength > 0 && returnStr.Length > maxLength)
                {
                    return returnStr.Substring(0, maxLength);
                }
                return returnStr;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
