using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using AutoServices.Common;
using AutoServices.Models.SaveYCJCService;
using AutoServices.SaveYCJCService01;
using CommonUtil;

namespace AutoServices.Services
{
    /// <summary>
    /// 返回数据格式
    /// </summary>
    public class ResultClass
    {
        public string True { get; set; }

        public string False { get; set; }
    }

    /// <summary>
    /// YCJCService
    /// </summary>
    public class YCJCService
    {
        private static string serviceName = "services/SaveYCJCService";

        private static readonly Dictionary<string, SaveYCJCServiceUrlModel> typeClients = new Dictionary<string, SaveYCJCServiceUrlModel>();

        #region params
        //DevID:|:911000011#|#Time:|:2016-06-29 12:48:33#|#HUMI:|:27#|#TEMP:|:30#|#PRE:|:150#|#WINDD:|:200#|#WINDS:|:3#|#NOISE:|:120#|#PM25:|:45#|#PM10:|:100.00#|#TSP:|:300

        /// <summary>
        /// 设备id  9位编码，前3位由平台提供，后6位随机码保持不重复即可
        /// </summary>
        public static string DevID = "DevID";

        /// <summary>
        /// 时间  yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static string Time = "Time";

        /// <summary>
        /// 湿度 %
        /// </summary>
        public static string HUMI = "HUMI";

        /// <summary>
        /// 温度 ℃
        /// </summary>
        public static string TEMP = "TEMP";

        /// <summary>
        /// 大气压 帕
        /// </summary>
        public static string PRE = "PRE";

        /// <summary>
        /// 风向  0~360（0正北，90正东，180正南，270正西 ）
        /// </summary>
        public static string WINDD = "WINDD";

        /// <summary>
        /// 风速 m/s
        /// </summary>
        public static string WINDS = "WINDS";

        /// <summary>
        /// 噪声 dB
        /// </summary>
        public static string NOISE = "NOISE";

        /// <summary>
        ///  PM2.5  μg/m³(微克/立方米)
        /// </summary>
        public static string PM25 = "PM25";

        /// <summary>
        /// PM10 μg/m³(微克/立方米)
        /// </summary>
        public static string PM10 = "PM10";

        /// <summary>
        /// 总悬浮颗粒 mg/ m³(毫克/立方米)
        /// </summary>
        public static string TSP = "TSP";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        static YCJCService()
        {
            //原始地址调用方式
            //typeClient = new SaveYCJCService01.SaveYCJCServicePortTypeClient().ChannelFactory.CreateChannel;


            //自定义地址调用方式（格式必须和原有的相同）
            List<KeyValuePair<string, string>> webServiceUrls = DataTransferEnvironment.GetInstance().WebServiceUrls;
            string url = "";
            foreach (var item in webServiceUrls)
            {
                url = string.Format("{0}/{1}", item.Value, serviceName);
                typeClients.Add(item.Key, new SaveYCJCServiceUrlModel(url, WcfHelper.CreateWcfServiceByUrl(url, "basicHttpBinding")));
            }
        }

        /// <summary>
        /// 扬尘监控
        /// </summary>
        /// <param name="pairs"></param>
        /// <returns></returns>
        public string SaveYCJC(Dictionary<string, string> pairs, string webServcieKey)
        {
            #region 检测参数的完整性
            //设备id
            if (!pairs.ContainsKey(DevID))
                pairs.Add(DevID, "-1");

            //时间
            if (!pairs.ContainsKey(Time))
                pairs.Add(Time, "-1");

            //湿度
            if (!pairs.ContainsKey(HUMI))
                pairs.Add(HUMI, "-1");

            //温度
            if (!pairs.ContainsKey(TEMP))
                pairs.Add(TEMP, "-1");

            //大气压
            if (!pairs.ContainsKey(PRE))
                pairs.Add(PRE, "-1");

            //风向
            if (!pairs.ContainsKey(WINDD))
                pairs.Add(WINDD, "-1");

            //风速
            if (!pairs.ContainsKey(WINDS))
                pairs.Add(WINDS, "-1");

            //NOISE
            if (!pairs.ContainsKey(NOISE))
                pairs.Add(NOISE, "-1");

            //PM25
            if (!pairs.ContainsKey(PM25))
                pairs.Add(PM25, "-1");

            //PM10
            if (!pairs.ContainsKey(PM10))
                pairs.Add(PM10, "-1");

            //总悬浮颗粒
            if (!pairs.ContainsKey(TSP))
                pairs.Add(TSP, "-1");
            #endregion

            SaveYCJCServicePortType typeClient = null;
            if (typeClients.ContainsKey(webServcieKey))
            {
                typeClient = typeClients[webServcieKey].SaveYCJCServicePortType;
            }
            if (typeClient != null)
            {
                string elements = DataFormatHelper.GetSaveYCJCData(pairs);
                return typeClient.saveYCJC(elements);
            }
            return string.Format("请配置WebServie：{0}的url", webServcieKey);
        }

    }
}
