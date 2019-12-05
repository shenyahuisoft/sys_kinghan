using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models.SaveYCJCService
{
    public class BaseDataModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 设备地址
        /// </summary>
        public string deviceAddress { get; set; }

        /// <summary>
        /// 热传感器
        /// </summary>
        public string sensorCount { get; set; }

        /// <summary>
        /// PM25
        /// </summary>
        public string PM25 { get; set; }

        /// <summary>
        /// PM10
        /// </summary>
        public string PM10 { get; set; }

        /// <summary>
        /// 噪音
        /// </summary>
        public string noise { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public string temperature { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public string humidity { get; set; }

        /// <summary>
        /// 风速
        /// </summary>
        public string windSpeed { get; set; }

        /// <summary>
        /// 风力
        /// </summary>
        public string windPower { get; set; }

        /// <summary>
        /// 风向
        /// </summary>
        public string windDirection { get; set; }
        
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime dataTime { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public string transTo { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string transCode { get; set; }

        /// <summary>
        /// 总悬浮颗粒物
        /// </summary>
        public string TSP { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public string latitude { get; set; }

    }
}
