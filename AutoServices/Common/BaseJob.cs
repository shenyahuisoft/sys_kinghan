using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoServices.Models;
using Quartz;
using Topshelf.Logging;

namespace AutoServices.Common
{
    /// <summary>
    /// DisallowConcurrentExecution 任务同步
    /// </summary>
    [DisallowConcurrentExecution]
    public abstract class BaseJob<T> : IJob where T : class
    {
        public static readonly LogWriter _log = HostLogger.Get<T>();
        public void Execute(IJobExecutionContext context)
        {
            dataMap = context.JobDetail.JobDataMap;

            //database  program
            string jobtype = dataMap.Get("JOBTYPE").ToString();
            SourceTaskItem sourceTask = (SourceTaskItem)dataMap.Get("SOURCETASK");
            TargetTaskItem targetTask = (TargetTaskItem)dataMap.Get("TARGETTASK");
            LogTaskItem logTask = (LogTaskItem)dataMap.Get("LOGTASK");
            if (jobtype == "program")
            {
                ExecuteProgramJob(sourceTask, logTask);
            }
            else if (jobtype == "database")
            {
                ExecuteDataJob(sourceTask, targetTask, logTask);
            }
        }

        public JobDataMap dataMap { get; set; }
        public virtual void ExecuteDataJob(SourceTaskItem sourceTask, TargetTaskItem targetTask, LogTaskItem logTask)
        { }
        public virtual void ExecuteProgramJob(SourceTaskItem sourceTask, LogTaskItem logTask)
        { }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="sqlStr"></param>
        protected void WriteLog(string connectionString, string sqlStr)
        {
            SqlHelper.ExecuteNonQuery(connectionString, sqlStr);
        }
    }
}
