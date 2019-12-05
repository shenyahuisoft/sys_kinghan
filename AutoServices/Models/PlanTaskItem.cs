using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models
{
    /// <summary>
    /// 计划任务
    /// </summary>
    public class PlanTaskItem
    {
        /// <summary>
        /// 频率 
        /// </summary>
        public string Frequency { get; set; }

        /// <summary>
        /// 本月第几天
        /// </summary>
        public string MonthDay { get; set; }

        /// <summary>
        /// 本周第几天
        /// </summary>
        public string WeekDay { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// dll名称
        /// </summary>
        public string DllName { get; set; }

        /// <summary>
        /// 类的全路径
        /// </summary>
        public string ClassName { get; set; }
    }
}
