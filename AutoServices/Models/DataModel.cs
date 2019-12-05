using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Models
{
    /// <summary>
    /// 工人信息
    /// </summary>
    public class WorkerInfo
    {
        public WorkerInfo()
        {
            name = "";
            idCardNum = "";
            gender = 0;
            constructionPermitNum = "";
            bankCardNum = "";
            attenNum = "";
            nation = "";
            headPicBase64 = "";
            nativePlace = "";
            phone = "";
            workState = 0;
            entranceTime = "1970-01-01";
        }
        /// <summary>
        /// 工人姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string idCardNum { get; set; }

        /// <summary>
        /// 性别 0女 1男
        /// </summary>
        public int gender { get; set; }

        /// <summary>
        /// 工人所在工地的施工许可证号
        /// </summary>
        public string constructionPermitNum { get; set; }

        /// <summary>
        /// 工人银行卡号
        /// </summary>
        public string bankCardNum { get; set; }

        /// <summary>
        /// 考勤编号
        /// </summary>
        public string attenNum { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string nation { get; set; }

        /// <summary>
        /// 工人头像图片base64数据
        /// </summary>
        public string headPicBase64 { get; set; }

        /// <summary>
        /// 工人籍贯
        /// </summary>
        public string nativePlace { get; set; }

        /// <summary>
        /// 工人手机号
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 工人进退场状态（是否离职），0 已退场 1 已进场
        /// </summary>
        public int workState { get; set; }

        /// <summary>
        /// 进场时间
        /// </summary>
        public string entranceTime { get; set; }
    }

    /// <summary>
    /// 考勤数据
    /// </summary>
    public class AttendanceInfo
    {
        public AttendanceInfo()
        {
            projectName = "";
            constructionPermitNum = "";
            WorkerAttenInfos = new List<WorkerAttenInfo>();
        }
        /// <summary>
        /// 工地名称
        /// </summary>
        public string projectName { get; set; }

        /// <summary>
        /// 工地施工许可证号
        /// </summary>
        public string constructionPermitNum { get; set; }

        /// <summary>
        /// 该工地下的考勤数据集合
        /// </summary>
        public List<WorkerAttenInfo> WorkerAttenInfos { get; set; }
    }

    /// <summary>
    /// 考勤数据
    /// </summary>
    public class WorkerAttenInfo : WorkerAttenInfoBase
    {
        public WorkerAttenInfo() : base()
        {
            recordID = "";
        }
        /// <summary>
        /// 考勤记录id
        /// </summary>
        public string recordID { get; set; }
    }

    /// <summary>
    /// 考勤数据发送数据格式
    /// </summary>
    public class WorkerAttenInfoBase
    {
        public WorkerAttenInfoBase()
        {
            workerName = "";
            idCardNum = "";
            inoutType = 0;
            attenTime = "1970-01-01";
            photoBase64 = "";
            attenCode = "";
        }
        /// <summary>
        /// 工人姓名
        /// </summary>
        public string workerName { get; set; }

        /// <summary>
        /// 工人身份证号
        /// </summary>
        public string idCardNum { get; set; }

        /// <summary>
        /// 进出标识 0进 1出 （否）
        /// </summary>
        public int inoutType { get; set; }

        /// <summary>
        /// 考勤时间，格式[yyyy-MM-dd HH:mm:ss]
        /// </summary>
        public string attenTime { get; set; }

        /// <summary>
        /// 图片base64数据 （否）
        /// </summary>
        public string photoBase64 { get; set; }

        /// <summary>
        /// 工人考勤编号
        /// </summary>
        public string attenCode { get; set; }
    }
}
