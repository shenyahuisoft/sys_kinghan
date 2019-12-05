using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class SourceTaskItem : DataBaseSupper
    {
        /// <summary>
        /// 查询语句
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// 存储过程名称
        /// </summary>
        public string ProcName { get; set; }

        /// <summary>
        /// 命令类型（text,storedprocedure）
        /// </summary>
        public string CommandtType { get; set; }
    }
}
