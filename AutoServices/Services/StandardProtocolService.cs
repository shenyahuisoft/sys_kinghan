using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Services
{
   public class StandardProtocolService
    {
        /// <summary>
        /// 通讯包组成 （包头(2个字符)、数据段长度(十进制4个字符)、数据段(变长)、CRC校验(16进制整数 4位)、包尾2个字符(CR、LF)）
        /// 
        /// 数据段：（请求编码、系统编码、命令编码、密码、设备唯一标识、标志位、总包数、包号、指令参数）
        /// </summary>
        public StandardProtocolService()
        {

        }
        
    }
}
