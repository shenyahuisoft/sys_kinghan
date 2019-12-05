using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models
{
    /// <summary>
    /// 目标数据库
    /// </summary>
    public class TargetTaskItem: DataBaseSupper
    {

        /// <summary>
        /// 是否追加数据
        /// </summary>
        public string Increment { get; set; }
    }
}
