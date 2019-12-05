using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Common
{
    /// <summary>
    /// 数据类型
    /// </summary>
    internal enum SqlValueTypes
    {
        PrototypeString,
        SimpleQuotesString,
        String,
        BoolToString,
        BoolToInterger,

        /// <summary>
        /// 不被支持的
        /// </summary>
        NotSupport
    }
}
