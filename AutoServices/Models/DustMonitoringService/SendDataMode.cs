using System;
using CommonUtil;

namespace AutoServices.Models
{

    /// <summary>
    /// 发送数据结构
    /// </summary>
    public class SendDataMode
    {
        public SendDataMode()
        {
            HEAD = "15";
            TAIL = "02";
        }
        /// <summary>
        /// 帧头(1 byte) 1  
        /// </summary>
        public string HEAD { get; set; }





        /// <summary>
        /// 控制码(1 byte) 2
        /// </summary>
        public string CMD { get; set; }

        /// <summary>
        /// 帧流水号(1 byte) 3
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 当前帧序号(2bytes) 4
        /// </summary>
        public string CID { get; set; }

        /// <summary>
        /// 总帧数(2bytes) 5
        /// </summary>
        public string TID { get; set; }

        /// <summary>
        /// 数据域长度(2bytes) 6
        /// </summary>
        public string DL { get; set; }

        /// <summary>
        /// 数据域，该区域的字节长度由 DL 域指定。(DL bytes) 7
        /// </summary>
        public string Data { get; set; }




        /// <summary>
        /// 校验码，该值为 CC= SUM % 256，SUM 为除帧头、帧尾和校验码外，其它字节的和。(1 byte) 8
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// 帧尾（ 0x02 ）。(1byte) 9
        /// </summary>
        public string TAIL { get; set; }

        public override string ToString()
        {
            CC = ((Convert.ToByte(CMD, 8) + Convert.ToByte(ID, 8) + Convert.ToByte(CID, 8) + Convert.ToInt16(TypeHelper.ReverseBytesByIndex(TID), 16) + Convert.ToInt16(TypeHelper.ReverseBytesByIndex(DL), 16) + TypeHelper.StringToCharSum(Data)) % 256).ToString("X2");
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", HEAD, CMD, ID, CID, TID, DL, Data, CC, TAIL).ToUpper();
        }
        
    }
}
