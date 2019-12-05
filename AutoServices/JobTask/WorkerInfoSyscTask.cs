using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AutoServices.Common;
using AutoServices.Models;
using AutoServices.Services;
using CommonUtil;
using DatabaseLayer;
namespace AutoServices.JobTask
{
    /// <summary>
    /// 同步工人信息
    /// </summary>
    public class WorkerInfoSyscTask : BaseJob<WorkerInfoSyscTask>
    {
        public override void ExecuteProgramJob(SourceTaskItem sourceTask, LogTaskItem logTask)
        {
            _log.InfoFormat(DateTime.Now + Environment.NewLine + "WorkerInfoSyscTask 开始执行");

            Excate(sourceTask);
        }

        /// <summary>
        /// 
        /// </summary>
        private void Excate(SourceTaskItem sourceTask)
        {
            try
            {
                string sqlStr = "SELECT * FROM Users WHERE ISAsync='0'";

                _log.InfoFormat(DateTime.Now + Environment.NewLine + "WorkerInfoSyscTask sqlStr:{0}", sqlStr);

                DataTable dt = new SQLHelper(sourceTask.ConnectionString).ExecuteQuery(sqlStr, CommandType.Text);

                //DataTable dt = Query.ExecuteSQLQuery(sqlStr, SystemEnvironment.Instance.DefaultDataSource);
                List<WorkerInfo> WorkerInfos = dt.AsEnumerable().Select(x => new WorkerInfo()
                {
                    name = ConvertObject.ToString(x["Names"]),
                    idCardNum = ConvertObject.ToString(x["ICNum"]),
                    gender = ConvertObject.ToInt32(x["Sex"]),
                    //constructionPermitNum = "410185201903070501",
                    constructionPermitNum = "SGXKZZZ",
                    bankCardNum = ConvertObject.ToString(x["CardNo"]),
                    attenNum = ConvertObject.ToString(x["No"]),
                    nation = ConvertObject.ToString(x["Nation"]),
                    headPicBase64 = PictureTools.GetBase64String(ConvertObject.ToString(x["Pic"])),//Pic
                    nativePlace = "",
                    phone = ConvertObject.ToString(x["Phone"]),
                    workState = 1,
                    entranceTime = ConvertObject.ToString(x["StartDate"]),
                }).ToList();
                //工人信息上传
                if (WorkerInfos.Count>0)
                {
                    string result = new HttpMethod().SendWorkerInfo(WorkerInfos);
                    _log.InfoFormat("WorkerInfoSyscTask result:{0}", result);

                    sqlStr = string.Format("UPDATE Users SET ISAsync = '1' WHERE ISAsync = '0'");
                    int resultint = new SQLHelper(sourceTask.ConnectionString).ExecuteNonQuery(sqlStr, CommandType.Text);

                    _log.InfoFormat("WorkerInfoSyscTask resultint:{0}", resultint);
                }
            }
            catch (Exception ex)
            {
                _log.InfoFormat(DateTime.Now + Environment.NewLine + "WorkerInfoSyscTask 发生异常:{0}", ex.Message);
            }
        }
    }

}
