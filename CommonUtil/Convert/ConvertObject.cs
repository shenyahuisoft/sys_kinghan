using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;

namespace CommonUtil
{
    public class ConvertObject
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
        /// 获取季度的第一天
        /// </summary>
        /// <param name="month">2018Q1</param>
        /// <returns></returns>
        public static DateTime ToQuarterFirstday(string quarter)
        {
            if (quarter == "")
            {
                quarter = ConvertDateTime.ToYearQuarterString();
            }

            int yearIndex = ConvertObject.ToInt32(quarter.Substring(0, 4));
            int quarterIndex = ConvertObject.ToInt32(quarter.Substring(5, 1));
            switch (quarterIndex)
            {
                case 4:
                    return new DateTime(yearIndex, 10, 1);
                case 3:
                    return new DateTime(yearIndex, 7, 1);
                case 2:
                    return new DateTime(yearIndex, 4, 1);
                default:
                    return new DateTime(yearIndex, 1, 1);
            }
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
                string returnStr = Convert.ToString(obj).Replace("\r\n", " ").Trim();
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


        /// <summary>
        /// 将类型转换为Int32
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>如果转换失败默认返回 0 </returns>
        public static Int32 ToInt32(object obj)
        {
            return ToInt32(obj, 0);
        }

        /// <summary>
        /// 将类型转换为Int32
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败后的默认值</param>
        /// <returns></returns>
        public static Int32 ToInt32(object obj, Int32 defaultValue)
        {
            try
            {
                return Int32.Parse(Math.Round(ToDecimal(obj, defaultValue)).ToString());
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 将类型转换为Int32
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>如果转换失败默认返回 0 </returns>
        public static long ToLong(object obj)
        {
            return ToLong(obj, 0);
        }

        /// <summary>
        /// 将类型转换为Int32
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败后的默认值</param>
        /// <returns></returns>
        public static long ToLong(object obj, Int32 defaultValue)
        {
            return long.Parse(Math.Round(ToDecimal(obj, defaultValue)).ToString());
        }

        /// <summary>
        /// 将类型转换为Float
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>如果转换失败默认返回 0 </returns>
        public static float ToFloat(object obj)
        {
            return ToFloat(obj, 0);
        }

        /// <summary>
        /// 将类型转换为Float
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败后的默认值</param>
        /// <returns></returns>
        public static float ToFloat(object obj, float defaultValue)
        {
            float dnum = 0;
            if (float.TryParse(obj.ToString(), out dnum))
            {
                return dnum;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将类型转换为Decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>如果转换失败默认返回 0 </returns>
        public static Decimal ToDecimal(object obj)
        {
            return ToDecimal(obj, 0);
        }

        /// <summary>
        /// 将类型转换为Decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败后的默认值</param>
        /// <returns></returns>
        public static Decimal ToDecimal(object obj, Decimal defaultValue)
        {
            Decimal dnum = 0;
            if (Decimal.TryParse(obj.ToString(), out dnum))
            {
                return dnum;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将类型转换为Double
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>如果转换失败默认返回 0 </returns>
        public static Double ToDouble(object obj)
        {
            return ToDouble(obj, 0);
        }

        /// <summary>
        /// 将类型转换为Decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败后的默认值</param>
        /// <returns></returns>
        public static Double ToDouble(object obj, Double defaultValue)
        {
            Double dnum = 0;
            if (Double.TryParse(obj.ToString(), out dnum))
            {
                return dnum;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将类型转换为DateTime
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>转换失败默认返回当前时间</returns>
        public static DateTime ToDateTime(object obj)
        {
            return ToDateTime(obj, DateTime.Now);
        }

        /// <summary>
        /// 将类型转换为DateTime
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败后的默认值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            DateTime date = DateTime.Now;
            if (DateTime.TryParse(obj.ToString(), out date))
            {
                return date;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将类型转换为Boolean
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Boolean ToBoolean(object obj)
        {
            if (ConvertObject.ToString(obj) == "1")
            {
                return true;
            }
            if (ConvertObject.ToString(obj) == "0")
            {
                return false;
            }
            return ToBoolean(obj, false);
        }

        /// <summary>
        /// 将类型转换为Boolean
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转换失败后的默认值</param>
        /// <returns></returns>
        public static Boolean ToBoolean(object obj, Boolean defaultValue)
        {
            Boolean val = false;
            if (Boolean.TryParse(obj.ToString(), out val))
            {
                return val;
            }
            return defaultValue;
        }

    }
}
