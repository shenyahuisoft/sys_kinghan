using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models
{
    public class TaskModel
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否可用 1:可用
        /// </summary>
        public string Enabled { get; set; }

        /// <summary>
        /// 任务类型 dataBase、program
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 任务计划
        /// </summary>
        public PlanTaskItem PlanTask { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        public SourceTaskItem SourceTask { get; set; }

        /// <summary>
        /// 目标数据
        /// </summary>
        public TargetTaskItem TargetTask { get; set; }

        /// <summary>
        /// 日志数据库
        /// </summary>
        public LogTaskItem LogTask { get; set; }

    }

}
