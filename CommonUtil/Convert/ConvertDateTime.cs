using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Xml;

namespace CommonUtil
{
    public class ConvertDateTime
    {

        #region ToString

        /// <summary>
        /// 将 当前日期 转换为指定格式 String
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static String ToString(string format)
        {
            return ToString(DateTime.Now, format);
        }

        /// <summary>
        /// 将 DateTimeString 转换为指定格式 String
        /// </summary>
        /// <param name="dateString"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static String ToString(string dateString, string format)
        {
            DateTime date = ConvertObject.ToDateTime(dateString);
            return ToString(date, format);
        }

        /// <summary>
        /// 将 DateTime 转换为指定格式 String
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static String ToString(DateTime date, string format)
        {
            return date.ToString(format);
        }

        #endregion

        #region ToYearString

        /// <summary>
        /// 将 当前日期 转换为 ""yyyy" 格式 String
        /// </summary>
        /// <returns></returns>
        public static String ToYearString()
        {
            return ToYearString(DateTime.Now);
        }

        /// <summary>
        /// 将 DateTimeString 转换为 ""yyyy" 格式 String
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static String ToYearString(string dateString)
        {
            DateTime date = ConvertObject.ToDateTime(dateString);
            return ToYearString(date);
        }

        /// <summary>
        /// 将 DateTime 转换为 ""yyyy" 格式 String
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static String ToYearString(DateTime date)
        {
            return date.ToString("yyyy");
        }

        #endregion

        #region ToMonthString

        /// <summary>
        /// 将 当前日期 转换为 ""yyyyMM" 格式 String
        /// </summary>
        /// <returns></returns>
        public static String ToMonthString()
        {
            return ToMonthString(DateTime.Now);
        }

        /// <summary>
        /// 将 DateTimeString 转换为 ""yyyyMM" 格式 String
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static String ToMonthString(string dateString)
        {
            DateTime date = ConvertObject.ToDateTime(dateString);
            return ToMonthString(date);
        }

        /// <summary>
        /// 将 DateTime 转换为 ""yyyyMM" 格式 String
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static String ToMonthString(DateTime date)
        {
            return date.ToString("yyyyMM");
        }

        #endregion

        #region ToDayString

        /// <summary>
        /// 将 当前日期 转换为 "yyyy-MM-dd" 格式 String
        /// </summary>
        /// <returns></returns>
        public static String ToDayString()
        {
            return ToDayString(DateTime.Now);
        }

