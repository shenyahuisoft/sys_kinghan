using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtil;

namespace AutoServices.Services
{
    public class RequestHelper
    {
        static RequestHelper()
        {
            HostUrl = ConvertObject.ToString(ConfigurationManager.AppSettings["HostUrl"]);
            UserName = ConvertObject.ToString(ConfigurationManager.AppSettings["UserName"]);
            Password = ConvertObject.ToString(ConfigurationManager.AppSettings["Password"]);
        }
        /// <summary>
        /// HostUrl
        /// </summary>
        public static string HostUrl { get; set; }

        /// <summary>
        /// UserName
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public static string Password { get; set; }



        /// <summary>
        /// 获取post提交的数据
        /// </summary>
        /// <param name="postParams"></param>
        /// <returns></returns>
        public static string GetPostData(ICollection<KeyValuePair<string, dynamic>> postParams)
        {
            int index = 0;
            string postString = "";
            Encoding sendDataEncode = Encoding.GetEncoding("utf-8");
            foreach (KeyValuePair<string, object> de in postParams)
            {
                //把提交按钮中的中文字符转换成url格式，以防中文或空格等信息  

                postString += System.Web.HttpUtility.UrlEncode(de.Key.ToString(), sendDataEncode) + "=" + System.Web.HttpUtility.UrlEncode(de.Value.ToString(), sendDataEncode);
                if (index < postParams.Count() - 1)
                {
                    postString += "&";
                }
                index++;
            }
            return postString;
        }
    }
}
