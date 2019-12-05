using System;
using System.Web;

namespace CommonUtil
{
    public class WebHelper
    {
        /// <summary>
        /// 获取Request参数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static string GetRequestString(HttpRequestBase request, string para)
        {
            if (para == "timestamp")
            {
                if (request.QueryString[para] != null)
                {
                    return request.QueryString[para].ToString();
                }
                else if (request.Form[para] != null)
                {
                    return request.Form[para].ToString();
                }
                return "";
            }

            string timestamp = GetRequestString(request, "timestamp");
            if (timestamp != "")
            {
                TimeSpan timeSpan = CommonUtil.ConvertObject.ToDateTime(timestamp) - DateTime.Now;
                if (timeSpan.Hours > 1)
                {
                    return "";
                }
            }

            if (request.QueryString[para] != null)
            {
                return request.QueryString[para].ToString();
            }
            else if (request.Form[para] != null)
            {
                return request.Form[para].ToString();
            }
            else if (request.Params[para] != null)
            {
                return request.Params[para].ToString();
            }
            return "";

        }

        /// <summary>
        /// 获取浏览器的语言
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetRequestLanguage(HttpRequestBase request)
        {
            try
            {
                string languageid = request.UserLanguages[0].ToLower();
                if (languageid == "en-us")
                {
                    return languageid;
                }
            }
            catch { }
            return "zh-cn";
        }

        /// <summary>
        /// 获取站点根路径
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetRootUrl(HttpRequestBase request)
        {
            return "http://" + request.Url.Authority + "/";
        }


        #region useragent

        /// <summary>
        /// 判断浏览器
        /// </summary>
        /// <param name="useragent"></param>
        /// <returns></returns>
        public static string GetBrowser(string useragent)
        {
            string[] strs = useragent.Replace("(", ";").Replace(")", ";").Split(';');//Replace(" ", ";").
            foreach (string str in strs)
            {
                if (str.IndexOf("MSIE") >= 0)
                {
                    return str.Replace("MS", "").Trim();
                }
                if (str.IndexOf("Chrome") >= 0)
                {
                    return "Chrome";
                }
                if (str.IndexOf("Safari") >= 0)
                {
                    return "Safari";
                }
                if (str.IndexOf("Firefox") >= 0)
                {
                    return "Firefox";
                }
                if (str.IndexOf("Opera") >= 0)
                {
                    return "Opera";
                }
            }
            return "others";
        }

        /// <summary>
        /// 判断操作系统平台
        /// </summary>
        /// <param name="useragent"></param>
        /// <returns></returns>
        public static string GetPlatform(string useragent)
        {
            string[] strs = useragent.Replace("(", ";").Replace(")", ";").Split(';');
            foreach (string str in strs)
            {
                if (str.IndexOf("Windows Phone") >= 0)
                {
                    return str.Trim();
                }
                if (str.IndexOf("Windows NT") >= 0)
                {
                    string platform = str.Trim();
                    if (platform == "Windows NT 6.1")
                    {
                        return "Windows 7";
                    }
                    if (platform == "Windows NT 6.0")
                    {
                        return "Vista";
                    }
                    if (platform == "Windows NT 5.2")
                    {
                        return "Windows 2003";
                    }
                    if (platform == "Windows NT 5.1")
                    {
                        return "Windows XP";
                    }
                    return platform;
                }
                if (str.IndexOf("iPhone") >= 0)
                {
                    return "iPhone";
                }
                if (str.IndexOf("iPad") >= 0)
                {
                    return "iPad";
                }
                if (str.IndexOf("Mac") >= 0)
                {
                    return "Mac";
                }
                if (str.IndexOf("Linux") >= 0)
                {
                    return "Linux";
                }
            }
            return "others";
        }

        #endregion
    }



}
