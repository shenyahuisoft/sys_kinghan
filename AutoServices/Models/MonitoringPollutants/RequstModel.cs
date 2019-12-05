using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServices.Common;

namespace AutoServices.Models.MonitoringPollutants
{
    /// <summary>
    /// 数据包结构
    /// ## 0101 QN=20160801085857223;ST=32;CN=1062;PW=100000;MN=010000A8900016F000169DC0;Flag=5;CP=&&RtdInterval=30&& 1C80 \r\n
    /// </summary>
    public class RequstModel
    {
        /// <summary>
        /// 
        /// </summary>
        private DataModel _dataModel { get; set; }

        /// <summary>
        /// 包头 固定为## 
        /// 长度 2
        /// </summary>
        public string Header { get { return "##"; } }

        /// <summary>
        /// 数据段长度(十进制)
        /// 长度 4   如:0225
        /// </summary>
        public string DataLength { get; set; }

        /// <summary>
        /// 数据
        /// 长度 0≤n≤1024
        /// </summary>
        public string Data
        {
            get
            {
                if (_dataModel != null)
                {
                    return _dataModel.ToString();
                }
                return "";
            }
        }

        /// <summary>
        /// 数据段的校验结果 如果 CRC 错误，执行结束
        /// 长度 4
        /// </summary>
        public string CRCData { get; set; }

        /// <summary>
        /// 固定为<CR><LF>（回车、换行）
        /// 长度 2
        /// </summary>
        public string LastData { get { return "\r\n"; } }

        /// <summary>
        /// 获取请求包体
        /// </summary>
        /// <returns></returns>
        public string ToString(DataModel dataModel)
        {
            _dataModel = dataModel;
            DataLength = Data.Length.ToString().PadLeft(4, '0');
            CRCData = CRC.ToCRC16(Data, Encoding.ASCII, false);

            return string.Format("{0}{1}{2}{3}{4}", Header, DataLength, Data, CRCData, LastData);
        }
    }
}
