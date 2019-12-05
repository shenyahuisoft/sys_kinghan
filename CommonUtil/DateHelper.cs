using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonUtil
{
    public class DateHelper
    {

        #region 年度

        #region GetYearString

        /// <summary>
        /// 将 当前日期 转换为 ""yyyy" 格式 String
        /// </summary>
        /// <returns></returns>
        public static String GetYearString()
        {
            return GetYearString(DateTime.Now);
        }

        /// <summary>
        /// 将 DateTime 转换为 ""yyyy" 格式 String
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static String GetYearString(DateTime date)
        {
            return date.ToString("yyyy");
        }

        /// <summary>
        /// 获取yyyy格式的年度
        /// </summary>
        /// <param name="data">yyyy-MM-dd 或 yyyyMM</param>
        /// <returns></returns>
        public static string GetYearString(string data)
        {
            return data.Substring(0, 4);
        }

        #endregion

        #endregion


        #region 月度

        #region GetMonthString

        /// <summary>
        /// 将 当前日期 转换为 ""yyyyMM" 格式 String
        /// </summary>
        /// <returns></returns>
        public static String GetMonthString()
        {
            return GetMonthString(DateTime.Now);
        }

        /// <summary>
        /// 将 DateTimeString 转换为 ""yyyyMM" 格式 String
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static String GetMonthString(string dateString)
        {
            DateTime date = ConvertObject.ToDateTime(dateString);
            return GetMonthString(date);
        }

        /// <summary>
        /// 将 DateTime 转换为 ""yyyyMM" 格式 String
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static String GetMonthString(DateTime date)
        {
            return date.ToString("yyyyMM");
        }

        #endregion

        /// <summary>
        /// 获取月份匹配前缀
        /// </summary>
        /// <param name="month">yyyy-MM</param>
        /// <param name=""></param>
        /// <returns></returns>
        public static string GetMonthPrefix(string month)
        {
            try
            {
                if (month.Length == 6)
                {
                    return month.Substring(0, 4) + "-" + month.Substring(4, 2);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                return DateTime.Now.ToString("yyyy-MM");
            }
        }

        /// <summary>
        /// 获取后一个月
        /// </summary>
        /// <param name="month">当前月</param>
        /// <param name="nexts">后面的月数（1为后一个月，2为后两个月，-1为前一个月）</param>
        /// <returns></returns>
        public static string GetNextMonth(string month, int nexts)
        {
            try
            {
                if (month.Length == 6)
                {
                    int yearIndex = ConvertObject.ToInt32(month.Substring(0, 4));
                    int monthIndex = ConvertObject.ToInt32(month.Substring(4, 2));
                    return GetMonthString(new DateTime(yearIndex, monthIndex, 1).AddMonths(nexts));
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                DateTime today = DateTime.Now;
                return GetMonthString(new DateTime(today.Year, today.Month, 1));
            }
        }

        /// <summary>
        /// 获取月份的第一天
        /// </summary>
        /// <param name="month">yyyyMM</param>
        /// <returns></returns>
        public static DateTime GetMonthFirstday(string month)
        {
            try
            {
                if (month.Length == 6)
                {
                    int yearIndex = ConvertObject.ToInt32(month.Substring(0, 4));
                    int monthIndex = ConvertObject.ToInt32(month.Substring(4, 2));
                    return new DateTime(yearIndex, monthIndex, 1);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                return GetMonthFirstday();
            }
        }

        /// <summary>
        /// 获取月份的最后一天
        /// </summary>
        /// <param name="month">yyyyMM</param>
        /// <returns></returns>
        public static DateTime GetMonthLastday(string month)
        {
            return GetMonthFirstday(month).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 获取当前月份的第一天
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static DateTime GetMonthFirstday()
        {
            DateTime today = DateTime.Now;
            return new DateTime(today.Year, today.Month, 1);
        }

        /// <summary>
        /// 获取当前月份的最后一天
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMonthLastday()
        {
            return GetMonthFirstday().AddMonths(1).AddDays(-1);
        }

        #endregion


        #region 季度

        #region GetQuarterNumber

        /// <summary>
        /// 获取 当前日期 的季度
        /// </summary>
        /// <returns></returns>
        public static int GetQuarterNumber()
        {
            return GetQuarterNumber(DateTime.Now);
        }

        /// <summary>
        /// 获取 DateTimeString 的季度
        /// </summary>
        /// <param name="data">yyyy-MM-dd 或 yyyyMM</param>
        /// <returns></returns>
        public static int GetQuarterNumber(string data)
        {
            if (data.Length == 10)
            {
                DateTime date = ConvertObject.ToDateTime(data);
                return GetQuarterNumber(date);
            }
            else if (data.Length == 6)
            {
                string month = data.Substring(4, 2);
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
            }
            return 0;
        }

        /// <summary>
        /// 获取 DateTime 的季度
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetQuarterNumber(DateTime date)
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

        /// <summary>
        /// 获取当前月处于本季度的第几个月
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetQuarterPosition(DateTime date)
        {
            string month = date.ToString("MM");

            switch (month)
            {
                case "01":
                case "04":
                case "07":
                case "10":
                    return 1;

                case "02":
                case "05":
                case "08":
                case "11":
                    return 2;

                case "03":
                case "06":
                case "09":
                case "12":
                    return 3;
            }

            return 0;
        }

        #endregion


        #region GetYearQuarterString

        /// <summary>
        /// 获取 当前日期 的季度（yyyyQ1）
        /// </summary>
        /// <returns></returns>
        public static string GetYearQuarterString()
        {
            return GetYearQuarterString(DateTime.Now);
        }

        /// <summary>
        /// 获取 monthString 的季度（yyyyQ1）
        /// </summary>
        /// <param name="monthString"></param>
        /// <returns></returns>
        public static string GetYearQuarterString(string monthString)
        {
            if (monthString == "")
            {
                return GetYearQuarterString(DateTime.Now);
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
        public static string GetYearQuarterString(DateTime date)
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


        /// <summary>
        /// 获取季度的第一天
        /// </summary>
        /// <param name="month">2018Q1</param>
        /// <returns></returns>
        public static DateTime GetQuarterFirstday(string quarter)
        {
            if (quarter == "")
            {
                quarter = GetYearQuarterString();
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
        /// 获取当前季度的月份
        /// </summary>
        /// <param name="yearQuarter"></param>
        /// <returns></returns>
        public static string[] GetQuarterMonths(string yearQuarter)
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

        /// <summary>
        /// 获取季度的最后一个月
        /// </summary>
        /// <param name="year"></param>
        /// <param name="quarter"></param>
        /// <returns>yyyyMM</returns>
        public static string GetQuarterLastMonth(int year, int quarter)
        {
            switch (quarter)
            {
                case 1:
                    return year + "03";
                case 2:
                    return year + "06";
                case 3:
                    return year + "09";
                case 4:
                    return year + "12";
            }
            return "";
        }
        #endregion


        #region 周

        /// <summary>
        /// 获取当前日期所在的周日期
        /// </summary>
        /// <returns></returns>
        public static string[] GetWeekDays(DateTime date)
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

        public static DateTime[] GetWeekdays(DateTime date)
        {
            // int dateIndex = (int)date.DayOfWeek;
            int dateIndex = ((int)date.DayOfWeek) == 0 ? 7 : (int)date.DayOfWeek;
            DateTime[] weekdays = {
                date.AddDays(1-dateIndex),
                date.AddDays(2-dateIndex),
                date.AddDays(3-dateIndex),
                date.AddDays(4-dateIndex),
                date.AddDays(5-dateIndex),
                date.AddDays(6-dateIndex),
                date.AddDays(7-dateIndex)
            };
            return weekdays;
        }

        /// <summary>
        /// 获取日期所在的周一
        /// </summary>
        /// <returns></returns>
        public static DateTime GetWeekMonday(DateTime date)
        {
            int dateIndex = ((int)date.DayOfWeek) == 0 ? 7 : (int)date.DayOfWeek;
            return date.AddDays(1 - dateIndex);
        }

        #endregion


        #region 日



        #endregion


        #region 时间

        #region 获取时间间隔

        /// <summary>
        /// 获取当前日期到1970年1月1日之间的毫秒数
        /// </summary>
        /// <returns></returns>
        public static long GetComputerMilliseconds()
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
        public static string GetTimeSpan(DateTime beginTime)
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

        #endregion



        /// <summary>
        /// 获取持续时长的文本显示
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string GetDurationString(int seconds)
        {
            TimeSpan ts = new TimeSpan(0, 0, 1835);
            if (ts.Hours > 0)
            {
                return ts.Hours + "小时" + ts.Minutes + "分" + ts.Seconds + "秒";
            }
            else if (ts.Minutes > 0)
            {
                return ts.Minutes + "分" + ts.Seconds + "秒";
            }
            return seconds + "秒";
        }


        #region 

        /// <summary>
        /// 检查是否临近生日
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
        public static bool CheckNearlyBirthday(string birthday)
        {
            if (birthday.Length == 10)
            {
                DateTime date;
                if (DateTime.TryParse(birthday.ToString(), out date))
                {
                    int diff = date.DayOfYear - DateTime.Now.DayOfYear;
                    if (diff < 7 && diff >= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

    }

}
