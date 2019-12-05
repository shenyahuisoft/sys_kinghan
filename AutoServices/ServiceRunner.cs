using AutoServices.Common;
using AutoServices.Models;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Topshelf;
using Topshelf.Logging;

namespace AutoServices
{
    public class ServiceRunner : ServiceControl, ServiceSuspend
    {
        private readonly IScheduler scheduler;
        static readonly LogWriter _log = HostLogger.Get<ServiceRunner>();
        public ServiceRunner()
        {
            //LogHelper.AppLogger.InfoFormat(DateTime.Now.ToString() + "StartService=》Start：开始配置服务");
            _log.InfoFormat(DateTime.Now.ToString() + "StartService=》Start：开始配置服务");

            //创建调度实例
            ISchedulerFactory sf = new StdSchedulerFactory();
            scheduler = sf.GetScheduler();

            //获取配置文件并创建job
            List<TaskModel> taskModels = DataTransferEnvironment.GetInstance().TaskModels;
            IDictionary<string, Tuple<string, string, string>> databaseCollection = DataTransferEnvironment.GetInstance().DatabaseCollection;
            if (taskModels == null)
            {
                //LogHelper.AppLogger.InfoFormat(DateTime.Now.ToString() + "任务不能为空");
                _log.InfoFormat(DateTime.Now.ToString() + "任务不能为空");
                return;
            }
            foreach (var item in taskModels)
            {
                if (item.PlanTask != null)
                {

                    #region cronExpression 执行周期设置
                    //"0/5 * * * * ?"
                    //这些星号由左到右按顺序代表 
                    //：      *    *     *     *    *     *   *
                    //格式： [秒]  [分] [小时] [日] [月]  [周] [年](一般情况下年不指定 为空即可)

                    string hour = "0";//0 - 23
                    string minute = "0";// 0 - 59
                    string second = "0";// 0 - 59 
                    string cronExpression = "";
                    string[] timeArray = item.PlanTask.Time.Split(':');
                    if (timeArray.Length == 1)
                    {
                        if (!string.IsNullOrEmpty(timeArray[0]))
                            hour = timeArray[0];
                    }
                    else if (timeArray.Length == 2)
                    {
                        if (!string.IsNullOrEmpty(timeArray[0]))
                            hour = timeArray[0];

                        if (!string.IsNullOrEmpty(timeArray[1]))
                            minute = timeArray[1];
                    }
                    else if (timeArray.Length == 3)
                    {
                        if (!string.IsNullOrEmpty(timeArray[0]))
                            hour = timeArray[0];

                        if (!string.IsNullOrEmpty(timeArray[1]))
                            minute = timeArray[1];

                        if (!string.IsNullOrEmpty(timeArray[2]))
                            second = timeArray[2];
                    }
                    //1、monthly 月
                    if (item.PlanTask.Frequency == "monthly")
                    {
                        cronExpression = string.Format("{0} {1} {2} {3} * ?", second, minute, hour, string.IsNullOrEmpty(item.PlanTask.MonthDay) ? "?" : item.PlanTask.MonthDay);
                    }
                    //2、weekly 星期
                    else if (item.PlanTask.Frequency == "weekly")
                    {
                        cronExpression = string.Format("{0} {1} {2} 0 0 {3} *", second, minute, hour, string.IsNullOrEmpty(item.PlanTask.WeekDay) ? "?" : item.PlanTask.WeekDay);
                    }
                    //3、daily 天
                    else if (item.PlanTask.Frequency == "daily")
                    {
                        cronExpression = string.Format("{0} {1} {2} * * ?", second, minute, hour);
                    }
                    //4、hourly 小时
                    else if (item.PlanTask.Frequency == "hourly")
                    {
                        cronExpression = string.Format("{0} {1} * * * ?", second, minute);
                        //cronExpression = string.Format("0 0/1 9-17 * * ? ");
                    }
                    //5、minutly 分钟
                    else if (item.PlanTask.Frequency == "minutly")
                    {
                        cronExpression = string.Format("{0} * * * * ?", second);
                    }

                    if (string.IsNullOrEmpty(cronExpression))
                    {
                        continue;
                    }

                    #endregion


                    //Assembly asm = Assembly.LoadFile(item.PlanTask.DllName);//task dll路径
                    Assembly asm = Assembly.GetExecutingAssembly();
                    Type typeofJob = asm.GetType(item.PlanTask.ClassName, false);
                    //创建任务实例
                    JobDataMap jobDataMap = new JobDataMap();

                    jobDataMap.Put("JOBTYPE", item.Type);
                    jobDataMap.Put("SOURCETASK", item.SourceTask);
                    jobDataMap.Put("TARGETTASK", item.TargetTask);
                    jobDataMap.Put("LOGTASK", item.LogTask);

                    IJobDetail job = JobBuilder.Create(typeofJob).SetJobData(jobDataMap)
                        .WithIdentity(new JobKey(item.Name)).Build();
                    ITrigger trigger = TriggerBuilder.Create()
                        .StartAt(DateTime.Now).WithCronSchedule(cronExpression).Build(); //创建触发器实例

                    //绑定触发器和任务
                    scheduler.ScheduleJob(job, trigger);
                }
                if (item.SourceTask != null)
                {
                    item.SourceTask.ConnectionString = databaseCollection[item.SourceTask.DataSource] == null ? "" : databaseCollection[item.SourceTask.DataSource].Item3;
                }
                if (item.TargetTask != null)
                {
                    item.TargetTask.ConnectionString = databaseCollection[item.TargetTask.DataSource] == null ? "" : databaseCollection[item.TargetTask.DataSource].Item3;
                }
                if (item.LogTask != null)
                {
                    item.LogTask.ConnectionString = databaseCollection[item.LogTask.DataSource] == null ? "" : databaseCollection[item.LogTask.DataSource].Item3;
                }
            }

            //CommonHelper.AppLogger.InfoFormat(DateTime.Now.ToString() + "StartService=》Start：开始绑定定时任务");
            //CommonHelper.AppLogger.InfoFormat(DateTime.Now.ToString() + "StartService=》Start：开始监控定时任务");
        }

        public bool Start(HostControl hostControl)
        {
            scheduler.Start();//启动监控
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            scheduler.Shutdown(false);
            return true;
        }

        public bool Continue(HostControl hostControl)
        {
            scheduler.ResumeAll();
            return true;
        }

        public bool Pause(HostControl hostControl)
        {
            scheduler.PauseAll();
            return true;
        }

    }
}
