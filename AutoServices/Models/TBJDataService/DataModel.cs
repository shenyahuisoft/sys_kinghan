using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models.TBJDataService
{
    public class DataModel
    {

        private string _msgType;
        private string _companyCode;
        private string _msgVersion;
        private string _terminalNumber;
        private string _temperature;
        private string _humidity;
        private string _PM25;
        private string _PM10;
        private string _RainFall;
        private string _Noise;
        private string _WindSpeed;
        private string _WindDirection;
        private string _DeviceAddress;


        /// <summary>
        /// 数字类型   长度：2 实时数据：01
        /// 默认值：01
        /// </summary>
        public string MsgType
        {
            get { return string.IsNullOrEmpty(_msgType) ? "01" : _msgType; }
            set { _msgType = value; }
        }

        /// <summary>
        /// byte
        /// 公司代码  长度：2 字母缩写，2位
        /// 默认值：WM  
        /// </summary> 
        public string CompanyCode
        {
            get { return string.IsNullOrEmpty(_companyCode) ? "WM" : _companyCode; }
            set { _companyCode = value; }
        }

        /// <summary>
        /// 消息版本 最长10位
        /// 默认值：V1.0
        /// </summary>
        public string MsgVersion
        {
            get { return string.IsNullOrEmpty(_msgVersion) ? "V1.0" : _msgVersion; }
            set { _msgVersion = value; }
        }

        /// <summary>
        /// 终端序列号 最长10位 不能为空，不能有汉字
        /// </summary>
        public string TerminalNumber
        {
            get { return string.IsNullOrEmpty(_terminalNumber) ? "" : _terminalNumber; }
            set { _terminalNumber = value; }
        }


        /// <summary>
        /// 温度 精确到1位小数 .0
        /// 默认值：±N
        /// </summary>
        public string Temperature
        {
            get { return string.IsNullOrEmpty(_temperature) ? "±N" : _temperature; }
            set { _temperature = value; }
        }

        /// <summary>
        /// 湿度 精确到1位小数 .0
        /// 默认值：N
        /// </summary>
        public string Humidity
        {
            get { return string.IsNullOrEmpty(_humidity) ? "N" : _humidity; }
            set { _humidity = value; }
        }

        /// <summary>
        /// PM2.5值 精确到2位小数 .00
        /// 默认值：N
        /// </summary>
        public string PM25
        {
            get { return string.IsNullOrEmpty(_PM25) ? "N" : _PM25; }
            set { _PM25 = value; }
        }

        /// <summary>
        /// PM10 值 精确到2位小数 .00
        /// 默认值：N
        /// </summary>
        public string PM10
        {
            get { return string.IsNullOrEmpty(_PM10) ? "N" : _PM10; }
            set { _PM10 = value; }
        }

        /// <summary>
        /// 雨量 精确到1位小数 .0
        /// 默认值：N
        /// </summary>
        public string RainFall
        {
            get { return string.IsNullOrEmpty(_RainFall) ? "N" : _RainFall; }
            set { _RainFall = value; }
        }

        /// <summary>
        /// 噪音 精确到2位小数 .00
        /// 默认值：N
        /// </summary>
        public string Noise
        {
            get { return string.IsNullOrEmpty(_Noise) ? "N" : _Noise; }
            set { _Noise = value; }
        }


        /// <summary>
        /// 风速 精确到2位小数 .00
        /// 默认值：N
        /// </summary>
        public string WindSpeed
        {
            get { return string.IsNullOrEmpty(_WindSpeed) ? "N" : _WindSpeed; }
            set { _WindSpeed = value; }
        }


        /// <summary>
        /// 风向 ES—表示东南风，8方位
        /// 默认值：ESWN
        /// </summary>
        public string WindDirection
        {
            get { return string.IsNullOrEmpty(_WindDirection) ? "ESWN" : _WindDirection; }
            set { _WindDirection = value; }
        }

        /// <summary>
        /// 设备唯一的编号 最长10位 不能为空
        /// 默认值：N
        /// </summary>
        public string DeviceAddress
        {
            get { return string.IsNullOrEmpty(_DeviceAddress) ? "N" : _DeviceAddress; }
            set { _DeviceAddress = value; }
        }

        /// <summary>
        /// 域1|域2|域3|域4|域5|域6|域7|域8|域9|域10|域11|域12|域13
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}",
                MsgType, CompanyCode, MsgVersion, TerminalNumber, Temperature, Humidity, PM25, PM10, RainFall, Noise, WindSpeed, WindDirection, DeviceAddress);
        }
    }
}
