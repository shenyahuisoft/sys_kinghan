using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models.TBJDataService
{

    /// <summary>
    /// 河南特必佳 数据模型
    /// </summary>
    public class RequstModel
    {

        /// <summary>
        /// 包头 固定为STX
        /// </summary>
        public string Header { get { return "STX"; } }


        /// <summary>
        /// 数据段长度(十进制) 表示此后整个消息体的长度（字节数）。
        /// </summary>
        public string DataLength { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public DataModel DataModel { get; set; }

        public string DataModelStr { get; private set; }


        /// <summary>
        /// 包尾
        /// </summary>
        public string LastData { get { return "ETX\r\n"; } }

        /// <summary>
        /// STX XXX域1|域2|域3|域4|域5|域6|域7|域8|域9|域10|域11|域12|域13 ETX
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public string ToString(DataModel dataModel)
        {
            DataModel = dataModel;
            DataModelStr = DataModel.ToString();
            DataLength = DataModelStr.Length.ToString();

            return string.Format("{0}{1}{2}{3}", Header, DataLength, DataModelStr, LastData);
        }
    }
}
