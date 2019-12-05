using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models.MonitoringPollutants
{
    /// <summary>
    /// 数据结构
    /// </summary>
    public class DataModel
    {
        /// <summary>
        /// 请求编码 精确到毫秒的时间戳:QN=YYYYMMDDhhmmsszzz，用来唯一标识一次命令交互
        /// 长度 20
        /// </summary>
        public string QN { get; set; }

        /// <summary>
        /// ST=系统编码, 系统编码取值详见 6.6.1 章节的表 5《系统编码表》  
        /// 工地扬尘污染源  39 
        /// 长度 5
        /// </summary>
        public string ST { get; set; }

        /// <summary>
        /// CN=命令编码, 命令编码取值详见 6.6.5 章节的表 9《命令编码表》
        /// 长度 7
        /// </summary>
        public string CN { get; set; }

        /// <summary>
        /// PW=访问密码
        /// 长度 9
        /// </summary>
        public string PW { get; set; }

        /// <summary>
        /// MN=设备唯一标识，这个标识固化在设备中，用于唯一标识一个设备
        /// 即 MN 由 24 个 0~9，A~F 的字
        /// 长度 27
        /// </summary>
        public string MN { get; set; }

        /// <summary>
        /// Flag=标志位，这个标志位包含标准版本号、是否拆分包、数据是否应答。
        /// 长度 8 整数(0 - 255)
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// PNUM 指示本次通讯中总共包含的包数
        /// 注：不分包时可以没有本字段，与标志位有关
        /// 长度 9
        /// </summary>
        public string PNUM { get; set; }

        /// <summary>
        /// PNO 指示当前数据包的包号
        /// 注：不分包时可以没有本字段，与标志位有关
        /// 长度 8
        /// </summary>
        public string PNO { get; set; }

        /// <summary>
        /// CP=&&数据区&&，数据区定义见 6.3.3 章节
        /// 长度 0≤n≤950
        /// </summary>
        public string CP { get; set; }


        public override string ToString()
        {
            StringBuilder sbResult = new StringBuilder();
            sbResult.AppendFormat("QN={0};ST={1};CN={2};PW={3};MN={4};Flag={5}", QN, ST, CN, PW, MN, Flag);
            if (!string.IsNullOrEmpty(PNUM))
            {
                sbResult.AppendFormat(";PNUM={0}", PNUM);
            }
            if (!string.IsNullOrEmpty(PNO))
            {
                sbResult.AppendFormat(";PNO={0}", PNO);
            }
            sbResult.AppendFormat(";CP={0}", CP);

            return sbResult.ToString();
        }
    }
}
