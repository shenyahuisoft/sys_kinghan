using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models
{
    public enum ResultCode
    {
        /// <summary>
        /// 正确、无错误
        /// </summary>
        Success,

        /// <summary>
        /// 服务器没有找到数据帧
        /// </summary>
        NotFinddatafram,

        /// <summary>
        /// 未登录
        /// </summary>
        NoLogin,

        /// <summary>
        /// 服务器内部异常
        /// </summary>
        SystemError,

        /// <summary>
        /// 数据格式错误
        /// </summary>
        DataRormateError,

        /// <summary>
        /// 未定义
        /// </summary>
        None
    }
}
