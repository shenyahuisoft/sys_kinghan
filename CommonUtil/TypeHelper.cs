using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtil
{
    /// <summary>
    /// 类型帮助类
    /// </summary>
    public class TypeHelper
    {
        #region 转Int
        /// <summary>
        /// 将string类型转换成int类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int StringToInt(string s, int defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                int result;
                if (int.TryParse(s, out result))
                    return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// 将string类型转换成int类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <returns></returns>
        public static int StringToInt(string s)
        {
            return StringToInt(s, 0);
        }

        /// <summary>
        /// 将object类型转换成int类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ObjectToInt(object o, int defaultValue)
        {
            if (o != null)
                return StringToInt(o.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 将object类型转换成int类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static int ObjectToInt(object o)
        {
            return ObjectToInt(o, 0);
        }

        #endregion
        #region 转ushort

        /// <summary>
        /// 将string类型转换成ushort类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static ushort StringToushort(string s, ushort defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                ushort result;
                if (ushort.TryParse(s, out result))
                    return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// 将string类型转换成ushort类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <returns></returns>
        public static ushort StringToushort(string s)
        {
            return StringToushort(s, 0);
        }


        /// <summary>
        /// 将object类型转换成ushort类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static ushort ObjectToushort(object o, ushort defaultValue)
        {
            if (o != null)
                return StringToushort(o.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 将object类型转换成ushort类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static ushort ObjectToushort(object o)
        {
            return ObjectToushort(o, 0);
        }

        /// <summary>
        /// 将string类型转换成Byte类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static byte StringToByte(string s, byte defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                byte result;
                if (byte.TryParse(s, out result))
                    return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// 将string类型转换成Byte类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <returns></returns>
        public static byte StringToByte(string s)
        {
            return StringToByte(s, 0);
        }

        /// <summary>
        /// 将object类型转换成Byte类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static byte ObjectToByte(object o, byte defaultValue)
        {
            if (o != null)
                return StringToByte(o.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 将object类型转换成Byte类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static byte ObjectToByte(object o)
        {
            return ObjectToByte(o, 0);
        }



        #endregion
        #region 转Bool

        /// <summary>
        /// 将string类型转换成bool类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool StringToBool(string s, bool defaultValue)
        {
            if (s.ToLower() == "false")
                return false;
            else if (s.ToLower() == "true")
                return true;

            return defaultValue;
        }

        /// <summary>
        /// 将string类型转换成bool类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <returns></returns>
        public static bool StringToBool(string s)
        {
            return StringToBool(s, false);
        }

        /// <summary>
        /// 将object类型转换成bool类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool ObjectToBool(object o, bool defaultValue)
        {
            if (o != null)
                return StringToBool(o.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 将object类型转换成bool类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static bool ObjectToBool(object o)
        {
            return ObjectToBool(o, false);
        }

        #endregion

        #region 转DateTime

        /// <summary>
        /// 将string类型转换成datetime类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string s, DateTime defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                DateTime result;
                if (DateTime.TryParse(s, out result))
                    return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将string类型转换成datetime类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string s)
        {
            return StringToDateTime(s, DateTime.Now);
        }

        /// <summary>
        /// 将object类型转换成datetime类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime ObjectToDateTime(object o, DateTime defaultValue)
        {
            if (o != null)
                return StringToDateTime(o.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 将object类型转换成datetime类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static DateTime ObjectToDateTime(object o)
        {
            return ObjectToDateTime(o, DateTime.Now);
        }

        #endregion

        #region 转Decimal

        /// <summary>
        /// 将string类型转换成decimal类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal StringToDecimal(string s, decimal defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                decimal result;
                if (decimal.TryParse(s, out result))
                    return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// 将string类型转换成decimal类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <returns></returns>
        public static decimal StringToDecimal(string s)
        {
            return StringToDecimal(s, 0m);
        }

        /// <summary>
        /// 将object类型转换成decimal类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal ObjectToDecimal(object o, decimal defaultValue)
        {
            if (o != null)
                return StringToDecimal(o.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 将object类型转换成decimal类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static decimal ObjectToDecimal(object o)
        {
            return ObjectToDecimal(o, 0m);
        }

        #endregion

        #region 转Float

        /// <summary>
        /// 将string类型转换成Double类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static Double StringToDouble(string s, Double defaultValue)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                Double result;
                if (Double.TryParse(s, out result))
                    return result;
            }

            return defaultValue;
        }

        /// <summary>
        /// 将string类型转换成Double类型
        /// </summary>
        /// <param name="s">目标字符串</param>
        /// <returns></returns>
        public static Double StringToDouble(string s)
        {
            return StringToDouble(s, 0d);
        }

        /// <summary>
        /// 将object类型转换成Double类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static Double ObjectToDouble(object o, Double defaultValue)
        {
            if (o != null)
                return StringToDouble(o.ToString(), defaultValue);

            return defaultValue;
        }

        /// <summary>
        /// 将object类型转换成Double类型
        /// </summary>
        /// <param name="s">目标对象</param>
        /// <returns></returns>
        public static Double ObjectToDouble(object o)
        {
            return ObjectToDouble(o, 0d);
        }

        #endregion

        #region 网络大小端转化
        /// <summary>
        /// 网络大小端转化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ushort ReverseUShortBytes(ushort value)
        {
            return (ushort)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
        }

        /// <summary>
        /// 网络大小端转化,指定位数的
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte[] ReverseBytesByIndex(byte[] bytes, int start, int len)
        {
            int end = start + len - 1;
            byte tem;
            int i = 0;
            for (int index = start; index < start + len / 2; index++, i++)
            {
                tem = bytes[end - i];
                bytes[end - 1] = bytes[index];
                bytes[index] = tem;
            }
            return bytes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static string ReverseBytesByIndex(string hexString)
        {
            if (hexString.Length==4)
            {
                return hexString.Substring(2, 2) + hexString.Substring(0, 2);
            }
            return "0000";
        }
        #endregion



        /// <summary>
        /// 字符串转换为16进制字符
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符
            {
                result += Convert.ToString(b[i], 16);
            }
            return result;
        }


        /// <summary>
        /// 16进制字符转换为字符串
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string HexStringToString(string hs, Encoding encode)
        {
            string strTemp = "";
            byte[] b = new byte[hs.Length / 2];
            for (int i = 0; i < hs.Length / 2; i++)
            {
                strTemp = hs.Substring(i * 2, 2);
                b[i] = Convert.ToByte(strTemp, 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }




        /// <summary>
        /// 获取字节和
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int StringToCharSum(string hexString)
        {
            int result = 0;
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                result += Convert.ToInt32(hexString.Substring(i * 2, 2), 16);
            return result;
        }
    }
}
