using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Common
{
    /// <summary>
    /// 关系操作表达式
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equal,

        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,

        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan,

        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// 匹配
        /// </summary>
        Match,

        /// <summary>
        /// 不匹配
        /// </summary>
        NotMatch,

        /// <summary>
        /// 前端匹配
        /// </summary>
        MatchPrefix
    }
}
