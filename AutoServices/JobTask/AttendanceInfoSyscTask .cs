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
    /// 同步打卡信息
    /// </summary>
    public class AttendanceInfoSyscTask : BaseJob<AttendanceInfoSyscTask>
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
                //string sqlStr = @"
                //    SELECT DISTINCT ISNULL(Department.ProjectName,'') AS ProjectName,
                //         ISNULL(Department.ConstructionPermitNum,'') AS ConstructionPermitNum,
                //            ISNULL(Users.Names,'') AS Names,
                //            ISNULL(Users.ICNum,'') AS ICNum,
                //            ISNULL(Record.Types,'') AS Types,
                //            ISNULL(CONVERT(varchar(100), Record.OpenTime, 120),'') AS attentime,
                //            ISNULL(Record.Pic,'') AS photo,
                //            ISNULL(Record.OpenID,'') AS attcode,
                //            ISNULL(Record.RecordID,'') AS RecordID
                //    FROM Record
                //            INNER JOIN Users ON Record.OpenID = Users.OpenID 
                //            INNER JOIN Department  ON  Users.DepartmentID = Users.DepartmentID 
                //                AND  Department.DepartmentID > 0
                //    WHERE 
                //             Record.ISAsync = '0' OR Record.ISAsync IS NULL
                //    ";
                string sqlStr = @"
                    SELECT  DISTINCT ISNULL('绿地熹和府项目','') AS ProjectName,
                    ISNULL('登建施字第410185201903070501号','') AS ConstructionPermitNum,
                    ISNULL(Users.Names,'') AS Names,
                    ISNULL(Users.ICNum,'') AS ICNum,
                    ISNULL(Record.Types,'') AS Types,
                    ISNULL(CONVERT(varchar(100), Record.OpenTime, 120),'') AS attentime,
                    ISNULL(Record.Pic,'') AS photo,
                    ISNULL(Record.OpenID,'') AS attcode,
                    ISNULL(Record.RecordID,'') AS RecordID
                    FROM Record
                    INNER JOIN Users ON Record.OpenID = Users.OpenID 
                    INNER JOIN Department  ON  Users.DepartmentID = Users.DepartmentID 
                        AND  Department.DepartmentID > 0  and len(Users.ICNum) > 5

                    WHERE 
                     Record.ISAsync = '0' OR Record.ISAsync IS NULL
                    order by RecordID
                ";
                _log.InfoFormat(DateTime.Now + Environment.NewLine + "AttendanceInfoSyscTask sqlStr:{0}", sqlStr);

                //List<AttendanceItem> attendanceList = Query.ExecuteSQLQuery(sqlStr, SystemEnvironment.Instance.DefaultDataSource).ToList<AttendanceItem>();

                List<AttendanceItem> attendanceList = new SQLHelper(sourceTask.ConnectionString).ExecuteQuery(sqlStr, CommandType.Text).ToList<AttendanceItem>();

                List<AttendanceInfo> workerInfos = attendanceList.GroupBy(x => new { ProjectName = x.ProjectName, ConstructionPermitNum = x.ConstructionPermitNum })
                    .Select(_item => new AttendanceInfo()
                    {
                        projectName = _item.Key.ProjectName,
                        //constructionPermitNum = _item.Key.ConstructionPermitNum,
                        constructionPermitNum = "SGXKZZZ",

                        WorkerAttenInfos = _item.Select(_x => new WorkerAttenInfo()
                        {
                            workerName = _x.Names,//必填
                            idCardNum = string.IsNullOrEmpty(_x.ICNum) ? "410222100909098998" : "",//必填
                            inoutType = (_x.Types == "2") ? 0 : 1,//必填
                            attenTime = _x.attentime,//必填
                            photoBase64 = "",
                            attenCode = _x.attcode,//必填
                            recordID = _x.RecordID//必填
                        }).ToList()
                    }).ToList();

                //工人考勤数据上传
                foreach (AttendanceInfo info in workerInfos)
                {
                    string result = new HttpMethod().SendAttendanceInfo(info, _log, sourceTask.ConnectionString);
                    _log.InfoFormat("AttendanceInfoSyscTask result:{0}", result);
                }
            }
            catch (Exception ex)
            {
                _log.InfoFormat(DateTime.Now + Environment.NewLine + "AttendanceInfoSyscTask 发生异常:{0}", ex.Message);
            }
        }
    }

    public class AttendanceItem
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ConstructionPermitNum { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>

        public string Names { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string ICNum { get; set; }


        public string Types { get; set; }

        public string attentime { get; set; }

        public string photo { get; set; }

        public string attcode { get; set; }

        /// <summary>
        /// 记录id
        /// </summary>
        public string RecordID { get; set; }

    }

}
