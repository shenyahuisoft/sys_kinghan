using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoServices.Services
{
    /// <summary>
    /// 扬尘上传类
    /// </summary>
    public class DustDataUploadService
    {
    }

    public class BaseParam
    {
        public string token { get; set; }

        public string sign { get; set; }

        public object body { get; set; }
    }

    /// <summary>
    /// ReportDust 参数
    /// </summary>
    public class ReportDustParam
    {
        /// <summary>
        /// 数据采集时间，格式 yyyy-MM-dd hh:mm:ss
        /// </summary>
        public string recDate { get; set; }

        /// <summary>
        /// 采集数据设备的设备编号
        /// </summary>
        public string deviceId { get; set; }

        /// <summary>
        /// pm10的值
        /// </summary>
        public int pm10 { get; set; }

        /// <summary>
        /// PM2.5的值
        /// </summary>
        public int pm25 { get; set; }

        /// <summary>
        /// 空气质量指数
        /// </summary>
        public int aqi { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public int temperature { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public int humidity { get; set; }

        /// <summary>
        /// 风速
        /// </summary>
        public int windSpeed { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public int longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public int latitude { get; set; }
    }

    /// <summary>
    /// ReportDb 参数
    /// </summary>
    public class ReportDbParam
    {
        /// <summary>
        /// 数据采集时间，格式 yyyy-MM-dd hh:mm:ss
        /// </summary>
        public string recDate { get; set; }

        /// <summary>
        /// 采集数据设备的设备编号
        /// </summary>
        public string deviceId { get; set; }

        /// <summary>
        /// 噪声分贝值
        /// </summary>
        public int dbVal { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public int minVal { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public int maxVal { get; set; }
    }

    /// <summary>
    /// ReportStaffPunch 参数
    /// </summary>
    public class ReportStaffPunchParam
    {
        /// <summary>
        /// 数据采集时间，格式 yyyy-MM-dd hh:mm:ss
        /// </summary>
        public string recDate { get; set; }

        /// <summary>
        /// 采集数据设备的设备编号
        /// </summary>
        public string deviceId { get; set; }

        /// <summary>
        /// 人员唯一标识
        /// </summary>
        public string unionNo { get; set; }

        /// <summary>
        /// 员工名字
        /// </summary>
        public string staffName { get; set; }

        /// <summary>
        /// 身份证号 最大长度18
        /// </summary>
        public string idCard { get; set; }

        /// <summary>
        /// IC卡卡号
        /// </summary>
        public string icCard { get; set; }

        /// <summary>
        /// 人员类型 【1-管理人员】 【2-劳务人员】
        /// </summary>
        public int staffType { get; set; }

        /// <summary>
        /// 进出状态 【1-进】 【2-出】
        /// </summary>
        public int inOrOut { get; set; }

        /// <summary>
        /// 考勤照片 base64编码图片
        /// </summary>
        public string photo { get; set; }

        /// <summary>
        /// 考勤打卡类型 【1人脸考勤】【2指纹考勤】【3虹膜考勤】 【4IC卡识别】【5视频识别】【ID卡识别】
        /// </summary>
        public int punchType { get; set; }

        /// <summary>
        /// 考勤时间 yyyy-MM-dd hh:mm:ss
        /// </summary>
        public string punchTime { get; set; }

    }

    public class reportTDParam
    {
        /// <summary>
        /// 数据采集时间，格式 yyyy-MM-dd hh:mm:ss
        /// </summary>
        public string recDate { get;set; }

        /// <summary>
        /// 采集数据设备的设备编号
        /// </summary>
        public string deviceId { get; set; }

        /// <summary>
        /// 塔吊的唯一标识
        /// </summary>
        public string creanceId { get; set; }

        /// <summary>
        /// 当前角度
        /// </summary>
        public int maxAngle { get; set; }

        /// <summary>
        /// 回转角度,当前相对于0°的夹角,0-360°
        /// </summary>
        public int rotation { get; set; }

        /// <summary>
        /// 当前力矩
        /// </summary>
        public int ratedKn { get; set; }

        /// <summary>
        /// 当前载重
        /// </summary>
        public int ratedLoad { get; set; }

        /// <summary>
        /// 最大载重
        /// </summary>
        public int maxLoad { get; set; }

        /// <summary>
        /// 工作幅度
        /// </summary>
        public int workRange { get; set; }

        /// <summary>
        /// 工作高度
        /// </summary>
        public int workHeight { get; set; }

        /// <summary>
        /// 工作风速
        /// </summary>
        public int workSpeed { get; set; }

        /// <summary>
        /// 打卡时间 yyyy-MM-dd
        /// </summary>
        public string punchTime { get; set; }

        /// <summary>
        /// 打卡人员
        /// </summary>
        public string punchPerson { get; set; }
    }

    public class reportDeviceTdBaseParam
    {
        /// <summary>
        /// 数据采集时间，格式 yyyy-MM-dd hh:mm:ss
        /// </summary>
        public string recDate { get; set; }

        /// <summary>
        /// 采集数据设备的设备编号
        /// </summary>
        public string deviceId { get; set; }

        /// <summary>
        /// 塔吊的唯一标识
        /// </summary>
        public string creanceId { get; set; }

        /// <summary>
        /// 塔吊名字
        /// </summary>
        public string deviceName { get; set; }

        /// <summary>
        /// 力矩最小值
        /// </summary>
        public int ratedKnStart { get; set; }

        /// <summary>
        /// 力矩最大值
        /// </summary>
        public int ratedKnEnd { get; set; }

        /// <summary>
        /// 载重最小值
        /// </summary>
        public int ratedLoadStart { get; set; }

        /// <summary>
        /// 载重最大值
        /// </summary>
        public int ratedLoadEnd { get; set; }

        /// <summary>
        /// 最大载重
        /// </summary>
        public int maxLoad { get; set; }

        /// <summary>
        /// 倾角最小值
        /// </summary>
        public int angleStart { get; set; }

        /// <summary>
        /// 倾角最大值
        /// </summary>
        public int angleEnd { get; set; }

        /// <summary>
        /// 工作幅度最小值
        /// </summary>
        public int workRangeStart { get; set; }

        /// <summary>
        /// 工作幅度最大值
        /// </summary>
        public int workRangeEnd { get; set; }

        /// <summary>
        /// 工作高度最小值
        /// </summary>
        public int workHeightStart { get; set; }

        /// <summary>
        /// 工作高度范围
        /// </summary>
        public int workHeightEnd { get; set; }

        /// <summary>
        /// 工作风速最小值
        /// </summary>
        public int workSpeedStart { get; set; }

        /// <summary>
        /// 工作风速最大值
        /// </summary>
        public int workSpeedEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        public string coordinate { get; set; }

        /// <summary>
        /// 小臂长
        /// </summary>
        public int shortArm { get; set; }

        /// <summary>
        /// 大臂长
        /// </summary>
        public int longArm { get; set; }

        /// <summary>
        /// 备案日期 yyyy-MM-dd hh:mm:ss
        /// </summary>
        public string filiStartTime { get; set; }

        /// <summary>
        /// 备案有效日期 yyyy-MM-dd hh:mm:ss
        /// </summary>
        public string filiEndTime { get; set; }
    }

    public class reportDeviceSjjParam
    {
        /// <summary>
        /// 升降机的唯一标识,此处需要对应
        /// </summary>
        public string creanceId { get; set; }

        /// <summary>
        /// 数据采集时间，格式 yyyy-MM-dd hh:mm:ss
        /// </summary>
        public string recDate { get; set; }

        public string deviceId { get; set; }
    }

    public class DustDataUploadMethod
    {

        ///// <summary>
        ///// 扬尘数据上传
        ///// </summary>
        ///// <returns></returns>
        //public string ReportDust(ReportDustParam param)
        //{
            
        //}

        ///// <summary>
        ///// 噪声数据上传接口
        ///// </summary>
        ///// <returns></returns>
        //public string ReportDb(ReportDbParam param)
        //{

        //}

        ///// <summary>
        ///// 考勤记录上传接口
        ///// </summary>
        ///// <returns></returns>
        //public string ReportStaffPunch(ReportStaffPunchParam param)
        //{

        //}

        ///// <summary>
        ///// 塔吊据上传接口
        ///// </summary>
        ///// <returns></returns>
        //public string ReportTD()
        //{

        //}

        ///// <summary>
        ///// 塔吊基础信息上传接口
        ///// </summary>
        ///// <returns></returns>
        //public string ReportDeviceTdBase()
        //{

        //}

        ///// <summary>
        ///// 升降机数据上传接口
        ///// </summary>
        ///// <returns></returns>
        //public string ReportDeviceSjj()
        //{

        //}

        ///// <summary>
        ///// 升降机基础数据上传接口
        ///// </summary>
        ///// <returns></returns>
        //public string ReportDeviceSjjBase()
        //{

        //}
    }
}