        /// <summary>
        /// 将 DateTime 转换为 "yyyy-MM-dd" 格式 String
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static String ToDayString(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        #endregion


        #region ToDateString

        /// <summary>
        /// 将 当前日期 转换为 ""yyyy-MM-dd HH:mm:ss" 格式 String
        /// </summary>
        /// <returns></returns>
        public static String ToDateString()
        {
            return ToDateString(DateTime.Now);
        }

        /// <summary>
        /// 将 当前日期 转换为指定格式
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static String ToDateString(string format)
        {
            return ToDateString(DateTime.Now, format);
        }

        /// <summary>
        /// 将 DateTime 转换为 ""yyyy-MM-dd HH:mm:ss" 格式 String
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static String ToDateString(DateTime date)
        {
            string format = "yyyy-MM-dd HH:mm:ss";
            return ToDateString(date, format);
        }

        /// <summary>
        /// 将 DateTime 转换为指定格式
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static String ToDateString(DateTime date, string format)
        {
            return date.ToString(format);
        }

        #endregion


        #region ToQuarterNumber

        /// <summary>
        /// 获取 当前日期 的季度
        /// </summary>
        /// <returns></returns>
        public static int ToQuarterNumber()
        {
            return ToQuarterNumber(DateTime.Now);
        }

        /// <summary>
        /// 获取 DateTimeString 的季度
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static int ToQuarterNumber(string dateString)
        {
            DateTime date = ConvertObject.ToDateTime(dateString);
            return ToQuarterNumber(date);
        }

        /// <summary>
        /// 获取 DateTime 的季度
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int ToQuarterNumber(DateTime date)
        {
            string month = date.ToString("MM");

            switch (month)
            {
                case "01":
                case "02":
                case "03":
                    return 1;

                case "04":
                case "05":
                case "06":
                    return 2;

                case "07":
                case "08":
                case "09":
                    return 3;

                case "10":
                case "11":
                case "12":
                    return 4;
            }

            return 0;
        }

        #endregion


        #region ToYearQuarterString

        /// <summary>
        /// 获取 当前日期 的季度（yyyyQ1）
        /// </summary>
        /// <returns></returns>
        public static string ToYearQuarterString()
        {
            return ToYearQuarterString(DateTime.Now);
        }

        /// <summary>
        /// 获取 monthString 的季度（yyyyQ1）
        /// </summary>
        /// <param name="monthString"></param>
        /// <returns></returns>
        public static string ToYearQuarterString(string monthString)
        {
            if (monthString == "")
            {
                return ToYearQuarterString(DateTime.Now);
            }

            string year = monthString.Substring(0, 4);
            string month = monthString.Substring(4, 2);
            switch (month)
            {
                case "01":
                case "02":
                case "03":
                    return year + "Q" + 1;

                case "04":
                case "05":
                case "06":
                    return year + "Q" + 2;

                case "07":
                case "08":
                case "09":
                    return year + "Q" + 3;

                case "10":
                case "11":
                case "12":
                    return year + "Q" + 4;
            }
            return "";
        }

        /// <summary>
        /// 获取 DateTime 的季度（yyyyQ1）
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToYearQuarterString(DateTime date)
        {
            string year = date.ToString("yyyy");
            string month = date.ToString("MM");

            switch (month)
            {
                case "01":
                case "02":
                case "03":
                    return year + "Q" + 1;

                case "04":
                case "05":
                case "06":
                    return year + "Q" + 2;

                case "07":
                case "08":
                case "09":
                    return year + "Q" + 3;

                case "10":
                case "11":
                case "12":
                    return year + "Q" + 4;
            }

            return "";
        }

        #endregion


        #region ToQuarterMonths

        /// <summary>
        /// 获取当前季度的月份
        /// </summary>
        /// <param name="yearQuarter"></param>
        /// <returns></returns>
        public static string[] ToQuarterMonths(string yearQuarter)
        {
            int yearIndex = ConvertObject.ToInt32(yearQuarter.Substring(0, 4));
            int quarterIndex = ConvertObject.ToInt32(yearQuarter.Substring(5, 1));

            switch (quarterIndex)
            {
                case 1:
                    return new String[] {
                        yearIndex + "01",
                        yearIndex + "02",
                        yearIndex + "03"
                    };
                case 2:
                    return new String[] {
                        yearIndex + "04",
                        yearIndex + "05",
                        yearIndex + "06"
                    };
                case 3:
                    return new String[] {
                        yearIndex + "07",
                        yearIndex + "08",
                        yearIndex + "09"
                    };
                case 4:
                    return new String[] {
                        yearIndex + "10",
                        yearIndex + "11",
                        yearIndex + "12"
                    };
            }
            return new String[] { };
        }

        #endregion


        #region ToWeekDays

        /// <summary>
        /// 获取当前日期所在的周日期
        /// </summary>
        /// <returns></returns>
        public static string[] ToWeekDays(DateTime date)
        {
            // int dateIndex = (int)date.DayOfWeek;
            int dateIndex = ((int)date.DayOfWeek) == 0 ? 7 : (int)date.DayOfWeek;
            String[] weekdays = {
                ConvertDateTime.ToDayString(date.AddDays(1-dateIndex)),
                ConvertDateTime.ToDayString(date.AddDays(2-dateIndex)),
                ConvertDateTime.ToDayString(date.AddDays(3-dateIndex)),
                ConvertDateTime.ToDayString(date.AddDays(4-dateIndex)),
                ConvertDateTime.ToDayString(date.AddDays(5-dateIndex)),
                ConvertDateTime.ToDayString(date.AddDays(6-dateIndex)),
                ConvertDateTime.ToDayString(date.AddDays(7-dateIndex))
            };
            return weekdays;
        }

        #endregion


        #region ToWeekFirstDay

        /// <summary>
        /// 获取日期所在的周一
        /// </summary>
        /// <returns></returns>
        public static DateTime ToWeekMonday(DateTime date)
        {
            int dateIndex = ((int)date.DayOfWeek) == 0 ? 7 : (int)date.DayOfWeek;
            return date.AddDays(1 - dateIndex);
        }

        #endregion


        #region 获取时间间隔

        /// <summary>
        /// 获取当前日期到1970年1月1日之间的毫秒数
        /// </summary>
        /// <returns></returns>
        public static long ToComputerMilliseconds()
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            var timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds; // 相差毫秒数
            return timeStamp;
        }

        /// <summary>
        /// 获取时间间隔
        /// </summary>
        /// <param name="beginTime"></param>
        /// <returns></returns>
        public static string ToTimeSpan(DateTime beginTime)
        {
            TimeSpan span = DateTime.Now - beginTime;
            string strSpan = "";
            strSpan += (span.Days > 0) ? span.Days + "天" : "";
            strSpan += (span.Hours > 0) ? span.Hours + "小时" : "";
            strSpan += (span.Minutes > 0) ? span.Minutes + "分钟" : "";
            strSpan += (span.Seconds > 0) ? span.Seconds + "秒" : "";
            strSpan += (span.Milliseconds > 0) ? span.Milliseconds + "毫秒" : "";
            strSpan += (strSpan == "") ? "0秒" : "";
            return strSpan;
        }

        #endregion

    }
}
